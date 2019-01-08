using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;


[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
[assembly: WebResource("Shove.Web.UI.Script.ShovePart2DesignMenu.js", "application/javascript")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShovePart2基页
    /// </summary>
    public class ShovePart2BasePage : System.Web.UI.Page
    {
        #region 控件

        /// <summary>
        /// ShovePart2容器层
        /// </summary>
        public System.Web.UI.WebControls.Panel ShovePart2_bodyMain;

        /// <summary>
        /// 编辑菜单 Panel
        /// </summary>
        public System.Web.UI.WebControls.Panel ShovePart2_panel;

        /// <summary>
        /// 打开关闭 Panel 的按钮
        /// </summary>
        public System.Web.UI.HtmlControls.HtmlImage ShovePart2_btnEdit;


        /// <summary>
        /// 设计、运行模式切换
        /// </summary>
        public System.Web.UI.WebControls.DropDownList ShovePart2_ddlMode;

        /// <summary>
        /// 设计菜单标题
        /// </summary>
        public System.Web.UI.WebControls.Label ShovePart2_labTitle;

        /// <summary>
        /// 设计菜单副标题
        /// </summary>
        public System.Web.UI.WebControls.Label ShovePart2_labSubTitle;

        /// <summary>
        /// 添加新页按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnAddNewPage;


        /// <summary>
        /// 导入页面布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnAddNewLayout;
        /// <summary>
        /// 添加ShovePart2按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnAddShovePart2;

        /// <summary>
        /// 备份本站布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnBackupSiteLayout;

        /// <summary>
        /// 恢复本站布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnRestoreSiteLayout;

        /// <summary>
        /// 删除本站布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnClearSiteLayout;

        /// <summary>
        /// 备份本页布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnBackupPageLayout;

        /// <summary>
        /// 恢复本页布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnRestorePageLayout;

        /// <summary>
        /// 删除本页布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnClearPageLayout;

        /// <summary>
        /// 上传/下载样式表
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnUpLoadStyle;

        /// <summary>
        /// 查看网页列表
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnPageList;

        /// <summary>
        /// 复制页面
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_btnCopyPage;

        /// <summary>
        /// 保存页面设计
        /// </summary>
        public System.Web.UI.WebControls.Button ShovePart2_SavePage;

        #endregion

        /// <summary>
        /// 站点ID默认为-1
        /// </summary>
        public long SiteID
        {
            get
            {
                object obj = this.ViewState["SiteID"];

                try
                {
                    return Convert.ToInt64(obj);
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                this.ViewState["SiteID"] = value;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID
        {
            get
            {
                object obj = this.ViewState["UserID"];

                try
                {
                    return Convert.ToInt64(obj);
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                this.ViewState["UserID"] = value;
            }
        }

        /// <summary>
        /// 是否允许设计，由继承页面根据用户是否登陆，是否有设计权限而复制
        /// </summary>
        public bool IsAllowDesign
        {
            get
            {
                object obj = this.ViewState["IsAllowDesign"];

                try
                {
                    return Convert.ToBoolean(obj);
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                this.ViewState["IsAllowDesign"] = value;
            }
        }

        /// <summary>
        /// 是否是设计状态
        /// </summary>
        public bool IsDesigning
        {
            get
            {
                if (!IsAllowDesign)
                {
                    return false;
                }

                return (this.Request.QueryString["Designing"] == "True");
            }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool Lock
        {
            get
            {
                object obj = this.ViewState["Lock"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                this.ViewState["Lock"] = value;
            }
        }
        /// <summary>
        /// 页名
        /// </summary>
        public string PageName
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Request["PN"]))
                {
                    return this.Request["PN"];
                }
                else
                {
                    return "Default";
                }
            }
        }
        /// <summary>
        /// 图片上传地址
        /// </summary>
        public string ImageUploadDir
        {
            get
            {
                object obj = this.ViewState["ImageUploadDir"];
                if (obj != null)
                {
                    return (string)obj;
                }
                else
                {
                    return "~/Images/Upload/MultiSite/" + SiteID.ToString();
                }
            }

            set
            {
                this.ViewState["ImageUploadDir"] = value;
            }
        }
        // 相对于根目录的相对路径
        private string SupportDir
        {
            get
            {
                return PublicFunction.GetCurrentRelativePath() + "ShoveWebUI_client";
            }
        }

        /// <summary>
        /// ShovePart2基页构造函数
        /// </summary>
        public ShovePart2BasePage()
        {
            ShovePart2_panel = new Panel();

            ShovePart2_btnEdit = new HtmlImage();

            ShovePart2_ddlMode = new DropDownList();

            ShovePart2_labTitle = new Label();
            ShovePart2_labSubTitle = new Label();

            ShovePart2_btnAddNewPage = new Button();

            ShovePart2_btnAddNewLayout = new Button();
            ShovePart2_btnAddShovePart2 = new Button();
            ShovePart2_btnBackupSiteLayout = new Button();
            ShovePart2_btnRestoreSiteLayout = new Button();
            ShovePart2_btnClearSiteLayout = new Button();
            ShovePart2_btnBackupPageLayout = new Button();
            ShovePart2_btnRestorePageLayout = new Button();
            ShovePart2_btnClearPageLayout = new Button();
            ShovePart2_btnUpLoadStyle = new Button();
            ShovePart2_btnPageList = new Button();
            ShovePart2_btnCopyPage = new Button();
            ShovePart2_SavePage = new Button();
            SiteID = -1;
            UserID = -1;
            IsAllowDesign = true;
        }

        /// <summary>
        /// 设置站点ID的虚拟方法
        /// </summary>
        protected virtual void SetSiteID()
        {
            // 如果需要多站，需要实现此方法
        }

        /// <summary>
        /// 设置用户的虚拟方法
        /// </summary>
        protected virtual void SetUserID()
        {
            // 如果需要多站，需要实现此方法
        }

        /// <summary>
        /// 设置页面是否允许设计
        /// </summary>
        protected virtual void SetAllowDesign()
        {
            // 根据用户是否具有设计权限设置
        }

        /// <summary>
        /// 重写页面加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            SetSiteID();
            SetUserID();
            SetAllowDesign();

            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShovePart2", this.Page.ClientScript.GetWebResourceUrl(typeof(ShovePart2), "Shove.Web.UI.Script.ShovePart2.js"));

            if (IsDesigning)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ShovePart2));
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ShovePart2BasePage));

                this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShovePart2Designing", this.Page.ClientScript.GetWebResourceUrl(typeof(ShovePart2), "Shove.Web.UI.Script.ShovePart2Designing.js"));

                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "str", "<script>window.onload=function(){CoolDrag.init('bodyMain','" + SiteID.ToString() + "','" + PageName + "');}</script>");
                
            }

            #region 引用样式表

            if (!System.IO.File.Exists(Server.MapPath("~/ShoveWebUI_client/Style/" + SiteID.ToString() + ".css")))
            {
                if (System.IO.File.Exists(Server.MapPath("~/ShoveWebUI_client/Style/0.css")))
                {
                    System.IO.File.Copy(Server.MapPath("~/ShoveWebUI_client/Style/0.css"), Server.MapPath("~/ShoveWebUI_client/Style/" + SiteID.ToString() + ".css"));
                }
                else
                {
                    System.IO.File.WriteAllText(Server.MapPath("~/ShoveWebUI_client/Style/" + SiteID.ToString() + ".css"), "<!-- Style -->\r\n");
                }
            }

            HtmlLink hl = new HtmlLink();
            hl.Href = SupportDir + "/Style/" + SiteID.ToString() + ".css";
            hl.Attributes.Add("type", "text/css");
            hl.Attributes.Add("rel", "stylesheet");

            try
            {
                this.Page.Header.Controls.Add(hl);
            }
            catch { }

            #endregion

            #region 创建主 Part 容器 Div

            Control c = this.Form.FindControl("bodyMain");
            Panel ShovePart2_bodyMain = null;
            if (c == null)
            {
                ShovePart2_bodyMain = new Panel();
                ShovePart2_bodyMain.ID = "bodyMain";
                ShovePart2_bodyMain.CssClass = "bodyMainDiv";

                this.Form.Controls.Add(ShovePart2_bodyMain);

           }

            #endregion

            if (IsAllowDesign)
            {
                #region 画：设计 Panel 菜单浮动层

                #region ShovePart2_divMenu

                ShovePart2_panel.ID = "ShovePart2_divMenu";
                ShovePart2_panel.Width = new Unit("132px");
                ShovePart2_panel.Height = new Unit("440px");
                ShovePart2_panel.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShovePart2_panel.Style.Add(HtmlTextWriterStyle.Left, "0px");
                ShovePart2_panel.Style.Add(HtmlTextWriterStyle.Top, "5px");
                ShovePart2_panel.Style.Add(HtmlTextWriterStyle.ZIndex, "20000");
                ShovePart2_panel.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                //ShovePart2_panel.Style.Add(HtmlTextWriterStyle.Display, "none");
                ShovePart2_panel.Style.Add(HtmlTextWriterStyle.BackgroundImage, SupportDir + "/Images/Designer_bg.gif");

                #endregion

                #region ShovePart2_btnEdit

                ShovePart2_btnEdit.ID = "ShovePart2_btnEdit";
                ShovePart2_btnEdit.Src = SupportDir + "/Images/DesignerMenuOpen.gif";
                ShovePart2_btnEdit.Style.Add(HtmlTextWriterStyle.Left, "132px");
                ShovePart2_btnEdit.Style.Add(HtmlTextWriterStyle.Top, "100px");
                ShovePart2_btnEdit.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShovePart2_btnEdit.Attributes.Add("onclick", "ShovePart2DesignMenu_moveMenu()");

                #endregion

                #region ShovePart2_ddlMode

                ShovePart2_ddlMode.ID = "ShovePart2_ddlMode";
                ShovePart2_ddlMode.Items.Clear();
                ShovePart2_ddlMode.Width = new Unit("109px");
                ShovePart2_ddlMode.Height = new Unit("25px");
                ShovePart2_ddlMode.Items.Add(new ListItem("浏览模式", "0"));
                ShovePart2_ddlMode.Items.Add(new ListItem("设计模式", "1"));
                ShovePart2_ddlMode.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShovePart2_ddlMode.Style.Add(HtmlTextWriterStyle.Top, "70px");
                ShovePart2_ddlMode.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShovePart2_ddlMode.Style.Add(HtmlTextWriterStyle.FontSize, "15");
                ShovePart2_ddlMode.AutoPostBack = true;
                ShovePart2_ddlMode.SelectedIndexChanged += new EventHandler(ShovePart2_ddlMode_SelectedIndexChanged);
                ShovePart2_ddlMode.SelectedIndex = IsDesigning ? 1 : 0;

                #endregion

                IniFile ini = new IniFile(this.Server.MapPath("~/ShoveWebUI_client/Data/ShovePart2.UserControls.ini"));

                #region ShovePart2_labTitle

                ShovePart2_labTitle.ID = "ShovePart2_labTitle";
                ShovePart2_labTitle.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShovePart2_labTitle.Style.Add(HtmlTextWriterStyle.Top, "15px");
                ShovePart2_labTitle.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShovePart2_labTitle.Font.Name = "tahoma";
                ShovePart2_labTitle.Font.Size = FontUnit.Point(8);

                string _title = ini.Read("Options", "DesignerTitle").Trim();

                if (_title == "")
                {
                    _title = "ShovePart2";
                }

                ShovePart2_labTitle.Text = _title;

                #endregion

                #region ShovePart2_labSubTitle

                ShovePart2_labSubTitle.ID = "ShovePart2_labSubTitle";
                ShovePart2_labSubTitle.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShovePart2_labSubTitle.Style.Add(HtmlTextWriterStyle.Top, "35px");
                ShovePart2_labSubTitle.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShovePart2_labSubTitle.Font.Name = "tahoma";
                ShovePart2_labSubTitle.Font.Size = FontUnit.Point(8);

                _title = ini.Read("Options", "DesignerSubTitle").Trim();

                if (_title == "")
                {
                    _title = "设计菜单";
                }

                ShovePart2_labSubTitle.Text = _title;

                #endregion

                string supportdir = SupportDir;

                #region ShovePart2_btnAddNewPage

                SetButtonStyle(ShovePart2_btnAddNewPage, "ShovePart2_btnAddNewPage", "添加一个新网页", "100px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShovePart2_btnAddNewPage.OnClientClick = "this.disabled=true";
                ShovePart2_btnAddNewPage.OnClientClick = "var NewPageName = ShovePart2DesignMenu_AddNewPage_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "'); if (!NewPageName) return false; window.location.href = 'Default.aspx?PN=' + NewPageName; return false;";

                #endregion

                #region ShovePart2_btnAddNewLayout

                SetButtonStyle(ShovePart2_btnAddNewLayout, "ShovePart2_btnAddNewLayout", "导入页面布局", "130px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                //ShovePart2_btnAddNewLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要导入本页布局吗？(建议导入布局前先备份此页面！)") + "')) return false; this.disabled=true";
                ShovePart2_btnAddNewLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要导入新页面布局吗？(建议导入布局前先备份此页面！)") + "')) return false; this.disabled=true;ShovePart2DesignMenu_AddNewPageLayout_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "');";

                #endregion

                #region ShovePart2_btnAddShovePart2

                SetButtonStyle(ShovePart2_btnAddShovePart2, "ShovePart2_btnAddShovePart2", "添加一个Part", "160px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShovePart2_btnAddShovePart2.OnClientClick = "this.disabled=true";
                ShovePart2_btnAddShovePart2.Click += new EventHandler(ShovePart2_btnAddShovePart2_Click);

                #endregion

                #region ShovePart2_btnBackupSiteLayout

                SetButtonStyle(ShovePart2_btnBackupSiteLayout, "ShovePart2_btnBackupSiteLayout", "备份本站布局", "190px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShovePart2_btnBackupSiteLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要备份本站布局吗？") + "')) return false; this.disabled = true; var CallAjaxResult = Shove.Web.UI.ShovePart2BasePage.BackupSite(" + SiteID.ToString() + "); if (CallAjaxResult.value == '') alert('备份成功。'); else alert(CallAjaxResult.value); this.disabled = false; return false;";

                #endregion

                #region ShovePart2_btnRestoreSiteLayout

                SetButtonStyle(ShovePart2_btnRestoreSiteLayout, "ShovePart2_btnRestoreSiteLayout", "恢复本站布局", "220px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShovePart2_btnRestoreSiteLayout.OnClientClick = "if (ShovePart2DesignMenu_RestoreSiteLayout_Open(" + SiteID + ", '" + supportdir + "') != 'Restored') return false;";
                ShovePart2_btnRestoreSiteLayout.Click += new EventHandler(ShovePart2_btnRestoreSiteLayout_Click);

                #endregion

                #region ShovePart2_btnClearSiteLayout

                //SetButtonStyle(ShovePart2_btnClearSiteLayout, "ShovePart2_btnClearSiteLayout", "删除本站布局", "250px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                //ShovePart2_btnClearSiteLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要删除本站布局吗？") + "')) return false; this.disabled=true";
                //ShovePart2_btnClearSiteLayout.Click += new EventHandler(ShovePart2_btnClearSiteLayout_Click);

                #endregion

                #region ShovePart2_btnBackupPageLayout

                SetButtonStyle(ShovePart2_btnBackupPageLayout, "ShovePart2_btnBackupPageLayout", "备份本页布局", "280px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShovePart2_btnBackupPageLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要备份本页布局吗？") + "')) return false; this.disabled = true; var CallAjaxResult = Shove.Web.UI.ShovePart2BasePage.BackupPage(" + SiteID.ToString() + ", '" + PageName + "'); if (CallAjaxResult.value == '') alert('备份成功。'); else alert(CallAjaxResult.value); this.disabled = false; return false;";

                #endregion

                #region ShovePart2_btnRestorePageLayout

                SetButtonStyle(ShovePart2_btnRestorePageLayout, "ShovePart2_btnRestorePageLayout", "恢复本页布局", "250px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShovePart2_btnRestorePageLayout.OnClientClick = "if (ShovePart2DesignMenu_RestorePageLayout_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "') != 'Restored') return false;";
                ShovePart2_btnRestorePageLayout.Click += new EventHandler(ShovePart2_btnRestorePageLayout_Click);

                #endregion

                #region ShovePart2_btnClearPageLayout

                SetButtonStyle(ShovePart2_btnClearPageLayout, "ShovePart2_btnClearPageLayout", "删除本页布局", "310px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShovePart2_btnClearPageLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要删除本页布局吗？") + "')) return false; this.disabled=true";
                ShovePart2_btnClearPageLayout.Click += new EventHandler(ShovePart2_btnClearPageLayout_Click);

                #endregion

                #region ShovePart2_btnUpLoadStyle

                SetButtonStyle(ShovePart2_btnUpLoadStyle, "ShovePart2_btnUpLoadStyle", "上传/下载样式表", "340px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShovePart2_btnUpLoadStyle.OnClientClick = "if (ShovePart2DesignMenu_UpLoadStyle_Open(" + SiteID + ", '" + supportdir + "') != 'Uploaded') return false;";
                ShovePart2_btnUpLoadStyle.Click += new EventHandler(ShovePart2_btnUpLoadStyle_Click);

                #endregion

                #region ShovePart2_btnPageList

                SetButtonStyle(ShovePart2_btnPageList, "ShovePart2_btnPageList", "查看网页列表", "370px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShovePart2_btnPageList.OnClientClick = "var PageName = ShovePart2DesignMenu_PageList_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "'); if (!PageName) return false; window.location.href = 'Default.aspx?PN=' + PageName + '&Designing=False'; return false;";

                #endregion

                #region ShovePart2_btnCopyPage

                //SetButtonStyle(ShovePart2_btnCopyPage, "ShovePart2_btnCopyPage", "复制页面", "430px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                //ShovePart2_btnCopyPage.OnClientClick = "if (ShovePart2DesignMenu_CopyPage_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "') != 'Copied') return false;";
                //ShovePart2_btnCopyPage.Click += new EventHandler(ShovePart2_btnCopyPage_Click);

                #endregion

                #region ShovePart2_SavePage

                SetButtonStyle(ShovePart2_SavePage, "ShovePart2_SavePage", "保存页面设计", "400px", supportdir + "/Images/botton_bg.gif", IsDesigning);

                ShovePart2_SavePage.Click += new EventHandler(ShovePart2_SavePageLayout);

                #endregion

                try
                {
                    ShovePart2_panel.Controls.Add(ShovePart2_btnEdit);
                    ShovePart2_panel.Controls.Add(ShovePart2_ddlMode);

                    ShovePart2_panel.Controls.Add(ShovePart2_labTitle);
                    ShovePart2_panel.Controls.Add(ShovePart2_labSubTitle);

                    ShovePart2_panel.Controls.Add(ShovePart2_btnAddNewLayout);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnAddShovePart2);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnAddNewPage);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnBackupSiteLayout);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnRestoreSiteLayout);
                    //ShovePart2_panel.Controls.Add(ShovePart2_btnClearSiteLayout);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnBackupPageLayout);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnRestorePageLayout);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnClearPageLayout);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnUpLoadStyle);
                    ShovePart2_panel.Controls.Add(ShovePart2_btnPageList);
                    //ShovePart2_panel.Controls.Add(ShovePart2_btnCopyPage);
                    ShovePart2_panel.Controls.Add(ShovePart2_SavePage);
                }
                catch { }

                #endregion

                try
                {
                    this.Form.Controls.Add(ShovePart2_panel);

                }
                catch { }
            }
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShovePart2DesignMenu", this.Page.ClientScript.GetWebResourceUrl(typeof(ShovePart2), "Shove.Web.UI.Script.ShovePart2DesignMenu.js"));

            //加入样式应用文件
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("href", "Private/" + SiteID.ToString() + "/style/" + PageName + ".css");
            this.Header.Controls.Add(link);

            #region 装载 Part

            int RowNo = -1;
            DataTable dtParts = new ShovePart2File(SiteID, PageName).Read(null, null, ref RowNo);
            //读布局
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            string FileDir = AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString();

            string Path = AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/Layout/" + PageName + ".xml";
            if (!System.IO.File.Exists(Path))
            {
                return;
            }
            try
            {
                doc.Load(Path);
            }
            catch (Exception ex)
            {
                JavaScript.Alert(this.Page, "读取布局文件失败！" + ex.Message);
            }

            if (PublicFunction.GetCookie("ShoveWebPageLayout_" + SiteID.ToString() + "_" + PageName) == "")
            {
                string strDIV = System.IO.File.ReadAllText(Path);
                PublicFunction.AddCookie("ShoveWebPageLayout_" + SiteID.ToString() + "_" + PageName, strDIV);
            }

            string va = PublicFunction.GetCookie("cooldrag_" + SiteID.ToString() + "_" + PageName);
            if (va.Trim() == "")
            {
                if (!System.IO.File.Exists(FileDir + "/ShovePart2Layout.ini"))
                {
                    FileStream fs = System.IO.File.Create(FileDir + "/ShovePart2Layout.ini");
                    fs.Close();
                }

                IniFile ini1 = new IniFile(FileDir + "/ShovePart2Layout.ini");
                va = ini1.Read("Layout", PageName);
                PublicFunction.AddCookie("cooldrag_" + SiteID.ToString() + "_" + PageName, va);
            }
            string CheckPartIsLoad = "";

            getChildren(doc.ChildNodes, ShovePart2_bodyMain, va, dtParts, ShovePart2_bodyMain, ref CheckPartIsLoad);

            for (int k = 0; k < dtParts.Rows.Count; k++)
            {
                if (CheckPartIsLoad.IndexOf(dtParts.Rows[k]["id"].ToString() + ",") < 0)
                {
                    ShovePart2 swp = new ShovePart2();

                    swp.ID = dtParts.Rows[k]["id"].ToString();
                    swp.SiteID = SiteID;
                    swp.UserID = UserID;
                    swp.PageName = PageName;
                    if (Form.FindControl(swp.ID) != null)
                    {
                        this.Response.Write(String.Format("有多个重复的相同 ID 为 {0} 的 Part。系统只装载了 1 个。<br />", swp.ID));

                        continue;
                    }

                    try
                    {
                        CheckPartIsLoad += dtParts.Rows[k]["id"].ToString() + ",";
                        ShovePart2_bodyMain.Controls.Add(swp);
                    }
                    catch
                    {
                        this.Response.Write(String.Format("ID 为 {0} 的 Part 装载失败。<br />", swp.ID));
                    }
                }
            }

            #endregion

            if (IsDesigning)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShoveWebUI_ShovePart2_SetOnKeyDown", "<script type=\"text/javascript\">ShoveWebUI_ShovePart2_SetOnKeyDown(" + SiteID.ToString() + ", '" + PageName + "');</script>");
            }
            //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script>if (document.all){    window.attachEvent('onload',ShovePart2DesignMenu_Init)}else{    window.addEventListener('load',ShovePart2DesignMenu_Init,false);}</script>");

            base.OnLoad(e);
        }

        /// <summary>
        /// 设置 button 的外观
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="top"></param>
        /// <param name="backgroundimage"></param>
        /// <param name="enabled"></param>
        private void SetButtonStyle(Button btn, string id, string text, string top, string backgroundimage, bool enabled)
        {
            btn.ID = id;
            btn.Text = text;
            btn.Width = new Unit("109px");
            btn.Height = new Unit("25px");
            btn.Style.Add(HtmlTextWriterStyle.Left, "10px");
            btn.Style.Add(HtmlTextWriterStyle.Top, top);
            btn.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            btn.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            btn.Style.Add(HtmlTextWriterStyle.BackgroundImage, backgroundimage);
            if (enabled)
            {
                btn.Style.Add(HtmlTextWriterStyle.Cursor, "hand");
            }
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            btn.UseSubmitBehavior = false;
            btn.CausesValidation = false;
            btn.Enabled = enabled;
        }

        #region 事件处理区

        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShovePart2_ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsDesigning)
            {
                this.Session.Remove("Shove.Web.UI.ShovePart2.RunMode");
            }
            else
            {
                this.Session["Shove.Web.UI.ShovePart2.RunMode"] = "DESIGN";
            }

            this.Response.Redirect(BuildUrl(!IsDesigning), true);
        }

        private string BuildUrl(bool DesignState)
        {
            string Url = this.Request.Url.AbsoluteUri;

            if (Url.IndexOf("Designing=" + DesignState.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Url;
            }

            if (Url.IndexOf("Designing=" + (!DesignState).ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Url.Replace("Designing=" + (!DesignState).ToString(), "Designing=" + DesignState.ToString());
            }

            if (String.IsNullOrEmpty(this.Request.Url.Query))
            {
                Url += "?";
            }
            else
            {
                Url += "&";
            }

            Url += "Designing=" + DesignState.ToString();

            return Url;
        }

        /// <summary>
        /// 动态添加ShovePart2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShovePart2_btnAddShovePart2_Click(object sender, EventArgs e)
        {
            Control panelDiv = this.Form.FindControl("bodyMain");

            if (panelDiv == null)
            {
                JavaScript.Alert(this.Page, "没有找到可以放置 ShovePart2 部件的父容器控件(bodyMain)。");

                return;
            }

            // 将原来的 Part 的 ZIndex 全部减去1
            ShovePart2File swpf = new ShovePart2File(SiteID, PageName);
            swpf.ZIndexSubtractOne();

            // 增加一个 Part
            ShovePart2 ShovePart2 = new ShovePart2();
            ShovePart2.ID = swpf.GetNewPartID();

            #region 先将其加入到布局文件中ini
            //string FileDir = AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString();

            //if (!System.IO.File.Exists(FileDir + "/ShovePart2Layout.ini"))
            //{
            //    FileStream fs = System.IO.File.Create(FileDir + "/ShovePart2Layout.ini");
            //    fs.Close();
            //}

            //IniFile ini = new IniFile(FileDir + "/ShovePart2Layout.ini");
            //string va = ini.Read("Layout", PageName);
            ////string va = PublicFunction.GetCookie("cooldrag" + PageName);

            //va = va + "|" + ShovePart2.ID + ",";
            ////PublicFunction.AddCookie("cooldrag" + PageName, va);


            //ini.Write("Layout", PageName, va);
            #endregion

            ShovePart2.ZIndex = 10000;

            try
            {
                panelDiv.Controls.Add(ShovePart2);
            }
            catch (Exception ee)
            {
                JavaScript.Alert(this.Page, "增加新 ShovePart2 部件发生错误。(" + ee.Message + ")");

                return;
            }
        }

        /// <summary>
        /// 备份站点布局
        /// </summary>
        /// <param name="siteid"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string BackupSite(long siteid)
        {
            ShovePart2File swpf = new ShovePart2File(siteid, null);
            swpf.BackupSite();

            return "";
        }

        /// <summary>
        /// 保存站点页面的Part框架布局
        /// </summary>
        /// <param name="layoutvalue"></param>
        /// <param name="PageName"></param>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string SavePageLayout(string layoutvalue, string PageName, long SiteID)
        {
            try
            {
                string FileDir = this.Page.Server.MapPath("../Private/" + SiteID.ToString());
                if (!System.IO.Directory.Exists(FileDir))
                {
                    System.IO.Directory.CreateDirectory(FileDir);
                }
                if (!System.IO.File.Exists(FileDir + "/ShovePart2Layout.ini"))
                {
                    FileStream fs = System.IO.File.Create(FileDir + "/ShovePart2Layout.ini");
                    fs.Close();
                }
                IniFile ini = new IniFile(FileDir + "/ShovePart2Layout.ini");
                ini.Write("Layout", PageName, layoutvalue);

                ShovePart2File swpf = new ShovePart2File(SiteID, PageName);
                int RowNo = 0;
                DataTable dt = swpf.Read(null, null, ref RowNo);
                swpf.Write(dt, null);

            }
            catch (Exception e)
            { }
            return "";
        }

        /// <summary>
        /// 读取站点页面的Part框架布局
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string ReadPageLayout(string PageName, long SiteID)
        {
            string FileDir = this.Page.Server.MapPath("../Private/" + SiteID.ToString());
            if (!System.IO.File.Exists(FileDir + "/ShovePart2Layout.ini"))
            {
                FileStream fs = System.IO.File.Create(FileDir + "/ShovePart2Layout.ini");
                fs.Close();
            }
            IniFile ini = new IniFile(FileDir + "/ShovePart2Layout.ini");
            string va = ini.Read("Layout", PageName);
            return va;

        }

        /// <summary>
        /// 写 ini 文件
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Section"></param>
        protected void WriteIni(string Text, string Section)
        {
            try
            {
                IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + "SystemLog.ini");
                ini.Write(Section, System.DateTime.Now.ToString(), Text);
            }
            catch { }
        }

        /// <summary>
        /// 保存设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShovePart2_SavePageLayout(object sender, EventArgs e)
        {
            // string FileDir = this.Page.Server.MapPath("../Private/" + SiteID.ToString());

            try
            {
                //保存之前备份布局文件
                BackupPage(SiteID, PageName);

                string FileDir = AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString();
                if (!System.IO.Directory.Exists(FileDir))
                {
                    System.IO.Directory.CreateDirectory(FileDir);
                }
                if (!System.IO.File.Exists(FileDir + "/ShovePart2Layout.ini"))
                {
                    FileStream fs = System.IO.File.Create(FileDir + "/ShovePart2Layout.ini");
                    fs.Close();
                }
                IniFile ini = new IniFile(FileDir + "/ShovePart2Layout.ini");
                string layoutvalue = PublicFunction.GetCookie("cooldrag_" + SiteID.ToString() + "_" + PageName);

                ini.Write("Layout", PageName, layoutvalue);
                ShovePart2File swpf = new ShovePart2File(SiteID, PageName);
                int RowNo = 0;
                DataTable dt = swpf.Read(null, null, ref RowNo);
                swpf.Write(dt, null);

                PublicFunction.DeleteCookie("cooldrag_" + SiteID.ToString() + "_" + PageName);
                //HttpContext.Current.Request.Cookies.Clear();

                JavaScript.Alert(this.Page, "保存设计成功");
            }
            catch (Exception ex)
            {
                JavaScript.Alert(this.Page, "保存设计失败" + ex.Message + "Trace：" + ex.StackTrace);
            }

            return;

        }

        /// <summary>
        /// 恢复本站布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShovePart2_btnRestoreSiteLayout_Click(object sender, EventArgs e)
        {
            // 本按钮先在客户端弹出页面，如果对话框返回值等于"Restored"，才会执行下面代码
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// 删除本站布局 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShovePart2_btnClearSiteLayout_Click(object sender, EventArgs e)
        {
            ShovePart2File swpf = new ShovePart2File(SiteID, PageName);
            swpf.DeleteSite();

            string Designing = this.Request.QueryString["Designing"];

            if (Designing != "True")
            {
                Designing = "False";
            }

            this.Response.Redirect("Default.aspx?Designing=" + Designing.ToString(), true);
        }

        /// <summary>
        /// 备份页面布局
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string BackupPage(long siteid, string pagename)
        {
            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            swpf.BackupPage();

            return "";
        }

        /// <summary>
        /// 恢复本页布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShovePart2_btnRestorePageLayout_Click(object sender, EventArgs e)
        {
            // 本按钮先在客户端弹出页面，如果对话框返回值等于"Restored"，才会执行下面代码
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// 删除本页布局 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShovePart2_btnClearPageLayout_Click(object sender, EventArgs e)
        {
            ShovePart2File swpf = new ShovePart2File(SiteID, PageName);
            swpf.DeletePage();

            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// 上传、下载样式表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShovePart2_btnUpLoadStyle_Click(object sender, EventArgs e)
        {
            // 本按钮先在客户端弹出页面，如果对话框返回值等于"Uploaded"，才会执行下面代码
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// 复制页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShovePart2_btnCopyPage_Click(object sender, EventArgs e)
        {
            // 本按钮先在客户端弹出页面，如果对话框返回值等于"Copied"，才会执行下面代码
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }
        /// <summary>
        /// 获取一个新的用户控件的 ID
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="UserControlTagName"></param>
        /// <returns></returns>
        private string GetNewUserControlID(string Html, string UserControlTagName)
        {
            int i = 1;

            while (true)
            {
                string ID = UserControlTagName + i.ToString();
                string Content = "<ShoveWebUI:" + UserControlTagName + " ID=\"" + UserControlTagName + i.ToString();

                if (Html.Contains(Content))
                {
                    i++;

                    continue;
                }

                return UserControlTagName + i.ToString();
            }
        }

        /// <summary>
        /// 根据用户控件的文件名，获取一个 TagName
        /// </summary>
        /// <param name="AscxFileName"></param>
        /// <returns></returns>
        private string GetUserControlTagName(string AscxFileName)
        {
            if (String.IsNullOrEmpty(AscxFileName))
            {
                return "";
            }

            if (AscxFileName.StartsWith("~/"))
            {
                AscxFileName = AscxFileName.Substring(2);
            }

            if (AscxFileName.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase))
            {
                AscxFileName = AscxFileName.Substring(0, AscxFileName.Length - 5);
            }

            return AscxFileName.Replace("/", "_");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnl"></param>
        /// <param name="parent"></param>
        /// <param name="Layout"></param>
        /// <param name="dtParts"></param>
        /// <param name="BasePanel"></param>
        /// <param name="CheckPartIsLoad"></param>
        protected void getChildren(XmlNodeList xnl, Panel parent, string Layout, DataTable dtParts, Panel BasePanel, ref string CheckPartIsLoad)
        {
            if (xnl.Count == 0)
            {
                return;
            }

            for (int m = 0; m < xnl.Count; m++)
            {
                if (xnl[m].Attributes["id"] == null)
                {
                    continue;
                }

                string id = xnl[m].Attributes["id"].Value;

                Panel p = new Panel();
                p.ID = id;

                if (IsDesigning)//设计模式div加边线
                {
                    if ((p.BorderStyle == BorderStyle.None) || (p.BorderStyle == BorderStyle.NotSet))
                    {
                        p.BorderStyle = BorderStyle.Dashed;
                        p.BorderWidth = new Unit("1px");
                        p.BorderColor = Color.Gray;
                    }
                }

                // container 为系统特殊的字符串，ShovePart 保留关键字
                if (id.ToLower().IndexOf("container") >= 0)
                {
                    string[] Parts = Layout.Split('|');
                    string strShoveParts = "";  //容器包含的Part

                    //string CheckPartIsLoad = "";//判读是否这个Part已经被加载
                    for (int i = 0; i < Parts.Length; i++)
                    {
                        string idpart = "";

                        if (Parts[i].Length <= 0)
                        {
                            continue;
                        }

                        if (Parts[i].IndexOf(':') < 0)
                        {
                            continue;
                        }

                        idpart = Parts[i].Substring(0, Parts[i].IndexOf(':')).Trim();
                        if (id != idpart)
                        {
                            continue;
                        }

                        strShoveParts = Parts[i].Substring(Parts[i].IndexOf(':') + 1, Parts[i].Length - (Parts[i].IndexOf(':') + 1)); //容器包含的Part
                        string[] ShovePartsList = strShoveParts.Split(',');

                        for (int y = 0; y < ShovePartsList.Length; y++)
                        {
                            if (ShovePartsList[y] == "")
                            {
                                continue;
                            }

                            if (dtParts.Select("id='" + ShovePartsList[y] + "'").Length <= 0)
                            {
                                continue;
                            }

                            ShovePart2 swp = new ShovePart2();

                            swp.ID = ShovePartsList[y];
                            swp.SiteID = SiteID;
                            swp.UserID = UserID;
                            swp.PageName = PageName;

                            if (Form.FindControl(swp.ID) != null)
                            {
                                this.Response.Write(String.Format("有多个重复的相同 ID 为 {0} 的 Part。系统只装载了 1 个。<br />", swp.ID));

                                continue;
                            }

                            try
                            {
                                CheckPartIsLoad += ShovePartsList[y] + ",";

                                p.Controls.Add(swp);
                            }
                            catch
                            {
                                this.Response.Write(String.Format("ID 为 {0} 的 Part 装载失败。<br />", swp.ID));
                            }
                        }
                    }
                }

                parent.Controls.Add(p);
                XmlNodeList x = xnl[m].ChildNodes;
                getChildren(x, p, Layout, dtParts, BasePanel, ref CheckPartIsLoad);
            }
        }

        #endregion
    }
}
