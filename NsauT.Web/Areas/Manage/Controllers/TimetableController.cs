using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NsauT.Web.Areas.Manage.Models.TimetableController;
using NsauT.Web.DAL.DataStore;
using System.Collections.Generic;
using System.Linq;

namespace NsauT.Web.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TimetableController : Controller
    {
        private readonly TimetableContext _context;

        public TimetableController(TimetableContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Timetables()
        {
            List<TimetableInfoModel> timetables = _context.Timetables
                .AsNoTracking()
                .Include(t => t.Subjects)
                .Select(t => new TimetableInfoModel
                {
                    Id = t.Id,
                    Key = t.Key,
                    IsApproved = t.IsApproved,
                    SubjectsAmount = t.Subjects.Count,
                    SubjectsApproved = t.Subjects.Count(s => s.IsApproved)
                })
                .ToList();
            return View(timetables);
        }

        [HttpGet]
        public IActionResult Timetable(int id)
        {
            TimetableModel timetable = _context.Timetables
                .AsNoTracking()
                .Include(t => t.Groups)
                .Include(t => t.Subjects)
                .Where(t => t.Id == id)
                .Select(t => new TimetableModel
                {
                    Key = t.Key,
                    Groups = t.Groups.Select(g => g.Number),
                    Subjects = t.Subjects.Select(s => new SubjectModel
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Teachers = s.Teachers,
                        IsApproved = s.IsApproved
                    }),
                    IsApproved = t.IsApproved
                })
                .FirstOrDefault();

            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }
    }
}
