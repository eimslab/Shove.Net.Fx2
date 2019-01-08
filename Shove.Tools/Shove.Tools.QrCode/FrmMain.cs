using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.google.zxing;
using com.google.zxing.common;
using System.IO;

namespace Shove.Tools.QrCode
{
    public partial class FrmMain : Form
    {
        public IList<string> TempFiles = new List<string>();
        private string LogoFileName = "";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.cbFormat.SelectedIndex = 7;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (tbInput.Text.Trim() == "")
            {
                MessageBox.Show("请输入需要生成的内容。");
                tbInput.Focus();

                return;
            }

            BarcodeFormat format = BarcodeFormat.QR_CODE;

            switch (this.cbFormat.SelectedIndex)
            {
                case 0:
                    format = BarcodeFormat.CODE_128;
                    break;
                case 1:
                    format = BarcodeFormat.CODE_39;
                    break;
                case 2:
                    format = BarcodeFormat.DATAMATRIX;
                    break;
                case 3:
                    format = BarcodeFormat.EAN_13;
                    break;
                case 4:
                    format = BarcodeFormat.EAN_8;
                    break;
                case 5:
                    format = BarcodeFormat.ITF;
                    break;
                case 6:
                    format = BarcodeFormat.PDF417;
                    break;
                case 7:
                    format = BarcodeFormat.QR_CODE;
                    break;
                case 8:
                    format = BarcodeFormat.UPC_A;
                    break;
                case 9:
                    format = BarcodeFormat.UPC_E;
                    break;
                default:
                    MessageBox.Show("不支持的格式。");
                    break;
            }

            int width = 350;
            int.TryParse(tbWidth.Text, out width);
            int height = 350;
            int.TryParse(tbHeight.Text, out height);

            if ((width < 1) || (height < 1))
            {
                MessageBox.Show("请输入有效的画布宽度和高度。");

                return;
            }

            string outputFileName = System.IO.Path.GetTempFileName();
            System.IO.File.Delete(outputFileName);
            outputFileName += ".png";

            Shove.InformationCode.QrCode.CreateCode(tbInput.Text, format, width, height, outputFileName, System.Drawing.Imaging.ImageFormat.Png, LogoFileName);
            this.pbCode.ImageLocation = outputFileName;

            TempFiles.Add(outputFileName);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            try
            {
                this.pb2.ImageLocation = openFileDialog1.FileName;
                tbContent2.Text = Shove.InformationCode.QrCode.ReadCode(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                tbContent2.Text = ex.Message;
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (string file in TempFiles)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch { }
            }
        }

        private Shove._System.Device.CameraWorker _camWorker = null;
        System.Timers.Timer _timer = null;
        int failCount = 0;

        private void btnOpenCamera_Click(object sender, EventArgs e)
        {
            failCount = 0;

            _camWorker = new Shove._System.Device.CameraWorker(panel1.Handle, 0, 0, panel1.Width, panel1.Height);
            _camWorker.Open();
            tbResult.Text = "Cameram is working...";

            if (_timer != null && _timer.Enabled)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            _timer = new System.Timers.Timer(1000);
            _timer.Start();
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string outputFileName = System.IO.Path.GetTempFileName();
            System.IO.File.Delete(outputFileName);
            outputFileName += ".bmp";
            string code = "";

            TempFiles.Add(outputFileName);

            try
            {
                _camWorker.Capture(outputFileName);

                if (File.Exists(outputFileName))
                {
                    code = Shove.InformationCode.QrCode.ReadCode(outputFileName);

                    tbResult.BeginInvoke(new Action(() =>
                    {
                        tbResult.Text = code;
                    }));

                    CloseCamera();
                }
            }
            catch(Exception ex)
            {
                failCount++;
                if (failCount > 200)
                {
                    CloseCamera();
                }

                tbResult.BeginInvoke(new Action(() =>
                {
                    tbResult.Text = ex.Message;
                }));
            }
        }

        private void btnCloseCamera_Click(object sender, EventArgs e)
        {
            CloseCamera();
        }

        private void CloseCamera()
        {
            if (_timer != null && _timer.Enabled)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            _camWorker.Close();
        }

        private void pbCode_DoubleClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.pbCode.ImageLocation))
            {
                return;
            }

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            File.Copy(this.pbCode.ImageLocation, saveFileDialog1.FileName, true);
        }

        private void btnCallingCard_Click(object sender, EventArgs e)
        {
            FrmCallingCard fcc = new FrmCallingCard();

            if (fcc.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            string str = "BEGIN:VCARD\r\nVERSION:3.0\r\n";
            str += "FN:" + fcc.textBox1.Text.Trim() + "\r\n";
            str += "TEL;CELL;VOICE:" + fcc.textBox2.Text.Trim() + "\r\n";
            str += "TEL;WORK;VOICE:" + fcc.textBox3.Text.Trim() + "\r\n";
            str += "TEL;WORK;FAX:" + fcc.textBox4.Text.Trim() + "\r\n";
            str += "EMAIL;PREF;INTERNET:" + fcc.textBox5.Text.Trim() + "\r\n";
            str += "URL:" + fcc.textBox6.Text.Trim() + "\r\n";
            str += "orG:" + fcc.textBox7.Text.Trim() + "\r\n";
            str += "ROLE:" + fcc.textBox8.Text.Trim() + "\r\n";
            str += "TITLE:" + fcc.textBox9.Text.Trim() + "\r\n";
            str += "ADR;WORK;POSTAL:" + fcc.textBox10.Text.Trim() + ";" + fcc.textBox11.Text.Trim() + "\r\n";
            str += "REV:" + "\r\n";
            str += "END:VCARD";

            tbInput.Text = str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                label9.Text = "未选择 Logo 文件";
                LogoFileName = "";

                return;
            }

            LogoFileName = openFileDialog2.FileName;
            label9.Text = System.IO.Path.GetFileName(LogoFileName);
        }
    }
}
