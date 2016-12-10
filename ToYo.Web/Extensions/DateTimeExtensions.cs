using System;

namespace ToYo.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime DayInWeek(this DateTime datetime, DayOfWeek dayOfWeek)
        {
            var mondayDateDiff = ToInt(dayOfWeek) - ToInt(datetime.DayOfWeek);
            return datetime.AddDays(mondayDateDiff);
        }

        private static int ToInt(DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday)
            {
                return 7;
            }
            return (int)day;
        }
    }
}