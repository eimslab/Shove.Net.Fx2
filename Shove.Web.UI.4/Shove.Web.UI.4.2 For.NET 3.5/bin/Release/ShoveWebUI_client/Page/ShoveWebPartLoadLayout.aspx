<%@ Page Language="C#" AutoEventWireup="true" Inherits="Shove.Web.UI.ShoveWebPartLoadLayout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<title>导入页面布局</title>
  
   
</head>
<body >
    <form id="form1" runat="server">
    <div>

        <table bgcolor="#CCCCCC" width="550px"  height="380px">
            <tr>
                <td width="10%"  valign="middle" bgcolor="White">
                    请输入布局样式：
                </td>
                <td  valign="middle" bgcolor="White" style="padding-left: 10px" Width="500px" Height="100px">
                    <asp:TextBox runat="server" ID="tbStyle"  TextMode="MultiLine" Width="500px" Height="100px" />
                </td>
            </tr>
            <tr>
                <td   valign="middle" bgcolor="White">
                    请输入布局标签：
                </td>
                <td  valign="middle" bgcolor="White" style="padding-left: 10px" Width="500px" Height="100px">
                    <asp:TextBox runat="server" ID="tbTag" TextMode="MultiLine" Width="500px" Height="100px"/>
                </td>
            </tr>
            <tr>
                <td  colspan="2" align="center" valign="middle" bgcolor="#f7f7f7">
                    <asp:Button runat="server" ID="btnOK" Text="确定" OnClick="btnOK_Click" Width="94px" UseSubmitBehavior="false" />&nbsp;
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>