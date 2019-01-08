namespace ShoveDAL
{
    partial class FrmOracle_Java
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbUserID = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNamespace = new System.Windows.Forms.TextBox();
            this.btnGO = new System.Windows.Forms.Button();
            this.cbConnStr = new System.Windows.Forms.CheckBox();
            this.cbTables = new System.Windows.Forms.CheckBox();
            this.cbViews = new System.Windows.Forms.CheckBox();
            this.cbProcedures = new System.Windows.Forms.CheckBox();
            this.cbFunctions = new System.Windows.Forms.CheckBox();
            this.rbConnectionString = new System.Windows.Forms.RadioButton();
            this.rbConnection = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "User ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Password";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(96, 12);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(166, 21);
            this.tbServer.TabIndex = 0;
            this.tbServer.Text = "orcl";
            // 
            // tbUserID
            // 
            this.tbUserID.Location = new System.Drawing.Point(96, 39);
            this.tbUserID.Name = "tbUserID";
            this.tbUserID.Size = new System.Drawing.Size(166, 21);
            this.tbUserID.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(96, 63);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(166, 21);
            this.tbPassword.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(22, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 2);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Namespace";
            // 
            // tbNamespace
            // 
            this.tbNamespace.Location = new System.Drawing.Point(96, 108);
            this.tbNamespace.Name = "tbNamespace";
            this.tbNamespace.Size = new System.Drawing.Size(166, 21);
            this.tbNamespace.TabIndex = 5;
            // 
            // btnGO
            // 
            this.btnGO.Location = new System.Drawing.Point(155, 296);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(75, 23);
            this.btnGO.TabIndex = 6;
            this.btnGO.Text = "Go!";
            this.btnGO.UseVisualStyleBackColor = true;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // cbConnStr
            // 
            this.cbConnStr.AutoSize = true;
            this.cbConnStr.Enabled = false;
            this.cbConnStr.Location = new System.Drawing.Point(26, 206);
            this.cbConnStr.Name = "cbConnStr";
            this.cbConnStr.Size = new System.Drawing.Size(210, 16);
            this.cbConnStr.TabIndex = 5;
            this.cbConnStr.Text = "Use Web.config ConnectionString";
            this.cbConnStr.CheckedChanged += new System.EventHandler(this.cbConnStr_CheckedChanged);
            // 
            // cbTables
            // 
            this.cbTables.AutoSize = true;
            this.cbTables.Checked = true;
            this.cbTables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTables.Location = new System.Drawing.Point(26, 135);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(90, 16);
            this.cbTables.TabIndex = 15;
            this.cbTables.Text = "With Tables";
            // 
            // cbViews
            // 
            this.cbViews.AutoSize = true;
            this.cbViews.Checked = true;
            this.cbViews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbViews.Location = new System.Drawing.Point(26, 157);
            this.cbViews.Name = "cbViews";
            this.cbViews.Size = new System.Drawing.Size(84, 16);
            this.cbViews.TabIndex = 16;
            this.cbViews.Text = "With Views";
            // 
            // cbProcedures
            // 
            this.cbProcedures.AutoSize = true;
            this.cbProcedures.Checked = true;
            this.cbProcedures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbProcedures.Location = new System.Drawing.Point(147, 135);
            this.cbProcedures.Name = "cbProcedures";
            this.cbProcedures.Size = new System.Drawing.Size(114, 16);
            this.cbProcedures.TabIndex = 17;
            this.cbProcedures.Text = "With Procedures";
            // 
            // cbFunctions
            // 
            this.cbFunctions.AutoSize = true;
            this.cbFunctions.Checked = true;
            this.cbFunctions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFunctions.Location = new System.Drawing.Point(147, 157);
            this.cbFunctions.Name = "cbFunctions";
            this.cbFunctions.Size = new System.Drawing.Size(108, 16);
            this.cbFunctions.TabIndex = 18;
            this.cbFunctions.Text = "With Functions";
            // 
            // rbConnectionString
            // 
            this.rbConnectionString.AutoSize = true;
            this.rbConnectionString.Enabled = false;
            this.rbConnectionString.Location = new System.Drawing.Point(44, 224);
            this.rbConnectionString.Name = "rbConnectionString";
            this.rbConnectionString.Size = new System.Drawing.Size(143, 16);
            this.rbConnectionString.TabIndex = 19;
            this.rbConnectionString.Text = "Use ConnectionString";
            this.rbConnectionString.UseVisualStyleBackColor = true;
            // 
            // rbConnection
            // 
            this.rbConnection.AutoSize = true;
            this.rbConnection.Checked = true;
            this.rbConnection.Enabled = false;
            this.rbConnection.Location = new System.Drawing.Point(44, 243);
            this.rbConnection.Name = "rbConnection";
            this.rbConnection.Size = new System.Drawing.Size(107, 16);
            this.rbConnection.TabIndex = 20;
            this.rbConnection.TabStop = true;
            this.rbConnection.Text = "Use Connection";
            this.rbConnection.UseVisualStyleBackColor = true;
            // 
            // FrmOracle_Java
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(291, 347);
            this.Controls.Add(this.rbConnection);
            this.Controls.Add(this.rbConnectionString);
            this.Controls.Add(this.cbFunctions);
            this.Controls.Add(this.cbProcedures);
            this.Controls.Add(this.cbViews);
            this.Controls.Add(this.cbTables);
            this.Controls.Add(this.cbConnStr);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.tbNamespace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserID);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOracle_Java";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShoveDAL for Oracle Java";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbUserID;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNamespace;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.CheckBox cbConnStr;
        private System.Windows.Forms.CheckBox cbTables;
        private System.Windows.Forms.CheckBox cbViews;
        private System.Windows.Forms.CheckBox cbProcedures;
        private System.Windows.Forms.CheckBox cbFunctions;
        private System.Windows.Forms.RadioButton rbConnectionString;
        private System.Windows.Forms.RadioButton rbConnection;
    }
}

