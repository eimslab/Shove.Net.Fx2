/*
ShovePart2 设计时代码。
作者：shove
时间：2007-8-2
*/

var ShoveWebUI_ShovePart2_d = 5;
var ShoveWebUI_ShovePart2_l, ShoveWebUI_ShovePart2_t, ShoveWebUI_ShovePart2_r, ShoveWebUI_ShovePart2_b, ShoveWebUI_ShovePart2_ex, ShoveWebUI_ShovePart2_ey, ShoveWebUI_ShovePart2_cur;

var ShoveWebUI_ShovePart2_oldX = 0;
var ShoveWebUI_ShovePart2_oldY = 0;

var ShoveWebUI_ShovePart2_moving = false;
var ShoveWebUI_ShovePart2_resizing = false;

var ShoveWebUI_ShovePart2_KeyAdjusted = false;

// 获取所有选中的 Part
function ShoveWebUI_ShovePart2_GetSelectedParts() {
    var objSelectedParts = [];
    var objParts = ShoveWebUI_ShovePart2_GetParts(false, false);

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
function ShoveWebUI_ShovePart2_OnKeyDown(e, SiteID, PageName) {
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
        ShoveWebUI_ShovePart2_OnPaste(e, SiteID, PageName);

        return;
    }

    var objParts = ShoveWebUI_ShovePart2_GetSelectedParts();

    if (objParts.length < 1) {
        return;
    }

    if ((e.keyCode == 67) && e.ctrlKey) // Ctrl + C
    {
        ShoveWebUI_ShovePart2_OnCopy(e, SiteID, PageName, objParts);

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

            Limit = parseInt(ShoveWebUI_ShovePart2_GetParentTop(objParts[0]).replace("px", ""));

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

            ShoveWebUI_ShovePart2_KeyAdjusted = true;
            break;

        case 40:
            if (e.ctrlKey || e.shiftKey) {
                Step = 5;
            }

            for (i = 0; i < objParts.length; i++) {
                var Top = parseInt(objParts[i].style.top.replace("px", ""));

                objParts[i].style.top = (Top + Step) + "px";
            }

            ShoveWebUI_ShovePart2_KeyAdjusted = true;
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

            Limit = parseInt(ShoveWebUI_ShovePart2_GetParentLeft(objParts[0]).replace("px", ""));

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

            ShoveWebUI_ShovePart2_KeyAdjusted = true;
            break;

        case 39:
            var MaxRight = -1;

            for (i = 0; i < objParts.length; i++) {
                var Right = parseInt(objParts[i].style.left.replace("px", "")) + parseInt(objParts[i].style.width.replace("px", ""));

                if (MaxRight < Right) {
                    MaxRight = Right;
                }
            }

            Limit = (parseInt(ShoveWebUI_ShovePart2_GetParentLeft(objParts[0].parentNode).replace("px", "")) + parseInt(ShoveWebUI_ShovePart2_GetParentWidth(objParts[0].parentNode).replace("px", "")));

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

            ShoveWebUI_ShovePart2_KeyAdjusted = true;
            break;
    }

    if (objParts.length == 1) {
        ShoveWebUI_ShovePart2_ShowRuleLine(objParts[0]);
    }

    e.returnValue = false;
}

// 键抬起，进行保存数据
function ShoveWebUI_ShovePart2_OnKeyUp(e, SiteID, PageName) {
    e = e || event;

    // ShoveWebUI_ShovePart2_HideRuleLine();

    if (!ShoveWebUI_ShovePart2_KeyAdjusted && (e.keyCode != 46)) {
        return;
    }

    if ((e.keyCode != 38) && (e.keyCode != 40) && (e.keyCode != 37) && (e.keyCode != 39) && (e.keyCode != 46) && (e.keyCode != 67) && (e.keyCode != 86) && (e.keyCode != 83) && (e.keyCode != 13)) {
        return;
    }

    var objParts = ShoveWebUI_ShovePart2_GetSelectedParts();

    if (objParts.length < 1) {
        return;
    }

    if (ShoveWebUI_ShovePart2_KeyAdjusted) {
        var Datas = [];

        for (i = 0; i < objParts.length; i++) {
            var Data = objParts[i].id + "," + objParts[i].style.left + "," + objParts[i].style.top;

            Datas.push(Data);
        }

        var CallAjaxResult = Shove.Web.UI.ShovePart2.SaveForKeyMove(SiteID, PageName, Datas);

        ShoveWebUI_ShovePart2_KeyAdjusted = false;
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

        var CallAjaxResult = Shove.Web.UI.ShovePart2.DeleteForKeyMove(SiteID, PageName, Datas);

        return;
    }
}

function ShoveWebUI_ShovePart2_OnCopy(e, SiteID, PageName, objParts) {
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

function ShoveWebUI_ShovePart2_OnPaste(e, SiteID, PageName) {
    var SourcePartList = window.clipboardData.getData("text");

    if ((SourcePartList == null) || (SourcePartList == "")) {
        return;
    }

    var CallAjaxResult = Shove.Web.UI.ShovePart2.Paste(SiteID, PageName, SourcePartList);

    if (CallAjaxResult.value == "") {
        window.location.href = window.location.href;
    }
}

function ShoveWebUI_ShovePart2_OnMouseDown(e, sender, SiteID, PageName) {
    e = e || event;

    if (e.button != 1) {
        return;
    }

    // 绑定按键处理事件
    ShoveWebUI_ShovePart2_SetOnKeyDown(SiteID, PageName);

    // 增加对齐线
    ShoveWebUI_ShovePart2_DrawRuleLine();

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        return;
    }

    ShoveWebUI_ShovePart2_SetZindexToTop(e, sender, SiteID, PageName);
    ShoveWebUI_ShovePart2_SetSelected(e, sender);

    if (e.button == 1 && sender.style.cursor == "move") {
        ShoveWebUI_ShovePart2_moving = true;
        sender.setCapture();

        ShoveWebUI_ShovePart2_oldX = e.screenX;
        ShoveWebUI_ShovePart2_oldY = e.screenY;

        return;
    }

    if (e.button == 1 && sender.style.cursor) {
        ShoveWebUI_ShovePart2_resizing = true;
        sender.setCapture();
    }
}

