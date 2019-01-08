using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Collections;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    /// <summary>
    /// 委托
    /// </summary>
    public delegate void PageIndexChangeEvent(object sender, PageChangeEventArgs e);

    /// <summary>
    /// 分页组件，仅触发 OnPageIndexChange 回传事件，请在此事件中使用 BindData 等操作。
    /// </summary>
    [DefaultProperty("PageSize"), ToolboxData("<{0}:ShovePager runat=server></{0}:ShovePager>"), DefaultEvent("PageIndexChanged")]
    public class ShovePager : WebControl, INamingContainer, IPostBackEventHandler
    {
        private Label labPageTotal;
        private LinkButton lbFirst;
        private LinkButton lbPrevious;
        private LinkButton lbNext;
        private LinkButton lbLast;
        private DropDownList ddlPages;
        private Table tbl;

        [Description("改变页时激发的事件"), Category("Event"), Browsable(true)]
        public event PageIndexChangeEvent PageIndexChanged;

        protected virtual void OnPageIndexChanged(PageChangeEventArgs e)
        {
            if (PageIndexChanged != null)
            {
                PageIndexChanged(this, e);
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (!String.IsNullOrEmpty(eventArgument))
            {
                this.PageIndex = Convert.ToInt32(eventArgument);
            }
        }

        #region 按钮、下拉选择的事件处理

        private void lbFirst_OnClick(object sender, System.EventArgs e)
        {
            PageIndex = 0;

            //PageChangeEventArgs pe = new PageChangeEventArgs(PageIndex);
            //OnPageIndexChanged(pe);
            ddlPages.SelectedIndex = PageIndex;
            DownDropList_OnSelectedChanged(ddlPages, new EventArgs());
        }

        private void lbPrevious_OnClick(object sender, System.EventArgs e)
        {
            PageIndex = PageIndex - 1;

            //PageChangeEventArgs pe = new PageChangeEventArgs(PageIndex);
            //OnPageIndexChanged(pe);
            ddlPages.SelectedIndex = PageIndex;
            DownDropList_OnSelectedChanged(ddlPages, new EventArgs());
        }

        private void lbNext_OnClick(object sender, System.EventArgs e)
        {
            PageIndex = PageIndex + 1;

            //PageChangeEventArgs pe = new PageChangeEventArgs(PageIndex);
            //OnPageIndexChanged(pe);
            ddlPages.SelectedIndex = PageIndex;
            DownDropList_OnSelectedChanged(ddlPages, new EventArgs());
        }

        private void lbLast_OnClick(object sender, System.EventArgs e)
        {
            PageIndex = PageCount - 1;

            //PageChangeEventArgs pe = new PageChangeEventArgs(PageIndex);
            //OnPageIndexChanged(pe);
            ddlPages.SelectedIndex = PageIndex;
            DownDropList_OnSelectedChanged(ddlPages, new EventArgs());
        }

        private void DownDropList_OnSelectedChanged(object sender, System.EventArgs e)
        {
            PageIndex = int.Parse(ddlPages.SelectedItem.Value);

            PageChangeEventArgs pe = new PageChangeEventArgs(PageIndex);
            OnPageIndexChanged(pe);
        }

        #endregion

        #region 基本属性

        [Description("总行数"), Category("Appearance"), DefaultValue(0), Browsable(true)]
        public int RowCount
        {
            get
            {
                object obj = this.ViewState["RowCount"];

                if (obj != null)
                {
                    return (int)obj;
                }

                return 0;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("RowCount must be greater or equal than zero.");
                }

                this.ViewState["RowCount"] = value;
                CalcPageCount();
            }
        }

        [Description("每页的行数"), Category("Appearance"), DefaultValue(20), Browsable(true)]
        public int PageSize
        {
            get
            {
                object obj = this.ViewState["PageSize"];

                if (obj != null)
                {
                    return (int)obj;
                }

                return 20;
            }
            set
            {
                if (value < 1)
                {
                    throw new Exception("PageSize must be greater than zero.");
                }

                this.ViewState["PageSize"] = value;
                CalcPageCount();
            }
        }

        private void CalcPageCount()
        {
            this.ViewState["PageCount"] = (RowCount / PageSize) + ((RowCount % PageSize) == 0 ? 0 : 1);
        }

        [Description("总页数"), Browsable(false)]
        public int PageCount
        {
            get
            {
                object obj = this.ViewState["PageCount"];

                if (obj != null)
                {
                    return (int)obj;
                }

                return 0;
            }
        }

        [Description("当前页"), Category("Appearance"), DefaultValue(0), Browsable(true)]
        public int PageIndex
        {
            get
            {
                object obj = this.ViewState["PageIndex"];

                if (obj != null)
                {
                    return (int)obj;
                }

                return 0;
            }
            set
            {
                this.ViewState["PageIndex"] = value;
            }
        }

        [Description("上一页按钮文字"), Category("Appearance"), DefaultValue("上一页"), Browsable(true)]
        public string PreviousPageText
        {
            get
            {
                object obj = this.ViewState["PreviousPageText"];

                if (obj == null)
                {
                    this.PreviousPageText = "上一页";

                    return "上一页";
                }

                return (string)obj;
            }
            set
            {
                this.ViewState["PreviousPageText"] = value;
            }
        }

        [Description("下一页按钮文字"), Category("Appearance"), DefaultValue("上一页"), Browsable(true)]
        public string NextPageText
        {
            get
            {
                object obj = this.ViewState["NextPageText"];
                if (obj == null)
                {
                    this.NextPageText = "下一页";
                }
                if (obj != null)
                {
                    return (string)obj;
                }
                return "下一页";
            }
            set
            {
                this.ViewState["NextPageText"] = value;
            }
        }

        [Description("首页按钮文字"), Category("Appearance"), DefaultValue("首 页"), Browsable(true)]
        public string FirstPageText
        {
            get
            {
                object obj = this.ViewState["FirstPageText"];
                if (obj == null)
                {
                    this.FirstPageText = "首 页";
                }
                if (obj != null)
                {
                    return (string)obj;
                }
                return "首 页";
            }
            set
            {
                this.ViewState["FirstPageText"] = value;
            }
        }

        [Description("尾页按钮文字"), Category("Appearance"), DefaultValue("尾 页"), Browsable(true)]
        public string LastPageText
        {
            get
            {
                object obj = this.ViewState["LastPageText"];
                if (obj == null)
                {
                    this.LastPageText = "尾 页";
                }
                if (obj != null)
                {
                    return (string)obj;
                }
                return "尾 页";
            }
            set
            {
                this.ViewState["LastPageText"] = value;
            }
        }

        [Description("“第几页”代替“第”字的文字"), Category("Appearance"), DefaultValue("第"), Browsable(true)]
        public string GoToPageTextBefore
        {
            get
            {
                object obj = this.ViewState["GoToPageTextBefore"];
                if (obj == null)
                {
                    this.GoToPageTextBefore = "第";
                }
                if (obj != null)
                {
                    return (string)obj;
                }
                return "第";
            }
            set
            {
                this.ViewState["GoToPageTextBefore"] = value;
            }
        }

        [Description("“第几页”代替“页”字的文字"), Category("Appearance"), DefaultValue("页"), Browsable(true)]
        public string GoToPageTextAfter
        {
            get
            {
                object obj = this.ViewState["GoToPageTextAfter"];
                if (obj == null)
                {
                    this.GoToPageTextAfter = "页";
                }
                if (obj != null)
                {
                    return (string)obj;
                }
                return "页";
            }
            set
            {
                this.ViewState["GoToPageTextAfter"] = value;
            }
        }

        [Description("“共几页”代替“共”字的文字"), Category("Appearance"), DefaultValue("共"), Browsable(true)]
        public string TotalTextBefore
        {
            get
            {
                object obj = this.ViewState["TotalTextBefore"];
                if (obj == null)
                {
                    this.TotalTextBefore = "共";
                }
                if (obj != null)
                {
                    return (string)obj;
                }
                return "共";
            }
            set
            {
                this.ViewState["TotalTextBefore"] = value;
            }
        }

        [Description("“共几页”代替“页”字的文字"), Category("Appearance"), DefaultValue("页"), Browsable(true)]
        public string TotalTextAfter
        {
            get
            {
                object obj = this.ViewState["TotalTextAfter"];
                if (obj == null)
                {
                    this.TotalTextAfter = "页";
                }
                if (obj != null)
                {
                    return (string)obj;
                }
                return "页";
            }
            set
            {
                this.ViewState["TotalTextAfter"] = value;
            }
        }

        #endregion

        private void CreateControl()
        {
            labPageTotal = new Label();
            labPageTotal.ID = "labPageTotal";
            labPageTotal.Text = TotalTextBefore + " " + PageCount.ToString() + " " + TotalTextAfter;

            lbFirst = new LinkButton();
            lbFirst.ID = "lbFirst";
            lbFirst.Text = FirstPageText;
            lbFirst.Visible = true;
            lbFirst.Click += new EventHandler(lbFirst_OnClick);
            lbFirst.CssClass = this.CssClass;

            lbPrevious = new LinkButton();
            lbPrevious.ID = "lbPrevious";
            lbPrevious.Text = PreviousPageText;
            lbPrevious.Visible = true;
            lbPrevious.Click += new EventHandler(lbPrevious_OnClick);
            lbPrevious.CssClass = this.CssClass;

            lbNext = new LinkButton();
            lbNext.ID = "lbNext";
            lbNext.Text = NextPageText;
            lbNext.Visible = true;
            lbNext.Click += new EventHandler(lbNext_OnClick);
            lbNext.CssClass = this.CssClass;

            lbLast = new LinkButton();
            lbLast.ID = "lbLast";
            lbLast.Text = LastPageText;
            lbLast.Visible = true;
            lbLast.Click += new EventHandler(lbLast_OnClick);
            lbLast.CssClass = this.CssClass;

            ddlPages = new DropDownList();
            ddlPages.ID = "ddlPages";
            ddlPages.AutoPostBack = true;
            ddlPages.SelectedIndexChanged += new EventHandler(DownDropList_OnSelectedChanged);
            ddlPages.Items.Clear();

            for (int i = 0; i < PageCount; i++)
            {
                ddlPages.Items.Add(new ListItem((i + 1).ToString(), i.ToString()));
            }

            if (this.PageIndex == 0)
            {
                lbFirst.Enabled = false;
                lbPrevious.Enabled = false;
            }
            if (this.PageIndex == this.PageCount - 1)
            {
                lbLast.Enabled = false;
                lbNext.Enabled = false;
            }

            if (this.RowCount == 0)
            {
                lbFirst.Enabled = false;
                lbPrevious.Enabled = false;
                lbLast.Enabled = false;
                lbNext.Enabled = false;
                ddlPages.Enabled = false;
            }

            if (PageCount > 0)
            {
                ddlPages.SelectedIndex = PageIndex;
            }

            tbl = new Table();
            tbl.CssClass = this.CssClass;

            tbl.Width = Unit.Parse("100%");
            TableRow row = new TableRow();
            row.Width = Unit.Parse("100%");
            int cellIndex = 0;
            cellIndex = row.Cells.Add(new TableCell());
            row.Cells[cellIndex].Controls.Add(labPageTotal);
            row.Cells[cellIndex].Style[HtmlTextWriterStyle.TextAlign] = "center";
            row.Cells[cellIndex].Width = Unit.Parse("16%");
            row.Cells[cellIndex].Wrap = false;
            cellIndex = row.Cells.Add(new TableCell());
            row.Cells[cellIndex].Controls.Add(lbFirst);
            row.Cells[cellIndex].Style[HtmlTextWriterStyle.TextAlign] = "center";
            row.Cells[cellIndex].Width = Unit.Parse("16%");
            row.Cells[cellIndex].Wrap = false;
            cellIndex = row.Cells.Add(new TableCell());
            row.Cells[cellIndex].Controls.Add(lbPrevious);
            row.Cells[cellIndex].Style[HtmlTextWriterStyle.TextAlign] = "center";
            row.Cells[cellIndex].Width = Unit.Parse("16%");
            row.Cells[cellIndex].Wrap = false;
            cellIndex = row.Cells.Add(new TableCell());
            row.Cells[cellIndex].Controls.Add(lbNext);
            row.Cells[cellIndex].Style[HtmlTextWriterStyle.TextAlign] = "center";
            row.Cells[cellIndex].Width = Unit.Parse("16%");
            row.Cells[cellIndex].Wrap = false;
            cellIndex = row.Cells.Add(new TableCell());
            row.Cells[cellIndex].Controls.Add(lbLast);
            row.Cells[cellIndex].Style[HtmlTextWriterStyle.TextAlign] = "center";
            row.Cells[cellIndex].Width = Unit.Parse("16%");
            row.Cells[cellIndex].Wrap = false;
            cellIndex = row.Cells.Add(new TableCell());
            row.Cells[cellIndex].Text = GoToPageTextBefore;
            row.Cells[cellIndex].Style[HtmlTextWriterStyle.TextAlign] = "right";
            row.Cells[cellIndex].Width = Unit.Parse("6%");
            cellIndex = row.Cells.Add(new TableCell());
            row.Cells[cellIndex].Controls.Add(ddlPages);
            row.Cells[cellIndex].Style[HtmlTextWriterStyle.TextAlign] = "center";
            row.Cells[cellIndex].Width = Unit.Parse("8%");
            cellIndex = row.Cells.Add(new TableCell());
            row.Cells[cellIndex].Text = GoToPageTextAfter;
            row.Cells[cellIndex].Style[HtmlTextWriterStyle.TextAlign] = "left";
            row.Cells[cellIndex].Width = Unit.Parse("6%");
            tbl.Rows.Add(row);

            this.Controls.Add(tbl);
        }

        protected override void CreateChildControls()
        {
            CreateControl();

            base.CreateChildControls();
        }

        #region Render

        protected override void Render(HtmlTextWriter output)
        {
            this.Controls.Clear();
            this.ClearChildViewState();
            CreateChildControls();

            output.WriteLine("\r\n<!-- ShovePager Start -->");
            base.RenderBeginTag(output);
            base.RenderChildren(output);
            base.RenderEndTag(output);
            output.WriteLine("\r\n<!-- ShovePager End -->");
        }

        #endregion
    }
}