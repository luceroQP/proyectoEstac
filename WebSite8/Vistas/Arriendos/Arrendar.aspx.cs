using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Arrendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        else
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!IsPostBack)
            {
                this.llenarEstacionamientos(usuario.cod_usuario);
                this.llenarVehiculos(usuario.cod_usuario);
            }
        }
    }

    private void llenarEstacionamientos(int codUsuario = 0)
    {
        dpd_estacionamiento.DataSource = new Estacionamiento().estacionamientosDisponibles(codUsuario, true);
        dpd_estacionamiento.DataTextField = "direccion";
        dpd_estacionamiento.DataValueField = "cod_estacionamiento";
        dpd_estacionamiento.DataBind();
    }

    private void llenarVehiculos(int codUsuario = 0)
    {
        dpd_vehiculo.DataSource = new Vehiculo().buscarVehiculosUsuario(codUsuario, true);
        dpd_vehiculo.DataTextField = "patente";
        dpd_vehiculo.DataValueField = "cod_vehiculo";
        dpd_vehiculo.DataBind();
    }
    protected void dpd_tipo_disponibilidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (dpd_tipo_disponibilidad.SelectedValue)
        {
            case "0":
                divArrendarPorHoras.Visible = false;
                divInicioArriendo.Visible = false;
                divFinArriendo.Visible = false;
                break;
            case "1":
                divArrendarPorHoras.Visible = true;
                divInicioArriendo.Visible = false;
                divFinArriendo.Visible = false;
                break;
            case "2":
                divArrendarPorHoras.Visible = false;
                divInicioArriendo.Visible = true;
                divFinArriendo.Visible = true;
                break;
            default: 
                break;
        }
    }
    protected void dpd_estacionamiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        string estacionamientoSeleccionado = dpd_estacionamiento.SelectedValue;
        if (!estacionamientoSeleccionado.Equals("") || !estacionamientoSeleccionado.Equals("0"))
        {
            divDatosEstacionamiento.Visible = true;
            Session["estacionamiento"] = new Estacionamiento().buscarPorPk(Int32.Parse(estacionamientoSeleccionado), true);
        }
        else
        {
            divDatosEstacionamiento.Visible = false;
            Session["estacionamiento"] = null;
        }
    }
}