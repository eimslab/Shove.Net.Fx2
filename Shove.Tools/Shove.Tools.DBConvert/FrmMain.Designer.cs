namespace Shove.Tools.DBConvert
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbSQLiteFileName = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGo = new System.Windows.Forms.Button();
            this.tbServer1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDatabase1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUserName1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPassword1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPassword2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUserName2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDatabase2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbServer2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "From SQLite file:";
            // 
            // tbSQLiteFileName
            // 
            this.tbSQLiteFileName.Location = new System.Drawing.Point(136, 19);
            this.tbSQLiteFileName.Name = "tbSQLiteFileName";
            this.tbSQLiteFileName.Size = new System.Drawing.Size(413, 21);
            this.tbSQLiteFileName.TabIndex = 1;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(555, 17);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(33, 23);
            this.btnOpenFile.TabIndex = 2;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*.*";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(25, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(563, 160);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbPassword1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tbUserName1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tbDatabase1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbServer1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(555, 134);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "To SQLServer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbPassword2);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.tbUserName2);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.tbDatabase2);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.tbServer2);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(555, 134);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "To MySQL";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(278, 242);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Convert !";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // tbServer1
            // 
            this.tbServer1.Location = new System.Drawing.Point(93, 15);
            this.tbServer1.Name = "tbServer1";
            this.tbServer1.Size = new System.Drawing.Size(448, 21);
            this.tbServer1.TabIndex = 3;
            this.tbServer1.Text = "(local)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server:";
            // 
            // tbDatabase1
            // 
            this.tbDatabase1.Location = new System.Drawing.Point(93, 42);
            this.tbDatabase1.Name = "tbDatabase1";
            this.tbDatabase1.Size = new System.Drawing.Size(448, 21);
            this.tbDatabase1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Database:";
            // 
            // tbUserName1
            // 
            this.tbUserName1.Location = new System.Drawing.Point(93, 69);
            this.tbUserName1.Name = "tbUserName1";
            this.tbUserName1.Size = new System.Drawing.Size(448, 21);
            this.tbUserName1.TabIndex = 7;
            this.tbUserName1.Text = "sa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "UserName:";
            // 
            // tbPassword1
            // 
            this.tbPassword1.Location = new System.Drawing.Point(93, 96);
            this.tbPassword1.Name = "tbPassword1";
            this.tbPassword1.Size = new System.Drawing.Size(448, 21);
            this.tbPassword1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Password:";
            // 
            // tbPassword2
            // 
            this.tbPassword2.Location = new System.Drawing.Point(93, 96);
            this.tbPassword2.Name = "tbPassword2";
            this.tbPassword2.Size = new System.Drawing.Size(448, 21);
            this.tbPassword2.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "Password:";
            // 
            // tbUserName2
            // 
            this.tbUserName2.Location = new System.Drawing.Point(93, 69);
            this.tbUserName2.Name = "tbUserName2";
            this.tbUserName2.Size = new System.Drawing.Size(448, 21);
            this.tbUserName2.TabIndex = 15;
            this.tbUserName2.Text = "root";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "UserName:";
            // 
            // tbDatabase2
            // 
            this.tbDatabase2.Location = new System.Drawing.Point(93, 42);
            this.tbDatabase2.Name = "tbDatabase2";
            this.tbDatabase2.Size = new System.Drawing.Size(448, 21);
            this.tbDatabase2.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "Database:";
            // 
            // tbServer2
            // 
            this.tbServer2.Location = new System.Drawing.Point(93, 15);
            this.tbServer2.Name = "tbServer2";
            this.tbServer2.Size = new System.Drawing.Size(448, 21);
            this.tbServer2.TabIndex = 11;
            this.tbServer2.Text = "localhost";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "Server:";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 287);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.tbSQLiteFileName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shove.Tools.DBConvert";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSQLiteFileName;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbPassword1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbUserName1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDatabase1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbServer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbUserName2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDatabase2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbServer2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGo;
    }
}

