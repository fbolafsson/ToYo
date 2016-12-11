using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitSeydisfjordurModule.Extensions
{
    public static class StringExtensions
    {
        public static int ParseMonthToInt(this string month)
        {
            switch (month.ToLower())
            {
                case "1":
                case "01":
                case "jan":
                case "janúar":
                case "januar":
                case "january":
                    return 1;
                case "2":
                case "02":
                case "feb":
                case "februar":
                case "february":
                    return 2;
                case "3":
                case "03":
                case "mar":
                case "mars":
                    return 3;
                case "4":
                case "04":
                case "apr":
                case "apríl":
                case "april":
                    return 4;
                case "5":
                case "05":
                case "maí":
                case "mai":
                case "may":
                    return 5;
                case "6":
                case "06":
                case "jun":
                case "jún":
                case "júní":
                case "june":
                    return 6;
                case "7":
                case "07":
                case "jul":
                case "júl":
                case "júlí":
                case "july":
                    return 7;
                case "8":
                case "08":
                case "agu":
                case "ágú":
                case "agust":
                case "ágúst":
                case "aug":
                case "august":
                    return 8;
                case "9":
                case "09":
                case "sep":
                case "september":
                    return 9;
                case "10":
                case "okt":
                case "oct":
                case "október":
                case "oktober":
                case "october":
                    return 10;
                case "11":
                case "nov":
                case "nóv":
                case "november":
                case "nóvember":
                    return 11;
                case "12":
                case "des":
                case "dec":
                case "desember":
                case "december":
                    return 12;
                default:
                    throw new ArgumentException(nameof(month));
            }
        }
    }
}
