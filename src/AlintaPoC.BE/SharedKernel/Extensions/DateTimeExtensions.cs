using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly string dateFormat = "dd/MM/yyyy";

        public static DateTime ParseDate(this string date)
        {
            DateTime res = DateTime.ParseExact(date, dateFormat, CultureInfo.CurrentUICulture.DateTimeFormat);
            return res;
        }

        public static string ParseDate(this DateTime date)
        {
            string res = date.ToString(dateFormat);
            return res;
        }
    }
}
