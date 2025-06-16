using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    public class UsuarioDTO : DTOBase
    {
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string documento { get; set; }
        public string categoria { get; set; }
    }
}