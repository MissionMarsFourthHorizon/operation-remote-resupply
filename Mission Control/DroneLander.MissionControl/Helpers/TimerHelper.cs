using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLander.MissionControl.Helpers
{
    public class TimerHelper
    {
        IDisposable MissionTimer = null;
        IDisposable CurrentTimer = null;
 
        public void StartMissionTimer()
        {
            MissionTimer = Observable
             .Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
             .Subscribe(q =>
             {
                 UpdateTimer();
             });
        }

        public void StartClocks()
        {
            CurrentTimer = Observable
             .Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
             .Subscribe(q =>
             {
                 UpdateClocks();
             });
        }

        private async void UpdateClocks()
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                App.ViewModel.CurrentEarthTime = DateTime.UtcNow;
                App.ViewModel.CurrentMartianTime = DateTime.UtcNow.ToMartianTime();
            });

        }

        private async void UpdateTimer()
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {    
                App.ViewModel.CurrentMissionTimer = DateTime.Now.TimeOfDay;
            });

        }
    }
    
}
