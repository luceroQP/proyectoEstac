<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EstacionamientoEditar.aspx.cs" Inherits="EstacionamientoEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%
        Estacionamiento estacionamientoSession = (Estacionamiento)Session["estacionamiento"];
    %>
    <link rel="stylesheet" href="/css/google_maps.css"/>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDC3mh16ySlzaS1hVXfzyrRE33l3UbcqfU&callback=initMap" async defer></script>
    <script>
        function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: <% Response.Write(estacionamientoSession.latitud); %>, lng: <% Response.Write(estacionamientoSession.longitud);%>},
                zoom: 18
            });
            var infoWindow = new google.maps.InfoWindow({ map: map });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox 
        ID="txt_cod_estacionamiento"
        Visible="false"
        runat="server">
    </asp:TextBox>
    <div class="col-md-4"></div>
    <div class="col-md-8">
        <div class="col-md-12 text-left">
            <h4 class="form-signin-heading">Edición de Estacionamiento</h4>
        </div>
         <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label1" runat="server" Text="Direccion"></asp:Label>
                <div id="map"></div>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_direccion" ErrorMessage="Debe ingresar Direccion"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label2" runat="server" Text="Direccion"></asp:Label>
                <asp:TextBox 
                    ID="txt_direccion"
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_direccion" ErrorMessage="Debe ingresar Direccion"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label3" runat="server" Text="Valor por Hora"></asp:Label>
                <asp:TextBox 
                    ID="txt_valor_hora" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_valor_hora" ErrorMessage="Ingrese Valor por Hora"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-12 row">
            <div class="col-md-6 form-group">
                <asp:Label ID="Label4" runat="server" Text="Capacidad"></asp:Label>
                <asp:TextBox 
                    ID="txt_capacidad" 
                    runat="server" 
                    CssClass="form-control input-sm">
                </asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_capacidad" ErrorMessage="Debe ingresar capacidad"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-md-6 form-group">
            <asp:Button
                ID="Button1" 
                runat="server" 
                Text="Editar" 
                CssClass="btn btn-sm btn-primary btn-block"
                />
        </div>
    </div>
</asp:Content>

