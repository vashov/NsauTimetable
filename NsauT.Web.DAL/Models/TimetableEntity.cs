using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NsauT.Web.DAL.Models
{
    public class TimetableEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        
        [Required]
        public string Timetable { get; set; }

        [Required]
        public string Hash { get; set; }

        public bool IsApproved { get; set; }
    }
}
