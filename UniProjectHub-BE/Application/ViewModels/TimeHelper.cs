using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public static class TimeHelper
    {
        public static DateTime GetVietnamTime()
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.Now, vietnamTimeZone);
        }
    }
}
