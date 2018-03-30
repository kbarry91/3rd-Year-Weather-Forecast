using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace WeatherForecast
{
    public enum SoundEfxEnum
    {
        CLICK,
        SWIPE,
        WRONG,
        NOTGOOD,
    }
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
            // sound sourced for free from https://www.zapsplat.com/?s=click
            effects.Add(SoundEfxEnum.CLICK, await LoadSoundFile("click.mp3"));
            effects.Add(SoundEfxEnum.SWIPE, await LoadSoundFile("swipe.mp3"));
            effects.Add(SoundEfxEnum.WRONG, await LoadSoundFile("wrong.mp3"));
             effects.Add(SoundEfxEnum.NOTGOOD, await LoadSoundFile("notgood.mp3"));
        }

        private async Task<MediaElement> LoadSoundFile(string v)
        {
            MediaElement snd = new MediaElement();

            snd.AutoPlay = false;
            StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await folder.GetFileAsync(v);
            var stream = await file.OpenAsync(FileAccessMode.Read);
            snd.SetSource(stream, file.ContentType);
            return snd;
        }

        public void Play(SoundEfxEnum efx)
        {
            var mediaElement = effects[efx];
            mediaElement.Play();
        }
    }
}
