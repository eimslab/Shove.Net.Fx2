using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
	/// <summary>
	/// ShoveConfirmButton 的摘要说明。
	/// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:ShoveConfirmButton runat=server></{0}:ShoveConfirmButton>")]
	public class ShoveConfirmButton : System.Web.UI.WebControls.Button
	{
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("按钮的背景图片")]
		public string BackgroupImage
		{
            get
            {
                object obj = this.ViewState["BackgroupImage"];
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
                this.ViewState["BackgroupImage"] = value;

                if (value.Trim() != "")
                {
                    this.BackColor = System.Drawing.Color.Transparent;
                }
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

            if (SubmitingText.Trim() != "")
            {
                clickScript += "this.value='" + System.Web.HttpUtility.HtmlEncode(SubmitingText) + "';";
            }

            OnClientClick = clickScript + OnClientClick;
            
            if (BackgroupImage.Trim() != "")
            {
                this.Style.Add("background-image", BackgroupImage);

                this.Style.Add("border-left-style", "none");
                this.Style.Add("border-top-style", "none");
                this.Style.Add("border-right-style", "none");
                this.Style.Add("border-bottom-style", "none");
            }

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveConfirmButton Start -->");
		
            base.Render(output);
            
            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveConfirmButton End -->");
        }
	}
}
