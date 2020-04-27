using Microsoft.EntityFrameworkCore;
using NsauT.Web.BLL.Services.Subject.DTO;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NsauT.Web.BLL.Services.Subject
{
    public class SubjectService : ISubjectService
    {
        private TimetableContext Context { get; }

        public SubjectService(TimetableContext context)
        {
            Context = context;
        }

        public SubjectDto GetSubject(int subjectId)
        {
            IQueryable<SubjectDto> query = Context.Subjects
                .AsNoTracking()
                .Include(s => s.Days)
                .ThenInclude(d => d.Periods)
                .Where(t => t.Id == subjectId)
                .Select(s => new SubjectDto
                {
                    Title = s.Title,
                    Teachers = s.Teachers,
                    LectureStartDate = s.LectureStartDate,
                    LectureEndDate = s.PracticeEndDate,
                    PracticeStartDate = s.PracticeStartDate,
                    PracticeEndDate = s.PracticeEndDate,
                    Days = s.Days.Select(d => new SchoolDayDto
                    {
                        Id = d.Id,
                        Day = d.Day,
                        IsDayOfEvenWeek = d.IsDayOfEvenWeek,
                        IsApproved = d.IsApproved,
                        Periods = d.Periods.Select(p => new PeriodDto
                        {
                            Id = p.Id,
                            Number = p.Number,
                            Cabinet = p.Cabinet,
                            Subgroup = p.Subgroup,
                            IsLecture = p.IsLecture,
                            Option = p.Option,
                            OptionDate = p.OptionDate,
                            OptionCabinet = p.OptionCabinet,
                            IsApproved = p.IsApproved
                        })
                    })
                });

            SubjectDto subject = query.SingleOrDefault();
            return subject;
        }

        public ServiceResult UpdateApprovedStatus(int subjectId)
        {
            SubjectEntity subject = Context.Subjects
                .Include(s => s.Days)
                .Include(s => s.Timetable)
                .SingleOrDefault(s => s.Id == subjectId);

            if (subject == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            subject.IsApproved = subject.Days.All(d => d.IsApproved);

            Context.SaveChanges();

            int timetableId = subject.Timetable.Id;
            return new ServiceResult(Result.OK, timetableId);
        }
    }
}
