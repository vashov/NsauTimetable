using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.BLL.Services;
using NsauT.Web.BLL.Services.User;
using NsauT.Web.BLL.Services.User.DTO;
using NsauT.Web.Models.AccountController;

namespace NsauT.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }

        public AccountController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

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
        public async Task<IActionResult> Login(SignInUserViewModel user)
        {
            var signInUserDto = Mapper.Map<SignInUserDto>(user);
            ServiceResult serviceResult = await UserService.SignInAsync(signInUserDto);

            if (serviceResult.Result == Result.Error)
            {
                foreach (var error in serviceResult.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Message);
                }

                return View();
            }

            // проверяем, принадлежит ли URL приложению
            if (!string.IsNullOrEmpty(user.ReturnUrl) && Url.IsLocalUrl(user.ReturnUrl))
            {
                return Redirect(user.ReturnUrl);
            }

            return RedirectToAction(actionName: "timetables", controllerName: "timetable", new { area = "manage" });
        }
    }
}