using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace NsauT.Web.Checker
{
    internal class TimetableUpdater
    {
        public bool IsSameTimetable(TimetableEntity timetable, TimetableEntity newTimetable)
        {
            return HashCoder.IsSameHashes(timetable.Hash, newTimetable.Hash);
        }

        public void UpdateTimetable(ApplicationContext context, TimetableEntity timetableEntity, 
            TimetableEntity newTimetable)
        {
            timetableEntity.IsApproved = false;
            timetableEntity.Hash = newTimetable.Hash;

            UpdateGroups(timetableEntity.Groups, newTimetable.Groups);
            UpdateSubjects(timetableEntity.Subjects, newTimetable.Subjects);

            context.SaveChanges();
        }

        public void AddTimetable(ApplicationContext context, TimetableEntity newTimetable)
        {
            context.Timetables.Add(newTimetable);
            context.SaveChanges();
        }

        private void UpdateGroups(List<GroupEntity> groups, IReadOnlyCollection<GroupEntity> newGroups)
        {
            groups.RemoveAll(g => newGroups.All(m => m.Number != g.Number));
            groups.AddRange(newGroups.Where(m => groups.All(g => g.Number != m.Number)));
        }

        private void UpdateSubjects(List<SubjectEntity> subjects, IReadOnlyCollection<SubjectEntity> newSubjects)
        {
            subjects.RemoveAll(s => newSubjects.All(n => n.Info.Title != s.Info.Title));

            foreach (SubjectEntity newSubject in newSubjects)
            {
                SubjectEntity subjectEntity = subjects.FirstOrDefault(s => s.Info.Title == newSubject.Info.Title);

                if (subjectEntity == null)
                {
                    subjects.Add(newSubject);
                    continue;
                }

                if (HashCoder.IsSameHashes(subjectEntity.Hash, newSubject.Hash))
                {
                    continue;
                }

                UpdateSubject(subjectEntity, newSubject);
            }
        }

        private void UpdateSubject(SubjectEntity subject, SubjectEntity newSubject)
        {
            subject.Hash = newSubject.Hash;
            subject.IsApproved = false;

            if (!HashCoder.IsSameHashes(subject.Info.Hash, newSubject.Info.Hash))
            {
                UpdateSubjectInfo(subject.Info, newSubject.Info);
            }

            if (HashCoder.IsSameHashes(subject.HashDays, newSubject.HashDays))
            {
                return;
            }

            UpdateDays(subject.Days, newSubject.Days);
        }

        private void UpdateDays(List<SchoolDayEntity> days, IReadOnlyCollection<SchoolDayEntity> newDays)
        {
            days.RemoveAll(d => newDays.All(n => n.Day != d.Day && n.IsDayOfEvenWeek != d.IsDayOfEvenWeek));

            foreach (SchoolDayEntity newDay in newDays)
            {
                SchoolDayEntity dayEntity = days
                    .FirstOrDefault(d => d.Day == newDay.Day && d.IsDayOfEvenWeek == newDay.IsDayOfEvenWeek);

                if (dayEntity == null)
                {
                    days.Add(newDay);
                    continue;
                }

                if (HashCoder.IsSameHashes(dayEntity.Hash, newDay.Hash))
                {
                    continue;
                }

                UpdateDay(dayEntity, newDay);
            }
        }

        private void UpdateDay(SchoolDayEntity day, SchoolDayEntity newDay)
        {
            day.Hash = newDay.Hash;
            day.IsApproved = false;

            UpdatePeriods(day.Periods, newDay.Periods);
        }

        private void UpdatePeriods(List<PeriodEntity> periods, IReadOnlyCollection<PeriodEntity> newPeriods)
        {
            periods.RemoveAll(p => newPeriods.All(n => n.Number != p.Number));

            foreach (PeriodEntity newPeriod in newPeriods)
            {
                PeriodEntity periodEntity = periods.FirstOrDefault(p => p.Number == newPeriod.Number);

                if (periodEntity == null)
                {
                    periods.Add(newPeriod);
                    continue;
                }

                if (HashCoder.IsSameHashes(periodEntity.Hash, newPeriod.Hash))
                {
                    continue;
                }

                UpdatePeriod(periodEntity, newPeriod);
            }
        }

        private void UpdatePeriod(PeriodEntity period, PeriodEntity newPeriod)
        {
            period.Hash = newPeriod.Hash;
            period.IsApproved = false;

            period.Cabinet = newPeriod.Cabinet;
            period.Subgroup = newPeriod.Subgroup;
            period.IsLecture = newPeriod.IsLecture;
            period.Option = newPeriod.Option;
            period.OptionDate = newPeriod.OptionDate;
            period.OptionCabinet = newPeriod.OptionCabinet;
        }

        private void UpdateSubjectInfo(SubjectInfoEntity info, SubjectInfoEntity newInfo)
        {
            info.Hash = newInfo.Hash;
            info.Title = newInfo.Title;
            info.Teachers = newInfo.Teachers;
            info.LectureStartDate = newInfo.LectureStartDate;
            info.LectureEndDate = newInfo.LectureEndDate;
            info.PracticeStartDate = newInfo.PracticeStartDate;
            info.PracticeEndDate = newInfo.PracticeEndDate;
            info.IsApproved = false;
        }
    }
}
