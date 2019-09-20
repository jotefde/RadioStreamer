using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RadioStreamer.Models;
using RadioStreamer.Helpers.H_RmfOn;
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
        private const String STATIONS_URL = @"https://www.rmfon.pl/xml/stations.txt";
        private const String PLAYLIST_URL = @"https://www.rmfon.pl/stacje/playlista_{id}.json.txt";
        private const String STREAMS_URL = @"https://www.rmfon.pl/stacje/flash_aac_{id}.xml.txt";
        public RmfOn()
        {
            
        }
                
        public async override Task<bool> Prepare()
        {
            _stations = new List<Station>();
            bool stationListSynchronized = await SyncStationList();
            foreach(Station el in _stations)
            {
                await el.Sync();
                Console.WriteLine(el._streamUrl);
            }
            return true;
        }
        public async override Task<bool> SyncStationList()
        {
            String stationListXML = await App.GetAsync(STATIONS_URL);
            XmlSerializer serializer = new XmlSerializer(typeof(H_StationList));
            using (TextReader reader = new StringReader(stationListXML))
            {
                H_StationList list = (H_StationList)serializer.Deserialize(reader);
                list.Stations.ForEach(el => {
                    _stations.Add(
                        new Station(Convert.ToInt16(el.Id), el.Slug, el.Name)
                        );
                    _stations.Last().Parent = this;
                });
                
            }
            return true;
        }

        public override async Task<Playlist> GetPlaylist(Int16 station_id)
        {
            String playlistUrl = PLAYLIST_URL.Replace("{id}",
                                                      Convert.ToString(station_id));
            String playlistJSON = await App.GetAsync(playlistUrl);
            //String playlistJSON = await App.GetAsync(@"http://localhost/test.json");
            Playlist results = new Playlist();
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(playlistJSON)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(H_Playlist));
                H_Playlist list = (H_Playlist)deserializer.ReadObject(ms);
                list.ForEach(
                    el => results.Add(
                        new Song(el.title, el.author, Convert.ToInt16(el.lenght),
                            Convert.ToInt16(el.uptime), new DateTime(el.timestamp),
                            el.authorUrl, el.coverUrl, el.coverBigUrl)
                ));
            }
            return results;
        }

        public async override Task<String> GetStreamUrl(Int16 station_id)
        {
            List<String> results = new List<String>();
            String streamListUrl = STREAMS_URL.Replace("{id}",
                                                      Convert.ToString(station_id));
            String streamListXML = await App.GetAsync(streamListUrl);
            TrimToDeafultXMLStreamList(ref streamListXML);
            XmlSerializer serializer = new XmlSerializer(typeof(H_StreamList));
            using (TextReader reader = new StringReader(streamListXML))
            {
                H_StreamList list = (H_StreamList)serializer.Deserialize(reader);
                return list.Items[0].Text;
            }
        }

        private void TrimToDeafultXMLStreamList(ref String xml)
        {
            int startIndex = xml.IndexOf("<playlist");
            int endIndex = xml.IndexOf("</playlist>") + "</playlist>".Length;
            int length = endIndex - startIndex;
            String data = xml.Substring(startIndex, length);
            xml = "<?xml version=\"1.0\" encoding=\"utf - 8\"?>" + data;
        }
    }
}
