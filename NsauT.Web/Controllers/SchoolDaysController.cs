using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.DAL.DataStore;

namespace NsauT.Web.Controllers
{
    public class SchoolDaysController : Controller
    {
        private TimetableContext _context;

        public SchoolDaysController(TimetableContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}