using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;

namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPaet�û��ؼ�����
    /// </summary>
    public class ShoveWebPartUserControl : UserControl
    {
        /// <summary>
        /// �ؼ�����
        /// </summary>
        public string Name = "";

        /// <summary>
        /// �ؼ��������ͣ����ݿؼ���������𣬾�������վ�ĺ�̨������Щ��������ݡ���Ҫ��Ϊ�˽����ؼ��ͺ�̨�༭֮��Ķ�Ӧ��ϵ��
        /// </summary>
        public string ContentType = ShoveWebPartUserControlContentType.BuildTypeString(ShoveWebPartUserControlContentType.Unknow);

        /// <summary>
        /// ���Կ�����
        /// </summary>
        public ShoveWebPartAttributeCollections swpAttributes = new ShoveWebPartAttributeCollections();

        /// <summary>
        /// ����ʱ��ҳ���Ͽؼ������� SiteID ���ԣ�����Ҫ�Ӹ�ҳ��ȡ�������� OnLoad �����дӸ�ҳ��ȡ��
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
        /// �Ƿ����ģʽ(���ģʽ)
        /// </summary>
        public bool isDesigning = false;

        /// <summary>
        /// �û��ؼ����Լ���
        /// </summary>
        public ShoveWebPartAttribute[] swpas;

        /// <summary>
        /// ҳ���ϸ����ؼ����õ�����ֵ�б�
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
        /// ���캯��
        /// </summary>
        public ShoveWebPartUserControl()
        {
            // _ControlName = "";
            // ContentType = ShoveWebPartUserControlContentType.BuildTypeString(ShoveWebPartUserControlContentType.Unknow);

            // swpAttributes = new ShoveWebPartAttributeCollections();

            // ���� 3 �����Բ�Ҫ�ٴγ�ʼ������Ϊ��������Ĺ��캯����2�������1���ؼ���Ϊ .ascx, .ascx.cs ʱ�����캯���ñ���Ĺ��캯����û�����⡣2���ؼ�Ϊ .ascx һ���ļ�ʱ�����캯��ֻ���� protected override void Construct()���ڱ����๹�캯��ǰִ�У����ԣ�����˴���ʼ��������������Ĺ��졣
        }

        /// <summary>
        /// ��д����OnLoad()����
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
                    throw new Exception("ShoveWebPart ���û��ؼ�������ڻ��� ShoveWebPartBasePage ���� ShoveWebPartBasePageRun ��ҳ���ϣ�");
                }
            }

            base.OnLoad(e);
        }
    }
}