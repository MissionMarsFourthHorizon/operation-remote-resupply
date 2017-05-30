using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLander.MissionControl
{
    public static class StorageExtensions
    {
        public static double EvalToDouble(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            return (composite.ContainsKey(key)) ? Convert.ToDouble(composite[key]) : 0.0;
        }

        public static long EvalToLongNumber(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            return (composite.ContainsKey(key)) ? Convert.ToInt64(composite[key]) : 0;
        }

        public static int EvalToNumber(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            return (composite.ContainsKey(key)) ? Convert.ToInt16(composite[key]) : 0;
        }

        public static bool EvalToBoolean(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            return (composite.ContainsKey(key)) ? (bool)composite[key] : false;
        }

        public static Guid EvalToGuid(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            return (composite.ContainsKey(key)) ? new Guid(composite[key] + "") : Guid.Empty;
        }

        public static DateTime EvalToDateTime(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            return (composite.ContainsKey(key)) ? DateTime.Parse(composite[key] + "") : DateTime.Now;
        }

        public static DateTimeOffset EvalToDateTimeOffset(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            return (composite.ContainsKey(key)) ? DateTimeOffset.Parse(composite[key] + "") : new DateTimeOffset(DateTime.Now);
        }

        public static TimeSpan? EvalToTimespan(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            if (composite.ContainsKey(key))
            {
                TimeSpan timespan = TimeSpan.FromSeconds(0);

                TimeSpan.TryParse(composite[key] + "", out timespan);

                if (timespan == TimeSpan.FromSeconds(0))
                {
                    return null;
                }
                else
                {
                    return timespan;
                }  
            }
            else
            {
                return null;
            }  
        }

        public static string EvalToString(this Windows.Storage.ApplicationDataCompositeValue composite, string key)
        {
            return (composite.ContainsKey(key)) ? (composite[key] + "").Trim() : "";
        }
    }
}
