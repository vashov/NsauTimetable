using Microsoft.AspNetCore.Mvc.Rendering;
using NsauT.Shared.Enums;
using NsauT.Web.Areas.Manage.Helpers;

namespace NsauT.Web.Areas.Manage.Models.PeriodController
{
    public class PeriodViewModel
    {
        public PeriodViewModel(PeriodBindingModel periodBinging, int? periodId)
        {
            Period = periodBinging;
            PeriodId = periodId;

            PeriodNumber number = PeriodNumbersHelper.ConvertStringToPeriodNumber(Period.Number);
            PeriodOption option = PeriodOptionsHelper.ConvertStringToPeriodOption(Period.Option);

            NumbersList = PeriodNumbersHelper.GetPeriodNumbersList(number);
            OptionsList = PeriodOptionsHelper.GetPeriodOptionsList(option);
        }

        public int? PeriodId { get; set; }
        public PeriodBindingModel Period { get; set; }
        public SelectList NumbersList { get; set; }
        public SelectList OptionsList { get; set; }
    }
}
