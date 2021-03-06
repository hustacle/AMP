﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AnyListen.Music.Playlist;
using AnyListen.Music.Track;

namespace AnyListen.AppCommunication.Commands
{
    public class Utilities
    {
        private const string timeSpanFormat = "hh:mm:ss";

        public static string TimeSpanToString(TimeSpan timeSpan)
        {
            return timeSpan.ToString(timeSpanFormat);
        }

        public static TimeSpan StringToTimeSpan(string str)
        {
            return TimeSpan.ParseExact(str, timeSpanFormat, CultureInfo.InvariantCulture);
        }

        public static PlayableBase GetTrackByAuthenticationCode(long authenticationCode, IList<NormalPlaylist> playlists)
        {
            foreach (var playlist in playlists)
            {
                var track = playlist.Tracks.FirstOrDefault(x => x.AuthenticationCode == authenticationCode);
                if (track != null) return track;
            }
            return null;
        }
    }
}
