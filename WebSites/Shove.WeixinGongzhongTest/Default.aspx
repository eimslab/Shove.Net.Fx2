<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Dels(aa) {
            $(aa).addClass("ColorBackGray");
            $("#cre").removeClass("ColorBack");
            $("#del").show();
            $("#create").hide();
            $("#h").css("color", "white");
        }
        function create(bb) {
            $(bb).addClass("ColorBack");
            $("#lil").removeClass("ColorBackGray");
            $("#h").css("color", "gray");
            $("#create").show();
            $("#del").hide();
        }
    </script>
    <title></title>
    <style type="text/css">
        .td
        {
            color: Red;
        }
        .ColorBack
        {
            background-color: #80FFFF;
        }
        .ColorBackGray
        {
            background-color: Gray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:FileUpload ID="fileupload" runat="server" />
    <div style="width: 1364px; height: 100px;">
        <img alt="" src="images/logo.png" style="width: 200px; height: 30px;" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div style="margin-left: 350px; margin-top: 1px; position: absolute">
            <ul style="list-style: none; width: 100; margin: 0px; padding: 0px;">
                <li style="width: 80px; border: 1px solid gray; color: gray; margin-bottom: 10px;
                    text-align: center;" id="cre" onclick="create(this)" class="ColorBack">
                    <h4 style="margin: 0px; padding: 0px; cursor: pointer">
                        1创建菜单</h4>
                </li>
                <li style="width: 80px; border: 1px solid gray; color: gray; text-align: center"
                    id="lil" onclick="Dels(this)">
                    <h4 id="h" style="margin: 0px; padding: 0px; cursor: pointer">
                        2删除菜单</h4>
                </li>
            </ul>
        </div>
        <div style="display: none; margin: 0px auto; width: 500px; border: 1px solid Black;
            background-color: Gray;" id="del">
            <table style="width: auto; height: auto; color: White" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" class="td" style="text-align: center; margin: 0px;">
                        <img alt="x" src="images/logo.png" style="width: 200px; height: 30px;" />
                        <h2>
                            删除微信菜单</h2>
                    </td>
                </tr>
                <tr style="border-bottom-color: red">
                    <td>
                        (第三方用户唯一凭证)appid:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_AppIddel" Text="" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="">
                    <td>
                        (第三方用户唯一凭证密钥,既appsecret)secret:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Appsecretdel" Text="" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="bnt_del" runat="server" Text="删除菜单" onclick="bnt_del_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin: 0px auto; width: 500px; border: 1px solid Black; background-color: #86FFFF"
            id="create">
            <table style="width: auto; height: auto" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" class="td" style="text-align: center; margin: 0px;">
                        <img alt="x" src="images/logo.png" style="width: 200px; height: 30px;" />
                        <h2>
                            创建微信菜单</h2>
                    </td>
                </tr>
                <tr style="border: 200px solid red">
                    <td>
                        (第三方用户唯一凭证)appid:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_AppId" Text="wxdf9f01a2ae215175" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        (第三方用户唯一凭证密钥,既appsecret)secret:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Appsecret" Text="15cab4854630bd48f3de1d239c589a95" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="bnt_create" runat="server" Text="创建菜单" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
