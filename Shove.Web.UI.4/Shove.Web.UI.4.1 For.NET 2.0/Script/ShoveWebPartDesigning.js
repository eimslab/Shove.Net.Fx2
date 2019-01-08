
/*
ShoveWebPart 设计时代码。
作者：shove
时间：2007-8-2
*/

var ShoveWebUI_ShoveWebPart_d = 5;
var ShoveWebUI_ShoveWebPart_l, ShoveWebUI_ShoveWebPart_t, ShoveWebUI_ShoveWebPart_r, ShoveWebUI_ShoveWebPart_b, ShoveWebUI_ShoveWebPart_ex, ShoveWebUI_ShoveWebPart_ey, ShoveWebUI_ShoveWebPart_cur;

var ShoveWebUI_ShoveWebPart_oldX = 0;
var ShoveWebUI_ShoveWebPart_oldY = 0;

var ShoveWebUI_ShoveWebPart_moving = false;
var ShoveWebUI_ShoveWebPart_resizing = false;

var ShoveWebUI_ShoveWebPart_KeyAdjusted = false;

// 获取所有选中的 Part
function ShoveWebUI_ShoveWebPart_GetSelectedParts() {
    var objSelectedParts = [];
    var objParts = ShoveWebUI_ShoveWebPart_GetParts(false, false);

    for (var i = 0; i < objParts.length; i++) {
        // 如果不是选中的控件，不处理
        var divTitleBar = document.getElementById(objParts[i].id + "_divTitleBar");

        if (!divTitleBar) {
            continue;
        }

        if (divTitleBar.style.backgroundColor != "red") {
            continue;
        }

        objSelectedParts.push(objParts[i]);
    }

    return objSelectedParts;
}

// 键按下进行修改、移动等等
function ShoveWebUI_ShoveWebPart_OnKeyDown(e, SiteID, PageName) {
    e = e || event;

    // 上 -> 38
    // 下 -> 40
    // 左 -> 37
    // 右 -> 39
    // Delete -> 46
    // C  -> 67
    // V  -> 86
    // S  -> 83
    // Enter -> 13

    if ((e.keyCode != 38) && (e.keyCode != 40) && (e.keyCode != 37) && (e.keyCode != 39) && (e.keyCode != 46) && (e.keyCode != 67) && (e.keyCode != 86) && (e.keyCode != 83) && (e.keyCode != 13)) {
        return;
    }

    if ((e.keyCode == 86) && e.ctrlKey) // Ctrl + V
    {
        ShoveWebUI_ShoveWebPart_OnPaste(e, SiteID, PageName);

        return;
    }

    var objParts = ShoveWebUI_ShoveWebPart_GetSelectedParts();

    if (objParts.length < 1) {
        return;
    }

    if ((e.keyCode == 67) && e.ctrlKey) // Ctrl + C
    {
        ShoveWebUI_ShoveWebPart_OnCopy(e, SiteID, PageName, objParts);

        return;
    }

    var i;
    var Limit = 0;
    var Step = 1;

    switch (e.keyCode) {
        case 38:
            var MinTop = -1;

            for (i = 0; i < objParts.length; i++) {
                var Top = parseInt(objParts[i].style.top.replace("px", ""));

                if (MinTop == -1) {
                    MinTop = Top;
                }

                if (MinTop > Top) {
                    MinTop = Top;
                }
            }

            Limit = parseInt(ShoveWebUI_ShoveWebPart_GetParentTop(objParts[0]).replace("px", ""));

            if (MinTop <= Limit) {
                return;
            }

            if (e.ctrlKey || e.shiftKey) {
                if (MinTop - Limit < 5) {
                    Step = MinTop - Limit;
                }
                else {
                    Step = 5;
                }
            }

            for (i = 0; i < objParts.length; i++) {
                var Top = parseInt(objParts[i].style.top.replace("px", ""));

                objParts[i].style.top = (Top - Step) + "px";
            }

            ShoveWebUI_ShoveWebPart_KeyAdjusted = true;
            break;

        case 40:
            if (e.ctrlKey || e.shiftKey) {
                Step = 5;
            }

            for (i = 0; i < objParts.length; i++) {
                var Top = parseInt(objParts[i].style.top.replace("px", ""));

                objParts[i].style.top = (Top + Step) + "px";
            }

            ShoveWebUI_ShoveWebPart_KeyAdjusted = true;
            break;

        case 37:
            var MinLeft = -1;

            for (i = 0; i < objParts.length; i++) {
                var Left = parseInt(objParts[i].style.left.replace("px", ""));

                if (MinLeft == -1) {
                    MinLeft = Left;
                }

                if (MinLeft > Left) {
                    MinLeft = Left;
                }
            }

            Limit = parseInt(ShoveWebUI_ShoveWebPart_GetParentLeft(objParts[0]).replace("px", ""));

            if (MinLeft <= Limit) {
                return;
            }

            if (e.ctrlKey || e.shiftKey) {
                if (MinLeft - Limit < 5) {
                    Step = MinLeft - Limit;
                }
                else {
                    Step = 5;
                }
            }

            for (i = 0; i < objParts.length; i++) {
                var Left = parseInt(objParts[i].style.left.replace("px", ""));

                objParts[i].style.left = (Left - Step) + "px";
            }

            ShoveWebUI_ShoveWebPart_KeyAdjusted = true;
            break;

        case 39:
            var MaxRight = -1;

            for (i = 0; i < objParts.length; i++) {
                var Right = parseInt(objParts[i].style.left.replace("px", "")) + parseInt(objParts[i].style.width.replace("px", ""));

                if (MaxRight < Right) {
                    MaxRight = Right;
                }
            }

            Limit = (parseInt(ShoveWebUI_ShoveWebPart_GetParentLeft(objParts[0]).replace("px", "")) + parseInt(ShoveWebUI_ShoveWebPart_GetParentWidth(objParts[0]).replace("px", "")));

            if (MaxRight >= Limit) {
                return;
            }

            if (e.ctrlKey || e.shiftKey) {
                if (Limit - MaxRight < 5) {
                    Step = Limit - MaxRight;
                }
                else {
                    Step = 5;
                }
            }

            for (i = 0; i < objParts.length; i++) {
                var Left = parseInt(objParts[i].style.left.replace("px", ""));

                objParts[i].style.left = (Left + Step) + "px";
            }

            ShoveWebUI_ShoveWebPart_KeyAdjusted = true;
            break;
    }

    if (objParts.length == 1) {
        ShoveWebUI_ShoveWebPart_ShowRuleLine(objParts[0]);
    }

    e.returnValue = false;
}

