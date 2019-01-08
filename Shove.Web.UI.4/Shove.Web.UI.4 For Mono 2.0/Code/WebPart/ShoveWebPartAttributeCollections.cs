using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web.UI;

namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPart 用户控件的属性集合
    /// </summary>
    public class ShoveWebPartAttributeCollections
    {
        public ArrayList alKey;
        public ArrayList alValue;

        public ShoveWebPartAttributeCollections()
        {
            alKey = new ArrayList();
            alValue = new ArrayList();
        }

        public string this[int Key]
        {
            get
            {
                int Locate = FindKey(Key);

                if (Locate < 0)
                {
                    return null;
                }

                return alValue[Locate].ToString();
            }
            set
            {
                int Locate = FindKey(Key);

                if (Locate < 0)
                {
                    alKey.Add(Key);
                    alValue.Add(value);
                }
                else
                {
                    alValue[Locate] = value;
                }
            }
        }

        public void Remove(int Key)
        {
            int Locate = FindKey(Key);

            if (Locate >= 0)
            {
                alKey.RemoveAt(Locate);
                alValue.RemoveAt(Locate);
            }
        }

        public void Clear()
        {
            alKey.Clear();
            alValue.Clear();
        }

        public override string ToString()
        {
            string Result = "";

            for (int i = 0; i < alKey.Count; i++)
            {
                Result += (Result == "" ? "" : "&") + alKey[i].ToString() + "=" + System.Web.HttpUtility.UrlEncode(alValue[i].ToString());
            }

            return Result;
        }

        public void FromString(string AttributesString, ShoveWebPartUserControl swpuc)
        {
            Clear();

            if (AttributesString == "")
            {
                return;
            }

            string[] strs = AttributesString.Split('&');

            foreach (string str in strs)
            {
                string[] t = str.Split('=');

                if (t.Length != 2)
                {
                    continue;
                }

                int key = Convert.ToInt32(t[0]);

                if (swpuc != null && swpuc.swpas.Length > key && (swpuc.swpas[key]).Type == "Image" && string.IsNullOrEmpty(t[1]))
                {
                    t[1] = "about:blank";
                }

                this[Convert.ToInt32(t[0])] = System.Web.HttpUtility.UrlDecode(t[1]);
            }
        }

        public int FindKey(int Key)
        {
            for (int i = 0; i < alKey.Count; i++)
            {
                if (alKey[i].ToString() == Key.ToString())
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
