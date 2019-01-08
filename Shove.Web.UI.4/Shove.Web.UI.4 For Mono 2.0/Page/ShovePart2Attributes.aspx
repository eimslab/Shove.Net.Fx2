<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShovePart2Attributes.aspx.cs" Inherits="Shove.Web.UI.ShovePart2Attributes" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑窗口控件属性</title>
    <base target="_self" />
    <style type="text/css">
        body
        {
            scrollbar-face-color: #CECBCE;
            scrollbar-highlight-color: WHITE;
            scrollbar-shadow-color: #848284;
            scrollbar-3dlight-color: #848284;
            scrollbar-arrow-color: WHITE;
            scrollbar-track-color: #EAEAEA;
            scrollbar-darkshadow-color: WHITE;
            font-family: "宋体";
            color: #3C3C3C;
            font-size: 9pt;
            line-height: 14pt;
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 0px;
        }
    </style>

    <script type="text/javascript">
    function LeftPad(Str, Len,  PadChar)
    {
        while (Str.length < Len)
        {
            Str = PadChar + Str;
        }
        
        return Str;
    }

    function btnOK_onclick()
    {
        var o_ddlAscxFileName = document.getElementById("ddlAscxFileName");
        var o_tbCssClass = document.getElementById("tbCssClass");
        var o_hiControlAttributes = document.getElementById("hiControlAttributes");

        var AscxFileName = o_ddlAscxFileName.value;
        
        var oldascxFileName = document.getElementById("hiascxFileName").value;
        
        var ControlAttributes = o_hiControlAttributes.value;
        
        if (AscxFileName == "不需要填充内容")
        {
            AscxFileName = "";
            ControlAttributes = "";
        }
        if((AscxFileName != oldascxFileName) && (oldascxFileName != ""))
        {
           ControlAttributes = "";
        }
                
        var ReturnParams = new Object();
        
        ReturnParams.AscxFileName = AscxFileName;       
        ReturnParams.CssClass = o_tbCssClass.value;
        ReturnParams.ControlAttributes = ControlAttributes;

        window.returnValue = ReturnParams;
        window.opener = null;
        window.close();
       
        return true;
    }
    
    function btnCancel_onclick()
    {
        window.opener = null;
        window.close();
        
        return true;
    }
    
    function btnEditAscxAttribute_onclick()
    {
        var o_ddlAscxFileName = document.getElementById("ddlAscxFileName");
        var o_hiControlAttributes = document.getElementById("hiControlAttributes");
        var oldascxFileName = document.getElementById("hiascxFileName").value;
        var SiteDir = document.getElementById("hiSiteDir").value;
        var AscxFileName = o_ddlAscxFileName.value;
        var UserID = document.getElementById("hiUserID").value;
            
        if(AscxFileName != oldascxFileName)
        {
            o_hiControlAttributes.value = "";
        }
        
        if ((o_ddlAscxFileName.value != "不需要填充内容") )
        { 
            var AscxParams = new Object();

            AscxParams.SiteDir = SiteDir;
            AscxParams.UserID = UserID;
            AscxParams.AscxFileName = o_ddlAscxFileName.value;
            AscxParams.ControlAttributes = o_hiControlAttributes.value;

            var returnValue = window.showModalDialog("ShovePart2_AscxAttribute.htm", AscxParams, "dialogWidth:550px;");

            if((returnValue == null) || (returnValue == "undefined"))
            {
                return false;
            }

            document.getElementById("hiControlAttributes").value = returnValue;
        }
        else
        {
            if (o_ddlAscxFileName.value == "")
            {
                alert('请先选择填充内容！');
            }
        }
    }
          
    function btnChooseUserControl()
    {
        var maxWidth = 810 + "px";   
        var maxHeight = screen.availHeight + "px"; 

        var UserID = document.getElementById("hiUserID").value;

        window.open("ShovePart2UserControlLibrary.aspx?UserID=" + UserID, "", "dialogWidth=" + maxWidth + ",dialogHeight=" + maxWidth + ",dialogTop:10px");
    }
    
    </script>

</head>
<body style="margin: 0px; background-color: buttonface;">
    <object id="dlgHelper" classid="clsid:3050f819-98b5-11cf-bb82-00aa00bdce0b" width="0px" height="0px">
    </object>
    <form id="form1" runat="server">
    <div style="clear: both; height: auto;">
        <asp:Label ID="Label1" runat="server" Text="调整窗口控件属性" Style="z-index: 0; position: absolute; left: 26px; top: 8px;" Font-Bold="True"></asp:Label>
        <hr style="border: thick solid #0099ff; width: 664px; position: absolute; left: 14px; top: 31px;" />
        <asp:TextBox ID="tbCssClass" runat="server"             
            
            Style="z-index: 17; left: 80px; position: absolute; top: 52px; width: 120px; right: 999px;"></asp:TextBox>
        &nbsp;<asp:Label ID="Label7" runat="server" 
            Style="z-index: 19; left: 229px; position: absolute; top: 51px; width: 70px;" 
            Text="窗口内容："></asp:Label>
        <asp:DropDownList ID="ddlAscxFileName" runat="server" 
            Style="z-index: 20; left: 308px; position: absolute; top: 49px;" 
            Width="200px">
        </asp:DropDownList>
        <input id="btnChooseUC" runat="server" type="button" value="..." 
            disabled="disabled" 
            style="z-index: 20; left: 516px; position: absolute; top: 47px; width: 20px;" 
            name="btnChooseUC" onclick="return btnChooseUserControl()" />
        &nbsp;
        <hr style="border: thick solid #0099ff; width: 664px; position: absolute; left: 14px; top: 70px;" />
        <input id="btnOK" type="button" value="确定" onclick="return btnOK_onclick();" 
            style="z-index: 24; position: absolute; left: 453px; top: 87px; width: 60px;" />
        <input id="btnCancel" type="button" value="取消" onclick="return btnCancel_onclick();" style="z-index: 25; position: absolute; left: 533px; top: 87px; width: 60px;" />
        <asp:CheckBox ID="cbTopUpLimit" runat="server" Text="限制自动上移" Style="z-index: 25; position: absolute; left: 340px; top: 74px;display:none;" Width="97px" />
        <input type="button" value="编辑窗口内容属性" id="btnEditAscxAttribute" 
            onclick="return btnEditAscxAttribute_onclick();" 
            style="z-index: 16; left: 544px; position: absolute; top: 47px; width: 136px;" />
        <input type="hidden" ID="hiControlAttributes" runat="server" Style="z-index: 0; position: absolute; left: 226px; top: 8px;" />
        <input type="hidden" ID="hiascxFileName" runat="server" Style="z-index: 0; position: absolute; left: 226px; top: 8px;" />
        <input type="hidden" ID="hiSiteDir" runat="server" Style="z-index: 0; position: absolute; left: 226px; top: 8px;" />
        <input type="hidden" ID="hiUserID" runat="server" Style="z-index: 0; position: absolute; left: 226px; top: 8px;" />
        <input type="hidden" ID="hiSiteID" runat="server" Style="z-index: 0; position: absolute; left: 226px; top: 8px;" />
    </div>
    <p>
        <asp:Label ID="Label16" runat="server" 
            Style="z-index: 16; left: 18px; position: absolute; top: 51px" Text="样式名称：" 
            Width="61px"></asp:Label>
        </p>
    </form>
</body>
</html>
