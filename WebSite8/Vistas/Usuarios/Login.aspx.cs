using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;
using System.ServiceModel.Channels;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        String rut=txtUsuario.Text;
        String password = txtPassword.Text;
        Usuario usuario = new Usuario().login(rut, password);

        if (usuario.cod_usuario > 0)
        {
            Session["usuario"] = usuario;
            Response.Redirect("~/Default.aspx");
        }
        else {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "<strong>Atención!</strong> Usuario y/o contraseña erróneos"},
                {"clase","alert-danger"}
            };
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Usuario y/o contraseña erróneos');", true);
            //txtUsuario.Text = "";
            //txtPassword.Text = "";
        }
    }
}