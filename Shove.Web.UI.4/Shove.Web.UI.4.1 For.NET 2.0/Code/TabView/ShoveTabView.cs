using System;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
//[assembly: WebResource("Shove.Web.UI.Script.ShoveTabView.js", "application/javascript")]
namespace Shove.Web.UI
{
    [ToolboxData(@"<{0}:ShoveTabView runat=server></{0}:ShoveTabView>"), ParseChildren(true, "Tabs"), PersistChildren(false), Designer(typeof(ShoveTabViewDesigner))]
    public class ShoveTabView : CompositeControl
    {
        public ShoveTabView()
        {
            this.Width = new Unit("400px");
            this.Height = new Unit("300px");
        }

        //private StringCssStyleCollection selectedTabStyle;
        //private StringCssStyleCollection unSelectedTabStyle;
        #region declarations

        private ShoveTabPageCollection tabs;
        private readonly object TabSelectionChangingObject = new object();
        public delegate void ShoveTabSelectionChangingHandler(object sender, ShoveTabSelectionChangingEventArgs e);


        #endregion

        #region overrides

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            Table tbl = new Table();
            string js = "";
            tbl.CellPadding = tbl.CellSpacing = 0;

            //include java script
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveTabView", "ShoveWebUI_client/Script/ShoveTabView.js");

            if (Tabs.Count > 0)
            {
                //create tab headers.
                js = CreateTabHeaders(ref tbl);
                this.Page.ClientScript.RegisterArrayDeclaration("tabButtons_" + this.ClientID, js);

                //create tab contents.        
                js = CreateTabContents(ref tbl);
                this.Page.ClientScript.RegisterArrayDeclaration("tabContents_" + this.ClientID, js);

                //when the post back is performed than select the current tab.
                //During designer editing Page.Request is null therefore we have to
                //check for current Http context.
                if (!this.DesignMode /*HttpContext.Current != null*/)
                {
                    if (!AutoPostBack)
                    {
                        if (this.Page.Request[this.ClientID + "$hf"] != null)
                            CurrentTabIndex = int.Parse(this.Page.Request[this.ClientID + "$hf"]);

                        SelectTab();
                    }
                    else
                    {
                        //if (ViewState["Loaded"] == null)
                        //{
                        //  ViewState["Loaded"] = 1;
                        SelectTab();
                        //}
                    }

                    //SelectTab();
                }

            }


            this.Controls.Add(tbl);

        }

        #endregion

        #region helper functions

