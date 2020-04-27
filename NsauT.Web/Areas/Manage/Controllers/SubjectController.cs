using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NsauT.Web.Areas.Manage.Helpers;
using NsauT.Web.Areas.Manage.Models.SubjectController;
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
            //IQueryable<SubjectViewModel> query = _context.Subjects
            //    .AsNoTracking()
            //    .Include(s => s.Days)
            //    .ThenInclude(d => d.Periods)
            //    .Where(t => t.Id == id)
            //    .Select(s => new SubjectViewModel
            //    {
            //        Title = s.Title,
            //        Teachers = s.Teachers,
            //        LectureStartDate = s.LectureStartDate,
            //        LectureEndDate = s.PracticeEndDate,
            //        PracticeStartDate = s.PracticeStartDate,
            //        PracticeEndDate = s.PracticeEndDate,
            //        Days = s.Days.Select(d => new SchoolDayModel
            //        {
            //            Id = d.Id,
            //            Day = SchoolDayHelper.GetDayName(d.Day),
            //            IsDayOfEvenWeek = d.IsDayOfEvenWeek,
            //            IsApproved = d.IsApproved,
            //            Periods = d.Periods.Select(p => new PeriodModel
            //            {
            //                Id = p.Id,
            //                Number = PeriodNumbersHelper.ConvertPeriodNumberToString(p.Number),
            //                Cabinet = p.Cabinet,
            //                Subgroup = p.Subgroup,
            //                IsLecture = p.IsLecture,
            //                Option = PeriodOptionsHelper.ConvertPeriodOptionToString(p.Option),
            //                OptionDate = p.OptionDate,
            //                OptionCabinet = p.OptionCabinet,
            //                IsApproved = p.IsApproved
            //            })
            //        })
            //    });

            //SubjectViewModel subject = query.FirstOrDefault();

            if (subjectDto == null)
            {
                return NotFound();
            }

            SubjectViewModel subjectViewModel = Mapper.Map<SubjectViewModel>(subjectDto);

            //var mapper = new SubjectMapper();
            //Models.SubjectModel subjectModel = mapper.MapEntityToModel(subjectEntity);

            return View(subjectViewModel);
        }
    }
}