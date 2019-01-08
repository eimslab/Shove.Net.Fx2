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
        [Bindable(true), Category("Appearance"), DefaultValue("about:blank"), Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("��ť�ı���ͼƬ")]
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

            OnClientClick = clickScript + OnClientClick;

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveImageButton Start -->");

            base.Render(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveImageButton End -->");
        }
    }
}
