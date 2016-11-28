using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vistas_Arriendos_HistorialArriendos : System.Web.UI.Page
{
    protected string urlBack = "~/Vistas/Arriendos/HistorialArriendos.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }

        if (!IsPostBack)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            gv_arriendosHistorial.DataSource = new Arriendo().buscarHistorial(usuario.cod_usuario);
            gv_arriendosHistorial.DataBind();
        }
    }

    protected void gv_arriendosHistorial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gv_arriendosHistorial.Rows[rowIndex];
        int codigoArriendo = (int)gv_arriendosHistorial.DataKeys[rowIndex].Value;
        Session["cod_arriendo"] = codigoArriendo;
        Arriendo arriendo = new Arriendo();

        switch (e.CommandName)
        {
            case "Ver":
                Response.Redirect("~/Vistas/Arriendos/ArriendoVer.aspx");
                break;
        }
    }
}