using System;
using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Subject.DTO
{
    public class SchoolDayDto
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public bool IsDayOfEvenWeek { get; set; }
        public IEnumerable<PeriodDto> Periods { get; set; }
        public bool IsApproved { get; set; }
    }
}
