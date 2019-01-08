using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
	/// <summary>
	/// ShoveCheckCode ��ժҪ˵����
	/// </summary>
	/// 

    [DefaultProperty("TableName"), ToolboxData("<{0}:ShoveDataImage runat=server></{0}:ShoveDataImage>")]
	public class ShoveDataImage : System.Web.UI.WebControls.Image
	{
        public ShoveDataImage()
		{
		}

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("���ݱ���")]
        public string TableName
		{
			get
			{
                object obj = this.ViewState["TableName"];
				if (obj != null)
				{
					return (string) obj;
				}
				else
				{
					return "";
				}
			}

			set
			{
                this.ViewState["TableName"] = value;
			}
		}

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("�����ֶ���")]
        public string FieldName
        {
            get
            {
                object obj = this.ViewState["FieldName"];
                if (obj != null)
                {
                    return (string)obj;
                }
                else
                {
                    return "";
                }
            }

            set
            {
                this.ViewState["FieldName"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("��ѯ����")]
        public string Condition
        {
            get
            {
                object obj = this.ViewState["Condition"];
                if (obj != null)
                {
                    return (string)obj;
                }
                else
                {
                    return "";
                }
            }

            set
            {
                this.ViewState["Condition"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("ShoveWebUI_client"), Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("��ϵ�пؼ���֧��Ŀ¼�������ӵ���ص�ͼƬ���ű��ļ�")]
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

		/// <summary> 
		/// ���˿ؼ����ָ�ָ�������������
		/// </summary>
		/// <param name="output"> Ҫд������ HTML ��д�� </param>
		protected override void Render(HtmlTextWriter output)
		{
            this.ImageUrl = SupportDir + "/Page/BuildDataImage.aspx?TableName=" + System.Web.HttpUtility.UrlEncode(TableName) + "&FieldName=" + System.Web.HttpUtility.UrlEncode(FieldName) + "&Condition=" + System.Web.HttpUtility.UrlEncode(Condition);

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveDataImage Start -->");

            base.Render(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveDataImage End -->");
		}
	}
}
