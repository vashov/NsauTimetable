using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NsauT.Web.Parser;
using System;
using System.IO;
using System.Linq;

namespace NsauT.Web.Checker
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Checker started.");

            Configuration = GetConfiguration();
            string connectionString = GetConnectionString();

            var parser = new ParserWorker();
            var checker = new TimetableChecker(connectionString);
            parser.TimetableFileParsed += checker.StartCheckTimetables;

            bool downloadOnlyFirstTimetable = false;//IsTestRun(args);
            parser.StartParse(downloadOnlyFirstTimetable);

            Console.WriteLine("Checker finished.");
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
    }
}
