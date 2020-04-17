using System;

namespace NsauTimetable.Parser.Models.BusinessModels
{
    public enum PeriodOption
    {
        None = 0,
        Once = 1,
        Since = 2,
        Until = 3
    }

    /// <summary>
    /// Period can be once a *date*, since a *date* or until a *date*
    /// </summary>
    public struct PeriodModification
    {
        public PeriodOption Option { get; set; }
        public DateTime Date { get; set; }
    }
}
