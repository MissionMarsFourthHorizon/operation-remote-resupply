using DroneLander.MissionControl.Data;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

namespace DroneLander.MissionControl.Services
{
    public partial class NotificationManager
    {
        static NotificationManager defaultInstance = new NotificationManager();
        MobileServiceClient client;

        IMobileServiceTable<Landing> landingsTable;
       

        private NotificationManager()
        {
            this.client = new MobileServiceClient("https://dronelandermissioncontrol.azurewebsites.net");
            this.landingsTable = client.GetTable<Landing>();
        }

        public static NotificationManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public async Task SaveLandingAsync(Landing item)
        {
            try
            {
                if (item.Id == null)
                {
                    await landingsTable.InsertAsync(item);
                }
                else
                {
                    await landingsTable.UpdateAsync(item);
                }
            }
            catch (Exception ex)
            {

            }
        }

        
        
    }
}
