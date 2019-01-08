/*
ShoveWebPart 基本代码。
作者：shove
时间：2007-8-2
*/

// 获取所有 Part
function ShoveWebUI_ShoveWebPart_GetParts(isWithLocked, isWithHidden) {
    var objParts = [];
    var objDivs = document.getElementsByTagName("div");

    for (var i = 0; i < objDivs.length; i++) {
        // 不是本类型控件，不处理
        var ShoveWebUITypeName = objDivs[i].getAttribute("ShoveWebUITypeName");

        if (ShoveWebUITypeName != "ShoveWebPart") {
            continue;
        }

        // 如果不包含锁定
        if (!isWithLocked) {
            var LockState = objDivs[i].getAttribute("Lock");

            if (LockState == "True") {
                continue;
            }
        }

        // 如果是不显示的控件，不处理
        if (!isWithHidden) {
            if (objDivs[i].style.visibility == "hidden") {
                continue;
            }
        }

        objParts.push(objDivs[i]);
    }

    return objParts;
}

function ShoveWebUI_ShoveWebPart_GetParentLeft(sender) {
    if(sender==null)
    {
        return ;
    }
    
    var o_ParentDiv = sender.parentNode;
    var ParentLeft = "0px";

    if (o_ParentDiv) {
        ParentLeft = (o_ParentDiv.offsetLeft + o_ParentDiv.scrollLeft) + "px";
    }

    if (ParentLeft == "px") {
        ParentLeft = "0px";
    }

    return ParentLeft;
}

function ShoveWebUI_ShoveWebPart_GetParentTop(sender) {
    var o_ParentDiv = sender.parentNode;
    var ParentTop = "0px";

    if (o_ParentDiv) {
        ParentTop = (o_ParentDiv.offsetTop + o_ParentDiv.scrollTop) + "px";
    }

    if (ParentTop == "px") {
        ParentTop = "0px";
    }

    return ParentTop;
}

function ShoveWebUI_ShoveWebPart_GetParentWidth(sender) {
    var o_ParentDiv = sender.parentNode;
    var ParentWidth = "0px";

    if (o_ParentDiv) {
        ParentWidth = o_ParentDiv.style.width;
    }

    if ((ParentWidth == "") || (ParentWidth == "auto") || (ParentWidth == "0px")) {
        ParentWidth = (o_ParentDiv.offsetWidth) + "px";

        if (ParentWidth == "px") {
            ParentWidth = "0px";
        }
    }

    if (ParentWidth == "0px") {
        ParentWidth = (o_ParentDiv.scrollWidth) + "px";

        if (ParentWidth == "px") {
            ParentWidth = "0px";
        }
    }

    return ParentWidth;
}

function ShoveWebUI_ShoveWebPart_OffsetLeft(sender, ParentLeft) {
    if (typeof (sender) == "string") {
        sender = document.getElementById(sender);
    }

    if (!sender) {
        return;
    }

    var OffsetLeft = sender.getAttribute("OffsetLeft");

    if (!OffsetLeft) {
        return;
    }

    if (OffsetLeft == "") {
        OffsetLeft = "0px";
    }

    if (!ParentLeft) {
        ParentLeft = ShoveWebUI_ShoveWebPart_GetParentLeft(sender);
    }

    if (ParentLeft != OffsetLeft) {
       // sender.style.left = (parseInt(sender.style.left.replace("px", "")) - (parseInt(OffsetLeft.replace("px", "")) - parseInt(ParentLeft.replace("px", "")))) + "px";   //0902    
        
        OffsetLeft = ParentLeft;

        sender.setAttribute("OffsetLeft", OffsetLeft);
    }
}

if (window.addEventListener) {
    window.addEventListener("resize", ShoveWebUI_ShoveWebPart_OnResize, false);
}
else {
    window.onresize = function ShoveWebUI_ShoveWebPart_OnResize_IE() {
        if (document.readyState == "complete") {
            ShoveWebUI_ShoveWebPart_OnResize();
        }
    }
}

function ShoveWebUI_ShoveWebPart_OnResize(objParts) {
    if (!objParts) {
        objParts = ShoveWebUI_ShoveWebPart_GetParts(true, false);
    }

    if (objParts.length < 1) {
        return;
    }

    var ParentLeft = ShoveWebUI_ShoveWebPart_GetParentLeft(objParts[0]);

    for (var i = 0; i < objParts.length; i++) {
        ShoveWebUI_ShoveWebPart_OffsetLeft(objParts[i], ParentLeft);
    }
}

if (document.addEventListener) {
    document.addEventListener("DOMContentLoaded", ShoveWebUI_ShoveWebPart_OffsetTop, false);
}
else {
    document.onreadystatechange = function ShoveWebUI_ShoveWebPart_OffsetTop_IE() {
        if (document.readyState == "complete") {
            ShoveWebUI_ShoveWebPart_OffsetTop();
        }
    }
}

