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
using Shove.Web.UI;

namespace Shove.Web.UI
{
    public partial class ShoveWebPart_AscxAttribute : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.HiddenField hiControlAttributes;
        protected System.Web.UI.WebControls.HiddenField hiSiteDir;
        protected System.Web.UI.WebControls.HiddenField hiUserID;

        string ControlAttributes = "";

        ShoveWebPartUserControl ascx;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShoveWebPart.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (!string.IsNullOrEmpty(this.Request["SiteDir"]))
            {
                string SiteDir = HttpUtility.UrlDecode(this.Request["SiteDir"]);

                hiSiteDir.Value = SiteDir;
            }

            if (!string.IsNullOrEmpty(this.Request["UserID"]))
            {
                hiUserID.Value = this.Request["UserID"] + "";
            }

            if (!string.IsNullOrEmpty(this.Request["ControlAttributes"]))
            {
                ControlAttributes = this.Request["ControlAttributes"].ToString();

                ViewState["ControlAttributes"] = ControlAttributes;
            }

            if (!string.IsNullOrEmpty(this.Request["AscxFileName"]))
            {
                string fileName = HttpUtility.UrlDecode(this.Request["AscxFileName"].ToString());

                ViewState["AscxFileName"] = fileName;
            }

            InitLayOut(ViewState["ControlAttributes"] + "");

