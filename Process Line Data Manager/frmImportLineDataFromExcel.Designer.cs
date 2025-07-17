namespace Process_Line_Data_Manager
{
    partial class frmImportLineDataFromExcel
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
            this.lblProject = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.btnBrowseToFile = new System.Windows.Forms.Button();
            this.dgvImporting = new System.Windows.Forms.DataGridView();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.dgvExisting = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NE_ExcelDataCountLabel = new System.Windows.Forms.Label();
            this.ExcelDataCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImporting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExisting)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(12, 9);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(80, 13);
            this.lblProject.TabIndex = 0;
            this.lblProject.Text = "Current Project:";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 34);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(71, 13);
            this.lblFile.TabIndex = 2;
            this.lblFile.Text = "Selected File:";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSelectedFile.Location = new System.Drawing.Point(84, 33);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(199, 18);
            this.lblSelectedFile.TabIndex = 3;
            this.lblSelectedFile.Text = "Select a file...-->";
            // 
            // btnBrowseToFile
            // 
            this.btnBrowseToFile.Location = new System.Drawing.Point(289, 29);
            this.btnBrowseToFile.Name = "btnBrowseToFile";
            this.btnBrowseToFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseToFile.TabIndex = 4;
            this.btnBrowseToFile.Text = "Browse...";
            this.btnBrowseToFile.UseVisualStyleBackColor = true;
            this.btnBrowseToFile.Click += new System.EventHandler(this.btnBrowseToFile_Click);
            // 
            // dgvImporting
            // 
            this.dgvImporting.AllowUserToAddRows = false;
            this.dgvImporting.AllowUserToDeleteRows = false;
            this.dgvImporting.AllowUserToResizeRows = false;
            this.dgvImporting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvImporting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImporting.Location = new System.Drawing.Point(15, 250);
            this.dgvImporting.Name = "dgvImporting";
            this.dgvImporting.ReadOnly = true;
            this.dgvImporting.RowHeadersWidth = 51;
            this.dgvImporting.Size = new System.Drawing.Size(432, 136);
            this.dgvImporting.TabIndex = 9;
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportExcel.Enabled = false;
            this.btnImportExcel.Location = new System.Drawing.Point(16, 411);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnImportExcel.TabIndex = 10;
            this.btnImportExcel.Text = "Import";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // dgvExisting
            // 
            this.dgvExisting.AllowUserToAddRows = false;
            this.dgvExisting.AllowUserToDeleteRows = false;
            this.dgvExisting.AllowUserToResizeRows = false;
            this.dgvExisting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExisting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExisting.Location = new System.Drawing.Point(15, 78);
            this.dgvExisting.Margin = new System.Windows.Forms.Padding(2);
            this.dgvExisting.Name = "dgvExisting";
            this.dgvExisting.ReadOnly = true;
            this.dgvExisting.RowHeadersWidth = 51;
            this.dgvExisting.RowTemplate.Height = 24;
            this.dgvExisting.Size = new System.Drawing.Size(432, 122);
            this.dgvExisting.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 233);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Data to Import:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Existing Excel data in Project:";
            // 
            // NE_ExcelDataCountLabel
            // 
            this.NE_ExcelDataCountLabel.AutoSize = true;
            this.NE_ExcelDataCountLabel.Location = new System.Drawing.Point(164, 62);
            this.NE_ExcelDataCountLabel.Name = "NE_ExcelDataCountLabel";
            this.NE_ExcelDataCountLabel.Size = new System.Drawing.Size(35, 13);
            this.NE_ExcelDataCountLabel.TabIndex = 14;
            this.NE_ExcelDataCountLabel.Text = "label3";
            // 
            // ExcelDataCountLabel
            // 
            this.ExcelDataCountLabel.AutoSize = true;
            this.ExcelDataCountLabel.Location = new System.Drawing.Point(95, 233);
            this.ExcelDataCountLabel.Name = "ExcelDataCountLabel";
            this.ExcelDataCountLabel.Size = new System.Drawing.Size(35, 13);
            this.ExcelDataCountLabel.TabIndex = 15;
            this.ExcelDataCountLabel.Text = "label4";
            // 
            // frmImportLineDataFromExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 461);
            this.Controls.Add(this.ExcelDataCountLabel);
            this.Controls.Add(this.NE_ExcelDataCountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvExisting);
            this.Controls.Add(this.btnImportExcel);
            this.Controls.Add(this.dgvImporting);
            this.Controls.Add(this.btnBrowseToFile);
            this.Controls.Add(this.lblSelectedFile);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblProject);
            this.MinimumSize = new System.Drawing.Size(475, 500);
            this.Name = "frmImportLineDataFromExcel";
            this.Text = "Import Line Data from Excel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmImportLineDataFromExcel_FormClosed);
            this.Load += new System.EventHandler(this.frmImportLineDataFromExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImporting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExisting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.Button btnBrowseToFile;
        private System.Windows.Forms.DataGridView dgvImporting;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.DataGridView dgvExisting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label NE_ExcelDataCountLabel;
        private System.Windows.Forms.Label ExcelDataCountLabel;
    }
}