using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.Models.AccountController;

namespace NsauT.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserBindingModel user)
        {

            return RedirectToAction(actionName: "timetables", controllerName: "timetable", new { area = "manage" });
        }
    }
}