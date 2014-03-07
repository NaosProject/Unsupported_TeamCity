namespace Naos.TeamCity.APIWrapper.Types
{
    using System.Xml.Serialization;

    [XmlRoot("project")]
    [XmlType("project")]
    public class TCProject
    {
        [XmlAttribute]
        public string id { get; set; }
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public string href { get; set; }
    }
}