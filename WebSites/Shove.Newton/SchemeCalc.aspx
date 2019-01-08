<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SchemeCalc.aspx.cs" Inherits="SchemeCalc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        方案各功能组合后工作量计算器：<br />
        <br />
        <br />
    </div>

    <table>
        <tr>
        <td>PD 工作量（小时）</td><td><asp:TextBox ID="TextBox13" runat="server" Enabled="False"></asp:TextBox></td><td><asp:TextBox ID="TextBox1" runat="server" Enabled="False"></asp:TextBox>
            </td><td>&nbsp; 人月：</td><td><asp:TextBox ID="TextBox7" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>UI 工作量（小时）</td><td><asp:TextBox ID="TextBox14" runat="server" Enabled="False"></asp:TextBox></td><td><asp:TextBox ID="TextBox2" runat="server" Enabled="False"></asp:TextBox>
            </td><td>&nbsp; 人月：</td><td><asp:TextBox ID="TextBox8" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>代码工作量（小时）</td><td><asp:TextBox ID="TextBox15" runat="server" Enabled="False"></asp:TextBox></td><td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td><td>&nbsp; 人月：</td><td><asp:TextBox ID="TextBox9" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>QC 工作量（小时）</td><td><asp:TextBox ID="TextBox16" runat="server" Enabled="False"></asp:TextBox></td><td><asp:TextBox ID="TextBox4" runat="server" Enabled="False"></asp:TextBox>
            </td><td>&nbsp; 人月：</td><td><asp:TextBox ID="TextBox10" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>合计工作量（小时）</td><td><asp:TextBox ID="TextBox17" runat="server" Enabled="False"></asp:TextBox></td><td><asp:TextBox ID="TextBox5" runat="server" Enabled="False"></asp:TextBox>
            </td><td>&nbsp; 人月：</td><td><asp:TextBox ID="TextBox11" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <td>程序员人数</td><td>&nbsp;</td><td><asp:TextBox ID="TextBox6" runat="server">2</asp:TextBox>
            </td><td>&nbsp; 工期：</td><td><asp:TextBox ID="TextBox12" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        </table>
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
    <asp:Button ID="Button1" runat="server" Text="计算" Width="110px" 
        onclick="Button1_Click" />
    </form>
</body>
</html>
