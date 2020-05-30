using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NsauT.Web.Areas.Manage.Models.SubjectController;
using NsauT.Web.BLL.Services;
using NsauT.Web.BLL.Services.Subject;
using NsauT.Web.BLL.Services.Subject.DTO;
using System.Linq;

namespace NsauT.Web.Areas.Manage.Controllers
{
    public class SubjectController : ManageController
    {
        private ISubjectService SubjectService { get; }
        private IMapper Mapper { get; }

        public SubjectController(ISubjectService subjectService, IMapper mapper)
        {
            SubjectService = subjectService;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult Subject(int id)
        {
            SubjectDto subjectDto = SubjectService.GetSubject(id);

            if (subjectDto == null)
            {
                return NotFound();
            }
            subjectDto.Days = subjectDto.Days.OrderBy(d => d.Day).ThenBy(d => d.IsDayOfEvenWeek);
            SubjectViewModel subjectViewModel = Mapper.Map<SubjectViewModel>(subjectDto);

            return View(subjectViewModel);
        }

        [HttpPut]
        public IActionResult ApproveInfo(int id)
        {
            ServiceResult result = SubjectService.ApproveSubjectInfo(id);

            if (result.Result == Result.NotFound)
            {
                return NotFound();
            }

            if (result.Result != Result.OK)
            {
                return BadRequest();
            }

            bool subjectApproved = SubjectService.IsSubjectApproved(result.Id);
            return Ok(new
            {
                infoApproved = true,
                subjectApproved
            });
        }

        [HttpGet]
        public IActionResult Info(int id)
        {
            SubjectInfoDto subjectInfoDto = SubjectService.GetSubjectInfo(id);

            if (subjectInfoDto == null)
            {
                return NotFound();
            }

            SubjectInfoBindingModel subjectInfo = Mapper.Map<SubjectInfoBindingModel>(subjectInfoDto);
            var subjectInfoViewModel = new SubjectInfoViewModel(subjectInfo, id);

            return View(subjectInfoViewModel);
        }

        [HttpPost]
        public IActionResult Info(int id, [FromForm] SubjectInfoBindingModel subjectInfo)
        {
            SubjectInfoDto subjectInfoDto = Mapper.Map<SubjectInfoDto>(subjectInfo);

            ServiceResult serviceResult = SubjectService.UpdateSubjectInfo(subjectInfoDto, id);

            if (serviceResult.Result == Result.Error)
            {
                foreach (var error in serviceResult.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Message);
                }

                if (!ModelState.IsValid)
                {
                    var subjectInfoViewModel = new SubjectInfoViewModel(subjectInfo, id);
                    return View(subjectInfoViewModel);
                }
            }
            else if (serviceResult.Result == Result.NotFound)
            {
                return NotFound();
            }

            int subjectId = serviceResult.Id;
            return RedirectToSubject(subjectId);
        }

        private RedirectToActionResult RedirectToSubject(int subjectId)
        {
            return RedirectToAction(nameof(SubjectController.Subject), "subject", new { id = subjectId });
        }
    }
}