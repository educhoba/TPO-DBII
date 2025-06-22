using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Model
{
    public class CarritoItemSession
    {
        public int IdItem { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
    }
}