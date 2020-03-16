using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Music
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Group")]
        public string Group { get; set; }

        [BsonElement("Song")]
        public string Song { get; set; }
        [BsonElement("IdFile")]
        public string IdFile { get; set; }
        [BsonElement("FileType")]
        public string FileType { get; set; }
    }
}
