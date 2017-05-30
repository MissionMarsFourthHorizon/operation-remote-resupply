using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
 
namespace DroneLander.MissionControl.Data
{
    public class Landing
    {
        string id;        
        string title;
        string team;
        string description;
        string imageUrl;
        DateTime activityDate;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [JsonProperty(PropertyName = "team")]
        public string Team
        {
            get { return team; }
            set { team = value; }
        }

        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
      
        [JsonProperty(PropertyName = "activityDate")]
        public DateTime ActivityDate
        {
            get { return activityDate; }
            set { activityDate = value; }
        }

        [Version]
        public string Version { get; set; }
    }
    
}
