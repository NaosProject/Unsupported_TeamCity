namespace Naos.TeamCity.APIWrapper.Types
{
    using System.Xml.Serialization;

    [XmlRoot("buildType")]
    [XmlType(TypeName = "buildType")]
    public class TCBuildType
    {
        [XmlAttribute("id")]
        public string id { get; set; }

        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("href")]
        public string href { get; set; }

        [XmlArray("vcs-root-entries")]
        [XmlArrayItem("vcs-root-entry")]
        public TCVcsRootEntry[] vcsRootEntries { get; set; }

        [XmlElement("project")]
        public TCProject project { get; set; }

        [XmlAttribute("shared")]
        public bool shared { get; set; }

        [XmlArray("builds")]
        [XmlArrayItem("build")]
        public TCBuild[] builds { get; set; }

        [XmlArray("settings")]
        [XmlArrayItem("property")]
        public TCProperty[] settings { get; set; }

        [XmlArray("parameters")]
        [XmlArrayItem("property")]
        public TCProperty[] parameters { get; set; }

        [XmlArray("steps")]
        [XmlArrayItem("step")]
        public TCStep[] steps { get; set; }

        [XmlArray("features")]
        [XmlArrayItem("feature")]
        public TCFeature[] features { get; set; }

        [XmlArray("triggers")]
        [XmlArrayItem("trigger")]
        public TCTrigger[] triggers { get; set; }

        [XmlArray("snapshot-dependencies")]
        [XmlArrayItem("snapshot-dependency")]
        public TCProperty[] snapshotDependencies { get; set; }

        [XmlArray("artifact-dependencies")]
        [XmlArrayItem("artifact-dependency")]
        public TCProperty[] artifactDependencies { get; set; }

        [XmlArray("agent-requirements")]
        [XmlArrayItem("agent-requirement")]
        public TCProperty[] agentRequirements { get; set; }
    }
}