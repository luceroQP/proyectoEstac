<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListadoPendientesOwnerOwner.aspx.cs" Inherits="Vistas_Calificaciones_ListadoPendientesOwnerOwner" %>

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
     <asp:GridView ID="gv_calificacionesPendientes" runat="server" OnRowCommand="gv_calificacionesPendientes_RowCommand"  AutoGenerateColumns="False" CssClass="table table-hover table-bordered" DataKeyNames="cod_calificacion">
        <Columns>
            <asp:BoundField AccessibleHeaderText="ColCodCalificacion" DataField="cod_calificacion" HeaderText="CodArriendo" Visible="false"/>
            <asp:BoundField AccessibleHeaderText="ColEstacionamiento" DataField="Arriendo.Estacionamiento.direccion" HeaderText="Estacionamiento" />
            <asp:BoundField AccessibleHeaderText="ColVehiculo" DataField="Arriendo.Vehiculo.patente" HeaderText="Vehiculo" />
            <asp:BoundField AccessibleHeaderText="ColUsuario" DataField="Arriendo.Estacionamiento.Usuario.nombre_completo" HeaderText="Dueño Estacionamiento" />
            <asp:BoundField AccessibleHeaderText="ColHorasUsadas" DataField="Arriendo.horas_usadas" HeaderText="Horas" />
            <asp:ButtonField 
                AccessibleHeaderText="colBtnVer" 
                ButtonType="Button" 
                CommandName="Calificar"
                ControlStyle-CssClass="btn btn-xs btn-primary"
                Text="Calificar" />
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
                                    <h4 class="modal-title">Calificar Arriendo</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:TextBox 
                                        ID="txt_cod_calificacion"
                                        runat="server" 
                                        Visible="false">
                                    </asp:TextBox>
                                     <asp:TextBox 
                                        ID="txt_cod_usuario_calificador"
                                        runat="server" 
                                        Visible="false">
                                    </asp:TextBox>
                                    <asp:TextBox 
                                        ID="txt_cod_usuario_calificado"
                                        runat="server" 
                                        Visible="false">
                                    </asp:TextBox>
                                    <div class="col-md-12 form-group">
                                        <asp:Label 
                                            ID="Label6" 
                                            runat="server" 
                                            Text="Tipo Calificación">
                                        </asp:Label>
                                        <asp:DropDownList 
                                            ID="dpl_calificacion_tipo" 
                                            runat="server" 
                                            CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                        <div class="col-md-12 row">
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator1" 
                                                runat="server" 
                                                Display="Dynamic"
                                                InitialValue="0"
                                                ControlToValidate="dpl_calificacion_tipo"
                                                CssClass="error-message" 
                                                ErrorMessage="Debe seleccionar tipo calificacion">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group">
                                        <asp:Label 
                                            ID="Label4" 
                                            runat="server" 
                                            Text="Nota">
                                        </asp:Label>
                                        <asp:DropDownList 
                                            ID="dpl_nota" 
                                            runat="server" 
                                            CssClass="form-control input-sm">
                                            <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="1">2</asp:ListItem>
                                            <asp:ListItem Value="1">3</asp:ListItem>
                                            <asp:ListItem Value="1">4</asp:ListItem>
                                            <asp:ListItem Value="1">5</asp:ListItem>
                                            <asp:ListItem Value="1">6</asp:ListItem>
                                            <asp:ListItem Value="1">7</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="col-md-12 row">
                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator2" 
                                                runat="server" 
                                                Display="Dynamic"
                                                InitialValue="0"
                                                ControlToValidate="dpl_nota"
                                                CssClass="error-message" 
                                                ErrorMessage="Debe seleccionar nota">
                                            </asp:RequiredFieldValidator>
                                        </div>
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
                                            TextMode="MultiLine" Rows="8"
                                            CssClass="form-control input-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button
                                        ID="Button1" 
                                        runat="server" 
                                        Text="Calificar" 
                                        CssClass="btn btn-primary"
                                        OnClick="btn_calificar_Click"/>
                                    <asp:HyperLink 
                                        class="btn btn-default" 
                                        ID="btn_cancelar_modal" 
                                        runat="server">
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

