using MongoDB.Bson;
using MongoDB.Driver;
using Service.DTO;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Helper
{
    public class MongoLogger
    {
        public static void InsertarLog(Operacion operacion, string usuario, DTOBase dto, string table)
        {
            //TODO deberia ser ASYNC
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("DBII");
            var collection = database.GetCollection<Log>("Logs");

            var bson = dto.ToBsonDocument();

            var nuevoLog = new Log
            {
                Tabla = table,
                Operacion = operacion.ToString(),
                Usuario = usuario,
                Fecha = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}"),
                Data = bson
            };

            collection.InsertOne(nuevoLog);
        }
    }
    public enum Operacion
    {
        INSERT,
        DELETE,
        UPDATE
    }

}