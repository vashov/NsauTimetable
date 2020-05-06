using AutoMapper;
using NsauT.Shared.Enums;
using NsauT.Web.Areas.Manage.Helpers;

namespace NsauT.Web.Tools.Mapping
{
    public class PeriodOptionToStringConverter : ITypeConverter<PeriodOption, string>
    {
        public string Convert(PeriodOption source, string destination, ResolutionContext context)
        {
            return PeriodOptionsHelper.ConvertPeriodOptionToString(source);
        }
    }
}
