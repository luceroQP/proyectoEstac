<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Vehiculos.aspx.cs" Inherits="Vehiculos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Vistas/Vehiculos/VehiculoAgregar.aspx">Agregar Vehículos</asp:HyperLink>
    <asp:GridView ID="gv_vehiculos" runat="server" OnRowCommand="gv_vehiculos_RowCommand"  AutoGenerateColumns="False" CssClass="table table-hover table-bordered" DataKeyNames="cod_vehiculo">
        <Columns>
            <asp:BoundField AccessibleHeaderText="ColCodVehiculo" DataField="cod_vehiculo" HeaderText="CodVehiculo" Visible="false"/>
            <asp:BoundField AccessibleHeaderText="ColPatente" DataField="patente" HeaderText="Patente" />
            <asp:BoundField AccessibleHeaderText="ColModelo" DataField="modelo" HeaderText="Modelo" />
            <asp:ButtonField 
                AccessibleHeaderText="ColBtnEditar" 
                ButtonType="Button" 
                Text="Editar"
                CommandName="Editar" 
                ControlStyle-CssClass="btn btn-xs btn-primary"
            />
            <asp:ButtonField 
                AccessibleHeaderText="ColBtnEliminar" 
                ButtonType="Button" 
                Text="Eliminar"
                CommandName="Eliminar" 
                ControlStyle-CssClass="btn btn-xs btn-danger"
            />
        </Columns>
    </asp:GridView>
</asp:Content>

