using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Shove
{
    /// <summary>
    /// ControlExt ��ժҪ˵����
    /// </summary>
    public class ControlExt
    {
        /// <summary>
        /// ��� DropDownList �ؼ�
        /// </summary>
        public static int FillDropDownList(DropDownList ddl, DataTable dt, string TextFieldName, string ValueFieldName)
        {
            ddl.Items.Clear();

            if ((dt == null) || (dt.Rows.Count < 1))
            {
                return -1;
            }

            foreach (DataRow dr in dt.Rows)
            {
                ddl.Items.Add(new ListItem(dr[TextFieldName].ToString(), dr[ValueFieldName].ToString()));
            }

            if (ddl.Items.Count > 0)
            {
                ddl.SelectedIndex = 0;
            }

            return ddl.Items.Count;
        }

        /// <summary>
        /// �ݹ���� DropDownList �ؼ������ھ��� ParentID ָ�� ID �ı�
        /// </summary>
        public static int FillDropDownList(DropDownList ddl, DataTable dt, string TextFieldName, string IDFieldName, string ParentIDFieldName, long FirstLevelParentIDValue)
        {
            ddl.Items.Clear();

            if ((dt == null) || (dt.Rows.Count < 1))
            {
                return -1;
            }

            DataRow[] drs = dt.Select(ParentIDFieldName + "=" + FirstLevelParentIDValue);

            foreach (DataRow dr in drs)
            {
                ddl.Items.Add(new ListItem(dr[TextFieldName].ToString(), dr[IDFieldName].ToString()));
                FillDropDownList_Sub(ddl, dt, TextFieldName, IDFieldName, ParentIDFieldName, dr[IDFieldName].ToString(), 1);
            }

            if (ddl.Items.Count > 0)
            {
                ddl.SelectedIndex = 0;
            }

            return ddl.Items.Count;
        }

        /// <summary>
        /// ����һ�������ĵݹ��ӷ���
        /// </summary>
        private static void FillDropDownList_Sub(DropDownList ddl, DataTable dt, string TextFieldName, string IDFieldName, string ParentIDFieldName, string ParentIDFieldValue, int Level)
        {
            string PreFix = "".PadLeft(Level, (char)12288); // &#160; �ǰ��

            Level++;

            DataRow[] drs = dt.Select(ParentIDFieldName + "=" + ParentIDFieldValue);

            foreach (DataRow dr in drs)
            {
                ddl.Items.Add(new ListItem(PreFix + dr[TextFieldName].ToString(), dr[IDFieldName].ToString()));
                FillDropDownList_Sub(ddl, dt, TextFieldName, IDFieldName, ParentIDFieldName, dr[IDFieldName].ToString(), Level);
            }
        }

        /// <summary>
        /// ���������ı���return -1����������Ŀ��-2�������޴���Ŀ
        /// </summary>
        public static int SetDownListBoxText(DropDownList ddl, string Text)
        {
            if (ddl.Items.Count == 0)
                return -1;
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Text == Text)
                {
                    ddl.SelectedIndex = i;
                    return i;
                }
            }
            return -2;
        }

        /// <summary>
        /// ����Item��Value���������ı���return -1����������Ŀ��-2�������޴���Ŀ
        /// </summary>
        public static int SetDownListBoxTextFromValue(DropDownList ddl, string Value)
        {
            if (ddl.Items.Count == 0)
                return -1;
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Value == Value)
                {
                    ddl.SelectedIndex = i;
                    return i;
                }
            }
            return -2;
        }

        /// <summary>
        /// ��� ListBox �ؼ�
        /// </summary>
        public static int FillListBox(ListBox lb, DataTable dt, string TextField, string ValueField)
        {
            lb.Items.Clear();

            if ((dt == null) || (dt.Rows.Count < 1))
            {
                return -1;
            }

            foreach (DataRow dr in dt.Rows)
            {
                lb.Items.Add(new ListItem(dr[TextField].ToString(), dr[ValueField].ToString()));
            }

            if (lb.Items.Count > 0)
            {
                lb.SelectedIndex = 0;
            }

            return lb.Items.Count;
        }

        /// <summary>
        /// ����ListBox��return -1������Ŀ��-2���޴���Ŀ
        /// </summary>
        public static int SetListBoxText(ListBox lb, string Text)
        {
            if (lb.Items.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < lb.Items.Count; i++)
            {
                if (lb.Items[i].Text == Text)
                {
                    lb.SelectedIndex = i;

                    return i;
                }
            }
            return -2;
        }

        /// <summary>
        /// ����Item��Value���� ListBox �ı���return -1����������Ŀ��-2�������޴���Ŀ
        /// </summary>
        public static int SetListBoxTextFromValue(ListBox lb, string Value)
        {
            if (lb.Items.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < lb.Items.Count; i++)
            {
                if (lb.Items[i].Value == Value)
                {
                    lb.SelectedIndex = i;

                    return i;
                }
            }

            return -2;
        }

        /// <summary>
        /// ����ֵ���� CheryTreeView ��ѡ�нڵ�
        /// </summary>
        public static TreeNode SetTreeViewSelected(TreeView tv, string Text)
        {
            if ((tv == null) || (tv.Nodes.Count < 1))
            {
                return null;
            }

            foreach (TreeNode tn in tv.Nodes)
            {
                if (tn.Text == Text)
                {
                    tn.Selected = true;

                    return tn;
                }
            }

            return null;
        }

        /// <summary>
        /// ����ֵ���� CheryTreeView ��ѡ�нڵ�
        /// </summary>
        public static TreeNode SetTreeViewSelectedFromValue(TreeView tv, string Value)
        {
            if ((tv == null) || (tv.Nodes.Count < 1))
            {
                return null;
            }

            foreach (TreeNode tn in tv.Nodes)
            {
                if (tn.Value == Value)
                {
                    tn.Selected = true;

                    return tn;
                }
            }

            return null;
        }

        /// <summary>
        /// WinForm �� ListItem, DropdownList �� Item�����Լ�¼ value
        /// </summary>
        public class Item
        {
            /// <summary>
            /// 
            /// </summary>
            public string Text;
            /// <summary>
            /// 
            /// </summary>
            public object Value;

            /// <summary>
            /// 
            /// </summary>
            public Item()
            {
                Text = "";
                Value = null;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            public Item(string text)
            {
                Text = text;
                Value = null;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            /// <param name="value"></param>
            public Item(string text, object value)
            {
                Text = text;
                Value = value;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Text;
            }
        }
    }
}