function ShoveWebUI_ShovePart2_OnMouseUp(e, sender, SiteID, PageName) {
    e = e || event;

    ShoveWebUI_ShovePart2_HideRuleLine();

    if (e.button != 1) {
        return;
    }

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        return;
    }

    if (e.button == 1 && (ShoveWebUI_ShovePart2_resizing || ShoveWebUI_ShovePart2_moving)) {
        ShoveWebUI_ShovePart2_resizing = false;
        ShoveWebUI_ShovePart2_moving = false;

        //   var o = document.getElementById("p");
        //  if (getRealLeft(o) <= sender.offsetLeft && getRealLeft(o) + o.offsetWidth >= sender.offsetLeft && getRealTop(o) <= sender.offsetTop && getRealTop(o) + o.offsetHeight >= sender.offsetTop) {
        //       o.appendChild(sender);
        //   }


        sender.releaseCapture();

        var PrimitiveHeight;

        if (sender.style.height == "auto") {
            PrimitiveHeight = sender.scrollHeight + "px";
        }
        else {
            PrimitiveHeight = sender.style.height;
        }

        var CallAjaxResult = Shove.Web.UI.ShovePart2.Save(sender.id, SiteID, PageName, sender.style.left, sender.style.top, sender.style.width, sender.style.height, PrimitiveHeight, ShoveWebUI_ShovePart2_GetParentLeft(sender));
    }
}

function ShoveWebUI_ShovePart2_OnMouseMove(e, sender, SiteID, PageName) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        sender.style.cursor = "";

        return;
    }

    if (ShoveWebUI_ShovePart2_resizing || ShoveWebUI_ShovePart2_moving) {
        var CurrentDivLeft = parseInt(sender.style.left.replace("px", ""));
        var CurrentDivTop = parseInt(sender.style.top.replace("px", ""));
        var CurrentDivWidth = parseInt(sender.style.width.replace("px", ""));
        var CurrentDivHeight = parseInt(sender.style.height.replace("px", ""));

        var CurrentDivBorder = 0;

        if (sender.style.borderRightWidth != "") {
            CurrentDivBorder = parseInt(sender.style.borderRightWidth.replace("px", ""));
        }

        if (ShoveWebUI_ShovePart2_moving) {
            var x = e.screenX;
            var y = e.screenY;

            var parentNodeDivLeft = 0;
            var parentNodeDivTop = 0;
            var parentNodeDivWidth = 0;

            if (sender.parentNode) {
                if (sender.parentNode.className == "bodyMainDiv") {

                    parentNodeDivLeft = parseInt(ShoveWebUI_ShovePart2_GetParentLeft(sender).replace("px", ""));
                    parentNodeDivTop = parseInt(ShoveWebUI_ShovePart2_GetParentTop(sender).replace("px", ""));
                    parentNodeDivWidth = parseInt(ShoveWebUI_ShovePart2_GetParentWidth(sender).replace("px", ""));
                }
                else {

                    parentNodeDivLeft = parseInt(ShoveWebUI_ShovePart2_GetParentLeft(sender.parentNode).replace("px", ""));
                    parentNodeDivTop = parseInt(ShoveWebUI_ShovePart2_GetParentTop(sender.parentNode).replace("px", ""));
                    parentNodeDivWidth = parseInt(ShoveWebUI_ShovePart2_GetParentWidth(sender.parentNode).replace("px", ""));
                }

                var CurrentDivleftmargin = CurrentDivLeft + (x - ShoveWebUI_ShovePart2_oldX);

                if (CurrentDivleftmargin < parentNodeDivLeft) {
                    CurrentDivleftmargin = parentNodeDivLeft;
                }

                var CurrentDivtopmargin = CurrentDivTop + (y - ShoveWebUI_ShovePart2_oldY);

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

            //            if(document.all)
            //            {
            //                sender.style.styleFloat = CurrentDivleftmargin + "px";
            //            }
            //            else
            //            {
            //                 sender.style.cssFloat = CurrentDivleftmargin + "px";
            //            }
            sender.style.left = CurrentDivleftmargin + "px";

            sender.style.top = CurrentDivtopmargin + "px";





            //  var o = document.getElementById("p");
            // if (getRealLeft(o) <= CurrentDivleftmargin && getRealLeft(o) + o.offsetWidth >= CurrentDivleftmargin && getRealTop(o) <= CurrentDivtopmargin && getRealTop(o) + o.offsetHeight >= CurrentDivtopmargin) {
            //     o.appendChild(sender);
            //  }
            ShoveWebUI_ShovePart2_oldX = x;
            ShoveWebUI_ShovePart2_oldY = y;
        }
        else if (ShoveWebUI_ShovePart2_resizing) {
            var dx = e.screenX - ShoveWebUI_ShovePart2_ex;
            var dy = e.screenY - ShoveWebUI_ShovePart2_ey;

            var parentNodeDivLeft = 0;
            var parentNodeDivTop = 0;
            var parentNodeDivWidth = 0;

            if (sender.parentNode) {
                parentNodeDivLeft = parseInt(ShoveWebUI_ShovePart2_GetParentLeft(sender).replace("px", ""));
                parentNodeDivTop = parseInt(ShoveWebUI_ShovePart2_GetParentTop(sender).replace("px", ""));
                parentNodeDivWidth = parseInt(ShoveWebUI_ShovePart2_GetParentWidth(sender).replace("px", ""));
            }

            if (ShoveWebUI_ShovePart2_cur.indexOf("w") > -1) {
                ShoveWebUI_ShovePart2_l += dx;
            }
            else if (ShoveWebUI_ShovePart2_cur.indexOf("e") > -1) {
                ShoveWebUI_ShovePart2_r += dx;
            }
            if (ShoveWebUI_ShovePart2_cur.indexOf("n") > -1) {
                ShoveWebUI_ShovePart2_t += dy;
            }
            else if (ShoveWebUI_ShovePart2_cur.indexOf("s") > -1) {
                ShoveWebUI_ShovePart2_b += dy;
            }

            var s = sender.style;

            if (ShoveWebUI_ShovePart2_r - ShoveWebUI_ShovePart2_l > 2 * ShoveWebUI_ShovePart2_d) {
                s.left = ShoveWebUI_ShovePart2_l + "px";
                s.width = (ShoveWebUI_ShovePart2_r - ShoveWebUI_ShovePart2_l) + "px";
            }

            if (ShoveWebUI_ShovePart2_b - ShoveWebUI_ShovePart2_t > 2 * ShoveWebUI_ShovePart2_d) {
                s.top = ShoveWebUI_ShovePart2_t + "px";
                s.height = (ShoveWebUI_ShovePart2_b - ShoveWebUI_ShovePart2_t) + "px";
            }

            if (((s.width.replace("px", "")) >= (parentNodeDivWidth - CurrentDivBorder * 2))) {
                s.left = parentNodeDivLeft + "px";
                s.width = (parentNodeDivWidth - CurrentDivBorder * 2) + "px";
            }

            ShoveWebUI_ShovePart2_ex += dx;
            ShoveWebUI_ShovePart2_ey += dy;
        }

        //  ShoveWebUI_ShovePart2_ShowRuleLine(sender);
    }
    else// if (e.srcElement == sender)
    {
        var x = e.offsetX;
        var y = e.offsetY;
        var c = sender.currentStyle;

        var w = parseInt(c.width);
        var h = parseInt(c.height);

        ShoveWebUI_ShovePart2_cur = y < ShoveWebUI_ShovePart2_d ? "n" : h - y < ShoveWebUI_ShovePart2_d ? "s" : "";
        ShoveWebUI_ShovePart2_cur += x < ShoveWebUI_ShovePart2_d ? "w" : w - x < ShoveWebUI_ShovePart2_d ? "e" : "";

        if (ShoveWebUI_ShovePart2_cur) {
            sender.style.cursor = ShoveWebUI_ShovePart2_cur + "-resize";
            ShoveWebUI_ShovePart2_l = parseInt(c.left);
            ShoveWebUI_ShovePart2_t = parseInt(c.top);
            ShoveWebUI_ShovePart2_r = ShoveWebUI_ShovePart2_l + w;
            ShoveWebUI_ShovePart2_b = ShoveWebUI_ShovePart2_t + h;
            ShoveWebUI_ShovePart2_ex = e.screenX;
            ShoveWebUI_ShovePart2_ey = e.screenY;
        }
        else {
            sender.style.cursor = "move";
        }
    }
}

