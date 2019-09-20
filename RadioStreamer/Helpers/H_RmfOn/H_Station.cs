using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RadioStreamer.Helpers.H_RmfOn
{
    [XmlRoot(ElementName = "station")]
    public class H_Station
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "idname")]
        public string Idname { get; set; }
        [XmlAttribute(AttributeName = "slug")]
        public string Slug { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "link")]
        public string Link { get; set; }
        [XmlAttribute(AttributeName = "ramowka")]
        public string Ramowka { get; set; }
        [XmlAttribute(AttributeName = "air")]
        public string Air { get; set; }
        [XmlAttribute(AttributeName = "new")]
        public string New { get; set; }
        [XmlAttribute(AttributeName = "recommended")]
        public string Recommended { get; set; }
        [XmlAttribute(AttributeName = "similar")]
        public string Similar { get; set; }
    }
}
