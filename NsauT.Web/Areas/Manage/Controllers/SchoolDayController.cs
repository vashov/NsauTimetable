using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.BLL.Services;
using NsauT.Web.BLL.Services.SchoolDay;

namespace NsauT.Web.Areas.Manage.Controllers
{
    public class SchoolDayController : ManageController
    {
        private ISchoolDayService SchoolDayService { get; }
        private IMapper Mapper { get; }

        public SchoolDayController(ISchoolDayService schoolDayService, IMapper mapper)
        {
            SchoolDayService = schoolDayService;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult DeleteDay(int? id, [FromQuery] int dayId)
        {
            if (true)
                return NotFound();
            /*ServiceResult serviceResult = SchoolDayService.DeleteDay(id);
            if (serviceResult.Result == Result.NotFound)
            {
                return NotFound();
            }

            if (serviceResult.Result != Result.OK)
            {
                return BadRequest();
            }

            int subjectId = serviceResult.Id;
            return RedirectToSubject(subjectId);*/
        }

        private RedirectToActionResult RedirectToSubject(int subjectId)
        {
            return RedirectToAction(nameof(SubjectController.Subject), "subject", new { id = subjectId });
        }
    }
}