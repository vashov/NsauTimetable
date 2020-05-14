using System;
using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi
{
    public class SubjectApiDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Teachers { get; set; }
        public DateTime? LectureStartDate { get; set; }
        public DateTime? LectureEndDate { get; set; }
        public DateTime? PracticeStartDate { get; set; }
        public DateTime? PracticeEndDate { get; set; }
        public List<SchoolDayApiDto> Days { get; set; }
    }
}