function ShoveWebUI_ShovePart2_OnEditClick(e, sender, SiteID, PageName, UserID, RelativePath, AttributesPageFileName, AscxControlFileName, ControlAttributes) {

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

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        alert("控件已经被锁定，要对其进行编辑，请先解除锁定。");

        return;
    }

    var Params = new Object();

    Params.SiteID = SiteID;
    Params.UserID = UserID;
    Params.AscxControlFileName = AscxControlFileName;
    Params.CssClass = sender.className;
    Params.ControlAttributes = ControlAttributes;

    // 调用对话框，编辑属性sender.style.backgroundImage
    var strReturn = window.showModalDialog(RelativePath + "ShoveWebUI_client/Page/ShovePart2Attributes.htm", Params, "dialogWidth=740px;dialogHeight=460px;center: Yes;");

    if (strReturn == null) {
        return false;
    }

    var AttributesList = new Array(3);

    AttributesList[0] = strReturn.AscxFileName;
    AttributesList[1] = strReturn.CssClass;
    AttributesList[2] = strReturn.ControlAttributes;

    var isAscxControlFileNameChanged = false;

    if (AscxControlFileName != AttributesList[0]) {
        isAscxControlFileNameChanged = true;
    }

    var IsControlAttributeChanged = false;

    if (sender.getAttribute("ControlAttributes") != AttributesList[2]) {
        IsControlAttributeChanged = true;

        sender.setAttribute("ControlAttributes", AttributesList[2]);
    }

    var CallAjaxResult = Shove.Web.UI.ShovePart2.Edit(sender.id, SiteID, PageName, AttributesList);

    if (isAscxControlFileNameChanged) {
        window.location.href = window.location.href;

        return false;
    }
    window.location.href = window.location.href;
    if (IsControlAttributeChanged) {
        return true;
    }

    return false;
}

// 选中
function ShoveWebUI_ShovePart2_SetSelected(e, sender) {
    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        return;
    }

    var divTitleBar;

    // 没有按下 Ctrl 或者 Shift，将其他 Part 置为未选中状态
    if ((!e.ctrlKey) && (!e.shiftKey)) {
        var objParts = ShoveWebUI_ShovePart2_GetParts(false, false);

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
function ShoveWebUI_ShovePart2_OnLockClick(e, sender, SiteID, PageName, btnLock, divTitle) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");
    if (LockState == "True") {
        //alert("LockState ==" + LockState);
        LockState = "False";

        alert('解锁Part');
        btnLock.setAttribute("title", "锁定此窗口");
        divTitle.style.backgroundColor = "blue";
        divTitle.style.filter = "alpha(opacity=40)";
        sender.setAttribute("Lock", LockState);
    }
    else {
        //alert("LockState ==" + LockState);
        LockState = "True";

        alert("锁定Part")
        btnLock.setAttribute("title", "解除锁定此窗口");
        divTitle.style.backgroundColor = "black";
        divTitle.style.filter = "alpha(opacity=60)";
        sender.setAttribute("Lock", LockState);
    }

    var CallAjaxResult = Shove.Web.UI.ShovePart2.SaveLock(sender.id, SiteID, PageName, (LockState == "True") ? true : false);
}

function ShoveWebUI_ShovePart2_OnApplyToAllClick(e, sender, SiteID, PageName) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        alert("控件已经被锁定，要对其进行编辑，请先解除锁定。");

        return;
    }

    if (!confirm("确信要将此 Part 的属性应用到其他所有页面吗？")) {
        return;
    }

    var CallAjaxResult = Shove.Web.UI.ShovePart2.SaveApplyToAll(sender.id, SiteID, PageName, true, false, sender.getAttribute("ControlAttributes"));

    alert("应用成功。");
}

