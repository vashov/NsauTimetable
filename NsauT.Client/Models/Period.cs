using System.Collections.Generic;
using System.Linq;

namespace NsauT.Client.Models
{
    public class Period
    {
        public string Title { get; set; }
        public PeriodInterval Interval { get; set; }
        public List<string> Teachers { get; set; }
        public string Cabinet { get; set; }
        public string TeachersDisplay => string.Join(", ", Teachers);
        public string IntervalDisplay => $"{Interval.Start:hh\\:mm} - {Interval.End:hh\\:mm}";
    }
}
