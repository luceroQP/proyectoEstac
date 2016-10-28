<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Arrendar.aspx.cs" Inherits="Arrendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="/css/google_maps.css"/>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDC3mh16ySlzaS1hVXfzyrRE33l3UbcqfU&callback=initMap" async defer></script>
    <script>
        // Note: This example requires that you consent to location sharing when
        // prompted by your browser. If you see the error "The Geolocation service
        // failed.", it means you probably did not give permission for the browser to
        // locate you.

        function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: -34.397, lng: 150.644 },
                zoom: 18
            });
            var infoWindow = new google.maps.InfoWindow({ map: map });

            // Try HTML5 geolocation.
            if (navigator.geolocation){
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };
                    infoWindow.setPosition(pos);
                    infoWindow.setContent('Te encontre!.');
                    map.setCenter(pos);
                }, function () {
                    handleLocationError(true, infoWindow, map.getCenter());
                });
            } else {
                // Browser doesn't support Geolocation
                handleLocationError(false, infoWindow, map.getCenter());
            }
        }

        function handleLocationError(browserHasGeolocation, infoWindow, pos) {
            infoWindow.setPosition(pos);
            infoWindow.setContent(browserHasGeolocation ?
                                    'Error: The Geolocation service failed.' :
                                    'Error: Your browser doesn\'t support geolocation.');
        }
    </script>
    <script>
        var calcularPrecio = function () {
            var horas = $(".txtHorasUsadas").val();
            var valor = $(".valorHora").html().trim();
            $(".totalPagar").html((horas * valor));
        };
        $(document).ready(function () {
          

            $(".horaInicio, .minutoInicio, .horaFin, .minutoFin").change(function (e) {
                var horaInicio = $(".horaInicio").val();
                var minutoInicio = $(".minutoInicio").val();
                var horaFin = $(".horaFin").val();
                var minutoFin = $(".minutoFin").val();

                var inicioArriendo = new Date("1971-1-1 " + horaInicio + ":" + minutoInicio);
                var finArriendo = new Date("1971-1-1 " + horaFin + ":" + minutoFin);

                if (finArriendo > inicioArriendo) {
                    var minutosDiferencia = (finArriendo - inicioArriendo) / 1000 / 60;
                    var horasUsadas = minutosDiferencia / 60;
                    var horas = Math.floor(minutosDiferencia / 60);
                    var minutos = minutosDiferencia % 60;

                    $(".divHorasUsadas").html(horas + ":" + minutos);
                    $(".txtHorasUsadas").val(horasUsadas);
                    $(".txtHorasUsadas").attr("value", horasUsadas);

                    calcularPrecio();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-4"></div>
    <div class="col-md-8">
        <div class="col-md-12 text-left">
            <h4 class="form-signin-heading">Datos Arriendo</h4>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label1" runat="server" Text="Ubicación"></asp:Label>
                <div id="map"></div>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label7" runat="server" Text="Estacionamiento"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_estacionamiento"
                    CssClass="form-control input-sm" 
                    runat="server" 
                    AutoPostBack="true"
                    OnSelectedIndexChanged="dpd_estacionamiento_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div id="divDatosEstacionamiento" runat="server" visible="false" class="col-md-12 row form-group">
            <%
                if (Session["estacionamiento"] != null)
                {
                    Estacionamiento estacionamiento = (Estacionamiento)Session["estacionamiento"];
                    %>
                    <div class="col-md-12 form-group">
                        <asp:Label ID="Label10" runat="server" Text="Detalle Estacionamiento"></asp:Label>
                    </div>
                    <div class="col-md-12 row">
                        <div class="col-md-3">Dirección</div>
                        <div class="col-md-9">
                            <% Response.Write(estacionamiento.direccion); %>
                        </div>
                        <div class="col-md-3">Valor Hora</div>
                        <div class="col-md-9 valorHora">
                            <% Response.Write(estacionamiento.valor_hora); %>
                        </div>
                        <div class="col-md-3">Disponibilidad</div>
                        <div class="col-md-9">
                            <% Response.Write(estacionamiento.EstacionamientoEstado.nombre_estacionamiento_estado); %>
                        </div>
                        <div class="col-md-3">Cupos</div>
                        <div class="col-md-9">
                            <% Response.Write(estacionamiento.capacidad - estacionamiento.existencias); %>
                        </div>
                        <div class="col-md-3">Horario</div>
                        <div class="col-md-9">
                            <% Response.Write(estacionamiento.inicio_disponibilidad.ToString("HH:mm")); %>
                            -
                            <% Response.Write(estacionamiento.fin_disponibilidad.ToString("HH:mm")); %>
                        </div>
                    </div>
                    <%
                }
            %>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-6">
                <asp:Label ID="Label8" runat="server" Text="Vehículo"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_vehiculo"
                    CssClass="form-control input-sm" 
                    runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div class="col-md-12 row form-group hide">
            <div class="col-md-6">
                <asp:Label ID="Label13" runat="server" Text="Tipo de Arriendo"></asp:Label>
                <asp:DropDownList 
                    ID="dpd_tipo_disponibilidad"
                    CssClass="form-control input-sm" 
                    AutoPostBack="true"
                    runat="server" OnSelectedIndexChanged="dpd_tipo_disponibilidad_SelectedIndexChanged">
                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                    <asp:ListItem Value="1">Arrendar por Horas</asp:ListItem>
                    <asp:ListItem Value="2">Arrendar según Horario</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div id="divInicioArriendo" runat="server" visible="true" class="col-md-12 row form-group">
            <div class="col-md-6 row">
                <div class="col-md-12">
                    <asp:Label ID="Label5" runat="server" Text="Inicio Arriendo"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList 
                        ID="dpd_hora_inicio"
                        CssClass="form-control input-sm horaInicio" 
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList 
                        ID="dpd_minuto_inicio"
                        CssClass="form-control input-sm minutoInicio" 
                        runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div id="divFinArriendo" runat="server" visible="true" class="col-md-12 row form-group">
            <div class="col-md-6 row">
                <div class="col-md-12">
                    <asp:Label ID="Label6" runat="server" Text="Fin Disponibilidad"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList 
                        ID="dpd_hora_fin"
                        CssClass="form-control input-sm horaFin" 
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList 
                        ID="dpd_minuto_fin"
                        CssClass="form-control input-sm minutoFin" 
                        runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-12">
                <asp:Label ID="Label3" runat="server" Text="Total Horas Usadas"></asp:Label>
                <asp:TextBox 
                    ID="txt_horas_usadas" 
                    runat="server"
                    CssClass="form-control input-sm txtHorasUsadas hide">
                </asp:TextBox>
            </div>
            <div class="col-md-12 divHorasUsadas">0</div>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-12">
                <asp:Label ID="Label2" runat="server" Text="Total a Pagar"></asp:Label>
            </div>
            <div class="col-md-12 totalPagar">0</div>
        </div>
        <div class="col-md-6 form-group">
            <asp:Button
                ID="Button1" 
                runat="server" 
                Text="Arrendar" 
                CssClass="btn btn-sm btn-primary btn-block" OnClick="Button1_Click"
                />
        </div>
    </div>
</asp:Content>

