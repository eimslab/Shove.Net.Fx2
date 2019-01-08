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

namespace Shove.Web.UI
{
    public partial class UpLoadStyle : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.FileUpload FileUpload1;
        protected System.Web.UI.WebControls.Label MessageBox;
        protected System.Web.UI.WebControls.TextBox tbSiteID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden tbResult;
        protected System.Web.UI.WebControls.HyperLink hlDownload;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (!this.IsPostBack)
            {
                long SiteID = -1;

                try
                {
                    SiteID = long.Parse(Page.Request["SiteID"]);
                }
                catch { }

                if (SiteID < 0)
                {
                    this.Response.Write("参数错误！");
                    this.Response.End();

                    return;
                }

                tbSiteID.Text = SiteID.ToString();
                hlDownload.NavigateUrl = "Download.aspx?FileName=" + HttpUtility.UrlEncode(this.Server.MapPath("../Style/" + SiteID.ToString() + ".css"));
            }
        }

        /// <summary>
        /// 上传样式表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            long SiteID = -1;

            try
            {
                SiteID = long.Parse(tbSiteID.Text);
            }
            catch { }

            if (SiteID < 0)
            {
                this.Response.Write("参数错误！");
                this.Response.End();

                return;
            }

            bool falg = false;

            string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();

            if (!fileExtension.Equals(""))
            {
                if (FileUpload1.HasFile)
                {
                    if (fileExtension.Equals(".css"))
                    {
                        falg = true;
                    }
                }

                if (falg == true)
                {
                    if (FileUpload1.PostedFile.ContentLength >= 2048000000)
                    {
                        MessageBox.Text = "文件大小应小于2M！";

                        return;
                    }

                    if (System.IO.File.Exists(Server.MapPath("../Style/" + SiteID.ToString() + ".css")))
                    {
                        System.IO.File.Delete(Server.MapPath("../Style/" + SiteID.ToString() + ".css"));
                    }

                    //if (!File.ValidFileType(FileUpload1, "css"))
                    //{
                    //    this.MessageBox.Text = "文件类型错误！";

                    //    return;
                    //}

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("../Style/" + SiteID.ToString() + ".css"));

                    this.MessageBox.Text = "文件上传成功！";

                    tbResult.Value = "Uploaded";
                }
                else
                {
                    this.MessageBox.Text = "文件后缀名错误！";
                }
            }
            else
            {
                this.MessageBox.Text = "请选择要上传的文件！";
            }
        }
    }
}