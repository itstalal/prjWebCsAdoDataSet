﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webLinqDataset.aspx.cs" Inherits="prjWebCsAdoDataSet.webLinqDataset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function valider() {
            return window.confirm("Etes-vous sur de vouloir supprimer cette equipe ?");
        }
    </script>
<style type="text/css">
    table{
        margin:auto;
        width:700px;
        border-radius:10px;
        padding:2px;
        border-spacing:4px;
        background-color:bisque;
        font-weight:bold;
        color:saddlebrown;
    }
     .auto-style1 {
     width: 100%;
     border-style: solid;
     border-width: 1px;
 }

</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 style="text-align:center">GESTION DATASET STATIQUE</h1>
            <hr style="width:600px" />

            <table>
                <tr>
                    <td>
                        Choisir une equipe<br />
                        <asp:ListBox ID="lstEquipes" OnSelectedIndexChanged="lstEquipes_SelectedIndexChanged" runat="server" AutoPostBack="True" Font-Bold="True" ForeColor="Blue" Height="100" Width="200"></asp:ListBox>
                    </td>
                    <td>

                        <table class="auto-style1">
    <tr>
        <td colspan="2">Gestion de equipe selectionnee</td>
       
        <td rowspan="5">

        </td>
    </tr>
    <tr>
        <td>Nom : </td>
        <td>
            <asp:TextBox ID="txtNom" runat="server" Font-Bold="True" ForeColor="Blue" Width="200px"></asp:TextBox>


        </td>
       
    </tr>
    <tr>
        <td>Ville : </td>
        <td><asp:TextBox ID="txtVille" runat="server" Font-Bold="True" ForeColor="Blue" Width="200px"></asp:TextBox>


        </td>
        
    </tr>
    <tr>
        <td>Budget : </td>
        <td>
            <asp:TextBox ID="txtBudget" runat="server" Font-Bold="True" ForeColor="Blue" Width="200px"></asp:TextBox>
        </td>
        
    </tr>
    <tr>
        <td>Coach : </td>
        <td>
            <asp:TextBox ID="txtCoach" runat="server" Font-Bold="True" ForeColor="Blue" Width="200px"></asp:TextBox>
        </td>
        
    </tr>
</table>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gridJoueurs" runat="server" BackColor="#66FFFF" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" ForeColor="#663300" Width="100%"></asp:GridView>
                    </td>
                </tr>

            </table>

            
        </div>
    </form>
</body>
</html>