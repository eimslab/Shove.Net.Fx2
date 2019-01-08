using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;

namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPaet用户控件基类
    /// </summary>
    public class ShoveWebPartUserControl : UserControl
    {
        /// <summary>
        /// 控件名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 控件内容类型，根据控件的内容类别，决定在网站的后台，用那些表编译内容。主要是为了建立控件和后台编辑之间的对应关系。
        /// </summary>
        public string ContentType = ShoveWebPartUserControlContentType.BuildTypeString(ShoveWebPartUserControlContentType.Unknow);

        /// <summary>
        /// 属性控制类
        /// </summary>
        public ShoveWebPartAttributeCollections swpAttributes = new ShoveWebPartAttributeCollections();

        /// <summary>
        /// 运行时，页面上控件附带了 SiteID 属性，不需要从父页获取。否则在 OnLoad 方法中从父页获取。
        /// </summary>
        public long SiteID
        {
            get
            {
                object obj = this.ViewState["SiteID"];

                try
                {
                    return Convert.ToInt64(obj);
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                this.ViewState["SiteID"] = value;
            }
        }

        /// <summary>
        /// 是否设计模式(浏览模式)
        /// </summary>
        public bool isDesigning = false;

        /// <summary>
        /// 用户控件属性集合
        /// </summary>
        public ShoveWebPartAttribute[] swpas;

        /// <summary>
        /// 页面上给本控件设置的属性值列表
        /// </summary>
        public string ControlAttributes
        {
            get
            {
                object obj = this.ViewState["ControlAttributes"];

                try
                {
                    return Convert.ToString(obj);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                this.ViewState["ControlAttributes"] = value;

                if (!String.IsNullOrEmpty(value))
                {
                    swpAttributes.FromString(value, this);
                }
                else
                {
                    if (swpas != null)
                    {
                        for (int i = 0; i < swpas.Length; i++)
                        {
                            ShoveWebPartAttribute att = (ShoveWebPartAttribute)swpas[i];

                            swpAttributes[i] = att.Value.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ShoveWebPartUserControl()
        {
            // _ControlName = "";
            // ContentType = ShoveWebPartUserControlContentType.BuildTypeString(ShoveWebPartUserControlContentType.Unknow);

            // swpAttributes = new ShoveWebPartAttributeCollections();

            // 以上 3 个属性不要再次初始化，因为其派生类的构造函数有2中情况，1、控件分为 .ascx, .ascx.cs 时，构造函数用本身的构造函数，没有问题。2、控件为 .ascx 一个文件时，构造函数只能用 protected override void Construct()，在本基类构造函数前执行，所以，如果此处初始化，会冲掉派生类的构造。
        }

        /// <summary>
        /// 重写父类OnLoad()方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (!(this.Page is ShoveWebPartBasePageRun))
            {
                try
                {
                    ShoveWebPartBasePage page = (ShoveWebPartBasePage)this.Page;

                    SiteID = page.SiteID;
                    isDesigning = page.IsDesigning;
                }
                catch
                {
                    throw new Exception("ShoveWebPart 的用户控件必须放在基于 ShoveWebPartBasePage 或者 ShoveWebPartBasePageRun 的页面上！");
                }
            }

            base.OnLoad(e);
        }
    }
}