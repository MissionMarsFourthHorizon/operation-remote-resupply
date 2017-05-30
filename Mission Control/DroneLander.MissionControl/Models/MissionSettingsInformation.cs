using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLander.MissionControl
{
    public class MissionSettingsInformation : Common.ObservableBase
    {
        private string _eventName;
        public string EventName
        {
            get { return this._eventName; }
            set
            {
                this.SetProperty(ref this._eventName, value);
                this.CanSave = (value + "").Length > 3 && this.Status == Common.MissionStatusType.Unconfigured;
            }
        }

        private double _duration;
        public double Duration
        {
            get { return this._duration; }
            set { this.SetProperty(ref this._duration, value); }
        }

        private TimeSpan? _startTime;
        public TimeSpan? StartTime
        {
            get { return this._startTime; }
            set { this.SetProperty(ref this._startTime, value); }
        }

        private bool _canSave;
        public bool CanSave
        {
            get { return this._canSave; }
            set { this.SetProperty(ref this._canSave, value); }
        }

        private Common.MissionStatusType _status;
        public Common.MissionStatusType Status
        {
            get { return this._status; }
            set { this.SetProperty(ref this._status, value); }
        }
    }
}