// 键抬起，进行保存数据
function ShoveWebUI_ShoveWebPart_OnKeyUp(e, SiteID, PageName) {
    e = e || event;

    ShoveWebUI_ShoveWebPart_HideRuleLine();

    if (!ShoveWebUI_ShoveWebPart_KeyAdjusted && (e.keyCode != 46)) {
        return;
    }

    if ((e.keyCode != 38) && (e.keyCode != 40) && (e.keyCode != 37) && (e.keyCode != 39) && (e.keyCode != 46) && (e.keyCode != 67) && (e.keyCode != 86) && (e.keyCode != 83) && (e.keyCode != 13)) {
        return;
    }

    var objParts = ShoveWebUI_ShoveWebPart_GetSelectedParts();

    if (objParts.length < 1) {
        return;
    }

    if (ShoveWebUI_ShoveWebPart_KeyAdjusted) {
        var Datas = [];

        for (i = 0; i < objParts.length; i++) {
            var Data = objParts[i].id + "," + objParts[i].style.left + "," + objParts[i].style.top;

            Datas.push(Data);
        }

        var CallAjaxResult = Shove.Web.UI.ShoveWebPart.SaveForKeyMove(SiteID, PageName, Datas);

        ShoveWebUI_ShoveWebPart_KeyAdjusted = false;
        return;
    }

    if (e.keyCode == 46) {
        if (!confirm("确信要删除被选中的 " + objParts.length + " 个 Part 吗？")) {
            return;
        }

        var Datas = [];

        for (i = 0; i < objParts.length; i++) {
            objParts[i].style.visibility = "hidden";

            Datas.push(objParts[i].id);
        }

        var CallAjaxResult = Shove.Web.UI.ShoveWebPart.DeleteForKeyMove(SiteID, PageName, Datas);

        return;
    }
}

function ShoveWebUI_ShoveWebPart_OnCopy(e, SiteID, PageName, objParts) {
    if ((objParts == null) || (objParts.length < 1)) {
        return;
    }

    var Value = "";

    for (var i = 0; i < objParts.length; i++) {
        if (Value != "") {
            Value += ",";
        }

        Value += (PageName + "." + objParts[i].id);
    }

    window.clipboardData.setData("text", Value);
}

function ShoveWebUI_ShoveWebPart_OnPaste(e, SiteID, PageName) {
    var SourcePartList = window.clipboardData.getData("text");

    if ((SourcePartList == null) || (SourcePartList == "")) {
        return;
    }

    var CallAjaxResult = Shove.Web.UI.ShoveWebPart.Paste(SiteID, PageName, SourcePartList);

    if (CallAjaxResult.value == "") {
        window.location.href = window.location.href;
    }
}

function ShoveWebUI_ShoveWebPart_OnMouseDown(e, sender, SiteID, PageName) {
    e = e || event;

    if (e.button != 1) {
        return;
    }

    // 绑定按键处理事件
    ShoveWebUI_ShoveWebPart_SetOnKeyDown(SiteID, PageName);

    // 增加对齐线
    ShoveWebUI_ShoveWebPart_DrawRuleLine();

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        return;
    }

    ShoveWebUI_ShoveWebPart_SetZindexToTop(e, sender, SiteID, PageName);
    ShoveWebUI_ShoveWebPart_SetSelected(e, sender);

    if (e.button == 1 && sender.style.cursor == "move") {
        ShoveWebUI_ShoveWebPart_moving = true;
        sender.setCapture();

        ShoveWebUI_ShoveWebPart_oldX = e.screenX;
        ShoveWebUI_ShoveWebPart_oldY = e.screenY;

        return;
    }

    if (e.button == 1 && sender.style.cursor) {
        ShoveWebUI_ShoveWebPart_resizing = true;
        sender.setCapture();
    }
}

function ShoveWebUI_ShoveWebPart_OnMouseUp(e, sender, SiteID, PageName) {
    e = e || event;

    ShoveWebUI_ShoveWebPart_HideRuleLine();

    if (e.button != 1) {
        return;
    }

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        return;
    }

    if (e.button == 1 && (ShoveWebUI_ShoveWebPart_resizing || ShoveWebUI_ShoveWebPart_moving)) {
        ShoveWebUI_ShoveWebPart_resizing = false;
        ShoveWebUI_ShoveWebPart_moving = false;

        sender.releaseCapture();

        var PrimitiveHeight;

        if (sender.style.height == "auto") {
            PrimitiveHeight = sender.scrollHeight + "px";
        }
        else {
            PrimitiveHeight = sender.style.height;
        }

        var CallAjaxResult = Shove.Web.UI.ShoveWebPart.Save(sender.id, SiteID, PageName, sender.style.left, sender.style.top, sender.style.width, sender.style.height, PrimitiveHeight, ShoveWebUI_ShoveWebPart_GetParentLeft(sender));
    }
}

