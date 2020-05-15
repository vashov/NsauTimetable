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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ServiceResult serviceResult = SchoolDayService.DeleteDay(id);
            if (serviceResult.Result == Result.NotFound)
            {
                return NotFound();
            }

            if (serviceResult.Result != Result.OK)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}