            if (!IsPostBack)
            {
                try
                {
                    ascx = (ShoveWebPartUserControl)Page.LoadControl(ViewState["AscxFileName"] + "");

                    if (ascx != null)
                    {
                        for (int i = 0; i < ascx.swpas.Length; i++)
                        {
                            ascx.swpAttributes.alKey.Add(i);
                            ascx.swpAttributes.alValue.Add(ascx.swpas[i].Value);
                        }

                        //如何属性列表为“”，则直接加载控件的默认属性。
                        if (string.IsNullOrEmpty(ControlAttributes))
                        {
                            hiControlAttributes.Value = ascx.swpAttributes.ToString();

                            for (int Key = 0; Key < ascx.swpAttributes.alKey.Count; Key++)
                            {
                                BindData(ascx.swpAttributes.alKey[Key].ToString(), HttpUtility.UrlDecode(ascx.swpAttributes.alValue[Key].ToString()));
                            }
                        }
                        else
                        {
                            string[] attributes = ControlAttributes.Split('&');

                            foreach (string attribute in attributes)
                            {
                                string[] KeyAndValue = attribute.Split('=');

                                if (KeyAndValue.Length == 2)
                                {
                                    if (ascx.swpAttributes.alKey.Contains(Convert.ToInt32(KeyAndValue[0])))
                                    {
                                        ascx.swpAttributes[Convert.ToInt32(KeyAndValue[0])] = KeyAndValue[1];
                                    }
                                }
                            }

                            hiControlAttributes.Value = ascx.swpAttributes.ToString();

                            for (int Key = 0; Key < ascx.swpAttributes.alKey.Count; Key++)
                            {
                                BindData(ascx.swpAttributes.alKey[Key].ToString(), HttpUtility.UrlDecode(ascx.swpAttributes.alValue[Key].ToString()));
                            }
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 初始化布局
        /// </summary>
        /// <param name="controlAttribute"></param>
        private void InitLayOut(string controlAttribute)
        {
            ArrayList NameList = new ArrayList();

            Control BoolRow = this.Form.FindControl("BoolRow");
            Control EnumRow = this.Form.FindControl("EnumRow");
            Control IntRow = this.Form.FindControl("IntRow");
            Control ColorRow = this.Form.FindControl("ColorRow");
            Control ImageRow = this.Form.FindControl("ImageRow");
            Control PageRow = this.Form.FindControl("PageRow");
            Control TextRow = this.Form.FindControl("TextRow");

            if (ascx == null)
            {
                try
                {
                    ascx = (ShoveWebPartUserControl)Page.LoadControl(ViewState["AscxFileName"] + "");
                }
                catch { }
            }

            if ((ascx == null) || (ascx.swpas == null) || (ascx.swpas.Length == 0))
            {
                Response.Write("<script type=\"text/javascript\">alert('只有继承于 ShoveWebPartUserControl 的控件，并且设置了可以编辑的属性，才能编辑其属性！'); window.opener = null; window.returnValue = null; window.close();</script>");

                return;
            }

            for (int i = 0; i < ascx.swpas.Length; i++)
            {
                ascx.swpAttributes.alKey.Add(i);
                ascx.swpAttributes.alValue.Add(ascx.swpas[i].Value);
            }

            if (ascx.swpAttributes.alKey != null)
            {
                foreach (int Key in ascx.swpAttributes.alKey)
                {
                    switch ((ascx.swpas[Key]).Type)
                    {
                        case "Bool":
                            BoolRow.Visible = true;
                            InitAttribute("Bool", Key);
                            break;
                        case "Enum":
                            EnumRow.Visible = true;
                            InitAttribute("Enum", Key);
                            break;
                        case "Int":
                            IntRow.Visible = true;
                            InitAttribute("Int", Key);
                            break;
                        case "Color":
                            ColorRow.Visible = true;
                            InitAttribute("Color", Key);
                            break;
                        case "Image":
                            ImageRow.Visible = true;
                            InitAttribute("Image", Key);
                            break;
                        case "Page":
                            PageRow.Visible = true;
                            InitAttribute("Page", Key);
                            break;
                        case "Text":
                            TextRow.Visible = true;
                            InitAttribute("Text", Key);
                            break;
                        default: break;
                    }
                }
            }
        }

        /// <summary>
        /// 画属性编辑框
        /// </summary>
        /// <param name="AttributeType"></param>
        /// <param name="Key"></param>
        private void InitAttribute(string AttributeType, int Key)
        {
            switch (AttributeType)
            {
                case "Bool":
                    try
                    {
                        HtmlTable tbBool = (HtmlTable)this.Form.FindControl("tbBool");
                        CreateAttributeRow(tbBool, Key);
                        HtmlTableRow trBool = tbBool.Rows[tbBool.Rows.Count - 1];

                        CheckBox chkAttributeBool = new CheckBox();
                        chkAttributeBool.ID = "chk" + Key;

                        trBool.Cells[1].Controls.Add(chkAttributeBool);
                    }
                    catch { }
                    break;
                case "Enum":
                    try
                    {
                        HtmlTable tbEnum = (HtmlTable)this.Form.FindControl("tbEnum");
                        CreateAttributeRow(tbEnum, Key);
                        HtmlTableRow trEnum = tbEnum.Rows[tbEnum.Rows.Count - 1];

                        DropDownList drpAttributeEnum = new DropDownList();
                        drpAttributeEnum.ID = "drp" + Key;

                        string[] values = (ascx.swpas[Convert.ToInt32(Key)]).Range.Split(',');

                        drpAttributeEnum.Items.Clear();

                        foreach (string ValueOrText in values)
                        {
                            if (ValueOrText.IndexOf("=") < 0)
                            {
                                drpAttributeEnum.Items.Add(new ListItem(ValueOrText, ValueOrText));
                            }
                            else
                            {
                                string[] items = ValueOrText.Split('=');
                                object value = null;

                                if (items.Length != 2)
                                {
                                    continue;
                                }
                                if (items[1].StartsWith("V"))
                                {
                                    value = PublicFunction.VerticalAlignFromString(items[1].Substring(1));
                                }
                                else if (items[1].StartsWith("H"))
                                {
                                    value = PublicFunction.HorizontalAlignFromString(items[1].Substring(1));
                                }
                                else if (items[1].StartsWith("B"))
                                {
                                    value = PublicFunction.BorderStyleFromString(items[1].Substring(1));
                                }
                                drpAttributeEnum.Items.Add(new ListItem(items[0], value.ToString()));
                            }
                        }

                        trEnum.Cells[1].Controls.Add(drpAttributeEnum);
                    }
                    catch { }
                    break;
                default:
                    try
                    {
                        HtmlTable table = new HtmlTable();

                        if (AttributeType == "Int")
                        {
                            table = (HtmlTable)this.Form.FindControl("tbInt");
                        }
                        else if (AttributeType == "Color")
                        {
                            table = (HtmlTable)this.Form.FindControl("tbColor");
                        }
                        else if (AttributeType == "Image")
                        {
                            table = (HtmlTable)this.Form.FindControl("tbImage");
                        }
                        else if (AttributeType == "Page")
                        {
                            table = (HtmlTable)this.Form.FindControl("tbPage");
                        }
                        else if (AttributeType == "Text")
                        {
                            table = (HtmlTable)this.Form.FindControl("tbText");
                        }

                        CreateAttributeRow(table, Key);
                        HtmlTableRow tr = table.Rows[table.Rows.Count - 1];

                        TextBox txtAttributeValue = new TextBox();
                        txtAttributeValue.ID = "txt" + Key;

                        tr.Cells[1].Controls.Add(txtAttributeValue);

                        if (!IsPostBack)
                        {
                            HtmlButton btn = new HtmlButton();
                            btn.ID = "btn" + Key;
                            btn.Style.Add(HtmlTextWriterStyle.Width, "18px");
                            btn.Style.Add(HtmlTextWriterStyle.Height, "22px");
                            btn.Style.Add(HtmlTextWriterStyle.Left, "180px");

                            btn.InnerText = "...";
                            btn.Attributes.Add("name", "btn" + Key);

                            if (AttributeType == "Color")
                            {
                                btn.Attributes.Add("onclick", "return btnColor_onclick(" + Key + ")");
                                tr.Cells[1].Controls.Add(btn);
                            }
                            else if (AttributeType == "Image")
                            {
                                btn.Attributes.Add("onclick", "btnImg_onclick(" + Key + ")");
                                tr.Cells[1].Controls.Add(btn);
                            }
                        }
                    }
                    catch { }
                    break;

            }
        }

        /// <summary>
        /// 初始化绑定数据
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        private void BindData(string Key, string value)
        {
            string AttributeType = (ascx.swpas[Convert.ToInt32(Key)]).Type;

            switch (AttributeType)
            {
                case "Bool":
                    try
                    {
                        CheckBox chk = (CheckBox)this.Form.FindControl("chk" + Key);
                        chk.Checked = (value == "true" ? true : false);
                    }
                    catch { }
                    break;
                case "Enum":
                    try
                    {
                        DropDownList drp = (DropDownList)this.Form.FindControl("drp" + Key);
                        drp.SelectedValue = value;
                    }
                    catch { }
                    break;
                default:
                    try
                    {
                        TextBox txt = (TextBox)this.Form.FindControl("txt" + Key);
                        txt.Text = value;
                    }
                    catch { }
                    break;
            }
        }

        /// <summary>
        /// 创建属性行
        /// </summary>
        /// <param name="table"></param>
        /// <param name="Key"></param>
        private void CreateAttributeRow(HtmlTable table, int Key)
        {
            HtmlTableRow tr = new HtmlTableRow();
            table.Rows.Add(tr);

            HtmlTableCell td1 = new HtmlTableCell();
            HtmlTableCell td2 = new HtmlTableCell();
            HtmlTableCell td3 = new HtmlTableCell();
            tr.Cells.Add(td1);
            tr.Cells.Add(td2);
            tr.Cells.Add(td3);

            td1.Style.Add(HtmlTextWriterStyle.Width, "210");
            td1.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
            td1.Attributes.Add("class", "sdw_edit");
            td1.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + (ascx.swpas[Key]).Name;

            td2.Style.Add(HtmlTextWriterStyle.Width, "200px");
            td2.InnerHtml = "&nbsp;";
            td2.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
            td2.Attributes.Add("class", "sdw_edit");

            td3.Style.Add(HtmlTextWriterStyle.Width, "80px");
            td3.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
            td3.Attributes.Add("class", "sdw_edit");
            td3.InnerHtml = "&nbsp;<img src=\"../Images/shuoming.jpg\" alt=" + (ascx.swpas[Key]).Vote + " />";
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (ascx == null)
            {
                try
                {
                    ascx = (ShoveWebPartUserControl)Page.LoadControl(ViewState["AscxFileName"] + "");
                }
                catch { }
            }
            if (ascx != null)
            {
                for (int Key = 0; Key < ascx.swpAttributes.alKey.Count; Key++)
                {
                    SaveData(Key);
                }
            }

            ControlAttributes = ascx.swpAttributes.ToString();

            string str = "<script>window.returnValue=\'" + ControlAttributes + "\';window.parent.close();</script>";

            this.ClientScript.RegisterClientScriptBlock(GetType(), "ShoveWebPart_back", str);
        }

        /// <summary>
        /// 给属性重新赋值
        /// </summary>
        /// <param name="Key"></param>
        private void SaveData(int Key)
        {
            string type = (ascx.swpas[Key]).Type;

            if (type != "")
            {
                if (type == "Bool")
                {
                    try
                    {
                        CheckBox cb = (CheckBox)this.Form.FindControl("chk" + Key.ToString());
                        ascx.swpAttributes[Key] = (cb.Checked == true ? "true" : "false");
                    }
                    catch { }
                }
                else if (type == "Enum")
                {
                    try
                    {
                        DropDownList ddl = (DropDownList)this.Form.FindControl("drp" + Key.ToString());
                        ascx.swpAttributes[Key] = ddl.SelectedValue;
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        TextBox tb = (TextBox)this.Form.FindControl("txt" + Key.ToString());
                        if ((ascx.swpas[Key]).Type == "Image" && tb.Text == "")
                        {
                            ascx.swpAttributes[Key] = "about:blank";
                        }
                        else
                        {
                            ascx.swpAttributes[Key] = tb.Text;
                        }
                    }
                    catch { }
                }
            }
        }
    }
}