function ShoveWebUI_ShoveWebPart_OnMouseMove(e, sender, SiteID, PageName) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        sender.style.cursor = "";

        return;
    }

    if (ShoveWebUI_ShoveWebPart_resizing || ShoveWebUI_ShoveWebPart_moving) {
        var CurrentDivLeft = parseInt(sender.style.left.replace("px", ""));
        var CurrentDivTop = parseInt(sender.style.top.replace("px", ""));
        var CurrentDivWidth = parseInt(sender.style.width.replace("px", ""));
        var CurrentDivHeight = parseInt(sender.style.height.replace("px", ""));

        var CurrentDivBorder = 0;

        if (sender.style.borderRightWidth != "") {
            CurrentDivBorder = parseInt(sender.style.borderRightWidth.replace("px", ""));
        }

        if (ShoveWebUI_ShoveWebPart_moving) {
            var x = e.screenX;
            var y = e.screenY;

            var parentNodeDivLeft = 0;
            var parentNodeDivTop = 0;
            var parentNodeDivWidth = 0;

            if (sender.parentNode) {
                parentNodeDivLeft = parseInt(ShoveWebUI_ShoveWebPart_GetParentLeft(sender).replace("px", ""));
                parentNodeDivTop = parseInt(ShoveWebUI_ShoveWebPart_GetParentTop(sender).replace("px", ""));
                parentNodeDivWidth = parseInt(ShoveWebUI_ShoveWebPart_GetParentWidth(sender).replace("px", ""));

                var CurrentDivleftmargin = CurrentDivLeft + (x - ShoveWebUI_ShoveWebPart_oldX);

                if (CurrentDivleftmargin < parentNodeDivLeft) {
                    CurrentDivleftmargin = parentNodeDivLeft;
                }

                var CurrentDivtopmargin = CurrentDivTop + (y - ShoveWebUI_ShoveWebPart_oldY);

                if (CurrentDivtopmargin < parentNodeDivTop) {
                    CurrentDivtopmargin = parentNodeDivTop;
                }

                if (CurrentDivleftmargin + CurrentDivWidth > parentNodeDivWidth + parentNodeDivLeft) {
                    CurrentDivleftmargin = parentNodeDivWidth + parentNodeDivLeft - CurrentDivWidth - CurrentDivBorder * 2;
                }

                if (CurrentDivTop < parentNodeDivTop) {
                    CurrentDivtopmargin = parentNodeDivTop;
                }
            }

            sender.style.left = CurrentDivleftmargin + "px";
            sender.style.top = CurrentDivtopmargin + "px";

            ShoveWebUI_ShoveWebPart_oldX = x;
            ShoveWebUI_ShoveWebPart_oldY = y;
        }
        else if (ShoveWebUI_ShoveWebPart_resizing) {
            var dx = e.screenX - ShoveWebUI_ShoveWebPart_ex;
            var dy = e.screenY - ShoveWebUI_ShoveWebPart_ey;

            var parentNodeDivLeft = 0;
            var parentNodeDivTop = 0;
            var parentNodeDivWidth = 0;

            if (sender.parentNode) {
                parentNodeDivLeft = parseInt(ShoveWebUI_ShoveWebPart_GetParentLeft(sender).replace("px", ""));
                parentNodeDivTop = parseInt(ShoveWebUI_ShoveWebPart_GetParentTop(sender).replace("px", ""));
                parentNodeDivWidth = parseInt(ShoveWebUI_ShoveWebPart_GetParentWidth(sender).replace("px", ""));
            }

            if (ShoveWebUI_ShoveWebPart_cur.indexOf("w") > -1) {
                ShoveWebUI_ShoveWebPart_l += dx;
            }
            else if (ShoveWebUI_ShoveWebPart_cur.indexOf("e") > -1) {
                ShoveWebUI_ShoveWebPart_r += dx;
            }
            if (ShoveWebUI_ShoveWebPart_cur.indexOf("n") > -1) {
                ShoveWebUI_ShoveWebPart_t += dy;
            }
            else if (ShoveWebUI_ShoveWebPart_cur.indexOf("s") > -1) {
                ShoveWebUI_ShoveWebPart_b += dy;
            }

            var s = sender.style;

            if (ShoveWebUI_ShoveWebPart_r - ShoveWebUI_ShoveWebPart_l > 2 * ShoveWebUI_ShoveWebPart_d) {
                s.left = ShoveWebUI_ShoveWebPart_l + "px";
                s.width = (ShoveWebUI_ShoveWebPart_r - ShoveWebUI_ShoveWebPart_l) + "px";
            }

            if (ShoveWebUI_ShoveWebPart_b - ShoveWebUI_ShoveWebPart_t > 2 * ShoveWebUI_ShoveWebPart_d) {
                s.top = ShoveWebUI_ShoveWebPart_t + "px";
                s.height = (ShoveWebUI_ShoveWebPart_b - ShoveWebUI_ShoveWebPart_t) + "px";
            }

            if (((s.width.replace("px", "")) >= (parentNodeDivWidth - CurrentDivBorder * 2))) {
                s.left = parentNodeDivLeft + "px";
                s.width = (parentNodeDivWidth - CurrentDivBorder * 2) + "px";
            }

            ShoveWebUI_ShoveWebPart_ex += dx;
            ShoveWebUI_ShoveWebPart_ey += dy;
        }

        ShoveWebUI_ShoveWebPart_ShowRuleLine(sender);
    }
    else// if (e.srcElement == sender)
    {
        var x = e.offsetX;
        var y = e.offsetY;
        var c = sender.currentStyle;

        var w = parseInt(c.width);
        var h = parseInt(c.height);

        ShoveWebUI_ShoveWebPart_cur = y < ShoveWebUI_ShoveWebPart_d ? "n" : h - y < ShoveWebUI_ShoveWebPart_d ? "s" : "";
        ShoveWebUI_ShoveWebPart_cur += x < ShoveWebUI_ShoveWebPart_d ? "w" : w - x < ShoveWebUI_ShoveWebPart_d ? "e" : "";

        if (ShoveWebUI_ShoveWebPart_cur) {
            sender.style.cursor = ShoveWebUI_ShoveWebPart_cur + "-resize";
            ShoveWebUI_ShoveWebPart_l = parseInt(c.left);
            ShoveWebUI_ShoveWebPart_t = parseInt(c.top);
            ShoveWebUI_ShoveWebPart_r = ShoveWebUI_ShoveWebPart_l + w;
            ShoveWebUI_ShoveWebPart_b = ShoveWebUI_ShoveWebPart_t + h;
            ShoveWebUI_ShoveWebPart_ex = e.screenX;
            ShoveWebUI_ShoveWebPart_ey = e.screenY;
        }
        else {
            sender.style.cursor = "move";
        }
    }
}

