using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NsauT.Client.Models
{
    public class TimetableRepository : ITimetableRepository
    {
        private static ConcurrentDictionary<int, Timetable> _timetables =
            new ConcurrentDictionary<int, Timetable>();

        public TimetableRepository()
        {
            Add(new Timetable { Id = 1, GroupNumber = "1401", TimetableJson = "This is an group description." });
            Add(new Timetable { Id = 2, GroupNumber = "1402", TimetableJson = "This is an group description." });
            Add(new Timetable { Id = 3, GroupNumber = "1403", TimetableJson = "This is an group description." });
        }

        public IEnumerable<Timetable> GetAll()
        {
            return _timetables.Values;
        }

        public void Add(Timetable item)
        {
            item.Id = _timetables.Max(x => x.Value.Id);
            _timetables[item.Id] = item;
        }

        public Timetable Get(int id)
        {
            _timetables.TryGetValue(id, out Timetable item);
            return item;
        }

        public Timetable Remove(int id)
        {
            _timetables.TryRemove(id, out Timetable item);
            return item;
        }

        public void Update(Timetable item)
        {
            _timetables[item.Id] = item;
        }
    }
}
