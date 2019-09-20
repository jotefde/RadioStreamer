using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RadioStreamer.Helpers.H_RmfOn
{
    [XmlRoot(ElementName = "playlist")]
    public class H_StreamList
    {
        [XmlElement(ElementName = "item")]
        public List<H_StreamItem> Items { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "mountpoint")]
        public string Mountpoint { get; set; }
    }
}
