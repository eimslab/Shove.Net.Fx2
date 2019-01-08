using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

using Shove.Database.Persistences;

namespace ShoveDAL
{
    public partial class FrmSQLite_Net : Form
    {
        public FrmSQLite_Net()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            SQLite sqlite = new SQLite(tbDatabase.Text, tbPassword.Text, tbNamespace.Text, cbConnStr.Checked,
                rbConnectionString.Checked, cbTables.Checked, cbViews.Checked);

            string Result = sqlite.Generation();

            Clipboard.SetDataObject(Result, true);

            FrmResult fr = new FrmResult();
            fr.tbResult.Text = Result;
            fr.ShowDialog();
            fr.Dispose();
        }

        private void cbConnStr_CheckedChanged(object sender, EventArgs e)
        {
            rbConnectionString.Enabled = !cbConnStr.Checked;
            rbConnection.Enabled = rbConnectionString.Enabled;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbDatabase.Text = openFileDialog1.FileName;
        }
    }
}
