using System.Collections.Generic;

namespace NsauT.Shared.BusinessModels
{
    public class TimetableModel
    {
        /// <summary>
        /// Consists of groups joined by comma. e.g "1101,1102" or "1103"
        /// </summary>
        public string Key { get; set; }
        public string SheetTitle { get; set; }
        public List<string> Groups { get; set; }
        public List<SubjectModel> Subjects { get; set; }
        public string Hash { get; set; }
    }
}
