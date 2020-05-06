using AutoMapper;
using NsauT.Shared.Enums;
using NsauT.Web.Areas.Manage.Helpers;

namespace NsauT.Web.Tools.Mapping
{
    public class StringToPeriodNumberConverter : ITypeConverter<string, PeriodNumber>
    {
        public PeriodNumber Convert(string source, PeriodNumber destination, ResolutionContext context)
        {
            return PeriodNumbersHelper.ConvertStringToPeriodNumber(source);
        }
    }
}
