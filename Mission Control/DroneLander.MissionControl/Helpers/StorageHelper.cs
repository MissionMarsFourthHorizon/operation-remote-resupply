using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLander.MissionControl.Helpers
{
    public static class StorageHelper
    {
        public static bool DeleteMissionSettings()
        {
            bool successful = false;

            try
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;                
                localSettings.Values.Remove("MissionSettings");

                successful = true;
            }
            catch (Exception ex)
            {

            }

            return successful;
        }

        public static bool DeleteCompletionCounts()
        {
            bool successful = false;

            try
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values.Remove("CompletionCounts");

                successful = true;
            }
            catch (Exception ex)
            {

            }

            return successful;
        }

        public static bool SaveMissionSettings(MissionSettingsInformation missionSettings)
        {
            bool successful = false;

            try
            {
                Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();

                composite["EventName"] = missionSettings.EventName;
                composite["Duration"] = missionSettings.Duration;
                composite["StartTime"] = missionSettings.StartTime.ToString();                

                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["MissionSettings"] = composite;

                successful = true;
            }
            catch (Exception ex)
            {

            }

            return successful;
        }

        public static CompletionCountInformation GetCompletionCounts()
        {
            CompletionCountInformation completionCountInformation = new CompletionCountInformation()
            {
                SuccessfulLandings = 0,
                Failures = 0,
            };

            Windows.Storage.ApplicationDataCompositeValue composite = null;

            try
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                composite = (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["CompletionCounts"];

                if (composite != null)
                {
                    completionCountInformation = new CompletionCountInformation()
                    {
                        SuccessfulLandings = composite.EvalToNumber("SuccessfulLandings"),
                        Failures = composite.EvalToNumber("Failures"),                        
                    };
                }
            }
            catch (Exception ex)
            {

            }

            return completionCountInformation;
        }

        public static bool SaveCompletionCounts(CompletionCountInformation completionCounts)
        {
            bool successful = false;

            try
            {
                Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();

                composite["SuccessfulLandings"] = completionCounts.SuccessfulLandings;
                composite["Failures"] = completionCounts.Failures;
                
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["CompletionCounts"] = composite;

                successful = true;
            }
            catch (Exception ex)
            {

            }

            return successful;
        }

        public static MissionSettingsInformation GetMissionSettings()
        {
            MissionSettingsInformation missionSettings = null;

            Windows.Storage.ApplicationDataCompositeValue composite = null;

            try
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                composite = (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["MissionSettings"];

                if (composite != null)
                {
                    missionSettings = new MissionSettingsInformation()
                    {
                        EventName = composite.EvalToString("EventName"),
                        Duration = composite.EvalToDouble("Duration"),
                        StartTime = composite.EvalToTimespan("StartTime"),
                    };
                }

            }
            catch (Exception ex)
            {

            }

            return missionSettings;
        }

      
    }
}
