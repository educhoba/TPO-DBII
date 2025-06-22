using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Model
{
    public class Log
    {
        public Log() { }
        [BsonId]
        public ObjectId Id { get; set; }
        public string Tabla { get; set; }
        public string Operacion { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public BsonDocument Data { get; set; }

    }
}