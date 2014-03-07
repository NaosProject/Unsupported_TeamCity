namespace Naos.TeamCity.APIWrapper
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    internal class Caller
    {
        public const string CONTENT_TEXT = "text/plain";
        public const string CONTENT_XML = "application/xml";

        private readonly Credentials _configuration;

        public Caller(Credentials configuration)
        {
            this._configuration = configuration;
        }

        public void GetNoReturn(string urlPart, string contentType = CONTENT_XML, string acceptType = CONTENT_XML)
        {
            if (this.CheckForUserNameAndPassword())
                throw new ArgumentException("If you are not acting as a guest you must supply userName and password");

            if (string.IsNullOrEmpty(urlPart))
                throw new ArgumentException("Url must be specfied");

            var url = this.CreateUrl(urlPart);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = acceptType;
            req.ContentType = contentType;
            req.Timeout = 20000;
            this.AddBasicAuthHeader(req);

            req.PreAuthenticate = false;
            req.UseDefaultCredentials = false;

            using (var resp = req.GetResponse() as HttpWebResponse)
            {
                if (resp == null)
                {
                    throw new Exception("Error executing GetNoReturn - no code");
                }
                else if (resp.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Error executing GetNoReturn: " + resp.StatusCode);
                }
            }
        }

        public T Get<T>(string urlPart, string contentType = CONTENT_XML, string acceptType = CONTENT_XML)
        {
            if (this.CheckForUserNameAndPassword())
                throw new ArgumentException("If you are not acting as a guest you must supply userName and password");

            if (string.IsNullOrEmpty(urlPart))
                throw new ArgumentException("Url must be specfied");

            var url = this.CreateUrl(urlPart);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = acceptType;
            req.ContentType = contentType;
            req.Timeout = 20000;
            this.AddBasicAuthHeader(req);

            req.PreAuthenticate = false;
            req.UseDefaultCredentials = false;
            using (var resp = req.GetResponse())
            {
                using (var reader = new StreamReader(resp.GetResponseStream()))
                {
                    var raw = reader.ReadToEnd();
                    return Serializer.Deserialize<T>(raw);
                }
            }
        }

        public TOutput Post<TInput, TOutput>(string urlPart, TInput data, string contentType = CONTENT_XML, string acceptContentType = CONTENT_XML)
        {
            if (this.CheckForUserNameAndPassword())
                throw new ArgumentException("If you are not acting as a guest you must supply userName and password");

            if (string.IsNullOrEmpty(urlPart))
                throw new ArgumentException("Url must be specfied");

            var url = this.CreateUrl(urlPart);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.Accept = acceptContentType;
            req.ContentType = contentType;
            req.Timeout = 20000;
            this.AddBasicAuthHeader(req);

            req.PreAuthenticate = false;
            req.UseDefaultCredentials = false;

            var inputRaw = Serializer.Serialize(data);

            // sanitize off the xml header line; causes a content not allowed in prolog error
            var xmlHeaderLine = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            if (inputRaw.StartsWith(xmlHeaderLine))
            {
                inputRaw = inputRaw.Replace(xmlHeaderLine, string.Empty);
                if (inputRaw.StartsWith(Environment.NewLine))
                {
                    // trim leading newline
                    inputRaw = inputRaw.Substring(1);
                }
            }

            using (StreamWriter writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(inputRaw);
            }

            using (var resp = req.GetResponse())
            {
                using (var reader = new StreamReader(resp.GetResponseStream()))
                {
                    var raw = reader.ReadToEnd();
                    return Serializer.Deserialize<TOutput>(raw);
                }
            }
        }

        public T Put<T>(string urlPart, T data, string contentType = CONTENT_XML, string acceptContentType = CONTENT_XML)
        {
            if (this.CheckForUserNameAndPassword())
                throw new ArgumentException("If you are not acting as a guest you must supply userName and password");

            if (string.IsNullOrEmpty(urlPart))
                throw new ArgumentException("Url must be specfied");

            var url = this.CreateUrl(urlPart);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "PUT";
            req.Accept = acceptContentType;
            req.ContentType = contentType;
            req.Timeout = 20000;
            this.AddBasicAuthHeader(req);

            req.PreAuthenticate = false;
            req.UseDefaultCredentials = false;

            var inputRaw = Serializer.Serialize(data);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputRaw);

            using (Stream requestStream = req.GetRequestStream())
            {
                requestStream.Write(inputBytes, 0, inputBytes.Length);
                requestStream.Close();
            }

            using (var resp = req.GetResponse())
            {
                using (var reader = new StreamReader(resp.GetResponseStream()))
                {
                    var raw = reader.ReadToEnd();
                    return Serializer.Deserialize<T>(raw);
                }
            }
        }

        public void Delete(string urlPart, string contentType = CONTENT_XML, string acceptContentType = CONTENT_XML)
        {
            if (this.CheckForUserNameAndPassword())
                throw new ArgumentException("If you are not acting as a guest you must supply userName and password");

            if (string.IsNullOrEmpty(urlPart))
                throw new ArgumentException("Url must be specfied");

            var url = this.CreateUrl(urlPart);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "DELETE";
            req.Accept = acceptContentType;
            req.ContentType = contentType;
            req.Timeout = 20000;
            this.AddBasicAuthHeader(req);

            req.PreAuthenticate = false;
            req.UseDefaultCredentials = false;

            using (var resp = req.GetResponse() as HttpWebResponse)
            {
                if (resp == null)
                {
                    throw new Exception("Error executing Delete - no code");
                }
                else if (resp.StatusCode != HttpStatusCode.OK) 
                {
                    throw new Exception("Error executing Delete: " + resp.StatusCode);
                }
            }
        }

        private bool CheckForUserNameAndPassword()
        {
            return !this._configuration.ActAsGuest && string.IsNullOrEmpty(this._configuration.UserName) && string.IsNullOrEmpty(this._configuration.Password);
        }

        private string CreateUrl(string urlPart)
        {
            var protocol = this._configuration.UseSSL ? "https://" : "http://";
            var authType = this._configuration.ActAsGuest ? "/guestAuth" : "/httpAuth";

            return string.Format("{0}{1}{2}{3}", protocol, this._configuration.HostName, authType, urlPart);
        }

        private void AddBasicAuthHeader(HttpWebRequest req)
        {
            string authInfo = this._configuration.UserName + ":" + this._configuration.Password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            req.Headers["Authorization"] = "Basic " + authInfo;
        }
    }
}