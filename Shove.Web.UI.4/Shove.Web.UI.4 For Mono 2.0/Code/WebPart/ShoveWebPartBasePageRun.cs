using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPart基页
    /// </summary>
    public class ShoveWebPartBasePageRun : System.Web.UI.Page
    {
        /// <summary>
        /// 重写页面加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPart", this.Page.ClientScript.GetWebResourceUrl(typeof(ShoveWebPart), "Shove.Web.UI.Script.ShoveWebPart.js"));

            #region 加载缓存

            int SitePageCacheSeconds = -1;

            try
            {
                SitePageCacheSeconds = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["SitePageCacheSeconds"]);
            }
            catch { }

            if (SitePageCacheSeconds > 0)
            {
                this.Response.Cache.SetExpires(DateTime.Now.AddSeconds(SitePageCacheSeconds));
                this.Response.Cache.SetCacheability(HttpCacheability.Server);
                this.Response.Cache.VaryByParams["*"] = true;
                this.Response.Cache.SetValidUntilExpires(true);
            }

            #endregion

            #region 重新设置本页的 action

            string DomainName = PublicFunction.GetSiteUrl();
            Regex regex = new Regex(DomainName + "/Private/[\\d]+?/PageCaches/[\\S\\s]+?.aspx", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string Url = this.Request.Url.AbsoluteUri;

            if (regex.IsMatch(Url))
            {
                Url = ResolveUrl(regex.Replace(Url, "~/Default.aspx"));
            }

            this.Form.Action = Url;

            #endregion

            base.OnLoad(e);
        }
    }
}
