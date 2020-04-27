using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.Areas.Manage.Models.TimetableController;
using NsauT.Web.BLL.Services.Timetable;
using NsauT.Web.BLL.Services.Timetable.DTO;
using System.Collections.Generic;

namespace NsauT.Web.Areas.Manage.Controllers
{
    public class TimetableController : ManageController
    {
        private ITimetableService TimetableService { get; }
        private IMapper Mapper { get; }

        public TimetableController(ITimetableService timetableService, IMapper mapper)
        {
            TimetableService = timetableService;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult Timetables()
        {
            IEnumerable<TimetableInfoDto> timetablesInfo = TimetableService.GetTimetablesInfo();
            var timetablesInfoModels = Mapper.Map<IEnumerable<TimetableInfoModel>>(timetablesInfo);

            return View(timetablesInfoModels);
        }

        [HttpGet]
        public IActionResult Timetable(int id)
        {
            TimetableModelDto timetableDto = TimetableService.GetTimetable(id);

            if (timetableDto == null)
            {
                return NotFound();
            }

            var timetableViewModel = Mapper.Map<TimetableViewModel>(timetableDto);

            return View(timetableViewModel);
        }
    }
}
