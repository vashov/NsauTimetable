using System.IO;
using Xamarin.Essentials;

namespace NsauTimetable.Client.DAL.Helpers
{
    public class PathHelper
    {
        private const string DATABASENAME = "litedb.db";

        public static string GetLocalDbPath()
        {
            return GetLocalPath(DATABASENAME);
        }

        private static string GetLocalPath(string filename)
        {
            return Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
