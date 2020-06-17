using Microsoft.EntityFrameworkCore;
using NsauT.Shared.BusinessModels;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using NsauT.Web.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NsauT.Web.Checker
{
    public class TimetableChecker
    {
        private readonly string _connectionString;

        public TimetableChecker(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void StartCheckTimetables(object sender, EventArgs e)
        {
            var parserArgs = e as ParserEventArgs;
            var timetableChecker = new TimetableUpdater();

            List<TimetableModel> timetables = parserArgs.Timetables;

            DbContextOptions options = new DbContextOptionsBuilder()
                .UseNpgsql(_connectionString, o => o.SetPostgresVersion(9, 6)).Options;

            using (var context = new ApplicationContext(options))
            {
                foreach (TimetableModel timetable in timetables)
                {
                    TimetableEntity timetableEntity = context.Timetables
                        .Include(t => t.Groups)
                        .Include(t => t.Subjects)
                            .ThenInclude(s => s.Info)
                        .Include(t => t.Subjects)
                            .ThenInclude(s => s.Days)
                                .ThenInclude(d => d.Periods)
                        .SingleOrDefault(t => t.Key == timetable.Key);

                    TimetableEntity newTimetable = (new EntityMapper()).MapTimetable(timetable);

                    if (timetableEntity == null)
                    {
                        timetableChecker.AddTimetable(context, newTimetable);

                        // if (added) need notify?
                        continue;
                    }

                    if (timetableChecker.IsSameTimetable(timetableEntity, newTimetable))
                    {
                        timetableEntity.NotChanged = DateTime.UtcNow;
                        context.SaveChanges();
                        continue;
                    }

                    timetableChecker.UpdateTimetable(context, timetableEntity, newTimetable);
                    // if (updated) need notify?
                }
            }
        }
    }
}
