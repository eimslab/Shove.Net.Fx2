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
using System.Globalization;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
[assembly: WebResource("Shove.Web.UI.Script.ShovePart2.js", "application/javascript")]
[assembly: WebResource("Shove.Web.UI.Script.ShovePart2Designing.js", "application/javascript")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShovePart2 ��ҳ�����������
    /// </summary>
    [Designer(typeof(System.Web.UI.Design.ContainerControlDesigner)), ParseChildren(false), PersistChildren(true)]
    [DefaultProperty("AscxControlFileName"), ToolboxData("<{0}:ShovePart2 runat=server></{0}:ShovePart2>")]
    public class ShovePart2 : Panel, INamingContainer
    {
        /// <summary>
        /// Part ������ģʽ
        /// </summary>
        public enum RunMode
        {
            Run = 0,
            Design = 1
        }

        private struct LicenseLevel
        {
            public const short None = 0;
            public const short RunOnly = 1;
            public const short RunAndDesign = 2;
            public const short Trial = 3;
            public const short DesignOnly = 4;
        }

        /// <summary>
        /// Onload ����
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShovePart2", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Shove.Web.UI.Script.ShovePart2.js"));

            bool _isDesigning = (Mode == RunMode.Design);

            if (_isDesigning)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ShovePart2));

                this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShovePart2Designing", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Shove.Web.UI.Script.ShovePart2Designing.js"));
            }

            ShovePart2File swpf = new ShovePart2File(SiteID, PageName);
            int RowNo = -1;
            DataTable dtParts = swpf.Read(null, ID, ref RowNo);

            if (RowNo < 0)
            {
                DataRow dr = dtParts.NewRow();

                dr["id"] = ID;
                dr["Visable"] = Visible.ToString();
                dr["AscxControlFileName"] = AscxControlFileName;
                dr["ZIndex"] = ZIndex.ToString();
                dr["CssClass"] = CssClass;
                dr["ControlAttributes"] = ControlAttributes.ToString();
                dr["Lock"] = Lock.ToString();

                dtParts.Rows.Add(dr);
                swpf.Write(dtParts, null);
            }
            else
            {
                DataRow dr = dtParts.Rows[RowNo];

                Visible = (dr["Visable"].ToString() == "True");

                AscxControlFileName = dr["AscxControlFileName"].ToString();
                try { ZIndex = int.Parse(dr["ZIndex"].ToString()); }
                catch { }
                CssClass = dr["CssClass"].ToString();
                ControlAttributes = dr["ControlAttributes"].ToString();
                try { Lock = bool.Parse(dr["Lock"].ToString()); }
                catch { }
            }

            base.OnLoad(e);
        }

        #region Ajax ����״̬

        /// <summary>
        /// ����϶����ı��С�󱣴�����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="primitiveheight"></param>
        /// <param name="parentleft"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string Save(string id, long siteid, string pagename, string left, string top, string width, string height, string primitiveheight, string parentleft)
        {
            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            int RowNo = -1;
            DataTable dtParts = swpf.Read(null, id, ref RowNo);
            DataRow dr = null;

            if (RowNo < 0)
            {
                dr = dtParts.NewRow();

                dr["id"] = id;
                dr["AscxControlFileName"] = AscxControlFileName;
                dr["CssClass"] = ControlCssClass;//CssClass;
                dr["ControlAttributes"] = ControlAttributes;

                dtParts.Rows.Add(dr);
            }

            dr = dtParts.Rows[RowNo];

            dr["Visable"] = "True";
            //dr["Left"] = left;
            //dr["Top"] = top;
            //dr["Width"] = width;
            //dr["Height"] = height;
            //dr["PrimitiveHeight"] = primitiveheight;
            //dr["ParentLeft"] = parentleft;

            dtParts.AcceptChanges();
            swpf.Write(dtParts, null);

            return "";
        }

        /// <summary>
        /// �ӱ༭���ڱ༭�󱣴�����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="AttributesList"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string Edit(string id, long siteid, string pagename, string[] AttributesList)
        {
            string _AscxControlFileName = AttributesList[0];
            string _CssClass = AttributesList[1];
            string _ControlAttributes = AttributesList[2];

            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            int RowNo = -1;
            DataTable dtParts = swpf.Read(null, id, ref RowNo);

            DataRow dr = null;
            string _OldAttributes = "";

            if (RowNo < 0)
            {
                dr = dtParts.NewRow();

                dr["id"] = id;
                dr["ParentLeft"] = "0px";

                dtParts.Rows.Add(dr);
            }
            else
            {
                dr = dtParts.Rows[RowNo];
                _OldAttributes = dr["ControlAttributes"].ToString();
            }

            dr["Visable"] = "True";
            dr["AscxControlFileName"] = _AscxControlFileName;
            dr["CssClass"] = _CssClass;
            dr["ControlAttributes"] = _ControlAttributes;

            dtParts.AcceptChanges();
            swpf.Write(dtParts, null);

            Visible = true;
            AscxControlFileName = _AscxControlFileName;
            CssClass = _CssClass;
            ControlAttributes = _ControlAttributes;

            return "";
        }

        /// <summary>
        /// �ر�Part��������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string Close(string id, long siteid, string pagename)
        {
            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            swpf.Delete(id);

            return "";
        }

        /// <summary>
        /// �ı���Part��ǰ��˳�򱣴�����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="objOtherZIndexList"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string SaveZIndex(string id, long siteid, string pagename, string objOtherZIndexList)
        {
            if ((objOtherZIndexList == null) || (objOtherZIndexList == ""))
            {
                return "û����Ҫ����� z-index ���ݡ�";
            }

            objOtherZIndexList = objOtherZIndexList.Substring(0, objOtherZIndexList.Length - 1);
            string[] strs = objOtherZIndexList.Split(';');

            if ((strs == null) || (strs.Length < 1))
            {
                return "û����Ҫ����� z-index ���ݡ�";
            }

            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            swpf.SaveZIndex(strs);

            return "";
        }

        /// <summary>
        /// ����Part��������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="LockState"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string SaveLock(string id, long siteid, string pagename, bool LockState)
        {
            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            swpf.SaveLock(id, LockState);

            Lock = LockState;

            return "";
        }

        /// <summary>
        /// �� Part Ӧ�õ�����ҳ��������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="applytoallpage"></param>
        /// <param name="addtonoexistpage"></param>
        /// <param name="oldattributes"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string SaveApplyToAll(string id, long siteid, string pagename, bool applytoallpage, bool addtonoexistpage, string oldattributes)
        {
            if (!applytoallpage && !addtonoexistpage)
            {
                return "";
            }

            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            int RowNo = -1;
            DataTable dtParts = swpf.Read(null, id, ref RowNo);
            DataRow dr = null;

            if (RowNo < 0)
            {
                return "δ�ҵ�����";
            }

            dr = dtParts.Rows[RowNo];

            if (dr["AscxControlFileName"].ToString() == "")
            {
                return "û��װ�����ݵ� Part ����Ӧ�����Ե�����ҳ��";
            }

            swpf.SaveApplyToAll(id, applytoallpage, addtonoexistpage, oldattributes);

            return "";
        }

        /// <summary>
        /// ��Part�õ׺󱣴�����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string ToBackground(string id, long siteid, string pagename, int zIndex)
        {
            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            swpf.ToBackground(id, zIndex);

            ZIndex = zIndex;

            return "";
        }

        /// <summary>
        /// ͨ�������ƶ�Part�󱣴�����
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="Datas"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string SaveForKeyMove(long siteid, string pagename, string[] Datas)
        {
            if ((Datas == null) || (Datas.Length < 0))
            {
                return "û����Ҫ����ļ����ƶ� Part �����ݡ�";
            }

            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            swpf.SaveForKeyMove(Datas);

            return "";
        }

        /// <summary>
        /// ͨ������ɾ��Part�󱣴�����
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="Datas"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string DeleteForKeyMove(long siteid, string pagename, string[] Datas)
        {
            if ((Datas == null) || (Datas.Length < 0))
            {
                return "û����Ҫɾ���ļ����ƶ� Part �����ݡ�";
            }

            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            swpf.DeleteForKeyMove(Datas);

            return "";
        }

        /// <summary>
        /// ճ��
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        /// <param name="SourcePartList"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string Paste(long siteid, string pagename, string SourcePartList)
        {
            if (String.IsNullOrEmpty(SourcePartList))
            {
                return "������û�����ݡ�";
            }

            string[] SourceParts = SourcePartList.Split(',');

            if ((SourceParts == null) || (SourceParts.Length < 1))
            {
                return "������û�����ݡ�";
            }

            ShovePart2File swpf = new ShovePart2File(siteid, pagename);
            swpf.Paste(SourceParts);

            return "";
        }

        #endregion

        private void OnAttributeChanged(object sender, EventArgs e)
        {
            // this.Page.Response.Redirect(this.Page.Request.Url.AbsoluteUri, true);
        }

        #region IPostBackEventHandler ��Ա

        /// <summary>
        /// ���Ըı�Ļᷢ�¼�
        /// </summary>
        /// <param name="eventArgument"></param>
        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument == "OnAttributeChanged")
            {
                OnAttributeChanged(this, new EventArgs());
            }
        }

        #endregion

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("Part ���� Css ��ʽ")]
        public override string CssClass
        {
            get
            {
                return "";
            }
            set
            {
                this.ViewState["CssClass"] = value;

                ControlCssClass = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("Css ��ʽ (���ƿؼ�����)")]
        public string ControlCssClass
        {
            get
            {
                object obj = this.ViewState["ControlCssClass"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }
            set
            {
                this.ViewState["ControlCssClass"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("Part"), Description("���ڱ��⣬ֻ��ʾ�����ģʽ״̬��")]
        public string Title
        {
            get
            {
                object obj = this.ViewState["Title"];
                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "Part";
                }
            }

            set
            {
                this.ViewState["Title"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        [Bindable(true), Category("Data"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.UserControlFileEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("�˴����е� ascx �û��ؼ�")]
        public string AscxControlFileName
        {
            get
            {
                object obj = this.ViewState["AscxControlFileName"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }

            set
            {
                this.ViewState["AscxControlFileName"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(""), Description("��չ���ԣ����ݵ���չ���ԣ������ݽ��п���")]
        public string ControlAttributes
        {
            get
            {
                object obj = this.ViewState["ControlAttributes"];
                if (obj != null)
                {
                    return (string)obj;
                }
                else
                {
                    return "";
                }
            }

            set
            {
                this.ViewState["ControlAttributes"] = value;
            }
        }

        /// <summary>
        /// ҳ�������ģʽ ����/���
        /// </summary>
        [Bindable(true), Category("��Ϊ"), DefaultValue(RunMode.Run), Description("ģʽ(���/����)")]
        public RunMode Mode
        {
            get
            {
                if (this.Page is ShovePart2BasePage)
                {
                    return ((ShovePart2BasePage)this.Page).IsDesigning ? RunMode.Design : RunMode.Run;
                }

                if ((this.Page.Session["Shove.Web.UI.ShovePart2.RunMode"] != null) && (this.Page.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString().Trim().ToUpper() == "DESIGN"))
                {
                    return RunMode.Design;
                }

                return RunMode.Run;
            }
            set
            {
                if (value == ShovePart2.RunMode.Design)
                {
                    this.Page.Session["Shove.Web.UI.ShovePart2.RunMode"] = "DESIGN";
                }
                else
                {
                    this.Page.Session.Remove("Shove.Web.UI.ShovePart2.RunMode");
                }
            }
        }

        [Bindable(true), Category("��Ϊ"), DefaultValue("ȷ��Ҫɾ���˴��ڿؼ���"), Description("ɾ���˴��ڿؼ�ʱ����ʾ�ı���Ϊ������ʾ������ɾ��")]
        public string CloseConfirmText
        {
            get
            {
                object obj = this.ViewState["CloseConfirmText"];
                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "ȷ��Ҫɾ���˴��ڿؼ���";
                }
            }

            set
            {
                this.ViewState["CloseConfirmText"] = value;
            }
        }

        [Bindable(true), Category("��Ϊ"), DefaultValue("ShoveWebUI_client"), Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("��ϵ�пؼ���֧��Ŀ¼�������ӵ���ص�ͼƬ���ű��ļ�")]
        public string SupportDir
        {
            get
            {
                return PublicFunction.GetCurrentRelativePath() + "ShoveWebUI_client";
            }
        }

        /// <summary>
        /// վ��ID
        /// </summary>
        [Bindable(true), Category("��Ϊ"), DefaultValue(-1), Description("�ؼ������ĸ�վ��")]
        public long SiteID
        {
            get
            {
                if (this.Page is ShovePart2BasePage)
                {
                    return ((ShovePart2BasePage)this.Page).SiteID;
                }

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
        /// ��½���û� ID
        /// </summary>
        [Bindable(true), Category("��Ϊ"), DefaultValue(-1), Description("��ǰ��¼���û�ID")]
        public long UserID
        {
            get
            {
                if (this.Page is ShovePart2BasePage)
                {
                    return ((ShovePart2BasePage)this.Page).UserID;
                }

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
        /// ���ڵ�ҳ��
        /// </summary>
        [Bindable(true), Category("��Ϊ"), DefaultValue("Default"), Description("��ǰ�ؼ����ڵ�ҳ��")]
        public string PageName
        {
            get
            {
                if (this.Page is ShovePart2BasePage)
                {
                    return ((ShovePart2BasePage)this.Page).PageName;
                }

                object obj = this.ViewState["PageName"];

                try
                {
                    return Convert.ToString(obj);
                }
                catch
                {
                    return "Default";
                }
            }
            set
            {
                this.ViewState["PageName"] = value;
            }
        }

        /// <summary>
        /// �ؼ���ǰ��˳��
        /// </summary>
        [Bindable(true), Category("�ɷ�����"), DefaultValue(5000), Description("�ؼ����ֲ��,��ֵԽ�����Խ�ڶ���")]
        public int ZIndex
        {
            get
            {
                object obj = this.ViewState["ZIndex"];
                if (obj != null)
                {
                    return (int)obj;
                }
                else
                {
                    return 5000;
                }
            }

            set
            {
                this.ViewState["ZIndex"] = value;
            }
        }

        /// <summary>
        /// �ؼ��Ƿ��������������
        /// </summary>
        [Bindable(true), Category("�ɷ�����"), DefaultValue(false), Description("�ؼ��Ƿ��������������")]
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

        private short License
        {
            get
            {
                object obj = this.ViewState["License"];

                if (obj != null)
                {
                    return (short)obj;
                }
                else
                {
                    string NetCardMac = "";
                    object mac = HttpContext.Current.Session["NetCardMac"];

                    if (mac != null)
                    {
                        NetCardMac = mac.ToString();
                    }
                    else
                    {
                        NetCardMac = new SystemInformation().GetNetCardMACAddress();

                        HttpContext.Current.Session["NetCardMac"] = NetCardMac;
                    }

                    string FileName = this.Page.Server.MapPath(SupportDir + "/Data/ShovePart2.UserControls.ini");

                    if (String.IsNullOrEmpty(NetCardMac))
                    {
                        NetCardMac = "Default";
                    }

                    if (!System.IO.File.Exists(FileName))
                    {
                        this.ViewState["License"] = LicenseLevel.None;

                        return LicenseLevel.None;
                    }

                    IniFile ini = new IniFile(FileName);
                    string strEncrypt = ini.Read("Licenses", NetCardMac);

                    if (String.IsNullOrEmpty(strEncrypt))
                    {
                        strEncrypt = ini.Read("Licenses", "License");
                    }

                    if (String.IsNullOrEmpty(strEncrypt) || (strEncrypt.Length < 34))
                    {
                        this.ViewState["License"] = LicenseLevel.None;

                        return LicenseLevel.None;
                    }

                    string MD5 = strEncrypt.Substring(0, 32);
                    strEncrypt = strEncrypt.Substring(32);

                    if (MD5 != System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strEncrypt + "string FileName �� th��s.Page.�ӣ��v���.Ma���ath��SupportDir ��", "MD5"))
                    {
                        this.ViewState["License"] = LicenseLevel.None;

                        return LicenseLevel.None;
                    }

                    string strUnEncrypt = "";
                    try
                    {
                        strUnEncrypt = new Encrypt().UnEncryptString(strEncrypt);
                    }
                    catch { }

                    if (String.IsNullOrEmpty(strUnEncrypt) || (strUnEncrypt.Length < 32))
                    {
                        this.ViewState["License"] = LicenseLevel.None;

                        return LicenseLevel.None;
                    }

                    MD5 = strUnEncrypt.Substring(0, 32);
                    strUnEncrypt = strUnEncrypt.Substring(32);

                    if (MD5 != System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strUnEncrypt + "string FileName �� th��s.Page.�ӣ��v���.Ma���ath��SupportDir ��", "MD5"))
                    {
                        this.ViewState["License"] = LicenseLevel.None;

                        return LicenseLevel.None;
                    }

                    string[] strs = strUnEncrypt.Split(',');

                    if (strs.Length != 3)
                    {
                        this.ViewState["License"] = LicenseLevel.None;

                        return LicenseLevel.None;
                    }

                    if (NetCardMac != strs[1])
                    {
                        this.ViewState["License"] = LicenseLevel.None;

                        return LicenseLevel.None;
                    }

                    short Level = LicenseLevel.None;

                    try
                    {
                        Level = short.Parse(strs[0]);
                    }
                    catch { }

                    if ((Level < LicenseLevel.None) || (Level > LicenseLevel.DesignOnly))
                    {
                        this.ViewState["License"] = LicenseLevel.None;

                        return LicenseLevel.None;
                    }

                    if (Level == LicenseLevel.Trial)
                    {
                        DateTime dt = DateTime.Now.AddDays(-30);

                        try
                        {
                            dt = DateTime.Parse(strs[2], new CultureInfo("zh-CN"));
                        }
                        catch { }

                        if (dt < DateTime.Now)
                        {
                            this.ViewState["License"] = LicenseLevel.None;

                            return LicenseLevel.None;
                        }
                    }

                    this.ViewState["License"] = Level;

                    return Level;
                }
            }
        }

        /// <summary>
        /// װ���û��ؼ�
        /// </summary>
        protected override void CreateChildControls()
        {
            if (AscxControlFileName != "")
            {
                Control c = this.Page.LoadControl(AscxControlFileName);

                if (c != null)
                {
                    if (c is ShovePart2UserControl)
                    {
                        ((ShovePart2UserControl)c).ControlAttributes = ControlAttributes;
                    }

                    this.Controls.Add(c);
                }
            }

            base.CreateChildControls();
        }

        /// <summary>
        /// ���ֿؼ�
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            if (License == LicenseLevel.None && (HttpContext.Current.Request.Url.AbsoluteUri).IndexOf("http://localhost:") < 0)
            {
                output.WriteLine("\n<!-- Shove.Web.UI.ShovePart2 Start -->");
                output.WriteLine("ShovePart2 ����ѹ��ڣ�����ϵ������<a href=\"" + SupportDir + "/Page/ShovePart2License.aspx?LastRequestPage=" + HttpUtility.UrlEncode(this.Page.Request.Url.AbsoluteUri) + "\">��ȡ���µ���Ȩ</a>��лл��<br />");
                output.WriteLine("<!-- Shove.Web.UI.ShovePart2 End -->");
                return;
            }

            #region ������ֱ���

            string RelativePath = PublicFunction.GetCurrentRelativePath();

            try
            {
                if (this.Page.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString().Trim().ToUpper() == "DESIGN")
                {
                    Mode = RunMode.Design;
                }
            }
            catch { }

            if (License == LicenseLevel.RunOnly)
            {
                Mode = RunMode.Run;
            }

            if (License == LicenseLevel.DesignOnly)
            {
                Mode = RunMode.Design;
            };

            // output.WriteLine("\n<!--" + VerticalAlign.ToString().ToLower() + " == " + (VerticalAlign.ToString().ToLower() == "notset").ToString() + " -->");
            #endregion

            bool _isDesigning = (Mode == RunMode.Design);

            output.WriteLine("<!-- Shove.Web.UI.ShovePart2 Start -->");
            output.AddAttribute(HtmlTextWriterAttribute.Class, "dragLayer");
            output.AddAttribute("ShoveWebUITypeName", "ShovePart2");

            if (_isDesigning)
            {
                output.AddAttribute("Lock", Lock.ToString());

                output.AddAttribute("ControlAttributes", ControlAttributes);

                if ((BorderStyle == BorderStyle.None) || (BorderStyle == BorderStyle.NotSet))
                {
                    BorderStyle = BorderStyle.Dotted;
                    BorderWidth = new Unit("1px");
                    BorderColor = Color.Gray;
                }
            }

            string strstyle = "height:100%; overflow:hidden;";

            strstyle += " width: 100%; z-index: " + ZIndex.ToString();

            output.AddAttribute(HtmlTextWriterAttribute.Style, strstyle);

            base.RenderBeginTag(output);

            if (_isDesigning)
            {
                RenderDesignToolBar(output, RelativePath);
            }

            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_Content");
            output.AddAttribute(HtmlTextWriterAttribute.Class, ControlCssClass);
            output.RenderBeginTag(HtmlTextWriterTag.Div);

            base.RenderChildren(output); //����ؼ�����

            output.RenderEndTag(); 
            base.RenderEndTag(output);

            output.WriteLine();
            output.Write("<!-- Shove.Web.UI.ShovePart2 End -->");
        }

        private void RenderDesignToolBar(HtmlTextWriter output, string RelativePath)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Class, "dragHeader");
            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_divTitleBar");
            if (Lock)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Style, "top: 0px; left:0px;  background: black; width: 100%; height: 30px; color: white; filter:alpha(opacity=60);");
            }
            else
            {
                output.AddAttribute(HtmlTextWriterAttribute.Style, "top: 0px; left:0px; background: blue; width: 100%; height: 30px; color: white; filter:alpha(opacity=40);");
            }

            output.RenderBeginTag(HtmlTextWriterTag.Div);


            output.AddAttribute(HtmlTextWriterAttribute.Style, "float:left");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.Write(Title);
            output.RenderEndTag();


            output.AddAttribute(HtmlTextWriterAttribute.Style, "float:right");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.AddAttribute("class", "min");
            output.RenderBeginTag(HtmlTextWriterTag.Span);

            output.Write("\t");

            output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/Lock.gif");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
            output.AddAttribute(HtmlTextWriterAttribute.Title, Lock ? "��������˴���" : "�����˴���");
            output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShovePart2_OnLockClick(event, this.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "', this, this.parentNode.parentNode.parentNode)");
            output.RenderBeginTag(HtmlTextWriterTag.Img);

            output.RenderEndTag();
            output.WriteLine("&nbsp;");

            output.Write("\t");

            //output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/ApplyToAll.gif");
            //output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
            //output.AddAttribute(HtmlTextWriterAttribute.Title, "���� Part ����Ӧ�õ���������ҳ");
            //output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            //output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShovePart2_OnApplyToAllClick(event,this.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "')");
            //output.RenderBeginTag(HtmlTextWriterTag.Img);
            //output.RenderEndTag();
            //output.WriteLine();

            //output.Write("\t");

            output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/Edit.gif");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
            output.AddAttribute(HtmlTextWriterAttribute.Title, "�༭�˴�������");
            output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            output.AddAttribute(HtmlTextWriterAttribute.Onclick, "if (ShoveWebUI_ShovePart2_OnEditClick(event, this.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "'," + UserID.ToString() + ", '" + RelativePath + "', '" + SupportDir + "/Page/ShovePart2Attributes.aspx', '" + AscxControlFileName + "','" + ControlAttributes + "')) { " + this.Page.ClientScript.GetPostBackEventReference(this, "OnAttributeChanged") + "; }");
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();
            output.WriteLine("&nbsp;");

            output.Write("\t");

            output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/Close.gif");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
            output.AddAttribute(HtmlTextWriterAttribute.Title, "ɾ���˴���");
            output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShovePart2_OnCloseClick(event, this.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "', '" + CloseConfirmText + "')");
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();

            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

            output.WriteLine();
        }
    }
}
