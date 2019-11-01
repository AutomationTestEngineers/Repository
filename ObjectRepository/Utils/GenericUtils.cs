using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository
{
    public class GenericUtils
    {
        public static bool IsValueOdd  {  get { return (new Random()).Next() % 2 != 0; }  }

        public static int GetRandomNumber(int startNumber, int endNumber)
        {            
            return (new Random()).Next(startNumber, endNumber);
        }

        public static string GenerateDate(int months,int days, int years, string format = "MM/dd/yyyy")
        {
            DateTime date = System.DateTime.Now;
            date = date.AddYears(years);
            date = date.AddMonths(months);
            date = date.AddDays(months);
            var outDateTime = format != null ? date.ToString(format, CultureInfo.InvariantCulture) : date.GetDateTimeFormats('d')[0].ToString(CultureInfo.InvariantCulture);
            return outDateTime;
        }      
    }
}
