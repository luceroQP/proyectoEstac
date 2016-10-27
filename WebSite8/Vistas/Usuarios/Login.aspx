<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-4"></div>
    <div class="col-md-4">
        <h4 class="form-signin-heading">Por Favor, ingrese sus datos</h4>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Rut"></asp:Label>
            <asp:TextBox 
                ID="txtUsuario"
                runat="server" 
                CssClass="form-control input-sm"
                required="required">
            </asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Contraseña"></asp:Label>
            <asp:TextBox 
                ID="txtPassword" 
                runat="server" 
                CssClass="form-control input-sm" 
                TextMode="Password" 
                required="required">
            </asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" 
                CssClass="btn btn-sm btn-primary btn-block" onclick="btnLogin_Click" />
        </div>
    </div>
    <div class="col-md-4"></div>    
        
</asp:Content>

