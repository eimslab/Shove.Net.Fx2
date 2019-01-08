using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
	/// <summary>
	/// WebCustomControl1 的摘要说明。
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:ShoveCounter runat=server></{0}:ShoveCounter>")]
	public class ShoveCounter : System.Web.UI.WebControls.WebControl
	{
		private string text;
		private int number;
		private string imagesPath = "ShoveWebUI_client/Images/Counter/";

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
			DefaultValue(0)]
		public int Number
		{
			get
			{
				return number;
			}

			set
			{
				number = System.Math.Abs(value) % 100000000;
			}
		}

		[Bindable(true),
		Category("Appearance"),
		DefaultValue("ShoveWebUI_client/Images/Counter/")]
		public string ImagesPath
		{
			get
			{
				return imagesPath;
			}

			set
			{
				imagesPath = value;
			}
		}

		/// <summary>
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
		protected override void Render(HtmlTextWriter output)
		{
			string NumberStr = Number.ToString().PadLeft(8, '0');

			Text = "<img src=\"" + ImagesPath + NumberStr.Substring(0, 1) + ".gif\">";
			Text += "<img src=\"" + ImagesPath + NumberStr.Substring(1, 1) + ".gif\">";
			Text += "<img src=\"" + ImagesPath + NumberStr.Substring(2, 1) + ".gif\">";
			Text += "<img src=\"" + ImagesPath + NumberStr.Substring(3, 1) + ".gif\">";
			Text += "<img src=\"" + ImagesPath + NumberStr.Substring(4, 1) + ".gif\">";
			Text += "<img src=\"" + ImagesPath + NumberStr.Substring(5, 1) + ".gif\">";
			Text += "<img src=\"" + ImagesPath + NumberStr.Substring(6, 1) + ".gif\">";
			Text += "<img src=\"" + ImagesPath + NumberStr.Substring(7, 1) + ".gif\">";

			output.Write(Text);
		}
	}
}
