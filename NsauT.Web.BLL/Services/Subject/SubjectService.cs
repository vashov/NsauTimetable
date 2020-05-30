using Microsoft.EntityFrameworkCore;
using NsauT.Web.BLL.Services.Subject.DTO;
using NsauT.Web.BLL.Services.Timetable;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Linq;

namespace NsauT.Web.BLL.Services.Subject
{
    public class SubjectService : ISubjectService
    {
        private ApplicationContext Context { get; }
        private ITimetableService TimetableService { get; }

        public SubjectService(ApplicationContext context, ITimetableService timetableService)
        {
            Context = context;
            TimetableService = timetableService;
        }

        public SubjectDto GetSubject(int subjectId)
        {
            IQueryable<SubjectDto> query = Context.Subjects
                .AsNoTracking()
                .Include(s => s.Info)
                .Include(s => s.Days)
                .ThenInclude(d => d.Periods)
                .Where(t => t.Id == subjectId)
                .Select(s => new SubjectDto
                {
                    SubjectInfoId = s.Info.Id,
                    IsInfoApproved = s.Info.IsApproved,
                    Title = s.Info.Title,
                    Teachers = s.Info.Teachers,
                    LectureStartDate = s.Info.LectureStartDate,
                    LectureEndDate = s.Info.PracticeEndDate,
                    PracticeStartDate = s.Info.PracticeStartDate,
                    PracticeEndDate = s.Info.PracticeEndDate,
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
                .Include(s => s.Info)
                .Include(s => s.Days)
                .Include(s => s.Timetable)
                .SingleOrDefault(s => s.Id == subjectId);

            if (subject == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            subject.IsApproved = subject.Days.All(d => d.IsApproved) && subject.Info.IsApproved;

            Context.SaveChanges();

            int timetableId = subject.Timetable.Id;

            TimetableService.UpdateApprovedStatus(timetableId);
            return new ServiceResult(Result.OK, timetableId);
        }

        public ServiceResult ApproveSubjectInfo(int subjectInfoId)
        {
            SubjectInfoEntity subjectInfo = Context.SubjectInfos
               .SingleOrDefault(s => s.Id == subjectInfoId);

            if (subjectInfo == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            subjectInfo.IsApproved = true;

            Context.SaveChanges();

            UpdateApprovedStatus(subjectInfo.SubjectId);

            return new ServiceResult(Result.OK, subjectInfo.SubjectId);
        }

        public bool IsSubjectApproved(int subjectId)
        {
            SubjectEntity subject = Context.Subjects
                .AsNoTracking()
                .SingleOrDefault(s => s.Id == subjectId);

            return subject.IsApproved;
        }

        public SubjectInfoDto GetSubjectInfo(int subjectInfoId)
        {
            SubjectInfoDto subjectInfo = Context.SubjectInfos
                .AsNoTracking()
                .Select(i => new SubjectInfoDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    Teachers = i.Teachers,
                    LectureStartDate = i.LectureStartDate,
                    LectureEndDate = i.LectureEndDate,
                    PracticeStartDate = i.PracticeStartDate,
                    PracticeEndDate = i.PracticeEndDate
                })
                .SingleOrDefault(s => s.Id == subjectInfoId);

            return subjectInfo;
        }

        public ServiceResult UpdateSubjectInfo(SubjectInfoDto subjectInfoDto, int subjectInfoId)
        {
            var errors = SubjectInfoValidator.Validate(subjectInfoDto);
            if (errors.Any())
            {
                var result = new ServiceResult(Result.Error);
                result.Errors.AddRange(errors);
                return result;
            }

            SubjectInfoEntity info = Context.SubjectInfos.SingleOrDefault(i => i.Id == subjectInfoId);

            info.Title = subjectInfoDto.Title;
            info.Teachers = subjectInfoDto.Teachers;
            info.LectureStartDate = subjectInfoDto.LectureStartDate;
            info.LectureEndDate = subjectInfoDto.LectureEndDate;
            info.PracticeStartDate = subjectInfoDto.PracticeStartDate;
            info.PracticeEndDate = subjectInfoDto.PracticeEndDate;
            info.IsApproved = true;

            Context.SaveChanges();

            UpdateApprovedStatus(info.SubjectId);

            return new ServiceResult(Result.OK, info.SubjectId);
        }
    }
}
