using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RadioStreamer.Libs;

namespace RadioStreamer.Models
{
    public class Playlist : List<Song> { }
    public abstract class M_Service
    {
        protected List<Station> _stations = new List<Station>();
        public bool Prepared { get; set; } = false;
        public abstract Task<bool> Prepare();
        public virtual List<Station> GetStations()
        {
            return _stations;
        }
        public abstract Task<bool> SyncStationList();
        public abstract Task<Playlist> GetPlaylist(Int16 station_id);
    }
}
