using DroneLander.MissionControl.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Windows.UI;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace DroneLander.MissionControl
{
    public class MainViewModel : Common.ObservableBase
    {
        public MainViewModel(MissionSettingsInformation missionSettings, CompletionCountInformation completionCounts)
        {
            this.CurrentClock = new Helpers.TimerHelper();
            this.CurrentClock.StartClocks();

            this.CurrentEarthTime = DateTime.UtcNow;
            this.CurrentMartianTime = DateTime.UtcNow.ToMartianTime();
            this.CurrentActivity = new ObservableCollection<ActivityInformation>();
            this.BaseLocations = new ObservableCollection<PointCollection>();
            this.AvailableThemeColors = Helpers.ThemeHelper.GetAvailableColors();

            this.MissionDurations = this.AvailableMissionDurations;

            ConfigureMissionSettings(missionSettings, completionCounts);
        }
        
        private Random _randmonizer { get; set; }
        private Helpers.AudioHelper _audioHelper { get; set; }
        public Helpers.TimerHelper CurrentClock { get; set; }

        private Windows.UI.Xaml.Controls.GridView _activityGridView;
        public Windows.UI.Xaml.Controls.GridView ActivityGridView
        {
            get { return this._activityGridView; }
            set { this.SetProperty(ref this._activityGridView, value); }
        }

        private Windows.UI.Xaml.Controls.Grid _mapGrid;
        public Windows.UI.Xaml.Controls.Grid MapGrid
        {
            get { return this._mapGrid; }
            set { this.SetProperty(ref this._mapGrid, value); }
        }

        private MissionSettingsInformation _missionSettings;
        public MissionSettingsInformation MissionSettings
        {
            get { return this._missionSettings; }
            set { this.SetProperty(ref this._missionSettings, value); }
        }

        private bool _hasActiveActivity;
        public bool HasActiveActivity
        {
            get { return this._hasActiveActivity; }
            set { this.SetProperty(ref this._hasActiveActivity, value); }
        }

        private double _selectedMissionDuration;
        public double SelectedMissionDuration
        {
            get { return this._selectedMissionDuration; }
            set { this.SetProperty(ref this._selectedMissionDuration, value); }
        }

        private List<double> _missionDurations;
        public List<double> MissionDurations
        {
            get { return this._missionDurations; }
            set { this.SetProperty(ref this._missionDurations, value); }
        }

        public List<double> AvailableMissionDurations
        {
            get
            {
                List<double> durations = new List<double>();

                durations.Add(1.5);
                durations.Add(2.0);
                durations.Add(2.5);
                durations.Add(3.0);
                durations.Add(3.5);
                durations.Add(4.0);

                return durations;
            }
        }

        private Popup _currentSelectionPopup;
        public Popup CurrentSelectionPopup
        {
            get { return this._currentSelectionPopup; }
            set { this.SetProperty(ref this._currentSelectionPopup, value); }
        }

        private ObservableCollection<ActivityInformation> _currentActivity;
        public ObservableCollection<ActivityInformation> CurrentActivity
        {
            get { return this._currentActivity; }
            set { this.SetProperty(ref this._currentActivity, value); }
        }

        private ObservableCollection<PointCollection> _baseLocations;
        public ObservableCollection<PointCollection> BaseLocations
        {
            get { return this._baseLocations; }
            set { this.SetProperty(ref this._baseLocations, value); }
        }

        public List<Windows.UI.Color> AvailableThemeColors { get; set; }

        public Controls.DroneControl DroneControl { get; set; }

        private DateTime _currentEarthTime;
        public DateTime CurrentEarthTime
        {
            get { return this._currentEarthTime; }
            set { this.SetProperty(ref this._currentEarthTime, value); }
        }

        private TimeSpan _currentMartianTime;
        public TimeSpan CurrentMartianTime
        {
            get { return this._currentMartianTime; }
            set { this.SetProperty(ref this._currentMartianTime, value); }
        }

        private TimeSpan _currentMissionTimer;
        public TimeSpan CurrentMissionTimer
        {
            get { return this._currentMissionTimer; }
            set { this.SetProperty(ref this._currentMissionTimer, value); }
        }

        private int _activeDroneCount;
        public int ActiveDroneCount
        {
            get { return this._activeDroneCount; }
            set { this.SetProperty(ref this._activeDroneCount, value); }
        }

        private int _currentLandingsCount;
        public int CurrentLandingsCount
        {
            get { return this._currentLandingsCount; }
            set
            {
                this.SetProperty(ref this._currentLandingsCount, value);

                UpdateStoredCompletionCounts();
            }
        }

        private int _currentCrashesCount;
        public int CurrentCrashesCount
        {
            get { return this._currentCrashesCount; }
            set
            {
                this.SetProperty(ref this._currentCrashesCount, value);

                UpdateStoredCompletionCounts();
            }
        }

        private string _importantTitle;
        public string ImportantTitle
        {
            get { return this._importantTitle; }
            set { this.SetProperty(ref this._importantTitle, value); }
        }

        private string _importantMessage;
        public string ImportantMessage
        {
            get { return this._importantMessage; }
            set { this.SetProperty(ref this._importantMessage, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.SetProperty(ref this._isBusy, value); }
        }

        private void ConfigureMissionSettings(MissionSettingsInformation missionSettings, CompletionCountInformation completionCounts)
        {
            if (missionSettings == null)
            {
                missionSettings = new MissionSettingsInformation()
                {
                    Duration = 3.0,
                    EventName = "",
                    StartTime = null,
                    Status = Common.MissionStatusType.Unconfigured,
                };
            }
            else if (DateTime.Now.TimeOfDay > missionSettings.StartTime)
            {
                missionSettings.Status = Common.MissionStatusType.InProgress;

                this.CurrentClock.StartMissionTimer();
                Helpers.TelemetryHelper.StartListening(missionSettings.EventName);
            }
            else
            {
                missionSettings.Status = Common.MissionStatusType.Configured;
            }


            this.MissionSettings = missionSettings;
            this.SelectedMissionDuration = this.MissionDurations.Where(w => w.Equals(missionSettings.Duration)).FirstOrDefault();

            if (this.MissionSettings.Status == MissionStatusType.InProgress)
            {
                this.CurrentLandingsCount = completionCounts.SuccessfulLandings;
                this.CurrentCrashesCount = completionCounts.Failures;
            }

            this.MissionSettings.CanSave = this.MissionSettings.EventName.Length > 3 && this.MissionSettings.Status == Common.MissionStatusType.Unconfigured;
        }

        public void UpdateHasActiveActivity()
        {
            this.HasActiveActivity = this.CurrentActivity.Count > 0;
        }

        public void StartMission()
        {
            App.ViewModel.MissionSettings.StartTime = DateTime.Now.TimeOfDay;
            App.ViewModel.MissionSettings.Status = MissionStatusType.InProgress;

            Helpers.StorageHelper.SaveMissionSettings(App.ViewModel.MissionSettings);

            Helpers.TelemetryHelper.StartListening(App.ViewModel.MissionSettings.EventName);

            App.ViewModel.CurrentClock.StartMissionTimer();
        }
        
        public void RestartMission()
        {
            this.MissionSettings.StartTime = DateTime.Now.TimeOfDay;
            this.MissionSettings.Status = MissionStatusType.InProgress;

            Helpers.StorageHelper.SaveMissionSettings(this.MissionSettings);
        }

        public void EndMission()
        {
            this.MissionSettings.Status = MissionStatusType.Complete;
            this.CurrentActivity.Clear();

            Helpers.StorageHelper.SaveMissionSettings(this.MissionSettings);
        }

        public void DeleteMission()
        {
            this.MissionSettings = new MissionSettingsInformation()
            {
                Duration = 3.0,
                EventName = "",
                StartTime = null,
                Status = Common.MissionStatusType.Unconfigured,
            };

            this.SelectedMissionDuration = this.MissionDurations.Where(w => w.Equals(3.0)).FirstOrDefault();

            this.CurrentActivity.Clear();
            this.ActiveDroneCount = 0;
            RemoveAllDrones();
            this.CurrentLandingsCount = 0;
            this.CurrentCrashesCount = 0;

            Helpers.StorageHelper.DeleteMissionSettings();
            Helpers.StorageHelper.DeleteCompletionCounts();
        }


        private void UpdateStoredCompletionCounts()
        {
            Helpers.StorageHelper.SaveCompletionCounts(new CompletionCountInformation() { SuccessfulLandings = this.CurrentLandingsCount, Failures = this.CurrentCrashesCount });
        }

        public Color GetRandomColor()
        {
            return this.AvailableThemeColors[this._randmonizer.Next(this.AvailableThemeColors.Count - 1)];
        }

        public async void InitializeSystem(Windows.UI.Xaml.Controls.Grid mapGrid, Windows.UI.Xaml.Controls.GridView activityGridView)
        {
            this.MapGrid = mapGrid;
            this.ActivityGridView = activityGridView;

            this._randmonizer = new Random();
            this._audioHelper = new Helpers.AudioHelper();
            await this._audioHelper.InitializePlayer();
        }

        public void PlaySuccess()
        {
            this._audioHelper.PlaySuccess();
        }

        public void PlayFailure()
        {
            this._audioHelper.PlayFailure();
        }

        public void RemoveActivityItem(ActivityInformation droneUser)
        {
            if (droneUser != null) this.CurrentActivity.Remove(droneUser);

            UpdateHasActiveActivity();
        }

        private async void UpdateImportantMessageAsync(ActivityInformation activity, LandingActivityType landingType)
        {
            if (landingType == LandingActivityType.Success)
            {
                string tagline = (activity.Tagline + "").Trim();

                tagline = tagline.Substring(0, Math.Min(100, tagline.Length));

                this.ImportantTitle = $"{activity.DisplayName.ToUpper()} SAYS:";
                this.ImportantMessage = (tagline.Length == 0) ? "I just made a successful resupply..." : $"{tagline}";
            }
            else
            {
                if (!(this.ImportantTitle +"").EndsWith(" SAYS:"))
                {
                    this.ImportantTitle = $"{activity.DisplayName.ToUpper()}";
                    this.ImportantMessage = $"{activity.DisplayName.ToUpper()} has failed a resupply attempt...";
                }                
            }
            
            if (landingType != LandingActivityType.Success)
            {
                await Task.Delay(5000);

                this.ImportantTitle = "";
                this.ImportantMessage = "";
            }
        }

        public async void AddDroneAsync(ActivityInformation activity, Common.LandingActivityType activityType)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                double x = this._randmonizer.Next(-120, 120);
                double y = this._randmonizer.Next(-320, 220);

                Controls.DroneControl control = new Controls.DroneControl()
                {
                    Width = 80,
                    Height = 80,
                    RenderTransform = new CompositeTransform() { TranslateX = x, TranslateY = y },
                    BeaconFill = activity.ThemeBrush,
                    UserId = activity.Id,
                };

                this.MapGrid.Children.Add(control);

                int newValue = (this.ActiveDroneCount + 1) * 1;

                Windows.UI.Xaml.ElementSoundPlayer.Play(Windows.UI.Xaml.ElementSoundKind.Focus);

                this.ActiveDroneCount = -1;

                SetFinalDroneCount(activity, activityType, newValue);

            });
        }

        public void RemoveAllDrones()
        {
            try
            {
                for (int i = this.MapGrid.Children.Count - 1; i >= 0; i--)
                {
                    if (this.MapGrid.Children[i].GetType().Equals(typeof(Controls.DroneControl)))
                    {
                        this.MapGrid.Children.RemoveAt(i);
                    }
                }
            }
            catch { }
        }

        public async void CompleteDroneLandingAsync(ActivityInformation activity)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var droneControl = this.MapGrid.Children.OfType<Controls.DroneControl>().Where(w => w.UserId.Equals(activity.Id)).FirstOrDefault();
                var activityControl = this.ActivityGridView.GetChildrenOfType<Controls.ActivityControl>().Where(w => w.UserId.Equals(activity.Id)).FirstOrDefault();

                LandingActivityType finalLandingType = (activity.DescentRate > -5.0) ? Common.LandingActivityType.Success : Common.LandingActivityType.Crash;

                if (activity.Id.Equals("bd9ef412e1e3270ef7ff3823dc573928")) finalLandingType = LandingActivityType.Success;

                RemoveDroneControlAsync(activity, activityControl, droneControl, finalLandingType);
                
                int newValue = (this.ActiveDroneCount - 1) * 1;

                Windows.UI.Xaml.ElementSoundPlayer.Play(Windows.UI.Xaml.ElementSoundKind.Focus);
                
                this.ActiveDroneCount = -1;
                
                SetFinalDroneCount(activity, finalLandingType, newValue);

            });
        }

        private async void RemoveDroneControlAsync(ActivityInformation activity, Controls.ActivityControl activityControl, Controls.DroneControl droneControl, LandingActivityType landingType)
        {
            if (activityControl != null)
            {
                activityControl.DisplayCompletion(landingType);
            }

            if (droneControl != null)
            {
                if (landingType == LandingActivityType.Crash)
                {
                    droneControl.ExplodeDrone();
                }
                else
                {
                    droneControl.SetAsSuccessful();                    
                }

                UpdateImportantMessageAsync(activity, landingType);

                await Task.Delay(2000);

                if (landingType == LandingActivityType.Crash) this.MapGrid.Children.Remove(droneControl);
            }
        }

        private async void SetFinalDroneCount(ActivityInformation activity, Common.LandingActivityType activityType, int newValue)
        {
            await Task.Delay(200);
            this.ActiveDroneCount = newValue;

            await Task.Delay(500);

            switch (activityType)
            {
                case Common.LandingActivityType.Initialize:
                    break;
                case Common.LandingActivityType.Cancelled:
                    break;
                case Common.LandingActivityType.Crash:
                    this.CurrentCrashesCount++;
                    break;
                case Common.LandingActivityType.Success:
                    this.CurrentLandingsCount++;
                    break;
            }

            Windows.UI.Xaml.ElementSoundPlayer.Play(Windows.UI.Xaml.ElementSoundKind.Invoke);
                       
        }
    }
}
