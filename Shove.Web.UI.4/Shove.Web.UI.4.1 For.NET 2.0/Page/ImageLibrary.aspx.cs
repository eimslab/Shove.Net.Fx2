using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace Shove.Web.UI
{
    public partial class ImageLibrary : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TreeView tvImgDir;
        protected System.Web.UI.WebControls.LinkButton lnkFirst;
        protected System.Web.UI.WebControls.LinkButton lnkPrev;
        protected System.Web.UI.WebControls.LinkButton lnkNext;
        protected System.Web.UI.WebControls.LinkButton lnkLast;

        protected System.Web.UI.WebControls.DataList ImageList;
        protected System.Web.UI.WebControls.FileUpload fuUpLoadImg;
        protected System.Web.UI.WebControls.Label lblMsg;
        protected System.Web.UI.WebControls.Label lblDeleteInfo;
        protected System.Web.UI.WebControls.Label lblNav;
        protected System.Web.UI.WebControls.Label lblTotalPage;
        protected System.Web.UI.WebControls.TextBox tbPage;

        protected System.Web.UI.HtmlControls.HtmlTable tableUpLoadImg;

        protected System.Web.UI.HtmlControls.HtmlTable tableUpLoadElement;
        protected System.Web.UI.WebControls.DropDownList ddlImgTypes;
        protected System.Web.UI.WebControls.TextBox tbElementName;
        protected System.Web.UI.WebControls.TextBox tbElementWidth;
        protected System.Web.UI.WebControls.TextBox tbElementHeight;
        protected System.Web.UI.WebControls.TextBox tbDescription;
        protected System.Web.UI.WebControls.FileUpload fuUpLoadElement;
        protected System.Web.UI.WebControls.Label lblMsg2;

        protected System.Web.UI.WebControls.TextBox tbKey;
        protected System.Web.UI.WebControls.Label lblMsg3;

        string SiteDir = "";
        string UserID = "";

        public DataTable table;

        private int rows = 8;
        private int column = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (!string.IsNullOrEmpty(this.Request["SiteDir"]))
            {
                SiteDir = this.Request["SiteDir"] + "";
            }

            if (!string.IsNullOrEmpty(this.Request["UserID"]))
            {
                UserID = this.Request["UserID"] + "";
            }

            if (!IsPostBack)
            {
                TreeNode tn = new TreeNode("图片元素库", "Images/UserControl/");

                HttpContext.Current.Session["currPage"] = 1;

                TreeNode tn1 = new TreeNode("系统元素库", "Images/UserControl/");
                TreeNode tn2 = new TreeNode("站点元素库", SiteDir);

                tvImgDir.Nodes.Add(tn);
                tn.ChildNodes.Add(tn1);
                tn.ChildNodes.Add(tn2);

                if (UserID != "")
                {
                    TreeNode tn3 = new TreeNode("用户元素库", "UserElements/" + UserID + "/");

                    FindUserElementDir(tn3);
                    tn.ChildNodes.Add(tn3);

                    AddElementTypes();
                }

                tvImgDir.Nodes[0].ChildNodes[0].Selected = true;
                tvImgDir.SelectedNodeStyle.Font.Bold = true;
                lblNav.Text = " > " + tvImgDir.SelectedNode.Text;

                GetImages(tvImgDir.SelectedNode.Value);

                GetCurrentPageImages(1);
            }
        }

        /// <summary>
        /// 查找用户元素目录
        /// </summary>
        /// <param name="tn"></param>
        private void FindUserElementDir(TreeNode tn)
        {
            string ImageDirList = PublicFunction.GetWebConfigAppSettingAsString("ShovePart2UserElementDirectory");

            TreeNode ChildNode = null;

            string[] Dirs = null;

            if (ImageDirList != "" && ImageDirList.Contains(","))
            {
                tvImgDir.Nodes.Add(tn); //如果含有元素目录则显示【用户元素库】节点

                Dirs = ImageDirList.Split(',');

                for (int i = 0; i < Dirs.Length; i++)
                {
                    string Text = Dirs[i].Substring(Dirs[i].LastIndexOf("/") + 1) + "元素库";

                    string Value = Dirs[i].Replace("[UserID]", UserID);

                    if (Value.StartsWith("~/"))
                    {
                        Value = Value.Substring(2, Value.Length - 2) + "/";
                    }

                    ChildNode = new TreeNode();
                    ChildNode.Text = Text;
                    ChildNode.Value = Value;

                    tn.ChildNodes.Add(ChildNode);
                }
            }
        }

        /// <summary>
        /// 添加用户元素类别
        /// </summary>
        private void AddElementTypes()
        {
            string ElementTypeIDList = PublicFunction.GetWebConfigAppSettingAsString("ShovePart2UserElementTypeIDList");

            ddlImgTypes.Items.Clear();
            ddlImgTypes.Items.Add(new ListItem("===请选择元素类型===", "-1"));

            if (ElementTypeIDList != "" && ElementTypeIDList.Contains(","))
            {
                string[] List = ElementTypeIDList.Split(',');

                for (int i = 0; i < List.Length; i++)
                {
                    string[] TypeAndID = List[i].Split('=');

                    if (TypeAndID == null || TypeAndID.Length != 2)
                    {
                        continue;
                    }

                    ddlImgTypes.Items.Add(new ListItem(TypeAndID[0], TypeAndID[1]));
                }
            }
        }

        /// <summary>
        /// 获取目录下所有图片元素
        /// </summary>
        /// <param name="ImgDir"></param>
        private void GetImages(string ImgDir)
        {
            table = new DataTable();
            DataSet obj = new DataSet();

            DataColumnCollection columns = table.Columns;
            columns.Add("imgUrl", typeof(string));
            columns.Add("imgName", typeof(string));

            if (ImgDir.StartsWith("~/"))
            {
                ImgDir = ImgDir.Substring(2, ImgDir.Length - 2) + "/";
            }

            string Path = "../../" + ImgDir;

            try
            {
                if (Directory.Exists(this.Server.MapPath(Path)))
                {
                    string[] images = null;

                    if (tvImgDir.SelectedNode.Text == "用户元素库")
                    {
                        for (int i = 0; i < tvImgDir.SelectedNode.ChildNodes.Count; i++)
                        {
                            images = Directory.GetFiles(this.Server.MapPath("../../" + tvImgDir.SelectedNode.ChildNodes[i].Value));

                            AddToTable(table, images, "../../" + tvImgDir.SelectedNode.ChildNodes[i].Value);
                        }
                    }
                    else
                    {
                        images = Directory.GetFiles(this.Server.MapPath(Path));

                        AddToTable(table, images, Path);
                    }
                }
            }
            catch { }

            obj.Tables.Add(table);

            HttpContext.Current.Session["set"] = obj;

            HttpContext.Current.Session["totalPages"] = table.Rows.Count % (rows * column) == 0 ? table.Rows.Count / (rows * column) : table.Rows.Count / (rows * column) + 1;

            lblTotalPage.Text = HttpContext.Current.Session["totalPages"] + "";

        }

        private void AddToTable(DataTable table, string[] images, string Path)
        {
            foreach (string image in images)
            {
                if (image.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) || image.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || image.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) || image.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || image.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) || image.EndsWith(".swf", StringComparison.OrdinalIgnoreCase) || image.EndsWith(".psd", StringComparison.OrdinalIgnoreCase) || image.EndsWith(".fla", StringComparison.OrdinalIgnoreCase))
                {
                    string[] arry = image.Split('\\');

                    DataRow row = table.NewRow();
                    row["imgUrl"] = Path + arry[arry.Length - 1];
                    row["imgName"] = arry[arry.Length - 1];

                    table.Rows.Add(row);
                }
            }
        }

        /// <summary>
        /// 获取当前页图片
        /// </summary>
        /// <param name="currPage"></param>
        private void GetCurrentPageImages(int currPage)
        {
            DataTable dt = new DataTable();

            DataColumnCollection columns = dt.Columns;
            columns.Add("imgUrl", typeof(string));
            columns.Add("imgName", typeof(string));

            PublicFunction.DisPage((DataSet)HttpContext.Current.Session["set"], dt, currPage, rows, column);

            ImageList.DataSource = dt;
            ImageList.DataBind();

            PublicFunction.isDisplay(lnkFirst, lnkPrev, lnkNext, lnkLast, Convert.ToInt32(HttpContext.Current.Session["currPage"]), Convert.ToInt32(HttpContext.Current.Session["totalPages"]));
        }

        /// <summary>
        /// 分页显示按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Click(object sender, CommandEventArgs e)
        {
            LinkButton btn = sender as LinkButton;

            switch (btn.CommandArgument.ToString())
            {
                case "first":
                    HttpContext.Current.Session["currPage"] = 1;
                    break;

                case "next":
                    if (Convert.ToInt32(HttpContext.Current.Session["currPage"]) + 1 >= Convert.ToInt32(HttpContext.Current.Session["totalPages"]))
                    {
                        HttpContext.Current.Session["currPage"] = Convert.ToInt32(HttpContext.Current.Session["totalPages"]);
                    }
                    else
                    {
                        HttpContext.Current.Session["currPage"] = Convert.ToInt32(HttpContext.Current.Session["currPage"]) + 1;
                    }
                    break;

                case "prev":
                    if (Convert.ToInt32(HttpContext.Current.Session["currPage"]) - 1 <= 1)
                    {
                        HttpContext.Current.Session["currPage"] = 1;
                    }
                    else
                    {
                        HttpContext.Current.Session["currPage"] = Convert.ToInt32(HttpContext.Current.Session["currPage"]) - 1;
                    }
                    break;

                case "last":
                    HttpContext.Current.Session["currPage"] = Convert.ToInt32(HttpContext.Current.Session["totalPages"]);

                    break;

                default: break;
            }

            GetCurrentPageImages(Convert.ToInt32(HttpContext.Current.Session["currPage"]));
        }

        /// <summary>
        /// 选择图片目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvImgDir_SelectedNodeChanged(object sender, EventArgs e)
        {
            string nodeValue = tvImgDir.SelectedValue;

            tvImgDir.SelectedNodeStyle.Font.Bold = true;

            lblNav.Text = " > " + tvImgDir.SelectedNode.Text;

            if (tvImgDir.SelectedNode.Text == "系统元素库" || tvImgDir.SelectedNode.Text == "图片元素库")
            {
                tableUpLoadImg.Visible = false;
                tableUpLoadElement.Visible = false;
            }
            else if (tvImgDir.SelectedNode.Text == "站点元素库")
            {
                tableUpLoadImg.Visible = true;
                tableUpLoadElement.Visible = false;
            }
            else if (tvImgDir.SelectedNode.Text == "用户元素库" || tvImgDir.SelectedNode.Parent.Text == "用户元素库")
            {
                tableUpLoadImg.Visible = false;
                tableUpLoadElement.Visible = true;
                ddlImgTypes.SelectedValue = "-1";

                if (tvImgDir.SelectedNode.Text == "用户元素库")
                {
                    lblNav.Text = " > " + tvImgDir.SelectedNode.Text;
                }
                else
                {
                    lblNav.Text = " > " + tvImgDir.SelectedNode.Parent.Text + " > " + tvImgDir.SelectedNode.Text;
                }
            }

            GetImages(nodeValue);

            GetCurrentPageImages(1);
        }

        /// <summary>
        /// 全选功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSelectAll_Click(object sender, ImageClickEventArgs e)
        {
            if (tvImgDir.SelectedNode.Text == "系统元素库")
            {
                Shove.Web.UI.JavaScript.Alert(this, "您没有权限全选系统图片！");

                return;
            }

            foreach (DataListItem item in ImageList.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("chk");

                if (cb != null)
                {
                    cb.Checked = true;
                }
            }
        }

        /// <summary>
        /// 删除用户图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (tvImgDir.SelectedNode.Text == "系统元素库" || tvImgDir.SelectedNode.Text == "用户元素库" || tvImgDir.SelectedNode.Parent.Text == "用户元素库")
            {
                Shove.Web.UI.JavaScript.Alert(this, "您没有权限删除（系统/用户）图片！");

                return;
            }

            ArrayList imgList = new ArrayList();

            foreach (DataListItem item in ImageList.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("chk");

                if (cb.Checked)
                {
                    imgList.Add(cb.CssClass);
                }
            }
            if (imgList.Count > 0)
            {
                for (int i = 0; i < imgList.Count; i++)
                {
                    if (System.IO.File.Exists(Server.MapPath(imgList[i].ToString())))
                    {
                        System.IO.File.Delete(Server.MapPath(imgList[i].ToString()));
                    }
                }

                lblDeleteInfo.Text = "删除图片成功！";

                GetImages(tvImgDir.SelectedValue);

                GetCurrentPageImages(1);
            }
            else
            {
                lblDeleteInfo.Text = "请选择要删除的图片！";

                return;
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibUpLoad_Click(object sender, ImageClickEventArgs e)
        {
            lblMsg.Text = "";
            try
            {
                if (fuUpLoadImg.HasFile)
                {
                    if (".gif.png.bmp.jpg.jpeg.swf".IndexOf(Path.GetExtension(fuUpLoadImg.FileName).ToLower()) < 0)
                    {
                        lblMsg.Text = "文件后缀名错误！";

                        return;
                    }
                    else
                    {
                        if (!File.ValidFileType(fuUpLoadImg, "image,flash"))
                        {
                            lblMsg.Text = "文件类型错误！";

                            return;
                        }

                        fuUpLoadImg.SaveAs(Server.MapPath("../../" + SiteDir + fuUpLoadImg.FileName));

                        lblMsg.Text = "图片上传成功！";
                    }
                }
                else
                {
                    lblMsg.Text = "请选择要上传的图片！";

                    return;
                }
            }
            catch { }

            GetImages(tvImgDir.Nodes[0].ChildNodes[1].Value);

            GetCurrentPageImages(1);
        }

        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Submit_Click(object sender, ImageClickEventArgs e)
        {
            ArrayList imgList = new ArrayList();

            foreach (DataListItem item in ImageList.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("chk");

                if (cb.Checked)
                {
                    imgList.Add(cb.CssClass);
                }
            }

            if (imgList.Count != 1)
            {
                lblDeleteInfo.Text = "请选择一张图片！";

                return;
            }
            else
            {
                string str = "<script>window.returnValue=\'" + imgList[0].ToString().Substring(6) + "\';window.parent.close();</script>";

                this.ClientScript.RegisterClientScriptBlock(GetType(), "ShovePart2_Img", str);
            }
        }

        /// <summary>
        /// 跳到某一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pageSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (tbPage.Text != "")
            {
                int Number = -1;

                try
                {
                    Number = int.Parse(tbPage.Text);
                }
                catch { }

                if (Number < 0)
                {
                    lblDeleteInfo.Text = "请输入有效的数字！";

                    return;
                }
                else
                {
                    GetCurrentPageImages(Number);
                }
            }
            else
            {
                lblDeleteInfo.Text = "请输入页码！";

                return;
            }
        }

        protected void ibUpLoadElement_Click(object sender, ImageClickEventArgs e)
        {
            lblMsg2.Text = "";

            if (ddlImgTypes.SelectedItem.Value == "-1")
            {
                lblMsg2.Text = "请选择元素类型";

                return;
            }

            if (tbElementName.Text.Trim() == "")
            {
                lblMsg2.Text = "请输入元素名称";

                return;
            }

            int width = 0;
            int height = 0;

            try
            {
                width = Convert.ToInt32(tbElementWidth.Text.Trim());
                height = Convert.ToInt32(tbElementHeight.Text.Trim());

                if (width <= 0 || height <= 0)
                {
                    lblMsg2.Text = "请输入正确的元素宽度、高度";

                    return;
                }
            }
            catch
            {
                lblMsg2.Text = "请输入正确的元素宽度、高度";

                return;
            }

            if (tbDescription.Text.Trim() == "")
            {
                lblMsg2.Text = "请输入元素描述";

                return;
            }

            if (!fuUpLoadElement.HasFile)
            {
                lblMsg2.Text = "必须选择要上传的元素文件";

                return;
            }

            if (!fuUpLoadElement.FileName.EndsWith(ddlImgTypes.SelectedItem.Text, StringComparison.OrdinalIgnoreCase))
            {
                lblMsg2.Text = "元素文件不是" + ddlImgTypes.SelectedItem.Text + "文件类型";

                return;
            }

            string UpLoadPath = Server.MapPath("../../" + tvImgDir.SelectedNode.Value + fuUpLoadElement.FileName);

            try
            {
                if (System.IO.File.Exists(UpLoadPath))
                {
                    lblMsg2.Text = "该文件夹下存在同名元素，请重命名后上传";

                    return;
                }

                if (!File.ValidFileType(fuUpLoadElement, "image,flash"))
                {
                    lblMsg2.Text = "文件类型错误！";

                    return;
                }

                fuUpLoadElement.SaveAs(UpLoadPath);
            }
            catch { }

            InsertToDataBase();

            GetImages(tvImgDir.SelectedNode.Value);

            GetCurrentPageImages(1);
        }

        protected void ddlImgTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < tvImgDir.Nodes[0].ChildNodes[2].ChildNodes.Count; i++)
            {
                if (tvImgDir.Nodes[0].ChildNodes[2].ChildNodes[i].Text.Contains(ddlImgTypes.SelectedItem.Text))
                {
                    tvImgDir.Nodes[0].ChildNodes[2].ChildNodes[i].Selected = true;
                }
            }

            GetImages(tvImgDir.SelectedNode.Value);

            GetCurrentPageImages(1);

            tableUpLoadElement.Visible = true;
        }

        private void InsertToDataBase()
        {
            string Sql = PublicFunction.GetWebConfigAppSettingAsString("ShovePart2UserElementInsertSQL");

            SqlConnection con = new SqlConnection(PublicFunction.GetWebConfigAppSettingAsString("ConnectionString"));

            try
            {
                Sql = Sql.Replace("[TypeID]", ddlImgTypes.SelectedValue);
                Sql = Sql.Replace("[OwnerID]", UserID);
                Sql = Sql.Replace("[Name]", "'" + tbElementName.Text + "'");
                Sql = Sql.Replace("[Width]", tbElementWidth.Text);
                Sql = Sql.Replace("[Height]", tbElementHeight.Text);
                Sql = Sql.Replace("[Status]", 1 + "");
                Sql = Sql.Replace("[FileUrl]", "'" + fuUpLoadElement.FileName + "'");
                Sql = Sql.Replace("[Description]", "'" + tbDescription.Text + "'");

                SqlCommand com = new SqlCommand(Sql, con);

                con.Open();

                if (com.ExecuteNonQuery() > 0)
                {
                    lblMsg2.Text = "元素上传成功！";
                }
            }
            catch
            {
                con.Close();

                lblMsg2.Text = "元素上失败！";
            }
            finally
            {
                con.Close();
            }
        }

        protected void ibSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (tbKey.Text.Trim() == "")
            {
                lblMsg3.Text = "请输入关键字";

                return;
            }

            if (HttpContext.Current.Session["set"] != null)
            {
                DataSet ds = (DataSet)HttpContext.Current.Session["set"];

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataRow[] drs = ds.Tables[0].Select("imgName like '" + tbKey.Text.Trim() + "%'");

                    DataTable dt = new DataTable();
                    DataColumnCollection columns = dt.Columns;
                    columns.Add("imgUrl", typeof(string));
                    columns.Add("imgName", typeof(string));

                    if (drs != null)
                    {
                        foreach (DataRow row in drs)
                        {
                            DataRow r = dt.NewRow();
                            r["imgUrl"] = row["Imgurl"];
                            r["imgName"] = row["imgName"];

                            dt.Rows.Add(r);
                        }
                    }

                    DataSet obj = new DataSet();
                    obj.Tables.Add(dt);

                    HttpContext.Current.Session["searchset"] = obj;

                    HttpContext.Current.Session["totalPages"] = dt.Rows.Count % (rows * column) == 0 ? dt.Rows.Count / (rows * column) : dt.Rows.Count / (rows * column) + 1;

                    lblTotalPage.Text = HttpContext.Current.Session["totalPages"] + "";

                    GetSearchImages(1);
                }
            }

            tbKey.Text = "";
        }

        /// <summary>
        /// 获取搜索图片
        /// </summary>
        /// <param name="currPage"></param>
        private void GetSearchImages(int currPage)
        {
            DataTable dt = new DataTable();

            DataColumnCollection columns = dt.Columns;
            columns.Add("imgUrl", typeof(string));
            columns.Add("imgName", typeof(string));

            PublicFunction.DisPage((DataSet)HttpContext.Current.Session["searchset"], dt, currPage, rows, column);

            ImageList.DataSource = dt;
            ImageList.DataBind();

            PublicFunction.isDisplay(lnkFirst, lnkPrev, lnkNext, lnkLast, Convert.ToInt32(HttpContext.Current.Session["currPage"]), Convert.ToInt32(HttpContext.Current.Session["totalPages"]));
        }
    }
}