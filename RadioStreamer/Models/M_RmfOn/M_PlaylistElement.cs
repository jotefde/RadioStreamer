using System;
using System.Collections.Generic;
using System.Text;

namespace RadioStreamer.Models.M_RmfOn
{
    public class M_PlaylistElement
    {
        public int order { get; set; }
        public string author { get; set; }
        public string authorUrl { get; set; }
        public string title { get; set; }
        public string recordTitle { get; set; }
        public string lenght { get; set; }
        public string start { get; set; }
        public int timestamp { get; set; }
        public string coverUrl { get; set; }
        public string coverBigUrl { get; set; }
        public int uptime { get; set; }
    }
}
