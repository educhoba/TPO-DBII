using DBII.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBII.Pages.Main
{
    public partial class Carrito : System.Web.UI.Page
    {
        Service.Service ws = new Service.Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                reloadCarrito();
            }
        }

        private void reloadCarrito()
        {
            UsuarioDTO user = GetUsuarioSession();
            CarritoDTO carrito = ws.GetCarritoFrom(user);
            Session.Add(MasterPage.CARRITO, carrito);
            gvList.DataSource = ws.GetCarritoItems(carrito.id);
            gvList.DataBind();
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvList.Rows[index];

                int id = (int)gvList.DataKeys[index].Value;
                var item = ws.GetCarritoRow(id);

                if (e.CommandName == "Agregar")
                {
                    item.cantidad++;
                    //ws.UpdateCarritoRow
                }
                else if (e.CommandName == "Restar")
                {
                    item.cantidad--;
                }

                if (e.CommandName == "Eliminar" || item.cantidad == 0)
                {
                    ws.DeleteCarritoRow(id);
                }
                else
                {
                    ws.UpdateCarritoRow(item.idCarrito, item.idItem, item.cantidad, item.id);
                }

                reloadCarrito();
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
        public CarritoDTO GetCarritoSession()
        {
            //TODO recuperarla de redis?
            CarritoDTO var = (CarritoDTO)Session[MasterPage.CARRITO];
            if (var == null)
                Response.Redirect("~\\Pages\\Main\\Carrito.aspx");

            return var;
        }

        protected void btVolver_Click(object sender, EventArgs e)
        {
            //TODO redis volver atras PRIORIDAD
        }

        protected void btPedido_Click(object sender, EventArgs e)
        {
            try
            {
                var carrito = GetCarritoSession();
                var user = GetUsuarioSession();
                var condicion = ddlIVA.SelectedValue;
                ws.NuevoPedido(carrito, user, condicion);
                reloadCarrito();
            }
            catch(Exception ex)
            {
                lbError.Text = ex.Message;
            }
        }
    }
}