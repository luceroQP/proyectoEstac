<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HistorialArriendos.aspx.cs" Inherits="Vistas_Arriendos_HistorialArriendos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        $(document).on("ready", function () {
            $('#myModal').modal()
            $('#myModal').modal({ backdrop: 'static', keyboard: false })
            $('#myModal').modal('show')
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gv_arriendosHistorial" runat="server" OnRowCommand="gv_arriendosHistorial_RowCommand"  AutoGenerateColumns="False" CssClass="table table-hover table-bordered" DataKeyNames="cod_arriendo">
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

