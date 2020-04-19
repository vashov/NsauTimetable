using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NsauT.Web.Checker
{
    internal class TimetableChecker
    {
        public bool CheckNeedUpdateTimetable(TimetableEntity timetable, string newHash)
        {
            return EqualityComparer<string>.Default.Equals(timetable.Hash, newHash);
        }

        public async Task<bool> TryUpdateTimetableAsync(IRepository<TimetableEntity> repository, 
            TimetableEntity timetableEntity, string jsonTimetable, string hash)
        {
            timetableEntity.Hash = hash;
            timetableEntity.Timetable = jsonTimetable;
            return await repository.UpdateItemAsync(timetableEntity);
        }

        public async Task<bool> TryAddTimetableAsync(IRepository<TimetableEntity> repository, string timetableId,
            string hash, string jsonTimetable)
        {
            var timetable = new TimetableEntity
            {
                Id = timetableId,
                Hash = hash,
                Timetable = jsonTimetable
            };

            return await repository.AddItemAsync(timetable);
        }
    }
}
