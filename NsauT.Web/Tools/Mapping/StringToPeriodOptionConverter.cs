using AutoMapper;
using NsauT.Shared.Enums;
using NsauT.Web.Areas.Manage.Helpers;

namespace NsauT.Web.Tools.Mapping
{
    public class StringToPeriodOptionConverter : ITypeConverter<string, PeriodOption>
    {
        public PeriodOption Convert(string source, PeriodOption destination, ResolutionContext context)
        {
            return PeriodOptionsHelper.ConvertStringToPeriodOption(source);
        }
    }
}
