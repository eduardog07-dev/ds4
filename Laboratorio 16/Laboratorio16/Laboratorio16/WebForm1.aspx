<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Laboratorio16.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <style type="text/css">
        .cal
        {
            position:absolute;
            top:50px;
            left:400px;
            right:400px;
            height:600px;
            bottom:10px;
            background-color:dodgerblue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="cal">
        <asp:Label ID="Label1" Text="CALCULADORA BASICA" runat="server" Style="margin-left: 50px" 
                   Font-Bold="True" Font-Italic="False" ForeColor="White" Font-Size="25px"></asp:Label>
        <asp:TextBox ID="tResultado" Text="" runat="server" Style="margin-left: 50px; margin-top: 24px;" 
                     Width="335px" Height="41px"></asp:TextBox>
        <asp:Button ID="b1" Text="1" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b1_click" />
        <asp:Button ID="b2" Text="2" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b2_click" />
        <asp:Button ID="b3" Text="3" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b3_click" />
        <asp:Button ID="btnAdd" Text="+" runat="server" Height="37px" Style="margin-left: 0px; margin-top: 0px;" Width="57px" OnClick="add_click" />
        <asp:Button ID="b4" Text="4" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b4_click" />
        <asp:Button ID="b5" Text="5" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b5_click" />
        <asp:Button ID="b6" Text="6" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b6_click" />
        <asp:Button ID="bSub" Text="-" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="sub_click" />
        <asp:Button ID="b7" Text="7" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b7_click" />
        <asp:Button ID="b8" Text="8" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b8_click" />
        <asp:Button ID="b9" Text="9" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b9_click" />
        <asp:Button ID="bMul" Text="*" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="mul_click" />
        <asp:Button ID="b0" Text="0" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="b0_click" />
        <asp:Button ID="bClr" Text="CLR" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="clr_click" />
        <asp:Button ID="bEql" Text="=" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="eql_click" />
        <asp:Button ID="bDiv" Text="/" runat="server" Height="37px" Style="margin-left: 0px" Width="57px" OnClick="div_click" />
    </div>
    </form>
</body>
</html>
