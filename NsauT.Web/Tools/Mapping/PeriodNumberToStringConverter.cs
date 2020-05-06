using AutoMapper;
using NsauT.Shared.Enums;
using NsauT.Web.Areas.Manage.Helpers;

namespace NsauT.Web.Tools.Mapping
{
    public class PeriodNumberToStringConverter : ITypeConverter<PeriodNumber, string>
    {
        public string Convert(PeriodNumber source, string destination, ResolutionContext context)
        {
            return PeriodNumbersHelper.ConvertPeriodNumberToString(source);
        }
    }
}
