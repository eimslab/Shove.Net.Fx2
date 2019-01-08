
/*
    带软键盘的密码输入控件
    作者：shove
    时间：2007-8-24
*/

var ShoveWebUI_ShovePasswordTextBox_UpperLowerState = "lower";
var ShoveWebUI_ShovePasswordTextBox_MouseAtObject = null;

function ShoveWebUI_ShovePasswordTextBox_OnClick(sender)
{
    var objDiv = document.getElementById(sender.id + "_DivKeyBoard");
    if (!objDiv)
    {
        return;
    }
    
    var pos = ShoveWebUI_ShovePasswordTextBox_GetControlPosAtWindow(sender);
    objDiv.style.left = pos.left;
    objDiv.style.top = pos.bottom;
    
    objDiv.style.display = "";
    
    var objUpperLowerStatus = document.getElementById(sender.id + "_DivKeyBoard_UpperLowerStatus");
    if (objUpperLowerStatus)
    {
        objUpperLowerStatus.innerText = (ShoveWebUI_ShovePasswordTextBox_UpperLowerState == "lower") ? "[小写]" : "[大写]";
    }
}

function ShoveWebUI_ShovePasswordTextBox_OnBlur(sender)
{
    var objDiv = document.getElementById(sender.id + "_DivKeyBoard");
    if (!objDiv)
    {
        return;
    }

    if (ShoveWebUI_ShovePasswordTextBox_MouseAtObject != objDiv)
    {
        objDiv.style.display = "none";
    }
}

function ShoveWebUI_ShovePasswordInputKeyBoard_OnKeyPress(Key, objTextBox_id)
{
    var objTextBox = document.getElementById(objTextBox_id);
    if (!objTextBox)
    {
        return;
    }

    objTextBox.focus();
    
    var objUpperLowerStatus = document.getElementById(objTextBox_id + "_DivKeyBoard_UpperLowerStatus");
    if (!objUpperLowerStatus)
    {
        return;
    }
    
    var objDiv = document.getElementById(objTextBox_id + "_DivKeyBoard");
    if (!objDiv)
    {
        return;
    }

    var KeyChar = Key;
    
    if (Key == "Backspace")
    {
        if (objTextBox.value != "")
        {
            objTextBox.value = objTextBox.value.substring(0, objTextBox.value.length - 1);
        }
        return;
    }
    else if (Key == "CapsLock")
    {
        ShoveWebUI_ShovePasswordTextBox_UpperLowerState = (ShoveWebUI_ShovePasswordTextBox_UpperLowerState == "lower") ? "upper" : "lower";
        objUpperLowerStatus.innerText = (ShoveWebUI_ShovePasswordTextBox_UpperLowerState == "lower") ? "[小写]" : "[大写]";
        return;
    }
    else if (Key == "Quotes")
    {
        KeyChar = "'" + '"';
    }
    else if (Key == "Enter")
    {
        objDiv.style.display = "none";
        objTextBox.focus();
        return;
    }
    else if (Key == "Shift")
    {
        ShoveWebUI_ShovePasswordTextBox_UpperLowerState = (ShoveWebUI_ShovePasswordTextBox_UpperLowerState == "lower") ? "upper" : "lower";
        objUpperLowerStatus.innerText = (ShoveWebUI_ShovePasswordTextBox_UpperLowerState == "lower") ? "[小写]" : "[大写]";
        return;
    }
    
    if (KeyChar.length != 2)
    {
        return;
    }
    
    if (ShoveWebUI_ShovePasswordTextBox_UpperLowerState == "lower")
    {
        objTextBox.value += KeyChar.substring(0, 1);
    }
    else
    {
        objTextBox.value += KeyChar.substring(1, 2);
    }
}

function ShoveWebUI_ShovePasswordInputKeyBoard_OnClear(objTextBox_id)
{
    var objTextBox = document.getElementById(objTextBox_id);
    if (!objTextBox)
    {
        return;
    }

    objTextBox.value = "";
}

function ShoveWebUI_ShovePasswordInputKeyBoard_OnClose(objDiv_id, objTextBox_id)
{
    var objTextBox = document.getElementById(objTextBox_id);
    if (!objTextBox)
    {
        return;
    }

    var objDiv = document.getElementById(objTextBox_id + "_DivKeyBoard");
    if (!objDiv)
    {
        return;
    }

    objDiv.style.display = "none";
    objTextBox.focus();
}

function ShoveWebUI_ShovePasswordTextBox_SetMouseAtObject(sender, value)
{
    if (value)
    {
        ShoveWebUI_ShovePasswordTextBox_MouseAtObject = sender;
    }
    else
    {
        ShoveWebUI_ShovePasswordTextBox_MouseAtObject = null;
    }
}

function ShoveWebUI_ShovePasswordTextBox_GetControlPosAtWindow(sender)
{
    var oRect = sender.getBoundingClientRect();

    var oLTRB =
    {
        "left": oRect.left + document.body.scrollLeft + document.documentElement.scrollLeft,
        "top": oRect.top + document.body.scrollTop + document.documentElement.scrollTop,
        "right": oRect.right + document.body.scrollTop + document.documentElement.scrollTop,
        "bottom": oRect.bottom + document.body.scrollTop + document.documentElement.scrollTop
    };

    return oLTRB;
}
