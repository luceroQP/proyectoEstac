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

    protected string urlBack = "~/Vistas/Arriendos/Arriendos.aspx";

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
                Session["estacionamientos"] = new Estacionamiento().estacionamientosDisponibles(codUsuarioExcluir: usuario.cod_usuario, soloActivos: true, llenaCombo: false, incluirAsocc: true);
                this.llenarVehiculos(usuario.cod_usuario);
                this.llenarHorasMinutos();
            }else{
                fecha_inicio_arriendo.Text = Request.Form[fecha_inicio_arriendo.UniqueID];
                fecha_termino_arriendo.Text = Request.Form[fecha_termino_arriendo.UniqueID];
            }

            Boolean saldoDisponibleTarjeta = new Tarjeta().validarSaldoTarjeta(usuario.cod_usuario);
            if (!saldoDisponibleTarjeta)
            {
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "¡Atención! Su tarjeta no registra saldo suficiente."},
                    {"clase","alert-danger"}
                };
                btn_arrendar.Visible = false;
                btn_arrendar.Enabled = false;
            }
        }
        
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
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        Arriendo arriendo = new Arriendo();
        Usuario usuario = (Usuario)Session["usuario"];
        Boolean saldoDisponibleTarjeta = new Tarjeta().validarSaldoTarjeta(usuario.cod_usuario);

        if (!saldoDisponibleTarjeta)
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "¡Atención! Su tarjeta no registra saldo suficiente."},
                    {"clase","alert-danger"}
                };
            btn_arrendar.Visible = false;
            btn_arrendar.Enabled = false;
            return;
        }

        arriendo.cod_estacionamiento = Int32.Parse(txt_estacionamiento_id.Text);
        arriendo.cod_vehiculo = Int32.Parse(dpd_vehiculo.SelectedValue);
        arriendo.horas_usadas = Int32.Parse(txt_horas_usadas.Text);

        string horaInicio = this.normalizeTimeFormat(dpd_hora_inicio.SelectedValue);
        string minutoInicio = this.normalizeTimeFormat(dpd_minuto_inicio.SelectedValue);
        string horaFin = this.normalizeTimeFormat(dpd_hora_fin.SelectedValue);
        string minutoFin = this.normalizeTimeFormat(dpd_minuto_fin.SelectedValue);

        string fecha_inicio = Request.Form[fecha_inicio_arriendo.UniqueID] + " " + horaInicio + ":" + minutoInicio + ":00";
        string fecha_fin = Request.Form[fecha_termino_arriendo.UniqueID] + " " + horaFin + ":" + minutoFin + ":00";

        arriendo.inicio_arriendo = DateTime.ParseExact(fecha_inicio, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        arriendo.fin_arriendo = DateTime.ParseExact(fecha_fin, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        int codArriendoGuardado = arriendo.guardar(arriendo);
        if (codArriendoGuardado > 0)
        {
            Estacionamiento estacionamiento = new Estacionamiento();
            estacionamiento.cod_estacionamiento = arriendo.cod_estacionamiento;
            estacionamiento.cod_estacionamiento_estado = 1;
            estacionamiento.actualizar(estacionamiento);

            Arriendo arriendoGuardado = new Arriendo().datosPagar(codArriendoGuardado);

            /** Transacción por defecto **/
            Transaccion transaccion = new Transaccion();
            transaccion.cod_arriendo = codArriendoGuardado;
            transaccion.monto = (arriendoGuardado.Estacionamiento.valor_hora * arriendoGuardado.horas_usadas);
            transaccion.numero_tarjeta_origen = arriendoGuardado.Vehiculo.Usuario.tarjeta.cod_tarjeta;
            transaccion.numero_tarjeta_destino = arriendoGuardado.Estacionamiento.Usuario.tarjeta.cod_tarjeta;
            transaccion.guardar(transaccion);

            /** Calificación por defecto, primera **/
            Calificacion calificacion = new Calificacion();
            calificacion.cod_arriendo = codArriendoGuardado;
            calificacion.cod_usuario_calificado = arriendoGuardado.Estacionamiento.Usuario.cod_usuario;
            calificacion.cod_usuario_calificador = arriendoGuardado.Vehiculo.Usuario.cod_usuario;
            calificacion.nota = 0;
            calificacion.observacion = "";
            calificacion.cod_calificacion_tipo = 1;
            calificacion.guardar(calificacion);

            /** Calificación por defecto, segunda **/
            calificacion = new Calificacion();
            calificacion.cod_arriendo = codArriendoGuardado;
            calificacion.cod_usuario_calificado = arriendoGuardado.Vehiculo.Usuario.cod_usuario;
            calificacion.cod_usuario_calificador = arriendoGuardado.Estacionamiento.Usuario.cod_usuario; 
            calificacion.nota = 0;
            calificacion.observacion = "";
            calificacion.cod_calificacion_tipo = 1;
            calificacion.guardar(calificacion);

            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Arriendo ingresado correctamente."},
                {"clase","alert-success"}
            };
            Response.Redirect(this.urlBack);
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al registrar el Arriendo."},
                {"clase","alert-danger"}
            };
        }
    }

    private string normalizeTimeFormat(string time)
    {
        if (Int32.Parse(time) < 10)
        {
            time = "0" + time;
        }
        return time;
    }
}