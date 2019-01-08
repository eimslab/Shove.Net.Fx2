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

        [Bindable(true), Category("布局"), DefaultValue("400"), Localizable(true), Description("图片宽度")]
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

        [Bindable(true), Category("布局"), DefaultValue("300"), Localizable(true), Description("图片高度")]
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

        [Bindable(true), Category("布局"), DefaultValue("0"), Localizable(true), Description("图片说明文字高度")]
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

        [Bindable(true), Category("布局"), DefaultValue("f12"), Localizable(true), Description("图片说明文字样式（FlashPlay为JavaScriptPlay时有效）")]
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

        [Bindable(true), Category("布局"), DefaultValue("p1"), Localizable(true), Description("图片说明文字链接样式（FlashPlay为JavaScriptPlay时有效）")]
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

        [Bindable(false), Category("Appearance"), DefaultValue(typeof(Color), "ff6600"), Description("按钮样式（当前图片按钮样式，FlashPlay属性为JavaScriptPlay时/标题背景颜色FlashPlay属性为FlashPlay时）")]
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

        [Bindable(false), Category("Appearance"), DefaultValue(typeof(Color), "Black"), Description("按钮样式（其它图片按钮样式，FlashPlay属性为JavaScriptPlay时有效）")]
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

        [Bindable(false), Category("行为"), DefaultValue("5"), Localizable(true), Description("图片切换时间间隔（单位：秒）")]
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

        [Bindable(false), Category("行为"), DefaultValue(""), Localizable(true), Description("图片集（格式：1.jpg|2.jpg|3.jpg）")]
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

        [Bindable(false), Category("行为"), DefaultValue(""), Localizable(true), Description("链接集（格式：Page1.html|Page2.html|Page3.html）")]
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

        [Bindable(false), Category("行为"), DefaultValue(""), Localizable(true), Description("图片标题集（格式：PicTitle1|PicTitle2|PicTitle3）")]
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

        [Bindable(false), Category("行为"), DefaultValue("ShoveWebUI_client"), Localizable(true), Description("脚本文件地址/Flash所在文件夹")]
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

        [Bindable(false), Category("行为"), DefaultValue("ShoveWebUI_client/Images/pixviewer.swf"), Localizable(true), Editor(typeof(FlashUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Description("要支持播放图片的 Flash 文件（FlashPlay为FlashPlay时有效）")]
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

        [Bindable(true), Category("行为"), DefaultValue(RunMode.JavaScriptPlay), Description("使用的播放模式)")]
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

                output.WriteLine("要播放的图片路径。<br />格式：<br />Pics = \"1.jpg|2.jpg|3.jpg\"<br />Titles = \"111|222|333\"<br />Links = \"111.html|222.html|333.html\"");

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
