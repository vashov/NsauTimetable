using NsauT.Web.BLL.Services.Timetable.DTO;
using NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi;
using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Timetable
{
    public interface ITimetableService
    {
        IEnumerable<TimetableInfoDto> GetTimetablesInfo();
        TimetableModelDto GetTimetable(int timetableId);
        IEnumerable<ApprovedTimetableDto> GetApprovedTimetables();
        TimetableApiDto GetApprovedTimetable(int timetableId);
        ServiceResult UpdateApprovedStatus(int timetableId);
    }
}
