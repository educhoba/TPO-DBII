using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBII.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        Service.Service ws = new Service.Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var ret = ws.Login(TextBox1.Text);
                if (ret != null)
                {
                    Response.Redirect("~\\Pages\\Main\\Catalogo.aspx");
                }
            }
            catch(Exception ex)
            {
                tbmsg.Text = ex.Message;
            }
        }
    }
}