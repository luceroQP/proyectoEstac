using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
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
    
    protected void btn_agregar_Click(object sender, EventArgs e)
    {
        if (validarNumeroTarjeta())
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
                Response.Redirect("~/Vistas/Tarjetas/Tarjetas.aspx");
            }
            else
            {
                Session["mensaje"] = new Dictionary<string, string>() { 
                    {"texto", "Error al registrar el Tarjeta."},
                    {"clase","alert-danger"}
                };
            }
        }
        else
        {
            Session["mensaje"] = new Dictionary<string, string>() { 
                {"texto", "Error al registrar el Tarjeta, número no válido."},
                {"clase","alert-danger"}
            };
        }
    }

    public static bool validarInfoTarjeta(string cardNo, string expiryDate = "", string cvv = "")
    {
        var cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");

        if (!cardCheck.IsMatch(cardNo)) // <1>check card number is valid
            return false;

        if (!string.IsNullOrEmpty(cvv))
        {
            var cvvCheck = new Regex(@"^\d{3}$");
            if (!cvvCheck.IsMatch(cvv)) // <2>check cvv is valid as "999"
                return false;
        }

        if (!string.IsNullOrEmpty(expiryDate))
        {
            var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
            var yearCheck = new Regex(@"^20[0-9]{2}$");
            var dateParts = expiryDate.Split('/'); //expiry date in from MM/yyyy            
            if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1])) // <3 - 6>
                return false; // ^ check date format is valid as "MM/yyyy"

            var year = int.Parse(dateParts[1]);
            var month = int.Parse(dateParts[0]);
            var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); //get actual expiry date
            var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

            //check expiry greater than today & within next 6 years <7, 8>>
            if ( !(cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6)))
            {
                return false;
            }
        }
        return true;
    }

    private Boolean validarNumeroTarjeta(){
        string numeroTarjeta = txt_nro_tarjeta.Text;
        int tipoTarjeta;

        Boolean validadada = false;
        int largoNumeroTarjeta = numeroTarjeta.Length;

        if(largoNumeroTarjeta == 13 || largoNumeroTarjeta == 14 || largoNumeroTarjeta == 15 || largoNumeroTarjeta == 16){
            var valid = this.isValid(numeroTarjeta, largoNumeroTarjeta);
            if(valid){
                validadada = true;
            } else {
                validadada = false;
            }
        }else{
            validadada = false;
        }

        return validadada;
    }

    private Boolean isValid(string numeroTarjeta, int largo){
        Boolean validadada = false;
        Boolean esDouble = true;
        List<int> numArr = new List<int>();

        int sumTotal = 0;
        for(int i=0; i<largo; i++){
            int digit = Int32.Parse(numeroTarjeta[i].ToString());
            if(esDouble){
                digit = digit * 2;
                digit = this.toSingle(digit);
                esDouble = false;
            } else {
                esDouble = true;
            }
            numArr.Add(digit);
        }

        for(int i=0; i<numArr.Count; i++){
            sumTotal += numArr[i];
        }

        double diff = sumTotal % 10;
        if (diff.Equals(0)){
            validadada = true;
        }

        return validadada;
    }

    private int toSingle(Int32 digit){
        if(digit > 9){
            string tmp = digit.ToString();
            var d1 = Int32.Parse(tmp[0].ToString());
            var d2 = Int32.Parse(tmp[1].ToString());
            return (d1 + d2); 
        } else {
            return digit;
        }
    }
}