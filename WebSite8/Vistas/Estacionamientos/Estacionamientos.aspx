<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Estacionamientos.aspx.cs" Inherits="Estacionamientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        $("body").on("click", ".disabled", function (e) {
            return false;
        })
    </script>
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
            <asp:TemplateField>
                <ItemTemplate>
                        <asp:Button 
                            runat="server"
                            Text="Cambiar Disp."
                            CssClass="btn btn-xs btn-success disabled"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Visible='<%# mostrarBtnDeshabilitado((Arriendo)Eval("Arriendo")) %>' 
                            onClientclick="return false;"/>
                    <asp:Button 
                            runat="server"
                            Text="Cambiar Disp."
                            CssClass="btn btn-xs btn-success"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="CambiarEstado"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                        <asp:Button 
                            runat="server"
                            Text="Editar"
                            CssClass="btn btn-xs btn-primary disabled"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Visible='<%# mostrarBtnDeshabilitado((Arriendo)Eval("Arriendo")) %>' 
                            onClientclick="return false;"/>
                    <asp:Button 
                            runat="server"
                            Text="Editar"
                            CssClass="btn btn-xs btn-primary"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Editar"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                        <asp:Button 
                            runat="server"
                            Text="Eliminar"
                            CssClass="btn btn-xs btn-danger disabled"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Visible='<%# mostrarBtnDeshabilitado((Arriendo)Eval("Arriendo")) %>' 
                            onClientclick="return false;"/>
                    <asp:Button 
                            runat="server"
                            Text="Eliminar"
                            CssClass="btn btn-xs btn-danger"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Eliminar"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

