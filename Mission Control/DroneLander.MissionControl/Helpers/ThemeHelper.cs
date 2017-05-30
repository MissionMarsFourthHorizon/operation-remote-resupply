using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace DroneLander.MissionControl.Helpers
{
    public static class ThemeHelper
    {
        public static void UpdateTitleBar()
        {
            Color accentColor = (Color)App.Current.Resources["AppMainDarkColor"];

            var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = accentColor;
            titleBar.ForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = accentColor;
            titleBar.ButtonForegroundColor = Colors.White;

        }

        public static void UpdateTitleBar(Color accentColor, Color secondaryColor)
        {
            var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = secondaryColor;
            titleBar.ForegroundColor = secondaryColor;
            titleBar.ButtonBackgroundColor = secondaryColor;
            titleBar.ButtonForegroundColor = Colors.White;
        }

        public static List<Color> GetAvailableColors()
        {
            List<Color> colors = new List<Color>();

            foreach (var c in typeof(Colors).GetRuntimeProperties())
            {
                var color = (Color)c.GetValue(null);

                int brightness = (int)(color.R * 0.30 + color.G * 0.59 + color.B * 0.11);

                if (brightness > 140 && brightness < 220) colors.Add(color);
            }

            return colors;
        }

        internal static void UpdateSystemAssets()
        {
            Color accentColor = Color.FromArgb(255, 217, 0, 0);
            
            App.Current.Resources["SystemAccentColor"] = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlBackgroundAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlDisabledAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlForegroundAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAltAccentBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAltListAccentHighBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAltListAccentLowBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightAltListAccentMediumBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightListAccentHighBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightListAccentLowBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHighlightListAccentMediumBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["SystemControlHyperlinkTextBrush"]).Color = accentColor;

            ((SolidColorBrush)App.Current.Resources["ContentDialogBorderThemeBrush"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["JumpListDefaultEnabledBackground"]).Color = accentColor;
            ((SolidColorBrush)App.Current.Resources["ContentDialogBorderThemeBrush"]).Color = accentColor;


        }
    }
}
