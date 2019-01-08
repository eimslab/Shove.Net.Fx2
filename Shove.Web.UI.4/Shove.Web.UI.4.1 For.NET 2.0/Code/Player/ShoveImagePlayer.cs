using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
//[assembly: WebResource("Shove.Web.UI.Script.ShoveImagePlayer.js", "application/javascript")]
namespace Shove.Web.UI
{
    [ToolboxData("<{0}:ShoveImagePlayer runat=server></{0}:ShoveImagePlayer>")]
    public class ShoveImagePlayer : WebControl
    {
        public enum RunMode
        {
            JavaScriptPlay = 0,
            FlashPlay = 1
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveImagePlayer", "ShoveWebUI_client/Script/ShoveImagePlayer.js");

            base.OnLoad(e);
        }

        [Bindable(true), Category("����"), DefaultValue("400"), Localizable(true), Description("ͼƬ���")]
        public int imgWidth
        {
            get
            {
                try
                {
                    int Width = (int)ViewState["imgWidth"];

                    return ((Width == 0) ? 400 : (int) Width);
                }
                catch
                {
                    return 400;
                }
            }

            set
            {
                ViewState["imgWidth"] = value;
            }
        }

        [Bindable(true), Category("����"), DefaultValue("300"), Localizable(true), Description("ͼƬ�߶�")]
        public int imgHeight
        {
            get
            {
                try
                {
                    int Height = (int)ViewState["imgHeight"];

                    return ((Height == 0) ? 300 : (int) Height);
                }
                catch
                {
                    return 300;
                }               
            }

            set
            {
                ViewState["imgHeight"] = value;
            }
        }

        [Bindable(true), Category("����"), DefaultValue("0"), Localizable(true), Description("ͼƬ˵�����ָ߶�")]
        public int textFromHeight
        {
            get
            {
                try
                {
                    int FromHeight = (int)ViewState["textFromHeight"];

                    return ((FromHeight == 0) ? 0 : (int) FromHeight);
                }
                catch
                {
                    return 0;
                }          
            }

            set
            {
                ViewState["textFromHeight"] = value;
            }
        }

        [Bindable(true), Category("����"), DefaultValue("f12"), Localizable(true), Description("ͼƬ˵��������ʽ��FlashPlayΪJavaScriptPlayʱ��Ч��")]
        public string textStyle
        {
            get
            {
                String s = (String)ViewState["textStyle"];

                return ((s == null) ? "f12" : s);
            }

            set
            {
                ViewState["textStyle"] = value;
            }
        }

        [Bindable(true), Category("����"), DefaultValue("p1"), Localizable(true), Description("ͼƬ˵������������ʽ��FlashPlayΪJavaScriptPlayʱ��Ч��")]
        public string textLinkStyle
        {
            get
            {
                String s = (String)ViewState["textLinkStyle"];

                return ((s == null) ? "p1" : s);
            }

            set
            {
                ViewState["textLinkStyle"] = value;
            }
        }

        [Bindable(false), Category("Appearance"), DefaultValue(typeof(Color), "ff6600"), Description("��ť��ʽ����ǰͼƬ��ť��ʽ��FlashPlay����ΪJavaScriptPlayʱ/���ⱳ����ɫFlashPlay����ΪFlashPlayʱ��")]
        public Color buttonLineOn
        {
            get
            {
                object obj = this.ViewState["buttonLineOn"];
                if (obj != null)
                {
                    return (Color)obj;
                }
                else
                {
                    if (RunMode.JavaScriptPlay == PlayType)
                    {
                        return Color.FromArgb(System.Convert.ToInt32("ff6600", 16));
                    }
                    else
                    {
                        return Color.FromArgb(System.Convert.ToInt32("DFEFF9", 16));
                    }
                }
            }

            set
            {
                this.ViewState["buttonLineOn"] = value;
            }
        }

        [Bindable(false), Category("Appearance"), DefaultValue(typeof(Color), "Black"), Description("��ť��ʽ������ͼƬ��ť��ʽ��FlashPlay����ΪJavaScriptPlayʱ��Ч��")]
        public Color buttonLineOff
        {
            get
            {
                object obj = this.ViewState["buttonLineOff"];
                if (obj != null)
                {
                    return (Color)obj;
                }
                else
                {
                    return Color.Black;
                }
            }

            set
            {
                this.ViewState["buttonLineOff"] = value;
            }
        }

        [Bindable(false), Category("��Ϊ"), DefaultValue("5"), Localizable(true), Description("ͼƬ�л�ʱ��������λ���룩")]
        public int TimeOut
        {
            get
            {
                try
                {
                    int Time = (int)ViewState["TimeOut"];

                    return ((Time == 0) ? 5 : (int) Time);
                }
                catch
                {
                    return 5;
                }
            }

            set
            {
                ViewState["TimeOut"] = value;
            }
        }

