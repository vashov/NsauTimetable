using System.Collections.Generic;

namespace NsauT.Web.Areas.Manage.Models.SubjectController
{
    public class SchoolDayModel
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public bool IsDayOfEvenWeek { get; set; }
        public IEnumerable<PeriodModel> Periods { get; set; }
        public bool IsApproved { get; set; }
    }
}
