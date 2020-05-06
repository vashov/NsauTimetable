using AutoMapper;
using NsauT.Web.Areas.Manage.Helpers;
using System;

namespace NsauT.Web.Tools.Mapping
{
    public class DayOfWeekToStringConverter : ITypeConverter<DayOfWeek, string>
    {
        public string Convert(DayOfWeek source, string destination, ResolutionContext context)
        {
            return SchoolDayHelper.GetDayName(source);
        }
    }
}
