namespace ShoveDAL
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNetSQLServer = new System.Windows.Forms.Button();
            this.btnNetMySQL = new System.Windows.Forms.Button();
            this.btnNetOracle = new System.Windows.Forms.Button();
            this.btnNetSQLite = new System.Windows.Forms.Button();
            this.btnJavaSQLite = new System.Windows.Forms.Button();
            this.btnJavaOracle = new System.Windows.Forms.Button();
            this.btnJavaMySQL = new System.Windows.Forms.Button();
            this.btnJavaSQLServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "1、ShoveDAL for .NET";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "2、ShoveDAL for Java JDBC";
            // 
            // btnNetSQLServer
            // 
            this.btnNetSQLServer.Location = new System.Drawing.Point(57, 69);
            this.btnNetSQLServer.Name = "btnNetSQLServer";
            this.btnNetSQLServer.Size = new System.Drawing.Size(75, 23);
            this.btnNetSQLServer.TabIndex = 2;
            this.btnNetSQLServer.Text = "SQLServer";
            this.btnNetSQLServer.UseVisualStyleBackColor = true;
            this.btnNetSQLServer.Click += new System.EventHandler(this.btnNetSQLServer_Click);
            // 
            // btnNetMySQL
            // 
            this.btnNetMySQL.Location = new System.Drawing.Point(164, 69);
            this.btnNetMySQL.Name = "btnNetMySQL";
            this.btnNetMySQL.Size = new System.Drawing.Size(75, 23);
            this.btnNetMySQL.TabIndex = 3;
            this.btnNetMySQL.Text = "MySQL";
            this.btnNetMySQL.UseVisualStyleBackColor = true;
            this.btnNetMySQL.Click += new System.EventHandler(this.btnNetMySQL_Click);
            // 
            // btnNetOracle
            // 
            this.btnNetOracle.Location = new System.Drawing.Point(271, 69);
            this.btnNetOracle.Name = "btnNetOracle";
            this.btnNetOracle.Size = new System.Drawing.Size(75, 23);
            this.btnNetOracle.TabIndex = 4;
            this.btnNetOracle.Text = "Oracle";
            this.btnNetOracle.UseVisualStyleBackColor = true;
            this.btnNetOracle.Click += new System.EventHandler(this.btnNetOracle_Click);
            // 
            // btnNetSQLite
            // 
            this.btnNetSQLite.Location = new System.Drawing.Point(378, 69);
            this.btnNetSQLite.Name = "btnNetSQLite";
            this.btnNetSQLite.Size = new System.Drawing.Size(75, 23);
            this.btnNetSQLite.TabIndex = 5;
            this.btnNetSQLite.Text = "SQLite";
            this.btnNetSQLite.UseVisualStyleBackColor = true;
            this.btnNetSQLite.Click += new System.EventHandler(this.btnNetSQLite_Click);
            // 
            // btnJavaSQLite
            // 
            this.btnJavaSQLite.Location = new System.Drawing.Point(378, 176);
            this.btnJavaSQLite.Name = "btnJavaSQLite";
            this.btnJavaSQLite.Size = new System.Drawing.Size(75, 23);
            this.btnJavaSQLite.TabIndex = 9;
            this.btnJavaSQLite.Text = "SQLite";
            this.btnJavaSQLite.UseVisualStyleBackColor = true;
            this.btnJavaSQLite.Click += new System.EventHandler(this.btnJavaSQLite_Click);
            // 
            // btnJavaOracle
            // 
            this.btnJavaOracle.Location = new System.Drawing.Point(271, 176);
            this.btnJavaOracle.Name = "btnJavaOracle";
            this.btnJavaOracle.Size = new System.Drawing.Size(75, 23);
            this.btnJavaOracle.TabIndex = 8;
            this.btnJavaOracle.Text = "Oracle";
            this.btnJavaOracle.UseVisualStyleBackColor = true;
            this.btnJavaOracle.Click += new System.EventHandler(this.btnJavaOracle_Click);
            // 
            // btnJavaMySQL
            // 
            this.btnJavaMySQL.Location = new System.Drawing.Point(164, 176);
            this.btnJavaMySQL.Name = "btnJavaMySQL";
            this.btnJavaMySQL.Size = new System.Drawing.Size(75, 23);
            this.btnJavaMySQL.TabIndex = 7;
            this.btnJavaMySQL.Text = "MySQL";
            this.btnJavaMySQL.UseVisualStyleBackColor = true;
            this.btnJavaMySQL.Click += new System.EventHandler(this.btnJavaMySQL_Click);
            // 
            // btnJavaSQLServer
            // 
            this.btnJavaSQLServer.Location = new System.Drawing.Point(57, 176);
            this.btnJavaSQLServer.Name = "btnJavaSQLServer";
            this.btnJavaSQLServer.Size = new System.Drawing.Size(75, 23);
            this.btnJavaSQLServer.TabIndex = 6;
            this.btnJavaSQLServer.Text = "SQLServer";
            this.btnJavaSQLServer.UseVisualStyleBackColor = true;
            this.btnJavaSQLServer.Click += new System.EventHandler(this.btnJavaSQLServer_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 238);
            this.Controls.Add(this.btnJavaSQLite);
            this.Controls.Add(this.btnJavaOracle);
            this.Controls.Add(this.btnJavaMySQL);
            this.Controls.Add(this.btnJavaSQLServer);
            this.Controls.Add(this.btnNetSQLite);
            this.Controls.Add(this.btnNetOracle);
            this.Controls.Add(this.btnNetMySQL);
            this.Controls.Add(this.btnNetSQLServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "ShoveDAL Tools 4.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNetSQLServer;
        private System.Windows.Forms.Button btnNetMySQL;
        private System.Windows.Forms.Button btnNetOracle;
        private System.Windows.Forms.Button btnNetSQLite;
        private System.Windows.Forms.Button btnJavaSQLite;
        private System.Windows.Forms.Button btnJavaOracle;
        private System.Windows.Forms.Button btnJavaMySQL;
        private System.Windows.Forms.Button btnJavaSQLServer;
    }
}