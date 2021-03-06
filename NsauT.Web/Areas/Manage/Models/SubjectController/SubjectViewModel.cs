﻿using System;
using System.Collections.Generic;

namespace NsauT.Web.Areas.Manage.Models.SubjectController
{
    public class SubjectViewModel
    {
        public int SubjectInfoId { get; set; }
        public string Title { get; set; }
        public string Teachers { get; set; }
        public DateTime? LectureStartDate { get; set; }
        public DateTime? LectureEndDate { get; set; }
        public DateTime? PracticeStartDate { get; set; }
        public DateTime? PracticeEndDate { get; set; }
        public IEnumerable<SchoolDayModel> Days { get; set; }
        public bool IsInfoApproved { get; set; }
        public bool IsApproved { get; set; }
    }
}
