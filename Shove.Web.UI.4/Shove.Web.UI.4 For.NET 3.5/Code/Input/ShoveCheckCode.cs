using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveCheckCode ��ժҪ˵����
    /// </summary>
    /// 

    [DefaultProperty("Charset"), ToolboxData("<{0}:ShoveCheckCode runat=server></{0}:ShoveCheckCode>")]
    public class ShoveCheckCode : WebControl, INamingContainer
    {
        private System.Web.UI.HtmlControls.HtmlInputHidden tbCode = null;
        private System.Web.UI.WebControls.Image imgCode = null;

        public enum CharSet
        {
            All = 0,
            OnlyNumeric = 1,
            OnlyLetterUpper = 2,
            OnlyLetterLower = 3,
            OnlyLetter = 4
        }

        public ShoveCheckCode()
        {
            Width = 50;
            Height = 20;
            ForeColor = Color.Blue;
            BackColor = Color.MistyRose;

            tbCode = new System.Web.UI.HtmlControls.HtmlInputHidden();
            tbCode.ID = "tbCode";
            this.Controls.Add(tbCode);

            imgCode = new System.Web.UI.WebControls.Image();
            imgCode.ID = "imgCode";
            imgCode.Attributes.Add("onclick", "var newCode = Shove.Web.UI.ShoveCheckCode.Refresh(this.CharSet).value; this.parentNode.childNodes[0].value = newCode; var start = this.src.indexOf('?CheckCode='); var end = this.src.indexOf('&', start + 1); this.src = this.src.replace(this.src.substr(start, end - start), '?CheckCode=' + newCode);");
            imgCode.Style.Add("cursor", "pointer");
            imgCode.Attributes.Add("alt", RefreshTipText);
            this.Controls.Add(imgCode);
        }

        protected override void OnLoad(EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ShoveCheckCode));
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string Refresh(string cs)
        {
            Encrypt encrypt = new Encrypt();

            return encrypt.EncryptString(GetRandNum(cs));
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string CheckCode
        {
            get
            {
                Encrypt encrypt = new Encrypt();
                tbCode.Value = encrypt.EncryptString(GetRandNum());

                return tbCode.Value;
            }

            //set
            //{
            //	this.ViewState["CheckCode"] = value.Trim().PadRight(4, ' ');
            //}
        }

        [Bindable(true), Category("Appearance"), DefaultValue("ShoveWebUI_client"), Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("��ϵ�пؼ���֧��Ŀ¼�������ӵ���ص�ͼƬ���ű��ļ�")]
        public string SupportDir
        {
            get
            {
                object obj = this.ViewState["SupportDir"];
                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "ShoveWebUI_client";
                }
            }

            set
            {
                this.ViewState["SupportDir"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("�����壬������껻һ����֤��"), Description("�����壬������껻һ����֤���������ʾ")]
        public string RefreshTipText
        {
            get
            {
                object obj = this.ViewState["RefreshTipText"];

                return ((obj == null) ? "�����壬������껻һ����֤��" : obj.ToString());
            }

            set
            {
                this.ViewState["RefreshTipText"] = value;
            }
        }

        //[Bindable(true),
        //Category("Appearance"),
        //DefaultValue("ShoveWebUI_client/CheckCode/BuildImage.aspx"),
        //Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor)),
        //Description("��֤���ͼƬ Url����ָ���Ŀ¼�� ShoveWebUI_client/CheckCode/BuildImage.aspx")]
        //public string ImageUrl
        //{
        //    get
        //    {
        //        object obj = this.ViewState["ImageUrl"];
        //        if (obj != null)
        //        {
        //            return (string) obj;
        //        }
        //        return "ShoveWebUI_client/CheckCode/BuildImage.aspx";
        //    }

        //    set
        //    {
        //        this.ViewState["ImageUrl"] = value;
        //    }
        //}

        [Bindable(true), Category("Appearance"), DefaultValue(CharSet.All), Description("��֤���ַ���")]
        public CharSet Charset
        {
            get
            {
                object obj = this.ViewState["Charset"];
                if (obj != null)
                {
                    return (CharSet)obj;
                }
                else
                {
                    return CharSet.All;
                }
            }

            set
            {
                this.ViewState["Charset"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(true), Description("�Ƿ���Ҫ�����������ߣ�")]
        public bool isInterferenceBackgroup
        {
            get
            {
                object obj = this.ViewState["isInterferenceBackgroup"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                else
                {
                    return true;
                }
            }

            set
            {
                this.ViewState["isInterferenceBackgroup"] = value;
            }
        }

        private string GetRandNum()
        {
            string Vchar;

            switch (Charset)
            {
                case CharSet.All:
                    Vchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    break;
                case CharSet.OnlyNumeric:
                    Vchar = "0123456789";
                    break;
                case CharSet.OnlyLetterUpper:
                    Vchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case CharSet.OnlyLetterLower:
                    Vchar = "abcdefghijklmnopqrstuvwxyz";
                    break;
                case CharSet.OnlyLetter:
                    Vchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;
                default:
                    Vchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    break;
            }

            string VNum = "";

            Random rand = new Random(GetRandomSeed());
            for (int i = 1; i <= 4; i++)
            {
                string t_ch = "";
                while ((VNum.IndexOf(t_ch) >= 0) || (t_ch == ""))
                    t_ch = Vchar[rand.Next(Vchar.Length - 1)].ToString();
                VNum += t_ch;
            }

            return VNum;
        }

        private string GetRandNum(string cs)
        {
            string Vchar;

            switch (cs)
            {
                case "All":
                    Vchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    break;
                case "OnlyNumeric":
                    Vchar = "0123456789";
                    break;
                case "OnlyLetterUpper":
                    Vchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case "OnlyLetterLower":
                    Vchar = "abcdefghijklmnopqrstuvwxyz";
                    break;
                case "OnlyLetter":
                    Vchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;
                default:
                    Vchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    break;
            }

            string VNum = "";

            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 1; i <= 4; i++)
            {
                string t_ch = "";
                while ((VNum.IndexOf(t_ch) >= 0) || (t_ch == ""))
                    t_ch = Vchar[rand.Next(Vchar.Length - 1)].ToString();
                VNum += t_ch;
            }

            return VNum;
        }

        /// <summary>
        /// ��ȡ��������ӣ�Ϊ�˱�����ͬһҳ��Ŷ����֤��ؼ���ʱ��Ĭ�ϵ���ʱ��Ϊ�����������������ʱ����ɶ���ؼ�����֤��һ���������
        /// </summary>
        /// <returns></returns>
        private int GetRandomSeed()
        {
            return DateTime.Now.Millisecond + (int)this.ID[this.ID.Length - 1];
        }

        private bool IsDesignMode
        {
            get
            {
                return (Site != null) ? Site.DesignMode : false;
            }
        }

        /// <summary>
        /// У���û��������֤���Ƿ���ȷ
        /// </summary>
        /// <param name="input">�û��������֤��</param>
        /// <returns></returns>
        public bool Valid(string input)
        {
            return Valid(input, tbCode.Value);
        }

        /// <summary>
        /// У���û��������֤���Ƿ���ȷ
        /// </summary>
        /// <param name="input">�û��������֤��</param>
        /// <param name="Code">���ܵ���֤��</param>
        /// <returns></returns>
        public bool Valid(string input, string Code)
        {
            Encrypt encrypt = new Encrypt();
            Code = encrypt.UnEncryptString(Code);
            bool Result = (Code.ToUpper() == input.Trim().ToUpper());

            return Result;
        }

        /// <summary> 
        /// ���˿ؼ����ָ�ָ�������������
        /// </summary>
        /// <param name="output"> Ҫд������ HTML ��д�� </param>
        protected override void Render(HtmlTextWriter output)
        {
            imgCode.Width = Width;
            imgCode.Height = Height;
            imgCode.ForeColor = ForeColor;
            imgCode.BackColor = BackColor;

            imgCode.Attributes.Add("CharSet", Charset.ToString());

            if (IsDesignMode)
            {
                imgCode.ImageUrl = "about:blank";
            }
            else
            {
                Encrypt encrypt = new Encrypt();

                string t = SupportDir + "/Page/BuildImage.aspx?CheckCode=" + CheckCode + "&ForeColor=" + encrypt.EncryptString(ForeColor.Name) + "&BackColor=" + encrypt.EncryptString(BackColor.Name) + "&Width=" + encrypt.EncryptString(Width.ToString()) + "&Height=" + encrypt.EncryptString(Height.ToString()) + "&FontName=" + encrypt.EncryptString(Font.Name) + "&FontSize=" + encrypt.EncryptString(Font.Size.ToString()) + "&isInterferenceBackgroup=" + encrypt.EncryptString(isInterferenceBackgroup.ToString());

                if (!t.StartsWith("../") && !t.StartsWith("~/"))
                {
                    t = Page.ResolveUrl("~/" + t);
                }

                imgCode.ImageUrl = t;
            }

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveCheckCode Start -->");

            base.RenderBeginTag(output);
            output.WriteLine();

            base.RenderChildren(output);
            output.WriteLine();

            base.RenderEndTag(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveCheckCode End -->");
        }
    }
}
