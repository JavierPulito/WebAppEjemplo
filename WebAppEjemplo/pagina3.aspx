<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina3.aspx.cs" Inherits="WebAppEjemplo.pagina3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    Hola desde la pagina 3
    <form id="form1" runat="server">
        <div>
            <asp:LinkButton id="PostBackbtn" PostBackUrl="~/pagina2.aspx" Text="Presióname" runat="server">Otro texto</asp:LinkButton>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
