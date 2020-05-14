using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi
{
    public class TimetableApiDto
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public List<SubjectApiDto> Subjects { get; set; }

        public List<string> Groups { get; set; }

        public string Hash { get; set; }
    }
}
