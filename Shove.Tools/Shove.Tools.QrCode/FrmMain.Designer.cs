namespace Shove.Tools.QrCode
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCallingCard = new System.Windows.Forms.Button();
            this.pbCode = new System.Windows.Forms.PictureBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.pb2 = new System.Windows.Forms.PictureBox();
            this.tbContent2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.tbSelectImageFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tab3 = new System.Windows.Forms.TabPage();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCloseCamera = new System.Windows.Forms.Button();
            this.btnOpenCamera = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tab1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).BeginInit();
            this.tab2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).BeginInit();
            this.tab3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab1);
            this.tabControl1.Controls.Add(this.tab2);
            this.tabControl1.Controls.Add(this.tab3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(649, 412);
            this.tabControl1.TabIndex = 0;
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.label9);
            this.tab1.Controls.Add(this.button1);
            this.tab1.Controls.Add(this.label8);
            this.tab1.Controls.Add(this.btnCallingCard);
            this.tab1.Controls.Add(this.pbCode);
            this.tab1.Controls.Add(this.btnCreate);
            this.tab1.Controls.Add(this.tbHeight);
            this.tab1.Controls.Add(this.label4);
            this.tab1.Controls.Add(this.tbWidth);
            this.tab1.Controls.Add(this.label3);
            this.tab1.Controls.Add(this.cbFormat);
            this.tab1.Controls.Add(this.label2);
            this.tab1.Controls.Add(this.tbInput);
            this.tab1.Controls.Add(this.label1);
            this.tab1.Location = new System.Drawing.Point(4, 22);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(641, 386);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "从字符串生成二维码";
            this.tab1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(201, 370);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(257, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "提示：有图片的时候，可以双击图片保存文件。";
            // 
            // btnCallingCard
            // 
            this.btnCallingCard.Location = new System.Drawing.Point(513, 79);
            this.btnCallingCard.Name = "btnCallingCard";
            this.btnCallingCard.Size = new System.Drawing.Size(75, 23);
            this.btnCallingCard.TabIndex = 10;
            this.btnCallingCard.Text = "名片...";
            this.btnCallingCard.UseVisualStyleBackColor = true;
            this.btnCallingCard.Click += new System.EventHandler(this.btnCallingCard_Click);
            // 
            // pbCode
            // 
            this.pbCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCode.Location = new System.Drawing.Point(217, 167);
            this.pbCode.Name = "pbCode";
            this.pbCode.Size = new System.Drawing.Size(200, 200);
            this.pbCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCode.TabIndex = 9;
            this.pbCode.TabStop = false;
            this.pbCode.DoubleClick += new System.EventHandler(this.pbCode_DoubleClick);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(513, 119);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "生成二维码";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(414, 121);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(59, 21);
            this.tbHeight.TabIndex = 7;
            this.tbHeight.Text = "350";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(352, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "画布高度：";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(278, 121);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(59, 21);
            this.tbWidth.TabIndex = 5;
            this.tbWidth.Text = "350";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "画布宽度：";
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Items.AddRange(new object[] {
            "CODE_128",
            "CODE_39",
            "DATAMATRIX",
            "EAN_13",
            "EAN_8",
            "ITF",
            "PDF417",
            "QR_CODE",
            "UPC_A",
            "UPC_E"});
            this.cbFormat.Location = new System.Drawing.Point(78, 122);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(123, 20);
            this.cbFormat.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "条码版本：";
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(78, 15);
            this.tbInput.MaxLength = 1024;
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbInput.Size = new System.Drawing.Size(542, 96);
            this.tbInput.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "写入内容：";
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.pb2);
            this.tab2.Controls.Add(this.tbContent2);
            this.tab2.Controls.Add(this.label6);
            this.tab2.Controls.Add(this.btnSelectFile);
            this.tab2.Controls.Add(this.tbSelectImageFile);
            this.tab2.Controls.Add(this.label5);
            this.tab2.Location = new System.Drawing.Point(4, 22);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(641, 386);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "从图片文件读取二维码信息";
            this.tab2.UseVisualStyleBackColor = true;
            // 
            // pb2
            // 
            this.pb2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb2.Location = new System.Drawing.Point(210, 42);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(200, 200);
            this.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb2.TabIndex = 12;
            this.pb2.TabStop = false;
            // 
            // tbContent2
            // 
            this.tbContent2.Location = new System.Drawing.Point(78, 262);
            this.tbContent2.MaxLength = 1024;
            this.tbContent2.Multiline = true;
            this.tbContent2.Name = "tbContent2";
            this.tbContent2.ReadOnly = true;
            this.tbContent2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbContent2.Size = new System.Drawing.Size(542, 96);
            this.tbContent2.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "读出内容：";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(578, 13);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(33, 23);
            this.btnSelectFile.TabIndex = 3;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // tbSelectImageFile
            // 
            this.tbSelectImageFile.Location = new System.Drawing.Point(78, 15);
            this.tbSelectImageFile.Name = "tbSelectImageFile";
            this.tbSelectImageFile.Size = new System.Drawing.Size(494, 21);
            this.tbSelectImageFile.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "选择文件：";
            // 
            // tab3
            // 
            this.tab3.Controls.Add(this.tbResult);
            this.tab3.Controls.Add(this.label7);
            this.tab3.Controls.Add(this.btnCloseCamera);
            this.tab3.Controls.Add(this.btnOpenCamera);
            this.tab3.Controls.Add(this.panel1);
            this.tab3.Location = new System.Drawing.Point(4, 22);
            this.tab3.Name = "tab3";
            this.tab3.Size = new System.Drawing.Size(641, 386);
            this.tab3.TabIndex = 2;
            this.tab3.Text = "摄像头扫描二维码信息";
            this.tab3.UseVisualStyleBackColor = true;
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(80, 350);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(425, 21);
            this.tbResult.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 353);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "扫描结果：";
            // 
            // btnCloseCamera
            // 
            this.btnCloseCamera.Location = new System.Drawing.Point(535, 42);
            this.btnCloseCamera.Name = "btnCloseCamera";
            this.btnCloseCamera.Size = new System.Drawing.Size(83, 23);
            this.btnCloseCamera.TabIndex = 15;
            this.btnCloseCamera.Text = "关闭摄像头";
            this.btnCloseCamera.UseVisualStyleBackColor = true;
            this.btnCloseCamera.Click += new System.EventHandler(this.btnCloseCamera_Click);
            // 
            // btnOpenCamera
            // 
            this.btnOpenCamera.Location = new System.Drawing.Point(535, 13);
            this.btnOpenCamera.Name = "btnOpenCamera";
            this.btnOpenCamera.Size = new System.Drawing.Size(83, 23);
            this.btnOpenCamera.TabIndex = 14;
            this.btnOpenCamera.Text = "打开摄像头";
            this.btnOpenCamera.UseVisualStyleBackColor = true;
            this.btnOpenCamera.Click += new System.EventHandler(this.btnOpenCamera_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lime;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(14, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 331);
            this.panel1.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "二维码图片文件 png|*.png|二维码图片文件 bmp|*.bmp|二维码图片文件 jgp|*.jpg";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "二维码图片文件 png|*.png|二维码图片文件 bmp|*.bmp|二维码图片文件 jgp|*.jpg";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Logo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 218);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "未选择 Logo 文件";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图片文件 png|*.png|图片文件 bmp|*.bmp|图片文件 jgp|*.jpg|图片文件 gif|*.gif";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 436);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmMain";
            this.Text = "Shove.Tools.QrCode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCode)).EndInit();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).EndInit();
            this.tab3.ResumeLayout(false);
            this.tab3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tab2;
        private System.Windows.Forms.TabPage tab3;
        private System.Windows.Forms.PictureBox pbCode;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox tbSelectImageFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pb2;
        private System.Windows.Forms.TextBox tbContent2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCloseCamera;
        private System.Windows.Forms.Button btnOpenCamera;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnCallingCard;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

