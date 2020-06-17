using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NsauT.Web.DAL.Models
{
    public class TimetableEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }
        
        [Required]
        public List<SubjectEntity> Subjects { get; set; }

        [Required]
        public List<GroupEntity> Groups { get; set; }

        [Required]
        public string Hash { get; set; }

        public bool IsApproved { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? NotChanged { get; set; }
    }
}
