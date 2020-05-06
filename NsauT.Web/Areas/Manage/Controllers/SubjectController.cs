using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.Areas.Manage.Models.SubjectController;
using NsauT.Web.BLL.Services.Subject;
using NsauT.Web.BLL.Services.Subject.DTO;

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

            SubjectViewModel subjectViewModel = Mapper.Map<SubjectViewModel>(subjectDto);

            return View(subjectViewModel);
        }
    }
}