using Service.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using MongoDB.Bson;
using Service.Model;
using MongoDB.Driver;
using System.Data.SqlTypes;

namespace Service.DAO
{
    public class DAOItemEnriquecido
    {
        public static ItemEnriquecido GetItemEnriquecido(int idSQL) {

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("DBII");
            var collection = database.GetCollection<ItemEnriquecido>("Items");
            return collection.Find(a => a.SqlId == idSQL).FirstOrDefault();
        }
    }
}