using Microsoft.EntityFrameworkCore;
using NsauT.Web.BLL.Services.Timetable.DTO;
using NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi;
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
                    .ThenInclude(s => s.Info)
                .Where(t => t.Id == timetableId)
                .Select(t => new TimetableModelDto
                {
                    Key = t.Key,
                    Groups = t.Groups.Select(g => g.Number),
                    Subjects = t.Subjects.Select(s => new SubjectInfoDto
                    {
                        Id = s.Id,
                        Title = s.Info.Title,
                        Teachers = s.Info.Teachers,
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

        public TimetableApiDto GetApprovedTimetable(int timetableId)
        {
            TimetableApiDto timetable = Context.Timetables
                .Where(t => t.IsApproved)
                .Include(t => t.Groups)
                .Include(t => t.Subjects)
                    .ThenInclude(s => s.Days)
                        .ThenInclude(d => d.Periods)
                .Include(t => t.Subjects)
                    .ThenInclude(s => s.Info)
                .Select(t => new TimetableApiDto
                {
                    Id = t.Id,
                    Key = t.Key,
                    Hash = t.Hash,
                    Groups = t.Groups.Select(g => g.Number).ToList(),
                    Subjects = t.Subjects.Select(s => new SubjectApiDto
                    {
                        Id = s.Id, 
                        Title = s.Info.Title,
                        Teachers = s.Info.Teachers,
                        LectureStartDate = s.Info.LectureStartDate,
                        LectureEndDate = s.Info.LectureEndDate,
                        PracticeStartDate = s.Info.PracticeStartDate,
                        PracticeEndDate  = s.Info.PracticeEndDate,
                        Days = s.Days.Select(d => new SchoolDayApiDto
                        {
                            Id = d.Id,
                            Day = d.Day,
                            IsDayOfEvenWeek = d.IsDayOfEvenWeek,
                            Periods = d.Periods.Select(p => new PeriodApiDto
                            {
                                Id = p.Id,
                                Number = p.Number,
                                Cabinet = p.Cabinet,
                                Subgroup = p.Subgroup,
                                IsLecture = p.IsLecture,
                                Option = p.Option,
                                OptionDate = p.OptionDate,
                                OptionCabinet = p.OptionCabinet
                            }).ToList()
                        }).ToList()
                    }).ToList()
                })
                .SingleOrDefault(t => t.Id == timetableId);

            return timetable;
        }
    }
}
