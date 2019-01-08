using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    /// <summary>
    /// TabPage class represents the individual tab page.   ToolboxItem(false), ParseChildren(false), 
    /// </summary>  
    [ToolboxData(@"<{0}:ShoveTabPage runat=server></{0}:ShoveTabPage>"), ToolboxItem(false), ParseChildren(false)]
    public class ShoveTabPage : Panel, INamingContainer
    {
        /// <summary>
        /// The text of tab page.
        /// </summary>
        public string Text
        {
            get
            {
                object val = this.ViewState["Text"];
                if (val == null) return "";
                return val.ToString();
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }


    }
}
