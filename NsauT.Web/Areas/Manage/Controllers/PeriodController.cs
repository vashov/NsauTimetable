using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NsauT.Shared.Enums;
using NsauT.Web.Areas.Manage.Helpers;
using NsauT.Web.Areas.Manage.Models.PeriodController;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Linq;

namespace NsauT.Web.Areas.Manage.Controllers
{
    [Area("manage")]
    public class PeriodController : Controller
    {
        private TimetableContext _context;

        public PeriodController(TimetableContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Period(int? id, [FromQuery] int dayId)
        {
            if (dayId == 0)
            {
                return BadRequest();
            }
            if (id == null && dayId == 0)
            {
                return BadRequest();
            }

            PeriodEntity periodEntity = null;

            if (id != null)
            {
                periodEntity = _context.Periods
                    .AsNoTracking()
                    .FirstOrDefault(t => t.Id == id);

                if (periodEntity == null)
                {
                    return NotFound();
                }
            }

            var periodViewModel = new PeriodViewModel(periodEntity, dayId);

            if (id == null)
            {
                return View(periodViewModel);
            }

            if (periodEntity == null)
            {
                return NotFound();
            }

            return View(periodViewModel);
        }

        [HttpPost]
        public IActionResult Period(int? id, [FromQuery] int dayId, [FromForm]PeriodBindingModel period)
        {
            if (dayId == 0)
            {
                return BadRequest();
            }

            PeriodNumber periodNumber = PeriodNumbersHelper.ConvertStringToPeriodNumber(period.Number);
            PeriodOption periodOption = PeriodOptionsHelper.ConvertStringToPeriodOption(period.Option);

            PeriodValidator.Create(ModelState).ValidateAndUpdateModelState(period, periodOption);

            if (!ModelState.IsValid)
            {
                var periodViewModel = new PeriodViewModel(period, id, dayId);
                return View(periodViewModel);
            }

            PeriodEntity periodEntity = null;

            if (id == null)
            {
                periodEntity = new PeriodEntity();
            }
            else
            {
                periodEntity = _context.Periods
                    .FirstOrDefault(p => p.Id == id);

                if (periodEntity == null)
                {
                    return NotFound();
                }
            }

            periodEntity.Cabinet = period.Cabinet;
            periodEntity.Number = periodNumber;
            periodEntity.Subgroup = period.Subgroup;
            periodEntity.IsLecture = period.IsLecture;
            periodEntity.Option = periodOption;
            periodEntity.OptionDate = period.OptionDate;
            periodEntity.OptionCabinet = period.OptionCabinet;
            periodEntity.IsApproved = true;

            if (id == null)
            {
                SchoolDayEntity day = _context.SchoolDays
                    .Include(d => d.Periods)
                    .FirstOrDefault(d => d.Id == dayId);

                if (day == null)
                {
                    return NotFound();
                }

                day.Periods.Add(periodEntity);
            }
            else
            {
                _context.Periods.Update(periodEntity);
            }

            _context.SaveChanges();
            id = periodEntity.Id;

            var idPair = _context.SchoolDays
                .Include(d => d.Subject)
                .Where(d => d.Id == dayId)
                .Select(d => new { dayId = d.Id, subjectId = d.Subject.Id })
                .First();

            int schoolDayId = idPair.dayId;
            int subjectId = idPair.subjectId;

            Approver.Create(_context).CascadeUpdateApprovedDay(schoolDayId);

            _context.SaveChanges();


            return RedirectToSubject(subjectId);
        }

        public IActionResult ApprovePeriod(int id)
        {
            PeriodEntity period = _context.Periods
                .Include(p => p.SchoolDay)
                .ThenInclude(d => d.Subject)
                .FirstOrDefault(p => p.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            int subjectId = period.SchoolDay.Subject.Id;
            int dayId = period.SchoolDay.Id;

            period.IsApproved = true;
            Approver.Create(_context).CascadeUpdateApprovedDay(dayId);

            _context.SaveChanges();

            return RedirectToSubject(subjectId);
        }

        public IActionResult DeletePeriod(int id)
        {
            PeriodEntity period = _context.Periods
                .Include(p => p.SchoolDay)
                .ThenInclude(d => d.Subject)
                .FirstOrDefault(p => p.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            int subjectId = period.SchoolDay.Subject.Id;
            int dayId = period.SchoolDay.Id;

            _context.Periods.Remove(period);
            Approver.Create(_context).CascadeUpdateApprovedDay(dayId);

            _context.SaveChanges();

            return RedirectToSubject(subjectId);
        }

        private RedirectToActionResult RedirectToSubject(int subjectId)
        {
            return RedirectToAction(nameof(SubjectController.Subject), "subject", new { id = subjectId });
        }
    }
}