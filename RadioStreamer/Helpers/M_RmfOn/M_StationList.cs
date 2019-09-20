/* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace RadioStreamer.Helpers.M_RmfOn
{
    [XmlRoot(ElementName = "stations")]
    public class M_StationList
    {
        [XmlElement(ElementName = "station")]
        public List<M_Station> Stations { get; set; }
    }

}