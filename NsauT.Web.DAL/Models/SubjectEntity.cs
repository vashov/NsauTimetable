using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NsauT.Web.DAL.Models
{
    public class SubjectEntity
    {
        [Key]
        public int Id { get; set; }
        public SubjectInfoEntity Info { get; set; }
        public List<SchoolDayEntity> Days { get; set; }
        public TimetableEntity Timetable { get; set; }

        [Required]
        public string Hash { get; set; }

        [Required]
        public string HashDays { get; set; }
        public bool IsApproved { get; set; }
    }
}
