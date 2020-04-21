using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NsauT.Shared.BusinessModels;
using NsauT.Shared.Tools;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using NsauT.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            List<TimetableInfo> groups = _context.Timetables
                .AsNoTracking()
                .Select(t => new TimetableInfo
                {
                    Id = t.Id,
                    Key = t.Key,
                    IsApproved = t.IsApproved
                })
                .ToList();
            return View(groups);
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
    }
}
