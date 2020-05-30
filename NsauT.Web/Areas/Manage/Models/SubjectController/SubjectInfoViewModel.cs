namespace NsauT.Web.Areas.Manage.Models.SubjectController
{
    public class SubjectInfoViewModel
    {
        public SubjectInfoViewModel(SubjectInfoBindingModel subjectInfo, int subjectInfoId)
        {
            SubjectInfo = subjectInfo;
            SubjectInfoId = subjectInfoId;
        }

        public int SubjectInfoId { get; set; }
        public SubjectInfoBindingModel SubjectInfo { get; set; }
    }
}
