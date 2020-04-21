using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;

namespace NsauT.Web.Controllers
{
    public class SubjectsController : Controller
    {
        private TimetableContext _context;

        public SubjectsController(TimetableContext context)
        {
            _context = context;
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
        
    }
}