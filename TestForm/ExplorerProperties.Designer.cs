﻿namespace MainForm
{
    partial class ExplorerProperties
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.labelSeparatorTwo = new System.Windows.Forms.Label();
            this.buttonOkay = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pictureBoxImageIcon = new System.Windows.Forms.PictureBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelAllSizes = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Location = new System.Drawing.Point(7, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(293, 342);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.textBoxPath);
            this.tabPageGeneral.Controls.Add(this.labelAllSizes);
            this.tabPageGeneral.Controls.Add(this.labelSize);
            this.tabPageGeneral.Controls.Add(this.labelLocation);
            this.tabPageGeneral.Controls.Add(this.pictureBoxImageIcon);
            this.tabPageGeneral.Controls.Add(this.labelSeparatorTwo);
            this.tabPageGeneral.Controls.Add(this.labelSeparator);
            this.tabPageGeneral.Controls.Add(this.textBoxFileName);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(285, 316);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(95, 16);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(184, 20);
            this.textBoxFileName.TabIndex = 0;
            // 
            // labelSeparator
            // 
            this.labelSeparator.AutoSize = true;
            this.labelSeparator.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelSeparator.Location = new System.Drawing.Point(8, 53);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(271, 13);
            this.labelSeparator.TabIndex = 1;
            this.labelSeparator.Text = "____________________________________________";
            // 
            // labelSeparatorTwo
            // 
            this.labelSeparatorTwo.AutoSize = true;
            this.labelSeparatorTwo.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelSeparatorTwo.Location = new System.Drawing.Point(8, 170);
            this.labelSeparatorTwo.Name = "labelSeparatorTwo";
            this.labelSeparatorTwo.Size = new System.Drawing.Size(271, 13);
            this.labelSeparatorTwo.TabIndex = 2;
            this.labelSeparatorTwo.Text = "____________________________________________";
            // 
            // buttonOkay
            // 
            this.buttonOkay.Location = new System.Drawing.Point(144, 348);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(75, 23);
            this.buttonOkay.TabIndex = 1;
            this.buttonOkay.Text = "OK";
            this.buttonOkay.UseVisualStyleBackColor = true;
            this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(225, 348);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // pictureBoxImageIcon
            // 
            this.pictureBoxImageIcon.Image = global::MainForm.Properties.Resources.txt_file;
            this.pictureBoxImageIcon.Location = new System.Drawing.Point(29, 16);
            this.pictureBoxImageIcon.Name = "pictureBoxImageIcon";
            this.pictureBoxImageIcon.Size = new System.Drawing.Size(18, 19);
            this.pictureBoxImageIcon.TabIndex = 3;
            this.pictureBoxImageIcon.TabStop = false;
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocation.Location = new System.Drawing.Point(8, 83);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(54, 13);
            this.labelLocation.TabIndex = 4;
            this.labelLocation.Text = "Location:";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSize.Location = new System.Drawing.Point(8, 121);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(30, 13);
            this.labelSize.TabIndex = 5;
            this.labelSize.Text = "Size:";
            // 
            // labelAllSizes
            // 
            this.labelAllSizes.AutoSize = true;
            this.labelAllSizes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAllSizes.Location = new System.Drawing.Point(92, 121);
            this.labelAllSizes.Name = "labelAllSizes";
            this.labelAllSizes.Size = new System.Drawing.Size(26, 13);
            this.labelAllSizes.TabIndex = 7;
            this.labelAllSizes.Text = "size";
            // 
            // textBoxPath
            // 
            this.textBoxPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPath.Location = new System.Drawing.Point(95, 83);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(184, 13);
            this.textBoxPath.TabIndex = 8;
            this.textBoxPath.TabStop = false;
            // 
            // ExplorerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 395);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOkay);
            this.Controls.Add(this.tabControl);
            this.Name = "ExplorerProperties";
            this.Text = "ExplorerProperties";
            this.Load += new System.EventHandler(this.ExplorerProperties_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImageIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Label labelSeparator;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label labelSeparatorTwo;
        private System.Windows.Forms.Button buttonOkay;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBoxImageIcon;
        private System.Windows.Forms.Label labelAllSizes;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.TextBox textBoxPath;
    }
}