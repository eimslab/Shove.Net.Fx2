using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
	/// <summary>
	/// ShoveSetHomePage ��ժҪ˵����
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:ShoveSetHomePage runat=server></{0}:ShoveSetHomePage>")]
	public class ShoveSetHomePage : System.Web.UI.WebControls.WebControl
	{
		private string text;
		private string homepageurl = "http://...";
		private string tiptext = "��Ϊ��ҳ";

		[Bindable(true), 
			Category("Appearance"), 
			DefaultValue("")] 
		public string Text 
		{
			get
			{
				return text;
			}

			set
			{
				text = value;
			}
		}

		[Bindable(true), 
			Category("Appearance"), 
			DefaultValue("")] 
		public string HomePageUrl 
		{
			get
			{
				return homepageurl;
			}

			set
			{
				homepageurl = value;
			}
		}

		[Bindable(true),
			Category("Appearance"), 
			DefaultValue("")] 
		public string TipText 
		{
			get
			{
				return tiptext;
			}

			set
			{
				tiptext = value;
			}
		}

		/// <summary> 
		/// ���˿ؼ����ָ�ָ�������������
		/// </summary>
		/// <param name="output"> Ҫд������ HTML ��д�� </param>
		protected override void Render(HtmlTextWriter output)
		{
			Text = "<A " + ((CssClass == "") ? "" : "class=\"" + CssClass + "\"") + " onclick=\"this.style.behavior=\'url(#default#homepage)\';this.setHomePage(\'" + HomePageUrl + "\');\" href=\"#\">" + TipText + "</A>";
			output.Write(Text);
		}
	}
}
