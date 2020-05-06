using NsauT.Shared.BusinessModels;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NsauT.Web.Checker
{
    internal class TimetableChecker
    {
        private EntityMapper EntityMapper { get; } = new EntityMapper();

        public bool IsSameTimetable(TimetableEntity timetable, string newHash)
        {
            return EqualityComparer<string>.Default.Equals(timetable.Hash, newHash);
        }

        public void UpdateTimetable(ApplicationContext context, 
            TimetableEntity timetableEntity, TimetableModel timetableModel, string hash)
        {
            context.Subjects.RemoveRange(timetableEntity.Subjects);

            timetableEntity.Subjects.Clear();

            List<SubjectEntity> subjects = EntityMapper.MapSubjects(timetableModel.Subjects);
            timetableEntity.Subjects.AddRange(subjects);
            timetableEntity.IsApproved = false;
            timetableEntity.Hash = hash;
            timetableEntity.Groups = EntityMapper.MapGroups(timetableModel.Groups);

            context.Update(timetableEntity);
            context.SaveChanges();
        }

        public void AddTimetable(ApplicationContext context, TimetableModel timetableModel, string hash)
        {
            TimetableEntity timetableEntity = EntityMapper.MapTimetable(timetableModel, hash);

            context.Timetables.Add(timetableEntity);
            context.SaveChanges();
        }
    }
}
