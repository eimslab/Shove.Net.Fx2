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
    public partial class ShoveWebPartAttributes : System.Web.UI.Page
    {
        #region 组件

        protected System.Web.UI.WebControls.DropDownList ddlBorderStyle;
        protected System.Web.UI.WebControls.TextBox tbBorderWidth;
        protected System.Web.UI.WebControls.TextBox tbBorderColor;
        //protected System.Web.UI.HtmlControls.HtmlButton btnBorderColor;
        protected System.Web.UI.WebControls.DropDownList ddlTitleImage;
        protected System.Web.UI.WebControls.DropDownList ddlBackImage;
        protected System.Web.UI.WebControls.DropDownList ddlBottomImage;
        protected System.Web.UI.HtmlControls.HtmlInputFile fileBackImage;
        protected System.Web.UI.WebControls.DropDownList ddlAscxFileName;
        protected System.Web.UI.WebControls.DropDownList ddlAscxAlign;
        protected System.Web.UI.WebControls.DropDownList ddlAscxVAlign;
        protected System.Web.UI.HtmlControls.HtmlImage imgTitleImage;
        protected System.Web.UI.HtmlControls.HtmlImage imgBackImage;
        protected System.Web.UI.HtmlControls.HtmlImage imgBottomImage;
        protected System.Web.UI.WebControls.TextBox tbBackColor;
        protected System.Web.UI.WebControls.Button btnUploadImage;
        protected System.Web.UI.WebControls.Label labTip;
        protected System.Web.UI.WebControls.CheckBox cbAutoHeight;
        protected System.Web.UI.WebControls.TextBox tbLeft;
        protected System.Web.UI.WebControls.TextBox tbTop;
        protected System.Web.UI.WebControls.TextBox tbWidth;
        protected System.Web.UI.WebControls.TextBox tbHeight;
        protected System.Web.UI.WebControls.TextBox tbCssClass;
        protected System.Web.UI.WebControls.CheckBox cbTopUpLimit;

        protected System.Web.UI.WebControls.TextBox tbTitleImageUrlLink;
        protected System.Web.UI.WebControls.TextBox tbBottomImageUrlLink;

        protected System.Web.UI.WebControls.DropDownList ddlTitleImageUrlLinkTarget;
        protected System.Web.UI.WebControls.DropDownList ddlBottomImageUrlLinkTarget;

        protected System.Web.UI.WebControls.CheckBox cbApplyToAllPage;
        protected System.Web.UI.WebControls.CheckBox cbAddToNoExistPage;

        protected System.Web.UI.WebControls.HiddenField hiControlAttributes;
        protected System.Web.UI.WebControls.HiddenField hiascxFileName;
        protected System.Web.UI.WebControls.HiddenField hiSiteDir;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnChooseUC;

        protected System.Web.UI.WebControls.HiddenField hiUserID;
        protected System.Web.UI.WebControls.HiddenField hiSiteID;

        #endregion

        #region 属性变量

        public string Attrib_ImageUploadDir = "";
        public string Attrib_AscxControlFileName = "";
        public string Attrib_HorizontalAlign = "";
        public string Attrib_VerticalAlign = "";
        public string Attrib_Border = "";
        public string Attrib_BorderColor = "";
        public string Attrib_BorderWidth = "";
        public string Attrib_BorderStyle = "";
        public string Attrib_BackColor = "";
        public string Attrib_TitleImage = "";
        public string Attrib_BackImage = "";
        public string Attrib_BottomImage = "";
        public string Attrib_AutoHeight = "";
        public string Attrib_Left = "";
        public string Attrib_Top = "";
        public string Attrib_Width = "";
        public string Attrib_Height = "";
        public string Attrib_CssClass = "";
        public string Attrib_TopUpLimit = "";

        public string Attrib_TitleImageUrlLink = "";
        public string Attrib_BottomImageUrlLink = "";

        public string Attrib_TitleImageUrlLinkTarget = "";
        public string Attrib_BottomImageUrlLinkTarget = "";

        public string Attrib_ControlAttributes = "";

        public string Attrib_SiteID = "";
        public string Attrib_UserID = "";

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShoveWebPart.RunMode"].ToString() != "DESIGN")
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

                Attrib_ImageUploadDir = System.Web.HttpUtility.UrlDecode(this.Request["ImageUploadDir"]);
                Attrib_AscxControlFileName = System.Web.HttpUtility.UrlDecode(this.Request["AscxControlFileName"]);
                Attrib_HorizontalAlign = System.Web.HttpUtility.UrlDecode(this.Request["HorizontalAlign"]);
                Attrib_VerticalAlign = System.Web.HttpUtility.UrlDecode(this.Request["VerticalAlign"]);
                Attrib_Border = System.Web.HttpUtility.UrlDecode(this.Request["Border"]);
                Attrib_BackColor = System.Web.HttpUtility.UrlDecode(this.Request["BackColor"]);
                Attrib_TitleImage = System.Web.HttpUtility.UrlDecode(this.Request["TitleImageUrl"]);
                Attrib_BackImage = System.Web.HttpUtility.UrlDecode(this.Request["BackImage"]);
                Attrib_BottomImage = System.Web.HttpUtility.UrlDecode(this.Request["BottomImage"]);
                Attrib_AutoHeight = System.Web.HttpUtility.UrlDecode(this.Request["AutoHeight"]);
                Attrib_Left = System.Web.HttpUtility.UrlDecode(this.Request["Left"]);
                Attrib_Top = System.Web.HttpUtility.UrlDecode(this.Request["Top"]);
                Attrib_Width = System.Web.HttpUtility.UrlDecode(this.Request["Width"]);
                Attrib_Height = System.Web.HttpUtility.UrlDecode(this.Request["Height"]);
                Attrib_CssClass = System.Web.HttpUtility.UrlDecode(this.Request["CssClass"]);
                Attrib_TopUpLimit = System.Web.HttpUtility.UrlDecode(this.Request["TopUpLimit"]);

                Attrib_TitleImageUrlLink = System.Web.HttpUtility.UrlDecode(this.Request["TitleImageUrlLink"]);
                Attrib_BottomImageUrlLink = System.Web.HttpUtility.UrlDecode(this.Request["BottomImageUrlLink"]);

                Attrib_TitleImageUrlLinkTarget = System.Web.HttpUtility.UrlDecode(this.Request["TitleImageUrlLinkTarget"]);
                Attrib_BottomImageUrlLinkTarget = System.Web.HttpUtility.UrlDecode(this.Request["BottomImageUrlLinkTarget"]);

                Attrib_ControlAttributes = this.Request["ControlAttributes"];// System.Web.HttpUtility.UrlDecode(this.Request["ControlAttributes"]);

                #endregion

                #region 修改变量以适应要求

                if ((Attrib_ImageUploadDir != "") && (!Attrib_ImageUploadDir.EndsWith("/")))
                {
                    Attrib_ImageUploadDir += "/";
                }

                if (Attrib_ImageUploadDir.StartsWith("~/"))
                {
                    Attrib_ImageUploadDir = Attrib_ImageUploadDir.Substring(2, Attrib_ImageUploadDir.Length - 2);
                }

                string UploadPath = this.Server.MapPath("../../" + Attrib_ImageUploadDir);

                if (!System.IO.Directory.Exists(UploadPath))
                {
                    System.IO.Directory.CreateDirectory(UploadPath);
                }

                string[] Attrib_BorderList = Attrib_Border.Split(' ');

                if (Attrib_BorderList.Length == 3)
                {
                    Attrib_BorderColor = Attrib_BorderList[0];
                    Attrib_BorderWidth = Attrib_BorderList[1];
                    Attrib_BorderStyle = Attrib_BorderList[2];
                }
                else if (Attrib_BorderList.Length == 2)
                {
                    Attrib_BorderColor = Attrib_BorderList[0];
                    Attrib_BorderWidth = Attrib_BorderList[1];
                    Attrib_BorderStyle = "None";
                }

                if (Attrib_BorderWidth == "")
                {
                    Attrib_BorderWidth = "0px";
                }

                if (Attrib_BorderColor == "")
                {
                    Attrib_BorderColor = "black";
                }

                if (Attrib_Left == "")
                {
                    Attrib_Left = "0px";
                }

                if (Attrib_Top == "")
                {
                    Attrib_Top = "0px";
                }

                if (Attrib_Width == "")
                {
                    Attrib_Width = "100px";
                }

                if (Attrib_Height == "")
                {
                    Attrib_Height = "auto";

                    Attrib_AutoHeight = "True";
                }

                #region Attrib_TitleImage、Attrib_BackImage、Attrib_BottomImage、Attrib_TitleImageUrlLink、Attrib_BottomImageUrlLink

                string Url = PublicFunction.GetSiteUrl() + "/";
                try
                {
                    if (Attrib_TitleImage.StartsWith(Url))
                    {
                        Attrib_TitleImage = Attrib_TitleImage.Replace(Url, "");
                    }

                    int l = Attrib_TitleImage.IndexOf(Attrib_ImageUploadDir);

                    if (l >= 0)
                    {
                        Attrib_TitleImage = Attrib_TitleImage.Substring(l, Attrib_TitleImage.Length - l);
                    }

                    if (Attrib_BackImage.StartsWith("url("))
                    {
                        Attrib_BackImage = Attrib_BackImage.Substring(4, Attrib_BackImage.Length - 5);
                    }

                    if (Attrib_BackImage.StartsWith(Url))
                    {
                        Attrib_BackImage = Attrib_BackImage.Replace(Url, "");
                    }

                    l = Attrib_BackImage.IndexOf(Attrib_ImageUploadDir);

                    if (l >= 0)
                    {
                        Attrib_BackImage = Attrib_BackImage.Substring(l, Attrib_BackImage.Length - l);
                    }

                    if (Attrib_BottomImage.StartsWith(Url))
                    {
                        Attrib_BottomImage = Attrib_BottomImage.Replace(Url, "");
                    }

                    l = Attrib_BottomImage.IndexOf(Attrib_ImageUploadDir);

                    if (l >= 0)
                    {
                        Attrib_BottomImage = Attrib_BottomImage.Substring(l, Attrib_BottomImage.Length - l);
                    }

                    if (Attrib_TitleImageUrlLink.StartsWith(Url))
                    {
                        Attrib_TitleImageUrlLink = Attrib_TitleImageUrlLink.Replace(Url, "");
                    }

                    if (Attrib_BottomImageUrlLink.StartsWith(Url))
                    {
                        Attrib_BottomImageUrlLink = Attrib_BottomImageUrlLink.Replace(Url, "");
                    }
                }
                catch { }

                #endregion

                #endregion

                #region 将变量存入 ViewState

                ViewState["Attrib_SiteID"] = Attrib_SiteID;
                ViewState["Attrib_UserID"] = Attrib_UserID;
                ViewState["Attrib_ImageUploadDir"] = Attrib_ImageUploadDir;
                ViewState["Attrib_AscxControlFileName"] = Attrib_AscxControlFileName;
                ViewState["Attrib_HorizontalAlign"] = Attrib_HorizontalAlign;
                ViewState["Attrib_VerticalAlign"] = Attrib_VerticalAlign;
                ViewState["Attrib_Border"] = Attrib_Border;
                ViewState["Attrib_BorderColor"] = Attrib_BorderColor;
                ViewState["Attrib_BorderWidth"] = Attrib_BorderWidth;
                ViewState["Attrib_BorderStyle"] = Attrib_BorderStyle;
                ViewState["Attrib_BackColor"] = Attrib_BackColor;
                ViewState["Attrib_TitleImage"] = Attrib_TitleImage;
                ViewState["Attrib_BackImage"] = Attrib_BackImage;
                ViewState["Attrib_BottomImage"] = Attrib_BottomImage;
                ViewState["Attrib_AutoHeight"] = Attrib_AutoHeight;
                ViewState["Attrib_Left"] = Attrib_Left;
                ViewState["Attrib_Top"] = Attrib_Top;
                ViewState["Attrib_Width"] = Attrib_Width;
                ViewState["Attrib_Height"] = Attrib_Height;
                ViewState["Attrib_CssClass"] = Attrib_CssClass;
                ViewState["Attrib_TopUpLimit"] = Attrib_TopUpLimit;
                ViewState["Attrib_TitleImageUrlLink"] = Attrib_TitleImageUrlLink;
                ViewState["Attrib_BottomImageUrlLink"] = Attrib_BottomImageUrlLink;
                ViewState["Attrib_TitleImageUrlLinkTarget"] = Attrib_TitleImageUrlLinkTarget;
                ViewState["Attrib_BottomImageUrlLinkTarget"] = Attrib_BottomImageUrlLinkTarget;
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
                Attrib_ImageUploadDir = ViewState["Attrib_ImageUploadDir"] + "";
                Attrib_AscxControlFileName = ViewState["Attrib_AscxControlFileName"] + "";
                Attrib_HorizontalAlign = ViewState["Attrib_HorizontalAlign"] + "";
                Attrib_VerticalAlign = ViewState["Attrib_VerticalAlign"] + "";
                Attrib_Border = ViewState["Attrib_Border"] + "";
                Attrib_BorderColor = ViewState["Attrib_BorderColor"] + "";
                Attrib_BorderWidth = ViewState["Attrib_BorderWidth"] + "";
                Attrib_BorderStyle = ViewState["Attrib_BorderStyle"] + "";
                Attrib_BackColor = ViewState["Attrib_BackColor"] + "";
                Attrib_TitleImage = ViewState["Attrib_TitleImage"] + "";
                Attrib_BackImage = ViewState["Attrib_BackImage"] + "";
                Attrib_BottomImage = ViewState["Attrib_BottomImage"] + "";
                Attrib_AutoHeight = ViewState["Attrib_AutoHeight"] + "";
                Attrib_Left = ViewState["Attrib_Left"] + "";
                Attrib_Top = ViewState["Attrib_Top"] + "";
                Attrib_Width = ViewState["Attrib_Width"] + "";
                Attrib_Height = ViewState["Attrib_Height"] + "";
                Attrib_CssClass = ViewState["Attrib_CssClass"] + "";
                Attrib_TopUpLimit = ViewState["Attrib_TopUpLimit"] + "";
                Attrib_TitleImageUrlLink = ViewState["Attrib_TitleImageUrlLink"] + "";
                Attrib_BottomImageUrlLink = ViewState["Attrib_BottomImageUrlLink"] + "";
                Attrib_TitleImageUrlLinkTarget = ViewState["Attrib_TitleImageUrlLinkTarget"] + "";
                Attrib_BottomImageUrlLinkTarget = ViewState["Attrib_BottomImageUrlLinkTarget"] + "";
                Attrib_ControlAttributes = ViewState["Attrib_ControlAttributes"] + "";

                #endregion
            }

            if (!this.IsPostBack)
            {
                BindData();

                ddlTitleImage.Attributes.Add("onchange", "ddlTitleImage_onchange(this)");
                ddlBackImage.Attributes.Add("onchange", "ddlBackImage_onchange(this)");
                ddlBottomImage.Attributes.Add("onchange", "ddlBottomImage_onchange(this)");

                cbAutoHeight.Attributes.Add("onclick", "cbAutoHeight_onclick(this)");
            }
        }

        private void BindData()
        {
            // 填充边线类型下拉框
            ddlBorderStyle.Items.Clear();

            ddlBorderStyle.Items.Add(new ListItem("未设置", BorderStyle.NotSet.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("无边线", BorderStyle.None.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("点线", BorderStyle.Dotted.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("虚线", BorderStyle.Dashed.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("实线", BorderStyle.Solid.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("双线", BorderStyle.Double.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("凹下", BorderStyle.Groove.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("凸起", BorderStyle.Ridge.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("立体凹下", BorderStyle.Inset.ToString()));
            ddlBorderStyle.Items.Add(new ListItem("立体凸起", BorderStyle.Outset.ToString()));

            ControlExt.SetDownListBoxTextFromValue(ddlBorderStyle, Attrib_BorderStyle);

            // 填充边线宽度
            tbBorderWidth.Text = Attrib_BorderWidth;

            // 填充边线颜色
            tbBorderColor.Text = Attrib_BorderColor;

            // 填充定位：Left Top Width Height
            tbLeft.Text = Attrib_Left;
            tbTop.Text = Attrib_Top;
            tbWidth.Text = Attrib_Width;
            tbHeight.Text = Attrib_Height;

            // 填充自适应高度
            try
            {
                cbAutoHeight.Checked = bool.Parse(Attrib_AutoHeight);
            }
            catch { }

            // 填充限制上移
            try
            {
                cbTopUpLimit.Checked = bool.Parse(Attrib_TopUpLimit);
            }
            catch { }

            //获取系统图片
            string[] ImgSystem = Shove.Web.UI.File.GetFileList(this.Page.Server.MapPath("../../Images/UserControl/"), ".gif|.jpg|.bmp|.png|.jpeg|.swf");

            //获取站点图片
            string[] ImgFiles = Shove.Web.UI.File.GetFileList(this.Page.Server.MapPath("~/" + Attrib_ImageUploadDir), ".gif|.jpg|.bmp|.png|.jpeg|.swf");

            //获取用户元素库图片
            string ImageDirList = PublicFunction.GetWebConfigAppSettingAsString("ShoveWebPartUserElementDirectory");

            string[] Dirs = null;

            if (ImageDirList != "" && ImageDirList.Contains(","))
            {
                Dirs = ImageDirList.Split(',');
            }

            // 填充标题图片下拉框
            ddlTitleImage.Items.Clear();
            ddlTitleImage.Items.Add("不使用标题图片");

            string RootPath = this.Page.Server.MapPath("~/");

            if (ImgSystem != null)
            {
                foreach (string Img in ImgSystem)
                {
                    ddlTitleImage.Items.Add(Img.Replace(RootPath, "").Replace("\\", "/"));//加载系统图片
                }
            }

            if (ImgFiles != null)
            {
                foreach (string ImgFile in ImgFiles)
                {
                    ddlTitleImage.Items.Add(ImgFile.Replace(RootPath, "").Replace("\\", "/"));//加载站点图片
                }
            }
            AddDropList(ddlTitleImage, Dirs, RootPath);//加载用户元素库

            string _Attrib_TitleImage = Attrib_TitleImage;
            if (_Attrib_TitleImage.StartsWith("~/"))
            {
                _Attrib_TitleImage = _Attrib_TitleImage.Substring(2, _Attrib_TitleImage.Length - 2);
            }

            ControlExt.SetDownListBoxText(ddlTitleImage, _Attrib_TitleImage);

            // 标题图片链接地址
            tbTitleImageUrlLink.Text = Attrib_TitleImageUrlLink;

            // 标题图片链接打开位置
            ControlExt.SetDownListBoxText(ddlTitleImageUrlLinkTarget, Attrib_TitleImageUrlLinkTarget);

            // 填充背景图片下拉框
            ddlBackImage.Items.Clear();
            ddlBackImage.Items.Add("不使用背景图片");

            if (ImgSystem != null)
            {
                foreach (string Img in ImgSystem)
                {
                    ddlBackImage.Items.Add(Img.Replace(RootPath, "").Replace("\\", "/"));//加载系统图片
                }
            }

            if (ImgFiles != null)
            {
                foreach (string ImgFile in ImgFiles)
                {
                    ddlBackImage.Items.Add(ImgFile.Replace(RootPath, "").Replace("\\", "/"));
                }
            }
            AddDropList(ddlBackImage, Dirs, RootPath);//加载用户元素库

            string _Attrib_BackImage = Attrib_BackImage;
            if (_Attrib_BackImage.StartsWith("~/"))
            {
                _Attrib_BackImage = _Attrib_BackImage.Substring(2, _Attrib_BackImage.Length - 2);
            }

            ControlExt.SetDownListBoxText(ddlBackImage, _Attrib_BackImage);

            // 填充底部图片下拉框
            ddlBottomImage.Items.Clear();
            ddlBottomImage.Items.Add("不使用底部图片");

            if (ImgSystem != null)
            {
                foreach (string Img in ImgSystem)
                {
                    ddlBottomImage.Items.Add(Img.Replace(RootPath, "").Replace("\\", "/"));//加载系统图片
                }
            }

            if (ImgFiles != null)
            {
                foreach (string ImgFile in ImgFiles)
                {
                    ddlBottomImage.Items.Add(ImgFile.Replace(RootPath, "").Replace("\\", "/"));
                }
            }
            AddDropList(ddlBottomImage, Dirs, RootPath);//加载用户元素库

            string _Attrib_BottomImage = Attrib_BottomImage;
            if (_Attrib_BottomImage.StartsWith("~/"))
            {
                _Attrib_BottomImage = _Attrib_BottomImage.Substring(2, _Attrib_BottomImage.Length - 2);
            }

            ControlExt.SetDownListBoxText(ddlBottomImage, _Attrib_BottomImage);

            // 底部图片链接地址
            tbBottomImageUrlLink.Text = Attrib_BottomImageUrlLink;

            // 底部图片链接打开位置
            ControlExt.SetDownListBoxText(ddlBottomImageUrlLinkTarget, Attrib_BottomImageUrlLinkTarget);

            // 填充标题图片框
            if (Attrib_TitleImage != "")
            {
                imgTitleImage.Src = "../../" + _Attrib_TitleImage;
                imgTitleImage.Alt = "";
            }
            else
            {
                imgTitleImage.Src = "about:blank";
                imgTitleImage.Style["backgroundColor"] = "buttonface";
                imgTitleImage.Alt = "未设置图片";

                ddlTitleImage.SelectedIndex = 0;
            }

            // 填充背景图片框
            if (Attrib_BackImage != "")
            {
                imgBackImage.Src = "../../" + _Attrib_BackImage;
                imgBackImage.Alt = "";
            }
            else
            {
                imgBackImage.Src = "about:blank";
                imgBackImage.Style["backgroundColor"] = "buttonface";
                imgBackImage.Alt = "未设置图片";

                ddlBackImage.SelectedIndex = 0;
            }

            // 填充底部图片框
            if (Attrib_BottomImage != "")
            {
                imgBottomImage.Src = "../../" + _Attrib_BottomImage;
                imgBottomImage.Alt = "";
            }
            else
            {
                imgBottomImage.Src = "about:blank";
                imgBottomImage.Style["backgroundColor"] = "buttonface";
                imgBottomImage.Alt = "未设置图片";

                ddlBottomImage.SelectedIndex = 0;
            }

            // 填充背景颜色
            tbBackColor.Text = Attrib_BackColor;

            // 填充样式
            tbCssClass.Text = Attrib_CssClass;

            // 填充窗口内容
            ddlAscxFileName.Items.Clear();
            ddlAscxFileName.Items.Add("不需要填充内容");

            #region 获取控件列表，填充窗口内容

            IniFile ini = new IniFile(this.Server.MapPath("../Data/ShoveWebPart.UserControls.ini"));

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
                string DirPath = PublicFunction.GetWebConfigAppSettingAsString("ShoveWebPartUserControlDirectory");
                string[] AscxFiles = null;

                DataSet ds = null;

                if (DirPath == "ShoveWebPartUserControlsAndTypeDataSet")
                {
                    try
                    {
                        ds = (DataSet)HttpContext.Current.Application[SystemPreFix + "ShoveWebPartUserControlsAndTypeDataSet" + Attrib_UserID];
                    }
                    catch { }
                }

                if ((DirPath == "ShoveWebPartUserControlsAndTypeDataSet") && (ds != null) && (ds.Tables.Count == 4))
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
                else if ((DirPath != "ShoveWebPartUserControlsAndTypeDataSet") && (DirPath != ""))
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

            // 填充窗口内容水平对齐方式
            ddlAscxAlign.Items.Clear();

            ddlAscxAlign.Items.Add(new ListItem("未设置", HorizontalAlign.NotSet.ToString()));
            ddlAscxAlign.Items.Add(new ListItem("居左", HorizontalAlign.Left.ToString()));
            ddlAscxAlign.Items.Add(new ListItem("居中", HorizontalAlign.Center.ToString()));
            ddlAscxAlign.Items.Add(new ListItem("居右", HorizontalAlign.Right.ToString()));

            ControlExt.SetDownListBoxTextFromValue(ddlAscxAlign, Attrib_HorizontalAlign);

            // 填充窗口内容垂直对齐方式
            ddlAscxVAlign.Items.Clear();

            ddlAscxVAlign.Items.Add(new ListItem("未设置", VerticalAlign.NotSet.ToString()));
            ddlAscxVAlign.Items.Add(new ListItem("居顶", VerticalAlign.Top.ToString()));
            ddlAscxVAlign.Items.Add(new ListItem("居中", VerticalAlign.Middle.ToString()));
            ddlAscxVAlign.Items.Add(new ListItem("居底", VerticalAlign.Bottom.ToString()));

            ControlExt.SetDownListBoxTextFromValue(ddlAscxVAlign, Attrib_VerticalAlign);
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

                    if (ctrl is ShoveWebPartUserControl)
                    {
                        ShoveWebPartUserControl swpuc = (ShoveWebPartUserControl)ctrl;

                        ddlAscxFileName.Items.Add(new ListItem(String.IsNullOrEmpty(swpuc.Name) ? "~/" + AscxFile.Replace(RootPath, "").Replace("\\", "/") : swpuc.Name, "~/" + AscxFile.Replace(RootPath, "").Replace("\\", "/")));
                    }
                    else
                    {
                        ddlAscxFileName.Items.Add(new ListItem("~/" + AscxFile.Replace(RootPath, "").Replace("\\", "/"), "~/" + AscxFile.Replace(RootPath, "").Replace("\\", "/")));
                    }
                }
            }
        }

        /// <summary>
        /// 上传站点图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (fileBackImage.Value == "")
            {
                btnUploadImage.Enabled = false;
                return;
            }

            string ShortFileName = "";
            if (Shove.Web.UI.File.UploadFile(this.Page, fileBackImage, "../../" + Attrib_ImageUploadDir, ref ShortFileName, true, "image") == 0)
            {
                ddlTitleImage.Items.Add(Attrib_ImageUploadDir + ShortFileName);
                ddlBackImage.Items.Add(Attrib_ImageUploadDir + ShortFileName);
                ddlBottomImage.Items.Add(Attrib_ImageUploadDir + ShortFileName);

                labTip.Text = "上传成功。";
                labTip.Visible = true;
            }
            else
            {
                labTip.Text = "上传失败。";
                labTip.Visible = true;
            }
        }

        /// <summary>
        /// 读取用户元素库图片，填充下拉框
        /// </summary>
        /// <param name="list"></param>
        /// <param name="Dirs"></param>
        /// <param name="RootPath"></param>
        private void AddDropList(DropDownList list, string[] Dirs, string RootPath)
        {
            string[] ImgFiles = null;

            if (Dirs != null)
            {
                for (int i = 0; i < Dirs.Length; i++)
                {
                    ImgFiles = Shove.Web.UI.File.GetFileList(this.Page.Server.MapPath(Dirs[i].Replace("[UserID]", Attrib_UserID)), ".gif|.jpg|.swf|.png|.jpeg|.bmp|.fla|.psd");

                    if (ImgFiles != null)
                    {
                        foreach (string ImgFile in ImgFiles)
                        {
                            list.Items.Add(ImgFile.Replace(RootPath, "").Replace("\\", "/"));
                        }
                    }
                }
            }
        }
    }
}