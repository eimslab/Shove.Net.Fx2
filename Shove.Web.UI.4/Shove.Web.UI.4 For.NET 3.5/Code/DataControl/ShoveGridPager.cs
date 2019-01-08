using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
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

    public enum enumNavigationType
    {
        双击,
        单击
    }

    public class PageChangeEventArgs : EventArgs
    {
        public int PageIndex;

        public PageChangeEventArgs(int pageIndex)
        {
            this.PageIndex = pageIndex;
        }
    }

    internal class DatagridConvert : ValidatedControlConverter
    {
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ArrayList values = new ArrayList();

            for (int i = 0; i < context.Container.Components.Count; i++)
            {
                if (context.Container.Components[i].GetType().Equals(typeof(DataGrid)))
                {
                    values.Add(((DataGrid)context.Container.Components[i]).ID);
                }
            }

            return new TypeConverter.StandardValuesCollection(values);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    [DefaultProperty("DataGrid"), ToolboxData("<{0}:ShoveGridPager runat=server></{0}:ShoveGridPager>"), DefaultEvent("PageWillChange")]
    public class ShoveGridPager : WebControl, INamingContainer, IPostBackEventHandler
    {
        private System.Web.UI.WebControls.DataGrid _DataGrid;
        private DataTable _Table;
        private static readonly object PageChangeEvent = new object();
        private static readonly object SortEvent = new object();

        [Description("改变页时激发的事件")]
        public event PageChangeEventHandle PageWillChange
        {
            add
            {
                base.Events.AddHandler(PageChangeEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(PageChangeEvent, value);
            }
        }

        [Description("当排序前激发的事件")]
        public event DataGridSortCommandEventHandler SortBefore
        {
            add
            {
                base.Events.AddHandler(SortEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(SortEvent, value);
            }
        }

        public ShoveGridPager()
        {
            this.Width = new Unit("100%");
        }

        private void GetDataSource()
        {
            if ((this._DataGrid != null) && (this._DataGrid.DataSource != null))
            {
                string text;

                switch (this._DataGrid.DataSource.GetType().Name)
                {
                    case "DataView":
                        this._Table = ((DataView)this._DataGrid.DataSource).Table;
                        return;

                    case "DataSet":
                        if (this._DataGrid.DataMember != string.Empty)
                        {
                            this._Table = ((DataSet)this._DataGrid.DataSource).Tables[this._DataGrid.DataMember];
                            return;
                        }
                        this._Table = ((DataSet)this._DataGrid.DataSource).Tables[0];
                        return;

                    case "DataTable":
                        this._Table = (DataTable)this._DataGrid.DataSource;
                        return;
                }

                if (((text = this._DataGrid.DataSource.GetType().BaseType.Name) == null) || (string.IsInterned(text) != "DataSet"))
                {
                    throw new Exception("错误的数据源或该控件暂不支持该类型的数据源。" + this._DataGrid.DataSource.GetType().Name);
                }

                if (this._DataGrid.DataMember != string.Empty)
                {
                    this._Table = ((DataSet)this._DataGrid.DataSource).Tables[this._DataGrid.DataMember];
                }
                else
                {
                    this._Table = ((DataSet)this._DataGrid.DataSource).Tables[0];
                }
            }
        }

        private void Grid_DataBinding(object sender, EventArgs e)
        {
            if (this.AllowShorting)
            {
                this._DataGrid.AllowSorting = true;

                if (this._DataGrid.HeaderStyle.ForeColor == Color.Empty)
                {
                    this._DataGrid.HeaderStyle.ForeColor = ColorTranslator.FromHtml("Black");
                }

                for (int i = 0; i < this._DataGrid.Columns.Count; i++)
                {
                    if (this._DataGrid.Columns[i].GetType() == typeof(BoundColumn))
                    {
                        this._DataGrid.Columns[i].SortExpression = ((BoundColumn)this._DataGrid.Columns[i]).DataField;
                    }
                }

                this.GetDataSource();

                if ((this._Table != null) && (this.ViewState["SortExpression"] != null))
                {
                    this._Table.DefaultView.Sort = ((string)this.ViewState["SortExpression"]) + " " + ((string)this.ViewState["SortOrder"]);
                    this._DataGrid.DataSource = this._Table.DefaultView;
                }
            }

            if (this.PageIndex >= this.PageCount)
            {
                this.ViewState["_pageindex"] = 0;
            }

            this._DataGrid.CurrentPageIndex = this.PageIndex;
        }

        private void Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.SelectedItem)) || (e.Item.ItemType == ListItemType.Item))
            {
                this.SetColor(e.Item);

                if (this.ClientNavigation)
                {
                    try
                    {
                        if (this.NavigationAction == enumNavigationType.双击)
                        {
                            e.Item.Attributes["ondblclick"] = string.Concat(new object[] { "JavaScript:window.open('", this.NavigationURL, "?", this.NavigationTerm, "=", this._DataGrid.DataKeys[e.Item.ItemIndex], "','", this.NavigationTarget, "')" });
                        }
                        else
                        {
                            e.Item.Attributes["onclick"] = string.Concat(new object[] { "JavaScript:window.open('", this.NavigationURL, "?", this.NavigationTerm, "=", this._DataGrid.DataKeys[e.Item.ItemIndex], "','", this.NavigationTarget, "')" });
                        }
                    }
                    catch
                    {
                    }
                }

                if (this.ShowSelectColumn)
                {
                    e.Item.Cells[0].Attributes["onclick"] = "JavaScript:window.event.cancelBubble=true";
                }

                if (this.GuiseColor)
                {
                    e.Item.Attributes["onmouseover"] = "JavaScript:this.bgColor='" + ColorTranslator.ToHtml(this.HotColor) + "';";
                }

                if (!String.IsNullOrEmpty(RowCursorStyle))
                {
                    e.Item.Attributes["Style"] = "CURSOR:" + RowCursorStyle + ";";
                }
            }
        }

        private void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataGridItem item in this._DataGrid.Items)
            {
                this.SetColor(item);
            }
        }

        private void Grid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            DataGridSortCommandEventHandler handler = (DataGridSortCommandEventHandler)base.Events[SortEvent];

            if (handler != null)
            {
                handler(source, e);
            }

            int num = 0;

            foreach (DataGridColumn column in this._DataGrid.Columns)
            {
                if (column.SortExpression == e.SortExpression)
                {
                    break;
                }

                num++;
            }

            if (this.ViewState["_oldIndex"] == null)
            {
                this.ViewState["SortExpression"] = e.SortExpression;
                this.ViewState["SortOrder"] = "ASC";
                this.ViewState["_oldIndex"] = num;

                if (!this._DataGrid.EnableViewState)
                {
                    this.ViewState["_SortHead"] = this._DataGrid.Columns[num].HeaderText + "<font face='webdings'>6</font>";
                }

                DataGridColumn column1 = this._DataGrid.Columns[num];
                column1.HeaderText = column1.HeaderText + "<font face='webdings'>6</font>";
            }
            else if (num != ((int)this.ViewState["_oldIndex"]))
            {
                this.ViewState["SortExpression"] = e.SortExpression;
                this.ViewState["SortOrder"] = "ASC";

                if (!this._DataGrid.EnableViewState)
                {
                    this.ViewState["_SortHead"] = this._DataGrid.Columns[num].HeaderText + "<font face='webdings'>6</font>";
                }

                this._DataGrid.Columns[(int)this.ViewState["_oldIndex"]].HeaderText = Regex.Replace(this._DataGrid.Columns[(int)this.ViewState["_oldIndex"]].HeaderText, "(<font)(.+?)(>)(.+?)(</font>)", "");
                DataGridColumn column2 = this._DataGrid.Columns[num];
                column2.HeaderText = column2.HeaderText + "<font face='webdings'>6</font>";
                this.ViewState["_oldIndex"] = num;
            }
            else
            {
                if (!this._DataGrid.EnableViewState)
                {
                    this._DataGrid.Columns[num].HeaderText = (string)this.ViewState["_SortHead"];
                }

                if (((string)this.ViewState["SortOrder"]) == "ASC")
                {
                    this.ViewState["SortOrder"] = "DESC";
                    this._DataGrid.Columns[num].HeaderText = Regex.Replace(this._DataGrid.Columns[(int)this.ViewState["_oldIndex"]].HeaderText, "(<font)(.+?)(>)(.+?)(</font>)", "<font face='webdings'>5</font>");
                }
                else
                {
                    this.ViewState["SortOrder"] = "ASC";
                    this._DataGrid.Columns[num].HeaderText = Regex.Replace(this._DataGrid.Columns[(int)this.ViewState["_oldIndex"]].HeaderText, "(<font)(.+?)(>)(.+?)(</font>)", "<font face='webdings'>6</font>");
                }
            }

            this._DataGrid.SelectedIndex = -1;
            this._DataGrid.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (String.IsNullOrEmpty(this.DataGrid))
            {
                return;
            }

            DataGrid g = (DataGrid)this.Page.FindControl(this.DataGrid);

            if (g == null)
            {
                // 可能是装载在用户控件内，替换为 UniqueID 再找
                string _UniqueID = this.UniqueID.Substring(0, this.UniqueID.Length - this.ID.Length) + this.DataGrid;

                g = (DataGrid)this.Page.FindControl(_UniqueID);

                if (g == null)
                {
                    return;
                }
            }

            this._DataGrid = g;
            this._DataGrid.AllowPaging = true;
            this._DataGrid.PageSize = this.PageSize;
            this._DataGrid.PagerStyle.Visible = false;
            this._DataGrid.DataBinding += new EventHandler(this.Grid_DataBinding);

            if (this.GuiseColor || this.ClientNavigation)
            {
                if (this.GuiseColor)
                {
                    this._DataGrid.BorderColor = this.GridColor;
                    this._DataGrid.BorderWidth = new Unit("2px");
                    this._DataGrid.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    this._DataGrid.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
                    this._DataGrid.AlternatingItemStyle.BackColor = Color.Empty;
                    this._DataGrid.ItemStyle.BackColor = Color.Empty;
                    this._DataGrid.SelectedItemStyle.BackColor = Color.Empty;
                }

                this._DataGrid.ItemDataBound += new DataGridItemEventHandler(this.Grid_ItemDataBound);
                this._DataGrid.SelectedIndexChanged += new EventHandler(this.Grid_SelectedIndexChanged);

                if (this.AllowShorting)
                {
                    this._DataGrid.SortCommand += new DataGridSortCommandEventHandler(this.Grid_SortCommand);
                }
            }

            if (this.ShowSelectColumn)
            {
                if (((this._DataGrid.Columns.Count <= 0) || (this._DataGrid.Columns[0].GetType() != typeof(ButtonColumn))) || (((ButtonColumn)this._DataGrid.Columns[0]).CommandName != "Select"))
                {
                    ButtonColumn column = new ButtonColumn();

                    if (this.SelectColumnHeadImage != string.Empty)
                    {
                        column.HeaderImageUrl = this.SelectColumnHeadImage;
                    }

                    if (this.SelectColumnImage == string.Empty)
                    {
                        column.Text = "选择";
                    }
                    else
                    {
                        column.Text = "<Img src='" + this.SelectColumnImage + "' style='BORDER-STYLE:none'>";
                    }

                    column.CommandName = "Select";
                    column.HeaderStyle.Width = new Unit("45px");
                    this._DataGrid.Columns.AddAt(0, column);
                }
                else
                {
                    ButtonColumn column2 = (ButtonColumn)this._DataGrid.Columns[0];

                    if (this.SelectColumnHeadImage != string.Empty)
                    {
                        column2.HeaderImageUrl = this.SelectColumnHeadImage;
                    }

                    if (this.SelectColumnImage == string.Empty)
                    {
                        column2.Text = "选择";
                    }
                    else
                    {
                        column2.Text = "<Img src='" + this.SelectColumnImage + "' style='BORDER-STYLE:none'>";
                    }

                    column2.HeaderStyle.Width = new Unit("45px");
                    column2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                }
            }
            else if (((this._DataGrid.Columns.Count > 0) && (this._DataGrid.Columns[0].GetType() == typeof(ButtonColumn))) && (((ButtonColumn)this._DataGrid.Columns[0]).CommandName == "Select"))
            {
                this._DataGrid.Columns.RemoveAt(0);
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            switch (eventArgument)
            {
                case "Start":
                    this.PageIndex = 0;

                    return;

                case "End":
                    this.PageIndex = this.PageCount - 1;

                    return;

                case "PreGroup":
                    this.PageIndex = (this.CurrentGroup - 1) * this.ButtonCount;

                    return;

                case "NextGroup":
                    this.PageIndex = (this.CurrentGroup + 1) * this.ButtonCount;

                    return;
            }

            if ((eventArgument == "") || (eventArgument == null))
            {
                eventArgument = this.Page.Request[this.UniqueID + "_Page"];
            }

            this.PageIndex = Convert.ToInt32(eventArgument);
        }

        #region 语言设置

        [Description("“页次”的显示文字"), DefaultValue("页次"), Category("Appearance")]
        public string PageNoText
        {
            get
            {
                object obj2 = this.ViewState["_PageNoText"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "页次";
            }
            set
            {
                this.ViewState["_PageNoText"] = value;
            }
        }

        [Description("“每页行数”的显示文字"), DefaultValue("每页行数"), Category("Appearance")]
        public string PageRowCountText
        {
            get
            {
                object obj2 = this.ViewState["_PageRowCountText"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "每页行数";
            }
            set
            {
                this.ViewState["_PageRowCountText"] = value;
            }
        }

        [Description("“总行数”的显示文字"), DefaultValue("总行数"), Category("Appearance")]
        public string RowCountText
        {
            get
            {
                object obj2 = this.ViewState["_RowCountText"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "总行数";
            }
            set
            {
                this.ViewState["_RowCountText"] = value;
            }
        }


        [Description("“第一页”的显示文字"), DefaultValue("第一页"), Category("Appearance")]
        public string FirstPageText
        {
            get
            {
                object obj2 = this.ViewState["_FirstPageText"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "第一页";
            }
            set
            {
                this.ViewState["_FirstPageText"] = value;
            }
        }

        [Description("“最后一页”的显示文字"), DefaultValue("最后一页"), Category("Appearance")]
        public string LastPageText
        {
            get
            {
                object obj2 = this.ViewState["_LastPageText"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "最后一页";
            }
            set
            {
                this.ViewState["_LastPageText"] = value;
            }
        }

        [Description("“上一组”的显示文字"), DefaultValue("上一组"), Category("Appearance")]
        public string PreviousGroupText
        {
            get
            {
                object obj2 = this.ViewState["_PreviousGroupText"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "上一组";
            }
            set
            {
                this.ViewState["_PreviousGroupText"] = value;
            }
        }

        [Description("“下一组”的显示文字"), DefaultValue("下一组"), Category("Appearance")]
        public string NextGroupText
        {
            get
            {
                object obj2 = this.ViewState["_NextGroupText"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "下一组";
            }
            set
            {
                this.ViewState["_NextGroupText"] = value;
            }
        }


        [Description("“选择页数”的显示文字"), DefaultValue("选择页数"), Category("Appearance")]
        public string SelectPageNoText
        {
            get
            {
                object obj2 = this.ViewState["_SelectPageNoText"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "选择页数";
            }
            set
            {
                this.ViewState["_SelectPageNoText"] = value;
            }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            string text = (this.BackColor == Color.Empty) ? "Transparent" : ColorTranslator.ToHtml(this.BackColor);
            string text2 = (this.ForeColor == Color.Empty) ? "Black" : ColorTranslator.ToHtml(this.ForeColor);
            string text3 = (this.BorderColor == Color.Empty) ? "Transparent" : ColorTranslator.ToHtml(this.BorderColor);
            string text4 = this.BorderStyle.ToString();
            string text5 = this.BorderWidth.ToString();

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "BACKGROUND-COLOR:" + text + ";BORDER-WIDTH: " + text5 + ";BORDER-STYLE:" + text4 + ";BORDER-COLOR:" + text3 + ";WIDTH:" + this.Width.ToString() + ";HEIGHT:" + this.Height.ToString() + " ;TOP:" + base.Style["TOP"] + ";LEFT:" + base.Style["LEFT"] + ";POSITION:" + base.Style["POSITION"] + ";COLOR:" + text2 + ";Cursor:Default;");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "5");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.Write("\r\n");
            writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-weight:bold;FONT-SIZE: 9pt;");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            string[] textArray = new string[] { PageNoText, ":", (this.PageIndex + 1).ToString(), "/", this.PageCount.ToString(), " ", PageRowCountText, ":", this.PageSize.ToString(), " ", RowCountText, ":", this.ReportCount.ToString() };
            writer.Write(string.Concat(textArray));
            writer.RenderEndTag();
            writer.Write("\r\n");

            if (this.ShowButton)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "FONT-SIZE: 9pt;TEXT-DECORATION: underline;");
                writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("\r\n");

                if (this.PageIndex == 0)
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family:Webdings;");
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "Disabled");
                }
                else
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family:Webdings;color:" + text2 + "");
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, "color:" + text2 + "");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, "Start"));
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Title, FirstPageText);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(FirstPageText);//"&lt;&lt;");
                writer.RenderEndTag();
                writer.Write("\r\n");

                if (this.CurrentGroup == 0)
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family:Webdings;");
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "Disabled");
                }
                else
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family:Webdings;color:" + text2 + "");
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, "color:" + text2 + "");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, "PreGroup"));
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Title, PreviousGroupText);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(PreviousGroupText);//"&lt;");
                writer.RenderEndTag();
                writer.Write("\r\n");

                for (int i = 1; i <= this.ButtonCount; i++)
                {
                    if (((this.CurrentGroup * this.ButtonCount) + i) <= this.PageCount)
                    {
                        if (((this.CurrentGroup * this.ButtonCount) + i) == (this.PageIndex + 1))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-weight:bold;color:" + ColorTranslator.ToHtml(this.CurrentColor) + ";");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-weight:bold;color:" + text2 + ";");
                            int num3 = ((this.CurrentGroup * this.ButtonCount) + i) - 1;
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, num3.ToString()));
                        }

                        writer.AddAttribute(HtmlTextWriterAttribute.Title, PageNoText + ":" + (((this.CurrentGroup * this.ButtonCount) + i)).ToString());
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.Write((int)((this.CurrentGroup * this.ButtonCount) + i));
                        writer.RenderEndTag();
                        writer.Write("\r\n");
                    }
                }

                if (this.CurrentGroup == this.LastGroup)
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family:Webdings;");
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "Disabled");
                }
                else
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family:Webdings;color:" + text2 + "");
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, "color:" + text2 + "");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, "NextGroup"));
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Title, NextGroupText);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(NextGroupText);//"&gt;");
                writer.RenderEndTag();
                writer.Write("\r\n");

                if (this.PageIndex == (this.PageCount - 1))
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family:Webdings;");
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "Disabled");
                }
                else
                {
                    //writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family:Webdings;color:" + text2 + "");
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, "color:" + text2 + "");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, "End"));
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Title, LastPageText);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(LastPageText);//"&gt;&gt;");
                writer.RenderEndTag();
                writer.Write("\r\n");
                writer.RenderEndTag();
                writer.Write("\r\n");
            }

            if (this.ShowDropDownList)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("\r\n");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, SelectPageNoText);
                writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID + "_Page");
                writer.AddAttribute(HtmlTextWriterAttribute.Onchange, this.Page.ClientScript.GetPostBackClientHyperlink(this, string.Empty));
                writer.RenderBeginTag(HtmlTextWriterTag.Select);
                writer.Write("\r\n");

                for (int j = 1; j <= this.PageCount; j++)
                {
                    if (j == (this.PageIndex + 1))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Selected, "Selected");
                    }

                    writer.AddAttribute(HtmlTextWriterAttribute.Value, (j - 1).ToString());
                    writer.RenderBeginTag(HtmlTextWriterTag.Option);
                    writer.Write(j);
                    writer.RenderEndTag();
                }

                writer.RenderEndTag();
                writer.Write("\r\n");
                writer.RenderEndTag();
            }

            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        internal void SetColor(DataGridItem MyItem)
        {
            if ((((MyItem.ItemType == ListItemType.AlternatingItem) || (MyItem.ItemType == ListItemType.SelectedItem)) || (MyItem.ItemType == ListItemType.Item)) && this.GuiseColor)
            {
                switch (MyItem.ItemType)
                {
                    case ListItemType.Item:
                        MyItem.Attributes["bgColor"] = ColorTranslator.ToHtml(this.RowColor);
                        MyItem.Attributes["onmouseout"] = "JavaScript:this.bgColor='" + ColorTranslator.ToHtml(this.RowColor) + "';";

                        return;

                    case ListItemType.AlternatingItem:
                        MyItem.Attributes["bgColor"] = ColorTranslator.ToHtml(this.AlternatingRowColor);
                        MyItem.Attributes["onmouseout"] = "JavaScript:this.bgColor='" + ColorTranslator.ToHtml(this.AlternatingRowColor) + "';";

                        return;
                }

                MyItem.Attributes["bgColor"] = ColorTranslator.ToHtml(this.SelectRowColor);
                MyItem.Attributes["onmouseout"] = "JavaScript:this.bgColor='" + ColorTranslator.ToHtml(this.SelectRowColor) + "';";

                if (this.ShowSelectColumn && (this.SelectColumnSelectImage != string.Empty))
                {
                    MyItem.Cells[0].Text = "<Img src='" + this.SelectColumnSelectImage + "'>";
                }
            }
        }

        [Category("DataGrid 设置"), DefaultValue(true), Description("是否允许排序。")]
        public bool AllowShorting
        {
            get
            {
                object obj2 = this.ViewState["_AllowShorting"];

                if (obj2 != null)
                {
                    return (bool)obj2;
                }

                return true;
            }
            set
            {
                this.ViewState["_AllowShorting"] = value;

                if (this._DataGrid != null)
                {
                    this._DataGrid.AllowSorting = value;

                    if (value)
                    {
                        for (int i = 0; i < this._DataGrid.Columns.Count; i++)
                        {
                            if (this._DataGrid.Columns[i].GetType() == typeof(BoundColumn))
                            {
                                this._DataGrid.Columns[i].SortExpression = ((BoundColumn)this._DataGrid.Columns[i]).DataField;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < this._DataGrid.Columns.Count; j++)
                        {
                            if (this._DataGrid.Columns[j].GetType() == typeof(BoundColumn))
                            {
                                this._DataGrid.Columns[j].SortExpression = string.Empty;
                            }
                        }
                    }
                }
            }
        }

        [Category("DataGrid 设置"), DefaultValue("#E1F0FF"), Description("交替行的颜色"), TypeConverter(typeof(WebColorConverter))]
        public Color AlternatingRowColor
        {
            get
            {
                object obj2 = this.ViewState["_AlternatingRowColor"];

                if (obj2 != null)
                {
                    return (Color)obj2;
                }

                return ColorTranslator.FromHtml("#E1F0FF");
            }
            set
            {
                this.ViewState["_AlternatingRowColor"] = value;
            }
        }

        [DefaultValue(10), Description("导航按钮的个数"), Category("设置")]
        public int ButtonCount
        {
            get
            {
                object obj2 = this.ViewState["_buttoncount"];

                if (obj2 != null)
                {
                    return (int)obj2;
                }

                return 10;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("导航按钮的个数必须大于零");
                }

                this.ViewState["_buttoncount"] = value;
            }
        }

        [Category("DataGrid 设置"), Description("是否实现客户端点击转到相应的页面"), DefaultValue(false)]
        public bool ClientNavigation
        {
            get
            {
                object obj2 = this.ViewState["_ClientNavigation"];

                if (obj2 != null)
                {
                    return (bool)obj2;
                }

                return false;
            }
            set
            {
                this.ViewState["_ClientNavigation"] = value;
            }
        }

        [TypeConverter(typeof(WebColorConverter)), DefaultValue("Red"), Category("外观"), Description("当前页导航按钮的颜色")]
        public Color CurrentColor
        {
            get
            {
                object obj2 = this.ViewState["_currentcolor"];

                if (obj2 != null)
                {
                    return (Color)obj2;
                }

                return ColorTranslator.FromHtml("Red");
            }
            set
            {
                this.ViewState["_currentcolor"] = value;
            }
        }

        [DefaultValue(0), Description("当前组"), Browsable(false)]
        private int CurrentGroup
        {
            get
            {
                return (int)decimal.Truncate(this.PageIndex / this.ButtonCount);
            }
        }

        [Description("要控制分页的DataGrid的ID"), TypeConverter(typeof(DatagridConvert)), Category("设置")]
        public string DataGrid
        {
            get
            {
                object obj2 = this.ViewState["_datagrid"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return string.Empty;
            }
            set
            {
                this.ViewState["_datagrid"] = value;
            }
        }

        [Category("DataGrid 设置"), Description("当DataGrid边框的颜色"), DefaultValue("#6595D6"), TypeConverter(typeof(WebColorConverter))]
        public Color GridColor
        {
            get
            {
                object obj2 = this.ViewState["_GridColor"];

                if (obj2 != null)
                {
                    return (Color)obj2;
                }

                return ColorTranslator.FromHtml("#6595D6");
            }
            set
            {
                this.ViewState["_GridColor"] = value;

                if ((this._DataGrid != null) && this.GuiseColor)
                {
                    this._DataGrid.BorderColor = value;
                }
            }
        }

        [Category("DataGrid 设置"), Description("是否控制DataGrid的外观颜色"), DefaultValue(true)]
        public bool GuiseColor
        {
            get
            {
                object obj2 = this.ViewState["_GuiseColor"];

                if (obj2 != null)
                {
                    return (bool)obj2;
                }

                return true;
            }
            set
            {
                this.ViewState["_GuiseColor"] = value;
            }
        }

        [DefaultValue("#C1D2EE"), TypeConverter(typeof(WebColorConverter)), Category("DataGrid 设置"), Description("当鼠标悬停在行上时的颜色")]
        public Color HotColor
        {
            get
            {
                object obj2 = this.ViewState["_hotcolor"];

                if (obj2 != null)
                {
                    return (Color)obj2;
                }

                return ColorTranslator.FromHtml("#C1D2EE");
            }
            set
            {
                this.ViewState["_hotcolor"] = value;
            }
        }

        [Browsable(false), Description("最后一组"), DefaultValue(0)]
        private int LastGroup
        {
            get
            {
                return (int)decimal.Truncate((this.PageCount - 1) / this.ButtonCount);
            }
        }

        [DefaultValue("双击"), Category("DataGrid 设置"), Description("选择客户端何种动作跳转页面")]
        public enumNavigationType NavigationAction
        {
            get
            {
                object obj2 = this.ViewState["_NavigationAction"];

                if (obj2 != null)
                {
                    return (enumNavigationType)obj2;
                }

                return enumNavigationType.双击;
            }
            set
            {
                this.ViewState["_NavigationAction"] = value;
            }
        }

        [Description("跳转页面的Target"), DefaultValue("_self"), Category("DataGrid 设置"), TypeConverter(typeof(TargetConverter))]
        public string NavigationTarget
        {
            get
            {
                object obj2 = this.ViewState["_NavigationTarget"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "_self";
            }
            set
            {
                this.ViewState["_NavigationTarget"] = value;
            }
        }

        [Description("跳转页面的URL条件名称,条件的值为DataKeys"), DefaultValue("ID"), Category("DataGrid 设置")]
        public string NavigationTerm
        {
            get
            {
                object obj2 = this.ViewState["_NavigationTerm"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "ID";
            }
            set
            {
                this.ViewState["_NavigationTerm"] = value;
            }
        }

        [Editor(typeof(UrlEditor), typeof(UITypeEditor)), Category("DataGrid 设置"), Description("跳转页面的URL")]
        public string NavigationURL
        {
            get
            {
                object obj2 = this.ViewState["_NavigationURL"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return string.Empty;
            }
            set
            {
                this.ViewState["_NavigationURL"] = value;
            }
        }

        [Description("总页数"), DefaultValue(0), Browsable(false)]
        public int PageCount
        {
            get
            {
                if (this._DataGrid != null)
                {
                    return this._DataGrid.PageCount;
                }

                return 0;
            }
        }

        [Browsable(false), Description("当前页"), DefaultValue(0)]
        public int PageIndex
        {
            get
            {
                object obj2 = this.ViewState["_pageindex"];

                if (obj2 != null)
                {
                    return (int)obj2;
                }

                return 0;
            }
            set
            {
                this.ViewState["_pageindex"] = value;
                PageChangeEventHandle handle = (PageChangeEventHandle)base.Events[PageChangeEvent];
                this._DataGrid.SelectedIndex = -1;

                if (handle != null)
                {
                    handle(this, new PageChangeEventArgs(this.PageIndex));
                }

                this._DataGrid.DataBind();
            }
        }

        [DefaultValue(20), Category("设置"), Description("每页的行数")]
        public int PageSize
        {
            get
            {
                object obj2 = this.ViewState["_pagesize"];

                if (obj2 != null)
                {
                    return (int)obj2;
                }

                return 20;
            }
            set
            {
                this.ViewState["_pagesize"] = value;
            }
        }

        [Description("总行数"), Browsable(false)]
        public int ReportCount
        {
            get
            {
                this.GetDataSource();

                if (this._Table != null)
                {
                    return this._Table.Rows.Count;
                }

                return 0;
            }
        }

        [DefaultValue("White"), Category("DataGrid 设置"), Description("行的颜色"), TypeConverter(typeof(WebColorConverter))]
        public Color RowColor
        {
            get
            {
                object obj2 = this.ViewState["_rowcolor"];

                if (obj2 != null)
                {
                    return (Color)obj2;
                }

                return ColorTranslator.FromHtml("White");
            }
            set
            {
                this.ViewState["_rowcolor"] = value;
            }
        }

        [DefaultValue("hand"), Category("DataGrid 设置"), Description("行的鼠标样式")]
        public string RowCursorStyle
        {
            get
            {
                object obj2 = this.ViewState["_RowCursorStyle"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return "hand";
            }
            set
            {
                this.ViewState["_RowCursorStyle"] = value;
            }
        }

        [Category("DataGrid 设置"), DefaultValue(""), Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)), Description("选择列表头图片。")]
        public string SelectColumnHeadImage
        {
            get
            {
                object obj2 = this.ViewState["_SelectColumnHeadImage"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return string.Empty;
            }
            set
            {
                this.ViewState["_SelectColumnHeadImage"] = value;
            }
        }

        [Description("选择列没有选中时的图片。"), DefaultValue(""), Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)), Category("DataGrid 设置")]
        public string SelectColumnImage
        {
            get
            {
                object obj2 = this.ViewState["_SelectColumnImage"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return string.Empty;
            }
            set
            {
                this.ViewState["_SelectColumnImage"] = value;

                if ((((this._DataGrid != null) && this.ShowSelectColumn) && ((this._DataGrid.Columns.Count > 0) && (this._DataGrid.Columns[0].GetType() == typeof(ButtonColumn)))) && ((((ButtonColumn)this._DataGrid.Columns[0]).CommandName == "Select") && (value != string.Empty)))
                {
                    ((ButtonColumn)this._DataGrid.Columns[0]).Text = "<Img src='" + this.SelectColumnImage + "' style='BORDER-STYLE:none'>";
                }
            }
        }

        [Category("DataGrid 设置"), Description("选择列选中时的图片。"), Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)), DefaultValue("")]
        public string SelectColumnSelectImage
        {
            get
            {
                object obj2 = this.ViewState["_SelectColumnSelectImage"];

                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return string.Empty;
            }
            set
            {
                this.ViewState["_SelectColumnSelectImage"] = value;
            }
        }

        [Category("DataGrid 设置"), DefaultValue("#FF9933"), TypeConverter(typeof(WebColorConverter)), Description("选定行的颜色")]
        public Color SelectRowColor
        {
            get
            {
                object obj2 = this.ViewState["_selectrowcolor"];

                if (obj2 != null)
                {
                    return (Color)obj2;
                }

                return ColorTranslator.FromHtml("#FF9933");
            }
            set
            {
                this.ViewState["_selectrowcolor"] = value;
            }
        }

        [Description("是否显示导航按钮"), DefaultValue(true), Category("设置")]
        public bool ShowButton
        {
            get
            {
                object obj2 = this.ViewState["_showbutton"];

                if (obj2 != null)
                {
                    return (bool)obj2;
                }

                return true;
            }
            set
            {
                this.ViewState["_showbutton"] = value;
            }
        }

        [Category("设置"), Description("是否下拉列表导航按钮"), DefaultValue(true)]
        public bool ShowDropDownList
        {
            get
            {
                object obj2 = this.ViewState["_ShowDropDownList"];

                if (obj2 != null)
                {
                    return (bool)obj2;
                }

                return true;
            }
            set
            {
                this.ViewState["_ShowDropDownList"] = value;
            }
        }

        [DefaultValue(true), Category("DataGrid 设置"), Description("是否显示选择列。")]
        public bool ShowSelectColumn
        {
            get
            {
                object obj2 = this.ViewState["_ShowSelectColumn"];

                if (obj2 != null)
                {
                    return (bool)obj2;
                }

                return true;
            }
            set
            {
                this.ViewState["_ShowSelectColumn"] = value;
            }
        }

        public delegate void PageChangeEventHandle(object Sender, PageChangeEventArgs e);
    }
}

