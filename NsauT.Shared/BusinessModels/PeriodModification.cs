using NsauT.Shared.Enums;
using System;

namespace NsauT.Shared.BusinessModels
{
    public struct PeriodModification
    {
        public PeriodOption Option { get; set; }
        public DateTime? Date { get; set; }
        public string Cabinet { get; set; }
    }
}
