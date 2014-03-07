namespace Naos.TeamCity.APIWrapper.Types
{
    using System.Xml.Serialization;

    [XmlRoot("trigger")]
    [XmlType("trigger")]
    public class TCTrigger
    {
        [XmlAttribute]
        public string id { get; set; }

        [XmlAttribute]
        public string type { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public TCProperty[] properties { get; set; }
    }
}