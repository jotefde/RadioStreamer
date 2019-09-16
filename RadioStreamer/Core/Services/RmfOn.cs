using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RadioStreamer.Models;
using RadioStreamer.Models.M_RmfOn;
using RadioStreamer.Libs;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;

namespace RadioStreamer.Core.Services
{
    public class RmfOn : M_Service
    {
        private String _stationsUrl = @"https://www.rmfon.pl/xml/stations.txt";
        private String _playlistUrl = @"https://www.rmfon.pl/stacje/playlista_{id}.json.txt";
        private String _streamListUrl = @"https://www.rmfon.pl/stacje/flash_aac_{id}.xml.txt";
        
        public async override Task<bool> Prepare()
        {
            bool stationListSynchronized = await SyncStationList();
            Playlist playlist = await GetPlaylist(5);
            Console.WriteLine(playlist.GetType() + ", " + playlist.ToString());
            return true;
        }
        public async override Task<bool> SyncStationList()
        {
            String stationListXML = await App.GetAsync(_stationsUrl);
            XmlSerializer serializer = new XmlSerializer(typeof(M_StationList));
            using (TextReader reader = new StringReader(stationListXML))
            {
                M_StationList list = (M_StationList)serializer.Deserialize(reader);
                foreach (M_Station stationEl in list.Stations)
                {
                    _stations.Add(
                        new Station(Int16.Parse(stationEl.Id), stationEl.Slug, stationEl.Name));
                    _stations.Last().Parent = this;
                }
            }
            return true;
        }
        public async override Task<Playlist> GetPlaylist(Int16 station_id)
        {
            Playlist playlist = new Playlist();
            String playlistUrl = _playlistUrl.Replace("{id}", Convert.ToString(station_id));
            String playlistJSON = await App.GetAsync(playlistUrl);
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(playlistJSON)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(List<M_PlaylistElement>));
                List<M_PlaylistElement> list = (List<M_PlaylistElement>)deserializer.ReadObject(ms);
                foreach(M_PlaylistElement el in list)
                {
                    playlist.Add(
                        new Song(el.title, el.author, Convert.ToInt16(el.lenght),
                            Convert.ToInt16(el.uptime), new DateTime(el.timestamp),
                            el.authorUrl, el.coverUrl, el.coverBigUrl));
                }
            }
            foreach(Song song in playlist)
            {
                Console.WriteLine(song.GetType());
            }
            return playlist;
        }
    }
}
