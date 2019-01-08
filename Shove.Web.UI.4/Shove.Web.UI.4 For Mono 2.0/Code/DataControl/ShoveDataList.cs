using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveDataList 的摘要说明。
    /// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:ShoveDataList runat=server></{0}:ShoveDataList>")]
    public class ShoveDataList : System.Web.UI.WebControls.DataList, System.Web.UI.IPostBackEventHandler
    {
        // Events
        [Browsable(true), Category("Event"), Description("页改变时激发的事件")]
        public event EventHandler PageIndexChanged;

        // Properties
        [Description("是否允许分页"), Category("Appearance"), Browsable(true)]
        public virtual bool AllowPaging
        {
            get
            {
                object obj1 = this.ViewState["AllowPaging"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return false;
            }
            set
            {
                this.ViewState["AllowPaging"] = value;
            }
        }

        [Category("Appearance"), Browsable(false), Description("\u5f53\u524d\u6309\u94ae\u7ec4\u7684\u5e8f\u53f7")]
        public virtual int CurrentPageButtonGroupIndex
        {
            get
            {
                object obj1 = this.ViewState["CurrentPageButtonGroupIndex"];
                if (obj1 == null)
                {
                    this.CurrentPageButtonGroupIndex = 0;
                }
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 0;
            }
            set
            {
                this.ViewState["CurrentPageButtonGroupIndex"] = value;
            }
        }

        [Category("Appearance"), Description("当前页码"), Browsable(true)]
        public virtual int CurrentPageIndex
        {
            get
            {
                object obj1 = this.ViewState["CurrentPageIndex"];
                if (obj1 == null)
                {
                    this.CurrentPageIndex = 0;
                }
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 0;
            }
            set
            {
                this.ViewState["CurrentPageIndex"] = value;
                this.CurrentPageButtonGroupIndex = value / this.PageButtonGroupSize;
            }
        }

        new public IEnumerable DataSource
        {
            set
            {
                PagedDataSource source1 = new PagedDataSource();
                source1.AllowPaging = this.AllowPaging;
                source1.PageSize = this.PageSize;
                source1.CurrentPageIndex = this.CurrentPageIndex;
                source1.DataSource = value;
                base.DataSource = source1;
                this.PageCount = source1.PageCount;
                this.PageButtonGroupCount = this.PageCount / this.PageButtonGroupSize;
                if ((this.PageCount % this.PageButtonGroupSize) > 0)
                {
                    this.PageButtonGroupCount++;
                }
            }
        }

        [Browsable(false), Category("Appearance"), Description("\u5f53\u524d\u6309\u94ae\u7ec4\u603b\u4e2a\u6570")]
        public virtual int PageButtonGroupCount
        {
            get
            {
                object obj1 = this.ViewState["PageButtonGroupCount"];
                if (obj1 == null)
                {
                    this.PageButtonGroupCount = 0;
                }
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 0;
            }
            set
            {
                this.ViewState["PageButtonGroupCount"] = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("\u6309\u94ae\u4e2a\u6570")]
        public virtual int PageButtonGroupSize
        {
            get
            {
                object obj1 = this.ViewState["PageButtonGroupSize"];
                if (obj1 == null)
                {
                    this.PageButtonGroupSize = 10;
                }
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 10;
            }
            set
            {
                this.ViewState["PageButtonGroupSize"] = value;
            }
        }

        [Browsable(false), Description("总页数"), Category("Appearance")]
        public virtual int PageCount
        {
            get
            {
                object obj1 = this.ViewState["PageCount"];
                if (obj1 == null)
                {
                    this.PageCount = 0;
                }
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 0;
            }
            set
            {
                this.ViewState["PageCount"] = value;
            }
        }

        [Description("分页模式"), Browsable(true), Category("Appearance")]
        public virtual PagerMode PageMode
        {
            get
            {
                object obj1 = this.ViewState["PageMode"];
                if (obj1 == null)
                {
                    this.PageMode = PagerMode.NumericPages;
                }
                if (obj1 != null)
                {
                    return (PagerMode)obj1;
                }
                return PagerMode.NumericPages;
            }
            set
            {
                this.ViewState["PageMode"] = value;
            }
        }

        [Category("Appearance"), Browsable(true), Description("分页按钮的位置")]
        public virtual PagerPosition PagerPosition
        {
            get
            {
                object obj1 = this.ViewState["PagerPosition"];
                if (obj1 == null)
                {
                    this.PagerPosition = PagerPosition.Bottom;
                }
                if (obj1 != null)
                {
                    return (PagerPosition)obj1;
                }
                return PagerPosition.Bottom;
            }
            set
            {
                this.ViewState["PagerPosition"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty), Category("Appearance"), Description("\u5206\u9875\u8282\u7684\u6837\u5f0f\u5c5e\u6027"), Browsable(true)]
        public virtual TableItemStyle PagerStyle
        {
            get
            {
                object obj1 = this.ViewState["PagerStyle"];
                if (obj1 == null)
                {
                    this.ViewState["PagerStyle"] = new TableItemStyle();
                    obj1 = this.ViewState["PagerStyle"];
                }
                return (TableItemStyle)obj1;
            }
            set
            {
                this.ViewState["PagerStyle"] = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("页长")]
        public virtual int PageSize
        {
            get
            {
                object obj1 = this.ViewState["PageSize"];
                if (obj1 == null)
                {
                    this.PageSize = 10;
                }
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 10;
            }
            set
            {
                this.ViewState["PageSize"] = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("上一页按钮文字")]
        public virtual string PrevPageText
        {
            get
            {
                object obj1 = this.ViewState["PrevPageText"];
                if (obj1 == null)
                {
                    this.PrevPageText = "上一页";
                }
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "上一页";
            }
            set
            {
                this.ViewState["PrevPageText"] = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("下一页按钮文字")]
        public virtual string NextPageText
        {
            get
            {
                object obj1 = this.ViewState["NextPageText"];
                if (obj1 == null)
                {
                    this.NextPageText = "下一页";
                }
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "下一页";
            }
            set
            {
                this.ViewState["NextPageText"] = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("首页按钮文字")]
        public virtual string FirstPageText
        {
            get
            {
                object obj1 = this.ViewState["FirstPageText"];
                if (obj1 == null)
                {
                    this.FirstPageText = "首页";
                }
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "首页";
            }
            set
            {
                this.ViewState["FirstPageText"] = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("尾页按钮文字")]
        public virtual string LastPageText
        {
            get
            {
                object obj1 = this.ViewState["LastPageText"];
                if (obj1 == null)
                {
                    this.LastPageText = "尾页";
                }
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "尾页";
            }
            set
            {
                this.ViewState["LastPageText"] = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("“页次”文字")]
        public virtual string PageNumberText
        {
            get
            {
                object obj1 = this.ViewState["PageNumberText"];
                if (obj1 == null)
                {
                    this.PageNumberText = "页次";
                }
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "页次";
            }
            set
            {
                this.ViewState["PageNumberText"] = value;
            }
        }

        [Category("Appearance"), Description("是否显示分页按钮"), Browsable(true), DefaultValue(true)]
        public virtual bool ShowPager
        {
            get
            {
                object obj1 = this.ViewState["ShowPager"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowPager"] = value;
            }
        }


        // Methods
        public ShoveDataList()
        {
        }

        protected virtual void buildPageButton(HtmlTextWriter output)
        {
            int num7;
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            TableCell cell1 = new TableCell();
            if (this.PagerStyle != null)
            {
                cell1.ApplyStyle(this.PagerStyle);
            }
            cell1.RenderBeginTag(output);
            if (this.PageMode == PagerMode.NextPrev)
            {
                // FirstPage
                if (this.CurrentPageIndex > 0)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, 1.ToString()));
                }
                else
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Href, "#");
                    output.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                }
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.Write(this.FirstPageText);
                output.RenderEndTag();
                output.Write("&nbsp;");

                // PrevPage
                if (this.CurrentPageIndex > 0)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, this.CurrentPageIndex.ToString()));
                }
                else
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Href, "#");
                    output.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                }
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.Write(this.PrevPageText);
                output.RenderEndTag();
                output.Write("&nbsp;");

                // NextPage
                if (this.CurrentPageIndex < (this.PageCount - 1))
                {
                    num7 = this.CurrentPageIndex + 2;
                    output.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, num7.ToString()));
                }
                else
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Href, "#");
                    output.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                }
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.Write(this.NextPageText);
                output.RenderEndTag();
                output.Write("&nbsp;");

                //  LastPage
                if (this.CurrentPageIndex < (this.PageCount - 1))
                {
                    num7 = this.PageCount;
                    output.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, num7.ToString()));
                }
                else
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Href, "#");
                    output.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                }
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.Write(this.LastPageText);
                output.RenderEndTag();
                output.Write("&nbsp; &nbsp;");

                // 页次
                output.Write(PageNumberText + "：" + (this.CurrentPageIndex + 1).ToString() + "/" + this.PageCount.ToString());
            }
            else
            {
                if (this.CurrentPageButtonGroupIndex > 0)
                {
                    num7 = ((this.CurrentPageButtonGroupIndex - 1) * this.PageButtonGroupSize) + 1;
                    output.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, num7.ToString()));
                    output.RenderBeginTag(HtmlTextWriterTag.A);
                    output.Write("...");
                    output.RenderEndTag();
                    output.Write(" ");
                }
                int num1 = (this.CurrentPageButtonGroupIndex * this.PageButtonGroupSize) + 1;
                int num2 = this.PageCount;
                int num3 = this.PageButtonGroupSize;
                int num4 = this.CurrentPageIndex;
                int num5 = num1;
                for (int num6 = 0; (num5 <= num2) && (num6 < num3); num6++)
                {
                    if ((num5 - 1) == num4)
                    {
                        output.RenderBeginTag(HtmlTextWriterTag.Span);
                        output.Write(num5.ToString() + " ");
                        output.RenderEndTag();
                    }
                    else
                    {
                        output.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, num5.ToString()));
                        output.RenderBeginTag(HtmlTextWriterTag.A);
                        output.Write(num5.ToString());
                        output.RenderEndTag();
                        output.Write(" ");
                    }
                    num5++;
                }
                if (this.CurrentPageButtonGroupIndex < (this.PageButtonGroupCount - 1))
                {
                    num7 = ((this.CurrentPageButtonGroupIndex + 1) * this.PageButtonGroupSize) + 1;
                    output.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, num7.ToString()));
                    output.RenderBeginTag(HtmlTextWriterTag.A);
                    output.Write("...");
                    output.RenderEndTag();
                }
            }
            cell1.RenderEndTag(output);
            output.RenderEndTag();
        }

        protected virtual void OnPageIndexChanged(DataGridPageChangedEventArgs e)
        {
            if (this.PageIndexChanged != null)
            {
                this.PageIndexChanged(this, e);
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if ((eventArgument != null) && (eventArgument != ""))
            {
                int num1 = int.Parse(eventArgument);
                this.CurrentPageIndex = num1 - 1;
                this.OnPageIndexChanged(new DataGridPageChangedEventArgs(null, num1 - 1));
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.WriteLine();
            writer.Write("<!-- ShoveDataList Start -->");
            //base.RenderBeginTag(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //base.RenderEndTag(writer);
            writer.WriteLine();
            writer.Write("<!-- ShoveDatalist End -->");
            writer.WriteLine();
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            this.RenderBeginTag(output);
            if (this.ShowPager)
            {
                Table table1 = new Table();
                TableRow row1 = new TableRow();
                TableCell cell1 = new TableCell();
                table1.ApplyStyle(base.ControlStyle);
                table1.RenderBeginTag(output);
                if ((this.PagerPosition == PagerPosition.Top) || (this.PagerPosition == PagerPosition.TopAndBottom))
                {
                    this.buildPageButton(output);
                }
                row1.RenderBeginTag(output);
                cell1.RenderBeginTag(output);
                base.RenderContents(output);
                cell1.RenderEndTag(output);
                row1.RenderEndTag(output);
                if ((this.PagerPosition == PagerPosition.Bottom) || (this.PagerPosition == PagerPosition.TopAndBottom))
                {
                    this.buildPageButton(output);
                }
                table1.RenderEndTag(output);
            }
            else
            {
                base.RenderContents(output);
            }
            this.RenderEndTag(output);
        }
    }
}
