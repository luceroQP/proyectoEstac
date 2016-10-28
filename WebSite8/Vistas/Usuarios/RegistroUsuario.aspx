<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegistroUsuario.aspx.cs" Inherits="RegistroUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-4"></div>
    <div class="col-md-8">
        <div class="col-md-12 text-left">
            <h4 class="form-signin-heading">Registro de Usuarios</h4>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label2" runat="server" Text="Rut"></asp:Label>
                <asp:TextBox 
                    ID="txt_rut"
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_rut" ErrorMessage="Debe ingresar rut"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_rut" ErrorMessage="Ingrese rut válido" ValidationExpression="[0-9]{1,2}\.?[0-9]{3}\.?[0-9]{3}\-[0-9kK]{1}"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label3" runat="server" Text="Nombres"></asp:Label>
                <asp:TextBox 
                    ID="txt_nombres" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_nombres" ErrorMessage="Ingrese Nombres"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label4" runat="server" Text="Apellido Paterno"></asp:Label>
                <asp:TextBox 
                    ID="txt_apellido_pat" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_apellido_pat" ErrorMessage="Debe ingresar Apellido"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label5" runat="server" Text="Apellido Materno"></asp:Label>
                <asp:TextBox 
                    ID="txt_apellido_mat" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label1" runat="server" Text="Tipo Usuario"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_tipo_usuario" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:DropDownList>                
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dpl_sexo" ErrorMessage="Debe seleccionar sexo"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label6" runat="server" Text="Fecha Nacimiento"></asp:Label>
                <asp:Calendar 
                    ID="cal_fec_nac" 
                    runat="server">
                </asp:Calendar>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label7" runat="server" Text="Sexo"></asp:Label>
                <asp:DropDownList 
                    ID="dpl_sexo" 
                    runat="server" 
                    CssClass="form-control input-sm">
                    <asp:ListItem Selected="True">Seleccione</asp:ListItem>
                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                </asp:DropDownList>                
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dpl_sexo" ErrorMessage="Debe seleccionar sexo"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label8" runat="server" Text="Telefono"></asp:Label>
                <asp:TextBox 
                    ID="txt_telefono" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>                
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_telefono" ErrorMessage="Debe ingresar telefono"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                <asp:TextBox 
                    ID="txt_email" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="lbl_region" runat="server" Text="Region"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_region" 
                    runat="server" 
                    CssClass="form-control input-sm" 
                    AutoPostBack="True"
                    OnSelectedIndexChanged="dpd_region_SelectedIndexChanged">
                </asp:DropDownList>                
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div id="divProvincia" runat="server" visible="false" class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="lbl_provincia" runat="server" Text="Provincia" Visible="False"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_provincia" 
                    runat="server" 
                    CssClass="form-control input-sm" 
                    AutoPostBack="True"
                    Visible ="False"
                    OnSelectedIndexChanged="dpd_provincia_SelectedIndexChanged">
                </asp:DropDownList>                
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div id="divComuna" runat="server" visible="false" class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="lbl_comuna" runat="server" Text="Comuna" Visible="False"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_comuna" 
                    runat="server"
                    Visible ="False"
                    CssClass="form-control input-sm">
                </asp:DropDownList>                
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="dpd_comuna" ErrorMessage="Debe seleccionar comuna"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label13" runat="server" Text="Direccion"></asp:Label>
                <asp:TextBox 
                    ID="txt_direccion" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label11" runat="server" Text="Contraseña"></asp:Label>
                <asp:TextBox 
                    ID="txt_contraseña" 
                    runat="server" 
                    TextMode="Password" 
                    CssClass="form-control input-sm">
                </asp:TextBox>                
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_contraseña" ErrorMessage="Debe ingresar constraseña"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-6 form-group">
            <asp:Button
                ID="Button1" 
                runat="server" 
                Text="Registrarse" 
                CssClass="btn btn-sm btn-primary btn-block"
                OnClick="Button1_Click" />
        </div>
    </div>
</asp:Content>