using Microsoft.AspNetCore.Mvc.ModelBinding;
using NsauT.Shared.Enums;
using NsauT.Web.Areas.Manage.Models.PeriodController;

namespace NsauT.Web.Areas.Manage.Helpers
{
    public class PeriodValidator
    {
        private ModelStateDictionary _modelState;

        private PeriodValidator()
        {
        }

        public static PeriodValidator Create(ModelStateDictionary modelState)
        {
            var validator = new PeriodValidator();
            validator._modelState = modelState;
            return validator;
        }

        public void ValidateAndUpdateModelState(PeriodBindingModel period, PeriodOption periodOption)
        {
            string errorMessage;
            if (!IsValidPeriodOption(periodOption, period, out errorMessage))
            {
                _modelState.AddModelError(nameof(period.Option), errorMessage);
            }

            if (!period.IsLecture && string.IsNullOrWhiteSpace(period.Subgroup))
            {
                errorMessage = "Пара не лекция, но подгруппа не указана";
                _modelState.AddModelError(nameof(period.Subgroup), errorMessage);
            }
        }

        private bool IsValidPeriodOption(PeriodOption periodOption, PeriodBindingModel period,
            out string errorMessage)
        {
            if (periodOption == PeriodOption.None && !string.IsNullOrEmpty(period.OptionCabinet))
            {
                errorMessage = "Пара без доп. условия, но указан доп. кабинет";
                return false;
            }

            if (periodOption == PeriodOption.None && period.OptionDate != null)
            {
                errorMessage = "Пара без доп. условия, но указана доп. дата";
                return false;
            }

            if (periodOption == PeriodOption.Once && period.OptionDate == null)
            {
                errorMessage = "Пара единожды, но доп. дата не указана";
                return false;
            }

            if (periodOption == PeriodOption.OnceDifferentCabinet && string.IsNullOrWhiteSpace(period.OptionCabinet))
            {
                errorMessage = "Пара однажды в другом кабинете, но доп. кабинет не указан";
                return false;
            }

            if (periodOption == PeriodOption.OnceDifferentCabinet && period.OptionDate == null)
            {
                errorMessage = "Пара однажды в другом кабинете, но доп. дата не указана";
                return false;
            }

            if (periodOption == PeriodOption.Since && period.OptionDate == null)
            {
                errorMessage = "Пара начинается с определенной даты, но доп. дата не указана";
                return false;
            }

            if (periodOption == PeriodOption.Until && period.OptionDate == null)
            {
                errorMessage = "Пара идет до определенной даты, но доп. дата не указана";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
