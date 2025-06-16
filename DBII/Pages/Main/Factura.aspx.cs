using DBII.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBII.Pages.Main
{
    public partial class Factura : System.Web.UI.Page
    {
        Service.Service ws = new Service.Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                reloadFacturas();
            }
        }

        private void reloadFacturas()
        {
            var user = GetUsuarioSession();
            gvList.DataSource = ws.GetFacturasSinPagar(user);
            gvList.DataBind();
        }

        protected void btnProcesarSeleccion_Click(object sender, EventArgs e)
        {
            List<FacturaDTO> facturasAPagar = new List<FacturaDTO>();
            decimal total = 0;
            foreach (GridViewRow row in gvList.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSeleccionar");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gvList.DataKeys[row.RowIndex].Value);
                    var factura = ws.GetFactura(id);
                    facturasAPagar.Add(factura);
                    total += factura.total;
                }
            }
            var user = GetUsuarioSession();
            if (facturasAPagar.Count > 0)
                ws.NuevoPago(facturasAPagar.ToArray(), user, total);

        }
        public UsuarioDTO GetUsuarioSession()
        {
            //TODO recuperarla de redis?
            UsuarioDTO user = (UsuarioDTO)Session[MasterPage.USER];
            if (user == null)
                Response.Redirect("~\\Pages\\Login.aspx");
            return user;
        }

    }
}