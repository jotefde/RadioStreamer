using System;
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
            foreach (Station station in service.GetStations())
            {
                //Console.WriteLine(station.Name + ": " + station.ToString() + " : " + station.parent.ToString());
            }
            Thread.Sleep(2000);
        }
    }
}
