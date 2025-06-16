using DBII.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBII.Pages
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public const string USER = "USERDTO";
        public const string ITEM = "ITEMDTO";
        public const string CARRITO = "CARRITODTO";
        public const string PEDIDO = "PEDIDODTO";
        public const string FACTURA = "FACTURADTO";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}