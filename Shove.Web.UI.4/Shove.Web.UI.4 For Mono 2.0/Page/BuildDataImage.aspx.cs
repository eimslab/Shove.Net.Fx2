using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

namespace Shove.Web.UI
{
    /// <summary>
    /// BuildDataImage ��ժҪ˵����
    /// </summary>
    public class BuildDataImage : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            string TableName = "";
            string FieldName = "";
            string Condition = "";

            try
            {
                TableName = PublicFunction.FilteSqlInfusion(System.Web.HttpUtility.UrlDecode(Request["TableName"]));
            }
            catch { }

            try
            {
                FieldName = PublicFunction.FilteSqlInfusion(System.Web.HttpUtility.UrlDecode(Request["FieldName"]));
            }
            catch { }

            try
            {
                Condition = PublicFunction.FilteSqlInfusion(System.Web.HttpUtility.UrlDecode(Request["Condition"]));
            }
            catch { }

            if ((TableName == "") || (FieldName == ""))
            {
                //throw new Exception("���� BuildDataImage.aspx ҳû��ָ�� TableName ���� FieldName �ֶ�������");
                return;
            }

            string ConnectionString = WebConfigurationManager.AppSettings.Get("ConnectionString").Trim();

            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
            }
            catch
            {
                throw new Exception("���� BuildDataImage.aspx ҳû����ȷ�ش����ݿ�");
            }

            SqlCommand Cmd = new SqlCommand("select " + FieldName + " from " + TableName + ((Condition == "") ? "" : (" where " + Condition)), conn);
            object obj;

            try
            {
                obj = Cmd.ExecuteScalar();
            }
            catch
            {
                conn.Close();

                throw new Exception("���� BuildDataImage.aspx ҳ��ȡ���ݴ���");
            }

            conn.Close();

            byte[] Image = null;

            try
            {
                Image = (byte[])obj;
            }
            catch
            { }

            if ((Image == null) || (Image.Length < 2))
            {
                //Response.BinaryWrite(new byte[] { 0 });

                //return;
                try
                {
                    string FileName = this.Server.MapPath("../Images/NoImage.jpg");


                    byte[] obj1 = System.IO.File.ReadAllBytes(FileName);

                    if (obj1 == null)
                    {
                        Response.BinaryWrite(new byte[] { 0 });

                        return;
                    }

                    Response.BinaryWrite((byte[])obj1);
                }
                catch
                {
                }

                return;
            }

            Response.BinaryWrite(Image);
        }

        #region Web ������������ɵĴ���

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion
    }
}
