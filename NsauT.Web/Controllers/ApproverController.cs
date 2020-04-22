using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NsauT.Shared.BusinessModels;
using NsauT.Shared.Helpers;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using NsauT.Web.Helpers;
using NsauT.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace NsauT.Web.Controllers
{
    public class ApproverController : Controller
    {
        private TimetableContext _context;

        public ApproverController(TimetableContext context)
        {
            _context = context;
        }

        public IActionResult Timetables()
        {
            List<TimetableInfoViewModel> timetableInfo = _context.Timetables
                .AsNoTracking()
                .Include(t => t.Subjects)
                .Select(t => new TimetableInfoViewModel
                {
                    Id = t.Id,
                    Key = t.Key,
                    IsApproved = t.IsApproved,
                    SubjectsAmount = t.Subjects.Count,
                    SubjectsApproved = t.Subjects.Count(s => s.IsApproved)
                })
                .ToList();
            return View(timetableInfo);
        }

        public IActionResult Timetable(int id)
        {
            TimetableEntity timetable = _context.Timetables
                .AsNoTracking()
                .Include(t => t.Groups)
                .Include(t => t.Subjects)
                .FirstOrDefault(t => t.Id == id);
            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }

        public IActionResult Subject(int id)
        {
            SubjectEntity subject = _context.Subjects
                .AsNoTracking()
                .Include(s => s.Days)
                .ThenInclude(d => d.Periods)
                .FirstOrDefault(t => t.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpGet]
        public IActionResult Period(int? id, [FromQuery] int? dayId)
        {
            PeriodEntity periodEntity = null;

            if (id != null)
            {
                periodEntity = _context.Periods
                    .AsNoTracking()
                    .FirstOrDefault(t => t.Id == id);
            }

            SelectList periodNumbersList = PeriodNumbersHelper.GetPeriodNumbersList(periodEntity?.Number);
            SelectList periodOptionsList = PeriodOptionsHelper.GetPeriodOptionsList(periodEntity?.Option);

            ViewBag.PeriodNumbers = periodNumbersList;
            ViewBag.PeriodOptions = periodOptionsList;

            var periodViewModel = new PeriodViewModel
            {
                PeriodId = id,
                DayId = dayId,
                Period = new PeriodBindingModel
                {
                    Number = (string)periodNumbersList.SelectedValue,
                    Option = (string)periodOptionsList.SelectedValue
                }
            };

            if (id == null)
            {
                return View(periodViewModel);
            }

            if (periodEntity == null)
            {
                return NotFound();
            }

            periodViewModel.Period.Cabinet = periodEntity.Cabinet;
            periodViewModel.Period.Subgroup = periodEntity.Subgroup;
            periodViewModel.Period.IsLecture = periodEntity.IsLecture;
            periodViewModel.Period.OptionDate = periodEntity.OptionDate;
            periodViewModel.Period.OptionCabinet = periodEntity.OptionCabinet;

            return View(periodViewModel);
        }

        [HttpPost]
        public IActionResult Period(int? id, [FromQuery] int? dayId, [FromForm]PeriodBindingModel period)
        {
            PeriodNumber periodNumber = PeriodNumbersHelper.ConvertStringToPeriodNumber(period.Number);
            PeriodOption periodOption = PeriodOptionsHelper.ConvertStringToPeriodOption(period.Option);

            ValidatePeriod(period, periodOption);

            if (!ModelState.IsValid)
            {
                return View(period);
            }

            PeriodEntity periodEntity = null;

            if (id == null)
            {
                periodEntity = new PeriodEntity();
            }
            else
            {
                periodEntity = _context.Periods
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Id == id);

                if (periodEntity == null)
                {
                    return NotFound();
                }
            }

            //periodEntity.Cabinet = period.Cabinet;
            //periodEntity.Number = periodNumber;
            //periodEntity.Subgroup = period.Subgroup;
            //periodEntity.IsLecture = period.IsLecture;
            //periodEntity.Option = periodOption;
            //periodEntity.OptionDate = period.OptionDate;
            //periodEntity.OptionCabinet = period.OptionCabinet;
            //periodEntity.IsApproved = true;

            //if (periodId == null)
            //{
            //    if (dayId == null)
            //    {
            //        return BadRequest();
            //    }
            //    SchoolDayEntity day = _context.SchoolDays
            //        .AsNoTracking()
            //        .Include(d => d.Periods)
            //        .FirstOrDefault(d => d.Id == dayId);

            //    if (day == null)
            //    {
            //        return NotFound();
            //    }

            //    day.Periods.Add(periodEntity);
            //}
            //else
            //{
            //    _context.Periods.Update(periodEntity);
            //}

            //_context.SaveChanges();

            //periodEntity = _context.Periods
            //    .AsNoTracking()
            //    .Include(p => p.SchoolDay)
            //    .ThenInclude(d => d.Subject)
            //    .FirstOrDefault(p => p.Id == id);

            //CascadeUpdateApprovedStatus(periodEntity.Id);

            //_context.SaveChanges();

            int subjectId = periodEntity.SchoolDay.Subject.Id;

            return RedirectToSubject(subjectId);
        }

        private void ValidatePeriod(PeriodBindingModel period, PeriodOption periodOption)
        {
            string errorMessage;
            if (!CheckIsValidPeriodOption(periodOption, period, out errorMessage))
            {
                ModelState.AddModelError(nameof(period.Option), errorMessage);
            }

            if (!period.IsLecture && string.IsNullOrWhiteSpace(period.Subgroup))
            {
                errorMessage = "Пара не лекция, но подгруппа не указана";
                ModelState.AddModelError(nameof(period.Subgroup), errorMessage);
            }
        }

        private bool CheckIsValidPeriodOption(PeriodOption periodOption, PeriodBindingModel period,
            out string errorMessage)
        {
            if (periodOption == PeriodOption.None && !string.IsNullOrEmpty(period.OptionCabinet))
            {
                errorMessage = "Пара без доп. условия, но указан доп. кабинет";
                return false;
            }

            if (periodOption == PeriodOption.None && period.OptionDate != null)
            {
                errorMessage = "Пара без доп. условия, но указана доп. дата";
                return false;
            }

            if (periodOption == PeriodOption.Once && period.OptionDate == null)
            {
                errorMessage = "Пара единожды, но доп. дата не указана";
                return false;
            }

            if (periodOption == PeriodOption.OnceDifferentCabinet && string.IsNullOrWhiteSpace(period.OptionCabinet))
            {
                errorMessage = "Пара однажды в другом кабинете, но доп. кабинет не указан";
                return false;
            }

            if (periodOption == PeriodOption.OnceDifferentCabinet && period.OptionDate == null)
            {
                errorMessage = "Пара однажды в другом кабинете, но доп. дата не указана";
                return false;
            }

            if (periodOption == PeriodOption.Since && period.OptionDate == null)
            {
                errorMessage = "Пара начинается с определенной даты, но доп. дата не указана";
                return false;
            }

            if (periodOption == PeriodOption.Until && period.OptionDate == null)
            {
                errorMessage = "Пара идет до определенной даты, но доп. дата не указана";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        public IActionResult DeletePeriod(int id)
        {
            PeriodEntity period = _context.Periods.FirstOrDefault(p => p.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            int subjectId = period.SchoolDay.Subject.Id; ;

            _context.Periods.Remove(period);
            _context.SaveChanges();

            return RedirectToSubject(subjectId);
        }

        private RedirectToActionResult RedirectToSubject(int subjectId)
        {
            return RedirectToAction(nameof(Subject), new { id = subjectId });
        }

        private void CascadeUpdateApprovedStatus(int periodId)
        {
            if (!TryUpdateDayApprovedStatus(periodId, out int dayId))
            {
                return;
            }

            if (!TryUpdateSubjectApprovedStatus(dayId, out int subjectId))
            {
                return;
            }

            TryUpdateTimetableApprovedStatus(subjectId);
        }

        private bool TryUpdateDayApprovedStatus(int periodId, out int dayId)
        {
            SchoolDayEntity day = _context.SchoolDays
                .Include(d => d.Periods)
                .FirstOrDefault(d => d.Periods.Any(p => p.Id == periodId));

            if (day.Periods.Any(p => !p.IsApproved))
            {
                dayId = 0;
                return false;
            }

            day.IsApproved = true;
            //_context.SaveChanges();
            dayId = day.Id;

            return true;
        }

        private bool TryUpdateSubjectApprovedStatus(int dayId, out int subjectId)
        {
            SubjectEntity subject = _context.Subjects
                .Include(s => s.Days)
                .FirstOrDefault(s => s.Days.Any(d => d.Id == dayId));

            if (subject.Days.Any(d => !d.IsApproved))
            {
                subjectId = 0;
                return false;
            }

            subject.IsApproved = true;
            //_context.SaveChanges();
            subjectId = subject.Id;

            return true;
        }

        private bool TryUpdateTimetableApprovedStatus(int subjectId)
        {
            TimetableEntity timetable = _context.Timetables
                .Include(t => t.Subjects)
                .FirstOrDefault(t => t.Subjects.Any(s => s.Id == subjectId));

            if (timetable.Subjects.Any(s => !s.IsApproved))
            {
                return false;
            }

            timetable.IsApproved = true;
            //_context.SaveChanges();

            return true;
        }
    }
}
