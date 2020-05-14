using System;
using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi
{
    public class SchoolDayApiDto
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public bool IsDayOfEvenWeek { get; set; }
        public List<PeriodApiDto> Periods { get; set; }
    }
}
