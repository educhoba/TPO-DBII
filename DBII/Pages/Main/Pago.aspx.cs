using DBII.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBII.Pages.Main
{
    public partial class Pago : System.Web.UI.Page
    {
        Service.Service ws = new Service.Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                try
                {
                    var user = GetUsuarioSession();
                    gvList.DataSource = ws.GetPagosDeUsuario(user);
                    gvList.DataBind();
                }
                catch (Exception ex)
                {
                    lbError.Text = ex.Message;
                }
        }

        public UserSession GetUsuarioSession()
        {
            var user = ws.GetUser();
            return user;
        }
    }
}