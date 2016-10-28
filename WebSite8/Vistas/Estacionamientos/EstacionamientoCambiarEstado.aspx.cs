using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class EstacionamientoCambiarEstado : System.Web.UI.Page
{
    private List<int> horas = new List<int>();
    private List<int> minutos = new List<int>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null){
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }

        if (!IsPostBack)
        {
            int codEstacionamiento = (int)Session["cod_estacionamiento"];
            Session["estacionamiento"] = new Estacionamiento().buscarPorPk(codEstacionamiento);

            txt_cod_estacionamiento.Text = codEstacionamiento.ToString();
            llenarComboEstados();
            llenarHorasMinutos();
        }
    }

    private void llenarComboEstados()
    {
        dpd_estado.DataSource = new EstacionamientoEstado().buscarTodos(true);
        dpd_estado.DataTextField = "nombre_estacionamiento_estado";
        dpd_estado.DataValueField = "cod_estacionamiento_estado";
        dpd_estado.DataBind();
    }

    private void llenarHorasMinutos(){
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

    protected void dpd_estado_SelectedIndexChanged(object sender, EventArgs e)
    {
        int estadoSeleccionado = Int32.Parse(dpd_estado.SelectedValue);

        //Habilitado según horario(4), ahora estan solo los habilitados 
        if (estadoSeleccionado.Equals(2))
        {
            divInicioDisponibilidad.Visible = true;
            divFinDisponibilidad.Visible = true;
        }
        else
        {
            divInicioDisponibilidad.Visible = false;
            divFinDisponibilidad.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int estadoSeleccionado = Int32.Parse(dpd_estado.SelectedValue);
        Estacionamiento estacionamiento = new Estacionamiento();

        estacionamiento.cod_estacionamiento = Int32.Parse(txt_cod_estacionamiento.Text);
        estacionamiento.cod_estacionamiento_estado = estadoSeleccionado;

        //Habilitado según horario
        if (estadoSeleccionado.Equals(2))
        {
            string hora_inicio = dpd_hora_inicio.SelectedValue + ":" + dpd_minuto_inicio.SelectedValue + ":00";
            string hora_fin = dpd_hora_fin.SelectedValue + ":" + dpd_minuto_fin.SelectedValue + ":00";

            estacionamiento.inicio_disponibilidad = Convert.ToDateTime("2000-01-01 " + hora_inicio);
            estacionamiento.fin_disponibilidad = Convert.ToDateTime("2000-01-01 " + hora_fin);
        }

        if (estacionamiento.actualizar(estacionamiento))
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Estado actualizado correctamente."},
                {"clase","alert-success"}
            };
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Estado actualizado correctamente');", true);
            Response.Redirect("~/Vistas/Estacionamientos/Estacionamientos.aspx");
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al actualizar el estado, intente nuevamente."},
                {"clase","alert-danger"}
            };
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al actualizar el estado, intente nuevamente');", true);
        }
    }
}