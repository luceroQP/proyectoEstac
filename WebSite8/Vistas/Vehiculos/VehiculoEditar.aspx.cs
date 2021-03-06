﻿using System;
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
            Vehiculo vehiculo;
            int codVehiculo = (int)Session["cod_vehiculo"];
            Session["vehiculo"] = vehiculo = new Vehiculo().buscarPorPK(codVehiculo);
            //Vehiculo vehiculo = new Vehiculo().buscarPorPK(codVehiculo);

            txt_cod_patente.Text = vehiculo.cod_vehiculo.ToString();
            txt_modelo.Text = vehiculo.modelo;
            txt_patente.Text = vehiculo.patente;
            dpl_marca.SelectedValue = vehiculo.cod_vehiculo_marca.ToString();
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
        vehiculo.cod_vehiculo = Int32.Parse(txt_cod_patente.Text);
        vehiculo.patente = txt_patente.Text;
        vehiculo.modelo = txt_modelo.Text;
        vehiculo.cod_vehiculo_marca = Int32.Parse(dpl_marca.SelectedValue);
        if (vehiculo.actualizar(vehiculo))
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Vehiculo editado correctamente."},
                {"clase","alert-success"}
            };
            Response.Redirect("~/Vistas/Vehiculos/Vehiculos.aspx");
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al editar el vehiculo."},
                {"clase","alert-danger"}
            };
        }
    }
}