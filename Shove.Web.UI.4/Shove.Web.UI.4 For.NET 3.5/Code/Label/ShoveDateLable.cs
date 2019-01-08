using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
	/// <summary>
	/// ShoveDateLable ��ժҪ˵����
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:ShoveDateLable runat=server></{0}:ShoveDateLable>")]
	public class ShoveDateLable : System.Web.UI.WebControls.Label
	{
		/// <summary> 
		/// ���˿ؼ����ָ�ָ�������������
		/// </summary>
		/// <param name="output"> Ҫд������ HTML ��д�� </param>
		protected override void Render(HtmlTextWriter output)
		{
			string DayOfWeek = "";
			switch (DateTime.Now.DayOfWeek.ToString())
			{
				case "Sunday":
					DayOfWeek = "������";
					break;
				case "Monday":
					DayOfWeek = "����һ";
					break;
				case "Tuesday":
					DayOfWeek = "���ڶ�";
					break;
				case "Wednesday":
					DayOfWeek = "������";
					break;
				case "Thursday":
					DayOfWeek = "������";
					break;
				case "Friday":
					DayOfWeek = "������";
					break;
				case "Saturday":
					DayOfWeek = "������";
					break;
			}
			this.Text = DateTime.Now.ToLongDateString() + " " + DayOfWeek;
			base.Render(output);
			//output.Write();
		}
	}
}
