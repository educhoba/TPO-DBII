﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    public class PagoDTO : DTOBase
    {
        public decimal total { get; set; }
        public int idUsuario { get; set; }
    }
}