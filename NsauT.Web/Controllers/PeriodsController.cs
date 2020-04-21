using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Linq;

namespace NsauT.Web.Controllers
{
    public class PeriodsController : Controller
    {
        private TimetableContext _context;

        public PeriodsController(TimetableContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Period(int id)
        {
            PeriodEntity period = _context.Periods
                .AsNoTracking()
                .FirstOrDefault(t => t.Id == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        [HttpPut]
        public IActionResult UpdatePeriod([FromForm]PeriodEntity periodEntity)
        {
            bool isExist = _context.Periods.Any(p => p.Id == periodEntity.Id);
            if (!isExist)
            {
                return NotFound();
            }

            int subjectId = periodEntity.SchoolDay.Subject.Id;

            _context.Periods.Update(periodEntity);
            _context.SaveChanges();

            return RedirectToSubject(subjectId);
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
            return RedirectToAction(nameof(SubjectsController.Subject), nameof(SubjectsController), subjectId);
        }
    }
}