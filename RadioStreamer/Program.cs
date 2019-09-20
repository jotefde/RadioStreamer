using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RadioStreamer.Core.Services;
using RadioStreamer.Libs;
using RadioStreamer.Models;

namespace RadioStreamer
{
    class Program
    {
        static void Main(string[] args)
        {
            M_Service service = new Core.Services.RmfOn();
            Task<bool> prepared = service.Prepare();
            do
            {
            } while (!prepared.IsCompleted);
            /*List<Station> stations = service.GetStations();
            foreach (Station station in stations)
            {
                Console.WriteLine(station.Name);
            }*/
            //stations[0].GetPlaylist();
        }
    }
}
