using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

using Shove.Database.Persistences;

namespace ShoveDAL
{
    public partial class FrmOracle_Net : Form
    {
        public FrmOracle_Net()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            Oracle Oracle = new Oracle(tbServer.Text, tbUserID.Text, tbPassword.Text, tbNamespace.Text, cbConnStr.Checked,
                rbConnectionString.Checked, cbTables.Checked, cbViews.Checked, cbProcedures.Checked, cbFunctions.Checked);

            string Result = Oracle.Generation();

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
