using System;
using System.Collections.Generic;

namespace NsauT.Shared.Models.BusinessModels
{
    public class SubjectModel
    {
        public string Title { get; set; }
        public string Teachers { get; set; }
        public DateTime? LectureStartDate { get; set; }
        public DateTime? LectureEndDate { get; set; }
        public DateTime? PracticeStartDate { get; set; }
        public DateTime? PracticeEndDate { get; set; }
        public List<SchoolDayModel> Days { get; set; }
    }
}
