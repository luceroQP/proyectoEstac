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
            txt_cod_estacionamiento.Text = estacionamiento.cod_estacionamiento.ToString();
        }
    }
}