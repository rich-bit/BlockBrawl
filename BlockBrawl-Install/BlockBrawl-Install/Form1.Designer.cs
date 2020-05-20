namespace Install_Template
{
    partial class Installation
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rtxInfoBox = new System.Windows.Forms.RichTextBox();
            this.chkShortCDesk = new System.Windows.Forms.CheckBox();
            this.chkShortCStartMenu = new System.Windows.Forms.CheckBox();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnChangeDir = new System.Windows.Forms.Button();
            this.rtxInstallDir = new System.Windows.Forms.RichTextBox();
            this.fldBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(458, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(331, 353);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // rtxInfoBox
            // 
            this.rtxInfoBox.BackColor = System.Drawing.Color.Black;
            this.rtxInfoBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxInfoBox.ForeColor = System.Drawing.Color.White;
            this.rtxInfoBox.Location = new System.Drawing.Point(12, 12);
            this.rtxInfoBox.Name = "rtxInfoBox";
            this.rtxInfoBox.ReadOnly = true;
            this.rtxInfoBox.Size = new System.Drawing.Size(776, 353);
            this.rtxInfoBox.TabIndex = 1;
            this.rtxInfoBox.Text = "";
            this.rtxInfoBox.TextChanged += new System.EventHandler(this.rtxInfoBox_TextChanged);
            // 
            // chkShortCDesk
            // 
            this.chkShortCDesk.AutoSize = true;
            this.chkShortCDesk.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShortCDesk.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.chkShortCDesk.Location = new System.Drawing.Point(364, 439);
            this.chkShortCDesk.Name = "chkShortCDesk";
            this.chkShortCDesk.Size = new System.Drawing.Size(233, 17);
            this.chkShortCDesk.TabIndex = 2;
            this.chkShortCDesk.Text = "Create Desktop Shortcut";
            this.chkShortCDesk.UseVisualStyleBackColor = true;
            // 
            // chkShortCStartMenu
            // 
            this.chkShortCStartMenu.AutoSize = true;
            this.chkShortCStartMenu.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShortCStartMenu.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.chkShortCStartMenu.Location = new System.Drawing.Point(364, 416);
            this.chkShortCStartMenu.Name = "chkShortCStartMenu";
            this.chkShortCStartMenu.Size = new System.Drawing.Size(260, 17);
            this.chkShortCStartMenu.TabIndex = 3;
            this.chkShortCStartMenu.Text = "Create Start-Menu Shortcut";
            this.chkShortCStartMenu.UseVisualStyleBackColor = true;
            // 
            // btnInstall
            // 
            this.btnInstall.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstall.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnInstall.Location = new System.Drawing.Point(642, 416);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(146, 40);
            this.btnInstall.TabIndex = 6;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnChangeDir
            // 
            this.btnChangeDir.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeDir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnChangeDir.Location = new System.Drawing.Point(642, 371);
            this.btnChangeDir.Name = "btnChangeDir";
            this.btnChangeDir.Size = new System.Drawing.Size(146, 41);
            this.btnChangeDir.TabIndex = 8;
            this.btnChangeDir.Text = "Change Directory";
            this.btnChangeDir.UseVisualStyleBackColor = true;
            this.btnChangeDir.Click += new System.EventHandler(this.btnChangeDir_Click);
            // 
            // rtxInstallDir
            // 
            this.rtxInstallDir.BackColor = System.Drawing.Color.Black;
            this.rtxInstallDir.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxInstallDir.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rtxInstallDir.Location = new System.Drawing.Point(13, 382);
            this.rtxInstallDir.Name = "rtxInstallDir";
            this.rtxInstallDir.ReadOnly = true;
            this.rtxInstallDir.Size = new System.Drawing.Size(611, 19);
            this.rtxInstallDir.TabIndex = 9;
            this.rtxInstallDir.Text = "";
            // 
            // fldBrowser
            // 
            this.fldBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClose.Location = new System.Drawing.Point(12, 427);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(146, 29);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Installation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(801, 468);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.rtxInstallDir);
            this.Controls.Add(this.btnChangeDir);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.chkShortCStartMenu);
            this.Controls.Add(this.chkShortCDesk);
            this.Controls.Add(this.rtxInfoBox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Installation";
            this.Text = "BlockBrawl Installation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Installation_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox rtxInfoBox;
        private System.Windows.Forms.CheckBox chkShortCDesk;
        private System.Windows.Forms.CheckBox chkShortCStartMenu;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnChangeDir;
        private System.Windows.Forms.RichTextBox rtxInstallDir;
        private System.Windows.Forms.FolderBrowserDialog fldBrowser;
        private System.Windows.Forms.Button btnClose;
    }
}

