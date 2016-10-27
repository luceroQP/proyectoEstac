using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class TarjetaAgregar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }

        if (!IsPostBack)
        {
            llenarBancos();
            llenarTipoTarjeta();
        }
    }

    private void llenarBancos()
    {
        dpd_banco.DataSource = new Banco().buscarTodos(true);
        dpd_banco.DataTextField = "NOMBRE_BANCO";
        dpd_banco.DataValueField = "COD_BANCO";
        dpd_banco.DataBind();
    }

    private void llenarTipoTarjeta()
    {
        dpd_tipo_tarjeta.DataSource = new TarjetaTipo().buscarTodos(true);
        dpd_tipo_tarjeta.DataTextField = "NOMBRE_TARJETA_TIPO";
        dpd_tipo_tarjeta.DataValueField = "COD_TARJETA_TIPO";
        dpd_tipo_tarjeta.DataBind();
    }

    public Boolean validarTarjeta(){
        Boolean validado = false;

        validado = true;
        return validado;
    } 
    
    protected void btn_agregar_Click(object sender, EventArgs e)
    {
        if (validarTarjeta())
        {
            Tarjeta tarjeta = new Tarjeta();
            Usuario usuario = (Usuario)Session["usuario"];

            tarjeta.numero_tarjeta = txt_nro_tarjeta.Text;
            tarjeta.saldo = 100000;
            tarjeta.cod_banco = Int32.Parse(dpd_banco.SelectedValue);
            tarjeta.cod_tarjeta_tipo = Int32.Parse(dpd_tipo_tarjeta.SelectedValue);
            tarjeta.cod_usuario = usuario.cod_usuario;

            if (tarjeta.guardar(tarjeta) > -1)
            {
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Tarjeta ingresado correctamente."},
                    {"clase","alert-success"}
                };
                //Response.Write("<script language='javascript'>window.alert('Tarjeta ingresado correctamente.');</script>");
                Response.Redirect("~/Vistas/Tarjetas/Tarjetas.aspx");
            }
            else
            {
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Error al registrar el Tarjeta."},
                    {"clase","alert-danger"}
                };
                //Response.Write("<script language='javascript'>window.alert('Error al registrar el Tarjeta.');</script>");
            }
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al registrar el Tarjeta."},
                {"clase","alert-danger"}
            };
            //Response.Write("<script language='javascript'>window.alert('Error al registrar el Tarjeta.');</script>");
        }
    }
}