function ShoveWebUI_ShoveWebPart_OnEditClick(e, sender, SiteID, PageName, UserID, RelativePath, AttributesPageFileName, ImageUploadDir, AscxControlFileName, ControlAttributes) {
    e = e || event;

    function UrlEncode(str)//UserID,
    {
        var strSpecial = "!\"#$%&'()*+,/:;<=>?[]^`{|}~%";
        var Result = "";

        for (var i = 0; i < str.length; i++) {
            var chr = str.charAt(i);

            if (strSpecial.indexOf(chr) < 0) {
                Result += chr;

                continue;
            }

            chr = chr.charCodeAt(0).toString(16);
            Result += "%" + chr;
        }

        return Result;
    }

    function GetIsAutoHeight() {
        return (sender.style.height == "auto");
    }

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        alert("控件已经被锁定，要对其进行编辑，请先解除锁定。");

        return;
    }

    var TitleImageUrl = "";
    var o_TitleImage = document.getElementById(sender.id + "_TitleImage");

    if (o_TitleImage) {
        TitleImageUrl = o_TitleImage.src;
    }

    var TitleImageUrlLink = "";
    var TitleImageUrlLinkTarget = "";
    var o_TitleImageLink = document.getElementById(sender.id + "_TitleImageLink");

    if (o_TitleImageLink) {
        TitleImageUrlLink = o_TitleImageLink.href;
        TitleImageUrlLinkTarget = o_TitleImageLink.target;
    }

    if (TitleImageUrlLinkTarget == "") {
        TitleImageUrlLinkTarget = "_self";
    }

    var TableContet = document.getElementById(sender.id + "_TableContent"); //获取背景图片

    var BackgroundImageUrl = TableContet.style.backgroundImage;

    var BottomImageUrl = "";
    var o_BottomImage = document.getElementById(sender.id + "_BottomImage");

    if (o_BottomImage) {
        BottomImageUrl = o_BottomImage.src;
    }

    var BottomImageUrlLink = "";
    var BottomImageUrlLinkTarget = "";
    var o_BottomImageLink = document.getElementById(sender.id + "_BottomImageLink");

    if (o_BottomImageLink) {
        BottomImageUrlLink = o_BottomImageLink.href;
        BottomImageUrlLinkTarget = o_BottomImageLink.target;
    }

    if (BottomImageUrlLinkTarget == "") {
        BottomImageUrlLinkTarget = "_self";
    }

    var IsAutoHeight = GetIsAutoHeight();

    var HorizontalAlign = "";
    var VerticalAlign = "";

    var o_ContentTD = document.getElementById(sender.id + "_ContentTD");

    if (o_ContentTD) {
        HorizontalAlign = o_ContentTD.align;
        VerticalAlign = o_ContentTD.vAlign;
    }

    switch (HorizontalAlign) {
        case "left":
            HorizontalAlign = "Left";
            break;
        case "center":
            HorizontalAlign = "Center";
            break;
        case "right":
            HorizontalAlign = "Right";
            break;
        default:
            HorizontalAlign = "NoSet";
            break;
    }

    switch (VerticalAlign) {
        case "top":
            VerticalAlign = "Top";
            break;
        case "middle":
            VerticalAlign = "Middle";
            break;
        case "bottom":
            VerticalAlign = "Bottom";
            break;
        default:
            VerticalAlign = "NoSet";
            break;
    }

    var Params = new Object();

    Params.SiteID = SiteID;
    Params.UserID = UserID;
    Params.ImageUploadDir = ImageUploadDir;
    Params.AscxControlFileName = AscxControlFileName;
    Params.HorizontalAlign = HorizontalAlign;
    Params.VerticalAlign = VerticalAlign;
    Params.Border = sender.style.border;
    Params.BackColor = sender.style.backgroundColor;
    Params.TitleImageUrl = TitleImageUrl;
    Params.BackImage = BackgroundImageUrl;
    Params.BottomImage = BottomImageUrl;
    Params.AutoHeight = (IsAutoHeight ? "True" : "False");
    Params.Left = sender.style.left;
    Params.Top = sender.style.top;
    Params.Width = sender.style.width;
    Params.Height = sender.style.height;
    Params.CssClass = sender.className;
    Params.TopUpLimit = sender.getAttribute("TopUpLimit");
    Params.TitleImageUrlLink = TitleImageUrlLink;
    Params.BottomImageUrlLink = BottomImageUrlLink;
    Params.TitleImageUrlLinkTarget = TitleImageUrlLinkTarget;
    Params.BottomImageUrlLinkTarget = BottomImageUrlLinkTarget;
    Params.ControlAttributes = ControlAttributes;

    // 调用对话框，编辑属性sender.style.backgroundImage
    var strReturn = window.showModalDialog(RelativePath + "ShoveWebUI_client/Page/ShoveWebPartAttributes.htm", Params, "dialogWidth=740px;dialogHeight=460px;center: Yes;");

    if (strReturn == null) {
        return false;
    }

    var AttributesList = new Array(25);

    AttributesList[0] = strReturn.AscxFileName;
    AttributesList[1] = strReturn.AscxAlign;
    AttributesList[2] = strReturn.AscxVAlign;
    AttributesList[3] = strReturn.BorderStyle;
    AttributesList[4] = strReturn.BorderWidth;
    AttributesList[5] = strReturn.BorderColor;
    AttributesList[6] = strReturn.BackColor;
    AttributesList[7] = strReturn.TitleImage;
    AttributesList[8] = strReturn.BackgroundImage;
    AttributesList[9] = strReturn.BottomImage;
    AttributesList[10] = strReturn.AutoHeight;
    AttributesList[11] = strReturn.Left;
    AttributesList[12] = strReturn.Top;
    AttributesList[13] = strReturn.Width;
    AttributesList[14] = strReturn.Height;
    AttributesList[15] = strReturn.Height;
    AttributesList[16] = strReturn.CssClass;
    AttributesList[17] = strReturn.TopUpLimit;
    AttributesList[18] = strReturn.TitleImageUrlLink;
    AttributesList[19] = strReturn.BottomImageUrlLink;
    AttributesList[20] = strReturn.TitleImageUrlLinkTarget;
    AttributesList[21] = strReturn.BottomImageUrlLinkTarget;
    AttributesList[22] = strReturn.ControlAttributes;
    AttributesList[23] = strReturn.ApplyToAllPage;
    AttributesList[24] = strReturn.AddToNoExistPage;

    // 根据返回值设置属性
    // strReturn 的构成顺序：AscxControlFileName,HorizontalAlign,VerticalAlign,BorderStyle,BorderWidth,BorderColor,BackColor,TitleImageUrl,BackImageUrl,BottomImageUrl,AutoHeight,Left,Top,Width,Height,CssClass,TopUpLimit,TitleImageUrlLink,BottomImageUrlLink,TitleImageUrlLinkTarget,BottomImageUrlLinkTarget,ControlAttributes,ApplyToAllPage,AddToNoExistPage

    if (AttributesList[1].toLowerCase() == "notset") {
        AttributesList[1] = "Center";
    }

    if (AttributesList[2].toLowerCase() == "notset") {
        AttributesList[2] = "Middle";
    }

    if (AttributesList[3].toLowerCase() == "notset") {
        AttributesList[3] = "None";
    }

    var isAscxControlFileNameChanged = false;

    if (AscxControlFileName != AttributesList[0]) {
        isAscxControlFileNameChanged = true;
    }

    sender.style.textAlign = AttributesList[1].toLowerCase();
    sender.style.verticalAlign = AttributesList[2].toLowerCase();
    sender.style.backgroundColor = AttributesList[6];
    sender.style.border = AttributesList[5] + " " + AttributesList[4] + " " + AttributesList[3];

    if (o_ContentTD) {
        o_ContentTD.align = AttributesList[1].toLowerCase();
        o_ContentTD.vAlign = AttributesList[2].toLowerCase();
    }

    if ((AttributesList[5].toLowerCase() == "gray") && (AttributesList[4].toLowerCase() == "1px") && (AttributesList[3].toLowerCase() == "dotted")) {
        AttributesList[3] = "None";
    }

    if (o_TitleImage) {
        o_TitleImage.src = RelativePath + AttributesList[7];

        if (AttributesList[7] == "") {
            o_TitleImage.style.display = "none";
        }
        else {
            o_TitleImage.style.display = "block";
        }
    }

    TableContet.style.backgroundImage = ((AttributesList[8] == "") ? "" : "url(" + RelativePath + AttributesList[8] + ")");

    if (o_BottomImage) {
        o_BottomImage.src = RelativePath + AttributesList[9];

        if (AttributesList[9] == "") {
            o_BottomImage.style.display = "none";
        }
        else {
            o_BottomImage.style.display = "block";
        }
    }

    var IsAutoHeightChanged = false;

    if (IsAutoHeight != (AttributesList[10].toLowerCase() == "true")) {
        IsAutoHeightChanged = true;
    }

    sender.style.left = AttributesList[11];
    sender.style.top = AttributesList[12];
    sender.style.width = AttributesList[13];
    sender.style.height = AttributesList[14];

    if ((AttributesList[14] == "auto") || (AttributesList[14] == "")) {
        AttributesList[15] = sender.scrollHeight + "px";
    }

    sender.className = AttributesList[16];

    sender.setAttribute("TopUpLimit", AttributesList[17]);

    if (o_TitleImageLink) {
        o_TitleImageLink.href = AttributesList[18];
        o_TitleImageLink.target = AttributesList[20];
    }

    if (o_BottomImageLink) {
        o_BottomImageLink.href = AttributesList[19];
        o_BottomImageLink.target = AttributesList[21];
    }

    var IsControlAttributeChanged = false;

    if (sender.getAttribute("ControlAttributes") != AttributesList[22]) {
        IsControlAttributeChanged = true;

        sender.setAttribute("ControlAttributes", AttributesList[22]);
    }

    var CallAjaxResult = Shove.Web.UI.ShoveWebPart.Edit(sender.id, SiteID, PageName, AttributesList);

    if (isAscxControlFileNameChanged) {
        window.location.href = window.location.href;

        return false;
    }

    if (IsAutoHeightChanged || IsControlAttributeChanged) {
        return true;
    }

    return false;
}

