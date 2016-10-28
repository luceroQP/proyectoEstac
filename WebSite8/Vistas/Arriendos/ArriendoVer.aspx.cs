using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class ArriendoVer : System.Web.UI.Page
{
    private List<int> horas = new List<int>();
    private List<int> minutos = new List<int>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }else{
            Usuario usuario = (Usuario)Session["usuario"];
            int codArriendo = (int)Session["cod_arriendo"];
            Arriendo arriendo;

            Session["arriendo"] = arriendo = new Arriendo().buscarPorPk(codArriendo);
            Session["estacionamiento"] = new Estacionamiento().buscarPorPk(arriendo.cod_estacionamiento,true);
            if (!IsPostBack)
            {
                this.llenarEstacionamientos(arriendo.cod_estacionamiento);
                this.llenarVehiculos(arriendo.cod_vehiculo);
                this.llenarHorasMinutos();
            }

            dpd_hora_inicio.SelectedValue = arriendo.inicio_arriendo.Hour.ToString();
            dpd_minuto_inicio.SelectedValue = arriendo.inicio_arriendo.Minute.ToString();
            dpd_hora_fin.SelectedValue = arriendo.fin_arriendo.Hour.ToString();
            dpd_minuto_fin.SelectedValue = arriendo.fin_arriendo.Minute.ToString();
            txt_horas_usadas.Text = arriendo.horas_usadas.ToString();

            
            dpd_estacionamiento.Enabled = false;
            dpd_vehiculo.Enabled = false;

            dpd_hora_inicio.Enabled = false;
            dpd_minuto_inicio.Enabled = false;
            dpd_hora_fin.Enabled = false;
            dpd_minuto_fin.Enabled = false;
        }
    }

    private void llenarEstacionamientos(int codEstacionamiento = 0)
    {
        List<Estacionamiento> estacionamientos = new List<Estacionamiento>();
        estacionamientos.Add(new Estacionamiento().buscarPorPk(codEstacionamiento));

        dpd_estacionamiento.DataSource = estacionamientos;
        dpd_estacionamiento.DataTextField = "direccion";
        dpd_estacionamiento.DataValueField = "cod_estacionamiento";
        dpd_estacionamiento.DataBind();
    }

    private void llenarVehiculos(int codVehiculo = 0)
    {
        List<Vehiculo> vehiculos = new List<Vehiculo>();
        vehiculos.Add(new Vehiculo().buscarPorPK(codVehiculo));

        dpd_vehiculo.DataSource = vehiculos;
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
}