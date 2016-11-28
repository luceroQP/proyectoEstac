using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vistas_Transacciones_Transacciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }

        Usuario usuario = (Usuario) Session["usuario"];
        Tarjeta tarjeta = new Tarjeta().getReporteTarjeta(usuario.cod_usuario);
        Session["tarjetaReporte"] = tarjeta;
    }
}