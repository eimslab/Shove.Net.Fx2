using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using Shove.Database.Persistences.Java;

namespace ShoveDAL
{
    public partial class FrmMySQL_Java : Form
    {
        public FrmMySQL_Java()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            MySQL mysql = new MySQL(tbServer.Text, tbDatabase.Text, tbUserID.Text, tbPassword.Text, tbPort.Text, tbNamespace.Text, cbConnStr.Checked,
                rbConnectionString.Checked, cbTables.Checked, cbViews.Checked, cbProcedures.Checked, cbFunctions.Checked);

            string Result = mysql.Generation();

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
    }
}
