using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.Areas.Manage.Models.PeriodController;
using NsauT.Web.BLL.Services;
using NsauT.Web.BLL.Services.Period;
using NsauT.Web.BLL.Services.Period.DTO;
using NsauT.Web.BLL.Services.SchoolDay;

namespace NsauT.Web.Areas.Manage.Controllers
{
    public class PeriodController : ManageController
    {
        private IPeriodService PeriodService { get; }
        private ISchoolDayService SchoolDayService { get; }

        private IMapper Mapper { get; }

        public PeriodController(IPeriodService periodService, ISchoolDayService schoolDayService, IMapper mapper)
        {
            PeriodService = periodService;
            SchoolDayService = schoolDayService;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult Period(int? id, [FromQuery] int dayId)
        {
            if (dayId == 0)
            {
                return BadRequest();
            }

            PeriodDto periodDto;
            if (id != null)
            {
                periodDto = PeriodService.GetPeriod(id.Value);
                if (periodDto == null)
                {
                    return NotFound();
                }
                if (periodDto.SchoolDayId != dayId)
                {
                    return BadRequest();
                }
            }
            else
            {
                periodDto = new PeriodDto
                {
                    SchoolDayId = dayId,
                    Number = Shared.Enums.PeriodNumber.First
                };
            }

            var periodBindingModel = Mapper.Map<PeriodBindingModel>(periodDto);
            var periodViewModel = new PeriodViewModel(periodBindingModel, id);

            return View(periodViewModel);
        }

        [HttpPost]
        public IActionResult Period(int? id, [FromForm]PeriodBindingModel period)
        {
            if (period.SchoolDayId == 0)
            {
                return BadRequest();
            }

            var periodDto = Mapper.Map<PeriodDto>(period);

            ServiceResult serviceResult = PeriodService.AddOrUpdatePeriod(periodDto, id);

            if (serviceResult.Result == Result.Error)
            {
                foreach (var error in serviceResult.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Message);
                }

                if (!ModelState.IsValid)
                {
                    var periodViewModel = new PeriodViewModel(period, id);
                    return View(periodViewModel);
                }
            }
            else if (serviceResult.Result == Result.NotFound)
            {
                return NotFound();
            }

            int subjectId = serviceResult.Id;
            return RedirectToSubject(subjectId);
        }

        [HttpPut]
        public IActionResult Approve(int id, [FromQuery] int dayId)
        {
            // TODO: maybe return some boolean result
            ServiceResult result = PeriodService.ApprovePeriod(id);

            if (result.Result == Result.NotFound)
            {
                return NotFound();
            }

            if (result.Result != Result.OK)
            {
                return BadRequest();
            }

            bool dayApproved = SchoolDayService.IsDayApproved(dayId);
            return Ok(new
            {
                periodApproved = true,
                dayApproved,
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id, [FromQuery] int dayId)
        {
            ServiceResult result = PeriodService.DeletePeriod(id);

            if (result.Result == Result.NotFound)
            {
                return NotFound();
            }

            bool dayApproved = SchoolDayService.IsDayApproved(dayId);
            return Ok(new
            {
                dayApproved
            });
        }

        private RedirectToActionResult RedirectToSubject(int subjectId)
        {
            return RedirectToAction(nameof(SubjectController.Subject), "subject", new { id = subjectId });
        }
    }
}