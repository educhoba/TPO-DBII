using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Model
{
    public class ItemEnriquecido
    {
        public ItemEnriquecido() { }
        public class Comentario
        {
            public string Usuario { get; set; }
            public string ComentarioTexto { get; set; }
            public DateTime Fecha { get; set; }
            public int Calificacion { get; set; }
        }
        public class Especificacion
        {
            public string Clave { get; set; }
            public string Valor { get; set; }
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int SqlId { get; set; }
        public string Nombre { get; set; }
        public List<string> Imagenes { get; set; }
        public List<string> Videos { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<Especificacion> Especificaciones { get; set; }
        public string Marca { get; set; }
    }
}