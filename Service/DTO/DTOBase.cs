using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.DTO
{
    [Serializable]
    public abstract class DTOBase
    {
        public int id { get; set; }
    }
}