
/*
    实现用 Ajax 取服务器时间的 Label
    作者：shove
    时间：2007-8-23
*/

var ShoveWebUI_ShoveDateTimeLabel_times = 0;
var ShoveWebUI_ShoveDateTimeLabel_sender = null;

function ShoveWebUI_ShoveDateTimeLabel_ShowTime(sender, DateTimeFrom)
{
    ShoveWebUI_ShoveDateTimeLabel_times = 0;
    ShoveWebUI_ShoveDateTimeLabel_sender = sender;
    
    if (!ShoveWebUI_ShoveDateTimeLabel_sender)
    {
        return;
    }

    if (DateTimeFrom == "Server")
    {
        window.setInterval("ShoveWebUI_ShoveDateTimeLabel_ShowServerTime();", 1000);
    }
    else
    {
        window.setInterval("ShoveWebUI_ShoveDateTimeLabel_ShowClientTime();", 1000);
    }
}

function ShoveWebUI_ShoveDateTimeLabel_ShowServerTime()
{
    function ShoveWebUI_ShoveDateTimeLabel_GetServerTime_Receive(response)
    {
	    if (response == null)
	    {
		    return;
        }
        
	    if (response.value == null)
	    {
		    return;
	    }
    	
	    var r = response.value;
	    if (typeof(r) != "string")
	    {
		    return;
	    }
	    if (r == null)
	    {
		    return;
	    }
	    if (r == "")
	    {
		    return;
	    }
    	
        ShoveWebUI_ShoveDateTimeLabel_sender.innerText = r;
    }

	if (ShoveWebUI_ShoveDateTimeLabel_times >= 30)
	{
		ShoveWebUI_ShoveDateTimeLabel_times = 0;
		
		try
		{
			Shove.Web.UI.ShoveDateTimeLabel.GetServerTime(ShoveWebUI_ShoveDateTimeLabel_GetServerTime_Receive);
		}
		catch(e) { }
	}
	ShoveWebUI_ShoveDateTimeLabel_times ++;

	var ShoveWebUI_ShoveDateTimeLabel_SystemTime;
	try
	{
		ShoveWebUI_ShoveDateTimeLabel_SystemTime = new Date(sender.innerText.replace("年", "/").replace("月", "/").replace("日", ""));
		ShoveWebUI_ShoveDateTimeLabel_SystemTime.setSeconds(ShoveWebUI_ShoveDateTimeLabel_SystemTime.getSeconds() + 1);
	}
	catch(e)
	{
		ShoveWebUI_ShoveDateTimeLabel_SystemTime = new Date();
	}
	
	ShoveWebUI_ShoveDateTimeLabel_sender.innerText = ShoveWebUI_ShoveDateTimeLabel_SystemTime.toLocaleString();
}

function ShoveWebUI_ShoveDateTimeLabel_ShowClientTime()
{
	ShoveWebUI_ShoveDateTimeLabel_sender.innerText = (new Date()).toLocaleString();
}
