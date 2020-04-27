using Microsoft.EntityFrameworkCore;
using NsauT.Web.BLL.Infrastructure;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Linq;

namespace NsauT.Web.BLL.Services.SchoolDay
{
    public class SchoolDayService : ISchoolDayService
    {
        private TimetableContext Context { get; }
        private IApproverFacade Approver { get; }

        public SchoolDayService(TimetableContext context, IApproverFacade approver)
        {
            Context = context;
            Approver = approver;
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

            Approver.CascadeUpdateApprovedSubject(subjectId);

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
            return new ServiceResult(Result.OK, subjectId);
        }
    }
}
