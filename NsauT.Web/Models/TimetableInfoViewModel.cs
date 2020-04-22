namespace NsauT.Web.Models
{
    public class TimetableInfoViewModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int SubjectsApproved { get; set; }
        public int SubjectsAmount { get; set; }
        public bool IsApproved { get; set; }
    }
}