// 选中
function ShoveWebUI_ShoveWebPart_SetSelected(e, sender) {
    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        return;
    }

    var divTitleBar;

    // 没有按下 Ctrl 或者 Shift，将其他 Part 置为未选中状态
    if ((!e.ctrlKey) && (!e.shiftKey)) {
        var objParts = ShoveWebUI_ShoveWebPart_GetParts(false, false);

        for (var i = 0; i < objParts.length; i++) {
            if (sender.id == objParts[i].id) {
                continue;
            }

            divTitleBar = document.getElementById(objParts[i].id + "_divTitleBar");

            if (!divTitleBar) {
                continue;
            }

            if (divTitleBar.style.backgroundColor != "red") {
                continue;
            }

            divTitleBar.style.backgroundColor = "blue";
            divTitleBar.style.filter = "alpha(opacity=40)";
        }
    }

    divTitleBar = document.getElementById(sender.id + "_divTitleBar");

    if (!divTitleBar) {
        return;
    }

    if (divTitleBar.style.backgroundColor == "red") {
        if (e.ctrlKey || e.shiftKey) {
            divTitleBar.style.backgroundColor = "blue";
            divTitleBar.style.filter = "alpha(opacity=40)";
        }
    }
    else {
        divTitleBar.style.backgroundColor = "red";
        divTitleBar.style.filter = "alpha(opacity=60)";
    }
}

