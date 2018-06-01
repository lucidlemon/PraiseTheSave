namespace PraiseTheSave
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.ds3link = new System.Windows.Forms.Label();
            this.ds3_found_folder = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.chooseDirectoryButton = new System.Windows.Forms.Button();
            this.ds3_last_change_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.saveAmountInput = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveIntervalInput = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.backupNow = new System.Windows.Forms.Button();
            this.lastBackupLabel = new System.Windows.Forms.Label();
            this.backupFolderLabel = new System.Windows.Forms.Label();
            this.backupFolderSizeLabel = new System.Windows.Forms.Label();
            this.ds2_last_change_label = new System.Windows.Forms.Label();
            this.ds2_found_folder = new System.Windows.Forms.Label();
            this.ds2link = new System.Windows.Forms.Label();
            this.ds1_last_change_label = new System.Windows.Forms.Label();
            this.ds1_found_folder = new System.Windows.Forms.Label();
            this.ds1link = new System.Windows.Forms.Label();
            this.activateAutomaticBackups = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ds1R_last_change_label = new System.Windows.Forms.Label();
            this.ds1R_found_folder = new System.Windows.Forms.Label();
            this.ds1Rlink = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saveAmountInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saveIntervalInput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "Praise The Save";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ds3link
            // 
            this.ds3link.AutoSize = true;
            this.ds3link.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds3link.Location = new System.Drawing.Point(20, 446);
            this.ds3link.Name = "ds3link";
            this.ds3link.Size = new System.Drawing.Size(90, 17);
            this.ds3link.TabIndex = 1;
            this.ds3link.Text = "Dark Souls III";
            this.ds3link.Click += new System.EventHandler(this.Ds3link_Click);
            // 
            // ds3_found_folder
            // 
            this.ds3_found_folder.AutoSize = true;
            this.ds3_found_folder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ds3_found_folder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds3_found_folder.Location = new System.Drawing.Point(35, 475);
            this.ds3_found_folder.Name = "ds3_found_folder";
            this.ds3_found_folder.Size = new System.Drawing.Size(117, 13);
            this.ds3_found_folder.TabIndex = 4;
            this.ds3_found_folder.Text = "No SaveGames found";
            this.ds3_found_folder.Click += new System.EventHandler(this.Ds3link_Click);
            // 
            // chooseDirectoryButton
            // 
            this.chooseDirectoryButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseDirectoryButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chooseDirectoryButton.Location = new System.Drawing.Point(213, 153);
            this.chooseDirectoryButton.Name = "chooseDirectoryButton";
            this.chooseDirectoryButton.Size = new System.Drawing.Size(141, 23);
            this.chooseDirectoryButton.TabIndex = 5;
            this.chooseDirectoryButton.Text = "choose save location";
            this.chooseDirectoryButton.UseVisualStyleBackColor = true;
            this.chooseDirectoryButton.Click += new System.EventHandler(this.ChooseDirectory);
            // 
            // ds3_last_change_label
            // 
            this.ds3_last_change_label.AutoSize = true;
            this.ds3_last_change_label.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds3_last_change_label.Location = new System.Drawing.Point(35, 493);
            this.ds3_last_change_label.Name = "ds3_last_change_label";
            this.ds3_last_change_label.Size = new System.Drawing.Size(183, 13);
            this.ds3_last_change_label.TabIndex = 6;
            this.ds3_last_change_label.Text = "Last Change couldn\'t be detected.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Where to save the backups";
            // 
            // saveAmountInput
            // 
            this.saveAmountInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.saveAmountInput.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveAmountInput.Location = new System.Drawing.Point(23, 229);
            this.saveAmountInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.saveAmountInput.Name = "saveAmountInput";
            this.saveAmountInput.Size = new System.Drawing.Size(43, 18);
            this.saveAmountInput.TabIndex = 8;
            this.saveAmountInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.saveAmountInput.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.saveAmountInput.ValueChanged += new System.EventHandler(this.SaveAmountInput_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(79, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "No of backups";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(242, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Backup interval in min";
            // 
            // saveIntervalInput
            // 
            this.saveIntervalInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.saveIntervalInput.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveIntervalInput.Location = new System.Drawing.Point(186, 229);
            this.saveIntervalInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.saveIntervalInput.Name = "saveIntervalInput";
            this.saveIntervalInput.Size = new System.Drawing.Size(43, 18);
            this.saveIntervalInput.TabIndex = 11;
            this.saveIntervalInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.saveIntervalInput.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.saveIntervalInput.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(315, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "This little tool will backup the savegames of your DarkSouls.";
            // 
            // backupNow
            // 
            this.backupNow.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backupNow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.backupNow.Location = new System.Drawing.Point(245, 612);
            this.backupNow.Name = "backupNow";
            this.backupNow.Size = new System.Drawing.Size(109, 51);
            this.backupNow.TabIndex = 13;
            this.backupNow.Text = "Backup Now.";
            this.backupNow.UseVisualStyleBackColor = true;
            this.backupNow.Click += new System.EventHandler(this.DoBackup);
            // 
            // lastBackupLabel
            // 
            this.lastBackupLabel.AutoSize = true;
            this.lastBackupLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastBackupLabel.Location = new System.Drawing.Point(20, 622);
            this.lastBackupLabel.MaximumSize = new System.Drawing.Size(219, 50);
            this.lastBackupLabel.Name = "lastBackupLabel";
            this.lastBackupLabel.Size = new System.Drawing.Size(159, 13);
            this.lastBackupLabel.TabIndex = 14;
            this.lastBackupLabel.Text = "Last backup created at: never.";
            // 
            // backupFolderLabel
            // 
            this.backupFolderLabel.AutoEllipsis = true;
            this.backupFolderLabel.AutoSize = true;
            this.backupFolderLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backupFolderLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backupFolderLabel.Location = new System.Drawing.Point(20, 111);
            this.backupFolderLabel.Name = "backupFolderLabel";
            this.backupFolderLabel.Size = new System.Drawing.Size(170, 13);
            this.backupFolderLabel.TabIndex = 15;
            this.backupFolderLabel.Text = "Backing up to: C:\\PraiseTheSave";
            this.backupFolderLabel.Click += new System.EventHandler(this.BackupFolderLabel_Click);
            // 
            // backupFolderSizeLabel
            // 
            this.backupFolderSizeLabel.AutoEllipsis = true;
            this.backupFolderSizeLabel.AutoSize = true;
            this.backupFolderSizeLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backupFolderSizeLabel.Location = new System.Drawing.Point(20, 124);
            this.backupFolderSizeLabel.Name = "backupFolderSizeLabel";
            this.backupFolderSizeLabel.Size = new System.Drawing.Size(150, 13);
            this.backupFolderSizeLabel.TabIndex = 16;
            this.backupFolderSizeLabel.Text = "Size of these backups: 0 Mb";
            // 
            // ds2_last_change_label
            // 
            this.ds2_last_change_label.AutoSize = true;
            this.ds2_last_change_label.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds2_last_change_label.Location = new System.Drawing.Point(35, 410);
            this.ds2_last_change_label.Name = "ds2_last_change_label";
            this.ds2_last_change_label.Size = new System.Drawing.Size(183, 13);
            this.ds2_last_change_label.TabIndex = 19;
            this.ds2_last_change_label.Text = "Last Change couldn\'t be detected.";
            // 
            // ds2_found_folder
            // 
            this.ds2_found_folder.AutoSize = true;
            this.ds2_found_folder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ds2_found_folder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds2_found_folder.Location = new System.Drawing.Point(35, 392);
            this.ds2_found_folder.Name = "ds2_found_folder";
            this.ds2_found_folder.Size = new System.Drawing.Size(117, 13);
            this.ds2_found_folder.TabIndex = 18;
            this.ds2_found_folder.Text = "No SaveGames found";
            this.ds2_found_folder.Click += new System.EventHandler(this.Ds2link_Click);
            // 
            // ds2link
            // 
            this.ds2link.AutoSize = true;
            this.ds2link.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds2link.Location = new System.Drawing.Point(20, 363);
            this.ds2link.Name = "ds2link";
            this.ds2link.Size = new System.Drawing.Size(194, 17);
            this.ds2link.TabIndex = 17;
            this.ds2link.Text = "Dark Souls II (including SotfS)";
            this.ds2link.Click += new System.EventHandler(this.Ds2link_Click);
            // 
            // ds1_last_change_label
            // 
            this.ds1_last_change_label.AutoSize = true;
            this.ds1_last_change_label.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds1_last_change_label.Location = new System.Drawing.Point(35, 324);
            this.ds1_last_change_label.Name = "ds1_last_change_label";
            this.ds1_last_change_label.Size = new System.Drawing.Size(183, 13);
            this.ds1_last_change_label.TabIndex = 22;
            this.ds1_last_change_label.Text = "Last Change couldn\'t be detected.";
            // 
            // ds1_found_folder
            // 
            this.ds1_found_folder.AutoSize = true;
            this.ds1_found_folder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ds1_found_folder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds1_found_folder.Location = new System.Drawing.Point(35, 306);
            this.ds1_found_folder.Name = "ds1_found_folder";
            this.ds1_found_folder.Size = new System.Drawing.Size(117, 13);
            this.ds1_found_folder.TabIndex = 21;
            this.ds1_found_folder.Text = "No SaveGames found";
            this.ds1_found_folder.Click += new System.EventHandler(this.Ds1link_Click);
            // 
            // ds1link
            // 
            this.ds1link.AutoSize = true;
            this.ds1link.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds1link.Location = new System.Drawing.Point(20, 277);
            this.ds1link.Name = "ds1link";
            this.ds1link.Size = new System.Drawing.Size(82, 17);
            this.ds1link.TabIndex = 20;
            this.ds1link.Text = "Dark Souls I";
            this.ds1link.Click += new System.EventHandler(this.Ds1link_Click);
            // 
            // activateAutomaticBackups
            // 
            this.activateAutomaticBackups.AutoSize = true;
            this.activateAutomaticBackups.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activateAutomaticBackups.Location = new System.Drawing.Point(23, 203);
            this.activateAutomaticBackups.Name = "activateAutomaticBackups";
            this.activateAutomaticBackups.Size = new System.Drawing.Size(167, 17);
            this.activateAutomaticBackups.TabIndex = 23;
            this.activateAutomaticBackups.Text = "Activate Automatic Backups";
            this.activateAutomaticBackups.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.activateAutomaticBackups.UseVisualStyleBackColor = true;
            this.activateAutomaticBackups.CheckedChanged += new System.EventHandler(this.ActivateAutomaticBackups_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkGray;
            this.label5.Location = new System.Drawing.Point(122, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "created by /u/karreerose";
            // 
            // ds1R_last_change_label
            // 
            this.ds1R_last_change_label.AutoSize = true;
            this.ds1R_last_change_label.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds1R_last_change_label.Location = new System.Drawing.Point(35, 578);
            this.ds1R_last_change_label.Name = "ds1R_last_change_label";
            this.ds1R_last_change_label.Size = new System.Drawing.Size(183, 13);
            this.ds1R_last_change_label.TabIndex = 28;
            this.ds1R_last_change_label.Text = "Last Change couldn\'t be detected.";
            // 
            // ds1R_found_folder
            // 
            this.ds1R_found_folder.AutoSize = true;
            this.ds1R_found_folder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ds1R_found_folder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds1R_found_folder.Location = new System.Drawing.Point(35, 560);
            this.ds1R_found_folder.Name = "ds1R_found_folder";
            this.ds1R_found_folder.Size = new System.Drawing.Size(117, 13);
            this.ds1R_found_folder.TabIndex = 27;
            this.ds1R_found_folder.Text = "No SaveGames found";
            this.ds1R_found_folder.Click += new System.EventHandler(this.Ds1Rlink_Click);
            // 
            // ds1Rlink
            // 
            this.ds1Rlink.AutoSize = true;
            this.ds1Rlink.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ds1Rlink.Location = new System.Drawing.Point(20, 531);
            this.ds1Rlink.Name = "ds1Rlink";
            this.ds1Rlink.Size = new System.Drawing.Size(158, 17);
            this.ds1Rlink.TabIndex = 26;
            this.ds1Rlink.Text = "Dark Souls I Remastered";
            this.ds1Rlink.Click += new System.EventHandler(this.Ds1Rlink_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(366, 675);
            this.Controls.Add(this.ds1R_last_change_label);
            this.Controls.Add(this.ds1R_found_folder);
            this.Controls.Add(this.ds1Rlink);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.activateAutomaticBackups);
            this.Controls.Add(this.ds1_last_change_label);
            this.Controls.Add(this.ds1_found_folder);
            this.Controls.Add(this.ds1link);
            this.Controls.Add(this.ds2_last_change_label);
            this.Controls.Add(this.ds2_found_folder);
            this.Controls.Add(this.ds2link);
            this.Controls.Add(this.backupFolderSizeLabel);
            this.Controls.Add(this.backupFolderLabel);
            this.Controls.Add(this.lastBackupLabel);
            this.Controls.Add(this.backupNow);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.saveIntervalInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saveAmountInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ds3_last_change_label);
            this.Controls.Add(this.chooseDirectoryButton);
            this.Controls.Add(this.ds3_found_folder);
            this.Controls.Add(this.ds3link);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Praise The Save";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saveAmountInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saveIntervalInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ds3link;
        private System.Windows.Forms.Label ds3_found_folder;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button chooseDirectoryButton;
        private System.Windows.Forms.Label ds3_last_change_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.NumericUpDown saveAmountInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown saveIntervalInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button backupNow;
        private System.Windows.Forms.Label lastBackupLabel;
        private System.Windows.Forms.Label backupFolderLabel;
        private System.Windows.Forms.Label backupFolderSizeLabel;
        private System.Windows.Forms.Label ds2_last_change_label;
        private System.Windows.Forms.Label ds2_found_folder;
        private System.Windows.Forms.Label ds2link;
        private System.Windows.Forms.Label ds1_last_change_label;
        private System.Windows.Forms.Label ds1_found_folder;
        private System.Windows.Forms.Label ds1link;
        private System.Windows.Forms.CheckBox activateAutomaticBackups;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ds1R_last_change_label;
        private System.Windows.Forms.Label ds1R_found_folder;
        private System.Windows.Forms.Label ds1Rlink;
    }
}

