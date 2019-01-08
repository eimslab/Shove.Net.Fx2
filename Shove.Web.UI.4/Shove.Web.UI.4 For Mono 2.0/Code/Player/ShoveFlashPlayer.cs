using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
[assembly: WebResource("Shove.Web.UI.Script.ShoveFlashPlayer.js", "application/javascript")]
namespace Shove.Web.UI
{
    [DefaultProperty("Src"), ToolboxData("<{0}:ShoveFlashPlayer runat=server></{0}:ShoveFlashPlayer>")]
    public class ShoveFlashPlayer: WebControl
    {
        protected override void OnLoad(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveFlashPlayer", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Shove.Web.UI.Script.ShoveFlashPlayer.js"));

            base.OnLoad(e);
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(FlashUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("要播放的 Flash 文件")]
        public string Src
        {
            get
            {
                object obj = this.ViewState["Src"];

                if (obj != null)
                {
                    return (string)obj;
                }

                return "";
            }

            set
            {
                this.ViewState["Src"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public string Alt
        {
            get
            {
                String s = (String)ViewState["Alt"];
                return ((s == null)? String.Empty : s);
            }
 
            set
            {
                ViewState["Alt"] = value;
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
            output.WriteLine("\n<!-- Shove.Web.UI.ShoveFlashPlayer Start -->");

            if (Src == "")
            {
                base.RenderBeginTag(output);
                output.WriteLine();

                if ((BorderStyle == BorderStyle.None) || (BorderStyle == BorderStyle.NotSet))
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%; height: 100%; border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid");
                }
                else
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%; height: 100%;");
                }

                output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                output.RenderBeginTag(HtmlTextWriterTag.Table);
                output.RenderBeginTag(HtmlTextWriterTag.Tr);
                output.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                output.RenderBeginTag(HtmlTextWriterTag.Td);

                output.WriteLine((Alt == "") ? "请指定一个 Flash 文件。" : Alt);

                output.RenderEndTag();
                output.RenderEndTag();
                output.RenderEndTag();

                output.WriteLine();
                base.RenderEndTag(output);
            }
            else
            {
                if (IsDesignMode)
                {
                    base.RenderBeginTag(output);
                    output.WriteLine();

                    output.Write("<embed pluginspage=\"http://www.macromedia.com/go/getflashplayer\" src=\"" + Src + "\" width=\"100%\" height=\"100%\" type=\"application/x-shockwave-flash\" quality=\"high\"></embed>");

                    output.WriteLine(); 
                    base.RenderEndTag(output);
                }
                else
                {
                    base.RenderBeginTag(output);
                    output.WriteLine();

                    output.AddAttribute("type", "text/javascript");
                    output.RenderBeginTag(HtmlTextWriterTag.Script);

                    output.Write("ShoveWebUI_ShoveFlashPlayer_Play(\"" + Src + "\", \"100%\", \"100%\");");

                    output.RenderEndTag();

                    output.WriteLine();
                    base.RenderEndTag(output);
                }
            }

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveFlashPlayer End -->");
        }
    }
}