function ShoveWebUI_ShovePart2_OnToBackgroundClick(e, sender, SiteID, PageName) {
    e = e || event;

    var LockState = sender.getAttribute("Lock");

    if (LockState == "True") {
        alert("控件已经被锁定，要对其进行编辑，请先解除锁定。");

        return;
    }

    var objParts = ShoveWebUI_ShovePart2_GetParts(true, false);

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

        var CallAjaxResult = Shove.Web.UI.ShovePart2.ToBackground(sender.id, SiteID, PageName, sender.style.zIndex);
    }
    else {
        alert("控件已经在最底层。");

        return;
    }
}

function ShoveWebUI_ShovePart2_SetZindexToTop(e, sender, SiteID, PageName) {
    if (sender.style.zIndex == 10000) {
        return;
    }

    var objParts = ShoveWebUI_ShovePart2_GetParts(true, false);
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

    var CallAjaxResult = Shove.Web.UI.ShovePart2.SaveZIndex(sender.id, SiteID, PageName, objOtherZIndexList);
}

function ShoveWebUI_ShovePart2_OnCloseClick(e, sender, SiteID, PageName, CloseConfirmText) {
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
    //重写cookie

    var str = "";

    for (var i = 0; i < CoolDrag.container.getElementsByTagName("DIV").length; i++) {

        var o = CoolDrag.container.getElementsByTagName("DIV")[i];
        if (o.id.indexOf("container") < 0) continue;
        if (i > 0) str += "|";
        if (!CheckString(o.id, 'ShovePart2')) {
            str += o.id + ":";
        }
        else {
            str += o.id + ",";
            continue;
        }
        for (var j = 0; j < o.childNodes.length; j++) {


            str += o.childNodes[j].id + ",";

        }

    }

    CoolDrag.save("cooldrag_" + SiteID + "_" + PageName, str, 24);

    var CallAjaxResult = Shove.Web.UI.ShovePart2.Close(sender.id, SiteID, PageName);
}

function ShoveWebUI_ShovePart2_SetOnKeyDown(SiteID, PageName) {
    if (!document.body.onkeydown) {
        document.onkeydown = function() { ShoveWebUI_ShovePart2_OnKeyDown(event, SiteID, PageName) };
        document.onkeyup = function() { ShoveWebUI_ShovePart2_OnKeyUp(event, SiteID, PageName) };
    }
}

function ShoveWebUI_ShovePart2_DrawRuleLine() {
    if (!document.getElementById("ShoveWebUI_ShovePart2_LineH1")) {
        var ShoveWebUI_ShovePart2_LineH1 = document.createElement("img");
        ShoveWebUI_ShovePart2_LineH1.id = "ShoveWebUI_ShovePart2_LineH1";
        ShoveWebUI_ShovePart2_LineH1.setAttribute("src", "about:blank");
        ShoveWebUI_ShovePart2_LineH1.style.position = "absolute";
        ShoveWebUI_ShovePart2_LineH1.style.setExpression("width", "document.body.clientWidth || document.documentElement.clientWidth");
        ShoveWebUI_ShovePart2_LineH1.style.height = "1px";
        ShoveWebUI_ShovePart2_LineH1.style.setExpression("left", "document.body.scrollLeft || document.documentElement.scrollLeft");
        ShoveWebUI_ShovePart2_LineH1.style.top = "0px";
        ShoveWebUI_ShovePart2_LineH1.style.backgroundColor = "red";
        ShoveWebUI_ShovePart2_LineH1.style.zIndex = 10001;
        ShoveWebUI_ShovePart2_LineH1.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShovePart2_LineH1);

        var ShoveWebUI_ShovePart2_LineH2 = document.createElement("img");
        ShoveWebUI_ShovePart2_LineH2.id = "ShoveWebUI_ShovePart2_LineH2";
        ShoveWebUI_ShovePart2_LineH2.setAttribute("src", "about:blank");
        ShoveWebUI_ShovePart2_LineH2.style.position = "absolute";
        ShoveWebUI_ShovePart2_LineH2.style.setExpression("width", "document.body.clientWidth || document.documentElement.clientWidth");
        ShoveWebUI_ShovePart2_LineH2.style.height = "1px";
        ShoveWebUI_ShovePart2_LineH2.style.setExpression("left", "document.body.scrollLeft || document.documentElement.scrollLeft");
        ShoveWebUI_ShovePart2_LineH2.style.top = "0px";
        ShoveWebUI_ShovePart2_LineH2.style.backgroundColor = "red";
        ShoveWebUI_ShovePart2_LineH2.style.zIndex = 10001;
        ShoveWebUI_ShovePart2_LineH2.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShovePart2_LineH2);

        var ShoveWebUI_ShovePart2_LineH3 = document.createElement("img");
        ShoveWebUI_ShovePart2_LineH3.id = "ShoveWebUI_ShovePart2_LineH3";
        ShoveWebUI_ShovePart2_LineH3.setAttribute("src", "about:blank");
        ShoveWebUI_ShovePart2_LineH3.style.position = "absolute";
        ShoveWebUI_ShovePart2_LineH3.style.setExpression("width", "document.body.clientWidth || document.documentElement.clientWidth");
        ShoveWebUI_ShovePart2_LineH3.style.height = "1px";
        ShoveWebUI_ShovePart2_LineH3.style.setExpression("left", "document.body.scrollLeft || document.documentElement.scrollLeft");
        ShoveWebUI_ShovePart2_LineH3.style.top = "0px";
        ShoveWebUI_ShovePart2_LineH3.style.backgroundColor = "blue";
        ShoveWebUI_ShovePart2_LineH3.style.zIndex = 10001;
        ShoveWebUI_ShovePart2_LineH3.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShovePart2_LineH3);

        var ShoveWebUI_ShovePart2_LineV1 = document.createElement("img");
        ShoveWebUI_ShovePart2_LineV1.id = "ShoveWebUI_ShovePart2_LineV1";
        ShoveWebUI_ShovePart2_LineV1.style.position = "absolute";
        ShoveWebUI_ShovePart2_LineV1.setAttribute("src", "about:blank");
        ShoveWebUI_ShovePart2_LineV1.style.setExpression("height", "document.body.clientHeight || document.documentElement.clientHeight");
        ShoveWebUI_ShovePart2_LineV1.style.width = "1px";
        ShoveWebUI_ShovePart2_LineV1.style.left = "0px";
        ShoveWebUI_ShovePart2_LineV1.style.setExpression("top", "document.body.scrollTop || document.documentElement.scrollTop");
        ShoveWebUI_ShovePart2_LineV1.style.backgroundColor = "red";
        ShoveWebUI_ShovePart2_LineV1.style.zIndex = 10001;
        ShoveWebUI_ShovePart2_LineV1.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShovePart2_LineV1);

        var ShoveWebUI_ShovePart2_LineV2 = document.createElement("img");
        ShoveWebUI_ShovePart2_LineV2.id = "ShoveWebUI_ShovePart2_LineV2";
        ShoveWebUI_ShovePart2_LineV2.setAttribute("src", "about:blank");
        ShoveWebUI_ShovePart2_LineV2.style.position = "absolute";
        ShoveWebUI_ShovePart2_LineV2.style.setExpression("height", "document.body.clientHeight || document.documentElement.clientHeight");
        ShoveWebUI_ShovePart2_LineV2.style.width = "1px";
        ShoveWebUI_ShovePart2_LineV2.style.left = "0px";
        ShoveWebUI_ShovePart2_LineV2.style.setExpression("top", "document.body.scrollTop || document.documentElement.scrollTop");
        ShoveWebUI_ShovePart2_LineV2.style.backgroundColor = "red";
        ShoveWebUI_ShovePart2_LineV2.style.zIndex = 10001;
        ShoveWebUI_ShovePart2_LineV2.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShovePart2_LineV2);

        var ShoveWebUI_ShovePart2_LineV3 = document.createElement("img");
        ShoveWebUI_ShovePart2_LineV3.id = "ShoveWebUI_ShovePart2_LineV3";
        ShoveWebUI_ShovePart2_LineV3.setAttribute("src", "about:blank");
        ShoveWebUI_ShovePart2_LineV3.style.position = "absolute";
        ShoveWebUI_ShovePart2_LineV3.style.setExpression("height", "document.body.clientHeight || document.documentElement.clientHeight");
        ShoveWebUI_ShovePart2_LineV3.style.width = "1px";
        ShoveWebUI_ShovePart2_LineV3.style.left = "0px";
        ShoveWebUI_ShovePart2_LineV3.style.setExpression("top", "document.body.scrollTop || document.documentElement.scrollTop");
        ShoveWebUI_ShovePart2_LineV3.style.backgroundColor = "blue";
        ShoveWebUI_ShovePart2_LineV3.style.zIndex = 10001;
        ShoveWebUI_ShovePart2_LineV3.style.visibility = "hidden";
        document.body.appendChild(ShoveWebUI_ShovePart2_LineV3);
    }
}

