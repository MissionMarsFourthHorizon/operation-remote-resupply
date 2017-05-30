using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace DroneLander.MissionControl.Helpers
{
    public static class FlyoutHelper
    {
        public static void ShowSettingsPage()
        {
            Page child = new Flyouts.SettingsPage();

            child.Width = Window.Current.Bounds.Width;
            child.Height = Window.Current.Bounds.Height;

            App.ViewModel.CurrentSelectionPopup = new Popup()
            {
                IsLightDismissEnabled = true,
                Child = child,
                Width = Window.Current.Bounds.Width,
                Height = Window.Current.Bounds.Height,
            };

            App.ViewModel.CurrentSelectionPopup.IsOpen = true;
        }
    }
}
