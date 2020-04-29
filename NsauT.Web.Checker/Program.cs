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
            ConnectionString = Configuration.GetConnectionString("TimetableDatabase");

            var parser = new ParserWorker();
            parser.TimetableFileParsed += StartCheckTimetables;

            bool downloadOnlyFirstTimetable = IsTestRun(args);
            parser.StartParse(downloadOnlyFirstTimetable);
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
            var timetableChecker = new TimetableChecker();
            var timetableSerializer = new TimetableSerializer();
            var hashCoder = new HashCoder();

            List<TimetableModel> timetables = parserArgs.Timetables;

            DbContextOptions options = new DbContextOptionsBuilder().UseNpgsql(ConnectionString).Options;
            using (var context = new ApplicationContext(options))
            {
                foreach (TimetableModel timetable in timetables)
                {
                    string jsonTimetable = timetableSerializer.SerializeToJson(timetable);
                    string hash = hashCoder.GetSha256Hash(jsonTimetable);

                    TimetableEntity timetableEntity = context.Timetables
                        .FirstOrDefault(t => t.Key == timetable.Key);

                    if (timetableEntity == null)
                    {
                        timetableChecker.AddTimetable(context, timetable, hash);

                        // if (added) need notify?
                        continue;
                    }

                    if (timetableChecker.IsSameTimetable(timetableEntity, hash))
                    {
                        continue;
                    }

                    timetableChecker.UpdateTimetable(context, timetableEntity, timetable, hash);
                    // if (updated) need notify?
                }
            }
        }
    }
}
