using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    public class PedidoDTO : DTOBase
    {   
        public int idUsuario { get; set; }
        public string condicionIVA { get; set; }
    }
}