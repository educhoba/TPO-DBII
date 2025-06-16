using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    public class CarritoRowDTO : DTOBase
    {
        public int idCarrito { get; set; }
        public int idItem { get; set; }
    }
}