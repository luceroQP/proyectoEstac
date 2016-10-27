<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TarjetaAgregar.aspx.cs" Inherits="TarjetaAgregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-4"></div>
    <div class="col-md-8">
        <div class="col-md-12 text-left">
            <h4 class="form-signin-heading">Registro de Tarjeta</h4>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label2" runat="server" Text="Numero Tarjeta:"></asp:Label>
                <asp:TextBox 
                    ID="txt_nro_tarjeta"
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nro_tarjeta" ErrorMessage="Debe ingresar Numero de Tarjeta"></asp:RequiredFieldValidator>
            </div>
        </div>
         <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="lbl_banco" runat="server" Text="Banco"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_banco" 
                    runat="server" 
                    CssClass="form-control input-sm" >
                </asp:DropDownList>                
            </div>
            <div class="col-md-6"></div>
        </div>

         <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label1" runat="server" Text="Tipo Tarjeta"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_tipo_tarjeta" 
                    runat="server" 
                    CssClass="form-control input-sm" >
                </asp:DropDownList>                
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="col-md-6 form-group">
            <asp:Button
                ID="btn_agregar" 
                runat="server" 
                Text="Ingresar" 
                CssClass="btn btn-sm btn-primary btn-block" OnClick="btn_agregar_Click" 
                />
        </div>
    </div>
</asp:Content>

