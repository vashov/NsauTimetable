using NsauT.Shared.Enums;
using System;

namespace NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi
{
    public class PeriodApiDto
    {
        public int Id { get; set; }
        public PeriodNumber Number { get; set; }
        public string Cabinet { get; set; }
        public string Subgroup { get; set; }
        public bool IsLecture { get; set; }
        public PeriodOption Option { get; set; }
        public DateTime? OptionDate { get; set; }
        public string OptionCabinet { get; set; }
    }
}
