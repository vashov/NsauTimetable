using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NsauT.Web.Areas.Manage.Helpers;
using NsauT.Web.Areas.Manage.Models.SubjectController;
using NsauT.Web.DAL.DataStore;
using System.Linq;

namespace NsauT.Web.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SubjectController : Controller
    {
        private TimetableContext _context;

        public SubjectController(TimetableContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Subject(int id)
        {
            IQueryable<SubjectModel> query = _context.Subjects
                .AsNoTracking()
                .Include(s => s.Days)
                .ThenInclude(d => d.Periods)
                .Where(t => t.Id == id)
                .Select(s => new SubjectModel
                {
                    Title = s.Title,
                    Teachers = s.Teachers,
                    LectureStartDate = s.LectureStartDate,
                    LectureEndDate = s.PracticeEndDate,
                    PracticeStartDate = s.PracticeStartDate,
                    PracticeEndDate = s.PracticeEndDate,
                    Days = s.Days.Select(d => new SchoolDayModel
                    {
                        Id = d.Id,
                        Day = SchoolDayHelper.GetDayName(d.Day),
                        IsDayOfEvenWeek = d.IsDayOfEvenWeek,
                        IsApproved = d.IsApproved,
                        Periods = d.Periods.Select(p => new PeriodModel
                        {
                            Id = p.Id,
                            Number = PeriodNumbersHelper.ConvertPeriodNumberToString(p.Number),
                            Cabinet = p.Cabinet,
                            Subgroup = p.Subgroup,
                            IsLecture = p.IsLecture,
                            Option = PeriodOptionsHelper.ConvertPeriodOptionToString(p.Option),
                            OptionDate = p.OptionDate,
                            OptionCabinet = p.OptionCabinet,
                            IsApproved = p.IsApproved
                        })
                    })
                });

            SubjectModel subject = query.FirstOrDefault();

            if (subject == null)
            {
                return NotFound();
            }

            //var mapper = new SubjectMapper();
            //Models.SubjectModel subjectModel = mapper.MapEntityToModel(subjectEntity);

            return View(subject);
        }
    }
}