<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Transacciones.aspx.cs" Inherits="Vistas_Transacciones_Transacciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/css/estiloGeneral.css" rel="stylesheet"/>
    <link href="https://cdn.datatables.net/buttons/1.2.2/css/buttons.dataTables.min.css" rel="stylesheet"/>

    <script type="text/javascript" src=" //code.jquery.com/jquery-1.12.3.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.2.2/js/buttons.flash.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script type="text/javascript" src="//cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
    <script type="text/javascript" src="//cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.2.2/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.2.2/js/buttons.print.min.js"></script>
    <script>
        $(document).on("ready", function () {
            var table = $('#dataTable').DataTable({
                paging: false,
                ordering: false,
                searching: false,
                dom: 'Bfrtip',
                buttons: [
                    'excel', 'pdf'
                ]
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%
        Tarjeta Tarjeta = (Tarjeta)Session["tarjetaReporte"];
    %>
    <div class="col-md-12 form-group">
        <b>Cuenta:</b> <%=Tarjeta.numero_tarjeta %>
        <br />
        <b>Banco:</b> <%=Tarjeta.Banco.nombre_banco %>
        <br />
        <b>Tipo:</b> <%=Tarjeta.TarjetaTipo.nombre_tarjeta_tipo %>
        <br />
        <b>Saldo:</b> <%=Tarjeta.saldo %>
    </div>
    <table id="dataTable" class="table table-hover table-bordered">
        <thead>
            <tr>
                <th>N° Operación</th>
                <th>Descripción Arriendo</th>
                <th>Cargos</th>
                <th>Abonos</th>
            </tr>
        </thead>
        <tbody>
            <%
                int totalCargos = 0;
                int totalAbonos = 0;
                foreach (Transaccion transaccion in Tarjeta.Transacciones)
                {
                    int cargos = 0;
                    int abono = 0;
                    if (transaccion.numero_tarjeta_origen == Tarjeta.cod_tarjeta) { cargos = transaccion.monto; totalCargos += cargos; }
                    else { abono = transaccion.monto; totalAbonos += abono; }
                    %>
                        <tr>
                            <td><%=transaccion.cod_transaccion %></td>
                            <td>
                                <%=transaccion.Arriendo.Estacionamiento.direccion %>
                                <br />
                                Patente Vehículo:
                                <%=transaccion.Arriendo.Vehiculo.patente%>
                            </td>
                            <td><%=cargos %></td>
                            <td><%=abono %></td>
                        </tr>
                    <%
                }
            %>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="2" class="text-right">Totales</td>
                <td><%=totalCargos %></td>
                <td><%=totalAbonos %></td>
            </tr>
        </tfoot>
    </table>
</asp:Content>

