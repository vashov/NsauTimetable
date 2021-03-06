﻿using System;
using System.Collections.Generic;

namespace NsauT.Web.Parser.Models.ExcelParsedModels
{
    internal class SchoolDayInfo
    {
        public DayOfWeek Day { get; set; }
        public bool IsDayOfEvenWeek { get; set; }
        public List<PeriodInfo> Periods { get; set; }
    }
}
