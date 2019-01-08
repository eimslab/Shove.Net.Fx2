using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
	/// <summary>
	/// ShoveAddFavorite ��ժҪ˵����
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:ShoveAddFavorite runat=server></{0}:ShoveAddFavorite>")]
	public class ShoveAddFavorite : System.Web.UI.WebControls.WebControl
	{
		private string text;
		private string tiptext = "�����ղ�";
	
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
			Text = "<A " + ((CssClass == "") ? "" : "class=\"" + CssClass + "\"") + " onclick=\"javascript:window.external.AddFavorite(location.href,document.title);\" href=\"#\">" + TipText + "</A>";
			output.Write(Text);
		}
	}
}
