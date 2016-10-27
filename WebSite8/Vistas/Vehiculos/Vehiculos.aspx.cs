using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vehiculos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }

        if (!IsPostBack)
        {
            gv_vehiculos.DataSource = new Vehiculo().buscarTodos();
            gv_vehiculos.DataBind();
        }
    }

    protected void gv_vehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gv_vehiculos.Rows[rowIndex];
        int codigoVehiculo = (int)gv_vehiculos.DataKeys[rowIndex].Value;
        Session["cod_vehiculo"] = codigoVehiculo;

        switch (e.CommandName)
        {
            case "Editar":
                Response.Redirect("~/Vistas/Vehiculos/VehiculoEditar.aspx");
                break;
            case "Eliminar":
                //Response.Redirect("~/Vistas/Estacionamientos/EstacionamientoEliminar.aspx");
                break;
        }
    }
}