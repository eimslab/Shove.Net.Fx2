using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Shove.Web.UI
{
    /// <summary>
    /// 填写 ShovePart2 许可证
    /// </summary>
    public partial class ShovePart2License: System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DropDownList ddlLevel;
        protected System.Web.UI.WebControls.TextBox tbMac;
        protected System.Web.UI.WebControls.TextBox tbLicens;
        protected System.Web.UI.WebControls.Button btnOK;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string NetCardMac = new SystemInformation().GetNetCardMACAddress();

                if (String.IsNullOrEmpty(NetCardMac))
                {
                    NetCardMac = "Default";
                }

                tbMac.Text = NetCardMac;
            }
        }

        /// <summary>
        /// 保存许可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOK_Click(object sender, EventArgs e)
        {
            string FileName = this.Page.Server.MapPath("../Data/ShovePart2.UserControls.ini");

            IniFile ini = new IniFile(FileName);
            ini.Write("Licenses", tbMac.Text, tbLicens.Text);

            string LastRequestPage = this.Request["LastRequestPage"];

            if (String.IsNullOrEmpty(LastRequestPage))
            {
                JavaScript.Alert(this.Page, "授权许可证已经保存。");
            }
            else
            {
                JavaScript.Alert(this.Page, "授权许可证已经保存。", HttpUtility.UrlDecode(LastRequestPage));
            }
        }
    }
}