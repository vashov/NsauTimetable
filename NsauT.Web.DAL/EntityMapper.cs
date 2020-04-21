using NsauT.Shared.BusinessModels;
using NsauT.Web.DAL.Models;
using System.Collections.Generic;

namespace NsauT.Web.DAL
{
    public class EntityMapper
    {
        public TimetableEntity MapTimetable(TimetableModel timetableModel, string hash)
        {
            var entity = new TimetableEntity
            {
                Key = timetableModel.Key,
                Groups = MapGroups(timetableModel.Groups),
                Subjects = MapSubjects(timetableModel.Subjects),
                Hash = hash
            };

            return entity;
        }

        public List<SubjectEntity> MapSubjects(List<SubjectModel> subjects)
        {
            var subjectEntities = new List<SubjectEntity>();

            foreach (SubjectModel subjectModel in subjects)
            {
                SubjectEntity entity = MapOneSubject(subjectModel);
                subjectEntities.Add(entity);
            }

            return subjectEntities;
        }

        public List<GroupEntity> MapGroups(List<string> groups)
        {
            var groupEntities = new List<GroupEntity>();

            foreach (string groupNumber in groups)
            {
                groupEntities.Add(new GroupEntity
                {
                    Number = groupNumber
                });
            }
            return groupEntities;
        }

        private SubjectEntity MapOneSubject(SubjectModel subjectModel)
        {
            SubjectEntity entity = new SubjectEntity
            {
                Title = subjectModel.Title,
                Teachers = subjectModel.Teachers,
                LectureStartDate = subjectModel.LectureStartDate,
                LectureEndDate = subjectModel.LectureEndDate,
                PracticeStartDate = subjectModel.PracticeStartDate,
                PracticeEndDate = subjectModel.PracticeEndDate,
                Days = MapSchoolDays(subjectModel.Days)
            };

            return entity;
        }

        private List<SchoolDayEntity> MapSchoolDays(List<SchoolDayModel> days)
        {
            var schoolDayEntities = new List<SchoolDayEntity>();

            foreach (SchoolDayModel day in days)
            {
                SchoolDayEntity entity = MapOneSchoolDay(day);
                schoolDayEntities.Add(entity);
            }

            return schoolDayEntities;
        }

        private SchoolDayEntity MapOneSchoolDay(SchoolDayModel day)
        {
            var entity = new SchoolDayEntity
            {
                Day = day.Day,
                IsDayOfEvenWeek = day.IsDayOfEvenWeek,
                Periods = MapPeriods(day.Periods)
            };

            return entity;
        }

        private List<PeriodEntity> MapPeriods(List<PeriodModel> periods)
        {
            var entities = new List<PeriodEntity>();

            foreach (PeriodModel period in periods)
            {
                PeriodEntity entity = MapOnePeriod(period);
                entities.Add(entity);
            }

            return entities;
        }

        private PeriodEntity MapOnePeriod(PeriodModel period)
        {
            var entity = new PeriodEntity
            {
                Number = period.Number,
                Cabinet = period.Cabinet,
                Subgroup = period.Subgroup,
                IsLecture = period.IsLecture,
                Option = period.Modification.Option,
                OptionDate = period.Modification.Date,
                OptionCabinet = period.Modification.Cabinet
            };

            return entity;
        }
    }
}
