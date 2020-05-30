using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NsauT.Shared.BusinessModels;
using NsauT.Shared.Tools;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using NsauT.Web.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NsauT.Web.Checker
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        private static string ConnectionString { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Checker started.");

            Configuration = GetConfiguration();
            ConnectionString = GetConnectionString();

            var parser = new ParserWorker();
            parser.TimetableFileParsed += StartCheckTimetables;

            bool downloadOnlyFirstTimetable = IsTestRun(args);
            parser.StartParse(downloadOnlyFirstTimetable);
        }

        private static string GetConnectionString()
        {
            string connString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (!string.IsNullOrEmpty(connString))
            {
                return connString;
            }

            connString = Configuration.GetConnectionString("TimetableDatabase");
            return connString;
        }

        private static bool IsTestRun(string[] args) => args.Contains("-test");

        private static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("checkersettings.json")
                .Build();
        }

        private static void StartCheckTimetables(object sender, EventArgs e)
        {
            var parserArgs = e as ParserEventArgs;
            var timetableChecker = new TimetableUpdater();

            List<TimetableModel> timetables = parserArgs.Timetables;

            DbContextOptions options = new DbContextOptionsBuilder()
                .UseNpgsql(ConnectionString, o => o.SetPostgresVersion(9, 6)).Options;

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
                        continue;
                    }

                    timetableChecker.UpdateTimetable(context, timetableEntity, newTimetable);
                    // if (updated) need notify?
                }
            }
        }
    }
}
