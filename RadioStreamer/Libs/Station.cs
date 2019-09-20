﻿using RadioStreamer.Core;
using RadioStreamer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RadioStreamer.Libs
{
    public class Station
    {
        private Buffor _buffor = new Buffor(Core.App.BUFFOR_LIMIT);
        public M_Service Parent { get; set; }
        public Int16 ID { get;set; }
        public String Slug { get; set; }
        public String Name { get; set; }
        public Int16 HostsCount { get; set; }
        public Song CurrentSong { get; set; }

        public Station(Int16 _ID, String _Slug, String _Name)
        {
            this.ID = _ID;
            this.Slug = _Slug;
            this.Name = _Name;
        }

        public async Task Sync()
        {
            Playlist songs = await Parent.GetPlaylist(ID);
            CurrentSong = songs.FirstOrDefault(song => song.Uptime > 0);
        }
    }
}
