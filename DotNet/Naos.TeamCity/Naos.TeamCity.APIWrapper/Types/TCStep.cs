namespace Naos.TeamCity.APIWrapper.Types
{
    using System.Xml.Serialization;

    [XmlRoot("step")]
    [XmlType("step")]
    public class TCStep
    {
        [XmlAttribute]
        public string id { get; set; }
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string type { get; set; }
        [XmlAttribute("href")]
        public string href { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public TCProperty[] properties { get; set; }
    }
}