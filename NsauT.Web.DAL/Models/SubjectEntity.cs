using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NsauT.Web.DAL.Models
{
    public class SubjectEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Teachers { get; set; }
        public DateTime? LectureStartDate { get; set; }
        public DateTime? LectureEndDate { get; set; }
        public DateTime? PracticeStartDate { get; set; }
        public DateTime? PracticeEndDate { get; set; }
        public List<SchoolDayEntity> Days { get; set; }
        public TimetableEntity Timetable { get; set; }
        public bool IsApproved { get; set; }
    }
}
