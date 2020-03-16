using Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Client.Operations
{
    class UsersOperations
    {
        private string baseUrl;
        public UsersOperations()
        {
            this.baseUrl = "https://localhost:44360/users";
        }
        public User AuthenticateUser(string mail, string password)
        {
            string endpoint = this.baseUrl + "/" + mail;
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                mail,
                password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUserDetails(User user)
        {
            string endpoint = this.baseUrl + "/" + user.Id;
            string access_token = user.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = access_token;
            try
            {
                string response = wc.DownloadString(endpoint);
                user = JsonConvert.DeserializeObject<User>(response);
                user.access_token = access_token;
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User RegisterUser(string mail, string password)
        {
            string endpoint = this.baseUrl;
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                mail,
                password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void RemoveTemp(string mail)
        {
            WebRequest request = WebRequest.Create(this.baseUrl + "/temp/" + mail);
            request.Method = "DELETE";
            WebResponse response = request.GetResponse();
        }
        public Temp ChechPass(string mail)
        {
            string endpoint = this.baseUrl + "/sendmail";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                mail
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<Temp>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void ChangePass(string password)
        {
            string endpoint = this.baseUrl + "/" + Global.LoggedInUser.Id;
            string method = "PUT";

            string json = JsonConvert.SerializeObject(new
            {
                Global.LoggedInUser,
                password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                wc.UploadString(endpoint, method, json);
            }
            catch (Exception) { }
        }

        public void LoadPrivateMusic(string music, string idMusic, string fileType)
        {
            string endpoint = this.baseUrl + "/" + Global.LoggedInUser.Id;
            string method = "PUT";

            User user = Global.LoggedInUser;

            if (user.PersonalMusic == null && user.PersonalMusicId == null && user.PersonalMusicFileType == null)
            {
                user.PersonalMusic = new List<string>();
                user.PersonalMusic.Add(music);
                user.PersonalMusicId = new List<string>();
                user.PersonalMusicId.Add(idMusic);
                user.PersonalMusicFileType = new List<string>();
                user.PersonalMusicFileType.Add(fileType);
            }

            string json = JsonConvert.SerializeObject(user);

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                wc.UploadString(endpoint, method, json);
            }
            catch (Exception) { }
        }

        public List<string> GetListPersonalMusic(string id)
        {
            string endpoint = this.baseUrl + "/personalmusic/" + id;
            WebClient client = new WebClient();
            var responce = client.DownloadString(endpoint);
            return JsonConvert.DeserializeObject<List<string>>(responce);
        }

        public List<string> GetListPersonalMusicId(string id)
        {
            string endpoint = this.baseUrl + "/personalmusicid/" + id;
            WebClient client = new WebClient();
            var responce = client.DownloadString(endpoint);
            return JsonConvert.DeserializeObject<List<string>>(responce);
        }
        public List<string> GetListPersonalMusicFileType(string id)
        {
            string endpoint = this.baseUrl + "/personalmusicfiletype/" + id;
            WebClient client = new WebClient();
            var responce = client.DownloadString(endpoint);
            return JsonConvert.DeserializeObject<List<string>>(responce);
        }
    }
}
