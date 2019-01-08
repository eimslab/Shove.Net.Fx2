using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
	/// <summary>
	/// ShoveDateLable 的摘要说明。
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:ShoveDateLable runat=server></{0}:ShoveDateLable>")]
	public class ShoveDateLable : System.Web.UI.WebControls.Label
	{
		/// <summary> 
		/// 将此控件呈现给指定的输出参数。
		/// </summary>
		/// <param name="output"> 要写出到的 HTML 编写器 </param>
		protected override void Render(HtmlTextWriter output)
		{
			string DayOfWeek = "";
			switch (DateTime.Now.DayOfWeek.ToString())
			{
				case "Sunday":
					DayOfWeek = "星期日";
					break;
				case "Monday":
					DayOfWeek = "星期一";
					break;
				case "Tuesday":
					DayOfWeek = "星期二";
					break;
				case "Wednesday":
					DayOfWeek = "星期三";
					break;
				case "Thursday":
					DayOfWeek = "星期四";
					break;
				case "Friday":
					DayOfWeek = "星期五";
					break;
				case "Saturday":
					DayOfWeek = "星期六";
					break;
			}
			this.Text = DateTime.Now.ToLongDateString() + " " + DayOfWeek;
			base.Render(output);
			//output.Write();
		}
	}
}
