using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Hurricane.Music.Track.WebApi.AnyListen
{
    public static class CommonHelper
    {
        public static bool Is45Install()
        {
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                return  ndpKey != null && Convert.ToInt32(ndpKey.GetValue("Release")) >= 378389;
            }
        }

        public static string GetDownloadUrl(SongResult song, int bitRate, int prefer, bool isFormat)
        {
            string link;
            switch (bitRate)
            {
                case 0:
                    switch (prefer)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(song.FlacUrl))
                            {
                                link = song.FlacUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.ApeUrl))
                            {
                                link = song.ApeUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.WavUrl))
                            {
                                link = song.WavUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.SqUrl))
                            {
                                link = song.SqUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.HqUrl))
                            {
                                link = song.HqUrl;
                            }
                            else
                            {
                                link = song.LqUrl;
                            }
                            break;
                        case 1:
                            if (!string.IsNullOrEmpty(song.ApeUrl))
                            {
                                link = song.ApeUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.FlacUrl))
                            {
                                link = song.FlacUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.WavUrl))
                            {
                                link = song.WavUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.SqUrl))
                            {
                                link = song.SqUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.HqUrl))
                            {
                                link = song.HqUrl;
                            }
                            else
                            {
                                link = song.LqUrl;
                            }
                            break;
                        default:
                            if (!string.IsNullOrEmpty(song.WavUrl))
                            {
                                link = song.WavUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.FlacUrl))
                            {
                                link = song.FlacUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.ApeUrl))
                            {
                                link = song.ApeUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.SqUrl))
                            {
                                link = song.SqUrl;
                            }
                            else if (!string.IsNullOrEmpty(song.HqUrl))
                            {
                                link = song.HqUrl;
                            }
                            else
                            {
                                link = song.LqUrl;
                            }
                            break;
                    }
                    break;
                case 1:
                    if (!string.IsNullOrEmpty(song.SqUrl))
                    {
                        link = song.SqUrl;
                    }
                    else if (!string.IsNullOrEmpty(song.HqUrl))
                    {
                        link = song.HqUrl;
                    }
                    else
                    {
                        link = song.LqUrl;
                    }
                    break;
                case 2:
                    if (!string.IsNullOrEmpty(song.HqUrl))
                    {
                        link = song.HqUrl;
                    }
                    else if (!string.IsNullOrEmpty(song.SqUrl))
                    {
                        link = song.SqUrl;
                    }
                    else
                    {
                        link = song.LqUrl;
                    }
                    break;
                default:
                    link = song.LqUrl;
                    break;
            }
            if (isFormat)
            {
                if (link.ToLower().Contains(".flac"))
                {
                    link = "flac";
                }
                else if (link.ToLower().Contains(".ape"))
                {
                    link = "ape";
                }
                else if (link.ToLower().Contains(".wav"))
                {
                    link = "ape";
                }
                else if (link.ToLower().Contains(".ogg"))
                {
                    link = "ogg";
                }
                else if (link.ToLower().Contains(".aac"))
                {
                    link = "acc";
                }
                else if (link.ToLower().Contains(".wma"))
                {
                    link = "wma";
                }
                else
                {
                    link = "mp3";
                }
            }
            return link;
        }

        public static string GetFormat(string url)
        {
            string link;
            if (url.ToLower().Contains(".flac"))
            {
                link = "flac";
            }
            else if (url.ToLower().Contains(".ape"))
            {
                link = "ape";
            }
            else if (url.ToLower().Contains(".wav"))
            {
                link = "ape";
            }
            else if (url.ToLower().Contains(".ogg"))
            {
                link = "ogg";
            }
            else if (url.ToLower().Contains(".aac"))
            {
                link = "acc";
            }
            else if (url.ToLower().Contains(".wma"))
            {
                link = "wma";
            }
            else
            {
                link = "mp3";
            }
            return "." + link;
        }

        public static string NumToTime(int num)
        {
            var mins = num / 60;
            var seds = num % 60;
            string time;
            if (mins.ToString(CultureInfo.InvariantCulture).Length == 1)
            {
                time = @"0" + mins;
            }
            else
            {
                time = mins.ToString(CultureInfo.InvariantCulture);
            }
            time += ":";
            if (seds.ToString(CultureInfo.InvariantCulture).Length == 1)
            {
                time += @"0" + seds;
            }
            else
            {
                time += seds.ToString(CultureInfo.InvariantCulture);
            }
            return time;
        }
        public static void AddLog(string str)
        {
            str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + str + "\r\n";
            var path = Path.Combine(Environment.CurrentDirectory, "Log");
            if (Directory.Exists(path))
            {
                try
                {
                    File.AppendAllText(Path.Combine(path, DateTime.Now.ToString("yyyy_MM_dd") + ".log"), str, Encoding.Default);
                }
                catch (Exception)
                {
                    //文件被占用
                }
            }
            else
            {
                Directory.CreateDirectory(path);
                var fs = File.Create(Path.Combine(path, DateTime.Now.ToString("yyyy_MM_dd") + ".log"));
                fs.Write(Encoding.Default.GetBytes(str), 0, Encoding.Default.GetBytes(str).Length);
                fs.Close();
            }
        }
    }
}