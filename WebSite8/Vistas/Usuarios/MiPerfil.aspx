<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MiPerfil.aspx.cs" Inherits="Vistas_Usuarios_MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="col-md-12 text-left">
        <h4 class="form-signin-heading">Registro de Usuarios</h4>
        <hr/>
    </div>
    <div class="col-md-4">
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label2" runat="server" Text="Rut"></asp:Label>
            <asp:TextBox 
                ID="txt_rut"
                runat="server" 
                CssClass="form-control input-sm">
            </asp:TextBox>
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" 
                    runat="server" 
                    Display="Dynamic"
                    ControlToValidate="txt_rut"
                    CssClass="error-message" 
                    ErrorMessage="Debe ingresar rut">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator1"
                    Display="Dynamic"
                    runat="server" 
                    ControlToValidate="txt_rut" 
                    CssClass="error-message" 
                    ErrorMessage="Ingrese rut válido" 
                    ValidationExpression="[0-9]{1,2}\.?[0-9]{3}\.?[0-9]{3}\-[0-9kK]{1}">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label3" runat="server" Text="Nombres"></asp:Label>
            <asp:TextBox 
                ID="txt_nombres" 
                runat="server" 
                CssClass="form-control input-sm">
            </asp:TextBox>
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator2" 
                    runat="server" 
                    Display="Dynamic" 
                    CssClass="error-message" 
                    ControlToValidate="txt_nombres" 
                    ErrorMessage="Ingrese Nombres">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label4" runat="server" Text="Apellido Paterno"></asp:Label>
            <asp:TextBox 
                ID="txt_apellido_pat" 
                runat="server" 
                CssClass="form-control input-sm">
            </asp:TextBox>
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator3" 
                    runat="server" 
                    Display="Dynamic" 
                    CssClass="error-message" 
                    ControlToValidate="txt_apellido_pat" 
                    ErrorMessage="Debe ingresar Apellido">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label5" runat="server" Text="Apellido Materno"></asp:Label>
            <asp:TextBox 
                ID="txt_apellido_mat" 
                runat="server" 
                CssClass="form-control input-sm">
            </asp:TextBox>
        </div>
    </div>
    <div class="col-md-4">
         <div class="col-md-12 row form-group">
            <asp:Label ID="Label7" runat="server" Text="Sexo"></asp:Label>
            <asp:DropDownList 
                ID="dpl_sexo" 
                runat="server" 
                CssClass="form-control input-sm">
                <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Femenino</asp:ListItem>
            </asp:DropDownList>
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator5" 
                    runat="server" 
                    Display="Dynamic" 
                    InitialValue="0"
                    CssClass="error-message" 
                    ControlToValidate="dpl_sexo" 
                    ErrorMessage="Debe seleccionar sexo">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label6" runat="server" Text="Fecha Nacimiento"></asp:Label>
            <asp:TextBox 
                ID="txt_calendar" 
                runat="server" 
                ReadOnly="true"
                CssClass="form-control input-sm calendario">
            </asp:TextBox>
            <div class="col-md-12 row">
                 <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator14" 
                    runat="server" 
                    Display="Dynamic" 
                    CssClass="error-message" 
                    ControlToValidate="txt_calendar" 
                    ErrorMessage="Ingrese fecha nacimiento">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label8" runat="server" Text="Telefono"></asp:Label>
            <asp:TextBox 
                ID="txt_telefono" 
                runat="server" 
                CssClass="form-control input-sm">
            </asp:TextBox>
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator6" 
                    runat="server" 
                    CssClass="error-message" 
                    Display="Dynamic" 
                    ControlToValidate="txt_telefono" 
                    ErrorMessage="Debe ingresar telefono">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
            <asp:TextBox 
                ID="txt_email" 
                runat="server" 
                CssClass="form-control input-sm">
            </asp:TextBox>
        </div>
    </div>
    <div class="col-md-4">
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label13" runat="server" Text="Direccion"></asp:Label>
            <asp:TextBox 
                ID="txt_direccion" 
                runat="server" 
                CssClass="form-control input-sm">
            </asp:TextBox>
        </div>
        <div class="col-md-12 row form-group">
            <asp:Label ID="lbl_region" runat="server" Text="Region"></asp:Label>
            <asp:DropDownList 
                ID="dpd_region" 
                runat="server" 
                CssClass="form-control input-sm dpdRegion" 
                AutoPostBack="True"
                OnSelectedIndexChanged="dpd_region_SelectedIndexChanged">
            </asp:DropDownList>
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator9" 
                    runat="server" 
                    Display="Dynamic" 
                    InitialValue="0"
                    CssClass="error-message" 
                    ControlToValidate="dpd_region" 
                    ErrorMessage="Debe seleccionar Region">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div id="divProvincia" runat="server" class="col-md-12 row form-group">
            <asp:Label ID="lbl_provincia" runat="server" Text="Provincia"></asp:Label>
            <asp:DropDownList 
                ID="dpd_provincia" 
                runat="server" 
                CssClass="form-control input-sm dpdProvincia" 
                AutoPostBack="True"
                OnSelectedIndexChanged="dpd_provincia_SelectedIndexChanged">
            </asp:DropDownList>
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator10" 
                    runat="server" 
                    Display="Dynamic" 
                    CssClass="error-message" 
                    InitialValue="0"
                    ControlToValidate="dpd_provincia" 
                    ErrorMessage="Debe seleccionar Provincia">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div id="divComuna" runat="server" class="col-md-12 row form-group">
            <asp:Label ID="lbl_comuna" runat="server" Text="Comuna"></asp:Label>
            <asp:DropDownList 
                ID="dpd_comuna" 
                runat="server"
                CssClass="form-control input-sm dpdComuna">
            </asp:DropDownList>                
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator8" 
                    runat="server" 
                    Display="Dynamic" 
                    InitialValue="0"
                    CssClass="error-message" 
                    ControlToValidate="dpd_comuna" 
                    ErrorMessage="Debe seleccionar comuna">
                </asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="col-md-12 text-left">
        <h4 class="form-signin-heading">Nueva Contraseña</h4>
        <hr/>
    </div>
    <div class="col-md-12 row form-group">
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Contraseña"></asp:Label>
            <asp:TextBox 
                ID="txt_password_tmp" 
                runat="server" 
                CssClass="form-control input-sm">
            </asp:TextBox>
        </div>
    </div>
    <div class="col-md-12 row">
        <hr />
        <div class="col-md-2">
            <asp:Button
                ID="Button1" 
                runat="server" 
                Text="Guardar" 
                CssClass="btn btn-sm btn-primary btn-block"
                OnClick="Button1_Click" />
        </div>
        <div class="col-md-2">
            <asp:HyperLink
                ID="btn_cancelar" 
                runat="server" 
                Text="Cancelar" 
                class="btn btn-sm btn-default">
            </asp:HyperLink>
        </div>
    </div>
</asp:Content>

