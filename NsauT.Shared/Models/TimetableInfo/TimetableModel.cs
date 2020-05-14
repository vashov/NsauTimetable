using System.Collections.Generic;

namespace NsauT.Shared.Models.TimetableInfo
{
    public class TimetableModel
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public List<SubjectModel> Subjects { get; set; }

        public List<string> Groups { get; set; }

        public string Hash { get; set; }
    }
}
