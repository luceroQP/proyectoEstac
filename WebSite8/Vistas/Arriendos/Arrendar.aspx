<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Arrendar.aspx.cs" Inherits="Arrendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%
        
    %>
    <link rel="stylesheet" href="/css/google_maps.css"/>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDC3mh16ySlzaS1hVXfzyrRE33l3UbcqfU&callback=initMap" defer></script>
    <script>
        function initMap() {
            var posicionDefecto = { lat: -33.424376, lng: -70.769220 };

            var map = new google.maps.Map(document.getElementById('map'), {
                center: posicionDefecto,
                zoom: 12
            });
            
            // Try HTML5 geolocation.
            if (navigator.geolocation){
                navigator.geolocation.getCurrentPosition(function (position){
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };
                    map.setCenter(pos);
                    var markerDefault = new google.maps.Marker({
                        position: { lat: pos.lat, lng: pos.lng },
                        map: map,
                        icon: "/images/my_location.png"
                    });
                }, function () {
                    handleLocationError(true, map, map.getCenter());
                });
            } else {
                // Browser doesn't support Geolocation
                handleLocationError(false, map, map.getCenter());
            }

            var infoWindow = new google.maps.InfoWindow({});
            <%
                if (Session["estacionamientos"] != null) { 
                    List<Estacionamiento> estacionamientos = (List<Estacionamiento>)Session["estacionamientos"];
                    int index = 10;
                    foreach (Estacionamiento estacionamiento in estacionamientos)
                    {
                        string latitud = estacionamiento.latitud.ToString().Replace(",", ".");
                        string longitud = estacionamiento.longitud.ToString().Replace(",", ".");
                        string horario = estacionamiento.inicio_disponibilidad.ToString("HH:mm") + "-" + estacionamiento.fin_disponibilidad.ToString("HH:mm");
                        string descripcion = "<div id='content' style='max-width:300px !important;'>";
                        descripcion += "<h5 id='firstHeading' class='firstHeading col-md-12'>" + estacionamiento.direccion + "</h5>";
                            descripcion += "<div id='bodyContent' class='col-md-12 form-group row'>";
                                descripcion += "<div class='col-md-4'>Valor Hora</div>";
                                descripcion += "<div class='col-md-8'>:" + estacionamiento.valor_hora + "</div>";
                                descripcion += "<div class='col-md-4'>Disponibilidad</div>";
                                descripcion += "<div class='col-md-8'>:" + estacionamiento.EstacionamientoEstado.nombre_estacionamiento_estado + "</div>";
                                descripcion += "<div class='col-md-4'>Cupos</div>";
                                descripcion += "<div class='col-md-8'>:" + (estacionamiento.capacidad - estacionamiento.existencias) + "</div>";
                                descripcion += "<div class='col-md-4'>Horario</div>";
                                descripcion += "<div class='col-md-8'>:" + horario + "</div>";
                                descripcion += "<div class='row form-group col-md-12'></div>";
                                descripcion += "<div class='col-md-12'>";
                                    descripcion += "<a href='#' onclick='seleccionEstacionamiento(this, event);' class='btn btn-success btn-sm btnSeleccionar' estacionamiento='" + estacionamiento.cod_estacionamiento + "' valor='" + estacionamiento.valor_hora + "'>";
                                        descripcion += "Seleccionar";
                                    descripcion += "</a>";
                                descripcion += "</div>";
                            descripcion += "</div>";
                        descripcion += "</div>";
                                            
                        %>
                        var marker<%=index%> = new google.maps.Marker({
                            position: { lat: <%=latitud%>, lng: <%=longitud%> },
                            map: map,
                            title: "<%=estacionamiento.direccion%>",
                            descripcion: "<%=descripcion%>"
                        });

                        marker<%=index%>.addListener('click', function (e) {
                            infoWindow.setContent(this.descripcion);
                            infoWindow.open(map, marker<%=index%>);
                        });
                        <%
                        index++;
                    }
                }
            %>
        }

        function handleLocationError(browserHasGeolocation, map, pos) {
            var infoWindow = new google.maps.InfoWindow({ map: map });
            infoWindow.setPosition(pos);
            infoWindow.setContent(browserHasGeolocation ?
                                    'Error: The Geolocation service failed.' :
                                    'Error: Your browser doesn\'t support geolocation.');
        }
    </script>
    <script>
        var escribirValores = function(){
            var fechaInicio = $(".calendarioInicio").val();
            var fechaTermino = $(".calendarioTermino").val();

            if(fechaInicio != "" && fechaTermino != ""){
                var horaInicio = $(".horaInicio").val();
                var minutoInicio = $(".minutoInicio").val();
                var horaFin = $(".horaFin").val();
                var minutoFin = $(".minutoFin").val();

                fechaInicio = fechaInicio.split("/");
                fechaTermino = fechaTermino.split("/");

                fechaInicio = new Date(fechaInicio[2], fechaInicio[1] - 1, fechaInicio[0], horaInicio, minutoInicio);
                fechaTermino = new Date(fechaTermino[2], fechaTermino[1] - 1, fechaTermino[0], horaFin, minutoFin);

                if (fechaTermino > fechaInicio){
                    var minutosDiferencia = (fechaTermino - fechaInicio) / 1000 / 60;
                    var horasUsadas = minutosDiferencia / 60;
                    var horas = Math.floor(minutosDiferencia / 60);
                    var minutos = minutosDiferencia % 60;

                    $(".divHorasUsadas").html(horas + ":" + minutos);
                    $(".txtHorasUsadas").val(horasUsadas);
                    $(".txtHorasUsadas").attr("value", horasUsadas);
                    calcularPrecio();
                }
            }
        };

        var calcularPrecio = function () {
            var horas = $(".txtHorasUsadas").val();
            var valor = $(".valorHora").html().trim();
            $(".totalPagar").html((horas * valor));
        };

        $(document).on("ready", function(){
            $(".calendarioInicio, .calendarioTermino").datepicker({
                firstDay: 1,
                dateFormat: "dd/mm/yy",
                dayNamesMin: [ "Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa" ],
                monthNamesShort: [ "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" ],
                monthNames: [ "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" ],
                minDate: new Date(),
                onSelect: function (fechaText, obj){
                    var input = $("#" + obj.id);
                    input.val(fechaText);
                    input.attr("value", fechaText);
                    escribirValores();
                }
            });

            if($("#map").html().trim() == "<div class=\"loader-div\"><div></div></div>"){
                    initMap();
            }

            $(".horaInicio, .minutoInicio, .horaFin, .minutoFin").change(function (e) {
                escribirValores();
            });

            $("body").on("change", ".selectMinutos", function(){
                var valorSeleccionado = $(this).val();
                $(".selectMinutos").val(valorSeleccionado);
                escribirValores();
            });

            //$("body").on("click", ".selectMinutos", function(){
            //    alert("hola");
            //});
        });

        var seleccionEstacionamiento = function(btn, e){
            e.preventDefault(); 
            btn = $(btn);
            var estacionamientoId = btn.attr("estacionamiento");
            var valorHora = btn.attr("valor");

            $(".txtEstacionamientoId").val(estacionamientoId);
            $(".txtEstacionamientoId").attr("value", estacionamientoId);

            $(".valorHora").html(valorHora);
            escribirValores();
        };

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-12 text-center">
        <h4 class="form-signin-heading">Datos Arriendo</h4>
    </div>
    <div class="col-md-4"></div>
    <div class="col-md-4">
        <div class="col-md-12 row form-group">
            <asp:Label ID="Label8" runat="server" Text="Vehículo"></asp:Label>
            <asp:DropDownList 
                ID="dpd_vehiculo"
                CssClass="form-control input-sm" 
                runat="server">
            </asp:DropDownList>
            <div class="col-md-12 row">
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" 
                    runat="server" 
                    Display="Dynamic"
                    InitialValue="0"
                    ControlToValidate="dpd_vehiculo"
                    CssClass="error-message" 
                    ValidationGroup="validacionFiltro"
                    ErrorMessage="Debe seleccionar vehiculo">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div id="divInicioArriendo" runat="server" visible="true" class="col-md-12 row form-group">
            <div class="col-md-12 row">
                <asp:Label ID="Label5" runat="server" Text="Inicio Arriendo"></asp:Label>
            </div>
            <div class="col-md-12 row">
                <div class="col-md-6 row">
                    <asp:TextBox 
                        ID="fecha_inicio_arriendo" 
                        runat="server"
                        ReadOnly="true"
                        CssClass="form-control input-sm calendarioInicio">
                        </asp:TextBox>
                </div>
                <div class="col-md-5 row ml-less-8">
                    <asp:DropDownList 
                        ID="dpd_hora_inicio"
                        CssClass="form-control input-sm horaInicio" 
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-5 row ml-less-8">
                    <asp:DropDownList 
                        ID="dpd_minuto_inicio"
                        CssClass="form-control input-sm minutoInicio selectMinutos" 
                        runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-12 row">
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator3" 
                        runat="server" 
                        Display="Dynamic"
                        ControlToValidate="fecha_inicio_arriendo"
                        CssClass="error-message" 
                        ValidationGroup="validacionFiltro"
                        ErrorMessage="Indique fecha inicio arriendo">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div id="divFinArriendo" runat="server" visible="true" class="col-md-12 row form-group">
            <div class="col-md-12 row">
                <asp:Label ID="Label6" runat="server" Text="Fin Arriendo"></asp:Label>
            </div>
            <div class="col-md-12 row">
                <div class="col-md-6 row">
                    <asp:TextBox 
                        ID="fecha_termino_arriendo" 
                        runat="server"
                        ReadOnly="true"
                        CssClass="form-control input-sm calendarioTermino">
                        </asp:TextBox>
                </div>
                <div class="col-md-5 row ml-less-8">
                    <asp:DropDownList 
                        ID="dpd_hora_fin"
                        CssClass="form-control input-sm horaFin" 
                        runat="server">
                        </asp:DropDownList>
                </div>
                <div class="col-md-5 row ml-less-8">
                    <asp:DropDownList 
                        ID="dpd_minuto_fin"
                        CssClass="form-control input-sm minutoFin selectMinutos" 
                        runat="server">
                        </asp:DropDownList>
                </div>
                <div class="col-md-12 row">
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator4" 
                        runat="server" 
                        Display="Dynamic"
                        ControlToValidate="fecha_termino_arriendo"
                        CssClass="error-message" 
                        ValidationGroup="validacionFiltro"
                        ErrorMessage="Indique fecha fin arriendo">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4"></div>
    <div class="col-md-12 row">
        <div class="col-md-3">
        <asp:Button
                ID="btn_buscarDisponibilidad" 
                runat="server" 
                Text="Buscar Disponibilidad" 
                CssClass="btn btn-sm btn-primary btn-block"
                ValidationGroup="validacionFiltro"
                OnClick="btn_buscarDisponibilidad_Click"
                />
        </div>
    </div>
    <div class="col-md-12 row form-group"></div>
    <div class="col-md-12">
        <asp:Label ID="Label1" runat="server" Text="Ubicación"></asp:Label>
        <div id="map">
            <div class="loader-div"><div></div></div>
        </div>
        <asp:TextBox 
            ID="txt_estacionamiento_id" 
            runat="server"
            CssClass="txtEstacionamientoId hide">
            </asp:TextBox>
        <div class="col-md-12 row">
            <asp:RequiredFieldValidator 
                ID="RequiredFieldValidator2" 
                runat="server" 
                Display="Dynamic"
                ControlToValidate="txt_estacionamiento_id"
                CssClass="error-message" 
                ValidationGroup="validacionArriendo"
                ErrorMessage="Debe seleccionar estacionamiento">
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="form-group col-md-12"></div>
    <div class="form-group col-md-12">
        <div class="col-md-4 row form-group">
            <asp:Label ID="Label4" runat="server" Text="Valor Hora"></asp:Label>
            <div class="col-md-12 valorHora">0</div>
        </div>
        <div class="col-md-4 row form-group">
            <asp:Label ID="Label3" runat="server" Text="Total Horas Usadas"></asp:Label>
            <asp:TextBox 
                ID="txt_horas_usadas" 
                runat="server"
                CssClass="form-control input-sm txtHorasUsadas hide">
            </asp:TextBox>
            <div class="col-md-12 divHorasUsadas">0</div>
        </div>
        <div class="col-md-4 row form-group">
            <asp:Label ID="Label2" runat="server" Text="Total a Pagar"></asp:Label>
            <div class="col-md-12 totalPagar">0</div>
        </div>
    </div>
    <div class="col-md-12 row" id="divBtnGuardar" runat="server">
        <hr />
        <div class="col-md-2">
        <asp:Button
                ID="btn_arrendar" 
                runat="server" 
                Text="Arrendar" 
                CssClass="btn btn-sm btn-primary btn-block"
                ValidationGroup="validacionArriendo"
                OnClick="Button1_Click"
                />
        </div>
    </div>
</asp:Content>

