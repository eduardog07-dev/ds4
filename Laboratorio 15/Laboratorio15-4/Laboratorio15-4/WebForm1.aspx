<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Laboratorio15_4.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Introduzca dos valores a sumar"></asp:Label>
        </div>
        <p>
            <asp:TextBox ID="txtb1" runat="server">valor1</asp:TextBox>
            <asp:TextBox ID="txtb2" runat="server">valor2</asp:TextBox>
            <asp:Button ID="btnSumar" runat="server" OnClick="Button1_Click" Text="Realizar Suma" />
        </p>
        <asp:Label ID="lblResultado" runat="server"></asp:Label>
    </form>
</body>
</html>
