using System;
using System.Collections.Generic;
using System.Text;

namespace NsauT.Client.DAL.Models
{
    public class TimetableEntity
    {
        public int Id { get; set; }
        public string GroupNumber { get; set; }
        public string JsonTimetable { get; set; }
    }
}
