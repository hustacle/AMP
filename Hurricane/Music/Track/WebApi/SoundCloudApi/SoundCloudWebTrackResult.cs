using System;
using System.Windows.Media;
using Hurricane.Music.Download;
using Hurricane.Utilities;

namespace Hurricane.Music.Track.WebApi.SoundCloudApi
{
    class SoundCloudWebTrackResult : WebTrackResultBase
    {
        public override ProviderName ProviderName => ProviderName.SoundCloud;

        public override PlayableBase ToPlayable()
        {
            var result = (ApiResult) Result;
            var newtrack = new SoundCloudTrack
            {
                Url = Url,
                TimeAdded = DateTime.Now,
                IsChecked = false
            };

            newtrack.LoadInformation(result);
            return newtrack;
        }

        public override GeometryGroup ProviderVector => SoundCloudTrack.GetProviderVector();

        public override bool CanDownload => ((ApiResult)Result).downloadable && !string.IsNullOrEmpty(((ApiResult)Result).download_url);

        public override string DownloadParameter => ((ApiResult)Result).id.ToString();

        public override string DownloadFilename => Title.ToEscapedFilename();

        public override DownloadMethod DownloadMethod => DownloadMethod.SoundCloud;
    }
}
