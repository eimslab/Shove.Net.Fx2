using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
	/// <summary>
	/// ShoveConfirmButton ��ժҪ˵����
	/// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:ShoveConfirmButton runat=server></{0}:ShoveConfirmButton>")]
	public class ShoveConfirmButton : System.Web.UI.WebControls.Button
	{
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("����֮����ʾ�Ƿ�������е���ʾ�ı�")] 
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
        [Bindable(true), Category("Appearance"), DefaultValue("�ύ��..."), Description("����֮����ʾ�����ύ�е���ʾ�ı�")]
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
                    return "�ύ��...";
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
        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("��ť�ı���ͼƬ")]
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
		/// ���˿ؼ����ָ�ָ�������������
		/// </summary>
		/// <param name="output"> Ҫд������ HTML ��д�� </param>
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
