using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace NsauT.Web.DAL.Models
{
    public class SubjectInfoEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Teachers { get; set; }
        public DateTime? LectureStartDate { get; set; }
        public DateTime? LectureEndDate { get; set; }
        public DateTime? PracticeStartDate { get; set; }
        public DateTime? PracticeEndDate { get; set; }
        public int SubjectId { get; set; }
        public SubjectEntity Subject { get; set; }

        [Required]
        public string Hash { get; set; }
        public bool IsApproved { get; set; }
    }
}
