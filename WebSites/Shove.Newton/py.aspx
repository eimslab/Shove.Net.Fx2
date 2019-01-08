<%@ Page Language="C#" AutoEventWireup="true" CodeFile="py.aspx.cs" Inherits="py" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <strong><span class="style1">2-4 位声母查词</span></strong><br />
    <br />
    请输入声母组合：
     
    <asp:TextBox ID="tbPy" runat="server" MaxLength="4"></asp:TextBox>
&nbsp;<asp:Button ID="btnGo" runat="server" onclick="btnGo_Click" Text="查词" />
&nbsp;(请输入 2-4 位拼音声母的组合，不区分大小写，中间不要有空格)<br />
    <br />
    <asp:Label ID="labResult" runat="server"></asp:Label>
    </form>
</body>
</html>
