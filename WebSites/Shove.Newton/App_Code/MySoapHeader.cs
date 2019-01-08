using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
///MySoapHeader 的摘要说明
/// </summary>
public class MySoapHeader : System.Web.Services.Protocols.SoapHeader
{
    public string UserName = string.Empty;
    public string Password = string.Empty;

	public MySoapHeader()
	{
	}

    public MySoapHeader(string UserName, string Password)
    {
        this.UserName = UserName;
        this.Password = Password;
    }

    public bool Valid()
    {
        if (UserName != "admin" || Password != "admin")
            return false;

        return true;
    }
}