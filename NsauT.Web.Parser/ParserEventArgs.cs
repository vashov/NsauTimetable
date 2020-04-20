using NsauT.Shared.Models.BusinessModels;
using System;
using System.Collections.Generic;

namespace NsauT.Web.Parser
{
    public class ParserEventArgs : EventArgs
    {
        public List<TimetableModel> Timetables { get; set; }
    }
}
