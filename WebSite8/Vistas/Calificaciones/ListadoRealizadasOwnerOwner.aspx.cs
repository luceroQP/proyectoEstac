using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vistas_Calificaciones_ListadoRealizadasOwnerOwner : System.Web.UI.Page
{
    protected string urlBack = "~/Vistas/Calificaciones/ListadoRealizadasOwnerOwner.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        else
        {
            Usuario usuario = (Usuario)Session["usuario"];
            gv_calificacionesRealizadas.DataSource = new Calificacion().calificacionesRealizadas(usuario.cod_usuario, "Dueño", "Dueño");
            gv_calificacionesRealizadas.DataBind();
        }
    }

    protected void gv_calificacionesRealizadas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gv_calificacionesRealizadas.Rows[rowIndex];
        int codigoCalificacion = (int)gv_calificacionesRealizadas.DataKeys[rowIndex].Value;
        Session["cod_calificacion"] = codigoCalificacion;

        switch (e.CommandName)
        {
            case "Ver":
                Calificacion calificacion = new Calificacion().buscarPorPk(codigoCalificacion, true);
                //Arriendo arriendo = new Arriendo().datosPagar(calificacion.cod_arriendo);

                txt_nota.Text = calificacion.nota.ToString();
                txt_observacion.Text = calificacion.observacion;
                txt_tipo_calificacion.Text = calificacion.CalificacionTipo.nombre_calificacion_tipo;

                //txt_cod_calificacion.Text = codigoCalificacion.ToString();
                //txt_cod_usuario_calificador.Text = arriendo.Vehiculo.Usuario.cod_usuario.ToString();
                //txt_cod_usuario_calificado.Text = arriendo.Estacionamiento.Usuario.cod_usuario.ToString();

                btn_cerrar_modal.NavigateUrl = this.urlBack;
                btn_cancelar_modal.NavigateUrl = this.urlBack;
                Session["modalCalificar"] = true;
                break;
        }
    }
}