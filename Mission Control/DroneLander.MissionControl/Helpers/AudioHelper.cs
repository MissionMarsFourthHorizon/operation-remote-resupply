using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DroneLander.MissionControl.Helpers
{
    public class AudioHelper
    {   
        public AudioPlayer CurrentPlayer { get; set; }

        public async Task InitializePlayer()
        {
            this.CurrentPlayer = new AudioPlayer();

            var successSound = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Media/ALARM09.WAV"));
            var failureSound = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Media/EXPLOSION.WAV"));

            await this.CurrentPlayer.LoadFileAsync(successSound);
            await this.CurrentPlayer.LoadFileAsync(failureSound);

            return;
        }

        public void PlaySuccess()
        {
            this.CurrentPlayer.Play("ALARM09.WAV", 1.0);
        }

        public void PlayFailure()
        {
            this.CurrentPlayer.Play("EXPLOSION.WAV", 0.2);
        }
    }
}
