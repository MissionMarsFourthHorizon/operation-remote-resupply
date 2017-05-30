using DroneLander.MissionControl.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace DroneLander.MissionControl.Converters
{

    public class MissionStatusToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isEnabled = false;

            MissionStatusType status = (Common.MissionStatusType)value;
            MissionActionType action = (MissionActionType)Enum.Parse(typeof(MissionActionType), parameter+"", true);

            switch(action)
            {
                case MissionActionType.CanSave:
                    isEnabled = (status == MissionStatusType.Unconfigured);
                    break;
                case MissionActionType.CanDelete:
                    isEnabled = (status != MissionStatusType.Unconfigured);
                    break;
                case MissionActionType.CanRestart:
                    isEnabled = (status == MissionStatusType.InProgress);
                    break;
                case MissionActionType.CanStart:
                    isEnabled = (status == MissionStatusType.Configured);
                    break;
                case MissionActionType.CanEnd:
                    isEnabled = (status == MissionStatusType.InProgress);
                    break;
            }

            return isEnabled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class PrimaryMissionStatusLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Common.MissionStatusType status = (Common.MissionStatusType)value;

            switch (status)
            {
                case Common.MissionStatusType.Complete:
                    return "(mission complete)";
                case Common.MissionStatusType.Configured:
                    return "(not in progress)";
                case Common.MissionStatusType.InProgress:
                    return "(waiting for resupply attempts)";
                case Common.MissionStatusType.Unconfigured:
                    return "(no resupply missions scheduled)";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class BooleanToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class MissionStatusLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Common.MissionStatusType status = (Common.MissionStatusType)value;

            switch (status)
            {
                case Common.MissionStatusType.Complete:
                    return "complete";
                case Common.MissionStatusType.Configured:
                    return "not in progress";
                case Common.MissionStatusType.InProgress:
                    return "in progress";
                case Common.MissionStatusType.Unconfigured:
                    return "unscheduled";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class MissionClockLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (App.ViewModel.MissionSettings.StartTime == null)
            {
                return "00:00:00";
            }
            else
            {
                double missionDuration = App.ViewModel.MissionSettings.Duration;
                TimeSpan missionStartTime = (TimeSpan)App.ViewModel.MissionSettings.StartTime;
                TimeSpan timeOfDay = DateTime.Now.TimeOfDay;

                var timeLeft = (TimeSpan.FromHours(missionDuration) - TimeSpan.FromMinutes((timeOfDay.TotalMinutes - missionStartTime.TotalMinutes)));

                return (timeLeft.TotalSeconds <= 0.0) ? "00:00:00" : timeLeft.ToString("hh\\:mm\\:ss");
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class FuelStatusForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double fuelRemaining = Math.Min(1, (double)value);

            return (fuelRemaining < 0.1) ? App.Current.Resources["AppActivityRedBrush"] : App.Current.Resources["AppActivityWhiteBrush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class ToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value + "").ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class DecimalDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string extra = (parameter == null) ? "" : (string)parameter;

            return ((double)value).ToString("F0") + extra;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class ThousandsDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((double)value / 1000).ToString("0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public class AltitudeLocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Int32 altitude = System.Convert.ToInt32(value);
            int boxHeight = 160;

            int finalAltitude = Math.Min(5000, altitude);
            double finalHeight = finalAltitude / 5000.0;
            double next = boxHeight * finalHeight;
            return boxHeight - next;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    
    public class PercentDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((Math.Min(1, (double)value)).ToString("P0")).Replace(" ", "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public sealed class HasContentToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {  

            return string.IsNullOrEmpty(value + "") ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToBoolean(value) ? parameter : null;
        }
    }

    public sealed class DroneCountLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int newValue = (int)value;

            return (newValue < 0) ? "" : newValue.ToString("00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToBoolean(value) ? parameter : null;
        }
    }

    public sealed class LocalTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var time = (DateTime)value;

            return (time.ToLocalTime().ToString("hh:mm:ss"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToBoolean(value) ? parameter : null;
        }
    }

    public sealed class MartianTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var timespan = (TimeSpan)value;

            return (timespan.ToString("hh\\:mm\\:ss"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToBoolean(value) ? parameter : null;
        }
    }
}
