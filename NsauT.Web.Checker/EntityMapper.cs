using NsauT.Shared.BusinessModels;
using NsauT.Shared.Tools;
using NsauT.Web.DAL.Models;
using System.Collections.Generic;

namespace NsauT.Web.Checker
{
    public class EntityMapper
    {
        public TimetableEntity MapTimetable(TimetableModel timetableModel)
        {
            string jsonTimetable = TimetableSerializer.SerializeToJson(timetableModel);
            string hashTimetable = HashCoder.GetSha256Hash(jsonTimetable);

            var entity = new TimetableEntity
            {
                Key = timetableModel.Key,
                Groups = MapGroups(timetableModel.Groups),
                Subjects = MapSubjects(timetableModel.Subjects),
                Hash = hashTimetable
            };

            return entity;
        }

        private List<SubjectEntity> MapSubjects(List<SubjectModel> subjects)
        {
            var subjectEntities = new List<SubjectEntity>();

            foreach (SubjectModel subjectModel in subjects)
            {
                SubjectEntity entity = MapOneSubject(subjectModel);
                subjectEntities.Add(entity);
            }

            return subjectEntities;
        }

        private List<GroupEntity> MapGroups(List<string> groups)
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
            string subjectJson = TimetableSerializer.SerializeToJson(subjectModel);
            string subjectHash = HashCoder.GetSha256Hash(subjectJson);

            var info = new SubjectInfoEntity
            {
                Title = subjectModel.Title,
                Teachers = subjectModel.Teachers,
                LectureStartDate = subjectModel.LectureStartDate,
                LectureEndDate = subjectModel.LectureEndDate,
                PracticeStartDate = subjectModel.PracticeStartDate,
                PracticeEndDate = subjectModel.PracticeEndDate
            };

            string infoJson = TimetableSerializer.SerializeToJson(info);
            string infoHash = HashCoder.GetSha256Hash(infoJson);

            info.Hash = infoHash;

            string daysJson = TimetableSerializer.SerializeToJson(subjectModel.Days);
            string daysHash = HashCoder.GetSha256Hash(daysJson);

            SubjectEntity entity = new SubjectEntity
            {
                Info = info,
                Days = MapSchoolDays(subjectModel.Days),
                HashDays = daysHash,
                Hash = subjectHash
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
            string dayJson = TimetableSerializer.SerializeToJson(day);
            string dayHash = HashCoder.GetSha256Hash(dayJson);

            var entity = new SchoolDayEntity
            {
                Day = day.Day,
                IsDayOfEvenWeek = day.IsDayOfEvenWeek,
                Periods = MapPeriods(day.Periods),
                Hash = dayHash
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
            string periodJson = TimetableSerializer.SerializeToJson(period);
            string periodHash = HashCoder.GetSha256Hash(periodJson);

            var entity = new PeriodEntity
            {
                Number = period.Number,
                Cabinet = period.Cabinet,
                Subgroup = period.Subgroup,
                IsLecture = period.IsLecture,
                Option = period.Modification.Option,
                OptionDate = period.Modification.Date,
                OptionCabinet = period.Modification.Cabinet,
                Hash = periodHash
            };

            return entity;
        }
    }
}
