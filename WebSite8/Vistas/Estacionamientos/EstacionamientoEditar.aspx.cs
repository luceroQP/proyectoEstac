using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class EstacionamientoEditar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null){
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        if (!IsPostBack)
        {
            int codEstacionamiento = (int)Session["cod_estacionamiento"];
            Estacionamiento estacionamiento = new Estacionamiento();
            Session["estacionamiento"] = estacionamiento = new Estacionamiento().buscarPorPk(codEstacionamiento);

            txt_direccion.Text = estacionamiento.direccion;
            txt_valor_hora.Text = estacionamiento.valor_hora.ToString();
            txt_capacidad.Text = estacionamiento.capacidad.ToString();
            txt_latitud.Text = estacionamiento.latitud.ToString().Replace(",", ".");
            txt_longitud.Text = estacionamiento.longitud.ToString().Replace(",", ".");
            txt_cod_estacionamiento.Text = estacionamiento.cod_estacionamiento.ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Estacionamiento estacionamiento = new Estacionamiento();
        estacionamiento.cod_estacionamiento = Int32.Parse(txt_cod_estacionamiento.Text);
        estacionamiento.direccion = txt_direccion.Text;
        estacionamiento.valor_hora = Int32.Parse(txt_valor_hora.Text);
        estacionamiento.capacidad = Int32.Parse(txt_capacidad.Text);

        if (estacionamiento.actualizar(estacionamiento))
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Estacionamiento editado correctamente."},
                {"clase","alert-success"}
            };
            Response.Redirect("~/Vistas/Estacionamientos/Estacionamientos.aspx");
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al editar el estacionamiento."},
                {"clase","alert-danger"}
            };
        }
        //estacionamiento.latitud = Int32.Parse();
        //estacionamiento.longitud = Int32.Parse();
    }
}