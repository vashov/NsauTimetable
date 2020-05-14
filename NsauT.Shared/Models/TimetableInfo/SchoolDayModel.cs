using System;
using System.Collections.Generic;

namespace NsauT.Shared.Models.TimetableInfo
{
    public class SchoolDayModel
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public bool IsDayOfEvenWeek { get; set; }
        public List<PeriodModel> Periods { get; set; }
    }
}
