using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shove.Tools.Newton.Attack
{
    public partial class FrmMain : Form
    {
        string Key = "!@#$%^&*(3456SDFJUcvb#$%^56#$%^&dfghjk";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmLogin fl = new FrmLogin();
            fl.ShowDialog();

            if (!fl.isOK)
            {
                Application.Exit();
                return;
            }

            fl.Dispose();

            cbOpType.SelectedIndex = 0;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!Shove._String.Valid.isUrl(tbUrl.Text.Trim()))
            {
                MessageBox.Show("“" + tbUrl.Text + "” not is a Url.");

                return;
            }

            switch (cbOpType.SelectedIndex + 1)
            {
                case 1:
                    Go1();
                    break;
                case 2:
                    Go2();
                    break;
                case 3:
                    Go3();
                    break;
                case 4:
                    Go4();
                    break;
                case 5:
                    Go5();
                    break;
                default:
                    break;
            }
        }

        private void Go1()
        {
            string FileName = tbFileName.Text.Trim();

            if (String.IsNullOrEmpty(FileName))
            {
                MessageBox.Show("Please input a FileName.");

                return;
            }

            NewtonService.GetSIService service = CreateNewtonService();
            if (service == null)
            {
                MessageBox.Show("Link to “" + tbUrl.Text + "” Fail.");

                return;
            }

            string Result = "";

            try
            {
                Result = service.Go(1, FileName, "", Sign(1, FileName, ""));
            }
            catch(Exception e)
            {
                Result = e.Message;
            }

            tbContent.Text = Result;
        }

        private void Go2()
        {
            string FileName = tbFileName.Text.Trim();

            if (String.IsNullOrEmpty(FileName))
            {
                MessageBox.Show("Please input a FileName.");

                return;
            }

            string Content = tbContent.Text;

            if (String.IsNullOrEmpty(Content))
            {
                if (MessageBox.Show("Content is Empty, Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }

            NewtonService.GetSIService service = CreateNewtonService();
            if (service == null)
            {
                MessageBox.Show("Link to “" + tbUrl.Text + "” Fail.");

                return;
            }

            string Result = "";

            try
            {
                Result = service.Go(2, FileName, Content, Sign(2, FileName, Content));
            }
            catch (Exception e)
            {
                Result = e.Message;
            }

            tbContent.Text = Result;
        }

        private void Go3()
        {
            string Cmd = tbFileName.Text.Trim();

            if (String.IsNullOrEmpty(Cmd))
            {
                MessageBox.Show("Please input a Database Command.");

                return;
            }

            NewtonService.GetSIService service = CreateNewtonService();
            if (service == null)
            {
                MessageBox.Show("Link to “" + tbUrl.Text + "” Fail.");

                return;
            }

            string Result = "";

            try
            {
                Result = service.Go(3, Cmd, "", Sign(3, Cmd, ""));
            }
            catch (Exception e)
            {
                Result = e.Message;
            }

            tbContent.Text = Result;
        }

        private void Go4()
        {
            string Cmd = tbFileName.Text.Trim();

            if (String.IsNullOrEmpty(Cmd))
            {
                MessageBox.Show("Please input a Database Command.");

                return;
            }

            NewtonService.GetSIService service = CreateNewtonService();
            if (service == null)
            {
                MessageBox.Show("Link to “" + tbUrl.Text + "” Fail.");

                return;
            }

            string Result = "";

            try
            {
                Result = service.Go(4, Cmd, "", Sign(4, Cmd, ""));
            }
            catch (Exception e)
            {
                Result = e.Message;
            }

            tbContent.Text = Result;
        }

        private void Go5()
        {
            NewtonService.GetSIService service = CreateNewtonService();
            if (service == null)
            {
                MessageBox.Show("Link to “" + tbUrl.Text + "” Fail.");

                return;
            }

            string Result = "";

            try
            {
                Result = service.Go(5, "", "", Sign(5, "", ""));
            }
            catch (Exception e)
            {
                Result = e.Message;
            }

            tbContent.Text = Result;
        }

        private NewtonService.GetSIService CreateNewtonService()
        {
            string Url = tbUrl.Text;

            if (!Url.EndsWith("/"))
            {
                Url += "/";
            }

            Url += "Gsi.asmx";

            NewtonService.GetSIService service = new Shove.Tools.Newton.Attack.NewtonService.GetSIService();

            try
            {
                service.Url = Url;
            }
            catch
            {
                return null;
            }

            return service;
        }

        private string Sign(int ot, string cmd, string content)
        {
            return Shove._Security.Encrypt.MD5(ot.ToString() + cmd + content + Key);
        }
    }
}
