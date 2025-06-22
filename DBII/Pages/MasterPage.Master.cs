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

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            try
            {
                LogOut();
            }
            catch(Exception ex)
            {
                lbMasterMsg.Text = ex.Message;
            }
        }
        private void LogOut()
        {
            Service.Service ws = new Service.Service();
            var user = ws.GetUser();
            ws.LogOut(user);

            Response.Redirect("~\\Pages\\Login.aspx");
        }
    }
}