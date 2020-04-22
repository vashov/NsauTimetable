using Microsoft.AspNetCore.Mvc.Rendering;
using NsauT.Shared.Helpers;
using System;
using System.ComponentModel;

namespace NsauT.Web.Helpers
{
    public static class PeriodNumbersHelper
    {
        private const string First = "(1) Первая";
        private const string Second = "(2) Вторая";
        private const string Third = "(3) Третья";
        private const string Fourth = "(4) Четвертая";
        private const string Fifth = "(5) Пятая";
        private const string Sixth = "(6) Шестая";

        private static readonly string[] _numbersList = { First, Second, Third, Fourth, Fifth, Sixth };

        public static SelectList GetPeriodNumbersList(PeriodNumber? defaultPeriodNumber = null)
        {
            PeriodNumber defaultNumber = defaultPeriodNumber ?? PeriodNumber.First;
            string selectedPeriodNumber = ConvertPeriodNumberToString(defaultNumber);
            SelectList periodNumbersList = new SelectList(_numbersList, selectedPeriodNumber);
            return periodNumbersList;
        }

        public static PeriodNumber ConvertStringToPeriodNumber(string periodNumber)
        {
            switch (periodNumber)
            {
                case First:
                    return PeriodNumber.First;
                case Second:
                    return PeriodNumber.Second;
                case Third:
                    return PeriodNumber.Third;
                case Fourth:
                    return PeriodNumber.Fourth;
                case Fifth:
                    return PeriodNumber.Fifth;
                case Sixth:
                    return PeriodNumber.Sixth;
                default:
                    throw new ArgumentException($"Can't convert *{periodNumber}* to enum {typeof(PeriodNumber)}");
            }
        }

        private static string ConvertPeriodNumberToString(PeriodNumber periodNumber)
        {
            switch (periodNumber)
            {
                case PeriodNumber.First:
                    return First;
                case PeriodNumber.Second:
                    return Second;
                case PeriodNumber.Third:
                    return Third;
                case PeriodNumber.Fourth:
                    return Fourth;
                case PeriodNumber.Fifth:
                    return Fifth;
                case PeriodNumber.Sixth:
                    return Sixth;
                default:
                    throw new InvalidEnumArgumentException(nameof(periodNumber), (int)periodNumber, 
                        typeof(PeriodNumber));
            }
        }
    }
}
