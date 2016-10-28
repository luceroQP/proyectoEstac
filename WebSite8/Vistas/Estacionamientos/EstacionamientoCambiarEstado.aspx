<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EstacionamientoCambiarEstado.aspx.cs" Inherits="EstacionamientoCambiarEstado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%
        Estacionamiento estacionamiento = (Estacionamiento)Session["estacionamiento"];
    %>
    <div class="col-md-4"></div>
    <div class="col-md-8">
        <div class="col-md-12 text-left">
            <h4 class="form-signin-heading">Cambiar Estado Estacionamiento</h4>
        </div>
        <div id="divCodigoEstacionamiento" runat="server" visible="false">
            <asp:TextBox ID="txt_cod_estacionamiento" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label1" runat="server" Text="Dirección"></asp:Label>
            </div>
            <div class="col-md-6">
                <% Response.Write(estacionamiento.direccion); %>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label3" runat="server" Text="Valor por Hora"></asp:Label>
            </div>
            <div class="col-md-6">
                <% Response.Write(estacionamiento.valor_hora); %>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label2" runat="server" Text="Capacidad"></asp:Label>
            </div>
            <div class="col-md-6">
                <% Response.Write(estacionamiento.capacidad); %>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label4" runat="server" Text="Estado"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_estado"
                    CssClass="form-control input-sm" 
                    runat="server" 
                    AutoPostBack="true"
                    OnSelectedIndexChanged="dpd_estado_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div id="divInicioDisponibilidad" runat="server" visible="false" class="col-md-12 form-group row">
            <div class="col-md-6 row">
                <div class="col-md-12">
                    <asp:Label ID="Label5" runat="server" Text="Inicio Disponibilidad"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList 
                        ID="dpd_hora_inicio"
                        CssClass="form-control input-sm" 
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList 
                        ID="dpd_minuto_inicio"
                        CssClass="form-control input-sm" 
                        runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div id="divFinDisponibilidad" runat="server" visible="false" class="col-md-12 form-group row">
            <div class="col-md-6 row">
                <div class="col-md-12">
                    <asp:Label ID="Label6" runat="server" Text="Fin Disponibilidad"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList 
                        ID="dpd_hora_fin"
                        CssClass="form-control input-sm" 
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList 
                        ID="dpd_minuto_fin"
                        CssClass="form-control input-sm" 
                        runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div class="col-md-6 form-group">
            <asp:Button
                ID="Button1" 
                runat="server" 
                Text="Modificar" 
                CssClass="btn btn-sm btn-primary btn-block" OnClick="Button1_Click"
                />
        </div>
    </div>
</asp:Content>

