using System;
using System.Net;
using System.Threading.Tasks;
using Hurricane.Settings;

namespace Hurricane.Music.Download
{
    class SoundCloudDownloader
    {
        public static async Task DownloadSoundCloudTrack(string soundCloudId, string fileName, Action<double> progressChangedAction)
        {
            using (var client = new WebClient { Proxy = null })
            {
                client.DownloadProgressChanged += (s, e) => progressChangedAction.Invoke(e.ProgressPercentage);
                await
                    client.DownloadFileTaskAsync(
                        $"https://api.soundcloud.com/tracks/{soundCloudId}/download?client_id={SensitiveInformation.SoundCloudKey}", fileName);
            }
        }
    }
}