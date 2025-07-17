namespace Process_Line_Data_Manager
{
    partial class frmGetExcelRecord
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
            this.gbHRULines = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCodeJurisdiction = new System.Windows.Forms.Label();
            this.lblTemperatureValue = new System.Windows.Forms.Label();
            this.lblPressureValue = new System.Windows.Forms.Label();
            this.lblANSIClass = new System.Windows.Forms.Label();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.lblPipeThickness = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lstExcelLines = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbHRULines.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbHRULines
            // 
            this.gbHRULines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHRULines.Controls.Add(this.lblDescription);
            this.gbHRULines.Controls.Add(this.lblCodeJurisdiction);
            this.gbHRULines.Controls.Add(this.lblTemperatureValue);
            this.gbHRULines.Controls.Add(this.lblPressureValue);
            this.gbHRULines.Controls.Add(this.lblANSIClass);
            this.gbHRULines.Controls.Add(this.lblMaterial);
            this.gbHRULines.Controls.Add(this.lblPipeThickness);
            this.gbHRULines.Controls.Add(this.lblSize);
            this.gbHRULines.Controls.Add(this.label19);
            this.gbHRULines.Controls.Add(this.label18);
            this.gbHRULines.Controls.Add(this.label15);
            this.gbHRULines.Controls.Add(this.lstExcelLines);
            this.gbHRULines.Controls.Add(this.label3);
            this.gbHRULines.Controls.Add(this.label6);
            this.gbHRULines.Controls.Add(this.label8);
            this.gbHRULines.Controls.Add(this.label13);
            this.gbHRULines.Controls.Add(this.label12);
            this.gbHRULines.Location = new System.Drawing.Point(12, 12);
            this.gbHRULines.Name = "gbHRULines";
            this.gbHRULines.Size = new System.Drawing.Size(659, 253);
            this.gbHRULines.TabIndex = 1;
            this.gbHRULines.TabStop = false;
            this.gbHRULines.Text = "Excel Lines";
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.BackColor = System.Drawing.SystemColors.Window;
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescription.Location = new System.Drawing.Point(318, 29);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(332, 43);
            this.lblDescription.TabIndex = 96;
            this.lblDescription.Tag = "description";
            this.lblDescription.Text = "description";
            // 
            // lblCodeJurisdiction
            // 
            this.lblCodeJurisdiction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCodeJurisdiction.BackColor = System.Drawing.SystemColors.Window;
            this.lblCodeJurisdiction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodeJurisdiction.Location = new System.Drawing.Point(318, 224);
            this.lblCodeJurisdiction.Name = "lblCodeJurisdiction";
            this.lblCodeJurisdiction.Size = new System.Drawing.Size(223, 20);
            this.lblCodeJurisdiction.TabIndex = 93;
            this.lblCodeJurisdiction.Tag = "CodeJurisdiction";
            this.lblCodeJurisdiction.Text = "CodeJurisdiction";
            // 
            // lblTemperatureValue
            // 
            this.lblTemperatureValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTemperatureValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblTemperatureValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTemperatureValue.Location = new System.Drawing.Point(438, 138);
            this.lblTemperatureValue.Name = "lblTemperatureValue";
            this.lblTemperatureValue.Size = new System.Drawing.Size(103, 20);
            this.lblTemperatureValue.TabIndex = 90;
            this.lblTemperatureValue.Tag = "TemperatureValue";
            this.lblTemperatureValue.Text = "TemperatureValue";
            // 
            // lblPressureValue
            // 
            this.lblPressureValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPressureValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblPressureValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPressureValue.Location = new System.Drawing.Point(438, 95);
            this.lblPressureValue.Name = "lblPressureValue";
            this.lblPressureValue.Size = new System.Drawing.Size(103, 20);
            this.lblPressureValue.TabIndex = 87;
            this.lblPressureValue.Tag = "PressureValue";
            this.lblPressureValue.Text = "PressureValue";
            // 
            // lblANSIClass
            // 
            this.lblANSIClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblANSIClass.BackColor = System.Drawing.SystemColors.Window;
            this.lblANSIClass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblANSIClass.Location = new System.Drawing.Point(438, 181);
            this.lblANSIClass.Name = "lblANSIClass";
            this.lblANSIClass.Size = new System.Drawing.Size(100, 20);
            this.lblANSIClass.TabIndex = 86;
            this.lblANSIClass.Tag = "ansi_class";
            this.lblANSIClass.Text = "ansi_class";
            // 
            // lblMaterial
            // 
            this.lblMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaterial.BackColor = System.Drawing.SystemColors.Window;
            this.lblMaterial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMaterial.Location = new System.Drawing.Point(318, 181);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(103, 20);
            this.lblMaterial.TabIndex = 85;
            this.lblMaterial.Tag = "Material";
            this.lblMaterial.Text = "Material";
            // 
            // lblPipeThickness
            // 
            this.lblPipeThickness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPipeThickness.BackColor = System.Drawing.SystemColors.Window;
            this.lblPipeThickness.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPipeThickness.Location = new System.Drawing.Point(318, 138);
            this.lblPipeThickness.Name = "lblPipeThickness";
            this.lblPipeThickness.Size = new System.Drawing.Size(103, 20);
            this.lblPipeThickness.TabIndex = 82;
            this.lblPipeThickness.Tag = "pipethickness";
            this.lblPipeThickness.Text = "pipethickness";
            // 
            // lblSize
            // 
            this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSize.BackColor = System.Drawing.SystemColors.Window;
            this.lblSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSize.Location = new System.Drawing.Point(318, 95);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(100, 20);
            this.lblSize.TabIndex = 80;
            this.lblSize.Tag = "size";
            this.lblSize.Text = "size";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(438, 168);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 13);
            this.label19.TabIndex = 76;
            this.label19.Text = "ANSI Class";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(318, 211);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 13);
            this.label18.TabIndex = 71;
            this.label18.Text = "Code Jurisdiction";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(438, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 13);
            this.label15.TabIndex = 65;
            this.label15.Text = "Temperature Value";
            // 
            // lstExcelLines
            // 
            this.lstExcelLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstExcelLines.FormattingEnabled = true;
            this.lstExcelLines.Location = new System.Drawing.Point(6, 19);
            this.lstExcelLines.Name = "lstExcelLines";
            this.lstExcelLines.Size = new System.Drawing.Size(306, 225);
            this.lstExcelLines.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Description";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(318, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Size";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(318, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "PipeThickness";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(438, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 13);
            this.label13.TabIndex = 55;
            this.label13.Text = "Pressure Value";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(318, 168);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 54;
            this.label12.Text = "Material";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(18, 271);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Select";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(596, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmGetExcelRecord
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(684, 301);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbHRULines);
            this.MinimumSize = new System.Drawing.Size(700, 340);
            this.Name = "frmGetExcelRecord";
            this.Text = "Excel Line Details";
            this.Load += new System.EventHandler(this.frmGetExcelRecord_Load);
            this.gbHRULines.ResumeLayout(false);
            this.gbHRULines.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbHRULines;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox lstExcelLines;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCodeJurisdiction;
        private System.Windows.Forms.Label lblTemperatureValue;
        private System.Windows.Forms.Label lblPressureValue;
        private System.Windows.Forms.Label lblANSIClass;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.Label lblPipeThickness;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblDescription;
    }
}