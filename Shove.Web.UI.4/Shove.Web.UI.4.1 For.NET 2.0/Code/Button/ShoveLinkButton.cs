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
    /// <summary>
    /// ShoveLinkButton
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ShoveLinkButton runat=server></{0}:ShoveLinkButton>")]
    public class ShoveLinkButton : LinkButton
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
        [Bindable(true), Category("Appearance"), DefaultValue("提交中..."), Description("单击之后提示正在提交中的提示文本")]
        public string SubmitingText
        {
            get
            {
                object obj = this.ViewState["SubmitingText"];
                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "提交中...";
                }
            }
            set
            {
                this.ViewState["SubmitingText"] = value;
            }
        }

        /// <summary> 
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            string clickScript = "if (this.disabled==true) return false;";

            if (AlertText.Trim() != "")
            {
                clickScript += "if (!confirm('" + System.Web.HttpUtility.HtmlEncode(AlertText) + "')) return false;";
            }

            clickScript += "this.disabled=true;";

            if (SubmitingText.Trim() != "")
            {
                clickScript += "this.innerText='" + System.Web.HttpUtility.HtmlEncode(SubmitingText) + "';";
            }

            OnClientClick = clickScript + OnClientClick;

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveLinkButton Start -->");

            base.Render(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveLinkButton End -->");
        }
    }
}
