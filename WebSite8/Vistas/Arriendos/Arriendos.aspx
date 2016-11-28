<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Arriendos.aspx.cs" Inherits="Vistas_Arriendos_Arriendos" %>

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
    <%
        if (Session["modalPagar"] != null)
        {
            Arriendo arriendo = (Arriendo)Session["arriendoPago"];
            Session["modalPagar"] = null;
            %>
                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:HyperLink 
                                    class="btn close" 
                                    ID="HyperLink5" 
                                    runat="server" 
                                    NavigateUrl="~/Vistas/Arriendos/Arriendos.aspx">
                                    &times;
                                </asp:HyperLink>
                                <h4 class="modal-title">Realizar Pago</h4>
                            </div>
                            <div class="modal-body">
                                <asp:TextBox 
                                    ID="txt_cod_arriendo"
                                    runat="server" 
                                    Visible="false">
                                </asp:TextBox>
                                 <asp:TextBox 
                                    ID="txt_cod_tarjeta_origen"
                                    runat="server" 
                                    Visible="false">
                                </asp:TextBox>
                                 <asp:TextBox 
                                    ID="txt_cod_tarjeta_destino"
                                    runat="server" 
                                    Visible="false">
                                </asp:TextBox>
                                <div class="col-md-12 form-group">
                                    <asp:Label 
                                        ID="Label2" 
                                        runat="server" 
                                        Text="Numero Tarjeta">
                                    </asp:Label>
                                    <asp:TextBox 
                                        ID="txt_nro_tarjeta_origen"
                                        runat="server" 
                                        ReadOnly="true"
                                        CssClass="form-control input-sm">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-12 form-group">
                                    <asp:Label 
                                        ID="Label3" 
                                        runat="server" 
                                        Text="Transferir a">
                                    </asp:Label>
                                    <asp:TextBox 
                                        ID="txt_nro_tarjeta_destino"
                                        runat="server" 
                                        ReadOnly="true"
                                        CssClass="form-control input-sm">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-12 form-group">
                                    <asp:Label 
                                        ID="Label1" 
                                        runat="server" 
                                        Text="Monto">
                                    </asp:Label>
                                    <asp:TextBox 
                                        ID="txt_monto"
                                        runat="server" 
                                        ReadOnly="true"
                                        CssClass="form-control input-sm">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button
                                    ID="btn_pagar" 
                                    runat="server" 
                                    Text="Pagar" 
                                    CssClass="btn btn-primary"
                                    OnClick="btn_pagar_Click"/>
                                <asp:HyperLink 
                                    class="btn btn-default" 
                                    ID="HyperLink3" 
                                    runat="server" 
                                    NavigateUrl="~/Vistas/Arriendos/Arriendos.aspx">
                                    Cancelar
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            <%
        }
    %>
</asp:Content>

