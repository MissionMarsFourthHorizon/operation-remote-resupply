using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DroneLander.MissionControl.Common
{
    public class DeleteMissionCommand : ICommand
    {
        private bool _isProcessing = false;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_isProcessing;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            App.ViewModel.DeleteMission();
        }

    }

    public class EndMissionCommand : ICommand
    {
        private bool _isProcessing = false;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_isProcessing;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            App.ViewModel.EndMission();
        }

    }

    public class RestartMissionCommand : ICommand
    {
        private bool _isProcessing = false;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_isProcessing;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            App.ViewModel.RestartMission();
        }
    }

    public class StartMissionCommand : ICommand
    {
        private bool _isProcessing = false;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_isProcessing;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            App.ViewModel.StartMission();
        }
    }

    public class SaveMissionSettingsCommand : ICommand
    {
        private bool _isProcessing = false;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_isProcessing;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            this._isProcessing = true;
            this.RaiseCanExecuteChanged();

            App.ViewModel.IsBusy = true;

            UpdateMissionSettings();
        }

        private async void UpdateMissionSettings()
        {
            if (!string.IsNullOrEmpty(App.ViewModel.MissionSettings.EventName))
            {
                var savedMissionSettings = Helpers.StorageHelper.GetMissionSettings();

                string missionEventName = App.ViewModel.MissionSettings.EventName.Trim().Replace(" ", "");

                if (savedMissionSettings == null || !missionEventName.Equals(savedMissionSettings.EventName))
                {
                    await Helpers.TelemetryHelper.EnsureEventTopicAsync(missionEventName);
                }

                App.ViewModel.MissionSettings.Status = MissionStatusType.Configured;
                App.ViewModel.MissionSettings.EventName = missionEventName;
                App.ViewModel.MissionSettings.Duration = App.ViewModel.SelectedMissionDuration;

                Helpers.StorageHelper.SaveMissionSettings(App.ViewModel.MissionSettings);
            }

            App.ViewModel.IsBusy = false;

            //App.ViewModel.CurrentSelectionPopup.IsOpen = false;

            this._isProcessing = false;
            this.RaiseCanExecuteChanged();
        }
    }

    public class TryClosePopupCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            App.ViewModel.CurrentSelectionPopup.IsOpen = false;
        }

    }

    public class ShowSettingsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            Helpers.FlyoutHelper.ShowSettingsPage();
        }

    }
}