        [Bindable(false), Category("��Ϊ"), DefaultValue(""), Localizable(true), Description("ͼƬ������ʽ��1.jpg|2.jpg|3.jpg��")]
        public string Pics
        {
            get
            {
                String s = (String)ViewState["Pics"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Pics"] = value;
            }
        }

        [Bindable(false), Category("��Ϊ"), DefaultValue(""), Localizable(true), Description("���Ӽ�����ʽ��Page1.html|Page2.html|Page3.html��")]
        public string Links
        {
            get
            {
                String s = (String)ViewState["Links"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Links"] = value;
            }
        }

        [Bindable(false), Category("��Ϊ"), DefaultValue(""), Localizable(true), Description("ͼƬ���⼯����ʽ��PicTitle1|PicTitle2|PicTitle3��")]
        public string Titles
        {
            get
            {
                String s = (String)ViewState["Titles"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Titles"] = value;
            }
        }

        [Bindable(false), Category("��Ϊ"), DefaultValue("ShoveWebUI_client"), Localizable(true), Description("�ű��ļ���ַ/Flash�����ļ���")]
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

        [Bindable(false), Category("��Ϊ"), DefaultValue("ShoveWebUI_client/Images/pixviewer.swf"), Localizable(true), Editor(typeof(FlashUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("Ҫ֧�ֲ���ͼƬ�� Flash �ļ���FlashPlayΪFlashPlayʱ��Ч��")]
        public string FlashSrc
        {
            get
            {
                object obj = this.ViewState["FlashSrc"];

                if (RunMode.FlashPlay == PlayType)
                {
                    if (obj != null)
                    {
                        return (string)obj;
                    }
                    else
                    {
                        return SupportDir + "/Images/pixviewer.swf";
                    }
                }

                return "";
            }

            set
            {
                this.ViewState["FlashSrc"] = value;
            }
        }

        [Bindable(true), Category("��Ϊ"), DefaultValue(RunMode.JavaScriptPlay), Description("ʹ�õĲ���ģʽ)")]
        public RunMode PlayType
        {
            get
            {
                object s = this.ViewState["PlayType"];
                return ((s == null) ? RunMode.JavaScriptPlay : (RunMode)s);
            }

            set
            {
                ViewState["PlayType"] = value;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.WriteLine("\n<!-- Shove.Web.UI.ShoveFlashPlayer Start -->");

            if (Pics == "" || Titles == "" || Links == "")
            {
                base.RenderBeginTag(output);
                output.WriteLine();

                output.RenderBeginTag(HtmlTextWriterTag.Div);

                if ((BorderStyle == BorderStyle.None) || (BorderStyle == BorderStyle.NotSet))
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 400; height: 300; border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid");
                }
                else
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Style, "width: 400; height: 300;");
                }

                output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                output.RenderBeginTag(HtmlTextWriterTag.Table);
                output.RenderBeginTag(HtmlTextWriterTag.Tr);
                output.AddAttribute(HtmlTextWriterAttribute.Align, "left");
                output.RenderBeginTag(HtmlTextWriterTag.Td);

                output.WriteLine("Ҫ���ŵ�ͼƬ·����<br />��ʽ��<br />Pics = \"1.jpg|2.jpg|3.jpg\"<br />Titles = \"111|222|333\"<br />Links = \"111.html|222.html|333.html\"");

                output.RenderEndTag();
                output.RenderEndTag();
                output.RenderEndTag();

                output.RenderEndTag();  //End Div

                output.WriteLine();
                base.RenderEndTag(output);
            }
            else
            {
                if (RunMode.JavaScriptPlay == PlayType)
                {
                    base.RenderBeginTag(output);
                    output.WriteLine();

                    output.AddAttribute("type", "text/javascript");
                    output.RenderBeginTag(HtmlTextWriterTag.Script);

                    output.Write("ShoveWebUI_ShoveImagePlayer_Init(" + imgWidth + "," + imgHeight + "," + textFromHeight + ",'" + textStyle + "','" + textLinkStyle + "','" + buttonLineOn.Name + "','" + buttonLineOff.Name + "'," + TimeOut + ",'" + Pics + "','" + Links + "','" + Titles + "','" + Titles + "');");

                    output.RenderEndTag();  //End Script

                    output.WriteLine();
                    base.RenderEndTag(output);
                }
                else
                {
                    int FlashHeight = imgHeight + textFromHeight;
                    output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace(':', '_'));
                    output.AddAttribute(HtmlTextWriterAttribute.Style, "width: " + imgWidth + "px; height: " + FlashHeight.ToString() + "px;");
                    output.RenderBeginTag(HtmlTextWriterTag.Div);

                    output.AddAttribute("type", "text/javascript");
                    output.RenderBeginTag(HtmlTextWriterTag.Script);
                    output.WriteLine("ShoveWebUI_ShoveImagePlayerFlash_Onload('" + this.UniqueID.Replace(':', '_') + "_Content" + "'," + imgWidth + "," + imgHeight + "," + textFromHeight + ",'" + Pics + "','" + Links + "','" + Titles + "','" + FlashSrc + "','" + buttonLineOn.Name + "');");
                    output.RenderEndTag();

                    output.RenderEndTag();
                }
            }

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveFlashPlayer End -->");
        }
    }
}
