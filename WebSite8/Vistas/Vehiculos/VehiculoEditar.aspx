<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VehiculoEditar.aspx.cs" Inherits="VehiculoEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox 
        ID="txt_cod_patente"
        Visible="false"
        runat="server"></asp:TextBox>
    <div class="col-md-4"></div>
    <div class="col-md-8">
        <div class="col-md-12 text-left">
            <h4 class="form-signin-heading">Editar Vehiculo</h4>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label2" runat="server" Text="Patente"></asp:Label>
                <asp:TextBox 
                    ID="txt_patente"
                    runat="server" 
                    CssClass="form-control input-sm" 
                    MaxLength="6"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_patente" ErrorMessage="Ingrese Patente"></asp:RequiredFieldValidator>
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_patente" ErrorMessage="Ingrese rut válido" ValidationExpression="[0-9]{1,2}\.?[0-9]{3}\.?[0-9]{3}\-[0-9kK]{1}"></asp:RegularExpressionValidator>--%>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label3" runat="server" Text="Modelo"></asp:Label>
                <asp:TextBox 
                    ID="txt_modelo" 
                    runat="server" 
                    CssClass="form-control input-sm"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_modelo" ErrorMessage="Ingrese Modelo"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label7" runat="server" Text="Marca"></asp:Label>
                <asp:DropDownList 
                    ID="dpl_marca" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dpl_marca" ErrorMessage="Debe seleccionar marca"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-6 form-group">
            <asp:Button
                ID="Button1" 
                runat="server" 
                Text="Editar" 
                CssClass="btn btn-sm btn-primary btn-block"/>
        </div>
    </div>
</asp:Content>

