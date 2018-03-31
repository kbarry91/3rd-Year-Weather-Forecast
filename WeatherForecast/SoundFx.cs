// Author : G00339811
// Module : Mobile Application Developement 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace WeatherForecast
{
    public enum SoundEfxEnum
    {
        CLICK,
        SWIPE,
        WRONG
    }
    /*
     * SoundFx is a utility sound class to load and play various audio files.
     */

    public class SoundFx
    {
        private Dictionary<SoundEfxEnum, MediaElement> effects;

        public SoundFx()
        {
            effects = new Dictionary<SoundEfxEnum, MediaElement>();
            LoadEfx();
        }

        private async void LoadEfx()
        {
            // Sound sourced for free from https://www.zapsplat.com/?s=click
            effects.Add(SoundEfxEnum.CLICK, await LoadSoundFile("click.mp3"));
            effects.Add(SoundEfxEnum.SWIPE, await LoadSoundFile("swipe.mp3"));
            effects.Add(SoundEfxEnum.WRONG, await LoadSoundFile("wrong.mp3"));
        }
        /*
         * Load the sound files from Assets and return a media element.
        */
        private async Task<MediaElement> LoadSoundFile(string v)
        {
            MediaElement snd = new MediaElement
            {
                AutoPlay = false
            };
            
            StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await folder.GetFileAsync(v);
            var stream = await file.OpenAsync(FileAccessMode.Read);
            snd.SetSource(stream, file.ContentType);
            return snd;
        }

        public void Play(SoundEfxEnum efx)
        {
            try
            {
                var mediaElement = effects[efx];
                mediaElement.Play();
            }
            catch(KeyNotFoundException e)
            {
                Debug.WriteLine("DEBUG : fx not found:" + e);
            }
          
        }
    }
}
