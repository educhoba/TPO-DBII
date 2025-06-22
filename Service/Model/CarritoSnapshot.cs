using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Model
{
    public class CarritoSnapshot
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("fecha")]
        public DateTime Fecha { get; set; }

        [BsonElement("estado")]
        public List<CarritoItemSession> Estado { get; set; }

        [BsonElement("anterior")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Anterior { get; set; }  // ID del episodio anterior
    }
}