function ShoveWebUI_ShovePart2_HideRuleLine() {
    var ShoveWebUI_ShovePart2_LineH1 = document.getElementById("ShoveWebUI_ShovePart2_LineH1");
    var ShoveWebUI_ShovePart2_LineH2 = document.getElementById("ShoveWebUI_ShovePart2_LineH2");
    var ShoveWebUI_ShovePart2_LineH3 = document.getElementById("ShoveWebUI_ShovePart2_LineH3");
    var ShoveWebUI_ShovePart2_LineV1 = document.getElementById("ShoveWebUI_ShovePart2_LineV1");
    var ShoveWebUI_ShovePart2_LineV2 = document.getElementById("ShoveWebUI_ShovePart2_LineV2");
    var ShoveWebUI_ShovePart2_LineV3 = document.getElementById("ShoveWebUI_ShovePart2_LineV3");

    if (!ShoveWebUI_ShovePart2_LineH1) {
        return;
    }

    ShoveWebUI_ShovePart2_LineH1.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineH2.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineH3.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineV1.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineV2.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineV3.style.visibility = "hidden";
}

function ShoveWebUI_ShovePart2_ShowRuleLine(sender) {
    var ShoveWebUI_ShovePart2_LineH1 = document.getElementById("ShoveWebUI_ShovePart2_LineH1");
    var ShoveWebUI_ShovePart2_LineH2 = document.getElementById("ShoveWebUI_ShovePart2_LineH2");
    var ShoveWebUI_ShovePart2_LineH3 = document.getElementById("ShoveWebUI_ShovePart2_LineH3");
    var ShoveWebUI_ShovePart2_LineV1 = document.getElementById("ShoveWebUI_ShovePart2_LineV1");
    var ShoveWebUI_ShovePart2_LineV2 = document.getElementById("ShoveWebUI_ShovePart2_LineV2");
    var ShoveWebUI_ShovePart2_LineV3 = document.getElementById("ShoveWebUI_ShovePart2_LineV3");

    if (!ShoveWebUI_ShovePart2_LineH1) {
        return;
    }

    ShoveWebUI_ShovePart2_LineH1.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineH2.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineH3.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineV1.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineV2.style.visibility = "hidden";
    ShoveWebUI_ShovePart2_LineV3.style.visibility = "hidden";

    var objParts = ShoveWebUI_ShovePart2_GetParts(true, false);

    if (objParts.length <= 1) {
        return;
    }

    for (var i = 0; i < objParts.length; i++) {
        if (objParts[i].id == sender.id) {
            continue;
        }

        if (objParts[i].style.left == sender.style.left) {
            ShoveWebUI_ShovePart2_LineV1.style.left = sender.style.left;
            ShoveWebUI_ShovePart2_LineV1.style.visibility = "visible";
        }

        if (objParts[i].style.top == sender.style.top) {
            ShoveWebUI_ShovePart2_LineH1.style.top = sender.style.top;
            ShoveWebUI_ShovePart2_LineH1.style.visibility = "visible";
        }

        var objRight = parseInt(objParts[i].style.left.replace("px", "")) + parseInt(objParts[i].style.width.replace("px", ""));
        var senderRight = parseInt(sender.style.left.replace("px", "")) + parseInt(sender.style.width.replace("px", ""));

        if (objRight == senderRight) {
            ShoveWebUI_ShovePart2_LineV2.style.left = senderRight + "px";
            ShoveWebUI_ShovePart2_LineV2.style.visibility = "visible";
        }

        var objBottom = parseInt(objParts[i].style.top.replace("px", "")) + (objParts[i].style.height == "auto" ? parseInt(objParts[i].scrollHeight) : parseInt(objParts[i].style.height.replace("px", "")));
        var senderBottom = parseInt(sender.style.top.replace("px", "")) + (sender.style.height == "auto" ? parseInt(sender.scrollHeight) : parseInt(sender.style.height.replace("px", "")));

        if (objBottom == senderBottom) {
            ShoveWebUI_ShovePart2_LineH2.style.top = senderBottom + "px";
            ShoveWebUI_ShovePart2_LineH2.style.visibility = "visible";
        }

        // 衔接线
        if (objBottom + 1 == parseInt(sender.style.top.replace("px", ""))) {
            ShoveWebUI_ShovePart2_LineH3.style.top = sender.style.top;
            ShoveWebUI_ShovePart2_LineH3.style.visibility = "visible";
        }

        if (senderBottom - 1 == parseInt(objParts[i].style.top.replace("px", ""))) {
            ShoveWebUI_ShovePart2_LineH3.style.top = senderBottom + "px";
            ShoveWebUI_ShovePart2_LineH3.style.visibility = "visible";
        }

        if (objRight + 1 == parseInt(sender.style.left.replace("px", ""))) {
            ShoveWebUI_ShovePart2_LineV3.style.left = sender.style.left;
            ShoveWebUI_ShovePart2_LineV3.style.visibility = "visible";
        }

        if (senderRight - 1 == parseInt(objParts[i].style.left.replace("px", ""))) {
            ShoveWebUI_ShovePart2_LineV3.style.left = senderRight + "px";
            ShoveWebUI_ShovePart2_LineV3.style.visibility = "visible";
        }
    }
}

