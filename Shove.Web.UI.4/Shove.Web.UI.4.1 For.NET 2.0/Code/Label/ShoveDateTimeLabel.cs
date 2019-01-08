using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
//[assembly: WebResource("Shove.Web.UI.Script.ShoveDateTimeLabel.js", "application/javascript")]
namespace Shove.Web.UI
{
    public enum ServerCliect
    {
        Server = 0,
        Client = 1
    }

    [DefaultProperty("DateTimeFrom"), ToolboxData("<{0}:ShoveDateTimeLabel runat=server></{0}:ShoveDateTimeLabel>")]
    public class ShoveDateTimeLabel : System.Web.UI.WebControls.Label
    {
        protected override void OnLoad(EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ShoveDateTimeLabel));

            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveDateTimeLabel", "ShoveWebUI_client/Script/ShoveDateTimeLabel.js");

            base.OnLoad(e);
        }

        [Bindable(true), Category("行为"), DefaultValue(ServerCliect.Server), Localizable(true)]
        public ServerCliect DateTimeFrom
        {
            get
            {
                object s = this.ViewState["DateTimeFrom"];
                return ((s == null) ? ServerCliect.Server : (ServerCliect)s);
            }
 
            set
            {
                ViewState["DateTimeFrom"] = value;
            }
        }

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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string GetServerTime()
        {
            System.DateTime dt = System.DateTime.Now.AddSeconds(1);

            return dt.ToLongDateString() + " " + dt.ToLongTimeString();
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
            if (DateTimeFrom == ServerCliect.Server)
            {
                this.Text = GetServerTime();
            }

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveDateTimeLabel Start -->");

            base.RenderBeginTag(output);
            base.RenderContents(output);
            output.WriteLine();
            output.Write("\t");

            if (!IsDesignMode)
            {
                output.AddAttribute("src", "about:blank");
                output.AddAttribute("alt", "");
                output.AddAttribute("onerror", "ShoveWebUI_ShoveDateTimeLabel_ShowTime(this.parentNode, '" + DateTimeFrom.ToString() + "');");
                output.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
                output.RenderBeginTag(HtmlTextWriterTag.Img);
                output.RenderEndTag();
            }

            output.WriteLine();
            base.RenderEndTag(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveDateTimeLabel End -->");
        }
    }
}
