using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLander.MissionControl
{
    public class CompletionCountInformation : Common.ObservableBase
    {
        private int _successfulLandings;
        public int SuccessfulLandings
        {
            get { return this._successfulLandings; }
            set { this.SetProperty(ref this._successfulLandings, value); }
        }

        private int _failures;
        public int Failures
        {
            get { return this._failures; }
            set { this.SetProperty(ref this._failures, value); }
        }
    }
}
