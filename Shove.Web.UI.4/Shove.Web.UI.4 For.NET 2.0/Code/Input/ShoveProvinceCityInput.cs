using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
[assembly: WebResource("Shove.Web.UI.Script.ShoveProvinceCityInput.js", "application/javascript")]
namespace Shove.Web.UI
{
    [DefaultProperty("City")]
    [ToolboxData("<{0}:ShoveProvinceCityInput runat=server></{0}:ShoveProvinceCityInput>")]
    public class ShoveProvinceCityInput : WebControl, INamingContainer
    {
        private DropDownList ddlProvince;
        private DropDownList ddlCity;

        private string SessionKey_ProvinceID = "ShoveProvinceCityInput_SessionKey_ProvinceID";
        private string SessionKey_CityID = "ShoveProvinceCityInput_SessionKey_CityID";

        public ShoveProvinceCityInput()
        {
            base.Width = Width;
            base.Height = Height;

            ddlProvince = new DropDownList();
            ddlProvince.ID = "ddlProvince";
            ddlProvince.Width = new Unit("50%");
            ddlProvince.BackColor = BackColor;
            ddlProvince.ForeColor = ForeColor;
            ddlProvince.Height = Height;
            ddlProvince.Items.Clear();
            ddlProvince.Attributes.Add("onchange", "ShoveWebUI_ShoveProvinceCityInput_ProvinceOnChange(this);");

            ddlCity = new DropDownList();
            ddlCity.ID = "ddlCity";
            ddlCity.Width = new Unit("50%");
            ddlCity.BackColor = BackColor;
            ddlCity.ForeColor = ForeColor;
            ddlCity.Height = Height;
            ddlCity.Items.Clear();
            ddlCity.Attributes.Add("onchange", "ShoveWebUI_ShoveProvinceCityInput_CityOnChange(this);");

            this.Controls.Add(ddlProvince);
            this.Controls.Add(ddlCity);
        }

