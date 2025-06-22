using MongoDB.Driver;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Service.Model.ItemEnriquecido;

namespace Service.ChatGPT
{
    public class InsertarArticulos
    {
        public static void Insertar()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("DBII");
            var collection = database.GetCollection<ItemEnriquecido>("Items");

            var articulos = new List<ItemEnriquecido>
            {
                new ItemEnriquecido
                {
                    SqlId = 1,
                    Nombre = "Mouse inalámbrico",
                    Imagenes = new List<string> { "https://mi-tienda.com/img/mouse1.jpg", "https://mi-tienda.com/img/mouse1_2.jpg" },
                    Videos = new List<string> { "https://www.youtube.com/watch?v=video_mouse_demo" },
                    Comentarios = new List<Comentario>
                    {
                        new Comentario { Usuario = "juanperez", ComentarioTexto = "Excelente calidad por el precio.", Fecha = DateTime.Parse("2025-06-15"), Calificacion = 5 },
                        new Comentario { Usuario = "maria2024", ComentarioTexto = "Funciona bien, pero el alcance podría ser mejor.", Fecha = DateTime.Parse("2025-06-18"), Calificacion = 4 }
                    },
                    Especificaciones = new List<Especificacion>
                    {
                        new Especificacion {Clave = "conexion", Valor = "Inalámbrica 2.4GHz" },
                        new Especificacion {Clave = "bateria", Valor = "AA (incluida)" },
                        new Especificacion {Clave = "color", Valor = "Negro" }
                    },
                    Marca = "Logitech"
                },
                new ItemEnriquecido
                {
                    SqlId = 2,
                    Nombre = "Teclado mecánico",
                    Imagenes = new List<string> { "https://mi-tienda.com/img/teclado1.jpg" },
                    Videos = new List<string> { "https://www.youtube.com/watch?v=video_teclado_review" },
                    Comentarios = new List<Comentario>
                    {
                        new Comentario { Usuario = "techguy", ComentarioTexto = "Buen sonido de las teclas, ideal para gaming.", Fecha = DateTime.Parse("2025-06-20"), Calificacion = 5 }
                    },
                    Especificaciones = new List<Especificacion>
                    {
                        new Especificacion{ Clave = "tipo_teclado", Valor = "Mecánico" },
                        new Especificacion{ Clave = "retroiluminacion", Valor = "RGB" }   ,
                        new Especificacion{ Clave = "conexion", Valor = "USB" }
                    },
                    Marca = "Redragon"
                },
                new ItemEnriquecido
                {
                    SqlId = 3,
                    Nombre = "Monitor 24\" Full HD",
                    Imagenes = new List<string> { "https://mi-tienda.com/img/monitor1.jpg" },
                    Videos = new List<string>(),
                    Comentarios = new List<Comentario>(),
                    Especificaciones = new List<Especificacion>
                    {
                        new Especificacion{ Clave = "tamaño", Valor = "24 pulgadas" },
                        new Especificacion{ Clave = "frecuencia", Valor = "75Hz" },
                        new Especificacion{ Clave = "puertos", Valor = "HDMI, VGA" }    ,
                    },
                    Marca = "Samsung"
                },
                new ItemEnriquecido
                {
                    SqlId = 4,
                    Nombre = "Auriculares Bluetooth",
                    Imagenes = new List<string> { "https://mi-tienda.com/img/auriculares.jpg" },
                    Videos = new List<string> { "https://www.youtube.com/watch?v=video_auriculares" },
                    Comentarios = new List<Comentario>
                    {
                        new Comentario { Usuario = "lucas23", ComentarioTexto = "Buena calidad de sonido, pero algo incómodos tras mucho uso.", Fecha = DateTime.Parse("2025-06-12"), Calificacion = 3 }
                    },
                    Especificaciones = new List<Especificacion>
                    {
                        new Especificacion{ Clave = "conexion", Valor = "Bluetooth 5.0" },
                        new Especificacion{ Clave = "duracion_bateria", Valor = "10 horas" },
                        new Especificacion{ Clave = "color", Valor = "Blanco" }
                    },
                    Marca = "Sony"
                },
                new ItemEnriquecido
                {
                    SqlId = 5,
                    Nombre = "Hub USB-C 4 puertos",
                    Imagenes = new List<string> { "https://mi-tienda.com/img/hub.jpg" },
                    Videos = new List<string>(),
                    Comentarios = new List<Comentario>(),
                    Especificaciones = new List<Especificacion>
                    {
                        new Especificacion{ Clave = "puertos", Valor = "USB 3.0 x4" },
                        new Especificacion{ Clave = "compatibilidad", Valor = "Windows / Mac / Android" },
                        new Especificacion{ Clave = "color", Valor = "Gris espacial" }  
                    },
                    Marca = "Anker"
                }
            };

            collection.InsertMany(articulos);
            Console.WriteLine("Artículos insertados en MongoDB.");
        }
    }
}