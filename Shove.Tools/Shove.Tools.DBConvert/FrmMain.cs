using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Shove.Tools.DBConvert
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            tbSQLiteFileName.Text = this.openFileDialog1.FileName;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (tbSQLiteFileName.Text.Trim() == "")
            {
                MessageBox.Show("Please select a SQLite file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!System.IO.File.Exists(tbSQLiteFileName.Text.Trim()))
            {
                MessageBox.Show("File “" + tbSQLiteFileName.Text.Trim() + "” does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string msg = "";
            Shove.DatabaseFactory.Convert.Converter converter = new Shove.DatabaseFactory.Convert.Converter();
            string ConnectionString = "";
            IDbConnection conn;

            if (this.tabControl1.SelectedIndex == 0)
            {
                ConnectionString = String.Format("server={0}; uid={1}; pwd={2}; database={3}; ", tbServer1.Text, tbUserName1.Text, tbPassword1.Text, tbDatabase1.Text);
                conn = new SqlConnection(ConnectionString);
                try
                {
                    conn.Open();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                conn.Close();

                if (!converter.SQLiteToMSSQL(tbSQLiteFileName.Text.Trim(), ConnectionString, true, ref msg))
                {
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("OK, Has been successfully converted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return;
            }

            ConnectionString = String.Format("server={0}; user id={1}; password={2}; database={3}; port=3306; charset=utf8", tbServer2.Text, tbUserName2.Text, tbPassword2.Text, tbDatabase2.Text);
            conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            conn.Close();

            if (!converter.SQLiteToMySQL(tbSQLiteFileName.Text.Trim(), ConnectionString, true, ref msg))
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("OK, Has been successfully converted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
