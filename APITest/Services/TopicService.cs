using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using testproj.Models;

namespace testproj.Services
{
    public class TopicService
    {
        private readonly IMongoCollection<Topic> _topics;

        public TopicService(IConfiguration config)
        {
            var url = MongoUrl.Create(config.GetConnectionString("StoreDb"));
            var client = new MongoClient(url);
            var db = client.GetDatabase(url.DatabaseName);
            _topics = db.GetCollection<Topic>("topic");
        }

        public List<Topic> Get()
        {
            return _topics.Find(topic => true).ToList();
        }

        public Topic Get(string id)
        {
            return _topics.Find(topic => topic.Id == id).FirstOrDefault();
        }

        public Topic Create(Topic topic)
        {
            topic.Id = ObjectId.GenerateNewId().ToString();
            topic.TimesTamp = DateTime.UtcNow;
            _topics.InsertOne(topic);
            return topic;
        }

        public void Update(string id, Topic topicIn)
        {
            _topics.ReplaceOne(topic => topic.Id == id, topicIn);
        }

        public void Remove(string id)
        {
            _topics.DeleteOne(topic => topic.Id == id);
        }

        //public void UploadFile(IFormFile file, string id)
        //{
        //    try
        //    {
        //        var topicTemp = Get(id);

        //        if (topicTemp.IconUrl != null && topicTemp.IconType != null)
        //        {
        //            bucket.Delete(ObjectId.Parse(topicTemp.IconUrl));
        //        }
        //    }
        //    catch (MongoDB.Driver.GridFS.GridFSFileNotFoundException) { }

        //    using (var stream = file.OpenReadStream())
        //    {
        //        var idFile = bucket.UploadFromStream(file.FileName, stream).ToString();

        //        var topicTemp = Get(id);
        //        topicTemp.IconUrl = idFile;
        //        topicTemp.IconType = file.ContentType;
        //        _topics.ReplaceOne(topic => topic.Id == id, topicTemp);
        //    }
        //}

        //public byte[] DownloadFile(string id)
        //{
        //    var topicTemp = Get(id);
        //    var bytes = bucket.DownloadAsBytes(ObjectId.Parse(topicTemp.IconUrl));
        //    return bytes;
        //}

    }
}
