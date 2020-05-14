using Microsoft.EntityFrameworkCore;
using NsauT.Web.BLL.Services.Subject;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Linq;

namespace NsauT.Web.BLL.Services.SchoolDay
{
    public class SchoolDayService : ISchoolDayService
    {
        private ApplicationContext Context { get; }
        private ISubjectService SubjectService { get; }

        public SchoolDayService(ApplicationContext context, ISubjectService subjectService)
        {
            Context = context;
            SubjectService = subjectService;
        }

        public ServiceResult DeleteDay(int dayId)
        {
            SchoolDayEntity day = Context.SchoolDays
                .Include(d => d.Subject)
                .SingleOrDefault(d => d.Id == dayId);

            if (day == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            int subjectId = day.Subject.Id;

            Context.SchoolDays.Remove(day);
            Context.SaveChanges();

            SubjectService.UpdateApprovedStatus(subjectId);

            return new ServiceResult(Result.OK, subjectId);
        }

        public ServiceResult UpdateApprovedStatus(int dayId)
        {
            SchoolDayEntity day = Context.SchoolDays
                .Include(d => d.Periods)
                .Include(d => d.Subject)
                .FirstOrDefault(d => d.Id == dayId);

            if (day == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            day.IsApproved = day.Periods.All(p => p.IsApproved);

            Context.SaveChanges();

            int subjectId = day.Subject.Id;
            SubjectService.UpdateApprovedStatus(subjectId);

            return new ServiceResult(Result.OK, subjectId);
        }
    }
}
