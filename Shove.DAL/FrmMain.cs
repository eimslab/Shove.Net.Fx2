using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShoveDAL
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnNetSQLServer_Click(object sender, EventArgs e)
        {
            FrmSQLServer_Net f = new FrmSQLServer_Net();

            f.ShowDialog();
            f.Dispose();
        }

        private void btnNetMySQL_Click(object sender, EventArgs e)
        {
            FrmMySQL_Net f = new FrmMySQL_Net();

            f.ShowDialog();
            f.Dispose();
        }

        private void btnNetOracle_Click(object sender, EventArgs e)
        {
            FrmOracle_Net f = new FrmOracle_Net();

            f.ShowDialog();
            f.Dispose();
        }

        private void btnNetSQLite_Click(object sender, EventArgs e)
        {
            FrmSQLite_Net f = new FrmSQLite_Net();

            f.ShowDialog();
            f.Dispose();
        }

        private void btnJavaSQLServer_Click(object sender, EventArgs e)
        {
            FrmSQLServer_Java f = new FrmSQLServer_Java();

            f.ShowDialog();
            f.Dispose();
        }

        private void btnJavaMySQL_Click(object sender, EventArgs e)
        {
            FrmMySQL_Java f = new FrmMySQL_Java();

            f.ShowDialog();
            f.Dispose();
        }

        private void btnJavaOracle_Click(object sender, EventArgs e)
        {
            FrmOracle_Java f = new FrmOracle_Java();

            f.ShowDialog();
            f.Dispose();
        }

        private void btnJavaSQLite_Click(object sender, EventArgs e)
        {
            FrmSQLite_Java f = new FrmSQLite_Java();

            f.ShowDialog();
            f.Dispose();
        }

    }
}
