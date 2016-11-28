<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tarjetas.aspx.cs" Inherits="Tarjetas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gv_tarjetas" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-bordered">
        <Columns>
            <asp:BoundField AccessibleHeaderText="ColNroTarjeta" DataField="numero_tarjeta" HeaderText="Número Tarjeta" />
            <asp:ButtonField 
                AccessibleHeaderText="ColEditar" 
                ButtonType="Button" 
                Text="Editar" 
                ControlStyle-CssClass="btn btn-xs btn-primary"
            />
        </Columns>
    </asp:GridView>
    
</asp:Content>

