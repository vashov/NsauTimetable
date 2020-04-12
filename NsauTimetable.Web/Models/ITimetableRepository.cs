using System.Collections.Generic;

namespace NsauTimetable.Client.Models
{
    public interface ITimetableRepository
    {
        void Add(Timetable item);
        void Update(Timetable item);
        Timetable Remove(int key);
        Timetable Get(int id);
        IEnumerable<Timetable> GetAll();
    }
}
