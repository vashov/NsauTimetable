using System.Collections.Generic;

namespace NsauT.Web.Areas.Manage.Models.TimetableController
{
    public class TimetableViewModel
    {
        public string Key { get; set; }
        public IEnumerable<string> Groups { get; set; }
        public IEnumerable<SubjectModel> Subjects { get; set; }
        public bool IsApproved { get; set; }
    }
}
