using System.Web.Http;
using System.Web.Http.Tracing;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using DroneLander.Service.DataObjects;

namespace DroneLander.Service.Controllers
{
    [Authorize]
    [MobileAppController]
    public class TelemetryController : ApiController
    {
        // GET api/telemetry
        public string Get()
        {
            MobileAppSettingsDictionary settings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            string host = settings.HostName ?? "localhost";
            string greeting = $"Hello {host}. You are currently connected to Mission Control";

            return greeting;
        }

        // POST api/telemetry
        public async Task<string> Post(TelemetryItem telemetry)
        {
            await Helpers.TelemetryHelper.SendToMissionControlAsync(telemetry);

            return $"Telemetry for {telemetry.UserId} received by Mission Control.";
        }
    }
}
