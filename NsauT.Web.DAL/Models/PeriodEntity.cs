using NsauT.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace NsauT.Web.DAL.Models
{
    public class PeriodEntity
    {
        [Key]
        public int Id { get; set; }
        public PeriodNumber Number { get; set; }
        public string Cabinet { get; set; }
        public string Subgroup { get; set; }
        public bool IsLecture { get; set; }
        public PeriodOption Option { get; set; }
        public DateTime? OptionDate { get; set; }
        public string OptionCabinet { get; set; }
        public SchoolDayEntity SchoolDay { get; set; }
        public bool IsApproved { get; set; }
    }
}
