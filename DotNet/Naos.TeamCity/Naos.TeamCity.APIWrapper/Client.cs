namespace Naos.TeamCity.APIWrapper
{
    using System;
    using System.Web;

    using Naos.TeamCity.APIWrapper.Types;

    public class Client
    {
        private readonly Caller _caller;

        public Client(string hostName, string username, string password)
        {
            var creds = new Credentials() { HostName = hostName, UserName = username, Password = password };
            this._caller = new Caller(creds);
        }

        public TCProject CreateProject(string name)
        {
            return this._caller.Post<string, TCProject>("/app/rest/projects", name);
        }

        public TCProject[] GetAllProjects()
        {
            return this._caller.Get<TCProject[]>("/app/rest/projects");
        }

        public TCProject GetProjectByName(string projectLocatorName)
        {
            var url = String.Format("/app/rest/projects/name:{0}", projectLocatorName);
            return this._caller.Get<TCProject>(url);
        }

        public TCProject GetProjectById(string projectLocatorId)
        {
            var url = String.Format("/app/rest/projects/id:{0}", projectLocatorId);
            return this._caller.Get<TCProject>(url);
        }

        public TCVcsRoot[] GetAllVcsRoots()
        {
            return this._caller.Get<TCVcsRoot[]>("/app/rest/vcs-roots");
        }

        public TCVcsRoot GetVcsRootById(string vcsRootId)
        {
            var url = string.Format("/app/rest/vcs-roots/id:{0}", vcsRootId);
            return this._caller.Get<TCVcsRoot>(url);
        }

        public TCVcsRoot CreateVcsRoot(TCVcsRoot newVcsRootComplete)
        {
            return this._caller.Post<TCVcsRoot, TCVcsRoot>("/app/rest/vcs-roots", newVcsRootComplete);
        }

        public TCBuildType CreateEmptyBuild(string projectId, string buildName)
        {
            return this._caller.Post<string, TCBuildType>("/app/rest/projects/id:" + projectId + "/buildTypes", buildName);
        }

        public TCBuildType[] GetAllBuildConfigs()
        {
            return this._caller.Get<TCBuildType[]>("/app/rest/buildTypes");
        }

        public TCBuildType[] GetAllBuildConfigsInProject(string projectId)
        {
            var url = string.Format("/app/rest/projects/id:{0}/buildTypes", projectId);
            return this._caller.Get<TCBuildType[]>(url);
        }

        public TCBuildType GetBuildConfigByConfigurationName(string buildConfigName)
        {
            var encodedName = HttpUtility.UrlEncode(buildConfigName);
            var url = string.Format("/app/rest/buildTypes/name:{0}", encodedName);
            return this._caller.Get<TCBuildType>(url);
        }

        public TCBuildType GetBuildConfigByConfigurationId(string buildConfigId)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}", buildConfigId);
            return this._caller.Get<TCBuildType>(url);
        }

        public TCBuildType GetBuildConfigByProjectNameAndConfigurationName(string projectName, string buildConfigName)
        {
            return this._caller.Get<TCBuildType>(string.Format("/app/rest/projects/name:{0}/buildTypes/name:{1}", projectName, buildConfigName));
        }

        public TCBuildType GetBuildConfigByProjectNameAndConfigurationId(string projectName, string buildConfigId)
        {
            return this._caller.Get<TCBuildType>(string.Format("/app/rest/projects/name:{0}/buildTypes/id:{1}", projectName, buildConfigId));
        }

        public TCBuildType GetBuildConfigByProjectIdAndConfigurationName(string projectId, string buildConfigName)
        {
            return this._caller.Get<TCBuildType>(string.Format("/app/rest/projects/id:{0}/buildTypes/name:{1}", projectId, buildConfigName));
        }

        public TCBuildType GetBuildConfigByProjectIdAndConfigurationId(string projectId, string buildConfigId)
        {
            return this._caller.Get<TCBuildType>(string.Format("/app/rest/projects/id:{0}/buildTypes/id:{1}", projectId, buildConfigId));
        }

        public TCBuildType[] GetBuildConfigsByProjectId(string projectId)
        {
            var url = string.Format("/app/rest/projects/id:{0}/buildTypes", projectId);
            return this._caller.Get<TCBuildType[]>(url);
        }

        public TCBuildType[] GetBuildConfigsByProjectName(string projectName)
        {
            var url = string.Format("/app/rest/projects/name:{0}/buildTypes", projectName);
            return this._caller.Get<TCBuildType[]>(url);
        }

        public TCVcsRootEntry[] GetVcsRootEntriesForBuildConfigByBuildConfigId(string buildConfigId)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/vcs-root-entries/", buildConfigId);
            return this._caller.Get<TCVcsRootEntry[]>(url);
        }

        public TCVcsRootEntry CreateVcsRootEntryOnBuildConfig(string buildConfigId, TCVcsRootEntry vcsRootEntry)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/vcs-root-entries/", buildConfigId);
            return this._caller.Post<TCVcsRootEntry, TCVcsRootEntry>(url, vcsRootEntry);
        }

        public TCTrigger[] GetTriggersForBuildConfigByBuildConfigId(string buildConfigId)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/triggers/", buildConfigId);
            return this._caller.Get<TCTrigger[]>(url);
        }

        public TCTrigger CreateTriggerOnBuildConfig(string buildConfigId, TCTrigger trigger)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/triggers/", buildConfigId);
            return this._caller.Post<TCTrigger, TCTrigger>(url, trigger);
        }

        public TCStep[] GetStepsForBuildConfigByBuildConfigId(string buildConfigId)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/steps/", buildConfigId);
            return this._caller.Get<TCStep[]>(url);
        }

        public TCStep CreateStepOnBuildConfig(string buildConfigId, TCStep step)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/steps/", buildConfigId);
            return this._caller.Post<TCStep, TCStep>(url, step);
        }

        public TCProperty[] GetSettingsForBuildConfigByBuildConfigId(string buildConfigId)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/settings/", buildConfigId);
            return this._caller.Get<TCProperty[]>(url);
        }

        public string SetSettingForBuildConfigByBuildConfigId(string buildConfigId, TCProperty setting)
        {
            var encodedName = HttpUtility.UrlEncode(setting.name);
            var url = string.Format("/app/rest/buildTypes/id:{0}/settings/{1}", buildConfigId, encodedName);
            return this._caller.Put<string>(url, setting.value, Caller.CONTENT_TEXT);
        }

        public string GetSettingForBuildConfigByBuildConfigIdAndSettingName(string buildConfigId, string settingName)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/settings/{1}", buildConfigId, settingName);
            return this._caller.Get<string>(url, Caller.CONTENT_TEXT);
        }

        public void DeleteBuildStep(string buildConfigId, TCStep step)
        {
            var url = string.Format("/app/rest/buildTypes/id:{0}/steps/{1}", buildConfigId, step.id);
            try
            {
                this._caller.Delete(url, Caller.CONTENT_TEXT);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Error executing Delete: NoContent"))
                {
                    // swallow b/c this is just what gets returned...
                }
                else
                {
                    throw;
                }
            }
        }

        public void TriggerBuild(string buildConfigId)
        {
            var url = string.Format("/action.html?add2Queue={0}", buildConfigId);
            this._caller.GetNoReturn(url);
        }
    }
}