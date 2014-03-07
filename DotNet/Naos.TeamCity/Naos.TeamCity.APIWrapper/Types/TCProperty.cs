namespace Naos.TeamCity.APIWrapper.Types
{
    using System.Xml.Serialization;

    [XmlRoot("property")]
    [XmlType("property")]
    public class TCProperty
    {
        [XmlAttribute()]
        public string name { get; set; }

        [XmlAttribute()]
        public string value { get; set; }
    }
}