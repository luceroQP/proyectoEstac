using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Arrendar : System.Web.UI.Page
{
    private List<int> horas = new List<int>();
    private List<int> minutos = new List<int>();

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
                this.llenarHorasMinutos();
            }
        }
    }

    private void llenarEstacionamientos(int codUsuario = 0)
    {
        dpd_estacionamiento.DataSource = new Estacionamiento().estacionamientosDisponibles(codUsuario, true, true);
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

    private void llenarHorasMinutos()
    {
        for (int i = 0; i < 24; i++)
        {
            this.horas.Add(i);
        }
        for (int i = 0; i < 60; i++)
        {
            this.minutos.Add(i);
        }

        dpd_hora_inicio.DataSource = this.horas;
        dpd_hora_fin.DataSource = this.horas;
        dpd_minuto_inicio.DataSource = this.minutos;
        dpd_minuto_fin.DataSource = this.minutos;

        dpd_hora_inicio.DataBind();
        dpd_hora_fin.DataBind();
        dpd_minuto_inicio.DataBind();
        dpd_minuto_fin.DataBind();
    }

    protected void dpd_tipo_disponibilidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (dpd_tipo_disponibilidad.SelectedValue)
        {
            case "0":
                divInicioArriendo.Visible = false;
                divFinArriendo.Visible = false;
                break;
            case "1":
                divInicioArriendo.Visible = false;
                divFinArriendo.Visible = false;
                break;
            case "2":
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Arriendo arriendo = new Arriendo();
        Estacionamiento estacionamiento = (Estacionamiento)Session["estacionamiento"];

        arriendo.cod_estacionamiento = Int32.Parse(dpd_estacionamiento.SelectedValue);
        arriendo.cod_vehiculo = Int32.Parse(dpd_vehiculo.SelectedValue);
        arriendo.horas_usadas = Int32.Parse(txt_horas_usadas.Text);

        string hora_inicio = dpd_hora_inicio.SelectedValue + ":" + dpd_minuto_inicio.SelectedValue + ":00";
        string hora_fin = dpd_hora_fin.SelectedValue + ":" + dpd_minuto_fin.SelectedValue + ":00";

        arriendo.inicio_arriendo = Convert.ToDateTime("2000-01-01 " + hora_inicio);
        arriendo.fin_arriendo = Convert.ToDateTime("2000-01-01 " + hora_fin);

        if (arriendo.guardar(arriendo) > 0)
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Arriendo ingresado correctamente."},
                {"clase","alert-success"}
            };
            Response.Redirect("~/Vistas/Estacionamientos/Estacionamientos.aspx");
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al registrar el Arriendo."},
                {"clase","alert-danger"}
            };
        }
    }
}