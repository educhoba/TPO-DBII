using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    public class PagoRowDTO : DTOBase
    {
        public int idPago { get; set; }
        public int idFactura { get; set; }
        public decimal monto { get; set; }
    }
}