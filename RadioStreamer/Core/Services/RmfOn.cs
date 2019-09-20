using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RadioStreamer.Models;
using RadioStreamer.Helpers.M_RmfOn;
using RadioStreamer.Libs;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace RadioStreamer.Core.Services
{
    public class RmfOn : M_Service
    {
        public RmfOn()
        {
            Prop.Add("STATIONS", @"https://www.rmfon.pl/xml/stations.txt");
            Prop.Add("PLAYLIST", @"https://www.rmfon.pl/stacje/playlista_{id}.json.txt");
            Prop.Add("STREAMS", @"https://www.rmfon.pl/stacje/flash_aac_{id}.xml.txt");
        }
                
        public async override Task<bool> Prepare()
        {
            _stations = new List<Station>();
            bool stationListSynchronized = await SyncStationList();
            _stations.ForEach(el => Console.WriteLine(el.CurrentSong.ToString()));
            return true;
        }
        public async override Task<bool> SyncStationList()
        {
            object stationListUrl = String.Empty;
            Prop.TryGetValue("STATIONS", out stationListUrl);
            String stationListXML = await App.GetAsync(stationListUrl.ToString());
            XmlSerializer serializer = new XmlSerializer(typeof(M_StationList));
            using (TextReader reader = new StringReader(stationListXML))
            {
                M_StationList list = (M_StationList)serializer.Deserialize(reader);
                foreach (M_Station stationEl in list.Stations)
                {
                    _stations.Add(
                        new Station(Convert.ToInt16(stationEl.Id), stationEl.Slug, stationEl.Name));
                    _stations.Last().Parent = this;
                    await _stations.Last().Sync();
                }
            }
            return true;
        }

        public override async Task<Playlist> GetPlaylist(Int16 station_id)
        {
            String playlistUrl = Prop.GetValueOrDefault("PLAYLIST").ToString()
                                     .Replace("{id}", Convert.ToString(station_id));
            String playlistJSON = await App.GetAsync(playlistUrl);
            //String playlistJSON = await App.GetAsync(@"http://localhost/test.json");
            Playlist playlist = new Playlist();
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(playlistJSON)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(M_Playlist));
                M_Playlist list = (M_Playlist)deserializer.ReadObject(ms);
                list.ForEach(
                    el => playlist.Add(
                        new Song(el.title, el.author, Convert.ToInt16(el.lenght),
                            Convert.ToInt16(el.uptime), new DateTime(el.timestamp),
                            el.authorUrl, el.coverUrl, el.coverBigUrl)
                ));
            }
            return playlist;
        }
    }
}
