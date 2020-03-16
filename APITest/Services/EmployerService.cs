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
    public class EmployerService
    {
        private readonly IMongoCollection<Employer> _employers;
        public IGridFSBucket bucket;

        public EmployerService(IConfiguration config)
        {
            var url = MongoUrl.Create(config.GetConnectionString("StoreDb"));
            var client = new MongoClient(url);
            var db = client.GetDatabase(url.DatabaseName);
            _employers = db.GetCollection<Employer>("employer");
            bucket = new GridFSBucket(db);
        }

        public List<Employer> Get()
        {
            return _employers.Find(employer=> true).ToList();
        }

        public Employer Get(string id)
        {
            return _employers.Find(employer => employer.Id == id).FirstOrDefault();
        }

        public Employer Create(Employer employer)
        {
            employer.Id = ObjectId.GenerateNewId().ToString();
            _employers.InsertOne(employer);
            return employer;
        }

        public void Update(string id, Employer employerIn)
        {
            _employers.ReplaceOne(employer => employer.Id == id, employerIn);
        }

        public void Remove(string id)
        {
            _employers.DeleteOne(employer => employer.Id == id);
        }

        public void UploadFile(IFormFile file, string id)
        {
            try
            {
                var employerTemp = Get(id);
                if (employerTemp.IconUrl != null && employerTemp.IconType != null)
                {
                    bucket.Delete(ObjectId.Parse(employerTemp.IconUrl));
                }
            }
            catch (MongoDB.Driver.GridFS.GridFSFileNotFoundException) { }

            using (var stream = file.OpenReadStream())
            {
                var idFile = bucket.UploadFromStream(file.FileName, stream).ToString();

                var employerTemp = Get(id);
                employerTemp.IconUrl = idFile;
                employerTemp.IconType = file.ContentType;
                _employers.ReplaceOne(employer => employer.Id == id, employerTemp);
            }
        }

        public byte[] DownloadFile(string id)
        {
            var employerTemp = Get(id);
            var bytes = bucket.DownloadAsBytes(ObjectId.Parse(employerTemp.IconUrl));
            return bytes;
        }
    }
}
