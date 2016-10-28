using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Acceder(object sender, EventArgs e)
    {
        
        Response.Redirect("./Vistas/Usuarios/Login.aspx");
        
    }
}