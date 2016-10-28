using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;
using System.Globalization;

public partial class EstacionamientoAgregar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null){
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Usuario usuarioLogeado = (Usuario)Session["usuario"];
        Estacionamiento estacionamiento = new Estacionamiento();

        estacionamiento.direccion = txt_direccion.Text;
        estacionamiento.valor_hora = Int32.Parse(txt_valor_hora.Text);
        estacionamiento.capacidad = Int32.Parse(txt_capacidad.Text);
        estacionamiento.existencias = 0;
        estacionamiento.cod_estacionamiento_estado = 1;
        estacionamiento.latitud = Double.Parse(txt_latitud.Text, CultureInfo.InvariantCulture);
        estacionamiento.longitud = Double.Parse(txt_longitud.Text, CultureInfo.InvariantCulture);
        estacionamiento.cod_usuario = usuarioLogeado.cod_usuario;

        if (estacionamiento.guardar(estacionamiento) > 0)
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Estacionamiento ingresado correctamente."},
                {"clase","alert-success"}
            };
            //Response.Write("<script language='javascript'>window.alert('Estacionamiento ingresado correctamente.');</script>");
            Response.Redirect("~/Vistas/Estacionamientos/Estacionamientos.aspx");
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al registrar el estacionamiento."},
                {"clase","alert-danger"}
            };
            //Response.Write("<script language='javascript'>window.alert('Error al registrar el estacionamiento.');</script>");
        }
    }
}