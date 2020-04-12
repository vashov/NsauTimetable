using NsauTimetable.Client.Models;
using System.Collections.Generic;

namespace NsauTimetable.Client.Services.TimetableService
{
    public interface ITimetableService
    {
        List<TimetableByDay> GetTimetables();
    }
}
