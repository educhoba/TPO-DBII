using DBII.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBII.Pages.Main
{
    public partial class Catalogo : System.Web.UI.Page
    {
        Service.Service ws = new Service.Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvList.DataSource = ws.GetItems();
                gvList.DataBind();
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

                    int id = (int)gvList.DataKeys[index].Value;

                    var item = ws.GetItem(id);

                    Session.Add("ITEMDTO", item);
                    //TODO traer data de mongo

                    tbPrecio.Text = item.importe.ToString();
                    tbDescripcion.Text = item.descripcion;

                    gvList.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                lbError.Text = ex.Message;
            }
        }

        protected void btAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioDTO user = GetUsuarioSession();
                ItemDTO item = (ItemDTO)Session[MasterPage.ITEM];

                if (item != null)
                {
                    ws.AddItem(user, item);
                }
                else
                    lbError.Text = "null item";
            }
            catch (Exception ex)
            {
                lbError.Text = ex.Message;
            }

            //agregar
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