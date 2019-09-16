using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RadioStreamer.Models.M_RmfOn
{
    [XmlRoot(ElementName = "stations")]
    public class M_StationList
    {
        [XmlElement(ElementName = "station")]
        public List<M_Station> Stations { get; set; }
    }
}
