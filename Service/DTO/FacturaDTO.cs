using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    public class FacturaDTO : DTOBase
    {
        public int idPedido { get; set; }
        public string condicionPago { get; set; }  
        public decimal total { get; set; }  
        public int idUsuario { get; set; }  
    }
}