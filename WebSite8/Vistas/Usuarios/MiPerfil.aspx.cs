using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vistas_Usuarios_MiPerfil : System.Web.UI.Page
{
    private string urlBack = "~/Default.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        btn_cancelar.NavigateUrl = this.urlBack;
        if (!IsPostBack)
        {
            Usuario usuarioSession = (Usuario)Session["usuario"];
            Usuario usuarioEditado = new Usuario().buscarPorPK(usuarioSession.cod_usuario, true);
            llenarRegiones();
            llenarProvincias(usuarioEditado.Comuna.Provincia.cod_region);
            llenarComunas(usuarioEditado.Comuna.cod_provincia);

            dpd_region.SelectedValue = usuarioEditado.Comuna.Provincia.cod_region.ToString();
            dpd_provincia.SelectedValue = usuarioEditado.Comuna.cod_provincia.ToString();
            dpd_comuna.SelectedValue = usuarioEditado.cod_comuna.ToString();

            txt_rut.Text = usuarioEditado.rut_completo;
            txt_nombres.Text = usuarioEditado.nombres;
            txt_apellido_pat.Text = usuarioEditado.apellido_pat;
            txt_apellido_mat.Text = usuarioEditado.apellido_mat;
            txt_telefono.Text = usuarioEditado.telefono;
            txt_email.Text = usuarioEditado.email;
            txt_direccion.Text = usuarioEditado.direccion;
            dpl_sexo.SelectedValue = usuarioEditado.sexo;
            txt_calendar.Text = usuarioEditado.fecha_nacimiento.ToString("dd/MM/yyyy");
        }
        else
        {
            txt_calendar.Text = Request.Form[txt_calendar.UniqueID];
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
        lbl_comuna.Visible = false;
        dpd_comuna.Visible = false;
        divComuna.Visible = false;
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
        }
        else
        {
            lbl_comuna.Visible = false;
            dpd_comuna.Visible = false;
            divComuna.Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.validarUsuario())
        {
            Usuario usuarioSession = (Usuario)Session["usuario"];
            Usuario usuario = new Usuario();
            string[] tmpRut = txt_rut.Text.Split('-');
            tmpRut[0] = tmpRut[0].Replace(".", string.Empty);

            usuario.cod_usuario = usuarioSession.cod_usuario;
            usuario.rut = Int32.Parse(tmpRut[0]);
            usuario.dv = tmpRut[1];
            usuario.nombres = txt_nombres.Text;
            usuario.apellido_pat = txt_apellido_pat.Text;
            usuario.apellido_mat = txt_apellido_mat.Text;
            usuario.sexo = dpl_sexo.SelectedValue;
            usuario.telefono = txt_telefono.Text;
            usuario.email = txt_email.Text;
            usuario.fecha_nacimiento = DateTime.ParseExact(Request.Form[txt_calendar.UniqueID], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            usuario.direccion = txt_direccion.Text;
            usuario.cod_comuna = Int32.Parse(dpd_comuna.SelectedValue);

            if (!txt_password_tmp.Text.Equals(""))
            {
                usuario.password = txt_password_tmp.Text;
            }

            if (usuario.actualizar(usuario))
            {
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Datos editados correctamente."},
                    {"clase","alert-success"}
                };
                Session["usuario"] = new Usuario().buscarPorPK(usuarioSession.cod_usuario);
                Response.Redirect(urlBack);
            }
            else
            {
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Error al editar el datos."},
                    {"clase","alert-danger"}
                };
            }
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al editar, verifique datos del usuario."},
                {"clase","alert-danger"}
            };
        }
    }

    private bool validarUsuario()
    {
        return true;
    }

    protected void dpd_region_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regionSeleccionado = Int32.Parse(dpd_region.SelectedValue);
        Session["autoFocus"] = "dpdProvincia";
        llenarProvincias(regionSeleccionado);
    }
    protected void dpd_provincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        int provinciaSeleccionada = Int32.Parse(dpd_provincia.SelectedValue);
        Session["autoFocus"] = "dpdComuna";
        llenarComunas(provinciaSeleccionada);
    }
}