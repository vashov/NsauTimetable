namespace NsauT.Web.Areas.Manage.Models.TimetableController
{
    public class TimetableInfoModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int SubjectsApproved { get; set; }
        public int SubjectsAmount { get; set; }
        public bool IsApproved { get; set; }
    }
}
