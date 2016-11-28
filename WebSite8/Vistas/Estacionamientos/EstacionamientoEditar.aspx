<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EstacionamientoEditar.aspx.cs" Inherits="EstacionamientoEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%
        Estacionamiento estacionamientoSession = (Estacionamiento)Session["estacionamiento"];
    %>
    <link rel="stylesheet" href="/css/google_maps.css"/>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDC3mh16ySlzaS1hVXfzyrRE33l3UbcqfU&libraries=places&callback=initAutocomplete" defer></script>

    <script>
        function initAutocomplete() {
            var map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: <%=estacionamientoSession.latitud.ToString().Replace(",", ".")%>, lng: <%=estacionamientoSession.longitud.ToString().Replace(",", ".")%>},
                zoom: 15,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            var input = document.getElementById('txt_direccion_map');
            var searchBox = new google.maps.places.SearchBox(input);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

            map.addListener('bounds_changed', function () {
                searchBox.setBounds(map.getBounds());
            });

            var markerDefault = new google.maps.Marker({
                position: { lat: <%=estacionamientoSession.latitud.ToString().Replace(",", ".")%>, lng: <%=estacionamientoSession.longitud.ToString().Replace(",", ".")%>},
            });
            markerDefault.setMap(map);

            var markers = [];
            searchBox.addListener('places_changed', function () {
                var places = searchBox.getPlaces();

                if (places.length == 0) {
                    return;
                }
                markers.forEach(function (marker) {
                    marker.setMap(null);
                });
                markerDefault.setMap(null);
                markers = [];
                var bounds = new google.maps.LatLngBounds();
                places.forEach(function (place) {
                    var txtBuscador = document.getElementById('txt_direccion_map');
                    var txtDirecciones = document.getElementsByClassName("direccion");
                    var txtLatitud = document.getElementsByClassName("latitud");
                    var txtLongitud = document.getElementsByClassName("longitud");

                    txtDirecciones[0].setAttribute("value", txtBuscador.value);
                    txtLatitud[0].setAttribute("value", place.geometry.location.lat());
                    txtLongitud[0].setAttribute("value", place.geometry.location.lng());

                    var icon = {
                        url: place.icon,
                        size: new google.maps.Size(71, 71),
                        origin: new google.maps.Point(0, 0),
                        anchor: new google.maps.Point(17, 34),
                        scaledSize: new google.maps.Size(25, 25)
                    };
                    markers.push(new google.maps.Marker({
                        map: map,
                        icon: icon,
                        title: place.name,
                        position: place.geometry.location
                    }));

                    if (place.geometry.viewport) {
                        bounds.union(place.geometry.viewport);
                    } else {
                        bounds.extend(place.geometry.location);
                    }
                });
                map.fitBounds(bounds);
            });
        }
    </script>
    <script>
        $(document).on("ready", function(){
            if($("#map").html().trim() == "<div class=\"loader-div\"><div></div></div>"){
                console.log("forzado!");
                initAutocomplete();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%
        Estacionamiento estacionamientoSession = (Estacionamiento)Session["estacionamiento"];
    %>
    <asp:TextBox 
        ID="txt_cod_estacionamiento"
        Visible="false"
        runat="server">
    </asp:TextBox>
    <div class="col-md-12 text-center">
        <h4 class="form-signin-heading">Edición de Estacionamiento</h4>
    </div>
    <div class="col-md-12 row form-group">
        <input id="txt_direccion_map" class="controls form-control input-sm" type="text" placeholder="Ingrese Dirección" value="<% Response.Write(estacionamientoSession.direccion); %>"/>
        <div id="map">
            <div class="loader-div"><div></div></div>
        </div>
        <asp:TextBox 
                ID="txt_direccion"
                runat="server"
                CssClass="direccion hide">
                </asp:TextBox>
        <asp:TextBox 
                ID="txt_latitud"
                runat="server" 
                CssClass="latitud hide">
                </asp:TextBox>
        <asp:TextBox 
                ID="txt_longitud"
                runat="server" 
                CssClass="longitud hide">
                </asp:TextBox>
    </div>
    <div class="col-md-4"></div>
    <div class="col-md-8">
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_direccion" ErrorMessage="Debe ingresar Direccion"></asp:RequiredFieldValidator>
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
                CssClass="btn btn-sm btn-primary btn-block" OnClick="Button1_Click"
                />
        </div>
    </div>
</asp:Content>

