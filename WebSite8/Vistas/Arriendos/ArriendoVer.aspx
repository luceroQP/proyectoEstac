<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ArriendoVer.aspx.cs" Inherits="ArriendoVer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%
        Estacionamiento estacionamiento = (Estacionamiento)Session["estacionamiento"];
        string latitud = estacionamiento.latitud.ToString().Replace(",", ".");
        string longitud = estacionamiento.longitud.ToString().Replace(",", ".");
    %>
    <link rel="stylesheet" href="/css/google_maps.css"/>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDC3mh16ySlzaS1hVXfzyrRE33l3UbcqfU&callback=initMap" defer></script>
    <script>
        function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: <%=latitud%>, lng: <%=longitud%>},
                zoom: 15,
                draggable: false,
                zoomControl: false, 
                scrollwheel: false, 
                disableDoubleClickZoom: true
            });

            var marker = new google.maps.Marker({
                position: { lat: <%=latitud%>, lng: <%=longitud%> },
                map: map,
                title: "<%=estacionamiento.direccion%>",
            });
        }
    </script>
    <script>
        var calcularPrecio = function () {
            var horas = $(".txtHorasUsadas").val();
            var valor = $(".valorHora").html().trim();
            $(".totalPagar").html((horas * valor));
        };
        $(document).ready(function () {
            calcularPrecio();

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

            if($("#map").html().trim() == "<div class=\"loader-div\"><div></div></div>"){
                initMap();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%
        Arriendo arriendo = (Arriendo)Session["arriendo"];
    %>
    <div class="col-md-12 text-center">
        <h4 class="form-signin-heading">Datos Arriendo</h4>
    </div>
    <div class="col-md-12 row form-group">
        <asp:Label ID="Label1" runat="server" Text="Ubicación"></asp:Label>
        <div id="map">
            <div class="loader-div"><div></div></div>
        </div>
    </div>
    <div class="col-md-4"></div>
    <div class="col-md-8">    
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
        <div id="divDatosEstacionamiento" runat="server" class="col-md-12 row form-group">
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
                    <asp:Label ID="Label6" runat="server" Text="Fin Arriendo"></asp:Label>
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
            <div class="col-md-12 divHorasUsadas"><% Response.Write(arriendo.horas_usadas); %></div>
        </div>
        <div class="col-md-12 row form-group">
            <div class="col-md-12">
                <asp:Label ID="Label2" runat="server" Text="Total a Pagar"></asp:Label>
            </div>
            <div class="col-md-12 totalPagar">0</div>
        </div>
    </div>
</asp:Content>

