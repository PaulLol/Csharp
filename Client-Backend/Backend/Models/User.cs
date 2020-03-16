using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Mail")]
        public string Mail { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("PersonalMusic")]
        public List<string> PersonalMusic { get; set; }
        [BsonElement("PersonalMusicId")]
        public List<string> PersonalMusicId { get; set; }
        [BsonElement("PersonalMusicFileType")]
        public List<string> PersonalMusicFileType { get; set; }
    }
}