// 锁定
function ShoveWebUI_ShoveWebPart_OnLockClick(e, sender, SiteID, PageName, btnLock, divTitle) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        LockState = "False";

        btnLock.setAttribute("title", "锁定此窗口");
        divTitle.style.backgroundColor = "blue";
        divTitle.style.filter = "alpha(opacity=40)";
        sender.setAttribute("Lock", LockState);
    }
    else {
        LockState = "True";

        btnLock.setAttribute("title", "解除锁定此窗口");
        divTitle.style.backgroundColor = "black";
        divTitle.style.filter = "alpha(opacity=60)";
        sender.setAttribute("Lock", LockState);
    }

    var CallAjaxResult = Shove.Web.UI.ShoveWebPart.SaveLock(sender.id, SiteID, PageName, (LockState == "True") ? true : false);
}

function ShoveWebUI_ShoveWebPart_OnApplyToAllClick(e, sender, SiteID, PageName) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        alert("控件已经被锁定，要对其进行编辑，请先解除锁定。");

        return;
    }

    if (!confirm("确信要将此 Part 的属性应用到其他所有页面吗？")) {
        return;
    }

    var CallAjaxResult = Shove.Web.UI.ShoveWebPart.SaveApplyToAll(sender.id, SiteID, PageName, true, false, sender.getAttribute("ControlAttributes"));

    alert("应用成功。");
}

function ShoveWebUI_ShoveWebPart_OnToBackgroundClick(e, sender, SiteID, PageName) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        alert("控件已经被锁定，要对其进行编辑，请先解除锁定。");

        return;
    }

    var objParts = ShoveWebUI_ShoveWebPart_GetParts(true, false);

    if (objParts.length == 1) {
        alert("控件已经在最底层。");

        return;
    }

    var index = sender.style.zIndex;

    for (var i = 0; i < objParts.length; i++) {
        if (sender.id == objParts[i].id) {
            continue;
        }

        if (index >= objParts[i].style.zIndex) {
            index = objParts[i].style.zIndex - 1;
        }
    }

    if (index != sender.style.zIndex) {
        sender.style.zIndex = index;

        var CallAjaxResult = Shove.Web.UI.ShoveWebPart.ToBackground(sender.id, SiteID, PageName, sender.style.zIndex);
    }
    else {
        alert("控件已经在最底层。");

        return;
    }
}

function ShoveWebUI_ShoveWebPart_SetZindexToTop(e, sender, SiteID, PageName) {
    if (sender.style.zIndex == 10000) {
        return;
    }

    var objParts = ShoveWebUI_ShoveWebPart_GetParts(true, false);
    var objOtherZIndexList = "";
    var i;

    for (i = 0; i < objParts.length; i++) {
        var index = 10000;

        if (sender.id == objParts[i].id) {
            objParts[i].style.zIndex = index;
        }
        else {
            index = objParts[i].style.zIndex - 1;

            if (index < 0) {
                index = 0;
            }

            try {
                objParts[i].style.zIndex = index;
            }
            catch (e)
            { }
        }

        objOtherZIndexList += objParts[i].id + ":" + index + ";";
    }

    var CallAjaxResult = Shove.Web.UI.ShoveWebPart.SaveZIndex(sender.id, SiteID, PageName, objOtherZIndexList);
}

function ShoveWebUI_ShoveWebPart_OnCloseClick(e, sender, SiteID, PageName, CloseConfirmText) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        alert("控件已经被锁定，要对其进行编辑，请先解除锁定。");

        return;
    }

    if (CloseConfirmText != "") {
        if (!confirm(CloseConfirmText)) {
            return;
        }
    }

    sender.style.visibility = "hidden";

    var CallAjaxResult = Shove.Web.UI.ShoveWebPart.Close(sender.id, SiteID, PageName);
}

function ShoveWebUI_ShoveWebPart_SetOnKeyDown(SiteID, PageName) {
    if (!document.body.onkeydown) {
        document.onkeydown = function() { ShoveWebUI_ShoveWebPart_OnKeyDown(event, SiteID, PageName) };
        document.onkeyup = function() { ShoveWebUI_ShoveWebPart_OnKeyUp(event, SiteID, PageName) };
    }
}

