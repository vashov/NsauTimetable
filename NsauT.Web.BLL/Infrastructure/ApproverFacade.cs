using Microsoft.Extensions.DependencyInjection;
using NsauT.Web.BLL.Services;
using NsauT.Web.BLL.Services.SchoolDay;
using NsauT.Web.BLL.Services.Subject;
using NsauT.Web.BLL.Services.Timetable;
using System;

namespace NsauT.Web.BLL.Infrastructure
{
    public class ApproverFacade : IApproverFacade
    {
        private ISchoolDayService SchoolDayService { get; }
        private ISubjectService SubjectService { get; }
        private ITimetableService TimetableService { get; }

        public ApproverFacade(IServiceProvider serviceProvider)
        {
            SchoolDayService = serviceProvider.GetRequiredService<ISchoolDayService>();
            SubjectService = serviceProvider.GetRequiredService<ISubjectService>(); ;
            TimetableService = serviceProvider.GetRequiredService<ITimetableService>(); ;
        }

        public void CascadeUpdateApprovedDay(int dayId)
        {
            ServiceResult serviceResult = SchoolDayService.UpdateApprovedStatus(dayId);
            if (serviceResult.Result != Result.OK)
            {
                return;
            }

            int subjectId = serviceResult.Id;
            CascadeUpdateApprovedSubject(subjectId);
        }

        public void CascadeUpdateApprovedSubject(int subjectId)
        {
            ServiceResult subjectResult = SubjectService.UpdateApprovedStatus(subjectId);
            if (subjectResult.Result != Result.OK)
            {
                return;
            }

            int timetableId = subjectResult.Id;
            ServiceResult timetableResult = TimetableService.UpdateApprovedStatus(timetableId);
        }
    }
}
