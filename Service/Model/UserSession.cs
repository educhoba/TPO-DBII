using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Model
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Documento { get; set; }
        public DateTime Login { get; set; }
        public DateTime? Logout { get; set; }
    }
}