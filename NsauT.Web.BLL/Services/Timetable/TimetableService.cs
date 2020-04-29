using Microsoft.EntityFrameworkCore;
using NsauT.Web.BLL.Services.Timetable.DTO;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace NsauT.Web.BLL.Services.Timetable
{
    public class TimetableService : ITimetableService
    {
        private ApplicationContext Context { get; }

        public TimetableService(ApplicationContext context)
        {
            Context = context;
        }

        public IEnumerable<ApprovedTimetableDto> GetApprovedTimetables()
        {
            List<ApprovedTimetableDto> approvedTimetables = Context.Timetables
                .AsNoTracking()
                .Where(t => t.IsApproved)
                .Select(t => new ApprovedTimetableDto
                {
                    Id = t.Id,
                    Key = t.Key
                })
                .ToList();

            return approvedTimetables;
        }

        public TimetableModelDto GetTimetable(int timetableId)
        {
            TimetableModelDto timetable = Context.Timetables
                .AsNoTracking()
                .Include(t => t.Groups)
                .Include(t => t.Subjects)
                .Where(t => t.Id == timetableId)
                .Select(t => new TimetableModelDto
                {
                    Key = t.Key,
                    Groups = t.Groups.Select(g => g.Number),
                    Subjects = t.Subjects.Select(s => new SubjectInfoDto
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Teachers = s.Teachers,
                        IsApproved = s.IsApproved
                    }),
                    IsApproved = t.IsApproved
                })
                .SingleOrDefault();

            return timetable;
        }

        public IEnumerable<TimetableInfoDto> GetTimetablesInfo()
        {
            List<TimetableInfoDto> timetables = Context.Timetables
                .AsNoTracking()
                .Include(t => t.Subjects)
                .Select(t => new TimetableInfoDto
                {
                    Id = t.Id,
                    Key = t.Key,
                    IsApproved = t.IsApproved,
                    SubjectsAmount = t.Subjects.Count,
                    SubjectsApproved = t.Subjects.Count(s => s.IsApproved)
                })
                .ToList();

            return timetables;
        }

        public ServiceResult UpdateApprovedStatus(int timetableId)
        {
            TimetableEntity timetable = Context.Timetables
                .Include(t => t.Subjects)
                .FirstOrDefault(t => t.Id == timetableId);

            if (timetable == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            timetable.IsApproved = timetable.Subjects.All(s => s.IsApproved);
            Context.SaveChanges();

            return new ServiceResult(Result.OK, timetable.Id);
        }
    }
}
