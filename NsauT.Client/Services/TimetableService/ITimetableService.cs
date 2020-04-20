using NsauT.Client.Models;
using System.Collections.Generic;

namespace NsauT.Client.Services.TimetableService
{
    public interface ITimetableService
    {
        List<TimetableByDay> GetTimetables();
        List<string> GetGroups();
    }
}
