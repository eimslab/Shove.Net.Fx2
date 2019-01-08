using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Xml;

namespace ShoveEIMS3
{
    internal class SystemConfig
    {
        private string ConfigFileName;

        XmlDocument xmldoc = null;

        private long _soft_owner_id = 0;
        public long soft_owner_id
        {
            get
            {
                return _soft_owner_id;
            }
            set
            {
                _soft_owner_id = value;

                SetValue("soft_owner_id", value);
            }
        }

        private short _soft_type = 0;
        public short soft_type
        {
            get
            {
                return _soft_type;
            }
            set
            {
                _soft_type = value;

                SetValue("soft_type", value);
            }
        }

        private short _default_form = 0;
        public short default_form
        {
            get
            {
                return _default_form;
            }
            set
            {
                _default_form = value;

                SetValue("default_form", value);
            }
        }

        private short _is_actived = 0;
        public short is_actived
        {
            get
            {
                return _is_actived;
            }
            set
            {
                _is_actived = value;

                SetValue("is_actived", value);
            }
        }

        private string _verify_sum = "";
        public string verify_sum
        {
            get
            {
                return _verify_sum;
            }
            set
            {
                _verify_sum = value;

                SetValue("verify_sum", value);
            }
        }

        public SystemConfig()
        {
            ConfigFileName = System.AppDomain.CurrentDomain.BaseDirectory + "System.config";

            xmldoc = new XmlDocument();
            xmldoc.Load(ConfigFileName);

            _soft_owner_id = GetValue<long>("soft_owner_id");
            _soft_type = GetValue<short>("soft_type");
            _default_form = GetValue<short>("default_form");
            _is_actived = GetValue<short>("is_actived");
            _verify_sum = GetValue<string>("verify_sum");

            if (verify_sum.ToLower() != GenVerifySum().ToLower())
            {
                throw new Exception("无效的系统配置文件 System.config。请与系统开发商联系。");
            }
        }

        private T GetValue<T>(string key)
        {
            XmlNodeList nodeList = xmldoc.GetElementsByTagName(key);
            string value = "";

            if (nodeList.Count > 0)
            {
                value = String.IsNullOrEmpty(nodeList[0].InnerText) ? "" : nodeList[0].InnerText;
            }

            T Result = default(T);

            if (typeof(T) == typeof(string))
            {
                Result = (T)(object)value;

                return Result;
            }
            else if (typeof(T) == typeof(long))
            {
                long res;

                if (!long.TryParse(value, out res))
                {
                    res = 0;
                }

                Result = (T)(object)res;

                return Result;
            }
            else if (typeof(T) == typeof(short))
            {
                short res;

                if (!short.TryParse(value, out res))
                {
                    res = 0;
                }

                Result = (T)(object)res;

                return Result;
            }

            return Result;
        }

        private bool SetValue(string key, object value)
        {
            XmlNodeList nodeList = xmldoc.GetElementsByTagName(key);

            if (nodeList.Count == 0)
            {
                return false;
            }

            nodeList[0].InnerText = value.ToString();

            nodeList = xmldoc.GetElementsByTagName("verify_sum");

            if (nodeList.Count == 0)
            {
                return false;
            }

            nodeList[0].InnerText = GenVerifySum();

            return true;
        }

        public bool Save()
        {
            xmldoc.Save(ConfigFileName);

            return true;
        }

        public string GenVerifySum()
        {
            byte[] t = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(soft_owner_id + soft_type.ToString() + default_form.ToString() + is_actived.ToString() + "qwertyu1qwertyu3qwertyu6"));
            StringBuilder sb = new StringBuilder(32);

            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }
    }
}
