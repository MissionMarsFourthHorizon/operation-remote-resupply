using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System.Threading.Tasks;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DroneLander.MissionControl.Controls
{
    public sealed partial class DroneControl : UserControl
    {
        private bool _isComplete { get; set; }

        public string UserId
        {
            get { return base.GetValue(UserIdProperty) as string; }
            set { base.SetValue(UserIdProperty, value); }
        }

        public static readonly DependencyProperty UserIdProperty =
          DependencyProperty.Register("UserId", typeof(string), typeof(DroneControl), null);

        public SolidColorBrush BeaconFill
        {
            get { return base.GetValue(BeaconFillProperty) as SolidColorBrush; }
            set { base.SetValue(BeaconFillProperty, value); }
        }

        public static readonly DependencyProperty BeaconFillProperty =
          DependencyProperty.Register("BeaconFill", typeof(SolidColorBrush), typeof(DroneControl), null);
        
        public DroneControl()
        {
            this.InitializeComponent();
            this.Loaded += DroneControl_Loaded;
            App.ViewModel.DroneControl = this;
        }

        private void DroneControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.beacon.Fill = this.BeaconFill;

            StartPulse();
        }

        private async void StartPulse()
        {
            while (!_isComplete)
            {
                await beacon.Fade(value: 1.0f, duration: 150, delay: 500).StartAsync();
                await Task.Delay(2000);
                await beacon.Fade(value: 0.1f, duration: 500, delay: 500).StartAsync();
            }
        }

        public void ExplodeDrone()
        {
            this.beacon.Visibility = Visibility.Collapsed;
            App.ViewModel.PlayFailure();
            this.explosionGif.Play();
        }

        public async void SetAsSuccessful()
        {
            this._isComplete = true;
            this.UserId = "COMPLETE";

            beacon.Fill = (SolidColorBrush)App.Current.Resources["AppMainRedBrush"];
            beacon.Stroke = (SolidColorBrush)App.Current.Resources["AppMainDarkBrush"];

            await Task.Delay(3000);

            await beacon.Fade(value: 1.0f, duration: 150, delay: 500).StartAsync();
            await beacon.Scale(scaleX: 2.0f, scaleY: 2.0f, centerX: 9.0f, centerY: 9.0f, duration: 150, delay: 500).StartAsync();

            App.ViewModel.PlaySuccess();

            await star.Fade(value: 1.0f, duration: 150, delay: 500).StartAsync();
        }
    }
}
