namespace Naos.TeamCity.APIWrapper.Types
{
    using System.Xml.Serialization;

    [XmlRoot("vcs-root-entry")]
    [XmlType(TypeName = "vcs-root-entry")]
    public class TCVcsRootEntry
    {
        [XmlAttribute]
        public string id { get; set; }

        [XmlElement("vcs-root")]
        public TCVcsRootForVcsRootEntry vcsRoot { get; set; }

        [XmlArray("checkout-rules")]
        [XmlArrayItem("checkout-rules")]
        public TCProperty[] checkoutRules { get; set; }
    }
}