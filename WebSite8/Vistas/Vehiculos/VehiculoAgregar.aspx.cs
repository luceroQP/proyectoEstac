using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class VehiculoAgregar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        if (!IsPostBack)
        {
            this.llenarComboMarca();
        }
    }

    private void llenarComboMarca()
    {
        dpl_marca.DataSource = new VehiculoMarca().buscarTodos(true);
        dpl_marca.DataTextField = "nombre_vehiculo_marca";
        dpl_marca.DataValueField = "cod_vehiculo_marca";
        dpl_marca.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Usuario usuarioLogeado = (Usuario)Session["usuario"];
        Vehiculo vehiculo = new Vehiculo();
        vehiculo.modelo = txt_modelo.Text;
        vehiculo.patente = txt_patente.Text;
        vehiculo.cod_vehiculo_marca = Int32.Parse(dpl_marca.SelectedValue);
        vehiculo.cod_usuario = usuarioLogeado.cod_usuario;
        vehiculo.estado = 1;

        if (vehiculo.guardar(vehiculo) > 0)
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Vehiculo registrado correctamente."},
                {"clase","alert-success"}
            };
            //Response.Write("<script language='javascript'>window.alert('Vehiculo ingresado correctamente.');</script>");
            Response.Redirect("~/Vistas/Vehiculos/Vehiculos.aspx");
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al registrar el Vehiculo."},
                {"clase","alert-danger"}
            };
            //Response.Write("<script language='javascript'>window.alert('Error al registrar el vehiculo.');</script>");
        }
    }
}