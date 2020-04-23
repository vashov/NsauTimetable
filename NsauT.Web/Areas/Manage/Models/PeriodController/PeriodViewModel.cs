using Microsoft.AspNetCore.Mvc.Rendering;
using NsauT.Shared.Enums;
using NsauT.Web.Areas.Manage.Helpers;
using NsauT.Web.DAL.Models;

namespace NsauT.Web.Areas.Manage.Models.PeriodController
{
    public class PeriodViewModel
    {
        public PeriodViewModel(PeriodEntity periodEntity, int dayId)
        {
            SelectList periodNumbersList = PeriodNumbersHelper.GetPeriodNumbersList(periodEntity?.Number);
            SelectList periodOptionsList = PeriodOptionsHelper.GetPeriodOptionsList(periodEntity?.Option);

            PeriodId = periodEntity?.Id;
            DayId = dayId;
            Period = new PeriodBindingModel
            {
                Number = (string)periodNumbersList.SelectedValue,
                Option = (string)periodOptionsList.SelectedValue
            };
            NumbersList = periodNumbersList;
            OptionsList = periodOptionsList;

            if (periodEntity != null)
            {
                Period.Cabinet = periodEntity.Cabinet;
                Period.Subgroup = periodEntity.Subgroup;
                Period.IsLecture = periodEntity.IsLecture;
                Period.OptionDate = periodEntity.OptionDate;
                Period.OptionCabinet = periodEntity.OptionCabinet;
            }
        }

        public PeriodViewModel(PeriodBindingModel periodBinging, int? periodId, int dayId)
        {
            Period = periodBinging;
            PeriodId = periodId;
            DayId = dayId;

            PeriodNumber number = PeriodNumbersHelper.ConvertStringToPeriodNumber(Period.Number);
            PeriodOption option = PeriodOptionsHelper.ConvertStringToPeriodOption(Period.Option);

            NumbersList = PeriodNumbersHelper.GetPeriodNumbersList(number);
            OptionsList = PeriodOptionsHelper.GetPeriodOptionsList(option);
        }

        public int? PeriodId { get; set; }
        public int DayId { get; set; }
        public PeriodBindingModel Period { get; set; }
        public SelectList NumbersList { get; set; }
        public SelectList OptionsList { get; set; }
    }
}
