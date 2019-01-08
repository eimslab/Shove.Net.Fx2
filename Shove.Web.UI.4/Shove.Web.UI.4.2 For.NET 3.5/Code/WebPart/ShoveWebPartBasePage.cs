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
[assembly: WebResource("Shove.Web.UI.Script.ShoveWebPartDesignMenu.js", "application/javascript")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPart��ҳ
    /// </summary>
    public class ShoveWebPartBasePage : System.Web.UI.Page
    {
        #region �ؼ�

        /// <summary>
        /// shoveWebPart������
        /// </summary>
        public System.Web.UI.WebControls.Panel ShoveWebPart_bodyMain;

        /// <summary>
        /// �༭�˵� Panel
        /// </summary>
        public System.Web.UI.WebControls.Panel ShoveWebPart_panel;

        /// <summary>
        /// �򿪹ر� Panel �İ�ť
        /// </summary>
        public System.Web.UI.HtmlControls.HtmlImage ShoveWebPart_btnEdit;


        /// <summary>
        /// ��ơ�����ģʽ�л�
        /// </summary>
        public System.Web.UI.WebControls.DropDownList ShoveWebPart_ddlMode;

        /// <summary>
        /// ��Ʋ˵�����
        /// </summary>
        public System.Web.UI.WebControls.Label ShoveWebPart_labTitle;

        /// <summary>
        /// ��Ʋ˵�������
        /// </summary>
        public System.Web.UI.WebControls.Label ShoveWebPart_labSubTitle;

        /// <summary>
        /// �����ҳ��ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnAddNewPage;


        /// <summary>
        /// ����ҳ�沼�ְ�ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnAddNewLayout;
        /// <summary>
        /// ���ShoveWebPart��ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnAddShoveWebPart;

        /// <summary>
        /// ���ݱ�վ���ְ�ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnBackupSiteLayout;

        /// <summary>
        /// �ָ���վ���ְ�ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnRestoreSiteLayout;

        /// <summary>
        /// ɾ����վ���ְ�ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnClearSiteLayout;

        /// <summary>
        /// ���ݱ�ҳ���ְ�ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnBackupPageLayout;

        /// <summary>
        /// �ָ���ҳ���ְ�ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnRestorePageLayout;

        /// <summary>
        /// ɾ����ҳ���ְ�ť
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnClearPageLayout;

        /// <summary>
        /// �ϴ�/������ʽ��
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnUpLoadStyle;

        /// <summary>
        /// �鿴��ҳ�б�
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnPageList;

        /// <summary>
        /// ����ҳ��
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnCopyPage;

        /// <summary>
        /// ����ҳ�����
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_SavePage;

        #endregion

        /// <summary>
        /// վ��IDĬ��Ϊ-1
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
        /// �û�ID
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
        /// �Ƿ�������ƣ��ɼ̳�ҳ������û��Ƿ��½���Ƿ������Ȩ�޶�����
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
        /// �Ƿ������״̬
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
        /// �Ƿ�����
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
        /// ҳ��
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
        /// ͼƬ�ϴ���ַ
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
        // ����ڸ�Ŀ¼�����·��
        private string SupportDir
        {
            get
            {
                return PublicFunction.GetCurrentRelativePath() + "ShoveWebUI_client";
            }
        }

        /// <summary>
        /// ShoveWebPart��ҳ���캯��
        /// </summary>
        public ShoveWebPartBasePage()
        {
            ShoveWebPart_panel = new Panel();

            ShoveWebPart_btnEdit = new HtmlImage();

            ShoveWebPart_ddlMode = new DropDownList();

            ShoveWebPart_labTitle = new Label();
            ShoveWebPart_labSubTitle = new Label();

            ShoveWebPart_btnAddNewPage = new Button();

            ShoveWebPart_btnAddNewLayout = new Button();
            ShoveWebPart_btnAddShoveWebPart = new Button();
            ShoveWebPart_btnBackupSiteLayout = new Button();
            ShoveWebPart_btnRestoreSiteLayout = new Button();
            ShoveWebPart_btnClearSiteLayout = new Button();
            ShoveWebPart_btnBackupPageLayout = new Button();
            ShoveWebPart_btnRestorePageLayout = new Button();
            ShoveWebPart_btnClearPageLayout = new Button();
            ShoveWebPart_btnUpLoadStyle = new Button();
            ShoveWebPart_btnPageList = new Button();
            ShoveWebPart_btnCopyPage = new Button();
            ShoveWebPart_SavePage = new Button();
            SiteID = -1;
            UserID = -1;
            IsAllowDesign = true;
        }

        /// <summary>
        /// ����վ��ID�����ⷽ��
        /// </summary>
        protected virtual void SetSiteID()
        {
            // �����Ҫ��վ����Ҫʵ�ִ˷���
        }

        /// <summary>
        /// �����û������ⷽ��
        /// </summary>
        protected virtual void SetUserID()
        {
            // �����Ҫ��վ����Ҫʵ�ִ˷���
        }

        /// <summary>
        /// ����ҳ���Ƿ��������
        /// </summary>
        protected virtual void SetAllowDesign()
        {
            // �����û��Ƿ�������Ȩ������
        }

        /// <summary>
        /// ��дҳ������¼�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            SetSiteID();
            SetUserID();
            SetAllowDesign();

            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPart", this.Page.ClientScript.GetWebResourceUrl(typeof(ShoveWebPart), "Shove.Web.UI.Script.ShoveWebPart.js"));

            if (IsDesigning)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ShoveWebPart));
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ShoveWebPartBasePage));

                this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPartDesigning", this.Page.ClientScript.GetWebResourceUrl(typeof(ShoveWebPart), "Shove.Web.UI.Script.ShoveWebPartDesigning.js"));

                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "str", "<script>window.onload=function(){CoolDrag.init('bodyMain','" + SiteID.ToString() + "','" + PageName + "');}</script>");
                
            }

            #region ������ʽ��

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

            #region ������ Part ���� Div

            Control c = this.Form.FindControl("bodyMain");
            Panel ShoveWebPart_bodyMain = null;
            if (c == null)
            {
                ShoveWebPart_bodyMain = new Panel();
                ShoveWebPart_bodyMain.ID = "bodyMain";
                ShoveWebPart_bodyMain.CssClass = "bodyMainDiv";

                this.Form.Controls.Add(ShoveWebPart_bodyMain);

           }

            #endregion

            if (IsAllowDesign)
            {
                #region ������� Panel �˵�������

                #region ShoveWebPart_divMenu

                ShoveWebPart_panel.ID = "ShoveWebPart_divMenu";
                ShoveWebPart_panel.Width = new Unit("132px");
                ShoveWebPart_panel.Height = new Unit("500px");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Left, "0px");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Top, "5px");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.ZIndex, "20000");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                //ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Display, "none");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.BackgroundImage, SupportDir + "/Images/Designer_bg.gif");

                #endregion

                #region ShoveWebPart_btnEdit

                ShoveWebPart_btnEdit.ID = "ShoveWebPart_btnEdit";
                ShoveWebPart_btnEdit.Src = SupportDir + "/Images/DesignerMenuOpen.gif";
                ShoveWebPart_btnEdit.Style.Add(HtmlTextWriterStyle.Left, "132px");
                ShoveWebPart_btnEdit.Style.Add(HtmlTextWriterStyle.Top, "100px");
                ShoveWebPart_btnEdit.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_btnEdit.Attributes.Add("onclick", "ShoveWebPartDesignMenu_moveMenu()");

                #endregion

                #region ShoveWebPart_ddlMode

                ShoveWebPart_ddlMode.ID = "ShoveWebPart_ddlMode";
                ShoveWebPart_ddlMode.Items.Clear();
                ShoveWebPart_ddlMode.Width = new Unit("109px");
                ShoveWebPart_ddlMode.Height = new Unit("25px");
                ShoveWebPart_ddlMode.Items.Add(new ListItem("���ģʽ", "0"));
                ShoveWebPart_ddlMode.Items.Add(new ListItem("���ģʽ", "1"));
                ShoveWebPart_ddlMode.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShoveWebPart_ddlMode.Style.Add(HtmlTextWriterStyle.Top, "70px");
                ShoveWebPart_ddlMode.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_ddlMode.Style.Add(HtmlTextWriterStyle.FontSize, "15");
                ShoveWebPart_ddlMode.AutoPostBack = true;
                ShoveWebPart_ddlMode.SelectedIndexChanged += new EventHandler(ShoveWebPart_ddlMode_SelectedIndexChanged);
                ShoveWebPart_ddlMode.SelectedIndex = IsDesigning ? 1 : 0;

                #endregion

                IniFile ini = new IniFile(this.Server.MapPath("~/ShoveWebUI_client/Data/ShoveWebPart.UserControls.ini"));

                #region ShoveWebPart_labTitle

                ShoveWebPart_labTitle.ID = "ShoveWebPart_labTitle";
                ShoveWebPart_labTitle.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShoveWebPart_labTitle.Style.Add(HtmlTextWriterStyle.Top, "15px");
                ShoveWebPart_labTitle.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_labTitle.Font.Name = "tahoma";
                ShoveWebPart_labTitle.Font.Size = FontUnit.Point(8);

                string _title = ini.Read("Options", "DesignerTitle").Trim();

                if (_title == "")
                {
                    _title = "ShoveWebPart";
                }

                ShoveWebPart_labTitle.Text = _title;

                #endregion

                #region ShoveWebPart_labSubTitle

                ShoveWebPart_labSubTitle.ID = "ShoveWebPart_labSubTitle";
                ShoveWebPart_labSubTitle.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShoveWebPart_labSubTitle.Style.Add(HtmlTextWriterStyle.Top, "35px");
                ShoveWebPart_labSubTitle.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_labSubTitle.Font.Name = "tahoma";
                ShoveWebPart_labSubTitle.Font.Size = FontUnit.Point(8);

                _title = ini.Read("Options", "DesignerSubTitle").Trim();

                if (_title == "")
                {
                    _title = "��Ʋ˵�";
                }

                ShoveWebPart_labSubTitle.Text = _title;

                #endregion

                string supportdir = SupportDir;

                #region ShoveWebPart_btnAddNewPage

                SetButtonStyle(ShoveWebPart_btnAddNewPage, "ShoveWebPart_btnAddNewPage", "���һ������ҳ", "100px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnAddNewPage.OnClientClick = "this.disabled=true";
                ShoveWebPart_btnAddNewPage.OnClientClick = "var NewPageName = ShoveWebPartDesignMenu_AddNewPage_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "'); if (!NewPageName) return false; window.location.href = 'Default.aspx?PN=' + NewPageName; return false;";

                #endregion

                #region ShoveWebPart_btnAddNewLayout

                SetButtonStyle(ShoveWebPart_btnAddNewLayout, "ShoveWebPart_btnAddNewLayout", "����ҳ�沼��", "130px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                //ShoveWebPart_btnAddNewLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("ȷ��Ҫ���뱾ҳ������(���鵼�벼��ǰ�ȱ��ݴ�ҳ�棡)") + "')) return false; this.disabled=true";
                ShoveWebPart_btnAddNewLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("ȷ��Ҫ������ҳ�沼����(���鵼�벼��ǰ�ȱ��ݴ�ҳ�棡)") + "')) return false; this.disabled=true;ShoveWebPartDesignMenu_AddNewPageLayout_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "');";

                #endregion


                #region ShoveWebPart_btnAddShoveWebPart

                SetButtonStyle(ShoveWebPart_btnAddShoveWebPart, "ShoveWebPart_btnAddShoveWebPart", "���һ��Part", "160px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnAddShoveWebPart.OnClientClick = "this.disabled=true";
                ShoveWebPart_btnAddShoveWebPart.Click += new EventHandler(ShoveWebPart_btnAddShoveWebPart_Click);

                #endregion

                #region ShoveWebPart_btnBackupSiteLayout

                SetButtonStyle(ShoveWebPart_btnBackupSiteLayout, "ShoveWebPart_btnBackupSiteLayout", "���ݱ�վ����", "190px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnBackupSiteLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("ȷ��Ҫ���ݱ�վ������") + "')) return false; this.disabled = true; var CallAjaxResult = Shove.Web.UI.ShoveWebPartBasePage.BackupSite(" + SiteID.ToString() + "); if (CallAjaxResult.value == '') alert('���ݳɹ���'); else alert(CallAjaxResult.value); this.disabled = false; return false;";

                #endregion

                #region ShoveWebPart_btnRestoreSiteLayout

                SetButtonStyle(ShoveWebPart_btnRestoreSiteLayout, "ShoveWebPart_btnRestoreSiteLayout", "�ָ���վ����", "220px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnRestoreSiteLayout.OnClientClick = "if (ShoveWebPartDesignMenu_RestoreSiteLayout_Open(" + SiteID + ", '" + supportdir + "') != 'Restored') return false;";
                ShoveWebPart_btnRestoreSiteLayout.Click += new EventHandler(ShoveWebPart_btnRestoreSiteLayout_Click);

                #endregion

                #region ShoveWebPart_btnClearSiteLayout

                SetButtonStyle(ShoveWebPart_btnClearSiteLayout, "ShoveWebPart_btnClearSiteLayout", "ɾ����վ����", "250px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnClearSiteLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("ȷ��Ҫɾ����վ������") + "')) return false; this.disabled=true";
                ShoveWebPart_btnClearSiteLayout.Click += new EventHandler(ShoveWebPart_btnClearSiteLayout_Click);

                #endregion

                #region ShoveWebPart_btnBackupPageLayout

                SetButtonStyle(ShoveWebPart_btnBackupPageLayout, "ShoveWebPart_btnBackupPageLayout", "���ݱ�ҳ����", "280px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnBackupPageLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("ȷ��Ҫ���ݱ�ҳ������") + "')) return false; this.disabled = true; var CallAjaxResult = Shove.Web.UI.ShoveWebPartBasePage.BackupPage(" + SiteID.ToString() + ", '" + PageName + "'); if (CallAjaxResult.value == '') alert('���ݳɹ���'); else alert(CallAjaxResult.value); this.disabled = false; return false;";

                #endregion

                #region ShoveWebPart_btnRestorePageLayout

                SetButtonStyle(ShoveWebPart_btnRestorePageLayout, "ShoveWebPart_btnRestorePageLayout", "�ָ���ҳ����", "310px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnRestorePageLayout.OnClientClick = "if (ShoveWebPartDesignMenu_RestorePageLayout_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "') != 'Restored') return false;";
                ShoveWebPart_btnRestorePageLayout.Click += new EventHandler(ShoveWebPart_btnRestorePageLayout_Click);

                #endregion

                #region ShoveWebPart_btnClearPageLayout

                SetButtonStyle(ShoveWebPart_btnClearPageLayout, "ShoveWebPart_btnClearPageLayout", "ɾ����ҳ����", "340px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnClearPageLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("ȷ��Ҫɾ����ҳ������") + "')) return false; this.disabled=true";
                ShoveWebPart_btnClearPageLayout.Click += new EventHandler(ShoveWebPart_btnClearPageLayout_Click);

                #endregion

                #region ShoveWebPart_btnUpLoadStyle

                SetButtonStyle(ShoveWebPart_btnUpLoadStyle, "ShoveWebPart_btnUpLoadStyle", "�ϴ�/������ʽ��", "370px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnUpLoadStyle.OnClientClick = "if (ShoveWebPartDesignMenu_UpLoadStyle_Open(" + SiteID + ", '" + supportdir + "') != 'Uploaded') return false;";
                ShoveWebPart_btnUpLoadStyle.Click += new EventHandler(ShoveWebPart_btnUpLoadStyle_Click);

                #endregion

                #region ShoveWebPart_btnPageList

                SetButtonStyle(ShoveWebPart_btnPageList, "ShoveWebPart_btnPageList", "�鿴��ҳ�б�", "400px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnPageList.OnClientClick = "var PageName = ShoveWebPartDesignMenu_PageList_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "'); if (!PageName) return false; window.location.href = 'Default.aspx?PN=' + PageName + '&Designing=False'; return false;";

                #endregion

                #region ShoveWebPart_btnCopyPage

                SetButtonStyle(ShoveWebPart_btnCopyPage, "ShoveWebPart_btnCopyPage", "����ҳ��", "430px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnCopyPage.OnClientClick = "if (ShoveWebPartDesignMenu_CopyPage_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "') != 'Copied') return false;";
                ShoveWebPart_btnCopyPage.Click += new EventHandler(ShoveWebPart_btnCopyPage_Click);

                #endregion


                #region ShoveWebPart_SavePage

                SetButtonStyle(ShoveWebPart_SavePage, "ShoveWebPart_SavePage", "����ҳ�����", "460px", supportdir + "/Images/botton_bg.gif", IsDesigning);

                ShoveWebPart_SavePage.Click += new EventHandler(ShoveWebPart_SavePageLayout);

                #endregion


                try
                {
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnEdit);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_ddlMode);

                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_labTitle);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_labSubTitle);

                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnAddNewLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnAddShoveWebPart);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnAddNewPage);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnBackupSiteLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnRestoreSiteLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnClearSiteLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnBackupPageLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnRestorePageLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnClearPageLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnUpLoadStyle);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnPageList);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnCopyPage);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_SavePage);
                }
                catch { }

                #endregion

                try
                {
                    this.Form.Controls.Add(ShoveWebPart_panel);

                }
                catch { }
            }
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPartDesignMenu", this.Page.ClientScript.GetWebResourceUrl(typeof(ShoveWebPart), "Shove.Web.UI.Script.ShoveWebPartDesignMenu.js"));

            //������ʽӦ���ļ�
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("href", "Private/" + SiteID.ToString() + "/style/" + PageName + ".css");
            this.Header.Controls.Add(link);

            #region װ�� Part

            int RowNo = -1;
            DataTable dtParts = new ShoveWebPartFile(SiteID, PageName).Read(null, null, ref RowNo);
            //������
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
                JavaScript.Alert(this.Page, "��ȡ�����ļ�ʧ�ܣ�" + ex.Message);
            }

            if (PublicFunction.GetCookie("ShoveWebPageLayout_" + SiteID.ToString() + "_" + PageName) == "")
            {
                string strDIV = System.IO.File.ReadAllText(Path);
                PublicFunction.AddCookie("ShoveWebPageLayout_" + SiteID.ToString() + "_" + PageName, strDIV);
            }

            string va = PublicFunction.GetCookie("cooldrag_" + SiteID.ToString() + "_" + PageName);
            if (va.Trim() == "")
            {
                if (!System.IO.File.Exists(FileDir + "/ShoveWebPartLayout.ini"))
                {
                    FileStream fs = System.IO.File.Create(FileDir + "/ShoveWebPartLayout.ini");
                    fs.Close();
                }

                IniFile ini1 = new IniFile(FileDir + "/ShoveWebPartLayout.ini");
                va = ini1.Read("Layout", PageName);
                PublicFunction.AddCookie("cooldrag_" + SiteID.ToString() + "_" + PageName, va);
            }
            string CheckPartIsLoad = "";

            getChildren(doc.ChildNodes, ShoveWebPart_bodyMain, va, dtParts, ShoveWebPart_bodyMain, ref CheckPartIsLoad);

            for (int k = 0; k < dtParts.Rows.Count; k++)
            {
                if (CheckPartIsLoad.IndexOf(dtParts.Rows[k]["id"].ToString() + ",") < 0)
                {
                    ShoveWebPart swp = new ShoveWebPart();

                    swp.ID = dtParts.Rows[k]["id"].ToString();
                    swp.SiteID = SiteID;
                    swp.UserID = UserID;
                    swp.PageName = PageName;
                    if (Form.FindControl(swp.ID) != null)
                    {
                        this.Response.Write(String.Format("�ж���ظ�����ͬ ID Ϊ {0} �� Part��ϵͳֻװ���� 1 ����<br />", swp.ID));

                        continue;
                    }

                    try
                    {
                        CheckPartIsLoad += dtParts.Rows[k]["id"].ToString() + ",";
                        ShoveWebPart_bodyMain.Controls.Add(swp);
                    }
                    catch
                    {
                        this.Response.Write(String.Format("ID Ϊ {0} �� Part װ��ʧ�ܡ�<br />", swp.ID));
                    }
                }
            }

            #endregion

            if (IsDesigning)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShoveWebUI_ShoveWebPart_SetOnKeyDown", "<script type=\"text/javascript\">ShoveWebUI_ShoveWebPart_SetOnKeyDown(" + SiteID.ToString() + ", '" + PageName + "');</script>");
            }
            //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script>if (document.all){    window.attachEvent('onload',ShoveWebPartDesignMenu_Init)}else{    window.addEventListener('load',ShoveWebPartDesignMenu_Init,false);}</script>");

            base.OnLoad(e);
        }

        /// <summary>
        /// ���� button �����
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

        #region �¼�������

        /// <summary>
        /// ģʽ�л�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsDesigning)
            {
                this.Session.Remove("Shove.Web.UI.ShoveWebPart.RunMode");
            }
            else
            {
                this.Session["Shove.Web.UI.ShoveWebPart.RunMode"] = "DESIGN";
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
        /// ��̬���shoveWebPart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnAddShoveWebPart_Click(object sender, EventArgs e)
        {
            Control panelDiv = this.Form.FindControl("bodyMain");

            if (panelDiv == null)
            {
                JavaScript.Alert(this.Page, "û���ҵ����Է��� ShoveWebPart �����ĸ������ؼ�(bodyMain)��");

                return;
            }

            // ��ԭ���� Part �� ZIndex ȫ����ȥ1
            ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
            swpf.ZIndexSubtractOne();

            // ����һ�� Part
            ShoveWebPart shoveWebPart = new ShoveWebPart();
            shoveWebPart.ID = swpf.GetNewPartID();

            #region �Ƚ�����뵽�����ļ���ini
            //string FileDir = AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString();

            //if (!System.IO.File.Exists(FileDir + "/ShoveWebPartLayout.ini"))
            //{
            //    FileStream fs = System.IO.File.Create(FileDir + "/ShoveWebPartLayout.ini");
            //    fs.Close();
            //}

            //IniFile ini = new IniFile(FileDir + "/ShoveWebPartLayout.ini");
            //string va = ini.Read("Layout", PageName);
            ////string va = PublicFunction.GetCookie("cooldrag" + PageName);

            //va = va + "|" + shoveWebPart.ID + ",";
            ////PublicFunction.AddCookie("cooldrag" + PageName, va);


            //ini.Write("Layout", PageName, va);
            #endregion

            shoveWebPart.ZIndex = 10000;

            try
            {
                panelDiv.Controls.Add(shoveWebPart);
            }
            catch (Exception ee)
            {
                JavaScript.Alert(this.Page, "������ ShoveWebPart ������������(" + ee.Message + ")");

                return;
            }
        }

        /// <summary>
        /// ����վ�㲼��
        /// </summary>
        /// <param name="siteid"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string BackupSite(long siteid)
        {
            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, null);
            swpf.BackupSite();

            return "";
        }

        /// <summary>
        /// ����վ��ҳ���Part��ܲ���
        /// </summary>
        /// <param name="layout"></param>
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
                if (!System.IO.File.Exists(FileDir + "/ShoveWebPartLayout.ini"))
                {
                    FileStream fs = System.IO.File.Create(FileDir + "/ShoveWebPartLayout.ini");
                    fs.Close();
                }
                IniFile ini = new IniFile(FileDir + "/ShoveWebPartLayout.ini");
                ini.Write("Layout", PageName, layoutvalue);

                ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
                int RowNo = 0;
                DataTable dt = swpf.Read(null, null, ref RowNo);
                swpf.Write(dt, null);

            }
            catch (Exception e)
            { }
            return "";
        }

        /// <summary>
        /// ��ȡվ��ҳ���Part��ܲ���
        /// </summary>
        /// <param name="siteid"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string ReadPageLayout(string PageName, long SiteID)
        {
            string FileDir = this.Page.Server.MapPath("../Private/" + SiteID.ToString());
            if (!System.IO.File.Exists(FileDir + "/ShoveWebPartLayout.ini"))
            {
                FileStream fs = System.IO.File.Create(FileDir + "/ShoveWebPartLayout.ini");
                fs.Close();
            }
            IniFile ini = new IniFile(FileDir + "/ShoveWebPartLayout.ini");
            string va = ini.Read("Layout", PageName);
            return va;

        }

        /// <summary>
        /// д ini �ļ�
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
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShoveWebPart_SavePageLayout(object sender, EventArgs e)
        {
            // string FileDir = this.Page.Server.MapPath("../Private/" + SiteID.ToString());

            try
            {
                string FileDir = AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString();
                if (!System.IO.Directory.Exists(FileDir))
                {
                    System.IO.Directory.CreateDirectory(FileDir);
                }
                if (!System.IO.File.Exists(FileDir + "/ShoveWebPartLayout.ini"))
                {
                    FileStream fs = System.IO.File.Create(FileDir + "/ShoveWebPartLayout.ini");
                    fs.Close();
                }
                IniFile ini = new IniFile(FileDir + "/ShoveWebPartLayout.ini");
                string layoutvalue = PublicFunction.GetCookie("cooldrag_" + SiteID.ToString() + "_" + PageName);

                ini.Write("Layout", PageName, layoutvalue);
                ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
                int RowNo = 0;
                DataTable dt = swpf.Read(null, null, ref RowNo);
                swpf.Write(dt, null);

                PublicFunction.DeleteCookie("cooldrag_" + SiteID.ToString() + "_" + PageName);
                //HttpContext.Current.Request.Cookies.Clear();

                JavaScript.Alert(this.Page, "������Ƴɹ�");
            }
            catch (Exception ex)
            {
                JavaScript.Alert(this.Page, "�������ʧ��" + ex.Message + "Trace��" + ex.StackTrace);
            }

            return;

        }

        /// <summary>
        /// �ָ���վ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnRestoreSiteLayout_Click(object sender, EventArgs e)
        {
            // ����ť���ڿͻ��˵���ҳ�棬����Ի��򷵻�ֵ����"Restored"���Ż�ִ���������
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// ɾ����վ���� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnClearSiteLayout_Click(object sender, EventArgs e)
        {
            ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
            swpf.DeleteSite();

            string Designing = this.Request.QueryString["Designing"];

            if (Designing != "True")
            {
                Designing = "False";
            }

            this.Response.Redirect("Default.aspx?Designing=" + Designing.ToString(), true);
        }

        /// <summary>
        /// ����ҳ�沼��
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string BackupPage(long siteid, string pagename)
        {
            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
            swpf.BackupPage();

            return "";
        }

        /// <summary>
        /// �ָ���ҳ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnRestorePageLayout_Click(object sender, EventArgs e)
        {
            // ����ť���ڿͻ��˵���ҳ�棬����Ի��򷵻�ֵ����"Restored"���Ż�ִ���������
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// ɾ����ҳ���� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnClearPageLayout_Click(object sender, EventArgs e)
        {
            ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
            swpf.DeletePage();

            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// �ϴ���������ʽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnUpLoadStyle_Click(object sender, EventArgs e)
        {
            // ����ť���ڿͻ��˵���ҳ�棬����Ի��򷵻�ֵ����"Uploaded"���Ż�ִ���������
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnCopyPage_Click(object sender, EventArgs e)
        {
            // ����ť���ڿͻ��˵���ҳ�棬����Ի��򷵻�ֵ����"Copied"���Ż�ִ���������
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }
        /// <summary>
        /// ��ȡһ���µ��û��ؼ��� ID
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
        /// �����û��ؼ����ļ�������ȡһ�� TagName
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

                if (IsDesigning)//���ģʽdiv�ӱ���
                {
                    if ((p.BorderStyle == BorderStyle.None) || (p.BorderStyle == BorderStyle.NotSet))
                    {
                        p.BorderStyle = BorderStyle.Dashed;
                        p.BorderWidth = new Unit("1px");
                        p.BorderColor = Color.Gray;
                    }
                }

                // container Ϊϵͳ������ַ�����ShovePart �����ؼ���
                if (id.ToLower().IndexOf("container") >= 0)
                {
                    string[] Parts = Layout.Split('|');
                    string strShoveParts = "";  //����������Part

                    //string CheckPartIsLoad = "";//�ж��Ƿ����Part�Ѿ�������
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

                        strShoveParts = Parts[i].Substring(Parts[i].IndexOf(':') + 1, Parts[i].Length - (Parts[i].IndexOf(':') + 1)); //����������Part
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

                            ShoveWebPart swp = new ShoveWebPart();

                            swp.ID = ShovePartsList[y];
                            swp.SiteID = SiteID;
                            swp.UserID = UserID;
                            swp.PageName = PageName;

                            if (Form.FindControl(swp.ID) != null)
                            {
                                this.Response.Write(String.Format("�ж���ظ�����ͬ ID Ϊ {0} �� Part��ϵͳֻװ���� 1 ����<br />", swp.ID));

                                continue;
                            }

                            try
                            {
                                CheckPartIsLoad += ShovePartsList[y] + ",";

                                p.Controls.Add(swp);
                            }
                            catch
                            {
                                this.Response.Write(String.Format("ID Ϊ {0} �� Part װ��ʧ�ܡ�<br />", swp.ID));
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
