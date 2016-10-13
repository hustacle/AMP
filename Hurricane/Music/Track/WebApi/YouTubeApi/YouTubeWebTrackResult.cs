using System;
using System.Windows.Media;
using Hurricane.Music.Download;
using Hurricane.Music.Track.WebApi.YouTubeApi.DataClasses;
using Hurricane.Utilities;

namespace Hurricane.Music.Track.WebApi.YouTubeApi
{
    public class YouTubeWebTrackResult : WebTrackResultBase
    {
        public override ProviderName ProviderName => ProviderName.YouTube;

        public override PlayableBase ToPlayable()
        {
            var ytresult = (IVideoInfo) Result;
            var result = new YouTubeTrack
            {
                YouTubeId = YouTubeTrack.GetYouTubeIdFromLink(Url),
                TimeAdded = DateTime.Now,
                IsChecked = false
            };

            result.LoadInformation(ytresult);
            return result;
        }

        public override GeometryGroup ProviderVector => YouTubeTrack.GetProviderVector();

        public override bool CanDownload => true;

        public override string DownloadParameter => Url;

        public override string DownloadFilename => Title.ToEscapedFilename();

        public override DownloadMethod DownloadMethod => DownloadMethod.youtube_dl;
    }
}
