using NsauT.Web.BLL.Services.Timetable.DTO;
using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Timetable
{
    public interface ITimetableService
    {
        IEnumerable<TimetableInfoDto> GetTimetablesInfo();
        TimetableModelDto GetTimetable(int timetableId);
        IEnumerable<ApprovedTimetableDto> GetApprovedTimetables();
        ServiceResult UpdateApprovedStatus(int timetableId);
    }
}
