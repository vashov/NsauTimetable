using NsauT.Shared.Helpers;

namespace NsauT.Shared.BusinessModels
{
    public class PeriodModel
    {
        public PeriodNumber Number { get; set; }
        public string Cabinet { get; set; }
        public string Subgroup { get; set; }
        public bool IsLecture { get; set; }
        public PeriodModification Modification { get; set; }
    }
}
