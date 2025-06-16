using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    public class ItemDTO : DTOBase
    {
        public decimal importe { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
    }
}