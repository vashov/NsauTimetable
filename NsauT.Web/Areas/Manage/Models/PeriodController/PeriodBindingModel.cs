using System;

namespace NsauT.Web.Areas.Manage.Models.PeriodController
{
    public class PeriodBindingModel
    {
        public string Number { get; set; }
        public string Cabinet { get; set; }
        public string Subgroup { get; set; }
        public bool IsLecture { get; set; }
        public string Option { get; set; }
        public DateTime? OptionDate { get; set; }
        public string OptionCabinet { get; set; }
    }
}
