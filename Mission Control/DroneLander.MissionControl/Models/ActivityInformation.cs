using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace DroneLander.MissionControl
{
    public class ActivityInformation : Common.ObservableBase
    {
        private string _id;
        public string Id
        {
            get { return this._id; }
            set { this.SetProperty(ref this._id, value); }
        }

        private SolidColorBrush _themeBrush;
        public SolidColorBrush ThemeBrush
        {
            get { return this._themeBrush; }
            set { this.SetProperty(ref this._themeBrush, value); }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return this._displayName; }
            set { this.SetProperty(ref this._displayName, value); }
        }

        private string _tagline;
        public string Tagline
        {
            get { return this._tagline; }
            set { this.SetProperty(ref this._tagline, value); }
        }

        private double _altitude;
        public double Altitude
        {
            get { return this._altitude; }
            set { this.SetProperty(ref this._altitude, value); }
        }

        private double _descentRate;
        public double DescentRate
        {
            get { return this._descentRate; }
            set { this.SetProperty(ref this._descentRate, value); }
        }

        private double _fuelRemaining;
        public double FuelRemaining
        {
            get { return this._fuelRemaining; }
            set { this.SetProperty(ref this._fuelRemaining, value); }
        }

        private double _thrust;
        public double Thrust
        {
            get { return this._thrust; }
            set { this.SetProperty(ref this._thrust, value); }
        }

        private Windows.UI.Xaml.Visibility _isCompleted;
        public Windows.UI.Xaml.Visibility IsCompleted
        {
            get { return this._isCompleted; }
            set { this.SetProperty(ref this._isCompleted, value); }
        }

    }
}
