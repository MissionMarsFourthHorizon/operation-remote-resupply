using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLander.MissionControl
{
    public static class DateTimeExtensions
    {
        public static TimeSpan ToMartianTime(this DateTime value)
        {   
            return System.TimeSpan.FromHours((value.ToMartianSolDate() % 1) * 24);
        }

        public static double ToMartianSolDate(this DateTime value)
        {
            DateTime earthEpochDate = new System.DateTime(1970, 1, 1);
            double elapsedMilliseconds = (value - earthEpochDate).TotalMilliseconds;
            double epochJulianDate = 2440587.5 + (elapsedMilliseconds / (8.64 * Math.Pow(10, 7)));
            double terrestrialJulianDate = epochJulianDate + (37 + 32.184) / 86400;
            double martianEpochDifference = terrestrialJulianDate - 2451545.0;
            double martianSolDate = (((martianEpochDifference - 4.5) / 1.027491252) + 44796.0 - 0.00096);

            return martianSolDate;
        }
    }
}
