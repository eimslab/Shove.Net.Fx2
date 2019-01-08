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
[assembly: WebResource("Shove.Web.UI.Script.ShoveWebPart.js", "application/javascript")]
[assembly: WebResource("Shove.Web.UI.Script.ShoveWebPartDesigning.js", "application/javascript")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPart ��ҳ�����������
    /// </summary>
    [Designer(typeof(System.Web.UI.Design.ContainerControlDesigner)), ParseChildren(false), PersistChildren(true)]
    [DefaultProperty("AscxControlFileName"), ToolboxData("<{0}:ShoveWebPart runat=server></{0}:ShoveWebPart>")]
    public class ShoveWebPart : Panel, INamingContainer
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
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPart", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Shove.Web.UI.Script.ShoveWebPart.js"));

            bool _isDesigning = (Mode == RunMode.Design);

            if (_isDesigning)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ShoveWebPart));

                this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPartDesigning", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Shove.Web.UI.Script.ShoveWebPartDesigning.js"));
            }

            ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
            int RowNo = -1;
            DataTable dtParts = swpf.Read(null, ID, ref RowNo);

            if (RowNo < 0)
            {
                DataRow dr = dtParts.NewRow();

                dr["id"] = ID;
                dr["Visable"] = Visible.ToString();
                dr["Left"] = Left.ToString();
                dr["Top"] = Top.ToString();
                dr["Width"] = Width.ToString();
                dr["Height"] = Height.ToString();
                //---------------
                dr["float"] = Float.ToString();
                dr["MarginVertical"] = MarginVertical.ToString();
                dr["MarginLeftOrRight"] = MarginLeftOrRight.ToString();
                //---------------
                dr["PrimitiveHeight"] = PrimitiveHeight.ToString();
                dr["AscxControlFileName"] = AscxControlFileName;
                dr["HorizontalAlign"] = HorizontalAlign.ToString();
                dr["VerticalAlign"] = VerticalAlign.ToString();
                dr["BorderStyle"] = BorderStyle.ToString();
                dr["BorderWidth"] = BorderWidth.ToString();
                dr["BorderColor"] = BorderColor.Name;
                dr["BackColor"] = BackColor.Name;
                dr["TitleImageUrl"] = TitleImageUrl;
                dr["BackImageUrl"] = BackImageUrl;
                dr["BottomImageUrl"] = BottomImageUrl;
                dr["AutoHeight"] = AutoHeight.ToString();
                dr["ZIndex"] = ZIndex.ToString();
                dr["ParentLeft"] = ParentLeft.ToString();
                dr["CssClass"] = CssClass;
                dr["TopUpLimit"] = TopUpLimit.ToString();
                dr["TitleImageUrlLink"] = TitleImageUrlLink;
                dr["BottomImageUrlLink"] = BottomImageUrlLink;
                dr["TitleImageUrlLinkTarget"] = TitleImageUrlLinkTarget;
                dr["BottomImageUrlLinkTarget"] = BottomImageUrlLinkTarget;
                dr["ControlAttributes"] = ControlAttributes.ToString();
                dr["Lock"] = Lock.ToString();

                dtParts.Rows.Add(dr);
                swpf.Write(dtParts, null);
            }
            else
            {
                DataRow dr = dtParts.Rows[RowNo];

                Visible = (dr["Visable"].ToString() == "True");
                try { Left = new Unit(dr["Left"].ToString()); }
                catch { }
                try { Top = new Unit(dr["Top"].ToString()); }
                catch { }
                try { Width = new Unit(dr["Width"].ToString()); }
                catch { }
                try { Height = new Unit(dr["Height"].ToString()); }
                catch { }
                //--------------------------
                try
                {
                    Float = dr["float"].ToString();
                }
                catch { }
                try
                {
                    MarginVertical = new Unit(dr["MarginVertical"].ToString());
                }
                catch { }
                try { MarginLeftOrRight = new Unit(dr["MarginLeftOrRight"].ToString()); }
                catch { }
                //-------------------------------
                try { PrimitiveHeight = new Unit(dr["PrimitiveHeight"].ToString()); }
                catch { }
                AscxControlFileName = dr["AscxControlFileName"].ToString();
                HorizontalAlign = PublicFunction.HorizontalAlignFromString(dr["HorizontalAlign"].ToString());
                VerticalAlign = PublicFunction.VerticalAlignFromString(dr["VerticalAlign"].ToString());
                BorderStyle = PublicFunction.BorderStyleFromString(dr["BorderStyle"].ToString());
                try { BorderWidth = new Unit(dr["BorderWidth"].ToString()); }
                catch { }
                try { BorderColor = System.Drawing.Color.FromName(dr["BorderColor"].ToString()); }
                catch { }
                try { BackColor = System.Drawing.Color.FromName(dr["BackColor"].ToString()); }
                catch { }
                TitleImageUrl = dr["TitleImageUrl"].ToString();
                BackImageUrl = dr["BackImageUrl"].ToString();
                BottomImageUrl = dr["BottomImageUrl"].ToString();
                try { AutoHeight = bool.Parse(dr["AutoHeight"].ToString()); }
                catch { }
                try { ZIndex = int.Parse(dr["ZIndex"].ToString()); }
                catch { }
                try { ParentLeft = new Unit(dr["ParentLeft"].ToString()); }
                catch { }
                CssClass = dr["CssClass"].ToString();
                try { TopUpLimit = bool.Parse(dr["TopUpLimit"].ToString()); }
                catch { }
                TitleImageUrlLink = dr["TitleImageUrlLink"].ToString();
                BottomImageUrlLink = dr["BottomImageUrlLink"].ToString();
                TitleImageUrlLinkTarget = dr["TitleImageUrlLinkTarget"].ToString();
                BottomImageUrlLinkTarget = dr["BottomImageUrlLinkTarget"].ToString();
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
            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
            int RowNo = -1;
            DataTable dtParts = swpf.Read(null, id, ref RowNo);
            DataRow dr = null;

            if (RowNo < 0)
            {
                dr = dtParts.NewRow();

                dr["id"] = id;
                dr["AscxControlFileName"] = AscxControlFileName;
                dr["HorizontalAlign"] = HorizontalAlign.ToString();
                dr["VerticalAlign"] = VerticalAlign.ToString();
                dr["BorderStyle"] = BorderStyle.ToString();
                dr["BorderWidth"] = BorderWidth.ToString();
                dr["BorderColor"] = BorderColor.Name;
                dr["BackColor"] = BackColor.Name;
                dr["TitleImageUrl"] = TitleImageUrl;
                dr["BackImageUrl"] = BackgroundImageUrl;//BackImageUrl;
                dr["BottomImageUrl"] = BottomImageUrl;
                dr["AutoHeight"] = AutoHeight.ToString();
                dr["CssClass"] = ControlCssClass;//CssClass;
                dr["TopUpLimit"] = TopUpLimit.ToString();
                dr["TitleImageUrlLink"] = TitleImageUrlLink;
                dr["BottomImageUrlLink"] = BottomImageUrlLink;
                dr["TitleImageUrlLinkTarget"] = TitleImageUrlLinkTarget;
                dr["BottomImageUrlLinkTarget"] = BottomImageUrlLinkTarget;
                dr["ControlAttributes"] = ControlAttributes;

                dtParts.Rows.Add(dr);
            }

            dr = dtParts.Rows[RowNo];

            dr["Visable"] = "True";
            dr["Left"] = left;
            dr["Top"] = top;
            dr["Width"] = width;
            dr["Height"] = height;
            dr["PrimitiveHeight"] = primitiveheight;
            dr["ParentLeft"] = parentleft;

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
            string _HorizontalAlign = AttributesList[1];
            string _VerticalAlign = AttributesList[2];

            string _BorderStyle = AttributesList[3];
            string _BorderWidth = AttributesList[4];
            string _BorderColor = AttributesList[5];
            string _BackColor = AttributesList[6];
            string _TitleImageUrl = AttributesList[7];
            string _BackImageUrl = AttributesList[8];
            string _BottomImageUrl = AttributesList[9];
            string _AutoHeight = AttributesList[10];
            string _Left = AttributesList[11];
            string _Top = AttributesList[12];
            string _Width = AttributesList[13];
            string _Height = AttributesList[14];
            string _PrimitiveHeight = AttributesList[15];
            string _CssClass = AttributesList[16];
            string _TopUpLimit = AttributesList[17];
            string _TitleImageUrlLink = AttributesList[18];
            string _BottomImageUrlLink = AttributesList[19];
            string _TitleImageUrlLinkTarget = AttributesList[20];
            string _BottomImageUrlLinkTarget = AttributesList[21];
            string _ControlAttributes = AttributesList[22];

            bool _ApplyToAllPage = false;
            try
            {
                _ApplyToAllPage = bool.Parse(AttributesList[23]);
            }
            catch { }

            bool _AddToNoExistPage = false;
            try
            {
                _AddToNoExistPage = bool.Parse(AttributesList[24]);
            }
            catch { }
            //---------------------
            string _Float = AttributesList[25];
            string _MarginLeftOrRight = AttributesList[26];
            string _MarginVertical = AttributesList[27];

            //--------------------

            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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
            dr["HorizontalAlign"] = _HorizontalAlign;
            dr["VerticalAlign"] = _VerticalAlign;
            dr["BorderStyle"] = _BorderStyle;
            dr["BorderWidth"] = _BorderWidth;
            dr["BorderColor"] = _BorderColor;
            dr["BackColor"] = _BackColor;
            dr["TitleImageUrl"] = _TitleImageUrl;
            dr["BackImageUrl"] = _BackImageUrl;
            dr["BottomImageUrl"] = _BottomImageUrl;
            dr["AutoHeight"] = _AutoHeight;
            dr["Left"] = _Left;
            dr["Top"] = _Top;
            dr["Width"] = _Width;
            dr["Height"] = _Height;
            //-------------------------
            dr["Float"] = _Float;
            dr["MarginLeftOrRight"] = _MarginLeftOrRight;
            dr["MarginVertical"] = _MarginVertical;
            //------------------------
            dr["PrimitiveHeight"] = _PrimitiveHeight;
            dr["CssClass"] = _CssClass;
            dr["TopUpLimit"] = _TopUpLimit;
            dr["TitleImageUrlLink"] = _TitleImageUrlLink;
            dr["BottomImageUrlLink"] = _BottomImageUrlLink;
            dr["TitleImageUrlLinkTarget"] = _TitleImageUrlLinkTarget;
            dr["BottomImageUrlLinkTarget"] = _BottomImageUrlLinkTarget;
            dr["ControlAttributes"] = _ControlAttributes;

            dtParts.AcceptChanges();
            swpf.Write(dtParts, null);

            Visible = true;
            AscxControlFileName = _AscxControlFileName;
            HorizontalAlign = PublicFunction.HorizontalAlignFromString(_HorizontalAlign);
            VerticalAlign = PublicFunction.VerticalAlignFromString(_VerticalAlign);
            BorderStyle = PublicFunction.BorderStyleFromString(_BorderStyle);
            try { BorderWidth = new Unit(_BorderWidth); }
            catch { }
            try { BorderColor = System.Drawing.Color.FromName(_BorderColor); }
            catch { }
            try { BackColor = System.Drawing.Color.FromName(_BackColor); }
            catch { }
            TitleImageUrl = _TitleImageUrl;
            BackImageUrl = _BackImageUrl;
            BottomImageUrl = _BottomImageUrl;
            try { AutoHeight = bool.Parse(_AutoHeight); }
            catch { }
            try { Left = new Unit(_Left); }
            catch { }
            try { Top = new Unit(_Top); }
            catch { }
            try { Width = new Unit(_Width); }
            catch { }
            try { Height = new Unit(_Height); }
            catch { }
            //---------------------
            Float = _Float;
            try
            {
                MarginLeftOrRight = new Unit(_MarginLeftOrRight);
            }
            catch { }
            try
            {
                MarginVertical = new Unit(_MarginVertical);
            }
            catch
            { }

            //------------------
            try { PrimitiveHeight = new Unit(_PrimitiveHeight); }
            catch { }
            CssClass = _CssClass;
            try { TopUpLimit = bool.Parse(_TopUpLimit); }
            catch { }
            TitleImageUrlLink = _TitleImageUrlLink;
            BottomImageUrlLink = _BottomImageUrlLink;
            TitleImageUrlLinkTarget = _TitleImageUrlLinkTarget;
            BottomImageUrlLinkTarget = _BottomImageUrlLinkTarget;
            ControlAttributes = _ControlAttributes;

            if (_ApplyToAllPage || _AddToNoExistPage)
            {
                SaveApplyToAll(id, siteid, pagename, _ApplyToAllPage, _AddToNoExistPage, _OldAttributes);
            }

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
            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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

            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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
            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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

            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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
            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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

            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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

            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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

            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
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

        [Bindable(true), Category("Appearance"), DefaultValue(typeof(BorderStyle), "Solid"), Description("�߿�����ʽ")]
        public override BorderStyle BorderStyle
        {
            get
            {
                object obj = this.ViewState["BorderStyle"];
                if (obj != null)
                {
                    return (BorderStyle)obj;
                }
                else
                {
                    return BorderStyle.None;//.Solid;
                }
            }

            set
            {
                this.ViewState["BorderStyle"] = value;
            }
        }

        [Bindable(false), Category("Appearance"), DefaultValue(typeof(Unit), "1px"), Description("�߿��߿��")]
        public override Unit BorderWidth
        {
            get
            {
                object obj = this.ViewState["BorderWidth"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("1px");
                }
            }

            set
            {
                this.ViewState["BorderWidth"] = value;
            }
        }

        [Bindable(false), Category("Appearance"), DefaultValue(typeof(Color), "Black"), Description("�߿�����ɫ")]
        public override Color BorderColor
        {
            get
            {
                object obj = this.ViewState["BorderColor"];
                if (obj != null)
                {
                    return (Color)obj;
                }
                else
                {
                    return Color.Black;
                }
            }

            set
            {
                this.ViewState["BorderColor"] = value;
            }
        }

        [Bindable(false), Category("Appearance"), DefaultValue(typeof(Color), "White"), Description("������ɫ")]
        public override Color BackColor
        {
            get
            {
                object obj = this.ViewState["BackColor"];
                if (obj != null)
                {
                    return (Color)obj;
                }
                else
                {
                    return Color.Empty;// "";//Color.White;
                }
            }

            set
            {
                this.ViewState["BackColor"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("����ͼƬ")]
        public override string BackImageUrl
        {
            get
            {
                //object obj = this.ViewState["BackImageUrl"];
                //if (obj != null)
                //{
                //    return (string)obj;
                //}
                return "";
            }

            set
            {
                this.ViewState["BackImageUrl"] = value;

                BackgroundImageUrl = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("����ͼƬ(������������)")]
        public string BackgroundImageUrl
        {
            get
            {
                object obj = this.ViewState["BackgroundImageUrl"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }

            set
            {
                this.ViewState["BackgroundImageUrl"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("����ͼƬ")]
        public string TitleImageUrl
        {
            get
            {
                object obj = this.ViewState["TitleImageUrl"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }

            set
            {
                this.ViewState["TitleImageUrl"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("����ͼƬ���ӵ�ַ")]
        public string TitleImageUrlLink
        {
            get
            {
                object obj = this.ViewState["TitleImageUrlLink"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }

            set
            {
                this.ViewState["TitleImageUrlLink"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("����ͼƬ����Ŀ��")]
        public string TitleImageUrlLinkTarget
        {
            get
            {
                object obj = this.ViewState["TitleImageUrlLinkTarget"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }

            set
            {
                this.ViewState["TitleImageUrlLinkTarget"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("�ײ�ͼƬ")]
        public string BottomImageUrl
        {
            get
            {
                object obj = this.ViewState["BottomImageUrl"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }

            set
            {
                this.ViewState["BottomImageUrl"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("�ײ�ͼƬ���ӵ�ַ")]
        public string BottomImageUrlLink
        {
            get
            {
                object obj = this.ViewState["BottomImageUrlLink"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }

            set
            {
                this.ViewState["BottomImageUrlLink"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("�ײ�ͼƬ����Ŀ��")]
        public string BottomImageUrlLinkTarget
        {
            get
            {
                object obj = this.ViewState["BottomImageUrlLinkTarget"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "";
            }

            set
            {
                this.ViewState["BottomImageUrlLinkTarget"] = value;
            }
        }

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

        [Bindable(false), Category("����"), DefaultValue(typeof(Unit), "0px"), Description("���ؼ������꣬�����ݴ��������ʵ��������ƫ�ƶ�λ")]
        public Unit ParentLeft
        {
            get
            {
                object obj = this.ViewState["ParentLeft"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("0px");
                }
            }

            set
            {
                this.ViewState["ParentLeft"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(typeof(Unit), "0px"), Description("�ؼ�������")]
        public Unit Left
        {
            get
            {
                object obj = this.ViewState["Left"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("300px");
                }
            }

            set
            {
                this.ViewState["Left"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(typeof(Unit), "0px"), Description("�ؼ�������")]
        public Unit Top
        {
            get
            {
                object obj = this.ViewState["Top"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("100px");
                }
            }

            set
            {
                this.ViewState["Top"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(typeof(Unit), "400px"), Description("�ؼ����")]
        public override Unit Width
        {
            get
            {
                object obj = this.ViewState["Width"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("400px");
                }
            }

            set
            {
                this.ViewState["Width"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(typeof(Unit), "300px"), Description("�ؼ��߶�")]
        public override Unit Height
        {
            get
            {
                object obj = this.ViewState["Height"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("300px");
                }
            }

            set
            {
                this.ViewState["Height"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue("none"), Description("�ؼ���������")]
        public string Float
        {
            get
            {
                object obj = this.ViewState["Float"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return "none";
            }

            set
            {
                this.ViewState["Float"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(typeof(Unit), "0px"), Description("�ؼ�����ƫ����")]
        public Unit MarginVertical
        {
            get
            {
                object obj = this.ViewState["MarginVertical"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("0px");
                }
            }

            set
            {
                this.ViewState["MarginVertical"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(typeof(Unit), "0px"), Description("�ؼ�ˮƽƫ����")]
        public Unit MarginLeftOrRight
        {
            get
            {
                object obj = this.ViewState["MarginLeftOrRight"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("0px");
                }
            }

            set
            {
                this.ViewState["MarginLeftOrRight"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(typeof(Unit), "300px"), Description("�ؼ��Զ��߶�ʱԭʼ�߶�")]
        public Unit PrimitiveHeight
        {
            get
            {
                object obj = this.ViewState["PrimitiveHeight"];
                if (obj != null)
                {
                    return (Unit)obj;
                }
                else
                {
                    return new Unit("300px");
                }
            }

            set
            {
                this.ViewState["PrimitiveHeight"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(false), Description("�ؼ�����Ӧ�߶�")]
        public bool AutoHeight
        {
            get
            {
                object obj = this.ViewState["AutoHeight"];
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
                this.ViewState["AutoHeight"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(false), Description("�ؼ�����������Ӧ�߶ȿؼ�Ӱ��ʱ��Top �Ƿ���������")]
        public bool TopUpLimit
        {
            get
            {
                object obj = this.ViewState["TopUpLimit"];
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
                this.ViewState["TopUpLimit"] = value;
            }
        }

        [Bindable(false), Category("����"), DefaultValue(VerticalAlign.NotSet), Description("���ݴ�ֱ����")]
        public VerticalAlign VerticalAlign
        {
            get
            {
                object obj = this.ViewState["VerticalAlign"];


                if (obj != null)
                {

                    return (VerticalAlign)obj;
                }
                else
                {

                    return VerticalAlign.NotSet;
                }
            }
            set
            {
                this.ViewState["VerticalAlign"] = value;
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
                if (this.Page is ShoveWebPartBasePage)
                {
                    return ((ShoveWebPartBasePage)this.Page).IsDesigning ? RunMode.Design : RunMode.Run;
                }

                if ((this.Page.Session["Shove.Web.UI.ShoveWebPart.RunMode"] != null) && (this.Page.Session["Shove.Web.UI.ShoveWebPart.RunMode"].ToString().Trim().ToUpper() == "DESIGN"))
                {
                    return RunMode.Design;
                }

                return RunMode.Run;
            }
            set
            {
                if (value == ShoveWebPart.RunMode.Design)
                {
                    this.Page.Session["Shove.Web.UI.ShoveWebPart.RunMode"] = "DESIGN";
                }
                else
                {
                    this.Page.Session.Remove("Shove.Web.UI.ShoveWebPart.RunMode");
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

        [Bindable(true), Category("��Ϊ"), DefaultValue("~/Images/Upload/MultiSite"), Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("�ϴ���ͼƬ�ļ������·������·����ҪдȨ��")]
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
                    return "~/Images/Upload/MultiSite";
                }
            }

            set
            {
                this.ViewState["ImageUploadDir"] = value;
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
                if (this.Page is ShoveWebPartBasePage)
                {
                    return ((ShoveWebPartBasePage)this.Page).SiteID;
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
                if (this.Page is ShoveWebPartBasePage)
                {
                    return ((ShoveWebPartBasePage)this.Page).UserID;
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
                if (this.Page is ShoveWebPartBasePage)
                {
                    return ((ShoveWebPartBasePage)this.Page).PageName;
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

                    string FileName = this.Page.Server.MapPath(SupportDir + "/Data/ShoveWebPart.UserControls.ini");

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
                    if (c is ShoveWebPartUserControl)
                    {
                        ((ShoveWebPartUserControl)c).ControlAttributes = ControlAttributes;
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
                output.WriteLine("\n<!-- Shove.Web.UI.ShoveWebPart Start -->");
                output.WriteLine("ShoveWebPart ����ѹ��ڣ�����ϵ������<a href=\"" + SupportDir + "/Page/ShoveWebPartLicense.aspx?LastRequestPage=" + HttpUtility.UrlEncode(this.Page.Request.Url.AbsoluteUri) + "\">��ȡ���µ���Ȩ</a>��лл��<br />");
                output.WriteLine("<!-- Shove.Web.UI.ShoveWebPart End -->");

                return;
            }

            #region ������ֱ���

            string _TitleImageUrl = "";
            string _BottomImageUrl = "";
            string _TitleImageUrlLinkTarget = "_self";
            string _BottomImageUrlLinkTarget = "_self";


            string _ImageUploadDir = "";

            string RelativePath = PublicFunction.GetCurrentRelativePath();

            if (TitleImageUrl != "")
            {
                _TitleImageUrl = RelativePath + TitleImageUrl;
            }

            if (TitleImageUrlLinkTarget != "")
            {
                _TitleImageUrlLinkTarget = TitleImageUrlLinkTarget;
            }

            if (BackImageUrl != "")
            {
                BackImageUrl = RelativePath + BackImageUrl;
            }

            if (BottomImageUrl != "")
            {
                _BottomImageUrl = RelativePath + BottomImageUrl;
            }

            if (BottomImageUrlLinkTarget != "")
            {
                _BottomImageUrlLinkTarget = BottomImageUrlLinkTarget;
            }

            if ((!ImageUploadDir.EndsWith("/")) && (ImageUploadDir != ""))
            {
                _ImageUploadDir = ImageUploadDir + "/" + SiteID.ToString();
            }
            else
            {
                _ImageUploadDir = ImageUploadDir + SiteID.ToString();
            }

            try
            {
                if (this.Page.Session["Shove.Web.UI.ShoveWebPart.RunMode"].ToString().Trim().ToUpper() == "DESIGN")
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
            string _VerticalAlign = "vertical-align: " + (VerticalAlign.ToString().ToLower() == "notset" ? "none" : VerticalAlign.ToString().ToLower()) + "; ";

            #endregion

            bool _isDesigning = (Mode == RunMode.Design);

            output.WriteLine("\n<!-- Shove.Web.UI.ShoveWebPart Start -->");
            output.AddAttribute(HtmlTextWriterAttribute.Class, "dragLayer");
            output.AddAttribute("ShoveWebUITypeName", "ShoveWebPart");
            output.AddAttribute("OffsetLeft", ((ParentLeft.ToString() == "") ? "0px" : ParentLeft.ToString()));
            output.AddAttribute("PrimitiveHeight", ((PrimitiveHeight.ToString() == "") ? "0px" : PrimitiveHeight.ToString()));
            output.AddAttribute("TopUpLimit", TopUpLimit.ToString());

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

                //output.AddAttribute("onmousedown", "ShoveWebUI_ShoveWebPart_OnMouseDown(event, this, " + SiteID.ToString() + ", '" + PageName + "')");
                //output.AddAttribute("onmouseup", "ShoveWebUI_ShoveWebPart_OnMouseUp(event, this, " + SiteID.ToString() + ", '" + PageName + "')");
                //output.AddAttribute("onmousemove", "ShoveWebUI_ShoveWebPart_OnMouseMove(event, this, " + SiteID.ToString() + ", '" + PageName + "')");
            }

            string BC = (BackColor.Name == "Window") ? "none" : BackColor.Name;

            if (AutoHeight)
            {
                string strstyle = _VerticalAlign + "background-color: " + BC + "; float: " + Float + "; ";
                if (Float == "left")
                {
                    strstyle += "margin-left: " + MarginLeftOrRight + "; ";
                }
                else if (Float == "right")
                {
                    strstyle += "margin-right: " + MarginLeftOrRight + "; ";
                }
                else
                {
                    strstyle += "margin: 0px auto; ";
                }
                strstyle += "margin-top: " + MarginVertical + "; ";
                strstyle += "border: " + BorderWidth.ToString() + " " + BorderStyle.ToString() + " " + BorderColor.Name + "; width: " + Width.ToString() + "; height: auto; z-index: " + ZIndex.ToString();

                output.AddAttribute(HtmlTextWriterAttribute.Style, strstyle);
            }
            else
            {
                string strstyle = _VerticalAlign + "background-color: " + BC + "; float: " + Float + "; ";
                if (Float == "left")
                {
                    strstyle += "margin-left: " + MarginLeftOrRight + "; ";
                }
                else if (Float == "right")
                {
                    strstyle += "margin-right: " + MarginLeftOrRight + "; ";
                }
                else
                {
                    strstyle += "margin: 0px auto; ";
                }
                strstyle += "margin-top: " + MarginVertical + "; ";
                strstyle += "border: " + BorderWidth.ToString() + " " + BorderStyle.ToString() + " " + BorderColor.Name + "; width: " + Width.ToString() + "; height: " + Height.ToString() + "; z-index: " + ZIndex.ToString();

                output.AddAttribute(HtmlTextWriterAttribute.Style, strstyle);
            }

            base.RenderBeginTag(output);

            RenderOffsetLeft(output);

            if (_isDesigning)
            {
                RenderDesignToolBar(output, RelativePath, _ImageUploadDir);
            }


            output.AddAttribute(HtmlTextWriterAttribute.Class, "content");
            output.RenderBeginTag(HtmlTextWriterTag.Div);


            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_TableContent");
            output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%; height: auto; background-image:url(" + RelativePath + BackgroundImageUrl + ")");
            output.RenderBeginTag(HtmlTextWriterTag.Table);



            if (_TitleImageUrl == "")
            {
                output.AddAttribute(HtmlTextWriterAttribute.Style, "display: none;");
            }
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddAttribute(HtmlTextWriterAttribute.Valign, "top");

            output.RenderBeginTag(HtmlTextWriterTag.Td);

            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_TitleImageLink");
            if (TitleImageUrlLink != "")
            {
                output.AddAttribute(HtmlTextWriterAttribute.Href, TitleImageUrlLink);
            }
            //else
            //{
            //    output.AddAttribute(HtmlTextWriterAttribute.Href, "");
            //}

            output.AddAttribute(HtmlTextWriterAttribute.Target, _TitleImageUrlLinkTarget);
            output.RenderBeginTag(HtmlTextWriterTag.A);



            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_TitleImage");


            if (_TitleImageUrl != "")
            {
                output.AddAttribute(HtmlTextWriterAttribute.Src, _TitleImageUrl);
                output.AddAttribute(HtmlTextWriterAttribute.Style, "border: 0px; display: block;");

                //str += "<img id=\"" + this.UniqueID.Replace(':', '_') + "_TitleImage\" src=\"" + _TitleImageUrl + "\" alt=\"\" style=\"border: 0px; display: block;\" />";
            }
            else
            {

                //str += "<img id=\"" + this.UniqueID.Replace(':', '_') + "_TitleImage\" src=\"about:blank\" alt=\"\" style=\"display: none;\" />";
                output.AddAttribute(HtmlTextWriterAttribute.Src, "about:blank");
                output.AddAttribute(HtmlTextWriterAttribute.Style, "display: none;");
            }

            output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.WriteLine();



            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_Content");
            output.RenderBeginTag(HtmlTextWriterTag.Tr);

            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_ContentTD");
            output.AddAttribute(HtmlTextWriterAttribute.Class, ControlCssClass);

            //output.AddAttribute(HtmlTextWriterAttribute.Align, HorizontalAlign.ToString().ToLower());
            //output.AddAttribute(HtmlTextWriterAttribute.Valign, VerticalAlign.ToString().ToLower());
            output.AddAttribute(HtmlTextWriterAttribute.Style, "text-align: " + HorizontalAlign.ToString().ToLower() + "; vertical-align: " + VerticalAlign.ToString().ToLower() + ";");

            output.RenderBeginTag(HtmlTextWriterTag.Td);


            base.RenderChildren(output);

            output.Write("\t\t");
            output.RenderEndTag();
            output.RenderEndTag();
            output.WriteLine();

            // BottomImage Tr Td

            if (_BottomImageUrl == "")
            {
                output.AddAttribute(HtmlTextWriterAttribute.Style, "display:none;");
            }
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom");
            output.RenderBeginTag(HtmlTextWriterTag.Td);

            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_BottomImageLink");
            if (BottomImageUrlLink != "")
            {
                output.AddAttribute(HtmlTextWriterAttribute.Href, BottomImageUrlLink);

            }
            else
            {
                output.AddAttribute(HtmlTextWriterAttribute.Href, "");
            }

            output.AddAttribute(HtmlTextWriterAttribute.Target, _BottomImageUrlLinkTarget);
            output.RenderBeginTag(HtmlTextWriterTag.A);

            output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_BottomImage");
            if (_BottomImageUrl != "")
            {
                output.AddAttribute(HtmlTextWriterAttribute.Src, _BottomImageUrl);
                output.AddAttribute(HtmlTextWriterAttribute.Style, "border: 0px; display: block;");


            }
            else
            {
                output.AddAttribute(HtmlTextWriterAttribute.Src, "about:blank");
                output.AddAttribute(HtmlTextWriterAttribute.Style, "display: none;");
            }

            output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

            base.RenderEndTag(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveWebPart End -->");
        }

        private void RenderOffsetLeft(HtmlTextWriter output)
        {
            //output.AddAttribute("type", "text/javascript");
            //output.RenderBeginTag(HtmlTextWriterTag.Script);

            //output.Write("ShoveWebUI_ShoveWebPart_OffsetLeft(\"" + this.UniqueID.Replace(':', '_') + "\");");

            //output.RenderEndTag();
            //output.WriteLine();
        }

        //private void RenderDesignToolBar(HtmlTextWriter output, string RelativePath, string _ImageUploadDir)
        //{
        //    output.RenderBeginTag(HtmlTextWriterTag.Div);
        //    output.AddAttribute(HtmlTextWriterAttribute.Class, "dragHeader");
        //    output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_') + "_divTitleBar");

        //    if (Lock)
        //    {
        //       // output.AddAttribute(HtmlTextWriterAttribute.Style, "top: 0px; left:0px; position:absolute; background: black; width: 100%; height: 20px; color: white; filter:alpha(opacity=60);");
        //        output.AddAttribute(HtmlTextWriterAttribute.Style, "top: 0px; left:0px;  background: black; width: 100%; height: 20px; color: white; filter:alpha(opacity=60);");
        //    }
        //    else
        //    {
        //       // output.AddAttribute(HtmlTextWriterAttribute.Style, "top: 0px; left:0px; position:absolute; background: blue; width: 100%; height: 20px; color: white; filter:alpha(opacity=40);");
        //        output.AddAttribute(HtmlTextWriterAttribute.Style, "top: 0px; left:0px; background: blue; width: 100%; height: 20px; color: white; filter:alpha(opacity=40);");
        //    }

        //    output.RenderBeginTag(HtmlTextWriterTag.Div);
        //    output.AddAttribute("float", "left");
        //    output.Write(Title);
        //    output.RenderEndTag();
        //    output.WriteLine();
        //    output.RenderBeginTag(HtmlTextWriterTag.Div);
        //    //output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
        //    //output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
        //    //output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
        //    //output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 100%;");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Table);

        //    //output.AddAttribute(HtmlTextWriterAttribute.Style, "height: 20px;");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Tr);

        //    //output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 50%;");
        //    //output.AddAttribute(HtmlTextWriterAttribute.Align, "left");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Td);

        //    //output.Write(Title);

        //   // output.RenderEndTag();
        //   // output.WriteLine();
        //    output.AddAttribute("float", "right");
        //    //output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 50%;");
        //    //output.AddAttribute(HtmlTextWriterAttribute.Align, "right");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Td);
        //    //output.WriteLine();

        //    output.Write("\t");
        //    output.RenderBeginTag(HtmlTextWriterTag.Span);
        //    output.AddAttribute("class", "min");
        //    output.RenderBeginTag(HtmlTextWriterTag.Img);
        //    output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/Lock.gif");
        //    output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
        //    output.AddAttribute(HtmlTextWriterAttribute.Title, Lock ? "��������˴���" : "�����˴���");
        //    output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
        //    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShoveWebPart_OnLockClick(event, this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "', this, this.parentNode.parentNode.parentNode.parentNode.parentNode)");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Img);

        //    output.RenderEndTag();
        //    output.WriteLine("&nbsp;");

        //    output.Write("\t");
        //    output.RenderBeginTag(HtmlTextWriterTag.Img);
        //    output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/ApplyToAll.gif");
        //    output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
        //    output.AddAttribute(HtmlTextWriterAttribute.Title, "���� Part ����Ӧ�õ���������ҳ");
        //    output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
        //    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShoveWebPart_OnApplyToAllClick(event, this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "')");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Img);

        //    output.RenderEndTag();
        //    output.WriteLine("&nbsp;");

        //    output.Write("\t");
        //    output.RenderBeginTag(HtmlTextWriterTag.Img);
        //    output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/ToBackground.gif");
        //    output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
        //    output.AddAttribute(HtmlTextWriterAttribute.Title, "��Ϊ�ײ�");
        //    output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
        //    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShoveWebPart_OnToBackgroundClick(event, this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "')");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Img);

        //    output.RenderEndTag();
        //    output.WriteLine("&nbsp;");

        //    output.Write("\t");
        //    output.RenderBeginTag(HtmlTextWriterTag.Img);
        //    output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/Edit.gif");
        //    output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
        //    output.AddAttribute(HtmlTextWriterAttribute.Title, "�༭�˴�������");
        //    output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
        //    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "if (ShoveWebUI_ShoveWebPart_OnEditClick(event, this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "'," + UserID.ToString() + ", '" + RelativePath + "', '" + SupportDir + "/Page/ShoveWebPartAttributes.aspx', '" + _ImageUploadDir + "', '" + AscxControlFileName + "','" + ControlAttributes + "')) { " + this.Page.ClientScript.GetPostBackEventReference(this, "OnAttributeChanged") + "; }");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Img);

        //    output.RenderEndTag();
        //    output.WriteLine("&nbsp;");

        //    output.Write("\t");
        //    output.RenderBeginTag(HtmlTextWriterTag.Img);
        //    output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/Close.gif");
        //    output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
        //    output.AddAttribute(HtmlTextWriterAttribute.Title, "ɾ���˴���");
        //    output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
        //    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShoveWebPart_OnCloseClick(event, this.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "', '" + CloseConfirmText + "')");
        //    //output.RenderBeginTag(HtmlTextWriterTag.Img);

        //    output.RenderEndTag();
        //    //output.WriteLine("&nbsp;");

        //    output.RenderEndTag();
        //    output.RenderEndTag();
        //    output.RenderEndTag();
        //    output.RenderEndTag();
        //    output.WriteLine();
        //}

        private void RenderDesignToolBar(HtmlTextWriter output, string RelativePath, string _ImageUploadDir)
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
            output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShoveWebPart_OnLockClick(event, this.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "', this, this.parentNode.parentNode.parentNode)");
            output.RenderBeginTag(HtmlTextWriterTag.Img);

            output.RenderEndTag();

            output.Write("\t");

            output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/ApplyToAll.gif");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
            output.AddAttribute(HtmlTextWriterAttribute.Title, "���� Part ����Ӧ�õ���������ҳ");
            output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShoveWebPart_OnApplyToAllClick(event,this.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "')");
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();
            output.WriteLine();

            output.Write("\t");

            output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/Edit.gif");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
            output.AddAttribute(HtmlTextWriterAttribute.Title, "�༭�˴�������");
            output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            output.AddAttribute(HtmlTextWriterAttribute.Onclick, "if (ShoveWebUI_ShoveWebPart_OnEditClick(event, this.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "'," + UserID.ToString() + ", '" + RelativePath + "', '" + SupportDir + "/Page/ShoveWebPartAttributes.aspx', '" + _ImageUploadDir + "', '" + AscxControlFileName + "','" + ControlAttributes + "')) { " + this.Page.ClientScript.GetPostBackEventReference(this, "OnAttributeChanged") + "; }");
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();
            output.WriteLine("&nbsp;");

            output.Write("\t");

            output.AddAttribute(HtmlTextWriterAttribute.Src, SupportDir + "/Images/Close.gif");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "cursor: hand; vertical-align: middle;");
            output.AddAttribute(HtmlTextWriterAttribute.Title, "ɾ���˴���");
            output.AddAttribute(HtmlTextWriterAttribute.Alt, "");
            output.AddAttribute(HtmlTextWriterAttribute.Onclick, "ShoveWebUI_ShoveWebPart_OnCloseClick(event, this.parentNode.parentNode.parentNode.parentNode, " + SiteID.ToString() + ", '" + PageName + "', '" + CloseConfirmText + "')");
            output.RenderBeginTag(HtmlTextWriterTag.Img);
            output.RenderEndTag();

            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();

            output.WriteLine();
        }
    }
}
