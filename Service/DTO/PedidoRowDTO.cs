using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    public class PedidoRowDTO : DTOBase
    {
        public int idPedido { get; set; }
        public decimal importe { get; set; }
        public decimal IVA { get; set; }
        public decimal descuento { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
    }
}