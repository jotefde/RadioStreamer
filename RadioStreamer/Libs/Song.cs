using System;
using System.Collections.Generic;
using System.Text;

namespace RadioStreamer.Libs
{

    public class Song
    {
        public String Title { get; set; }
        public String Author { get; set; }
        public String AuthorUrl { get; set; }
        public Int16 Duration { get; set; }
        public String CoverUrl { get; set; }
        public String BigCoverUrl { get; set; }
        public Int16 Uptime { get; set; }
        public DateTime Timestamp { get; set; }
        public Song()
        {
        }
        public Song(String title, String author, Int16 dur, Int16 uptime, DateTime ts,
            String authorUrl = null, String coverUrl = null, String bcoverUrl = null)
        {
            Title = title;
            Author = author;
            AuthorUrl = authorUrl;
            Duration = dur;
            CoverUrl = coverUrl;
            BigCoverUrl = bcoverUrl;
            Uptime = uptime;
            Timestamp = ts;
        }

    }
}
