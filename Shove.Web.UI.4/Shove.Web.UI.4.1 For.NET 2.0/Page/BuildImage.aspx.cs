using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Shove.Web.UI
{
    /// <summary>
    /// BuildImg 的摘要说明。
    /// </summary>
    public class BuildImage : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            Encrypt encrypt = new Encrypt();

            string Key = "";
            try
            {
                Key = encrypt.UnEncryptString(Request["Key"]);
            }
            catch
            {
            }

            if (Key == "")
            {
                Key = "ShoveWebUI_CheckCode_CheckCode";
            }

            string CheckCode = "";
            try
            {
                CheckCode = encrypt.UnEncryptString(Request["CheckCode"]);
            }
            catch
            {
            }

            if (CheckCode == "")
            {
                Session[Key] = "ABCD";
                CheckCode = "ABCD";
            }
            else
            {
                Session[Key] = CheckCode;
            }

            Color forecolor = Color.Blue;
            Color backcolor = Color.MistyRose;
            try
            {
                forecolor = System.Drawing.Color.FromName(encrypt.UnEncryptString(Request["ForeColor"]));
            }
            catch
            {
            }

            try
            {
                backcolor = System.Drawing.Color.FromName(encrypt.UnEncryptString(Request["BackColor"]));
            }
            catch
            {
            }

            int width = 50;
            int height = 20;
            try
            {
                width = int.Parse(encrypt.UnEncryptString(Request["Width"]));
            }
            catch
            {
            }

            try
            {
                height = int.Parse(encrypt.UnEncryptString(Request["Height"]));
            }
            catch
            {
            }

            string fontname = "";
            try
            {
                fontname = encrypt.UnEncryptString(Request["FontName"]);
            }
            catch
            {
            }

            int fontsize = 11;
            try
            {
                fontsize = int.Parse(encrypt.UnEncryptString(Request["FontSize"]));
            }
            catch
            {
            }

            Font font = null;

            if ((fontname != "") && (fontsize > 0))
            {
                font = new Font(fontname, fontsize);
            }
            else
            {
                font = new Font("Arial Black", 11);
            }

            bool isInterferenceBackgroup = true;
            try
            {
                isInterferenceBackgroup = bool.Parse(encrypt.UnEncryptString(Request["isInterferenceBackgroup"]));
            }
            catch { }

            Bitmap Img = null;
            Graphics Grap = null;
            Img = new Bitmap(width, height);
            Grap = Graphics.FromImage(Img);
            SolidBrush sbFore = new SolidBrush(forecolor);
            SolidBrush sbBack = new SolidBrush(backcolor);

            Grap.FillRectangle(sbBack, 0, 0, width, height);

            if (isInterferenceBackgroup)
            {
                // 画干扰线
                Pen pen = new Pen(sbFore);
                System.Random rd = new Random();
                Point pointStart = new Point();
                Point pointEnd = new Point();
                int LineNum = rd.Next(2, 4);
                for (int i = 0; i < LineNum; i++)
                {
                    pointStart.X = rd.Next(width);
                    pointStart.Y = rd.Next(height);
                    pointEnd.X = rd.Next(width);
                    pointEnd.Y = rd.Next(height);
                    Grap.DrawLine(pen, pointStart, pointEnd);
                }
                // 画干扰点
                LineNum = rd.Next(10, 20);
                for (int i = 0; i < LineNum; i++)
                {
                    pointStart.X = rd.Next(width - 1);
                    pointStart.Y = rd.Next(height - 1);
                    pointEnd.X = pointStart.X + 1;
                    pointEnd.Y = pointStart.Y + 1;
                    Grap.DrawLine(pen, pointStart, pointEnd);
                }
            }

            // 输出文字
            Grap.DrawString(CheckCode, font, sbFore, 0, 0);

            Img.Save(Response.OutputStream, ImageFormat.Jpeg);

            Grap.Dispose();
            Img.Dispose();
            font.Dispose();
            sbFore.Dispose();
            sbBack.Dispose();
        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}
