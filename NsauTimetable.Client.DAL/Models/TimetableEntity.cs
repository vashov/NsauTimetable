using System;
using System.Collections.Generic;
using System.Text;

namespace NsauTimetable.Client.DAL.Models
{
    public class TimetableEntity
    {
        public int Id { get; set; }
        public string GroupNumber { get; set; }
        public string JsonTimetable { get; set; }
    }
}