function ShoveWebUI_ShoveWebPart_DrawRuleLine() {
    if (!document.getElementById("ShoveWebUI_ShoveWebPart_LineH1")) {
        var ShoveWebUI_ShoveWebPart_LineH1 = document.createElement("img");
        ShoveWebUI_ShoveWebPart_LineH1.id = "ShoveWebUI_ShoveWebPart_LineH1";
        ShoveWebUI_ShoveWebPart_LineH1.setAttribute("src", "about:blank");
        ShoveWebUI_ShoveWebPart_LineH1.style.position = "absolute";
        ShoveWebUI_ShoveWebPart_LineH1.style.setExpression("width", "document.body.clientWidth || document.documentElement.clientWidth");
        ShoveWebUI_ShoveWebPart_LineH1.style.height = "1px";
        ShoveWebUI_ShoveWebPart_LineH1.style.setExpression("left", "document.body.scrollLeft || document.documentElement.scrollLeft");
        ShoveWebUI_ShoveWebPart_LineH1.style.top = "0px";
        ShoveWebUI_ShoveWebPart_LineH1.style.backgroundColor = "red";
        ShoveWebUI_ShoveWebPart_LineH1.style.zIndex = 10001;
        ShoveWebUI_ShoveWebPart_LineH1.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShoveWebPart_LineH1);

        var ShoveWebUI_ShoveWebPart_LineH2 = document.createElement("img");
        ShoveWebUI_ShoveWebPart_LineH2.id = "ShoveWebUI_ShoveWebPart_LineH2";
        ShoveWebUI_ShoveWebPart_LineH2.setAttribute("src", "about:blank");
        ShoveWebUI_ShoveWebPart_LineH2.style.position = "absolute";
        ShoveWebUI_ShoveWebPart_LineH2.style.setExpression("width", "document.body.clientWidth || document.documentElement.clientWidth");
        ShoveWebUI_ShoveWebPart_LineH2.style.height = "1px";
        ShoveWebUI_ShoveWebPart_LineH2.style.setExpression("left", "document.body.scrollLeft || document.documentElement.scrollLeft");
        ShoveWebUI_ShoveWebPart_LineH2.style.top = "0px";
        ShoveWebUI_ShoveWebPart_LineH2.style.backgroundColor = "red";
        ShoveWebUI_ShoveWebPart_LineH2.style.zIndex = 10001;
        ShoveWebUI_ShoveWebPart_LineH2.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShoveWebPart_LineH2);

        var ShoveWebUI_ShoveWebPart_LineH3 = document.createElement("img");
        ShoveWebUI_ShoveWebPart_LineH3.id = "ShoveWebUI_ShoveWebPart_LineH3";
        ShoveWebUI_ShoveWebPart_LineH3.setAttribute("src", "about:blank");
        ShoveWebUI_ShoveWebPart_LineH3.style.position = "absolute";
        ShoveWebUI_ShoveWebPart_LineH3.style.setExpression("width", "document.body.clientWidth || document.documentElement.clientWidth");
        ShoveWebUI_ShoveWebPart_LineH3.style.height = "1px";
        ShoveWebUI_ShoveWebPart_LineH3.style.setExpression("left", "document.body.scrollLeft || document.documentElement.scrollLeft");
        ShoveWebUI_ShoveWebPart_LineH3.style.top = "0px";
        ShoveWebUI_ShoveWebPart_LineH3.style.backgroundColor = "blue";
        ShoveWebUI_ShoveWebPart_LineH3.style.zIndex = 10001;
        ShoveWebUI_ShoveWebPart_LineH3.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShoveWebPart_LineH3);

        var ShoveWebUI_ShoveWebPart_LineV1 = document.createElement("img");
        ShoveWebUI_ShoveWebPart_LineV1.id = "ShoveWebUI_ShoveWebPart_LineV1";
        ShoveWebUI_ShoveWebPart_LineV1.style.position = "absolute";
        ShoveWebUI_ShoveWebPart_LineV1.setAttribute("src", "about:blank");
        ShoveWebUI_ShoveWebPart_LineV1.style.setExpression("height", "document.body.clientHeight || document.documentElement.clientHeight");
        ShoveWebUI_ShoveWebPart_LineV1.style.width = "1px";
        ShoveWebUI_ShoveWebPart_LineV1.style.left = "0px";
        ShoveWebUI_ShoveWebPart_LineV1.style.setExpression("top", "document.body.scrollTop || document.documentElement.scrollTop");
        ShoveWebUI_ShoveWebPart_LineV1.style.backgroundColor = "red";
        ShoveWebUI_ShoveWebPart_LineV1.style.zIndex = 10001;
        ShoveWebUI_ShoveWebPart_LineV1.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShoveWebPart_LineV1);

        var ShoveWebUI_ShoveWebPart_LineV2 = document.createElement("img");
        ShoveWebUI_ShoveWebPart_LineV2.id = "ShoveWebUI_ShoveWebPart_LineV2";
        ShoveWebUI_ShoveWebPart_LineV2.setAttribute("src", "about:blank");
        ShoveWebUI_ShoveWebPart_LineV2.style.position = "absolute";
        ShoveWebUI_ShoveWebPart_LineV2.style.setExpression("height", "document.body.clientHeight || document.documentElement.clientHeight");
        ShoveWebUI_ShoveWebPart_LineV2.style.width = "1px";
        ShoveWebUI_ShoveWebPart_LineV2.style.left = "0px";
        ShoveWebUI_ShoveWebPart_LineV2.style.setExpression("top", "document.body.scrollTop || document.documentElement.scrollTop");
        ShoveWebUI_ShoveWebPart_LineV2.style.backgroundColor = "red";
        ShoveWebUI_ShoveWebPart_LineV2.style.zIndex = 10001;
        ShoveWebUI_ShoveWebPart_LineV2.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShoveWebPart_LineV2);

        var ShoveWebUI_ShoveWebPart_LineV3 = document.createElement("img");
        ShoveWebUI_ShoveWebPart_LineV3.id = "ShoveWebUI_ShoveWebPart_LineV3";
        ShoveWebUI_ShoveWebPart_LineV3.setAttribute("src", "about:blank");
        ShoveWebUI_ShoveWebPart_LineV3.style.position = "absolute";
        ShoveWebUI_ShoveWebPart_LineV3.style.setExpression("height", "document.body.clientHeight || document.documentElement.clientHeight");
        ShoveWebUI_ShoveWebPart_LineV3.style.width = "1px";
        ShoveWebUI_ShoveWebPart_LineV3.style.left = "0px";
        ShoveWebUI_ShoveWebPart_LineV3.style.setExpression("top", "document.body.scrollTop || document.documentElement.scrollTop");
        ShoveWebUI_ShoveWebPart_LineV3.style.backgroundColor = "blue";
        ShoveWebUI_ShoveWebPart_LineV3.style.zIndex = 10001;
        ShoveWebUI_ShoveWebPart_LineV3.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShoveWebPart_LineV3);
    }
}

