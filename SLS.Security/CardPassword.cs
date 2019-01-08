using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Shove;

namespace SLS.Security
{
    public class CardPassword
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

        /// <summary>
        /// 生成卡号
        /// </summary>
        /// <param name="CallCert"></param>
        /// <param name="AgentID"></param>
        /// <param name="CardPasswordID"></param>
        /// <returns></returns>
        public static string GenNumber(string CallCert, int AgentID, long CardPasswordID)
        {
            if (CallCert != GetCallCert())
            {
                throw new Exception("Call the CardPassword.GenNumber is request a CallCert.");
            }

            string t_CardPasswordID = AgentID.ToString() + CardPasswordID.ToString().PadLeft(12, '0');
            double t_NewCardPasswordID = 1;

            int t_Number = 0;

            for (int i = 0; i < t_CardPasswordID.Length; i++)
            {
                t_Number = int.Parse(t_CardPasswordID.Substring(i, 1));

                if (t_CardPasswordID.Substring(i, 1) == "0")
                {
                    t_Number = i + 1;
                }

                t_NewCardPasswordID = t_NewCardPasswordID * t_Number;
            }

            string t_strs = Math.Sin(t_NewCardPasswordID).ToString();
            string Key = "";

            for (int i = 0; i < t_strs.Length; i++)
            {
                if (("123456789".IndexOf(t_strs.Substring(i, 1)) >= 0) && Key.Length < 4)
                {
                    Key += t_strs.Substring(i, 1);
                }
            }

            int[,] strs = new int[10, 4];
            strs[0, 0] = 1; strs[1, 0] = 2; strs[2, 0] = 1; strs[3, 0] = 1; strs[4, 0] = 0; strs[5, 0] = 1; strs[6, 0] = 1; strs[7, 0] = 0; strs[8, 0] = 0; strs[9, 0] = 1;
            strs[0, 1] = 4; strs[1, 1] = 3; strs[2, 1] = 2; strs[3, 1] = 3; strs[4, 1] = 6; strs[5, 1] = 5; strs[6, 1] = 4; strs[7, 1] = 1; strs[8, 1] = 5; strs[9, 1] = 5;
            strs[0, 2] = 10; strs[1, 2] = 6; strs[2, 2] = 5; strs[3, 2] = 8; strs[4, 2] = 7; strs[5, 2] = 8; strs[6, 2] = 5; strs[7, 2] = 4; strs[8, 2] = 8; strs[9, 2] = 8;
            strs[0, 3] = 11; strs[1, 3] = 9; strs[2, 3] = 11; strs[3, 3] = 10; strs[4, 3] = 11; strs[5, 3] = 10; strs[6, 3] = 9; strs[7, 3] = 5; strs[8, 3] = 9; strs[9, 3] = 11;

            string Number = t_CardPasswordID;

            int j = Shove._Convert.StrToInt(Number.Substring(Number.Length - 1, 1), 1);

            Number = Number.Insert(strs[j, 3] + 4, Key.Substring(0, 1));
            Number = Number.Insert(strs[j, 2] + 4, Key.Substring(1, 1));
            Number = Number.Insert(strs[j, 1] + 4, Key.Substring(2, 1));
            Number = Number.Insert(strs[j, 0] + 4, Key.Substring(3, 1));

            // 拼合卡号
            return Number;
        }

        /// <summary>
        /// 提取卡真实ID
        /// </summary>
        /// <param name="CallCert"></param>
        /// <param name="Number"></param>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        public static long GetCardPasswordID(string CallCert, string Number, ref int AgentID)
        {
            if (CallCert != GetCallCert())
            {
                throw new Exception("Call the CardPassword.GenNumber is request a CallCert.");
            }

            AgentID = -1;

            if (String.IsNullOrEmpty(Number) || !Regex.IsMatch(Number, @"^[\d]{20}$"))
            {
                return -1;
            }

            AgentID = Shove._Convert.StrToInt(Number.Substring(0, 4), -1);

            if (AgentID < 0)
            {
                return -2;
            }

            long CardPasswordID = -4;

            try
            {
                int j = Shove._Convert.StrToInt(Number.Substring(Number.Length - 1, 1), 1);

                int[,] strs = new int[10, 4];
                strs[0, 0] = 1; strs[1, 0] = 2; strs[2, 0] = 1; strs[3, 0] = 1; strs[4, 0] = 0; strs[5, 0] = 1; strs[6, 0] = 1; strs[7, 0] = 0; strs[8, 0] = 0; strs[9, 0] = 1;
                strs[0, 1] = 4; strs[1, 1] = 3; strs[2, 1] = 2; strs[3, 1] = 3; strs[4, 1] = 6; strs[5, 1] = 5; strs[6, 1] = 4; strs[7, 1] = 1; strs[8, 1] = 5; strs[9, 1] = 5;
                strs[0, 2] = 10; strs[1, 2] = 6; strs[2, 2] = 5; strs[3, 2] = 8; strs[4, 2] = 7; strs[5, 2] = 8; strs[6, 2] = 5; strs[7, 2] = 4; strs[8, 2] = 8; strs[9, 2] = 8;
                strs[0, 3] = 11; strs[1, 3] = 9; strs[2, 3] = 11; strs[3, 3] = 10; strs[4, 3] = 11; strs[5, 3] = 10; strs[6, 3] = 9; strs[7, 3] = 5; strs[8, 3] = 9; strs[9, 3] = 11;

                string t_Number = "";

                t_Number = Number.Substring(strs[j, 0] + 4, 1);
                Number = Number.Remove(strs[j, 0] + 4, 1);
                string t_Key = t_Number;

                t_Number = Number.Substring(strs[j, 1] + 4, 1);
                Number = Number.Remove(strs[j, 1] + 4, 1);
                t_Key = t_Number + t_Key;

                t_Number = Number.Substring(strs[j, 2] + 4, 1);
                Number = Number.Remove(strs[j, 2] + 4, 1);
                t_Key = t_Number + t_Key;

                t_Number = Number.Substring(strs[j, 3] + 4, 1);
                Number = Number.Remove(strs[j, 3] + 4, 1);
                t_Key = t_Number + t_Key;

                string t_CardPasswordID = Number;
                double t_NewCardPasswordID = 1;

                int t_i = 0;

                for (int i = 0; i < t_CardPasswordID.Length; i++)
                {
                    t_i = int.Parse(t_CardPasswordID.Substring(i, 1));

                    if (t_CardPasswordID.Substring(i, 1) == "0")
                    {
                        t_i = i + 1;
                    }

                    t_NewCardPasswordID = t_NewCardPasswordID * t_i;
                }

                string t_strs = Math.Sin(t_NewCardPasswordID).ToString();
                string Key = "";

                for (int i = 0; i < t_strs.Length; i++)
                {
                    if (("123456789".IndexOf(t_strs.Substring(i, 1)) >= 0) && Key.Length < 4)
                    {
                        Key += t_strs.Substring(i, 1);
                    }
                }

                if (Key == t_Key)
                {
                    CardPasswordID = long.Parse(t_CardPasswordID.Substring(4));
                }
            }
            catch
            {
                return -3;
            }

            return CardPasswordID;
        }
    }
}
