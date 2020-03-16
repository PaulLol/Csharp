using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testproj.Models;

namespace testproj.Services
{
    public class ImgService
    {
        private readonly IMongoCollection<Topic> _topics;
        private readonly IMongoCollection<Employer> _employers;
        public IGridFSBucket bucket;

        public ImgService(IConfiguration config)
        {
            var url = MongoUrl.Create(config.GetConnectionString("StoreDb"));
            var client = new MongoClient(url);
            var db = client.GetDatabase(url.DatabaseName);
            _topics = db.GetCollection<Topic>("topic");
            _employers = db.GetCollection<Employer>("employer");
            bucket = new GridFSBucket(db);
        }

        public Topic GetTopic(string id)
        {
            return _topics.Find(topic => topic.Id == id).FirstOrDefault();
        }

        public Employer GetEmployer(string id)
        {
            return _employers.Find(employer => employer.Id == id).FirstOrDefault();
        }

        public void UploadFileTopic(IFormFile file, string id)
        {
            try
            {
                var topicTemp = GetTopic(id);
                if (topicTemp.IconUrl != null && topicTemp.IconType != null)
                {
                    bucket.Delete(ObjectId.Parse(topicTemp.IconUrl));
                }
            }
            catch (MongoDB.Driver.GridFS.GridFSFileNotFoundException) { }

            using (var stream = file.OpenReadStream())
            {
                var idFile = bucket.UploadFromStream(file.FileName, stream).ToString();
                var topicTemp = GetTopic(id);
                topicTemp.IconUrl = idFile;
                topicTemp.IconType = file.ContentType;
                _topics.ReplaceOne(topic => topic.Id == id, topicTemp);
            }
        }

        public void UploadFileEmployer(IFormFile file, string id)
        {
            try
            {
                var employerTemp = GetEmployer(id);
                if (employerTemp.IconUrl != null && employerTemp.IconType != null)
                {
                    bucket.Delete(ObjectId.Parse(employerTemp.IconUrl));
                }
            }
            catch (MongoDB.Driver.GridFS.GridFSFileNotFoundException) { }

            using (var stream = file.OpenReadStream())
            {
                var idFile = bucket.UploadFromStream(file.FileName, stream).ToString();
                var employerTemp = GetEmployer(id);
                employerTemp.IconUrl = idFile;
                employerTemp.IconType = file.ContentType;
                _employers.ReplaceOne(employer => employer.Id == id, employerTemp);
            }
        }

        public byte[] DownloadFile(string id,string collection)
        {
            if (collection == "topic")
            {
                return bucket.DownloadAsBytes(ObjectId.Parse(GetTopic(id).IconUrl));
            }
            return bucket.DownloadAsBytes(ObjectId.Parse(GetEmployer(id).IconUrl));
        }
    }
}
