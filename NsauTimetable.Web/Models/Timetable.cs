﻿using System.ComponentModel.DataAnnotations;

namespace NsauTimetable.Client.Models
{
    public class Timetable
    {
        public int Id { get; set; }

        [Required]
        public string GroupNumber { get; set; }

        [Required]
        public string TimetableJson { get; set; }
    }
}
