<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EstacionamientoAgregar.aspx.cs" Inherits="EstacionamientoAgregar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="/css/google_maps.css"/>


    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDC3mh16ySlzaS1hVXfzyrRE33l3UbcqfU&libraries=places&callback=initAutocomplete" async defer></script>
       <script>
           // Note: This example requires that you consent to location sharing when
           // prompted by your browser. If you see the error "The Geolocation service
           // failed.", it means you probably did not give permission for the browser to
           // locate you.

           function initAutocomplete() {
               var map = new google.maps.Map(document.getElementById('map'), {
                   center: { lat: -33.433070, lng: -70.615563 },
                   zoom: 11,
                   mapTypeId: google.maps.MapTypeId.ROADMAP
               });

               // Create the search box and link it to the UI element.
               var input = document.getElementById('txt_direccion_map');
               var searchBox = new google.maps.places.SearchBox(input);
               map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

               // Bias the SearchBox results towards current map's viewport.
               map.addListener('bounds_changed', function () {
                   searchBox.setBounds(map.getBounds());
               });

               var markers = [];
               // Listen for the event fired when the user selects a prediction and retrieve
               // more details for that place.
               searchBox.addListener('places_changed', function () {
                   var places = searchBox.getPlaces();

                   if (places.length == 0) {
                       return;
                   }

                   // Clear out the old markers.
                   markers.forEach(function (marker) {
                       marker.setMap(null);
                   });
                   markers = [];

                   // For each place, get the icon, name and location.
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

                       // Create a marker for each place.
                       markers.push(new google.maps.Marker({
                           map: map,
                           icon: icon,
                           title: place.name,
                           position: place.geometry.location
                       }));

                       if (place.geometry.viewport) {
                           // Only geocodes have viewport.
                           bounds.union(place.geometry.viewport);
                       } else {
                           bounds.extend(place.geometry.location);
                       }
                   });
                   map.fitBounds(bounds);
               });
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-4"></div>
    <div class="col-md-8">
        <div class="col-md-12 text-left">
            <h4 class="form-signin-heading">Registro de Estacionamiento</h4>
        </div>
        <div class="col-md-12 row form-group">
            <input id="txt_direccion_map" class="controls form-control input-sm" type="text" placeholder="Ingrese Dirección"/>
            <div id="map"></div>
        </div>
        <div class="hide">
            <asp:TextBox 
                ID="txt_direccion"
                runat="server" 
                CssClass="direccion">
            </asp:TextBox>
                <asp:TextBox 
                ID="txt_latitud"
                runat="server" 
                CssClass="latitud">
            </asp:TextBox>
                <asp:TextBox 
                ID="txt_longitud"
                runat="server" 
                CssClass="longitud">
            </asp:TextBox>
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
                Text="Ingresar" 
                CssClass="btn btn-sm btn-primary btn-block" 
                OnClick="Button1_Click"
                />
        </div>
    </div>
</asp:Content>
