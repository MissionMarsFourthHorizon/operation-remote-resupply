using DroneLander.Service.DataObjects;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DroneLander.Service.Helpers
{
    public class LandingParameters
    {        
        public double Altitude;
        public double Velocity;
        public double Fuel;
        public double Thrust;
    }

    public class TelemetryHelper
    {
        public async static Task SendToMissionControlAsync(TelemetryItem telemetry)
        {   
            var client = TopicClient.CreateFromConnectionString(Common.CoreConstants.HubConnectionString, Common.CoreConstants.HubTopicName);

            BrokeredMessage bm = new BrokeredMessage();

            bm.Label = telemetry.UserId;
            bm.Properties["UserId"] = telemetry.UserId;
            bm.Properties["TeamName"] = telemetry.TeamName;
            bm.Properties["Tagline"] = telemetry.Tagline;
            bm.Properties["Altitude"] = telemetry.Altitude;
            bm.Properties["DescentRate"] = telemetry.DescentRate;
            bm.Properties["Fuel"] = telemetry.Fuel;
            bm.Properties["Thrust"] = telemetry.Thrust;
           
            try
            {
                await client.SendAsync(bm);
            }
            catch
            {

            }

            return;
        }
    }
}