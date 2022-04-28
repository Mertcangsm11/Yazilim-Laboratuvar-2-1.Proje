using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarProje.Models
{
    public class ProjectModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("TARİH")]
        public DateTime TARİH { get; set; }
        [BsonElement("SAAT")]
        public DateTime SAAT { get; set; }
        [BsonElement("LAT")]
        public string LAT { get; set; }
        [BsonElement("LNG")]
        public string LNG { get; set; }
        [BsonElement("CAR")]
        public int CAR { get; set; }
    }
}
