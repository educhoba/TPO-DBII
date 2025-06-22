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
            UserSession user = GetUsuarioSession();
            gvList.DataSource = ws.GetCarritoItems(user);
            gvList.DataBind();
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvList.Rows[index];

                int idItem = (int)gvList.DataKeys[index].Value;
                UserSession user = GetUsuarioSession();
                var items = ws.GetCarritoItems(user);
                var item = items.First(x=>x.IdItem == idItem);

                if (e.CommandName == "Agregar")
                {
                    item.Cantidad++;
                    //ws.UpdateCarritoRow
                }
                else if (e.CommandName == "Restar")
                {
                    item.Cantidad--;
                }

                if (e.CommandName == "Eliminar" || item.Cantidad == 0)
                {
                    ws.DeleteCarritoRow(user,idItem);
                }
                else
                {
                    ws.UpdateCarritoRow(user,item.IdItem, item.Cantidad);
                }

                reloadCarrito();
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
        public List<CarritoItemSession> GetCarritoSession(UserSession user)
        {
            var carrito = ws.GetCarritoItems(user);
            return carrito?.ToList();
        }

        protected void btVolver_Click(object sender, EventArgs e)
        {
            try
            {
                gvList.DataSource = ws.RollBackCarrito(GetUsuarioSession());
                gvList.DataBind();
            }
            catch(Exception ex)
            {
                lbError.Text = ex.Message;
            }
        }

        protected void btPedido_Click(object sender, EventArgs e)
        {
            try
            {
                var user = GetUsuarioSession();
                var condicion = ddlIVA.SelectedValue;
                ws.NuevoPedido(user, condicion);
                reloadCarrito();
            }
            catch(Exception ex)
            {
                lbError.Text = ex.Message;
            }
        }
    }
}