<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Arriendos.aspx.cs" Inherits="Vistas_Arriendos_Arriendos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Vistas/Arriendos/Arrendar.aspx">Agregar Arriendo</asp:HyperLink>
    <asp:GridView ID="gv_arriendos" runat="server" OnRowCommand="gv_arriendos_RowCommand"  AutoGenerateColumns="False" CssClass="table table-hover table-bordered" DataKeyNames="cod_arriendo">
        <Columns>
            <asp:BoundField AccessibleHeaderText="ColCodArriendo" DataField="cod_arriendo" HeaderText="CodArriendo" Visible="false"/>
            <asp:BoundField AccessibleHeaderText="ColPatente" DataField="Vehiculo.patente" HeaderText="Patente" />
            <asp:BoundField AccessibleHeaderText="ColDireccion" DataField="Estacionamiento.direccion" HeaderText="Direccion" />
            <asp:BoundField AccessibleHeaderText="ColHorasUsadas" DataField="horas_usadas" HeaderText="Horas" />
            <asp:ButtonField 
                AccessibleHeaderText="colBtnVer" 
                ButtonType="Button" 
                CommandName="Ver" 
                ControlStyle-CssClass="btn btn-xs btn-primary"
                Text="Ver" />
        </Columns>
    </asp:GridView>
</asp:Content>

