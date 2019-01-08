using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveTreeView
    /// </summary>
    [DefaultProperty("DataTable")]
    [ToolboxData("<{0}:ShoveTreeView runat=server></{0}:ShoveTreeView>")]
    public class ShoveTreeView : TreeView
    {
        /// <summary>
        /// ShowChildNodesNumberMode
        /// </summary>
        public enum ShowChildNodesNumberMode
        {
            /// <summary>
            /// 
            /// </summary>
            None = 0,
            /// <summary>
            /// 
            /// </summary>
            Childrens = 1
            //ChildrensAll = 2
        }

        /// <summary>
        /// ���ݱ�
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(null), Localizable(true)]
        public DataTable DataTable
        {
            get
            {
                object obj = ViewState["DataTable"];
                return ((obj == null) ? null : (DataTable)obj);
            }

            set
            {
                ViewState["DataTable"] = value;
            }
        }

        /// <summary>
        /// IDFieldName
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue("id"), Localizable(true)]
        public string IDFieldName
        {
            get
            {
                object obj = ViewState["IDFieldName"];
                return ((obj == null) ? "id" : (string)obj);
            }

            set
            {
                ViewState["IDFieldName"] = value;
            }
        }

        /// <summary>
        /// ParentIDFieldName
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue("Parent_id"), Localizable(true)]
        public string ParentIDFieldName
        {
            get
            {
                object obj = ViewState["ParentIDFieldName"];
                return ((obj == null) ? "Parent_id" : (string)obj);
            }

            set
            {
                ViewState["ParentIDFieldName"] = value;
            }
        }

        /// <summary>
        /// TextFieldName
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue("Text"), Localizable(true)]
        public string TextFieldName
        {
            get
            {
                object obj = ViewState["TextFieldName"];
                return ((obj == null) ? "Text" : (string)obj);
            }

            set
            {
                ViewState["TextFieldName"] = value;
            }
        }

        /// <summary>
        /// ValueFieldName
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue("Value"), Localizable(true)]
        public string ValueFieldName
        {
            get
            {
                object obj = ViewState["ValueFieldName"];
                return ((obj == null) ? "Value" : (string)obj);
            }

            set
            {
                ViewState["ValueFieldName"] = value;
            }
        }

        /// <summary>
        /// ImageUrlFieldName
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(""), Localizable(true)]
        public string ImageUrlFieldName
        {
            get
            {
                object obj = ViewState["ImageUrlFieldName"];
                return ((obj == null) ? "" : (string)obj);
            }

            set
            {
                ViewState["ImageUrlFieldName"] = value;
            }
        }

        /// <summary>
        /// NavigateUrlFieldName
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(""), Localizable(true)]
        public string NavigateUrlFieldName
        {
            get
            {
                object obj = ViewState["NavigateUrlFieldName"];
                return ((obj == null) ? "" : (string)obj);
            }

            set
            {
                ViewState["NavigateUrlFieldName"] = value;
            }
        }

        /// <summary>
        /// TargetFrameFieldName
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(""), Localizable(true)]
        public string TargetFrameFieldName
        {
            get
            {
                object obj = ViewState["TargetFrameFieldName"];
                return ((obj == null) ? "" : (string)obj);
            }

            set
            {
                ViewState["TargetFrameFieldName"] = value;
            }
        }

        /// <summary>
        /// Node �ı�������ʾ�ӽڵ��������ʾ��ʽ
        /// </summary>
        [Bindable(true), Category("��Ϊ"), DefaultValue(ShowChildNodesNumberMode.None), Description("Node �ı�������ʾ�ӽڵ��������ʾ��ʽ")]
        public ShowChildNodesNumberMode ShowChildNodesNumber
        {
            get
            {
                object s = this.ViewState["ShowChildNodesNumberMode"];
                return ((s == null) ? ShowChildNodesNumberMode.None : (ShowChildNodesNumberMode)s);
            }

            set
            {
                ViewState["ShowChildNodesNumberMode"] = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public new void DataBind()
        {
            #region ����У��

            if (DataTable == null)
            {
                throw new Exception("û�и� ShoveTreeView ���͵Ķ���" + ID + "������ DataTable ���ԡ�");
            }

            if (IDFieldName == "")
            {
                throw new Exception("û��ָ����ʾ IDFieldName �е��ֶ����ơ�");
            }

            if (ParentIDFieldName == "")
            {
                throw new Exception("û��ָ����ʾ ParentIDFieldName �е��ֶ����ơ�");
            }

            if (!PublicFunction.IsExistColumn(DataTable, IDFieldName))
            {
                throw new Exception("���������ݱ��в����ڡ�" + IDFieldName + "�С���");
            }

            if (!PublicFunction.IsExistColumn(DataTable, ParentIDFieldName))
            {
                throw new Exception("���������ݱ��в����ڡ�" + ParentIDFieldName + "�С���");
            }

            if ((TextFieldName != "") && (!PublicFunction.IsExistColumn(DataTable, TextFieldName)))
            {
                throw new Exception("���������ݱ��в����ڡ�" + TextFieldName + "�С���");
            }

            if ((ValueFieldName != "") && (!PublicFunction.IsExistColumn(DataTable, ValueFieldName)))
            {
                throw new Exception("���������ݱ��в����ڡ�" + ValueFieldName + "�С���");
            }

            if ((ImageUrlFieldName != "") && (!PublicFunction.IsExistColumn(DataTable, ImageUrlFieldName)))
            {
                throw new Exception("���������ݱ��в����ڡ�" + ImageUrlFieldName + "�С���");
            }

            if ((NavigateUrlFieldName != "") && (!PublicFunction.IsExistColumn(DataTable, NavigateUrlFieldName)))
            {
                throw new Exception("���������ݱ��в����ڡ�" + NavigateUrlFieldName + "�С���");
            }

            if ((TargetFrameFieldName != "") && (!PublicFunction.IsExistColumn(DataTable, TargetFrameFieldName)))
            {
                throw new Exception("���������ݱ��в����ڡ�" + TargetFrameFieldName + "�С���");
            }
            
            #endregion

            this.Nodes.Clear();

            DataRow[] drs = DataTable.Select(ParentIDFieldName + "=-1");

            foreach (DataRow dr in drs)
            {
                TreeNode tn = BuildNewTreeNode(dr);
                this.Nodes.Add(tn);

                AddChildNodes(tn, dr[IDFieldName].ToString());
            }

            if ((this.Nodes.Count == 0) || (ShowChildNodesNumber == ShowChildNodesNumberMode.None))
            {
                return;
            }

            GetNodesNumber();
        }

        private void AddChildNodes(TreeNode tn, string ParentID)
        {
            DataRow[] drs = DataTable.Select(ParentIDFieldName + "=" + ParentID);

            foreach (DataRow dr in drs)
            {
                TreeNode child_tn = BuildNewTreeNode(dr);
                tn.ChildNodes.Add(child_tn);

                AddChildNodes(child_tn, dr[IDFieldName].ToString());
            }
        }

        private TreeNode BuildNewTreeNode(DataRow dr)
        {
            TreeNode tn = new TreeNode(
                (TextFieldName != "") ? dr[TextFieldName].ToString() : "",
                (ValueFieldName != "") ? dr[ValueFieldName].ToString() : "",
                (ImageUrlFieldName != "") ? dr[ImageUrlFieldName].ToString() : "",
                (NavigateUrlFieldName != "") ? dr[NavigateUrlFieldName].ToString() : "#",
                (ImageUrlFieldName != "") ? dr[ImageUrlFieldName].ToString() : "");

            return tn;
        }

        private void GetNodesNumber()
        {
            foreach (TreeNode tn in this.Nodes)
            {
                GetChildNodesNumber(tn);
            }
        }

        private void GetChildNodesNumber(TreeNode tn)
        {
            tn.Text += " <font color='red'>(" + tn.ChildNodes.Count.ToString() + ")</font>";

            foreach (TreeNode ctn in tn.ChildNodes)
            {
                GetChildNodesNumber(ctn);
            }
        }

        /// <summary>
        /// ���ֿؼ�
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            output.WriteLine("\n<!-- Shove.Web.UI.ShoveTreeView Start -->");

            base.Render(output);

            output.WriteLine();
            output.WriteLine("<!-- Shove.Web.UI.ShoveTreeView End -->");
        }
    }
}
