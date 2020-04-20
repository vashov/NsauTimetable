using System;

namespace NsauT.Client.Models
{
    public struct PeriodInterval
    {
        public int PeriodNumber { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
