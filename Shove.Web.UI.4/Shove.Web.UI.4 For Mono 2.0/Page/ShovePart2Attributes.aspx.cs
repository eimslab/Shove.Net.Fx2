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
    public partial class ShovePart2Attributes : System.Web.UI.Page
    {
        #region 组件

        protected System.Web.UI.WebControls.DropDownList ddlAscxFileName;
        protected System.Web.UI.WebControls.TextBox tbCssClass;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnChooseUC;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hiControlAttributes;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hiascxFileName;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hiSiteDir;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hiUserID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hiSiteID;

        #endregion

        #region 属性变量

        public string Attrib_ImageUploadDir = "";
        public string Attrib_AscxControlFileName = "";
        public string Attrib_CssClass = "";
        public string Attrib_ControlAttributes = "";

        public string Attrib_SiteID = "";
        public string Attrib_UserID = "";

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShovePart2.RunMode"]!=null && this.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (!IsPostBack)
            {
                #region 获取 Request 变量

                Attrib_SiteID = System.Web.HttpUtility.UrlDecode(this.Request["SiteID"]);
                Attrib_UserID = System.Web.HttpUtility.UrlDecode(this.Request["UserID"]);

                Attrib_AscxControlFileName = System.Web.HttpUtility.UrlDecode(this.Request["AscxControlFileName"]);
                Attrib_CssClass = System.Web.HttpUtility.UrlDecode(this.Request["CssClass"]);

                Attrib_ControlAttributes = this.Request["ControlAttributes"];// System.Web.HttpUtility.UrlDecode(this.Request["ControlAttributes"]);

                #endregion

                #region 将变量存入 ViewState

                ViewState["Attrib_SiteID"] = Attrib_SiteID;
                ViewState["Attrib_UserID"] = Attrib_UserID;
                ViewState["Attrib_AscxControlFileName"] = Attrib_AscxControlFileName;
                ViewState["Attrib_CssClass"] = Attrib_CssClass;
                ViewState["Attrib_ControlAttributes"] = Attrib_ControlAttributes;

                #endregion

                hiSiteID.Value = Attrib_SiteID;
                hiUserID.Value = Attrib_UserID;
                hiSiteDir.Value = Attrib_ImageUploadDir;
                hiControlAttributes.Value = Attrib_ControlAttributes;
                hiascxFileName.Value = Attrib_AscxControlFileName;
            }
            else
            {
                #region 从 ViewState 获取变量

                Attrib_SiteID = ViewState["Attrib_SiteID"] + "";
                Attrib_UserID = ViewState["Attrib_UserID"] + "";
                Attrib_AscxControlFileName = ViewState["Attrib_AscxControlFileName"] + "";
                Attrib_CssClass = ViewState["Attrib_CssClass"] + "";
                Attrib_ControlAttributes = ViewState["Attrib_ControlAttributes"] + "";

                #endregion
            }

            if (!this.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            string RootPath = this.Page.Server.MapPath("~/");

            // 填充样式
            tbCssClass.Text = Attrib_CssClass;

            // 填充窗口内容
            ddlAscxFileName.Items.Clear();
            ddlAscxFileName.Items.Add("不需要填充内容");

            #region 获取控件列表，填充窗口内容

            IniFile ini = new IniFile(this.Server.MapPath("../Data/ShovePart2.UserControls.ini"));

            int UserControlCount = 0;

            try
            {
                UserControlCount = int.Parse(ini.Read("UserControls", "Count"));
            }
            catch { }

            if (UserControlCount > 0)
            {
                for (int i = 1; i <= UserControlCount; i++)
                {
                    ddlAscxFileName.Items.Add(new ListItem(ini.Read("Control" + i.ToString(), "Name"), ini.Read("Control" + i.ToString(), "FileName")));
                }
            }
            else
            {
                string SystemPreFix = PublicFunction.GetWebConfigAppSettingAsString("SystemPreFix");
                string DirPath = PublicFunction.GetWebConfigAppSettingAsString("ShovePart2UserControlDirectory");
                string[] AscxFiles = null;

                DataSet ds = null;

                if (DirPath == "ShovePart2UserControlsAndTypeDataSet")
                {
                    try
                    {
                        ds = (DataSet)HttpContext.Current.Application[SystemPreFix + "ShovePart2UserControlsAndTypeDataSet" + Attrib_UserID];
                    }
                    catch { }
                }

                if ((DirPath == "ShovePart2UserControlsAndTypeDataSet") && (ds != null) && (ds.Tables.Count == 4))
                {
                    btnChooseUC.Disabled = false;

                    for (int i = 1; i < ds.Tables.Count; i++)
                    {
                        DataTable dt = ds.Tables[i];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow r in dt.Rows)
                            {
                                ddlAscxFileName.Items.Add(new ListItem(r["Name"] + "", r["FileUrl"] + ""));
                            }
                        }
                    }
                }
                else if ((DirPath != "ShovePart2UserControlsAndTypeDataSet") && (DirPath != ""))
                {
                    string[] Paths = DirPath.Split(',');

                    for (int i = 0; i < Paths.Length; i++)
                    {
                        AscxFiles = Shove.Web.UI.File.GetFileListWithSubDir(this.Page.Server.MapPath(Paths[i]), ".ascx");

                        AddUCDropDownList(AscxFiles, RootPath);
                    }
                }
                else
                {
                    AscxFiles = Shove.Web.UI.File.GetFileListWithSubDir(this.Page.Server.MapPath("~/"), ".ascx");

                    AddUCDropDownList(AscxFiles, RootPath);
                }
            }

            #endregion

            ControlExt.SetDownListBoxTextFromValue(ddlAscxFileName, Attrib_AscxControlFileName);
        }

        /// <summary>
        /// 查找指定目录下所有用户控件，填充下拉框
        /// </summary>
        /// <param name="AscxFiles"></param>
        /// <param name="RootPath"></param>
        private void AddUCDropDownList(string[] AscxFiles, string RootPath)
        {
            if ((AscxFiles != null) && (AscxFiles.Length > 0))
            {
                foreach (string AscxFile in AscxFiles)
                {
                    Control ctrl = null;

                    try
                    {
                        ctrl = Page.LoadControl(@"../../" + AscxFile.Replace(RootPath, "").Replace("\\", "/"));
                    }
                    catch { }

                    if (ctrl == null)
                    {
                        continue;
                    }

                    if (ctrl is ShovePart2UserControl)
                    {
                        ShovePart2UserControl swpuc = (ShovePart2UserControl)ctrl;

                        ddlAscxFileName.Items.Add(new ListItem(String.IsNullOrEmpty(swpuc.Name) ? "~/" + AscxFile.Replace(RootPath, "").Replace("\\", "/") : swpuc.Name, "~/" + AscxFile.Replace(RootPath, "").Replace("\\", "/")));
                    }
                    else
                    {
                        ddlAscxFileName.Items.Add(new ListItem("~/" + AscxFile.Replace(RootPath, "").Replace("\\", "/"), "~/" + AscxFile.Replace(RootPath, "").Replace("\\", "/")));
                    }
                }
            }
        }
    }
}