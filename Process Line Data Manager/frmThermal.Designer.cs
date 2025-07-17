namespace Process_Line_Data_Manager
{
    partial class frmThermal
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvHRUData = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvP4DData = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvCombinedData = new System.Windows.Forms.DataGridView();
            this.lblCompare = new System.Windows.Forms.Label();
            this.lblP4DData = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnImportToP4D = new System.Windows.Forms.Button();
            this.lblHRUData = new System.Windows.Forms.Label();
            this.lblProject = new System.Windows.Forms.Label();
            this.btnMatchP4DtoHRU = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHRUData)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvP4DData)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCombinedData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 95);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(816, 344);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvHRUData);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(808, 318);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvHRUData
            // 
            this.dgvHRUData.AllowUserToAddRows = false;
            this.dgvHRUData.AllowUserToDeleteRows = false;
            this.dgvHRUData.AllowUserToResizeRows = false;
            this.dgvHRUData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHRUData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHRUData.Location = new System.Drawing.Point(3, 3);
            this.dgvHRUData.Name = "dgvHRUData";
            this.dgvHRUData.ReadOnly = true;
            this.dgvHRUData.RowHeadersWidth = 51;
            this.dgvHRUData.Size = new System.Drawing.Size(557, 309);
            this.dgvHRUData.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvP4DData);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(808, 318);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvP4DData
            // 
            this.dgvP4DData.AllowUserToAddRows = false;
            this.dgvP4DData.AllowUserToDeleteRows = false;
            this.dgvP4DData.AllowUserToResizeRows = false;
            this.dgvP4DData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvP4DData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvP4DData.Location = new System.Drawing.Point(6, 4);
            this.dgvP4DData.Name = "dgvP4DData";
            this.dgvP4DData.ReadOnly = true;
            this.dgvP4DData.RowHeadersWidth = 51;
            this.dgvP4DData.Size = new System.Drawing.Size(624, 292);
            this.dgvP4DData.TabIndex = 10;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvCombinedData);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(808, 318);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvCombinedData
            // 
            this.dgvCombinedData.AllowUserToAddRows = false;
            this.dgvCombinedData.AllowUserToDeleteRows = false;
            this.dgvCombinedData.AllowUserToResizeRows = false;
            this.dgvCombinedData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCombinedData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCombinedData.Location = new System.Drawing.Point(7, 7);
            this.dgvCombinedData.Name = "dgvCombinedData";
            this.dgvCombinedData.Size = new System.Drawing.Size(795, 305);
            this.dgvCombinedData.TabIndex = 0;
            this.dgvCombinedData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCombinedData_DataBindingComplete);
            // 
            // lblCompare
            // 
            this.lblCompare.AutoSize = true;
            this.lblCompare.Location = new System.Drawing.Point(139, 79);
            this.lblCompare.Name = "lblCompare";
            this.lblCompare.Size = new System.Drawing.Size(61, 13);
            this.lblCompare.TabIndex = 20;
            this.lblCompare.Text = "Differences";
            this.lblCompare.Visible = false;
            // 
            // lblP4DData
            // 
            this.lblP4DData.AutoSize = true;
            this.lblP4DData.Location = new System.Drawing.Point(79, 79);
            this.lblP4DData.Name = "lblP4DData";
            this.lblP4DData.Size = new System.Drawing.Size(54, 13);
            this.lblP4DData.TabIndex = 19;
            this.lblP4DData.Text = "P4D Data";
            this.lblP4DData.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(931, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnImportToP4D
            // 
            this.btnImportToP4D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportToP4D.Location = new System.Drawing.Point(12, 445);
            this.btnImportToP4D.Name = "btnImportToP4D";
            this.btnImportToP4D.Size = new System.Drawing.Size(112, 36);
            this.btnImportToP4D.TabIndex = 17;
            this.btnImportToP4D.Text = "Import to P4D";
            this.btnImportToP4D.UseVisualStyleBackColor = true;
            this.btnImportToP4D.Click += new System.EventHandler(this.btnImportToP4D_Click);
            // 
            // lblHRUData
            // 
            this.lblHRUData.AutoSize = true;
            this.lblHRUData.Location = new System.Drawing.Point(16, 79);
            this.lblHRUData.Name = "lblHRUData";
            this.lblHRUData.Size = new System.Drawing.Size(57, 13);
            this.lblHRUData.TabIndex = 16;
            this.lblHRUData.Text = "HRU Data";
            this.lblHRUData.Visible = false;
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(19, 13);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(46, 13);
            this.lblProject.TabIndex = 22;
            this.lblProject.Text = "Project: ";
            // 
            // btnMatchP4DtoHRU
            // 
            this.btnMatchP4DtoHRU.Location = new System.Drawing.Point(16, 40);
            this.btnMatchP4DtoHRU.Name = "btnMatchP4DtoHRU";
            this.btnMatchP4DtoHRU.Size = new System.Drawing.Size(112, 36);
            this.btnMatchP4DtoHRU.TabIndex = 23;
            this.btnMatchP4DtoHRU.Text = "Match P4D <--> HRU";
            this.btnMatchP4DtoHRU.UseVisualStyleBackColor = true;
            this.btnMatchP4DtoHRU.Click += new System.EventHandler(this.btnMatchP4DtoHRU_Click);
            // 
            // frmThermal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 506);
            this.Controls.Add(this.btnMatchP4DtoHRU);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblCompare);
            this.Controls.Add(this.lblP4DData);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnImportToP4D);
            this.Controls.Add(this.lblHRUData);
            this.Name = "frmThermal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Thermal Data";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmThermal_FormClosed);
            this.Load += new System.EventHandler(this.HRUForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHRUData)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvP4DData)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCombinedData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvHRUData;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvP4DData;
        private System.Windows.Forms.Label lblCompare;
        private System.Windows.Forms.Label lblP4DData;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnImportToP4D;
        private System.Windows.Forms.Label lblHRUData;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvCombinedData;
        private System.Windows.Forms.Button btnMatchP4DtoHRU;
    }
}