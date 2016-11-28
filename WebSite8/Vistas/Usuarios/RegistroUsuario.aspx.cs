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
            llenarTipoUsuarios();
            llenarBancos();
            llenarTipoTarjeta();
        }else{
            txt_calendar.Text = Request.Form[txt_calendar.UniqueID];
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

    private void llenarTipoUsuarios()
    {
        dpd_tipo_usuario.DataSource = new UsuarioTipo().buscarTodos(true);
        dpd_tipo_usuario.DataTextField = "nombre_usuario_tipo";
        dpd_tipo_usuario.DataValueField = "cod_usuario_tipo";
        dpd_tipo_usuario.DataBind();
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
        }else{
            lbl_comuna.Visible = false;
            dpd_comuna.Visible = false;
            divComuna.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if(this.validarUsuario()){
            if (this.validarNumeroTarjeta())
            {
                Usuario usuario = new Usuario();
                string[] tmpRut = txt_rut.Text.Split('-');
                tmpRut[0] = tmpRut[0].Replace(".", string.Empty);

                //Datos de Usuario
                usuario.rut = Int32.Parse(tmpRut[0]);
                usuario.dv = tmpRut[1];
                usuario.nombres = txt_nombres.Text;
                usuario.apellido_pat = txt_apellido_pat.Text;
                usuario.apellido_mat = txt_apellido_mat.Text;
                usuario.sexo = dpl_sexo.SelectedValue;
                usuario.telefono = txt_telefono.Text;
                usuario.email = txt_email.Text;
                usuario.password = txt_contraseña.Text;
                usuario.fecha_nacimiento = DateTime.ParseExact(Request.Form[txt_calendar.UniqueID], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                usuario.direccion = txt_direccion.Text;
                usuario.estado = 1;
                usuario.cod_comuna = Int32.Parse(dpd_comuna.SelectedValue);
                usuario.cod_usuario_tipo = Int32.Parse(dpd_tipo_usuario.SelectedValue);

                //Datos de tarjeta
                usuario.tarjeta = new Tarjeta();
                usuario.tarjeta.numero_tarjeta = txt_nro_tarjeta.Text;
                usuario.tarjeta.saldo = 100000;
                usuario.tarjeta.cod_banco = Int32.Parse(dpd_banco.SelectedValue);
                usuario.tarjeta.cod_tarjeta_tipo = Int32.Parse(dpd_tipo_tarjeta.SelectedValue);

                if (usuario.registrarUsuario(usuario))
                {
                    Session["mensaje"] = new Dictionary<string, string>() { 
                        {"texto", "Usuario registrado correctamente."},
                        {"clase","alert-success"}
                    };
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    Session["mensaje"] = new Dictionary<string, string>() { 
                        {"texto", "Error al registrar el usuario."},
                        {"clase","alert-danger"}
                    };
                }
            }else{
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Error al guardar, verifique datos de la tarjeta de crédito."},
                    {"clase","alert-danger"}
                };
            }
        }else{
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al guardar, verifique datos del usuario."},
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

    private Boolean validarNumeroTarjeta()
    {
        string numeroTarjeta = txt_nro_tarjeta.Text;
        int tipoTarjeta;

        Boolean validadada = false;
        int largoNumeroTarjeta = numeroTarjeta.Length;

        if (largoNumeroTarjeta == 13 || largoNumeroTarjeta == 14 || largoNumeroTarjeta == 15 || largoNumeroTarjeta == 16)
        {
            var valid = this.isValid(numeroTarjeta, largoNumeroTarjeta);
            if (valid)
            {
                validadada = true;
            }
            else
            {
                validadada = false;
            }
        }
        else
        {
            validadada = false;
        }

        return validadada;
    }

    private Boolean isValid(string numeroTarjeta, int largo)
    {
        Boolean validadada = false;
        Boolean esDouble = true;
        List<int> numArr = new List<int>();

        int sumTotal = 0;
        for (int i = 0; i < largo; i++)
        {
            int digit = Int32.Parse(numeroTarjeta[i].ToString());
            if (esDouble)
            {
                digit = digit * 2;
                digit = this.toSingle(digit);
                esDouble = false;
            }
            else
            {
                esDouble = true;
            }
            numArr.Add(digit);
        }

        for (int i = 0; i < numArr.Count; i++)
        {
            sumTotal += numArr[i];
        }

        double diff = sumTotal % 10;
        if (diff.Equals(0))
        {
            validadada = true;
        }

        return validadada;
    }

    private int toSingle(Int32 digit)
    {
        if (digit > 9)
        {
            string tmp = digit.ToString();
            var d1 = Int32.Parse(tmp[0].ToString());
            var d2 = Int32.Parse(tmp[1].ToString());
            return (d1 + d2);
        }
        else
        {
            return digit;
        }
    }
}