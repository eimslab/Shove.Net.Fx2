
/*
    选择省份、城市。
    作者：shove
    时间：2007-8-2
*/

function ShoveWebUI_ShoveProvinceCityInput_ProvinceOnChange(sender)
{
    Shove.Web.UI.ShoveProvinceCityInput.SetProvinceID(parseInt(sender.value));

    var objCity = document.getElementById(sender.id.replace("ddlProvince", "ddlCity"));
    if (!objCity)
    {
        return;
    }

    while (objCity.length > 0)
    {
        objCity.remove(0);
    }

    var CityList = Shove.Web.UI.ShoveProvinceCityInput.GetCityList(parseInt(sender.value));
    if (CityList == null)
    {
        return;
    }

    CityList = CityList.value;
    if (CityList == null)
    {
        return;
    }
    if (CityList.length == 0)
    {
        return;
    }

    CityList = CityList.split(";");
    for (var i = 0; i < CityList.length; i ++)
    {
        var strs = CityList[i].split(",");
        if (strs.length != 2)
        {
            continue;
        }

        var customOptions = document.createElement("OPTION");
        customOptions.text = strs[1];
        customOptions.value = strs[0];
	    objCity.add(customOptions, objCity.length);
    }

    objCity.selectedIndex = 0;
    Shove.Web.UI.ShoveProvinceCityInput.SetCityID(parseInt(objCity.value));
}

function ShoveWebUI_ShoveProvinceCityInput_CityOnChange(sender)
{
    Shove.Web.UI.ShoveProvinceCityInput.SetCityID(parseInt(sender.value));
}

function ShoveWebUI_ShoveProvinceCityInput_FillProvince(sender_id)
{
    var objProvince = document.getElementById(sender_id);
    
    if (!objProvince)
    {
        return;
    }

    while (objProvince.length > 0)
    {
        objProvince.remove(0);
    }

    var ProvinceList = Shove.Web.UI.ShoveProvinceCityInput.GetProvinceList();
    if (ProvinceList == null)
    {
        return;
    }

    ProvinceList = ProvinceList.value;
    if (ProvinceList == null)
    {
        return;
    }

    if (ProvinceList.length == 0)
    {
        return;
    }

    ProvinceList = ProvinceList.split(";");
    for (var i = 0; i < ProvinceList.length; i ++)
    {
        var strs = ProvinceList[i].split(",");
        if (strs.length != 2)
        {
            continue;
        }

        var customOptions = document.createElement("OPTION");
        customOptions.text = strs[1];
        customOptions.value = strs[0];
	    objProvince.add(customOptions, objProvince.length);
    }

    var ProvinceID = Shove.Web.UI.ShoveProvinceCityInput.GetProvinceID().value;
    ShoveWebUI_ShoveProvinceCityInput_SetSelectIndexByValue(objProvince, ProvinceID);
}

function ShoveWebUI_ShoveProvinceCityInput_FillCity(sender_id)
{
    var objCity = document.getElementById(sender_id);
    
    if (!objCity)
    {
        return;
    }

    while (objCity.length > 0)
    {
        objCity.remove(0);
    }

    var ProvinceID = Shove.Web.UI.ShoveProvinceCityInput.GetProvinceID().value;
    var CityList = Shove.Web.UI.ShoveProvinceCityInput.GetCityList(ProvinceID);
    if (CityList == null)
    {
        return;
    }

    CityList = CityList.value;
    if (CityList == null)
    {
        return;
    }

    if (CityList.length == 0)
    {
        return;
    }

    CityList = CityList.split(";");
    for (var i = 0; i < CityList.length; i ++)
    {
        var strs = CityList[i].split(",");
        if (strs.length != 2)
        {
            continue;
        }

        var customOptions = document.createElement("OPTION");
        customOptions.text = strs[1];
        customOptions.value = strs[0];
	    objCity.add(customOptions, objCity.length);
    }

    var CityID = Shove.Web.UI.ShoveProvinceCityInput.GetCityID().value;
    ShoveWebUI_ShoveProvinceCityInput_SetSelectIndexByValue(objCity, CityID);
}

function ShoveWebUI_ShoveProvinceCityInput_SetSelectIndexByValue(sender, value)
{
    if (!sender)
    {
        return false;
    }
    
    for (var i = 0; i < sender.options.length; i ++)
    {
        if (sender.options[i].value == value)
        {
            sender.selectedIndex = i;
            return true;
        }
    }
    
    return false;
}