var CoolDrag = {

    obj: null, //目标对象 

    cloneobj: null, //拖动对象 

    container: null, //容器 

    dragged: false, //拖动标志 

    shadow: null, //阴影 

    SiteID: null, //站点ID

    PageName: null, //页面名称

    init: function(id, _SiteID, _PageName) {

        //CoolDrag.container = $(id);
        CoolDrag.container = document.getElementById(id);
        CoolDrag.SiteID = _SiteID;
        CoolDrag.PageName = _PageName;
        var cooldrag = CoolDrag.read("cooldrag_" + CoolDrag.SiteID + "_" + CoolDrag.PageName);

        if (cooldrag != "") {//读取cookie 

            var subcontainer = cooldrag.split("|");


            for (var i = 0; i < subcontainer.length; i++) {
                if (subcontainer[i].length > 0) {

                    var subcontainerItem = subcontainer[i].split(":");

                    if (document.getElementById(subcontainerItem[0])) {
                        if (subcontainerItem[1]) {
                            var items = subcontainerItem[1].split(",");

                            for (var j = 0; j < items.length; j++) {
                                if (items[j] != "") {

                                    if (document.getElementById(items[j])) { document.getElementById(subcontainerItem[0]).appendChild(document.getElementById(items[j])); }
                                    // if ($(items[j])) $(subcontainerItem[0]).appendChild($(items[j]));
                                }

                            }
                        }
                    }
                }

            }

        }

        cleanWhitespace(CoolDrag.container)//清除空白节点 

        var collection = CoolDrag.container.getElementsByTagName("DIV");

        for (var i = 0; i < collection.length; i++) {

            //  if (collection[i].getAttribute("ShoveWebUITypeName") == "ShovePart2") {
            if (collection[i].className == "dragLayer") {
                var o = collection[i].firstChild;

                SavedObject.push([o.parentNode.id, 1, o.parentNode.style.height]);

                o.onmousedown = CoolDrag.start;

            }

        }

        document.onmousemove = CoolDrag.drag;

        document.onmouseup = CoolDrag.end;

    },
    start: function(e) {

        if (!e) e = window.event;

        var obj = getT(e);

        if (obj.className == "min") {

            CoolDrag.min(e);
            return;

        } else if (obj.className == "close") {

            CoolDrag.close(e);
            return;
        }
        else if (obj.className == "tt") {
            obj.click();
            return false;
        }
        else {
            if (obj.className != "dragHeader") { obj = obj.parentNode };
        }

        if (obj.className != "dragHeader") {
            return;
        }
        //alert('html=' + obj.parentNode.getAttribute("Lock"));
        if (obj.parentNode.getAttribute("Lock") == "True") {
            return;
        }


        CoolDrag.dragged = true;

        CoolDrag.obj = obj.parentNode;

        CoolDrag.cloneobj = CoolDrag.obj.cloneNode(true);

        document.body.appendChild(CoolDrag.cloneobj);

        CoolDrag.shadow = document.createElement("DIV");

        document.body.appendChild(CoolDrag.shadow);

        with (CoolDrag.cloneobj.style) {

            position = "absolute";

            zIndex = 1000;

            left = getRealLeft(CoolDrag.obj) + "px";


            top = getRealTop(CoolDrag.obj) + "px";

        }

        with (CoolDrag.shadow.style) {

            position = "absolute";

            zIndex = 999;

            left = getRealLeft(CoolDrag.obj) + 4 + "px";

            top = getRealTop(CoolDrag.obj) + 4 + "px";

            width = CoolDrag.obj.offsetWidth + "px";

            height = CoolDrag.obj.offsetHeight + "px";

            backgroundColor = "#ccc";

            if (navigator.userAgent.indexOf("Gecko") != -1) {
                MozOpacity = "0.5";
            }
            else if (navigator.userAgent.indexOf("MSIE") != -1) {
                filter = "alpha(opacity=50)";
            }
        }


        CoolDrag.cloneobj.initMouseX = getMouseX(e);

        CoolDrag.cloneobj.initMouseY = getMouseY(e);

        CoolDrag.cloneobj.initoffsetL = getRealLeft(CoolDrag.obj);

        CoolDrag.cloneobj.initoffsetY = getRealTop(CoolDrag.obj);

        //change style 

        CoolDrag.cloneobj.firstChild.className = "dragHeader_over";

        CoolDrag.cloneobj.className = "dragLayer_over";

        CoolDrag.obj.className = "clone_dragLayer_over";
    },



    drag: function(e) {

        if (!CoolDrag.dragged || CoolDrag.obj == null) return;

        if (!e) e = window.event;

        var currenX = getMouseX(e);

        var currenY = getMouseY(e);

        if (CoolDrag.cloneobj.initoffsetL + currenX - CoolDrag.cloneobj.initMouseX > getRealLeft(CoolDrag.container)) {
            CoolDrag.cloneobj.style.left = (CoolDrag.cloneobj.initoffsetL + currenX - CoolDrag.cloneobj.initMouseX) + "px";
        }
        else {
            CoolDrag.cloneobj.style.left = currenX + "px";
        }


        if (CoolDrag.cloneobj.initoffsetY + currenY - CoolDrag.cloneobj.initMouseY > getRealTop(CoolDrag.container)) {
            CoolDrag.cloneobj.style.top = (CoolDrag.cloneobj.initoffsetY + currenY - CoolDrag.cloneobj.initMouseY) + "px";
        }
        else {
            CoolDrag.cloneobj.style.top = currenY + "px";
        }


        var collection = CoolDrag.container.getElementsByTagName("DIV");

        var finded = false;

        for (var i = 0; i < collection.length; i++) {

            var o = collection[i];

            if (o.className == "dragLayer") {

                if ((((getRealLeft(o) <= CoolDrag.cloneobj.offsetLeft && getRealLeft(o) + o.offsetWidth >= CoolDrag.cloneobj.offsetLeft) && getRealTop(o.parentNode) <= CoolDrag.cloneobj.offsetTop && getRealTop(o.parentNode) + o.offsetHeight >= CoolDrag.cloneobj.offsetTop) ||

     (getRealLeft(o) <= CoolDrag.cloneobj.offsetLeft + CoolDrag.cloneobj.offsetWidth &&

     getRealLeft(o) + o.offsetWidth >= CoolDrag.cloneobj.offsetLeft + CoolDrag.cloneobj.offsetWidth)) &&

        getRealTop(o.parentNode) <= CoolDrag.cloneobj.offsetTop && getRealTop(o.parentNode) + o.offsetHeight >= CoolDrag.cloneobj.offsetTop) {


                    if (getRealTop(o.parentNode) >= CoolDrag.cloneobj.offsetTop - 8) {

                        o.parentNode.insertBefore(CoolDrag.obj, o);

                    } else {

                        if (o.nextSibling) {
                            o.parentNode.insertBefore(CoolDrag.obj, o.nextSibling);
                        }
                        else {
                            o.parentNode.appendChild(CoolDrag.obj);
                        }
                    }

                    finded = true;

                    break;
                }
            }
        }

        if (!finded) {

            for (var i = 0; i < CoolDrag.container.getElementsByTagName("DIV").length; i++) {

                var o = CoolDrag.container.getElementsByTagName("DIV")[i];
                // alert("o.id="+o.id);
                if (getRealLeft(o) <= CoolDrag.cloneobj.offsetLeft && getRealLeft(o) + o.offsetWidth >= CoolDrag.cloneobj.offsetLeft && getRealTop(o) <= CoolDrag.cloneobj.offsetTop && getRealTop(o) + o.offsetHeight >= CoolDrag.cloneobj.offsetTop) {

                    if (o.id.indexOf("container") > -1) {
                        o.appendChild(CoolDrag.obj);                    }
                }
            }
        }

        with (CoolDrag.shadow.style) {
            left = (CoolDrag.cloneobj.offsetLeft + 4) + "px";
            top = (CoolDrag.cloneobj.offsetTop + 4) + "px";
        }

        //document.title = CoolDrag.cloneobj.style.left + "|" + CoolDrag.shadow.style.left; 

    },

    end: function(e) {

        if (!CoolDrag.dragged) return;

        CoolDrag.obj.className = "dragLayer";

        CoolDrag.dragged = false;

        CoolDrag.shadow.parentNode.removeChild(CoolDrag.shadow);

        CoolDrag.timer = CoolDrag.repos(150, 15);

        //保存cookie 

        var str = "";

        for (var i = 0; i < CoolDrag.container.getElementsByTagName("DIV").length; i++) {

            var o = CoolDrag.container.getElementsByTagName("DIV")[i];
            if ((!o.id)) continue;
            if (o.id.indexOf("container") < 0) continue;
            if (i > 0) str += "|";

            if (!CheckString(o.id, 'ShovePart2')) {
                str += o.id + ":";
            }
            else {
                str += o.id + ",";
                continue;
            }
            for (var j = 0; j < o.childNodes.length; j++) {

                // if (j > 0) str += ",";

                str += o.childNodes[j].id + ",";
            }
        }
        //alert("readCookies=cooldrag_"+CoolDrag.SiteID+"_"+CoolDrag.PageName+"="+str);

        CoolDrag.save("cooldrag_" + CoolDrag.SiteID + "_" + CoolDrag.PageName, str, 24);

    },

    repos: function(aa, ab) {

        //var f=CoolDrag.obj.filters.alpha.opacity; 

        var tl = getRealLeft(CoolDrag.cloneobj);

        var tt = getRealTop(CoolDrag.cloneobj);

        var kl = (tl - getRealLeft(CoolDrag.obj)) / ab;

        var kt = (tt - getRealTop(CoolDrag.obj)) / ab;

        //var kf=f/ab; 

        return setInterval(function() {
            if (ab < 1) {

                clearInterval(CoolDrag.timer);

                CoolDrag.cloneobj.parentNode.removeChild(CoolDrag.cloneobj);

                CoolDrag.obj = null;

                return;

            }

            ab--;

            tl -= kl;

            tt -= kt;

            //f-=kf; 

            CoolDrag.cloneobj.style.left = parseInt(tl) + "px";

            CoolDrag.cloneobj.style.top = parseInt(tt) + "px";

            //CoolDrag.tdiv.filters.alpha.opacity=f; 

        }
        , aa / ab)

    },

    min: function(e) {

        if (!e) e = window.event;

        var obj = getT(e);

        var rootObj = obj.parentNode.parentNode.parentNode;

        var id = rootObj.id;

        if (SavedObject.getStatus(id)[1]) {

            SavedObject.setStatus(id, 0);

            rootObj.style.height = "20px";

            rootObj.childNodes[1].style.display = 'none';

        } else {

            SavedObject.setStatus(id, 1);

            rootObj.lastChild.style.display = '';

            rootObj.style.height = SavedObject.getStatus(id)[2];

        }

        obj.innerHTML = obj.innerHTML == 0 ? 2 : 0;

    },

    close: function(e) {

        if (!e) e = window.event;

        var obj = getT(e);

        var rootObj = obj.parentNode.parentNode.parentNode;

        rootObj.parentNode.removeChild(rootObj);

    },

    save: function(name, value, hours) {

        var expire = "";

        if (hours != null) {

            expire = new Date((new Date()).getTime() + hours * 3600000);

            expire = "; expires=" + expire.toGMTString();

        }
        //alert("saveCookie="+name+"="+escape(value));

        document.cookie = name + "=" + escape(value) + expire;

        // alert('document.cookie2='+unescape(document.cookie));
        //            delCookie(name);
        //  SetCookie(name,value);
        //alert(document.cookie);
        //  alert('document.cookie4='+unescape(getCookie(name)));
        // Shove.Web.UI.ShovePart2BasePage.SavePageLayout(value,GetUrlParameter('PN'),CoolDrag.SiteID);
    },

    read: function(name) {

        var cookieValue = "";

        var search = name + "=";


        var cookieValue = "";


        if (document.cookie.length > 0) {

            offset = document.cookie.indexOf(search);

            if (offset != -1) {

                offset += search.length;

                end = document.cookie.indexOf(";", offset);

                if (end == -1) end = document.cookie.length;

                cookieValue = unescape(document.cookie.substring(offset, end))

            }

        }
        // alert(cookieValue);
        if (cookieValue == "") {
            return Shove.Web.UI.ShovePart2BasePage.ReadPageLayout(CoolDrag.PageName, CoolDrag.SiteID).value;
        }
        else {
            return cookieValue;
        }
    }
}

