using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NsauT.Web.DAL.Models
{
    public class SchoolDayEntity
    {
        [Key]
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public bool IsDayOfEvenWeek { get; set; }
        public List<PeriodEntity> Periods { get; set; }
        public SubjectEntity Subject { get; set; }

        [Required]
        public string Hash { get; set; }
        public bool IsApproved { get; set; }
    }
}
