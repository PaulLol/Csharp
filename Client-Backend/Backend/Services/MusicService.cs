using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class MusicService
    {
        private readonly IMongoCollection<Music> _music;
        //private readonly IMongoCollection<User> _users;
        public IGridFSBucket bucket;

        public MusicService(IConfiguration config)
        {
            var url = MongoUrl.Create(config.GetConnectionString("StoreDb"));
            var client = new MongoClient(url);
            var db = client.GetDatabase(url.DatabaseName);
            bucket = new GridFSBucket(db);
            _music = db.GetCollection<Music>("music");
            //_users = db.GetCollection<User>("users");
        }

        public void UploadFileMusic(IFormFile file)
        {
            string idFile;
            using (var stream = file.OpenReadStream())
            {
                idFile = bucket.UploadFromStream(file.FileName, stream).ToString();
            }

            var filename = file.FileName;
            int indexOfSubstring = filename.IndexOf("-");

            string group = filename.Remove(indexOfSubstring - 1);
            string song = filename.Remove(0, indexOfSubstring + 2);
            string extension = Path.GetExtension(song);
            indexOfSubstring = song.IndexOf(extension);
            song = song.Remove(indexOfSubstring);


            Music music = new Music()
            {
                Group = group,
                Song = song,
                IdFile = idFile,
                FileType = file.ContentType
            };
            music.Id = ObjectId.GenerateNewId().ToString();
            _music.InsertOne(music);
        }
        public async Task<List<string>> Groups()
        {
            List<string> groups = new List<string>();
            var cur = await _music.Find(music => true).ToListAsync();

            foreach (Music music in cur)
            {
                groups.Add(music.Group);
            }
            return groups;
        }
        public async Task<List<string>> FindSongs(string group)
        {
            List<string> songs = new List<string>();
            //var filter = new BsonDocument("Group", group);
            var cur = await _music.Find(x => x.Group == group).ToListAsync();

            foreach (Music music in cur)
            {
                songs.Add(music.Song);
            }
            return songs;
        }
        public async Task<string> GetIdFile(string group, string song)
        {
            List<string> songs = new List<string>();
            var cur = await _music.Find(x => x.Group == group).ToListAsync();

            foreach (Music music in cur)
            {
                if (music.Song == song)
                    return music.IdFile;
            }
            return null;
        }
        public byte[] Downloadfile(string idFile)
        {
            return bucket.DownloadAsBytes(ObjectId.Parse(idFile));
        }
        public string GetFileType(string idFile)
        {
            Music music = new Music();
            music = _music.Find(music => music.IdFile == idFile).FirstOrDefault();
            return music.FileType;
        }

        public List<string> UploadPrivateFileMusic(IFormFile file, string id)
        {
            string idFile;
            string fileType;
            using (var stream = file.OpenReadStream())
            {
                idFile = bucket.UploadFromStream(file.FileName, stream).ToString();
                fileType = file.ContentType;
            }

            List<string> ls = new List<string>();
            ls.Add(idFile);
            ls.Add(fileType);

            return ls;
        }
    }
}
