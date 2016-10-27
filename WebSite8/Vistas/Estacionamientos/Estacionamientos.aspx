<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Estacionamientos.aspx.cs" Inherits="Estacionamientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Vistas/Estacionamientos/EstacionamientoAgregar.aspx">Agregar Estacionamiento</asp:HyperLink>
    <asp:GridView ID="gv_estacionamientos" runat="server" AutoGenerateColumns="False" OnRowCommand="gv_estacionamientos_RowCommand" CssClass="table table-hover table-bordered" DataKeyNames="cod_estacionamiento">
        <Columns>
            <asp:BoundField AccessibleHeaderText="ColCod" DataField="cod_estacionamiento" HeaderText="Codigo" Visible="false" />
            <asp:BoundField AccessibleHeaderText="ColDireccion" DataField="direccion" HeaderText="Direccion" />
            <asp:BoundField AccessibleHeaderText="ColValorHora" DataField="valor_hora" HeaderText="Valor Hora" />
            <asp:BoundField AccessibleHeaderText="ColCapacidad" DataField="capacidad" HeaderText="Capacidad" />
            <asp:BoundField AccessibleHeaderText="ColExistencias" DataField="existencias" HeaderText="Existencias" />
            <asp:BoundField AccessibleHeaderText="ColEstado" DataField="EstacionamientoEstado.nombre_estacionamiento_estado" HeaderText="Estado" />
            <asp:ButtonField 
                AccessibleHeaderText="ColBtnCambiarEstado" 
                ButtonType="Button" 
                CommandName="CambiarEstado" 
                Text="Cambiar Estado"
                ControlStyle-CssClass="btn btn-xs btn-success"
            />
            <asp:ButtonField 
                AccessibleHeaderText="ColBtnEditar" 
                ButtonType="Button" 
                CommandName="Editar" 
                Text="Editar" 
                ControlStyle-CssClass="btn btn-xs btn-primary"
            />
            <asp:ButtonField 
                AccessibleHeaderText="ColBtnEliminar" 
                ButtonType="Button" 
                CommandName="Eliminar" 
                Text="Eliminar" 
                ControlStyle-CssClass="btn btn-xs btn-danger"
            />
        </Columns>
    </asp:GridView>
</asp:Content>

