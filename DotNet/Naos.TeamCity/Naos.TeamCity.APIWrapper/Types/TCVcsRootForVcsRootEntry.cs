namespace Naos.TeamCity.APIWrapper.Types
{
    using System.Xml.Serialization;

    [XmlRoot("vcs-root")]
    [XmlType(TypeName = "vcs-root")]
    public class TCVcsRootForVcsRootEntry
    {
        [XmlAttribute("id")]
        public string id { get; set; }

        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("href")]
        public string href { get; set; }

        public override string ToString()
        {
            return this.name;
        }

        public static TCVcsRootForVcsRootEntry Create(TCVcsRoot vcsRoot)
        {
            return new TCVcsRootForVcsRootEntry()
                {
                    id = vcsRoot.id,
                    name = vcsRoot.name,
                    href = vcsRoot.href,
                };
        }
    }
}