using Microsoft.Extensions.Configuration;
using NsauT.Shared;
using NsauT.Shared.Models.BusinessModels;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using NsauT.Web.Parser;
using System;
using System.Collections.Generic;
using System.IO;

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
            parser.StartParse();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("checkersettings.json")
                .Build();
        }

        private async static void StartCheckTimetables(object sender, EventArgs e)
        {
            var parserArgs = e as ParserEventArgs;
            var timetableChecker = new TimetableChecker();
            var timetableSerializer = new TimetableSerializer();
            var hashCoder = new HashCoder();

            List<TimetableModel> timetables = parserArgs.Timetables;

            using (var reporitory = new TimetableRepository(ConnectionString))
            {
                foreach (TimetableModel timetable in timetables)
                {
                    string jsonTimetable = timetableSerializer.SerializeToJson(timetable);
                    string hash = hashCoder.GetSha256Hash(jsonTimetable);

                    TimetableEntity timetableEntity = await reporitory.GetItemAsync(timetable.TimetableId);

                    if (timetableEntity == null)
                    {
                        bool timetableAdded = await timetableChecker
                            .TryAddTimetableAsync(reporitory, timetable.TimetableId, hash, jsonTimetable);

                        // if (added) need notify?
                        continue;
                    }

                    if (!timetableChecker.CheckNeedUpdateTimetable(timetableEntity, hash))
                    {
                        continue;
                    }

                    bool updated = await timetableChecker
                        .TryUpdateTimetableAsync(reporitory, timetableEntity, jsonTimetable, hash);

                    if (!updated)
                    {
                        continue;
                    }

                    // if (updated) need notify?
                }
            }
        }
    }
}
