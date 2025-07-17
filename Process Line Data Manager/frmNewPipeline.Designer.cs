namespace Process_Line_Data_Manager
{
    partial class frmNewPipeline
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtMainLineDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewPipeLine = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtThermalDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetThermal = new System.Windows.Forms.Button();
            this.btnGetExcel = new System.Windows.Forms.Button();
            this.lblHRU_line_no = new System.Windows.Forms.Label();
            this.lblExcel_ID = new System.Windows.Forms.Label();
            this.txtSecondaryLineDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Pipeline Name";
            // 
            // txtMainLineDescription
            // 
            this.txtMainLineDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtMainLineDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMainLineDescription.Location = new System.Drawing.Point(12, 179);
            this.txtMainLineDescription.Name = "txtMainLineDescription";
            this.txtMainLineDescription.Size = new System.Drawing.Size(436, 20);
            this.txtMainLineDescription.TabIndex = 3;
            this.txtMainLineDescription.Tag = "MainLineDescription";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Main Line Description";
            // 
            // txtNewPipeLine
            // 
            this.txtNewPipeLine.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNewPipeLine.Location = new System.Drawing.Point(12, 19);
            this.txtNewPipeLine.Name = "txtNewPipeLine";
            this.txtNewPipeLine.Size = new System.Drawing.Size(85, 20);
            this.txtNewPipeLine.TabIndex = 0;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAccept.Location = new System.Drawing.Point(12, 270);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(368, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtThermalDescription
            // 
            this.txtThermalDescription.Location = new System.Drawing.Point(12, 60);
            this.txtThermalDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtThermalDescription.Name = "txtThermalDescription";
            this.txtThermalDescription.ReadOnly = true;
            this.txtThermalDescription.Size = new System.Drawing.Size(436, 20);
            this.txtThermalDescription.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "Seed Line Description";
            // 
            // btnGetThermal
            // 
            this.btnGetThermal.Location = new System.Drawing.Point(12, 85);
            this.btnGetThermal.Name = "btnGetThermal";
            this.btnGetThermal.Size = new System.Drawing.Size(75, 23);
            this.btnGetThermal.TabIndex = 1;
            this.btnGetThermal.Text = "Get Thermal";
            this.btnGetThermal.UseVisualStyleBackColor = true;
            this.btnGetThermal.Click += new System.EventHandler(this.btnGetThermal_Click);
            // 
            // btnGetExcel
            // 
            this.btnGetExcel.Location = new System.Drawing.Point(368, 85);
            this.btnGetExcel.Name = "btnGetExcel";
            this.btnGetExcel.Size = new System.Drawing.Size(75, 23);
            this.btnGetExcel.TabIndex = 2;
            this.btnGetExcel.Text = "Get Excel";
            this.btnGetExcel.UseVisualStyleBackColor = true;
            this.btnGetExcel.Click += new System.EventHandler(this.btnGetExcel_Click);
            // 
            // lblHRU_line_no
            // 
            this.lblHRU_line_no.Location = new System.Drawing.Point(12, 111);
            this.lblHRU_line_no.Name = "lblHRU_line_no";
            this.lblHRU_line_no.Size = new System.Drawing.Size(100, 23);
            this.lblHRU_line_no.TabIndex = 84;
            this.lblHRU_line_no.Text = "lblHRU_line_no";
            // 
            // lblExcel_ID
            // 
            this.lblExcel_ID.Location = new System.Drawing.Point(362, 111);
            this.lblExcel_ID.Name = "lblExcel_ID";
            this.lblExcel_ID.Size = new System.Drawing.Size(81, 13);
            this.lblExcel_ID.TabIndex = 85;
            this.lblExcel_ID.Text = "lblExcel_ID";
            this.lblExcel_ID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSecondaryLineDescription
            // 
            this.txtSecondaryLineDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtSecondaryLineDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSecondaryLineDescription.Location = new System.Drawing.Point(12, 228);
            this.txtSecondaryLineDescription.Name = "txtSecondaryLineDescription";
            this.txtSecondaryLineDescription.Size = new System.Drawing.Size(436, 20);
            this.txtSecondaryLineDescription.TabIndex = 4;
            this.txtSecondaryLineDescription.Tag = "SecondaryLineDescription";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Secondary Line Description";
            // 
            // frmNewPipeline
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(455, 303);
            this.Controls.Add(this.txtSecondaryLineDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblExcel_ID);
            this.Controls.Add(this.lblHRU_line_no);
            this.Controls.Add(this.btnGetExcel);
            this.Controls.Add(this.btnGetThermal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtThermalDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMainLineDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNewPipeLine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewPipeline";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Pipeline";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMainLineDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewPipeLine;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtThermalDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGetThermal;
        private System.Windows.Forms.Button btnGetExcel;
        private System.Windows.Forms.Label lblHRU_line_no;
        private System.Windows.Forms.Label lblExcel_ID;
        private System.Windows.Forms.TextBox txtSecondaryLineDescription;
        private System.Windows.Forms.Label label4;
    }
}