using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vistas_Arriendos_Arriendos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        if (!IsPostBack)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            gv_arriendos.DataSource = new Arriendo().buscarTodos(usuario.cod_usuario);
            gv_arriendos.DataBind();
        }
    }

    protected void gv_arriendos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gv_arriendos.Rows[rowIndex];
        int codigoArriendo = (int)gv_arriendos.DataKeys[rowIndex].Value;
        Session["cod_arriendo"] = codigoArriendo;

        switch (e.CommandName)
        {
            case "Ver":
                Response.Redirect("~/Vistas/Arriendos/ArriendoVer.aspx");
                break;
        }
    }
}