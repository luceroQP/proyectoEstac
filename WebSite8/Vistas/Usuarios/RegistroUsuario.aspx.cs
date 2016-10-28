using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class RegistroUsuario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            llenarRegiones();
        }
    }

    private void llenarRegiones()
    {
        dpd_region.DataSource = new Region().buscarTodos(true);
        dpd_region.DataTextField = "nombre_region";
        dpd_region.DataValueField = "cod_region";
        dpd_region.DataBind();
    }

    private void llenarProvincias(int regionId = 0)
    {
        if (!regionId.Equals(0))
        {
            dpd_provincia.DataSource = new Provincia().buscarTodos(regionId, true);
            dpd_provincia.DataTextField = "nombre_provincia";
            dpd_provincia.DataValueField = "cod_provincia";
            dpd_provincia.DataBind();
            lbl_provincia.Visible = true;
            dpd_provincia.Visible = true;
            divProvincia.Visible = true;
        }
        else
        {
            lbl_provincia.Visible = false;
            dpd_provincia.Visible = false;
            divProvincia.Visible = false;
        }
        
    }

    private void llenarComunas(int provinciaId = 0)
    {
        if (!provinciaId.Equals(0))
        {
            dpd_comuna.DataSource = new Comuna().buscarTodos(provinciaId, true);
            dpd_comuna.DataTextField = "nombre_comuna";
            dpd_comuna.DataValueField = "cod_comuna";
            dpd_comuna.DataBind();
            lbl_comuna.Visible = true;
            dpd_comuna.Visible = true;
            divComuna.Visible = true;
        }else{
            lbl_comuna.Visible = false;
            dpd_comuna.Visible = false;
            divComuna.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Usuario usuario = new Usuario();
        if (this.validarUsuario())
        {
            string[] tmpRut = txt_rut.Text.Split('-');
            tmpRut[0] = tmpRut[0].Replace(".",string.Empty);

            usuario.rut = Int32.Parse(tmpRut[0]);
            usuario.dv = tmpRut[1];
            usuario.nombres = txt_nombres.Text;
            usuario.apellido_pat = txt_apellido_pat.Text;
            usuario.apellido_mat = txt_apellido_mat.Text;
            usuario.sexo = dpl_sexo.SelectedValue;
            usuario.telefono = txt_telefono.Text;
            usuario.email = txt_email.Text;
            usuario.password = txt_contraseña.Text;
            usuario.fecha_nacimiento = cal_fec_nac.SelectedDate;
            usuario.direccion = txt_direccion.Text;
            usuario.estado = 1;
            usuario.cod_comuna = Int32.Parse(dpd_comuna.SelectedValue);
            usuario.cod_usuario_tipo = 1;

            if (usuario.guardar(usuario) > 0)
            {
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Usuario registrado correctamente."},
                    {"clase","alert-success"}
                };
                //Response.Write("<script language='javascript'>window.alert('Usuario registrado correctamente.');</script>");
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Error al registrar el usuario."},
                    {"clase","alert-danger"}
                };
                //Response.Write("<script language='javascript'>window.alert('Error al registrar el usuario.');</script>");
            }
        }

    }

    private bool validarUsuario()
    {
        return true;
    }
    protected void dpd_region_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regionSeleccionado = Int32.Parse(dpd_region.SelectedValue);
        llenarProvincias(regionSeleccionado);
    }
    protected void dpd_provincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        int provinciaSeleccionada = Int32.Parse(dpd_provincia.SelectedValue);
        llenarComunas(provinciaSeleccionada);
    }
}