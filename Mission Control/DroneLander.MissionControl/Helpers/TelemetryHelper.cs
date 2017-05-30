using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ppatierno.AzureSBLite.Messaging;
using Windows.UI.Xaml.Media;
using System.Net.Http;

namespace DroneLander.MissionControl.Helpers
{
    public static class TelemetryHelper
    {
        public static SubscriptionClient TelemetryClient { get; set; }

        public async static Task<bool> EnsureEventTopicAsync(string eventName)
        {
            bool successful = false;

            try
            {
                HttpClient client = new HttpClient();

                var result = await client.PostAsync($"https://traininglabservices.azurewebsites.net/api/telemetry/?eventName={eventName}", null);

                successful = true;

            }
            catch { }
         
            return successful;
        }
 
        public static void StartListening(string eventName)
        {
            try
            {
                string HubConnectionString = "Endpoint=sb://dronelandermissioncontrol.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=w4LhH30a49mkOqD5qdgQ5qo9/odH27XCF15Kiy5f6Ek=";

                if (TelemetryClient == null)
                {
                    TelemetryClient = SubscriptionClient.CreateFromConnectionString(HubConnectionString, eventName, "MISSIONCONTROL");
                }
             
                TelemetryClient.OnMessage((receivedMessage) =>
                {
                    UpdateDisplay(receivedMessage);
                });
            }
            catch (Exception ex)
            {

            }
        }

        private async static void UpdateDisplay(BrokeredMessage receivedMessage)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                UpdateUserDisplay(receivedMessage);
            });
            
        }

        private static void UpdateUserDisplay(BrokeredMessage receivedMessage)
        {
            try
            {
                if (App.ViewModel.MissionSettings.Status == Common.MissionStatusType.InProgress)
                {
                    string userId = (string)receivedMessage.Properties["UserId"];

                    string displayName = (string)receivedMessage.Properties["DisplayName"];
                    string tagline = (string)receivedMessage.Properties["Tagline"];
                    double altitude = (double)receivedMessage.Properties["Altitude"];
                    double descentRate = (double)receivedMessage.Properties["DescentRate"];
                    double fuelRemaining = (double)receivedMessage.Properties["Fuel"];
                    double thrust = (double)receivedMessage.Properties["Thrust"];

                    var currentUser = App.ViewModel.CurrentActivity.Where(w => w.Id.Equals(userId)).FirstOrDefault();

                    if (currentUser == null)
                    {
                        SolidColorBrush themeBrush = new SolidColorBrush(App.ViewModel.GetRandomColor());

                        currentUser = new ActivityInformation()
                        {
                            ThemeBrush = themeBrush,
                            Altitude = altitude,
                            DescentRate = descentRate,
                            FuelRemaining = fuelRemaining,
                            Id = userId,
                            Tagline = tagline,
                            DisplayName = displayName,
                            Thrust = thrust,
                        };

                        App.ViewModel.CurrentActivity.Insert(0, currentUser);

                        App.ViewModel.UpdateHasActiveActivity();

                        App.ViewModel.AddDroneAsync(currentUser, Common.LandingActivityType.Initialize);
                    }
                    else
                    {
                        currentUser.Altitude = altitude;
                        currentUser.DescentRate = descentRate;
                        currentUser.FuelRemaining = fuelRemaining;
                        currentUser.Thrust = thrust;

                        if (altitude == 0.0)
                        {
                            currentUser.Tagline = tagline;

                            App.ViewModel.CompleteDroneLandingAsync(currentUser);
                        }
                    }
                }
            }
            catch { }

        }
    }
}