        private void SelectTab()
        {
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(),
                 "_SelectTab", "<script language='JavaScript'>SelectTab(" + CurrentTabIndex +
                 ",'" + this.ClientID + "','" + this.ClientID + "_hf" + "','" +
                 UnSelectedTabCSSClass + "','" + SelectedTabCSSClass + "')" + "</script>");
        }

        /// <summary>
        /// Create Tab headers
        /// </summary>
        /// <param name="tbl">Parent table control whole alignmnet of control</param>
        /// <returns></returns>
        private string CreateTabHeaders(ref Table tbl)
        {
            TableRow tr;
            TableCell tc;
            LinkButton lk;
            StringBuilder arrBtns = new StringBuilder();//contains header js
            string lkId;
            int i = 0;
            //hidden field used to store the current tab index(for java scripting)
            HiddenField hf = new HiddenField();
            hf.ID = "hf";           

            Table tblTabs = new Table();
            tblTabs.CellPadding = tblTabs.CellSpacing = 0;
            tr = new TableRow();

            VerifyTabIndex();

            foreach (ShoveTabPage tp in Tabs)
            {
                tc = new TableCell();
                lk = new LinkButton();
                lk.ID = "tp" + i.ToString();
                lk.Text = "&nbsp;" + tp.Text + "&nbsp;";
                lk.CommandArgument = i.ToString();
                if (AutoPostBack) lk.Click += new EventHandler(lk_Click);

                lkId = this.ClientID + "_" + lk.ID;

                tc.ID = lk.ID + "_Parent" + i;

                arrBtns.AppendFormat("\"{0}\"{1}", lkId, (i == Tabs.Count - 1 ? "" : ","));

                if (!AutoPostBack)
                    lk.OnClientClick = "return OnTabClick(this," + i.ToString() + ",'" +
                      this.ClientID + "','" + this.ClientID + "_" + hf.ClientID + "','" +
                      UnSelectedTabCSSClass + "','" + SelectedTabCSSClass + "');";


                tc.Controls.Add(lk);
                tr.Cells.Add(tc);
                ++i;
            }
            tblTabs.Rows.Add(tr);


            tc = new TableCell();
            tc.ID = "TabHeader";
            tc.Style.Add("text-align", "left");
            tc.Controls.Add(tblTabs);
            tc.Controls.Add(hf);

            tr = new TableRow();
            tr.Cells.Add(tc);


            tbl.Rows.Add(tr);
            return arrBtns.ToString();
        }

        /// <summary>
        /// create tab contents.
        /// </summary>
        /// <param name="tbl">parent table refrence.</param>
        /// <returns></returns>
        private string CreateTabContents(ref Table tbl)
        {
            TableRow tr;
            TableCell tc;
            int i = 0;
            string tpId;
            StringBuilder arrTabPages = new StringBuilder();

            Table tblContents = new Table();
            tblContents.CellPadding = tblContents.CellSpacing = 0;

            tr = new TableRow();
            tc = new TableCell();
            //int i = -1;
            foreach (ShoveTabPage tp in Tabs)
            {
                tpId = this.ClientID + "_" + tp.ID;
                tc.Controls.Add(tp);
                if (i == tabs.Count - 1)
                    arrTabPages.AppendFormat("\"{0}\"", tpId);
                else
                    arrTabPages.AppendFormat("\"{0}\",", tpId);
                ++i;

            }
            tr.Cells.Add(tc);
            tblContents.Rows.Add(tr);

            tc = new TableCell();
            tc.Controls.Add(tblContents);
            tr = new TableRow();
            tr.Cells.Add(tc);

            tbl.Rows.Add(tr);
            return arrTabPages.ToString();
        }

        void lk_Click(object sender, EventArgs e)
        {

            LinkButton lk = (LinkButton)sender;

            this.CurrentTabIndex = Convert.ToInt32(lk.CommandArgument);


            //select the current tab.
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(),
                "SelectTab_LinkButton", "<script language='JavaScript'>SelectTab(" + CurrentTabIndex +
                ",'" + this.ClientID + "','" + this.ClientID + "_hf" + "','" +
                UnSelectedTabCSSClass + "','" + SelectedTabCSSClass + "')" + "</script>");
        }

        private void VerifyTabIndex()
        {
            if (CurrentTabIndex >= Tabs.Count)
                throw new Exception("Invalid Tab Index");
        }

        //private string GetTabButtonBorderStyle()
        //{
        //  string btnStyle = "";
        //  string tabButtonBorderColor = TabButtonBorderColor.ToKnownColor().ToString();

        //  //"border-right: skyblue 1px outset; border-top: skyblue 1px outset; border-left: 
        //  //skyblue 1px outset; border-bottom: skyblue 1px outset"
        //  btnStyle += string.Format("border-right:{0} 1px outset;", tabButtonBorderColor);
        //  btnStyle += string.Format("border-top:{0} 1px outset;", tabButtonBorderColor);
        //  btnStyle += string.Format("border-left:{0} 1px outset;", tabButtonBorderColor);
        //  btnStyle += string.Format("border-bottom:{0} 1px outset;", tabButtonBorderColor);

        //  return btnStyle;
        //}

        #endregion

        #region properties

        /// <summary>
        /// Collection of Tabs
        /// </summary>
        [
          PersistenceMode(PersistenceMode.InnerDefaultProperty),
          DefaultValue(null),
          Browsable(false)
        ]
        public virtual ShoveTabPageCollection Tabs
        {
            get
            {
                if (tabs == null)
                {
                    tabs = new ShoveTabPageCollection(this);
                }

                return tabs;
            }

        }

        /// <summary>
        /// Whether to auto post back the tab page or not.
        /// </summary>
        [
          DefaultValue(false)
        ]
        public bool AutoPostBack
        {
            set
            {
                this.ViewState["AutoPostBack"] = value;
            }
            get
            {
                object val = this.ViewState["AutoPostBack"];
                if (val == null) return false;
                return Convert.ToBoolean(val);
            }
        }

        //public Color TabButtonBorderColor
        //{
        //  get
        //  {
        //    object val = this.ViewState["TabButtonBorderColor"];
        //    if (val == null) return Color.FromName("#d4d0c8");
        //    return (Color)val;        
        //  }
        //  set
        //  {
        //    this.ViewState["TabButtonBorderColor"] = value;
        //  }
        //}

        //public void Refresh()
        //{
        //  CreateChildControls();
        //}

        public int CurrentTabIndex
        {
            get
            {
                object val = this.ViewState["CurrentTabIndex"];
                if (val == null) return 0;


                return Convert.ToInt32(val);
            }
            set
            {
                object val = ViewState["CurrentTabIndex"];
                if (val != null)
                {
                    int oldIndex = Convert.ToInt32(val);
                    int newIndex = Convert.ToInt32(value);

                    if (oldIndex != newIndex)
                    {
                        ShoveTabSelectionChangingEventArgs e = new ShoveTabSelectionChangingEventArgs(oldIndex, newIndex);

                        ShoveTabSelectionChangingHandler handler = (ShoveTabSelectionChangingHandler)Events[TabSelectionChangingObject];
                        if (handler != null)
                            handler(this, e);

                    }

                }

                this.ViewState["CurrentTabIndex"] = value;
            }
        }

        public string SelectedTabCSSClass
        {
            set
            {
                this.ViewState["SelectedTabCSSClass"] = value;
            }
            get
            {
                object val = this.ViewState["SelectedTabCSSClass"];
                if (val == null) return "";
                return val.ToString();
            }
        }

        public string UnSelectedTabCSSClass
        {
            set
            {
                this.ViewState["UnSelectedTabCSSClass"] = value;
            }
            get
            {
                object val = this.ViewState["UnSelectedTabCSSClass"];
                if (val == null) return "";
                return val.ToString();
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("ShoveWebUI_client"), Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("本系列控件的支持目录，以连接到相关的图片、脚本文件")]
        public string SupportDir
        {
            get
            {
                object obj = this.ViewState["SupportDir"];

                if (obj != null)
                {
                    return obj.ToString();
                }
                else
                {
                    return "ShoveWebUI_client";
                }
            }

            set
            {
                this.ViewState["SupportDir"] = value;
            }
        }

        #endregion

        #region events

        public event ShoveTabSelectionChangingHandler ShoveTabSelectionChanging
        {
            add { Events.AddHandler(TabSelectionChangingObject, value); }
            remove { Events.RemoveHandler(TabSelectionChangingObject, value); }
        }

        #endregion

        //[
        // PersistenceMode(PersistenceMode.Attribute)
        //]
        //public StringCssStyleCollection SelectedTabStyle
        //{
        //  get
        //  {
        //    return selectedTabStyle;
        //    //StringCssStyleCollection s = new StringCssStyleCollection();
        //    //s.CssText = "background-Color:red";
        //    //return s;
        //  }
        //  set
        //  {
        //    selectedTabStyle = value;
        //  }
        //}

        //[
        //  DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        //  NotifyParentProperty(true),
        //  PersistenceMode(PersistenceMode.InnerProperty)
        //]
        //public StringCssStyleCollection UnSelectedTabStyle
        //{
        //  get
        //  {
        //    return unSelectedTabStyle;
        //  }
        //  set
        //  {
        //    unSelectedTabStyle = value;
        //  }
        //}

    }
}
