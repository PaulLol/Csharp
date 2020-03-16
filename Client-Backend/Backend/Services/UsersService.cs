using Backend.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Temp> _temp;
        public UsersService(IConfiguration config)
        {
            var url = MongoUrl.Create(config.GetConnectionString("StoreDb"));
            var client = new MongoClient(url);
            var db = client.GetDatabase(url.DatabaseName);
            _users = db.GetCollection<User>("users");
            _temp = db.GetCollection<Temp>("temp");
        }

        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public User GetForId(string id)
        {
            return _users.Find(user => user.Id == id).FirstOrDefault();
        }

        public User GetForMail(string mail)
        {
            return _users.Find(user => user.Mail == mail).FirstOrDefault();
        }
        public void Create(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            _users.InsertOne(user);
        }

        public void Remove(string mail)
        {
            _users.DeleteOne(user => user.Mail == mail);
        }

        public void SendMail(Temp temp)
        {
            temp.Id = ObjectId.GenerateNewId().ToString();
            _temp.InsertOne(temp);
        }

        public Temp GetForMailTemp(string mail)
        {
            return _temp.Find(temp => temp.Mail == mail).FirstOrDefault();
        }

        public void RemoveTemp(string mail)
        {
            _temp.DeleteOne(temp => temp.Mail == mail);
        }

        public void Update(string id, User userIn)
        {
            _users.ReplaceOne(user => user.Id == id, userIn);
        }

        public List<string> FindListPersonalMusic(string id)
        {
            return _users.Find(x => x.Id == id).FirstOrDefault().PersonalMusic;

        }
        public List<string> FindListPersonalMusicId(string id)
        {
            return _users.Find(x => x.Id == id).FirstOrDefault().PersonalMusicId;
        }

        public List<string> FindListPersonalMusicFileType(string id)
        {
            return _users.Find(x => x.Id == id).FirstOrDefault().PersonalMusicFileType;
        }
    }
}
