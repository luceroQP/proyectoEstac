<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListadoRealizadasCliente.aspx.cs" Inherits="Vistas_Calificaciones_ListadoRealizadas" %>

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
     <asp:GridView ID="gv_calificacionesRealizadas" runat="server" OnRowCommand="gv_calificacionesRealizadas_RowCommand"  AutoGenerateColumns="False" CssClass="table table-hover table-bordered" DataKeyNames="cod_calificacion">
        <Columns>
            <asp:BoundField AccessibleHeaderText="ColCodCalificacion" DataField="cod_calificacion" HeaderText="CodArriendo" Visible="false"/>
            <asp:BoundField AccessibleHeaderText="ColEstacionamiento" DataField="Arriendo.Estacionamiento.direccion" HeaderText="Estacionamiento" />
            <asp:BoundField AccessibleHeaderText="ColVehiculo" DataField="Arriendo.Vehiculo.patente" HeaderText="Vehiculo" />
            <asp:BoundField AccessibleHeaderText="ColUsuario" DataField="Arriendo.Estacionamiento.Usuario.nombre_completo" HeaderText="Dueño Estacionamiento" />
            <asp:BoundField AccessibleHeaderText="ColHorasUsadas" DataField="Arriendo.horas_usadas" HeaderText="Horas" />
            <asp:ButtonField 
                AccessibleHeaderText="colBtnVer" 
                ButtonType="Button" 
                CommandName="Ver"
                ControlStyle-CssClass="btn btn-xs btn-primary"
                Text="Ver" />
        </Columns>
    </asp:GridView>
    <%
        if (Session["modalCalificar"] != null)
            {
                Session["modalCalificar"] = null;
                %>
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:HyperLink 
                                        class="btn close" 
                                        ID="btn_cerrar_modal" 
                                        runat="server">
                                        &times;
                                    </asp:HyperLink>
                                    <h4 class="modal-title">Datos Calificación</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-12 form-group">
                                        <asp:Label 
                                            ID="Label6" 
                                            runat="server" 
                                            Text="Tipo Calificación">
                                        </asp:Label>
                                         <asp:TextBox 
                                            ID="txt_tipo_calificacion"
                                            runat="server" 
                                             ReadOnly="true"
                                            CssClass="form-control input-sm">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <asp:Label 
                                            ID="Label4" 
                                            runat="server" 
                                            Text="Nota">
                                        </asp:Label>
                                        <asp:TextBox 
                                            ID="txt_nota"
                                            runat="server" 
                                            ReadOnly="true"
                                            CssClass="form-control input-sm">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <asp:Label 
                                            ID="Label5" 
                                            runat="server" 
                                            Text="Observacion">
                                        </asp:Label>
                                        <asp:TextBox 
                                            ID="txt_observacion"
                                            runat="server" 
                                            ReadOnly="true"
                                            TextMode="MultiLine" Rows="8"
                                            CssClass="form-control input-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:HyperLink 
                                        class="btn btn-default" 
                                        ID="btn_cancelar_modal" 
                                        runat="server">
                                        Cerrar
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                <%
            }
        %>
</asp:Content>

