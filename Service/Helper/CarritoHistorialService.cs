using MongoDB.Bson;
using MongoDB.Driver;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Helper
{
    public class CarritoHistorialService
    {
        private readonly IMongoCollection<CarritoSnapshot> _collection;

        public CarritoHistorialService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("DBII");
            _collection = db.GetCollection<CarritoSnapshot>("historialCarritos");
        }

        public void GuardarSnapshot(int userId, List<CarritoItemSession> carritoActual)
        {
            var ultimo = ObtenerUltimoSnapshot(userId);

            var snapshot = new CarritoSnapshot
            {
                UserId = userId,
                Fecha = DateTime.Now,
                Estado = carritoActual,
                Anterior = ultimo?.Id.ToString()
            };

            _collection.InsertOne(snapshot);
        }


        public CarritoSnapshot DeshacerUltimoCambio(int userId)
        {
            var actual = ObtenerUltimoSnapshot(userId);
            if (actual?.Anterior == null) return null;

            var anterior = ObtenerEpisodioPorId(actual.Anterior);

            _collection.DeleteOne(s => s.Id == actual.Id);

            return anterior;
        }
        private CarritoSnapshot ObtenerUltimoSnapshot(int userId)
        {
            return _collection
                .Find(s => s.UserId == userId)
                .SortByDescending(s => s.Fecha)
                .FirstOrDefault();
        }

        private CarritoSnapshot ObtenerEpisodioPorId(string id)
        {
            return _collection.Find(s => s.Id == ObjectId.Parse(id)).FirstOrDefault();
        }

        public void BorrarHistorial(int userId)
        {
            _collection.DeleteMany(s => s.UserId == userId);
        }
    }
}