﻿using System;
using System.Collections.Generic;

namespace NsauTimetable.Parser.Models.BusinessModels
{
    public class SchoolDayModel
    {
        public DayOfWeek Day { get; set; }
        public bool IsDayOfEvenWeek { get; set; }
        public List<PeriodModel> Periods { get; set; }
    }
}