function ShoveWebUI_ShoveWebPart_HideRuleLine() {
    var ShoveWebUI_ShoveWebPart_LineH1 = document.getElementById("ShoveWebUI_ShoveWebPart_LineH1");
    var ShoveWebUI_ShoveWebPart_LineH2 = document.getElementById("ShoveWebUI_ShoveWebPart_LineH2");
    var ShoveWebUI_ShoveWebPart_LineH3 = document.getElementById("ShoveWebUI_ShoveWebPart_LineH3");
    var ShoveWebUI_ShoveWebPart_LineV1 = document.getElementById("ShoveWebUI_ShoveWebPart_LineV1");
    var ShoveWebUI_ShoveWebPart_LineV2 = document.getElementById("ShoveWebUI_ShoveWebPart_LineV2");
    var ShoveWebUI_ShoveWebPart_LineV3 = document.getElementById("ShoveWebUI_ShoveWebPart_LineV3");

    if (!ShoveWebUI_ShoveWebPart_LineH1) {
        return;
    }

    ShoveWebUI_ShoveWebPart_LineH1.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineH2.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineH3.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineV1.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineV2.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineV3.style.visibility = "hidden";
}

function ShoveWebUI_ShoveWebPart_ShowRuleLine(sender) {
    var ShoveWebUI_ShoveWebPart_LineH1 = document.getElementById("ShoveWebUI_ShoveWebPart_LineH1");
    var ShoveWebUI_ShoveWebPart_LineH2 = document.getElementById("ShoveWebUI_ShoveWebPart_LineH2");
    var ShoveWebUI_ShoveWebPart_LineH3 = document.getElementById("ShoveWebUI_ShoveWebPart_LineH3");
    var ShoveWebUI_ShoveWebPart_LineV1 = document.getElementById("ShoveWebUI_ShoveWebPart_LineV1");
    var ShoveWebUI_ShoveWebPart_LineV2 = document.getElementById("ShoveWebUI_ShoveWebPart_LineV2");
    var ShoveWebUI_ShoveWebPart_LineV3 = document.getElementById("ShoveWebUI_ShoveWebPart_LineV3");

    if (!ShoveWebUI_ShoveWebPart_LineH1) {
        return;
    }

    ShoveWebUI_ShoveWebPart_LineH1.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineH2.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineH3.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineV1.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineV2.style.visibility = "hidden";
    ShoveWebUI_ShoveWebPart_LineV3.style.visibility = "hidden";

    var objParts = ShoveWebUI_ShoveWebPart_GetParts(true, false);

    if (objParts.length <= 1) {
        return;
    }

    for (var i = 0; i < objParts.length; i++) {
        if (objParts[i].id == sender.id) {
            continue;
        }

        if (objParts[i].style.left == sender.style.left) {
            ShoveWebUI_ShoveWebPart_LineV1.style.left = sender.style.left;
            ShoveWebUI_ShoveWebPart_LineV1.style.visibility = "visible";
        }

        if (objParts[i].style.top == sender.style.top) {
            ShoveWebUI_ShoveWebPart_LineH1.style.top = sender.style.top;
            ShoveWebUI_ShoveWebPart_LineH1.style.visibility = "visible";
        }

        var objRight = parseInt(objParts[i].style.left.replace("px", "")) + parseInt(objParts[i].style.width.replace("px", ""));
        var senderRight = parseInt(sender.style.left.replace("px", "")) + parseInt(sender.style.width.replace("px", ""));

        if (objRight == senderRight) {
            ShoveWebUI_ShoveWebPart_LineV2.style.left = senderRight + "px";
            ShoveWebUI_ShoveWebPart_LineV2.style.visibility = "visible";
        }

        var objBottom = parseInt(objParts[i].style.top.replace("px", "")) + (objParts[i].style.height == "auto" ? parseInt(objParts[i].scrollHeight) : parseInt(objParts[i].style.height.replace("px", "")));
        var senderBottom = parseInt(sender.style.top.replace("px", "")) + (sender.style.height == "auto" ? parseInt(sender.scrollHeight) : parseInt(sender.style.height.replace("px", "")));

        if (objBottom == senderBottom) {
            ShoveWebUI_ShoveWebPart_LineH2.style.top = senderBottom + "px";
            ShoveWebUI_ShoveWebPart_LineH2.style.visibility = "visible";
        }

        // 衔接线
        if (objBottom + 1 == parseInt(sender.style.top.replace("px", ""))) {
            ShoveWebUI_ShoveWebPart_LineH3.style.top = sender.style.top;
            ShoveWebUI_ShoveWebPart_LineH3.style.visibility = "visible";
        }

        if (senderBottom - 1 == parseInt(objParts[i].style.top.replace("px", ""))) {
            ShoveWebUI_ShoveWebPart_LineH3.style.top = senderBottom + "px";
            ShoveWebUI_ShoveWebPart_LineH3.style.visibility = "visible";
        }

        if (objRight + 1 == parseInt(sender.style.left.replace("px", ""))) {
            ShoveWebUI_ShoveWebPart_LineV3.style.left = sender.style.left;
            ShoveWebUI_ShoveWebPart_LineV3.style.visibility = "visible";
        }

        if (senderRight - 1 == parseInt(objParts[i].style.left.replace("px", ""))) {
            ShoveWebUI_ShoveWebPart_LineV3.style.left = senderRight + "px";
            ShoveWebUI_ShoveWebPart_LineV3.style.visibility = "visible";
        }
    }
}
