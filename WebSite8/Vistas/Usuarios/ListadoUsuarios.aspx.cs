using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;

public partial class Vistas_Usuarios_ListadoUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/Vistas/Usuarios/Login.aspx");
        }
        gv_usuarios.DataSource = new Usuario().buscarTodos(true);
        gv_usuarios.DataBind();
    }

    protected void gv_usuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gv_usuarios.Rows[rowIndex];
        int codUsuario = (int)gv_usuarios.DataKeys[rowIndex].Value;
        Session["cod_usuario"] = codUsuario;
        Usuario usuario = new Usuario();

        switch (e.CommandName)
        {
            case "Editar":
                Response.Redirect("~/Vistas/Usuarios/EditarUsuario.aspx");
                break;
            case "Bloquear":
                usuario.cod_usuario = codUsuario;
                usuario.estado = 1;
                usuario.actualizar(usuario);
                Response.Redirect("~/Vistas/Usuarios/ListadoUsuarios.aspx");
                break;
            case "Eliminar":
                usuario.cod_usuario = codUsuario;
                usuario.nombres = "a";
                usuario.apellido_pat = "a";
                usuario.apellido_mat = "a";
                usuario.password = "a";
                usuario.actualizar(usuario);
                Response.Redirect("~/Vistas/Usuarios/ListadoUsuarios.aspx");
                break;
        }
    }
}