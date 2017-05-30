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
    public sealed partial class ActivityControl : UserControl
    {
        public string UserId
        {
            get { return base.GetValue(UserIdProperty) as string; }
            set { base.SetValue(UserIdProperty, value); }
        }

        public static readonly DependencyProperty UserIdProperty =
          DependencyProperty.Register("UserId", typeof(string), typeof(ActivityControl), null);

        public ActivityControl()
        {
            this.InitializeComponent();
            this.ShowSuccess.Completed += OnCompletionCompleted;
            this.ShowFailure.Completed += OnCompletionCompleted;
        }

        private void OnCompletionCompleted(object sender, object e)
        {
            App.ViewModel.RemoveActivityItem(this.DataContext as ActivityInformation);
        }

        public void DisplayCompletion(Common.LandingActivityType landingType)
        {
            if (landingType == Common.LandingActivityType.Crash)
            {
                this.ShowFailure.Begin();
            }
            else if (landingType == Common.LandingActivityType.Success)
            {
                this.ShowSuccess.Begin();
            }
        }
    }
}
