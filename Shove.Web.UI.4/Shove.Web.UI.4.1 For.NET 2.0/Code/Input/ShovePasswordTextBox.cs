using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
//[assembly: WebResource("Shove.Web.UI.Script.ShovePasswordTextBox.js", "application/javascript")]
namespace Shove.Web.UI
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ShovePasswordTextBox runat=server></{0}:ShovePasswordTextBox>")]
    public class ShovePasswordTextBox : TextBox
    {
        protected override void OnLoad(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShovePasswordTextBox", "ShoveWebUI_client/Script/ShovePasswordTextBox.js");

            this.Attributes.Add("value", this.Text);
            this.Attributes.Add("onclick", "ShoveWebUI_ShovePasswordTextBox_OnClick(this);");
            this.Attributes.Add("onblur", "ShoveWebUI_ShovePasswordTextBox_OnBlur(this);");

            base.OnLoad(e);
        }

        [Bindable(true), Category("Appearance"), DefaultValue(typeof(TextBoxMode), "Password"), Localizable(true)]
        public override TextBoxMode TextMode
        {
            get
            {
                object s = ViewState["TextMode"];
                return ((s == null) ? TextBoxMode.Password : (TextBoxMode)s);
            }

            set
            {
                ViewState["TextMode"] = value;
                base.TextMode = value;
            }
        }

        /// <summary>
        /// 支持文件路径
        /// </summary>
        [Bindable(true), Category("行为"), DefaultValue("ShoveWebUI_client"), Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("本系列控件的支持目录，以连接到相关的图片、脚本文件")]
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

        private bool IsDesignMode
        {
            get
            {
                return (Site != null) ? Site.DesignMode : false;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.WriteLine("\n<!-- Shove.Web.UI.ShovePasswordTextBox Start -->");

            base.Render(output);
            output.WriteLine();

            if (!IsDesignMode)
            {
                string KeyBoardHtmlText = "";
                try
                {
                    KeyBoardHtmlText = System.IO.File.ReadAllText(this.Page.Server.MapPath(SupportDir + "/Page/ShovePasswordTextBox.htm"));
                }
                catch { }

                KeyBoardHtmlText = KeyBoardHtmlText.Replace("[DIV_ID]", this.UniqueID.Replace(':', '_').Replace("$", "_") + "_DivKeyBoard").Replace("[KEYBOARD_IMAGEURL]", SupportDir + "/Images/KeyBoard.gif").Replace("[TEXTBOX_ID]", this.UniqueID.Replace(':', '_').Replace("$", "_"));
                output.Write(KeyBoardHtmlText);
            }

            output.WriteLine("<!-- Shove.Web.UI.ShovePasswordTextBox End -->");
        }
    }
}