function SetCookie(name, value)//两个参数，一个是cookie的名子，一个是值
{
    var Days = 30; //此 cookie 将被保存 30 天
    var exp = new Date(); //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);

    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
function delCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
function getCookie(name)//取cookies函数 
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;

}
////function delCookies(name)
////{
////alert('22222');
////　   var　expire = new Date((new Date()).getTime() - hours * 3600000);

////　　 var cval=getCookie(name);
////　　 alert(cval);
////　　 if(cval!=null) document.cookie=name +"="+cval+";expire="+expire.toGMTString();
////　　 alert('delCookies='+document.cookie);
////}
function getCookie(Name) {
    var search = Name + "="
    if (document.cookie.length > 0) {
        offset = document.cookie.indexOf(search)
        if (offset != -1) {
            offset += search.length
            end = document.cookie.indexOf(";", offset)
            if (end == -1) end = document.cookie.length
            return unescape(document.cookie.substring(offset, end))
        }
        else return ""
    }
}


function delCookie(name) {//为了删除指定名称的cookie，可以将其过期时间设定为一个过去的时间
    var date = new Date();
    date.setTime(date.getTime() - 10000);
    document.cookie = name + "=; expires=" + date.toGMTString();
}

function setCookie(name, value, expdate) {
    var argv = expdate.arguments;
    var argc = expdate.arguments.length;
    var expires = (argc > 2) ? argv[2] : null;
    if (expires != null) {
        var LargeExpDate = new Date();
        LargeExpDate.setTime(LargeExpDate.getTime() + (expires * 1000 * 3600 * 24));
    }
    document.cookie = name + "=" + escape(value) + ((expires == null) ? "" : ("; expires=" + LargeExpDate.toGMTString()));
}



