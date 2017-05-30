using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLander.MissionControl.Common
{
    public enum LandingActivityType { Unknown, Initialize, Cancelled, Crash, Success }

    public enum MissionStatusType { Unconfigured, Configured, InProgress, Complete }
    public enum MissionActionType { CanSave, CanDelete, CanRestart, CanStart, CanEnd }
}
