using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class VehiculoEditar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null){
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }

        if (!IsPostBack)
        {
            this.llenarComboMarca();
            int codVehiculo = (int)Session["cod_vehiculo"];
            Vehiculo vehiculo = new Vehiculo().buscarPorPK(codVehiculo);

            txt_cod_patente.Text = vehiculo.cod_vehiculo.ToString();
            txt_modelo.Text = vehiculo.modelo;
            txt_patente.Text = vehiculo.patente;
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
        Vehiculo vehiculo = new Vehiculo();
        vehiculo.patente = txt_cod_patente.Text;
        vehiculo.modelo = txt_modelo.Text;
        
    }
}