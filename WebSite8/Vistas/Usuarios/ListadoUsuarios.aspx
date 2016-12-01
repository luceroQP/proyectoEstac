<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListadoUsuarios.aspx.cs" Inherits="Vistas_Usuarios_ListadoUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        $(document).ready(function (){
            $("body").on("click", ".btnEliminar", function (e) {
                if (!confirm("¿Está seguro de eliminar?")) {
                    return false;
                }
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gv_usuarios" runat="server" OnRowCommand="gv_usuarios_RowCommand"  AutoGenerateColumns="False" CssClass="table table-hover table-bordered" DataKeyNames="cod_usuario">
        <Columns>
            <asp:BoundField AccessibleHeaderText="ColCodUsuario" DataField="cod_usuario" HeaderText="CodUsuario" Visible="false"/>
            <asp:BoundField AccessibleHeaderText="ColNombre" DataField="nombre_completo" HeaderText="Nombre" />
            <asp:BoundField AccessibleHeaderText="ColRut" DataField="rut_completo" HeaderText="Rut" />
            <asp:ButtonField 
                AccessibleHeaderText="colBtnVer" 
                ButtonType="Button" 
                CommandName="Editar"
                ControlStyle-CssClass="btn btn-xs btn-primary"
                Text="Editar"/>
             <asp:ButtonField 
                AccessibleHeaderText="colBtnVer" 
                ButtonType="Button" 
                CommandName="Eliminar"
                ControlStyle-CssClass="btn btn-xs btn-danger btnEliminar"
                Text="Eliminar"/>
        </Columns>
    </asp:GridView>
</asp:Content>

