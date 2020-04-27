using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Timetable.DTO
{
    public class TimetableModelDto
    {
        public string Key { get; set; }
        public IEnumerable<string> Groups { get; set; }
        public IEnumerable<SubjectInfoDto> Subjects { get; set; }
        public bool IsApproved { get; set; }
    }
}