function ShoveWebUI_ShoveWebPart_OffsetTop() {
    var objParts = ShoveWebUI_ShoveWebPart_GetParts(true, false).sort(Sort);

    // 调用偏移 Left。
    ShoveWebUI_ShoveWebPart_OnResize(objParts);

    function Sort(obj1, obj2) {
        var top1 = parseInt(obj1.style.top.replace("px", ""));
        var left1 = parseInt(obj1.style.left.replace("px", ""));
        var top2 = parseInt(obj2.style.top.replace("px", ""));
        var left2 = parseInt(obj2.style.left.replace("px", ""));

        if ((top1 > top2) || ((top1 == top2) && (left1 > left2))) {
            return 1;
        }

        return -1;
    }

    function FilterRepeated(objArray) {
        if (objArray == null) {
            return objArray;
        }

        var NewArray = [];

        for (var i = 0; i < objArray.length; i++) {
            var isExist = false;

            for (var j = 0; j < NewArray.length; j++) {
                if (objArray[i].id == NewArray[j].id) {
                    isExist = true;
                    break;
                }
            }

            if (!isExist) {
                NewArray.push(objArray[i]);
            }
        }

        return NewArray;
    }

    function GetHeight(sender) {
        var Height = sender.style.height;

        if (!Height) {
            return 0;
        }

        return parseInt(Height.replace("px", ""));
    }

    function GetPrimitiveHeight(sender) {
        var PrimitiveHeight = sender.getAttribute("PrimitiveHeight");

        if (!PrimitiveHeight) {
            return 0;
        }

        if (PrimitiveHeight == "") {
            return 0;
        }

        return parseInt(PrimitiveHeight.replace("px", ""));
    }

    function GetScrollHeight(sender) {
        var ScrollHeight = sender.scrollHeight;

        if (!ScrollHeight) {
            return 0;
        }

        return parseInt(ScrollHeight);
    }

    function GetRealityHeight(sender) {
        return IsAutoHeight(sender) ? GetScrollHeight(sender) : GetHeight(sender);
    }

    function GetTop(sender) {
        var Top = sender.style.top;

        if (!Top) {
            return 0;
        }

        return parseInt(Top.replace("px", ""));
    }

    function GetPrimitiveTop(sender) {
        var PrimitiveTop = sender.getAttribute("PrimitiveTop");

        if (!PrimitiveTop) {
            return 0;
        }

        return parseInt(PrimitiveTop);
    }

    function IsAutoHeight(sender) {
        return (sender.style.height == "auto");
    }

    function IsInfluenced(sender, higher) {
        var HigherBottom = GetTop(higher) + GetPrimitiveHeight(higher);

        // top不低于自己的情况，不受其影响
        if (GetTop(sender) <= HigherBottom) {
            return false;
        }

        var Left = parseInt(sender.style.left.replace("px", ""));
        var Right = Left + parseInt(sender.style.width.replace("px", ""));

        var HigherLeft = parseInt(higher.style.left.replace("px", ""));
        var HigherRight = Left + parseInt(higher.style.width.replace("px", ""));

        // 控件没有覆盖住，不受其影响
        if ((Right < HigherLeft) || (Left > HigherRight)) {
            return false;
        }

        return true;
    }

    // 调整顶
    var StartOffsetLocate = -1;

    for (var i = 0; i < objParts.length; i++) {
        if (IsAutoHeight(objParts[i])) {
            StartOffsetLocate = i;

            break;
        }
    }

    if (StartOffsetLocate > (objParts.length - 2)) {
        return;
    }

    // 保存原始高度属性
    for (var i = 0; i < objParts.length; i++) {
        objParts[i].setAttribute("PrimitiveTop", GetTop(objParts[i]));
    }

    // 开始调整
    for (var i = StartOffsetLocate + 1; i < objParts.length; i++) {
        var objBestUnder = objParts[0];
        var objPrimitiveBestUnder = objParts[0];

        var isHasInfluenced = false;

        for (var j = 0; j < i; j++) {
            if (!IsInfluenced(objParts[i], objParts[j])) {
                continue;
            }

            isHasInfluenced = true;

            var BestUnder = GetTop(objBestUnder) + GetRealityHeight(objBestUnder);
            var CurrentUnder = GetTop(objParts[j]) + GetRealityHeight(objParts[j]);

            if (BestUnder < CurrentUnder) {
                objBestUnder = objParts[j];
            }

            BestUnder = GetPrimitiveTop(objPrimitiveBestUnder) + GetPrimitiveHeight(objPrimitiveBestUnder);
            CurrentUnder = GetPrimitiveTop(objParts[j]) + GetPrimitiveHeight(objParts[j]);

            if (BestUnder < CurrentUnder) {
                objPrimitiveBestUnder = objParts[j];
            }
        }

        if ((!isHasInfluenced) || (objBestUnder == objParts[i])) {
            continue;
        }

        var TopOffset = (GetTop(objBestUnder) + GetRealityHeight(objBestUnder)) - (GetPrimitiveTop(objBestUnder) + GetPrimitiveHeight(objBestUnder));

        if (TopOffset < 0) {
            if (objParts[i].getAttribute("TopUpLimit") == "True") {
                continue;
            }
        }

        if ((objBestUnder == objPrimitiveBestUnder) || (TopOffset < 0)) {
            objParts[i].style.top = (GetTop(objParts[i]) + TopOffset) + "px";

            continue;
        }

        var PrimitiveTopOffset = GetPrimitiveTop(objParts[i]) - (GetPrimitiveTop(objPrimitiveBestUnder) + GetPrimitiveHeight(objPrimitiveBestUnder));

        objParts[i].style.top = (GetTop(objBestUnder) + GetRealityHeight(objBestUnder) + PrimitiveTopOffset) + "px";
    }
}
