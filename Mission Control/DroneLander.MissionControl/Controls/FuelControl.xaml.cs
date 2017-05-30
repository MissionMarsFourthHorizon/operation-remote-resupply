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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DroneLander.MissionControl.Controls
{
    public sealed partial class FuelControl : UserControl
    {
        private bool _isOn = false;

        public SolidColorBrush IndicatorBrush = (SolidColorBrush)App.Current.Resources["AppActivityWhiteBrush"];

        public double FuelRemaining
        {
            get { return (double)base.GetValue(FuelRemainingProperty); }
            set { base.SetValue(FuelRemainingProperty, value); }
        }

        public static readonly DependencyProperty FuelRemainingProperty =
          DependencyProperty.Register("FuelRemaining", typeof(double), typeof(FuelControl), new PropertyMetadata(0, OnFuelRemainingChanged));

        private static void OnFuelRemainingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FuelControl;

            if (Convert.ToDouble(e.NewValue) <= 0.0 && !control._isOn)
            {
                control.ToggleStrobe(true);
            }
            else if ((double)e.NewValue > 0.0 && control._isOn)
            {
                control.ToggleStrobe(false);
            }
        }

        public FuelControl()
        {
            this.InitializeComponent();
        }

        public void ToggleStrobe(bool isOn)
        {
            this._isOn = isOn;

            this.icon.Foreground = (isOn) ? (SolidColorBrush)App.Current.Resources["AppActivityRedBrush"] : (SolidColorBrush)App.Current.Resources["AppActivityWhiteBrush"];

            if (isOn)
            {
                this.Strobe.Begin();
            }
            else
            {
                this.Strobe.Stop();
                this.Strobe.Seek(TimeSpan.FromTicks(0));
            }
                
        }
    }
}
