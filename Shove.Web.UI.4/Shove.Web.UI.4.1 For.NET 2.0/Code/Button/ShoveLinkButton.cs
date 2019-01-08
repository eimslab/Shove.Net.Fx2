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
        /// ����֮����ʾ�Ƿ�������е���ʾ�ı�
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
        /// ���˿ؼ����ָ�ָ�������������
        /// </summary>
        /// <param name="output"> Ҫд������ HTML ��д�� </param>
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
