using System;
using System.Collections.Generic;
using System.Text;

namespace RadioStreamer.Helpers.M_RmfOn
{
    public class M_Playlist : List<M_PlaylistElement> { }
    public class M_PlaylistElement
    {
        public int order { get; set; }
        public string author { get; set; }
        public string authorUrl { get; set; }
        public int bio { get; set; }
        public string title { get; set; }
        public string recordTitle { get; set; }
        public int trackId { get; set; }
        public string selector { get; set; }
        private string _lenght;
        public string lenght {
            get
            {
                return _lenght;
            }
            set
            {
                if (value.Length < 1) _lenght = "0";
                else _lenght = value;
            }
        }
        public int year { get; set; }
        public int cover { get; set; }
        public string start { get; set; }
        public int timestamp { get; set; }
        public int hasBiography { get; set; }
        public int hasLyrics { get; set; }
        public string iTunes { get; set; }
        public string coverUrl { get; set; }
        public string coverBigUrl { get; set; }
        public int votes { get; set; }
        public int points { get; set; }
        public int uptime { get; set; }
        public string source { get; set; }
    }
}
