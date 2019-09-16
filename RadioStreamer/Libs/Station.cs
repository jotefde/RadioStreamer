using RadioStreamer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadioStreamer.Libs
{
    public class Station
    {
        private Buffor _buffor = new Buffor(Core.App.BUFFOR_LIMIT);
        private Song _currentSong;
        public M_Service Parent { get; set; }
        public Int16 ID { get;set; }
        public String Slug { get; set; }
        public String Name { get; set; }
        public Int16 HostsCount { get; set; }

        public Station(Int16 _ID, String _Slug, String _Name)
        {
            this.ID = _ID;
            this.Slug = _Slug;
            this.Name = _Name;
            this._currentSong = new Song();
        }

        public void Sync()
        {

        }

    }
}
