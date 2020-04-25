using NsauT.Shared.Enums;
using System;

namespace NsauT.Shared.Helpers
{
    public static class PeriodHelper
    {
        public static PeriodNumber GetNextPeriodNumber(PeriodNumber periodNumber)
        {
            periodNumber++;
            return periodNumber;
        }

        public static bool IsFirstPeriod(PeriodNumber periodNumber)
        {
            return periodNumber == PeriodNumber.First;
        }

        public static bool IsValidPeriod(PeriodNumber periodNumber)
        {
            return Enum.IsDefined(typeof(PeriodNumber), periodNumber);
        }

        public static int GetCountOfPeriods()
        {
            return Enum.GetNames(typeof(PeriodNumber)).Length;
        }
    }
}
