using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Tarjetas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null){
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }

        if (!IsPostBack)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            gv_tarjetas.DataSource = new Tarjeta().buscarTodos(usuario.cod_usuario);
            gv_tarjetas.DataBind();
        }
    }
}