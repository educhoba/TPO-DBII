using DBII.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBII.Pages.Main
{
    public partial class Pedido : System.Web.UI.Page
    {
        Service.Service ws = new Service.Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                reloadPedidos();
            }
        }

        private void reloadPedidos()
        {
            try
            {
                UsuarioDTO user = GetUsuarioSession();
                var pedidos = ws.GetPedidosSinFacturar(user);
                gvList.DataSource = pedidos;
                gvList.DataBind();
            }
            catch(Exception ex)
            {
                lbError.Text = ex.Message;
            }
        }

        protected void btFactura_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioDTO user = GetUsuarioSession();
                PedidoDTO pedido = GetPedidoSession();
                string condicion = ddlPago.SelectedValue;

                ws.NuevaFactura(pedido, user, condicion);
                reloadPedidos();
                gv.DataSource = new List<PedidoDTO>();
                gv.DataBind();
            }
            catch (Exception ex)
            {
                lbError.Text = ex.Message;
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvList.Rows[index];

                if (e.CommandName == "Buscar")
                {
                    int idPedido = (int)gvList.DataKeys[index].Value;
                    var pedido = ws.GetPedido(idPedido);
                    gvList.SelectedIndex = index;
                    Session.Add(MasterPage.PEDIDO, pedido);
                        
                    var rows = ws.GetPedidoRows(idPedido);

                    decimal total = 0;
                    foreach (var r in rows)
                    {
                        total += r.cantidad * r.importe * (1 - r.descuento / 100) * (1 + r.IVA / 100);
                    }

                    tbTotal.Text = "$ " + total.ToString();

                    gv.DataSource = rows;
                    gv.DataBind();
                }
            }
            catch (Exception ex)
            {
                lbError.Text = ex.Message;
            }
        }
        public UsuarioDTO GetUsuarioSession()
        {
            //TODO recuperarla de redis?
            UsuarioDTO user = (UsuarioDTO)Session[MasterPage.USER];
            if (user == null)
                Response.Redirect("~\\Pages\\Login.aspx");
            return user;
        }
        public PedidoDTO GetPedidoSession()
        {
            //TODO recuperarla de redis?
            PedidoDTO var = (PedidoDTO)Session[MasterPage.PEDIDO];
            if (var == null)
                Response.Redirect("~\\Pages\\Main\\Pedido.aspx");

            return var;
        }

    }
}