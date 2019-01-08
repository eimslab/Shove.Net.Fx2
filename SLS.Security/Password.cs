using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

using Shove;

namespace SLS.Security
{
    public class Password
    {
        private static string GetCallCert()
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

        public static string Encrypt(string CallCert, string input)
        {
            if (CallCert != GetCallCert())
            {
                throw new Exception("Call the Password.Encrypt is request a CallCert.");
            }

            return FormsAuthentication.HashPasswordForStoringInConfigFile(input + "7ien.shovesoft.shove 中国深圳 2007-10-25", "MD5");
        }
    }
}