function CheckString(SourceString, TargetString) {
    var regu = "^" + TargetString + "";
    var re = new RegExp(regu);
    if (SourceString.search(re) != -1) {
        return true;
    }
    else {
        return false;
    }

}

function GetUrlParameter(paramName) {
    var returnVal = "Default";
    try {
        var paramUrl = window.location.search;
        //处理长度
        if (paramUrl.length > 0) {
            paramUrl = paramUrl.substring(1, paramUrl.length);
            var paramUrlArray = paramUrl.split("&");
            for (var i = 0; i < paramUrlArray.length; i++) {
                if (paramUrlArray[i].toLowerCase().indexOf(paramName.toLowerCase()) != -1) {
                    var temp = paramUrlArray[i].split("=");
                    if (temp[0].toLowerCase() == paramName.toLowerCase()) {
                        returnVal = temp[1];
                        break;
                    }
                }
            }
        }
    }
    catch (e) { }
    return returnVal;
}



//    function $(id) {

//        return document.getElementById(id);

//    }



function getT(e) {

    return e.target || e.srcElement;

}



function getMouseX(e) {

    return e.pageX ? e.pageX : e.clientX + document.body.scrollLeft - document.body.clientLeft;

}



function getMouseY(e) {

    return e.pageY ? e.pageY : e.clientY + document.body.scrollTop - document.body.clientTop;

}



function getRealLeft(o) {

    var l = 0;

    while (o) {

        l += o.offsetLeft - o.scrollLeft;

        o = o.offsetParent;

    }

    return (l);

}



function getRealTop(o) {

    var t = 0;

    while (o) {

        t += o.offsetTop - o.scrollTop;

        o = o.offsetParent;

    }

    return (t);

}



function cleanWhitespace(node) {

    var notWhitespace = /\S/;
    if (node) {
        for (var i = 0; i < node.childNodes.length; i++) {

            var childNode = node.childNodes[i];

            if ((childNode.nodeType == 3) && (!notWhitespace.test(childNode.nodeValue))) {

                node.removeChild(node.childNodes[i]);

                i--;
            }

            if (childNode.nodeType == 1) {

                cleanWhitespace(childNode);
            }
        }
    }
}



var SavedObject = {

    elements: new Array(),

    setStatus: function(id, s) {

        for (var i = 0; i < SavedObject.elements.length; i++) {

            if (SavedObject.elements[i][0] == id) {

                SavedObject.elements[i][1] = s;

                break;
            }
        }
    },

    getStatus: function(id) {

        for (var i = 0; i < SavedObject.elements.length; i++) {
            if (SavedObject.elements[i][0] == id) return SavedObject.elements[i];
        }
    },
    push: function(o) {

        SavedObject.elements[SavedObject.elements.length] = o;

    }
} 



