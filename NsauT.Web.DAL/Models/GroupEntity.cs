using System.ComponentModel.DataAnnotations;

namespace NsauT.Web.DAL.Models
{
    public class GroupEntity
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public TimetableEntity Timetable { get; set; } 
    }
}
