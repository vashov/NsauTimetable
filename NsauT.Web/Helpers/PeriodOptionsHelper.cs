using Microsoft.AspNetCore.Mvc.Rendering;
using NsauT.Shared.BusinessModels;
using System;
using System.ComponentModel;

namespace NsauT.Web.Helpers
{
    public class PeriodOptionsHelper
    {
        private const string None = "Нет";
        private const string Once = "Пара один раз";
        private const string Since = "Начитается с даты ...";
        private const string Until = "Идет до даты ...";
        private const string OnceDifferentCabinet = "Другой кабинет в дату ...";

        private static readonly string[] _optionsList = { None, Once, Since, Until, OnceDifferentCabinet };

        public static SelectList GetPeriodOptionsList(PeriodOption? defaultPeriodOption = null)
        {
            PeriodOption defaultOption = defaultPeriodOption ?? PeriodOption.None;
            string selectedPeriodOption= ConvertPeriodOptionToString(defaultOption);
            SelectList periodOptionsList = new SelectList(_optionsList, selectedPeriodOption);
            return periodOptionsList;
        }

        public static PeriodOption ConvertStringToPeriodOption(string periodOption)
        {
            switch (periodOption)
            {
                case None:
                    return PeriodOption.None;
                case Once:
                    return PeriodOption.Once;
                case Since:
                    return PeriodOption.Since;
                case Until:
                    return PeriodOption.Until;
                case OnceDifferentCabinet:
                    return PeriodOption.OnceDifferentCabinet;
                default:
                    throw new ArgumentException($"Can't convert *{periodOption}* to enum {typeof(PeriodOption)}");
            }
        }

        private static string ConvertPeriodOptionToString(PeriodOption periodOption)
        {
            switch (periodOption)
            {
                case PeriodOption.None:
                    return None;
                case PeriodOption.Once:
                    return Once;
                case PeriodOption.Since:
                    return Since;
                case PeriodOption.Until:
                    return Until;
                case PeriodOption.OnceDifferentCabinet:
                    return OnceDifferentCabinet;
                default:
                    throw new InvalidEnumArgumentException(nameof(periodOption), (int)periodOption,
                        typeof(PeriodOption));
            }
        }
    }
}
