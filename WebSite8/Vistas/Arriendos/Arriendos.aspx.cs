using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vistas_Arriendos_Arriendos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        if (!IsPostBack)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            gv_arriendos.DataSource = new Arriendo().buscarActivos(usuario.cod_usuario);
            gv_arriendos.DataBind();
        }
    }

    protected void gv_arriendos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gv_arriendos.Rows[rowIndex];
        int codigoArriendo = (int)gv_arriendos.DataKeys[rowIndex].Value;
        Session["cod_arriendo"] = codigoArriendo;
        Arriendo arriendo = new Arriendo();

        switch (e.CommandName)
        {
            case "Ver":
                Response.Redirect("~/Vistas/Arriendos/ArriendoVer.aspx");
                break;
            case "Pagar":
                Session["modalPagar"] = true;
                arriendo = new Arriendo().datosPagar(codigoArriendo);
                txt_cod_arriendo.Text = arriendo.cod_arriendo.ToString();
                txt_cod_tarjeta_origen.Text = arriendo.Vehiculo.Usuario.tarjeta.cod_tarjeta.ToString();
                txt_cod_tarjeta_destino.Text = arriendo.Estacionamiento.Usuario.tarjeta.cod_tarjeta.ToString();
                txt_nro_tarjeta_origen.Text = arriendo.Vehiculo.Usuario.tarjeta.numero_tarjeta.ToString();
                txt_nro_tarjeta_destino.Text = arriendo.Estacionamiento.Usuario.tarjeta.numero_tarjeta.ToString();
                txt_monto.Text = (arriendo.Estacionamiento.valor_hora * arriendo.horas_usadas).ToString();

                Session["arriendoPago"] = new Arriendo().datosPagar(codigoArriendo);
                break;
        }
    }

    protected void btn_pagar_Click(object sender, EventArgs e)
    {
        Transaccion transaccion = new Transaccion();
        transaccion.monto = Int32.Parse(txt_monto.Text);
        transaccion.cod_arriendo = Int32.Parse(txt_cod_arriendo.Text);
        transaccion.numero_tarjeta_origen = Int32.Parse(txt_cod_tarjeta_origen.Text);
        transaccion.numero_tarjeta_destino = Int32.Parse(txt_cod_tarjeta_destino.Text);

        if (transaccion.guardar(transaccion) > 0) {
            Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Pago realizado correctamente."},
                    {"clase","alert-success"}
            };
        }else{
            Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Error al registrar el Pago."},
                    {"clase","alert-danger"}
            };
        }
        Response.Redirect("~/Vistas/Arriendos/Arriendos.aspx");
    }
}