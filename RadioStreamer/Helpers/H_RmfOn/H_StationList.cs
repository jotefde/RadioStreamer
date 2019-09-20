/* 
  Licensed under the Apache License, Version 2.0

  http://www.apache.org/licenses/LICENSE-2.0
  */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace RadioStreamer.Helpers.H_RmfOn
{
    [XmlRoot(ElementName = "stations")]
    public class H_StationList
    {
        [XmlElement(ElementName = "station")]
        public List<H_Station> Stations { get; set; }
    }

}