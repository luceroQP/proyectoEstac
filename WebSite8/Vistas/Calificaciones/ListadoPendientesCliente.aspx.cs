using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vistas_Calificaciones_ListadoPendientes : System.Web.UI.Page
{
    protected string urlBack = "~/Vistas/Calificaciones/ListadoPendientesCliente.aspx";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        else
        {
            Usuario usuario = (Usuario)Session["usuario"];
            gv_calificacionesPendientes.DataSource = new Calificacion().calificacionesPendientes(usuario.cod_usuario, "Cliente", "Dueño");
            gv_calificacionesPendientes.DataBind();
        }
    }

    protected void gv_calificacionesPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gv_calificacionesPendientes.Rows[rowIndex];
        int codigoCalificacion = (int)gv_calificacionesPendientes.DataKeys[rowIndex].Value;
        Session["cod_calificacion"] = codigoCalificacion;

        switch (e.CommandName)
        {
            case "Calificar":
                Calificacion calificacion = new Calificacion().buscarPorPk(codigoCalificacion);
                Arriendo arriendo = new Arriendo().datosPagar(calificacion.cod_arriendo);

                txt_cod_calificacion.Text = codigoCalificacion.ToString();
                txt_cod_usuario_calificador.Text = arriendo.Vehiculo.Usuario.cod_usuario.ToString();
                txt_cod_usuario_calificado.Text = arriendo.Estacionamiento.Usuario.cod_usuario.ToString();
                this.llenaTipoCalificacion();

                btn_cerrar_modal.NavigateUrl = this.urlBack;
                btn_cancelar_modal.NavigateUrl = this.urlBack;
                Session["modalCalificar"] = true;
                break;
        }
    }

    private void llenaTipoCalificacion()
    {
        dpl_calificacion_tipo.DataSource = new CalificacionesTipo().buscarTodos(true);
        dpl_calificacion_tipo.DataTextField = "NOMBRE_CALIFICACION_TIPO";
        dpl_calificacion_tipo.DataValueField = "COD_CALIFICACION_TIPO";
        dpl_calificacion_tipo.DataBind();
    }

    protected void btn_calificar_Click(object sender, EventArgs e)
    {
        Calificacion calificacion = new Calificacion();
        calificacion.cod_calificacion = Int32.Parse(txt_cod_calificacion.Text);
        calificacion.nota = Int32.Parse(dpl_nota.SelectedValue);
        calificacion.observacion = txt_observacion.Text;
        calificacion.cod_calificacion_tipo = Int32.Parse(dpl_calificacion_tipo.SelectedValue);

        if (calificacion.actualizar(calificacion))
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Calificación realizada correctamente."},
                    {"clase","alert-success"}
            };
        }else{
            Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Error al registrar la Calificación."},
                    {"clase","alert-danger"}
            };
        }
        Response.Redirect(this.urlBack);
    }
}