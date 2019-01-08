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
    public class ShovePart2UserControl : UserControl
    {
        /// <summary>
        /// �ؼ�����
        /// </summary>
        public string Name = "";

        /// <summary>
        /// �ؼ��������ͣ����ݿؼ���������𣬾�������վ�ĺ�̨������Щ��������ݡ���Ҫ��Ϊ�˽����ؼ��ͺ�̨�༭֮��Ķ�Ӧ��ϵ��
        /// </summary>
        public string ContentType = ShovePart2UserControlContentType.BuildTypeString(ShovePart2UserControlContentType.Unknow);

        /// <summary>
        /// ���Կ�����
        /// </summary>
        public ShovePart2AttributeCollections swpAttributes = new ShovePart2AttributeCollections();

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
        public ShovePart2Attribute[] swpas;

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
                            ShovePart2Attribute att = (ShovePart2Attribute)swpas[i];

                            swpAttributes[i] = att.Value.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public ShovePart2UserControl()
        {
            // _ControlName = "";
            // ContentType = ShovePart2UserControlContentType.BuildTypeString(ShovePart2UserControlContentType.Unknow);

            // swpAttributes = new ShovePart2AttributeCollections();

            // ���� 3 �����Բ�Ҫ�ٴγ�ʼ������Ϊ��������Ĺ��캯����2�������1���ؼ���Ϊ .ascx, .ascx.cs ʱ�����캯���ñ���Ĺ��캯����û�����⡣2���ؼ�Ϊ .ascx һ���ļ�ʱ�����캯��ֻ���� protected override void Construct()���ڱ����๹�캯��ǰִ�У����ԣ�����˴���ʼ��������������Ĺ��졣
        }

        /// <summary>
        /// ��д����OnLoad()����
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (!(this.Page is ShovePart2BasePageRun))
            {
                try
                {
                    ShovePart2BasePage page = (ShovePart2BasePage)this.Page;

                    SiteID = page.SiteID;
                    isDesigning = page.IsDesigning;
                }
                catch
                {
                    throw new Exception("ShovePart2 ���û��ؼ�������ڻ��� ShovePart2BasePage ���� ShovePart2BasePageRun ��ҳ���ϣ�");
                }
            }

            base.OnLoad(e);
        }
    }
}