using System;

namespace NsauTimetable.Parser.Helpers
{
    public enum PeriodNumber
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
        Sixth = 6
    }

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
