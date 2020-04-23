using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NsauT.Web.Areas.Manage.Helpers;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Linq;

namespace NsauT.Web.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SchoolDayController : Controller
    {
        private TimetableContext _context;

        public SchoolDayController(TimetableContext context)
        {
            _context = context;
        }

        public IActionResult DeleteDay(int id)
        {
            SchoolDayEntity day = _context.SchoolDays
                .Include(d => d.Subject)
                .FirstOrDefault(d => d.Id == id);
            if (day == null)
            {
                return NotFound();
            }

            int subjectId = day.Subject.Id;
            Approver.Create(_context).CascadeUpdateApprovedSubject(subjectId);

            _context.SaveChanges();

            return RedirectToSubject(subjectId);
        }

        private RedirectToActionResult RedirectToSubject(int subjectId)
        {
            return RedirectToAction(nameof(SubjectController.Subject), "subject", new { id = subjectId });
        }
    }
}