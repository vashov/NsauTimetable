using NsauT.Shared.Enums;
using NsauT.Web.BLL.Services.Period.DTO;
using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Period
{
    public static class PeriodValidator
    {
        public static List<(string Key, string Message)> Validate(PeriodDto period)
        {
            var errors = new List<(string, string)>();

            string errorMessage;
            if (!IsValidPeriodOption(period, out errorMessage))
            {
                var pair = (nameof(period.Option), errorMessage);
                errors.Add(pair);
            }

            if (!period.IsLecture && string.IsNullOrWhiteSpace(period.Subgroup))
            {
                errorMessage = "Пара не лекция, но подгруппа не указана";
                var pair = (nameof(period.Subgroup), errorMessage);
                errors.Add(pair);
            }

            return errors;
        }

        private static bool IsValidPeriodOption(PeriodDto period, out string errorMessage)
        {
            if (period.Option == PeriodOption.None && !string.IsNullOrEmpty(period.OptionCabinet))
            {
                errorMessage = "Пара без доп. условия, но указан доп. кабинет";
                return false;
            }

            if (period.Option == PeriodOption.None && period.OptionDate != null)
            {
                errorMessage = "Пара без доп. условия, но указана доп. дата";
                return false;
            }

            if (period.Option == PeriodOption.Once && period.OptionDate == null)
            {
                errorMessage = "Пара единожды, но доп. дата не указана";
                return false;
            }

            if (period.Option == PeriodOption.OnceDifferentCabinet && string.IsNullOrWhiteSpace(period.OptionCabinet))
            {
                errorMessage = "Пара однажды в другом кабинете, но доп. кабинет не указан";
                return false;
            }

            if (period.Option == PeriodOption.OnceDifferentCabinet && period.OptionDate == null)
            {
                errorMessage = "Пара однажды в другом кабинете, но доп. дата не указана";
                return false;
            }

            if (period.Option == PeriodOption.Since && period.OptionDate == null)
            {
                errorMessage = "Пара начинается с определенной даты, но доп. дата не указана";
                return false;
            }

            if (period.Option == PeriodOption.Until && period.OptionDate == null)
            {
                errorMessage = "Пара идет до определенной даты, но доп. дата не указана";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
