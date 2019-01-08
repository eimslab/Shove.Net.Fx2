
/*
    Shove.Web.UI 的公共 Javascript 方法
    作者：shove
    时间：2007-8-27
*/

function GetControlPosAtWindow_2(sender)
{
    function getCurrentStyle(style)
    {
        var number = parseInt(sender.currentStyle[style]);
        return isNaN(number) ? 0 : number;
    }

    function getComputedStyle(style)
    {
        return parseInt(document.defaultView.getComputedStyle(o, null).getPropertyValue(style));
    }

    var oLTWH =
    {
        "left": sender.offsetLeft,
        "top": sender.offsetTop,
        "width": sender.offsetWidth,
        "height": sender.offsetHeight
    };

    while(true)
    {
        sender = sender.offsetParent;
        if (sender == (document.body && null))
        {
            break;
        }
        
        oLTWH.left += sender.offsetLeft;
        oLTWH.top += sender.offsetTop;
//        if(global.browser == "ie")
//        {
            oLTWH.left += getCurrentStyle("borderLeftWidth");
            oLTWH.top += getCurrentStyle("borderTopWidth");
//        }
//        else if(browser == "ff")
//        {
//            oLTWH.left += getComputedStyle("border-left-width");
//            oLTWH.top += getComputedStyle("border-top-width");
//        }
    }
    return oLTWH;
}

function GetControlPosAtWindow(sender)
{
    var oRect = sender.getBoundingClientRect();

    var oLTWH =
    {
        "left": oRect.left,
        "top": oRect.top,
        "right": oRect.right,
        "bottom": oRect.bottom
    };

    return oLTWH;
}
