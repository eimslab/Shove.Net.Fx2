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
    [DefaultProperty("MultiRowHeader")]
    [ToolboxData("<{0}:ShoveGridView runat=server></{0}:ShoveGridView>")]
    public class ShoveGridView : GridView
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(true)]
        [Localizable(true)]
        public bool MultiRowHeader
        {
            get
            {
                object obj = this.ViewState["MultiRowHeader"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                else
                {
                    return true;
                }
            }

            set
            {
                this.ViewState["MultiRowHeader"] = value;
            }
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if ((!this.AutoGenerateColumns) && MultiRowHeader)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.SetRenderMethodDelegate(new RenderMethod(DrawGridHeader));
                }
            }

            base.OnRowCreated(e);
        }

        private void DrawGridHeader(HtmlTextWriter output, Control ctl)
        {
            output.Write("<td rowspan=\"3\">");
            output.Write("&nbsp;Lottery_id</td>");
            output.Write("<td colspan=\"3\">");
            output.Write("ÊôÐÔ</td>");
            output.Write("</tr>");
            output.Write("<tr>");
            output.Write("<td rowspan=\"2\">");
            output.Write("Name&nbsp;</td>");
            output.Write("<td colspan=\"2\">");
            output.Write("½ð¶î</td>");
            output.Write("</tr>");
            output.Write("<tr>");
            output.Write("<td>");
            output.Write("Sort&nbsp;</td>");
            output.Write("<td>");
            output.Write("Default&nbsp;</td>");
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.WriteLine("\n<!-- Shove.Web.UI.ShoveGridView Start -->");

            base.Render(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveGridView End -->");
        }
    }
}
