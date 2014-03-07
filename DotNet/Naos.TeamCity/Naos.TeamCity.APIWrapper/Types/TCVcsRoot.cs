namespace Naos.TeamCity.APIWrapper.Types
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("vcs-root")]
    [XmlType(TypeName = "vcs-root")]
    public class TCVcsRoot
    {
        private string _href;

        [XmlAttribute("id")]
        public string id { get; set; }

        [XmlAttribute("vcsName")]
        public string vcsName { get; set; }

        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("href")]
        public string href
        {
            get
            {
                if (String.IsNullOrEmpty(this._href))
                {
                    this.href = "/app/rest/vcs-roots/id:" + this.id;
                }

                return this._href;
            }

            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this._href = value;
                }
            }
        }

        [XmlAttribute("status")]
        public string status { get; set; }

        [XmlAttribute("lastChecked")]
        public string lastChecked { get; set; }

        [XmlIgnore]
        public DateTime lastCheckedDateTime
        {
            get
            {
                return DateTime.Parse(this.lastChecked);
            }
        }

        [XmlAttribute("shared")]
        public bool shared { get; set; }

        [XmlElement("project")]
        public TCProject project { get; set; }

        [XmlArray()]
        [XmlArrayItem("property")]
        public TCProperty[] properties { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}