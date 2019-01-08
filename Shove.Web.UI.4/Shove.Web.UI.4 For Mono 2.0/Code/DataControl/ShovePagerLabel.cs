using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
[assembly: WebResource("Shove.Web.UI.Script.ShovePagerLabel.js", "application/javascript")]
namespace Shove.Web.UI
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ShovePagerLabel runat=server></{0}:ShovePagerLabel>")]
    public class ShovePagerLabel : Panel
    {
        protected override void OnLoad(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShovePagerLabel", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Shove.Web.UI.Script.ShovePagerLabel.js"));

            base.OnLoad(e);
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(500), Description("每页的字符长度")]
        public int PageCharLength
        {
            get
            {
                object obj = this.ViewState["PageCharLength"];
                if (obj != null)
                {
                    return (int)obj;
                }
                else
                {
                    return 500;
                }
            }

            set
            {
                this.ViewState["PageCharLength"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("{$Page$}"), Description("页间的分隔符，如果设置了此属性，PageCharLength 属性将无效")]
        public string PageSplitChar
        {
            get
            {
                object obj = this.ViewState["PageSplitChar"];
                if (obj != null)
                {
                    return (string)obj;
                }
                else
                {
                    return "{$Page$}";
                }
            }

            set
            {
                this.ViewState["PageSplitChar"] = value;
            }
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

        private bool IsDesignMode
        {
            get
            {
                return (Site != null) ? Site.DesignMode : false;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.WriteLine("\n<!-- Shove.Web.UI.ShovePagerLabel Start -->");

            string[] SubText = SplitText();

            if (SubText == null)
            {
                base.Render(output);
            }
            else
            {
                base.RenderBeginTag(output);
                output.WriteLine();

                Unit PageHeight = new Unit(Height.Value - 80);
                string DivHtml = "";

                for (int i = 0; i < SubText.Length; i++)
                {
                    DivHtml += "<div id=\"" + this.UniqueID.Replace(':', '_') + "_Page" + i.ToString() + "\" style=\"width: 100%; height: " + PageHeight.ToString() + "; display: " + ((i == 0) ? "inherit" : "none") + "; overflow: hidden;\">\r\n";
                    DivHtml += SubText[i] + "\r\n";
                    DivHtml += "</div>\r\n";
                }

                DivHtml += "<br /><br /><div style=\"width: 100%; height: 40px; overflow: hidden; text-align:center;\">\r\n";

                if (SubText.Length > 1)
                {
                    for (int i = 0; i < SubText.Length; i++)
                    {
                        DivHtml += "<a href=\"javascript:ShoveWebUI_ShovePagerLabel_OnPageChanged('" + this.UniqueID.Replace(':', '_') + "_Page" + i.ToString() + "', '" + this.UniqueID.Replace(':', '_') + "_Page" + "')\">第" + (i + 1).ToString() + "页</a>\r\n";
                    }
                }
                else
                {
                    DivHtml += "&nbsp;";
                }

                DivHtml += "</div>";

                output.Write(DivHtml);

                base.RenderEndTag(output);
            }
            
            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShovePagerLabel End -->");
        }

        private string[] SplitText()
        {
            if (Text == "")
            {
                return null;
            }

            if (PageSplitChar != "")
            {
                return SplitText_2();
            }

            if (PageCharLength <= 0)
            {
                return null;
            }

            ArrayList al = new ArrayList();
            string str = Text;

            while (str != "")
            {
                if (str.Length <= PageCharLength)
                {
                    al.Add(str);

                    break;
                }

                string t = str.Substring(0, PageCharLength);

                al.Add(t);

                str = str.Substring(t.Length, str.Length - t.Length);
            }

            string[] Result = new string[al.Count];

            for (int i = 0; i < al.Count; i++)
            {
                Result[i] = al[i].ToString();
            }

            return Result;
        }

        private string[] SplitText_2()
        {
            if (PageSplitChar == "")
            {
                return SplitText();
            }

            return Text.Split(new string[] { PageSplitChar }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
