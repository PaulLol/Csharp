using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testproj.Models
{
    public class Employer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Position")]
        public string Position { get; set; }
        [BsonElement("Info")]
        public string Info { get; set; }
        [BsonElement("Color")]
        public string Color { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("IconUrl")]
        public string IconUrl { get; set; }
        [BsonElement("IconType")]
        public string IconType { get; set; }
    }
}
