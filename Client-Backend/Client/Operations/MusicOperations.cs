using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Client.Operations
{
    class MusicOperations
    {
        private string baseUrl;
        public MusicOperations()
        {
            this.baseUrl = "https://localhost:44360/music";
        }
        public void LoadFileAdmin(string filepath)
        {
            using (WebClient wc = new WebClient())
            {
                wc.UploadFile(baseUrl+"/file", "POST", filepath);
            }
        }
        public List<string> DownloadGroups()
        {
            string endpoint = this.baseUrl + "/groups";
            using (WebClient client = new WebClient())
            {
                var responce = client.DownloadString(endpoint);
                return JsonConvert.DeserializeObject<List<string>>(responce);
            }
        }
        public List<string> DownloadSongs(string group)
        {
            string endpoint = this.baseUrl + "/" + group;
            using (WebClient client = new WebClient())
            {
                var responce = client.DownloadString(endpoint);
                return JsonConvert.DeserializeObject<List<string>>(responce);
            }
        }
        public string GetIdFile(string group, string song)
        {
            string endpoint = this.baseUrl + "/"+ group + "/" + song;
            WebClient client = new WebClient();
            return client.DownloadString(endpoint);
        }

        public byte[] DownloadFile(string idFile)
        {
            string endpoint = this.baseUrl + "/file/" + idFile;
            WebClient client = new WebClient();
            byte[] song = client.DownloadData(endpoint);
            return song;
        }

        public List<string> LoadFileUser(string filepath, string idUser)
        {
            string uri = this.baseUrl + "/file/user/" + idUser;
            using (WebClient wc = new WebClient())
            {
                var responce = wc.UploadFile(uri, "POST", filepath);
                string myString = Encoding.ASCII.GetString(responce); ;
                return JsonConvert.DeserializeObject<List<string>>(myString);
            }
        }
        public byte[] DownloadPrivateFile(string idFile, string fileType)
        {
            string endpoint = this.baseUrl + "/file/" + idFile + "/" + fileType;
            WebClient client = new WebClient();
            byte[] song = client.DownloadData(endpoint);
            return song;
        }
    }
}
