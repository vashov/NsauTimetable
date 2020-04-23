using Microsoft.EntityFrameworkCore;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Linq;

namespace NsauT.Web.Areas.Manage.Helpers
{
    public class Approver
    {
        private TimetableContext _context;

        private Approver()
        { }

        public static Approver Create(TimetableContext context)
        {
            var approver = new Approver();
            approver._context = context;
            return approver;
        }

        public void CascadeUpdateApprovedDay(int dayId)
        {
            int subjectId = UpdateDayApprovedStatus(dayId);
            CascadeUpdateApprovedSubject(subjectId);
        }

        public void CascadeUpdateApprovedSubject(int subjectId)
        {
            int timetableId = UpdateSubjectApprovedStatus(subjectId);
            UpdateTimetableApprovedStatus(timetableId);
        }

        private int UpdateDayApprovedStatus(int dayId)
        {
            SchoolDayEntity day = _context.SchoolDays
                .Include(d => d.Periods)
                .Include(d => d.Subject)
                .FirstOrDefault(d => d.Id == dayId);

            day.IsApproved = day.Periods.All(p => p.IsApproved);

            //_context.SaveChanges();

            int subjectId = day.Subject.Id;
            return subjectId;
        }

        private int UpdateSubjectApprovedStatus(int subjectId)
        {
            SubjectEntity subject = _context.Subjects
                .Include(s => s.Days)
                .Include(s => s.Timetable)
                .FirstOrDefault(s => s.Id == subjectId);

            subject.IsApproved = subject.Days.All(d => d.IsApproved);

            //_context.SaveChanges();

            int timetableId = subject.Timetable.Id;
            return timetableId;
        }

        private bool UpdateTimetableApprovedStatus(int timetableId)
        {
            TimetableEntity timetable = _context.Timetables
                .Include(t => t.Subjects)
                .FirstOrDefault(t => t.Id == timetableId);

            timetable.IsApproved = timetable.Subjects.All(s => s.IsApproved);
            //_context.SaveChanges();
            return timetable.IsApproved;
        }
    }
}