        protected override void OnLoad(EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ShoveProvinceCityInput));

            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveProvinceCityInput", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Shove.Web.UI.Script.ShoveProvinceCityInput.js"));

            ddlProvince.Items.Clear();
            ddlCity.Items.Clear();

            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(System.Web.HttpContext.Current.Server.MapPath(SupportDir + "/Data/ProvinceCity.xml"));
            }
            catch { }

            if (ds.Tables.Count == 2)
            {
                DataTable dtProvince = ds.Tables["Province"];
                DataTable dtCity = ds.Tables["City"];

                int RowIndex = PublicFunction.FindRow(dtCity, "id", City_id);
                if (RowIndex >= 0)
                {
                    Province_id = int.Parse(dtCity.Rows[RowIndex]["Province_id"].ToString());
                }
                else
                {
                    Province_id = 1;
                    City_id = 1;
                }
            }

            ds.Dispose();

            base.OnLoad(e);
        }

        #region Ajax 方法

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string GetProvinceList()
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(System.Web.HttpContext.Current.Server.MapPath("~/" + SupportDir + "/Data/ProvinceCity.xml"));
            }
            catch
            {
                return null;
            }

            if (ds.Tables.Count < 2)
            {
                return null;
            }

            DataTable dtProvince = ds.Tables["Province"];
            DataTable dtCity = ds.Tables["City"];

            string Result = "";

            foreach (DataRow dr in dtProvince.Rows)
            {
                Result += dr["id"].ToString() + "," + dr["Name"].ToString() + ";";
            }

            ds.Dispose();

            if (Result.Length > 0)
            {
                Result = Result.Substring(0, Result.Length - 1);
            }

            return Result;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string GetCityList(int province_id)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(System.Web.HttpContext.Current.Server.MapPath("~/" + SupportDir + "/Data/ProvinceCity.xml"));
            }
            catch
            {
                return null;
            }

            if (ds.Tables.Count < 2)
            {
                return null;
            }

            DataTable dtProvince = ds.Tables["Province"];
            DataTable dtCity = ds.Tables["City"];

            string Result = "";

            foreach (DataRow dr in dtCity.Select("Province_id=" + province_id.ToString()))
            {
                Result += dr["id"].ToString() + "," + dr["Name"].ToString() + ";";
            }

            ds.Dispose();

            if (Result.Length > 0)
            {
                Result = Result.Substring(0, Result.Length - 1);
            }

            return Result;
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public void SetProvinceID(int province_id)
        {
            try
            {
                System.Web.HttpContext.Current.Session[SessionKey_ProvinceID] = province_id;
            }
            catch { }
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public int GetProvinceID()
        {
            try
            {
                return int.Parse(System.Web.HttpContext.Current.Session[SessionKey_ProvinceID].ToString());
            }
            catch
            {
                return 1;
            }
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public void SetCityID(int city_id)
        {
            try
            {
                System.Web.HttpContext.Current.Session[SessionKey_CityID] = city_id;
            }
            catch { }
        }

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public int GetCityID()
        {
            try
            {
                return int.Parse(System.Web.HttpContext.Current.Session[SessionKey_CityID].ToString());
            }
            catch
            {
                return 1;
            }
        }

        #endregion

        #region 布局

        [Bindable(false), Category("布局"), DefaultValue(typeof(Unit), "220px"), Description("控件宽度")]
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
                    return new Unit("220px");
                }
            }

            set
            {
                this.ViewState["Width"] = value;
                base.Width = value;
            }
        }

        [Bindable(false), Category("布局"), DefaultValue(typeof(Unit), "22px"), Description("控件高度")]
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
                    return new Unit("22px");
                }
            }

            set
            {
                this.ViewState["Height"] = value;
                base.Height = value;
            }
        }

        #endregion

        [Bindable(true), Category("行为"), DefaultValue("ShoveWebUI_client"), Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("本系列控件的支持目录，以连接到相关的图片、脚本文件")]
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

        [Bindable(true), Category("行为"), DefaultValue(1), Localizable(true)]
        public int Province_id
        {
            get
            {
                return GetProvinceID();
            }
            set
            {
                SetProvinceID(value);
            }
        }

        [Bindable(true), Category("行为"), DefaultValue(""), Localizable(true)]
        public string Province
        {
            get
            {
                DataSet ds = new DataSet();
                try
                {
                    ds.ReadXml(System.Web.HttpContext.Current.Server.MapPath(SupportDir + "/Data/ProvinceCity.xml"));
                }
                catch
                {
                    return "";
                }

                if (ds.Tables.Count < 2)
                {
                    return "";
                }

                string Result = "";

                DataTable dtProvince = ds.Tables["Province"];
                DataTable dtCity = ds.Tables["City"];

                int RowIndex = PublicFunction.FindRow(dtProvince, "id", Province_id.ToString());
                if (RowIndex >= 0)
                {
                    Result = dtProvince.Rows[RowIndex]["Name"].ToString();
                }

                ds.Dispose();

                return Result;
            }
        }

        [Bindable(true), Category("行为"), DefaultValue(1), Localizable(true)]
        public int City_id
        {
            get
            {
                return GetCityID();
            }
            set
            {
                SetCityID(value);
            }
        }

        [Bindable(true), Category("行为"), DefaultValue(""), Localizable(true)]
        public string City
        {
            get
            {
                DataSet ds = new DataSet();
                try
                {
                    ds.ReadXml(System.Web.HttpContext.Current.Server.MapPath(SupportDir + "/Data/ProvinceCity.xml"));
                }
                catch
                {
                    return "";
                }

                if (ds.Tables.Count < 2)
                {
                    return "";
                }

                string Result = "";

                DataTable dtProvince = ds.Tables["Province"];
                DataTable dtCity = ds.Tables["City"];

                int RowIndex = PublicFunction.FindRow(dtCity, "id", City_id.ToString());
                if (RowIndex >= 0)
                {
                    Result = dtCity.Rows[RowIndex]["Name"].ToString();
                }

                ds.Dispose();

                return Result;
            }
        }

        [Bindable(true), Category("行为"), DefaultValue(""), Localizable(true)]
        public string Text
        {
            get
            {
                return Province + City;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.WriteLine("\n<!-- Shove.Web.UI.ShoveProvinceCityInput Start -->");

            base.RenderBeginTag(output);
            output.WriteLine();

            base.RenderChildren(output);
            output.WriteLine();

            output.AddAttribute("type", "text/javascript");
            output.AddAttribute("language", "javascript");
            output.RenderBeginTag(HtmlTextWriterTag.Script);
            output.Write("ShoveWebUI_ShoveProvinceCityInput_FillProvince('" + ddlProvince.UniqueID.Replace(':', '_').Replace('$', '_') + "');");
            output.Write("ShoveWebUI_ShoveProvinceCityInput_FillCity('" + ddlCity.UniqueID.Replace(':', '_').Replace('$', '_') + "');");
            output.RenderEndTag();

            output.WriteLine();
            base.RenderEndTag(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveProvinceCityInput End -->");
        }
    }
}
