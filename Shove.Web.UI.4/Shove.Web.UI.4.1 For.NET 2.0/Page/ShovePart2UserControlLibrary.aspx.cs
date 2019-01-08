using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Shove.Web.UI;

namespace Shove.Web.UI
{
    /// <summary>
    /// 控件库
    /// </summary>
    public partial class ShovePart2UserControlLibrary : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DropDownList ddlControlTypes;
        protected System.Web.UI.WebControls.TreeView tvUserControlsDir;
        protected System.Web.UI.WebControls.GridView gvShovePart2UserControls;
        protected System.Web.UI.WebControls.Label lblMsg;
        protected System.Web.UI.WebControls.Label lblUserControlType;
        protected System.Web.UI.WebControls.Label lblNav;

        private DataSet data = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (!IsPostBack)
            {
                ddlControlTypes.Items.Clear();

                ddlControlTypes.Items.Add(new ListItem("系统控件库", "1"));
                ddlControlTypes.Items.Add(new ListItem("用户控件库", "2"));
                ddlControlTypes.Items.Add(new ListItem("用户共享控件库", "3"));

                lblUserControlType.Text = "系统控件库";

                tvUserControlsDir.CollapseAll();

                if (this.Page.Request["UserID"] != null)
                {
                    try
                    {
                        this.Session["UserID"] = Convert.ToInt32(this.Page.Request["UserID"]);
                    }
                    catch { }
                }

                data = GetApplicationSet();

                BindTreeView();

                BindData();
            }
        }

        private DataSet GetApplicationSet()
        {
            string SystemPreFix = PublicFunction.GetWebConfigAppSettingAsString("SystemPreFix");

            string DirPath = PublicFunction.GetWebConfigAppSettingAsString("ShovePart2UserControlDirectory");

            DataSet ds = null;

            if (DirPath != "")
            {
                if (DirPath == "ShovePart2UserControlsAndTypeDataSet")
                {
                    try
                    {
                        ds = (DataSet)HttpContext.Current.Application[SystemPreFix + "ShovePart2UserControlsAndTypeDataSet" + Session["UserID"]];
                    }
                    catch { }
                }
            }

            return ds;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            DataTable dt = null;

            if (data == null)
            {
                data = GetApplicationSet();
            }

            if (data != null && data.Tables.Count == 4)
            {
                if (int.Parse(ddlControlTypes.SelectedValue) < 1 || int.Parse(ddlControlTypes.SelectedValue) > 3)
                {
                    dt = data.Tables[1];
                }
                else
                {
                    dt = data.Tables[int.Parse(ddlControlTypes.SelectedValue)];
                }

                gvShovePart2UserControls.DataSource = dt;
                gvShovePart2UserControls.DataBind();
            }
        }

        /// <summary>
        /// 获取该类型下所有控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlControlTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();

            lblUserControlType.Text = ddlControlTypes.Items[ddlControlTypes.SelectedIndex].Text;
            lblNav.Text = "";
        }

        /// <summary>
        /// 分页显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvShovePart2UserControls_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShovePart2UserControls.PageIndex = e.NewPageIndex;

            BindData();
        }

        /// <summary>
        /// 选择控件目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvUserControlsDir_SelectedNodeChanged(object sender, EventArgs e)
        {
            string nodeValue = tvUserControlsDir.SelectedValue;

            lblNav.Text = tvUserControlsDir.SelectedNode.Text;

            DataTable CurrData = new DataTable();

            if (data == null)
            {
                data = GetApplicationSet();
            }

            if ((data != null) && (data.Tables.Count == 4))
            {
                if (int.Parse(ddlControlTypes.SelectedValue) < 1 || int.Parse(ddlControlTypes.SelectedValue) > 3)
                {
                    CurrData = GetUserControls(data.Tables[1], int.Parse(nodeValue));
                }
                else
                {
                    CurrData = GetUserControls(data.Tables[int.Parse(ddlControlTypes.SelectedValue)], int.Parse(nodeValue));
                }
            }

            gvShovePart2UserControls.DataSource = CurrData;
            gvShovePart2UserControls.DataBind();
        }

        private DataTable GetUserControls(DataTable dt, int TypeID)
        {
            DataTable newTable = dt.Clone();

            foreach (DataRow dr in dt.Rows)
            {
                if ((dr["TypeID"].ToString() == TypeID.ToString()) || (dr["ParentIDList"].ToString().Contains("," + TypeID.ToString() + ",")))
                {
                    DataRow newRow = newTable.NewRow();

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        newRow[dt.Columns[i].ColumnName] = dr[dt.Columns[i].ColumnName];
                    }

                    newTable.Rows.Add(newRow);
                }
            }

            return newTable;
        }

        private void BindTreeView()
        {
            tvUserControlsDir.Nodes.Clear();

            DataRow[] drs = data.Tables[0].Select("ParentID" + "= -1");

            foreach (DataRow dr in drs)
            {
                TreeNode tn = new TreeNode(dr["Name"] + "", dr["ID"] + "");// BuildNewTreeNode(dr);
                tvUserControlsDir.Nodes.Add(tn);

                AddChildNodes(tn, dr["ID"].ToString());
            }
        }

        private void AddChildNodes(TreeNode tn, string ParentID)
        {
            DataRow[] drs = data.Tables[0].Select("ParentID" + "=" + ParentID);

            foreach (DataRow dr in drs)
            {
                TreeNode child_tn = new TreeNode(dr["Name"] + "", dr["ID"] + "");//BuildNewTreeNode(dr);
                tn.ChildNodes.Add(child_tn);

                AddChildNodes(child_tn, dr["ID"].ToString());
            }
        }

        /// <summary>
        /// 控件选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibUserCotrol_Click(object sender, ImageClickEventArgs e)
        {
            ArrayList FileUrl = new ArrayList();

            foreach (GridViewRow row in gvShovePart2UserControls.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("cbSelect");

                if (cb.Checked)
                {
                    FileUrl.Add(cb.ToolTip);
                }
            }

            if (FileUrl.Count != 1)
            {
                lblMsg.Text = "请选择一个控件!";

                return;
            }
            else
            {
                string str = "<script>var ReturnValue=\'" + FileUrl[0].ToString() + "\'; window.opener.document.all.ddlAscxFileName.value =ReturnValue ; window.parent.close();</script>";

                this.ClientScript.RegisterClientScriptBlock(GetType(), "ShovePart2_UserControlFileUrl", str);
            }
        }
    }
}