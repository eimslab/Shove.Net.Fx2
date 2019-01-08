using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ShoveImageButton runat=server></{0}:ShoveImageButton>")]
    public class ShoveImageButton : ImageButton
    {
        /// <summary>
        /// 单击之后提示是否继续进行的提示文本
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("单击之后提示是否继续进行的提示文本")]
        public string AlertText
        {
            get
            {
                object obj = this.ViewState["AlertText"];
                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                this.ViewState["AlertText"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("about:blank"), Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("按钮的背景图片")]
        public override string ImageUrl
        {
            get
            {
                return String.IsNullOrEmpty(base.ImageUrl) ? "about:blank" : base.ImageUrl;
            }
            set
            {
                base.ImageUrl = value;
            }
        }

        /// <summary> 
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            string clickScript = "if (this.submiting==true) return false;";

            if (AlertText.Trim() != "")
            {
                clickScript += "if (!confirm('" + System.Web.HttpUtility.HtmlEncode(AlertText) + "')) return false;";
            }

            clickScript += "this.submiting=true;";

            OnClientClick = clickScript + OnClientClick;

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveImageButton Start -->");

            base.Render(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveImageButton End -->");
        }
    }
}
