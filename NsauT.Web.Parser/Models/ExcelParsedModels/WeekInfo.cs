using System.Collections.Generic;

namespace NsauT.Web.Parser.Models.ExcelParsedModels
{
    internal class WeekInfo
    {
        /// <summary>
        /// Contains subject title or from-to-time
        /// </summary>
        public string SubjectSection { get; set; }
        public List<SchoolDayInfo> Days { get; set; }
        public string TeachersSection { get; set; }
    }
}
