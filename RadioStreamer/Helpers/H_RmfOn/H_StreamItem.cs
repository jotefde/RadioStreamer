using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RadioStreamer.Helpers.H_RmfOn
{
    [XmlRoot(ElementName = "item")]
    public class H_StreamItem
    {
        [XmlAttribute(AttributeName = "ads")]
        public string Ads { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}
