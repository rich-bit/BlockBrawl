namespace BlockBrawl
{
    partial class PreConfigurations
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
            this.SettingsLbl = new System.Windows.Forms.Label();
            this.resolutionslst = new System.Windows.Forms.ComboBox();
            this.selectResolutionlbl = new System.Windows.Forms.Label();
            this.fullscreenchk = new System.Windows.Forms.CheckBox();
            this.dontShowAgainlbl = new System.Windows.Forms.CheckBox();
            this.runGamebtn = new System.Windows.Forms.Button();
            this.chkGamePad = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SettingsLbl
            // 
            this.SettingsLbl.AutoSize = true;
            this.SettingsLbl.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsLbl.ForeColor = System.Drawing.SystemColors.Window;
            this.SettingsLbl.Location = new System.Drawing.Point(12, 9);
            this.SettingsLbl.Name = "SettingsLbl";
            this.SettingsLbl.Size = new System.Drawing.Size(105, 19);
            this.SettingsLbl.TabIndex = 0;
            this.SettingsLbl.Text = "Settings";
            // 
            // resolutionslst
            // 
            this.resolutionslst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resolutionslst.FormattingEnabled = true;
            this.resolutionslst.Location = new System.Drawing.Point(16, 66);
            this.resolutionslst.Name = "resolutionslst";
            this.resolutionslst.Size = new System.Drawing.Size(157, 21);
            this.resolutionslst.TabIndex = 1;
            // 
            // selectResolutionlbl
            // 
            this.selectResolutionlbl.AutoSize = true;
            this.selectResolutionlbl.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectResolutionlbl.ForeColor = System.Drawing.SystemColors.Window;
            this.selectResolutionlbl.Location = new System.Drawing.Point(13, 45);
            this.selectResolutionlbl.Name = "selectResolutionlbl";
            this.selectResolutionlbl.Size = new System.Drawing.Size(160, 13);
            this.selectResolutionlbl.TabIndex = 2;
            this.selectResolutionlbl.Text = "Select Resolution";
            // 
            // fullscreenchk
            // 
            this.fullscreenchk.AutoSize = true;
            this.fullscreenchk.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullscreenchk.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.fullscreenchk.Location = new System.Drawing.Point(16, 93);
            this.fullscreenchk.Name = "fullscreenchk";
            this.fullscreenchk.Size = new System.Drawing.Size(104, 16);
            this.fullscreenchk.TabIndex = 3;
            this.fullscreenchk.Text = "Fullscreen";
            this.fullscreenchk.UseVisualStyleBackColor = true;
            // 
            // dontShowAgainlbl
            // 
            this.dontShowAgainlbl.AutoSize = true;
            this.dontShowAgainlbl.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dontShowAgainlbl.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dontShowAgainlbl.Location = new System.Drawing.Point(16, 137);
            this.dontShowAgainlbl.Name = "dontShowAgainlbl";
            this.dontShowAgainlbl.Size = new System.Drawing.Size(144, 16);
            this.dontShowAgainlbl.TabIndex = 4;
            this.dontShowAgainlbl.Text = "Dont show again";
            this.dontShowAgainlbl.UseVisualStyleBackColor = true;
            // 
            // runGamebtn
            // 
            this.runGamebtn.BackColor = System.Drawing.Color.AliceBlue;
            this.runGamebtn.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runGamebtn.Location = new System.Drawing.Point(166, 114);
            this.runGamebtn.Name = "runGamebtn";
            this.runGamebtn.Size = new System.Drawing.Size(75, 39);
            this.runGamebtn.TabIndex = 5;
            this.runGamebtn.Text = "Run game";
            this.runGamebtn.UseVisualStyleBackColor = false;
            this.runGamebtn.Click += new System.EventHandler(this.runGamebtn_Click);
            // 
            // chkGamePad
            // 
            this.chkGamePad.AutoSize = true;
            this.chkGamePad.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGamePad.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.chkGamePad.Location = new System.Drawing.Point(16, 115);
            this.chkGamePad.Name = "chkGamePad";
            this.chkGamePad.Size = new System.Drawing.Size(136, 16);
            this.chkGamePad.TabIndex = 6;
            this.chkGamePad.Text = "Gamepad Vesion";
            this.chkGamePad.UseVisualStyleBackColor = true;
            // 
            // PreConfigurations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(252, 164);
            this.ControlBox = false;
            this.Controls.Add(this.chkGamePad);
            this.Controls.Add(this.runGamebtn);
            this.Controls.Add(this.dontShowAgainlbl);
            this.Controls.Add(this.fullscreenchk);
            this.Controls.Add(this.selectResolutionlbl);
            this.Controls.Add(this.resolutionslst);
            this.Controls.Add(this.SettingsLbl);
            this.Name = "PreConfigurations";
            this.Text = "BlockBrawl - PreConfig";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SettingsLbl;
        private System.Windows.Forms.ComboBox resolutionslst;
        private System.Windows.Forms.Label selectResolutionlbl;
        private System.Windows.Forms.CheckBox fullscreenchk;
        private System.Windows.Forms.CheckBox dontShowAgainlbl;
        private System.Windows.Forms.Button runGamebtn;
        private System.Windows.Forms.CheckBox chkGamePad;
    }
}