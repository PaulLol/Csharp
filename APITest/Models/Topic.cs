using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testproj.Models
{
    public class Topic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Text")]
        public string Text { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }
        [BsonElement("TimesTamp")]
        public DateTime TimesTamp { get; set; }
        [BsonElement("Color")]
        public string Color { get; set; }
        [BsonElement("IconUrl")]
        public string IconUrl { get; set; }
        [BsonElement("IconType")]
        public string IconType { get; set; }

    }
}
