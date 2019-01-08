<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeixinTest.aspx.cs" Inherits="WeixinTest" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     
    <title></title>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ShowDiv(data) {
            $("#Div").html(" ");
            if (data == 1) {
                $("#Div").append("图片<input id=\"File1\" type=\"file\" />");
            } else if (data == 2) {
                $("#Div").append("文本<input id=\"File1\" type=\"text\"  />");
            } else if (data == 3) {
                $("#Div").append("声音<input id=\"File1\" type=\"file\"  />");
            } else if (data == 4) {
                $("#Div").append("图文<input id=\"File1\" type=\"file\"  />");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%">
        <div style="border: 1px solid Aqua; ">
            <h3>
                发送客服信息</h3>
            <input id="Button2" type="button" value="图片" onclick="ShowDiv(1)" />
            <input id="Button3" type="button" value="文本" onclick="ShowDiv(2)" />
            <input id="Button4" type="button" value="声音" onclick="ShowDiv(3)" />
            <input id="Button5" type="button" value="图文" onclick="ShowDiv(4)" />
            <div id="Div" style="width: 200px; height: 50px; margin: 0px auto">
            </div>
            <asp:Button ID="Button1" runat="server" Text="发送图片" OnClick="Button1_Click" />
            <asp:Button ID="Button16" runat="server" Text="发送文本" OnClick="Button16_Click" />
            <asp:Button ID="Button17" runat="server" Text="发送声音" OnClick="Button17_Click" />
            <asp:Button ID="Button18" runat="server" Text="发送图文" OnClick="Button18_Click" />
            <br />
            <br />
            <br />
        </div>
        <div style="height: 100px; border: 1px solid Black; ">
            <div style="width: 250px; height: 100px; border: 1px solid Black; float: left">
                <asp:Button ID="Button7" runat="server" Text="获取分组列表" OnClick="group_Click" />
                <p id="groupList" runat="server">
                   
                </p>
            </div>
            <div style="float: left; width: 250px; height: 100px; border: 1px solid Black;">
                <asp:Button ID="Button9" runat="server" Text="修改分组名称" OnClick="Button9_Click" /><br />
                <br />
                分组名称：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </div>
            <div style="float: left; width: 350px; height: 100px; border: 1px solid Black;">
                <asp:Button ID="Button10" runat="server" Text="移动分组" OnClick="Button10_Click" /><br />
                <br />
                移动的用户Openid：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
                移动至分组的ID：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </div>
            <div style="float: left; width: 200px; height: 100px; border: 1px solid Black;">
                <asp:Button ID="Button11" runat="server" Text="创建分组" OnClick="Button11_Click" /><br />
                <br />
                名称：<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br />
            </div>
        </div>
        <div style="height: auto; border: 1px solid Fuchsia;">
            <div style="float: left">
                <asp:Button ID="Button8" runat="server" Text="获取用户基本信息" OnClick="Button8_Click" />
                <table border="1">
                    <tr>
                        <td>
                             是否关注:
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            openid（用户账号）:
                        </td>
                        <td>
                           <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            用户的昵称:
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            性别:
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            用户s使用的语言:
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            所在市:
                        </td>
                        <td>
                           <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            省:
                        </td>
                        <td>
                           <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            国籍:
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            头像:
                        </td>
                        <td>
                            <asp:Image ID="Image1" runat="server" />

                           </td>
                    </tr>
                    <tr>
                        <td>
                            关注时间（最后一次关注时间）:
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
           <div style="float:left; text-align:center">
                <div style="width: 550px; height: 350px; border: 1px solid red; float: left">
                    <asp:Button ID="group" runat="server" Text="获取关注列表" OnClick="Button6_Click" />
                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
        </div>
        <div style="clear: left; height: 170px; border: 1px solid Gray; ">
            <h3 style="text-align: center">
                二维码</h3>
            <div style="width: 350px; height: 100px; border: 1px solid Gray; float: left">
                <asp:Button ID="Button12" runat="server" Text="创建临时二维码" 
                    onclick="Button12_Click" />
                <p>
                    scene_id 场景ID：<asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </p>
            </div>
            <div style="float: left; width: 350px; height: 100px; border: 1px solid Gray;">
                <asp:Button ID="Button13" runat="server" Text="创建永久二维码" 
                    onclick="Button13_Click" /><br />
                <br />
                scene_id 场景ID：<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </div>
            <div style="float: left; width: 350px; height: 100px; border: 1px solid Gray;">
                <asp:Button ID="Button6" runat="server" Text="下载二维码" 
                    onclick="Button6_Click1" /><br />
                <br />
                scene_id 场景ID：<asp:TextBox 
                    ID="TextBox7" runat="server" ></asp:TextBox>
            </div>
        </div>
        <div style="height: 170px; border: 1px solid red;">
            <h3 style="text-align: center">
                下载上传文件</h3>
            <div style="clear: left; width: 350px; height: 100px; border: 1px solid Gray; float: left">
                <asp:Button ID="Button14" runat="server" Text="上传媒体文件" 
                    onclick="Button14_Click" />
                <p>
                    选择本地文件上传：<asp:FileUpload ID="FileUpload1" runat="server" />
                    上传成功后微信返回文件Id： afda5245a21f1dfa5a 
                </p>
                <p>
                    &nbsp;</p>
            </div>
            <div style="float: left; width: 450px; height: 100px; border: 1px solid red;">
                <asp:Button ID="Button15" runat="server" Text="下载媒体文件" 
                    onclick="Button15_Click" style="height: 21px" /><br />
                <br />
                文件保存在 Downloads/Weixin/
            </div>
        </div>
    </div>
    </form>
    <p>
    </p>
</body>
</html>
