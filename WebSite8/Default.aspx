<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 99px;
            height: 92px;
        }
        .style2
        {
            width: 108px;
        }
        .style3
        {
            width: 221px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="texto_inicio" align="center">
        <asp:Label align="center" ID="texto_incicio" class="texto_inicio" runat="server" 
            Text="Bienvenido! "></asp:Label>
        </div>
        &nbsp;
        <div class="texto_inicio_par">
            <asp:Label ID="texto_inicio_par" class="texto_inicio_par" runat="server" Text="¡Nuestra aplicacion te facilitara el agotador trabajo de encontrar estacionamiento en unos simples pasos!"></asp:Label>
        </div>


        &nbsp;
<div class="centrarTabla" align="center">
    <table  align="center" style="150%: ;" width="50%">
        <tr>
            <td align ="right" class="style2" rowspan="3">
                &nbsp;
                <img class="style1" src="images/ico%20usuario.png" align="right" width="20%" /></td>
            <td align="center" class="style3">
                &nbsp;
                Estacionamientos arrendados:</td>
            <td align="center" width="15%">
                &nbsp;
                0%</td>
        </tr>
        <tr>
            <td class="style3" align="center">
                &nbsp;
                Arriendos publicados:</td>
            <td align="center" width="15%">
                &nbsp;
                0%</td>
        </tr>
       <%-- <tr>
            <td class="style3">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>--%>
    </table>
    </div>
       &nbsp;
          &nbsp;
<div align="center">
<td>
    &nbsp;<Img alt="imagen-estacio" ID="Image1" src="images\giphy (1).gif" 
        align="middle" width="50%" />
        </td>
        </div>
</asp:Content>

