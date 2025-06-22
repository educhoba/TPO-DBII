using DBII.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.Serialization;

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

                    tbPrecio.Text = item.importe.ToString();
                    tbDescripcion.Text = item.descripcion;

                    var itemEnriquecido = ws.GetItemEnriquecido(item.id);
                    MostrarItem(itemEnriquecido);

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

        private void MostrarItem(ItemEnriquecido item)
        {
            txtNombre.Text = item.Nombre;
            txtMarca.Text = item.Marca;
            txtSqlId.Text = item.SqlId.ToString();

            rptImagenes.DataSource = item.Imagenes;
            rptImagenes.DataBind();

            rptVideos.DataSource = item.Videos;
            rptVideos.DataBind();

            rptComentarios.DataSource = item.Comentarios;
            rptComentarios.DataBind();

            rptEspecificaciones.DataSource = item.Especificaciones;
            rptEspecificaciones.DataBind();
        }
    }
}