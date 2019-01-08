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
	/// ShoveCheckCode 的摘要说明。
	/// </summary>
	/// 

	[DefaultProperty("Charset"), ToolboxData("<{0}:ShoveCheckCode runat=server></{0}:ShoveCheckCode>")]
	public class ShoveCheckCode : System.Web.UI.WebControls.Image
	{
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
		}

		[Bindable(true),
		Category("Appearance"),
		DefaultValue("")]
		public string CheckCode
		{
			get
			{
				object obj = this.ViewState["CheckCode"];
				if (obj != null)
				{
					return (string) obj;
				}
				else
				{
					return GetRandNum();
				}
			}

			//set
			//{
			//	this.ViewState["CheckCode"] = value.Trim().PadRight(4, ' ');
			//}
		}

        [Bindable(true), Category("Appearance"), DefaultValue("ShoveWebUI_client"), Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("本系列控件的支持目录，以连接到相关的图片、脚本文件")]
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

        //[Bindable(true),
        //Category("Appearance"),
        //DefaultValue("ShoveWebUI_client/CheckCode/BuildImage.aspx"),
        //Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor)),
        //Description("验证码的图片 Url，请指向根目录下 ShoveWebUI_client/CheckCode/BuildImage.aspx")]
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

        [Bindable(true),
		Category("Appearance"),
		DefaultValue(CharSet.All),
		Description("验证码字符集")]
		public CharSet Charset
		{
			get
			{
				object obj = this.ViewState["Charset"];
				if (obj != null)
				{
					return (CharSet) obj;
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

		[Bindable(true),
		Category("Appearance"),
        DefaultValue("ShoveWebUI_CheckCode_CheckCode"),
		Description("验证码字符串存放在 Session 中的 Key。取值示例：Session[\"ShoveWebUI_CheckCode_CheckCode\"]")]
		public string SessionKeyName
		{
			get
			{
                object obj = this.ViewState["SessionKeyName"];
                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "ShoveWebUI_CheckCode_CheckCode";
                }
			}

            set
            {
                this.ViewState["SessionKeyName"] = value;
            }
		}

		[Bindable(true),
		Category("Appearance"),
        DefaultValue(true),
		Description("是否需要画背景干扰线？")]
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

        /// <summary>
        /// 获取随机数种子，为了避免在同一页面放多个验证码控件的时候，默认的以时间为随机数发生器的种子时，造成多个控件的验证码一样的情况。
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
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
		protected override void Render(HtmlTextWriter output)
		{
            if (IsDesignMode)
            {
                this.ImageUrl = "about:blank";
            }
            else
            {
                Encrypt encrypt = new Encrypt();

                string t = SupportDir + "/Page/BuildImage.aspx?Key=" + encrypt.EncryptString(SessionKeyName) + "&CheckCode=" + encrypt.EncryptString(CheckCode) + "&ForeColor=" + encrypt.EncryptString(ForeColor.Name) + "&BackColor=" + encrypt.EncryptString(BackColor.Name) + "&Width=" + encrypt.EncryptString(Width.ToString()) + "&Height=" + encrypt.EncryptString(Height.ToString()) + "&FontName=" + encrypt.EncryptString(Font.Name) + "&FontSize=" + encrypt.EncryptString(Font.Size.ToString()) + "&isInterferenceBackgroup=" + encrypt.EncryptString(isInterferenceBackgroup.ToString());

                if (!t.StartsWith("../") && !t.StartsWith("~/"))
                {
                    t = Page.ResolveUrl("~/" + t);
                }

                this.ImageUrl = t;
            }

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveCheckCode Start -->");

            base.Render(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveCheckCode End -->");
		}
	}
}
