using System;
using System.ComponentModel;

namespace NsauT.Web.Areas.Manage.Helpers
{
    public static class SchoolDayHelper
    {
        private const string Monday    = "Понедельник";
        private const string Tuesday   = "Вторник";
        private const string Wednesday = "Среда";
        private const string Thursday  = "Четверг";
        private const string Friday    = "Пятница";
        private const string Saturday  = "Суббота";
        private const string Sunday    = "Воскресенье";

        public static string GetDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return Sunday;
                case DayOfWeek.Monday:
                    return Monday;
                case DayOfWeek.Tuesday:
                    return Tuesday;
                case DayOfWeek.Wednesday:
                    return Wednesday;
                case DayOfWeek.Thursday:
                    return Thursday;
                case DayOfWeek.Friday:
                    return Friday;
                case DayOfWeek.Saturday:
                    return Saturday;
                default:
                    throw new InvalidEnumArgumentException(nameof(day), (int)day, typeof(DayOfWeek));
            }
        }
    }
}
