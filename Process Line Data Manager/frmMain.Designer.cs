namespace Process_Line_Data_Manager
{
    partial class frmMain
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
            this.cboServer = new System.Windows.Forms.ComboBox();
            this.lblServers = new System.Windows.Forms.Label();
            this.lblProjects = new System.Windows.Forms.Label();
            this.cboProject = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkNewHRUData = new System.Windows.Forms.CheckBox();
            this.chkIsComparable = new System.Windows.Forms.CheckBox();
            this.chkHRUData = new System.Windows.Forms.CheckBox();
            this.chkP4DData = new System.Windows.Forms.CheckBox();
            this.btnLoadProject = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnViewHRU = new System.Windows.Forms.Button();
            this.btnProcessLines = new System.Windows.Forms.Button();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboServer
            // 
            this.cboServer.BackColor = System.Drawing.SystemColors.Window;
            this.cboServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboServer.FormattingEnabled = true;
            this.cboServer.Location = new System.Drawing.Point(8, 23);
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size(133, 21);
            this.cboServer.TabIndex = 0;
            this.cboServer.SelectedIndexChanged += new System.EventHandler(this.cboServer_SelectedIndexChanged);
            // 
            // lblServers
            // 
            this.lblServers.AutoSize = true;
            this.lblServers.Location = new System.Drawing.Point(8, 7);
            this.lblServers.Name = "lblServers";
            this.lblServers.Size = new System.Drawing.Size(71, 13);
            this.lblServers.TabIndex = 1;
            this.lblServers.Text = "Select Server";
            // 
            // lblProjects
            // 
            this.lblProjects.AutoSize = true;
            this.lblProjects.Location = new System.Drawing.Point(8, 52);
            this.lblProjects.Name = "lblProjects";
            this.lblProjects.Size = new System.Drawing.Size(73, 13);
            this.lblProjects.TabIndex = 2;
            this.lblProjects.Text = "Select Project";
            // 
            // cboProject
            // 
            this.cboProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboProject.FormattingEnabled = true;
            this.cboProject.Location = new System.Drawing.Point(8, 68);
            this.cboProject.Name = "cboProject";
            this.cboProject.Size = new System.Drawing.Size(133, 21);
            this.cboProject.TabIndex = 3;
            this.cboProject.SelectedIndexChanged += new System.EventHandler(this.cboProject_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 163);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(473, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkNewHRUData);
            this.groupBox1.Controls.Add(this.chkIsComparable);
            this.groupBox1.Controls.Add(this.chkHRUData);
            this.groupBox1.Controls.Add(this.chkP4DData);
            this.groupBox1.Location = new System.Drawing.Point(306, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 102);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // chkNewHRUData
            // 
            this.chkNewHRUData.AutoSize = true;
            this.chkNewHRUData.Location = new System.Drawing.Point(8, 79);
            this.chkNewHRUData.Name = "chkNewHRUData";
            this.chkNewHRUData.Size = new System.Drawing.Size(105, 17);
            this.chkNewHRUData.TabIndex = 3;
            this.chkNewHRUData.Text = "NEW HRU Data";
            this.chkNewHRUData.UseVisualStyleBackColor = true;
            this.chkNewHRUData.CheckedChanged += new System.EventHandler(this.chkHRUData_CheckedChanged);
            // 
            // chkIsComparable
            // 
            this.chkIsComparable.AutoSize = true;
            this.chkIsComparable.Location = new System.Drawing.Point(8, 57);
            this.chkIsComparable.Name = "chkIsComparable";
            this.chkIsComparable.Size = new System.Drawing.Size(131, 17);
            this.chkIsComparable.TabIndex = 2;
            this.chkIsComparable.Text = "HRU/P4D Compatible";
            this.chkIsComparable.UseVisualStyleBackColor = true;
            this.chkIsComparable.CheckedChanged += new System.EventHandler(this.chkHRUData_CheckedChanged);
            // 
            // chkHRUData
            // 
            this.chkHRUData.AutoSize = true;
            this.chkHRUData.Location = new System.Drawing.Point(8, 15);
            this.chkHRUData.Name = "chkHRUData";
            this.chkHRUData.Size = new System.Drawing.Size(94, 17);
            this.chkHRUData.TabIndex = 1;
            this.chkHRUData.Text = "HRU has data";
            this.chkHRUData.UseVisualStyleBackColor = true;
            this.chkHRUData.CheckedChanged += new System.EventHandler(this.chkHRUData_CheckedChanged);
            // 
            // chkP4DData
            // 
            this.chkP4DData.AutoSize = true;
            this.chkP4DData.Location = new System.Drawing.Point(8, 36);
            this.chkP4DData.Name = "chkP4DData";
            this.chkP4DData.Size = new System.Drawing.Size(91, 17);
            this.chkP4DData.TabIndex = 0;
            this.chkP4DData.Text = "P4D has data";
            this.chkP4DData.UseVisualStyleBackColor = true;
            // 
            // btnLoadProject
            // 
            this.btnLoadProject.Location = new System.Drawing.Point(167, 14);
            this.btnLoadProject.Name = "btnLoadProject";
            this.btnLoadProject.Size = new System.Drawing.Size(133, 31);
            this.btnLoadProject.TabIndex = 14;
            this.btnLoadProject.Text = "Load Project";
            this.btnLoadProject.UseVisualStyleBackColor = true;
            this.btnLoadProject.Click += new System.EventHandler(this.btnLoadProject_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblServers);
            this.panel1.Controls.Add(this.cboServer);
            this.panel1.Controls.Add(this.btnLoadProject);
            this.panel1.Controls.Add(this.lblProjects);
            this.panel1.Controls.Add(this.cboProject);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 114);
            this.panel1.TabIndex = 16;
            // 
            // btnViewHRU
            // 
            this.btnViewHRU.Location = new System.Drawing.Point(171, 124);
            this.btnViewHRU.Name = "btnViewHRU";
            this.btnViewHRU.Size = new System.Drawing.Size(133, 31);
            this.btnViewHRU.TabIndex = 15;
            this.btnViewHRU.Text = "Import Thermal Data";
            this.btnViewHRU.UseVisualStyleBackColor = true;
            this.btnViewHRU.Click += new System.EventHandler(this.btnViewHRU_Click);
            // 
            // btnProcessLines
            // 
            this.btnProcessLines.Location = new System.Drawing.Point(331, 124);
            this.btnProcessLines.Name = "btnProcessLines";
            this.btnProcessLines.Size = new System.Drawing.Size(133, 31);
            this.btnProcessLines.TabIndex = 17;
            this.btnProcessLines.Text = "Pipe Lines";
            this.btnProcessLines.UseVisualStyleBackColor = true;
            this.btnProcessLines.Click += new System.EventHandler(this.BtnProcessLines_Click);
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.Location = new System.Drawing.Point(12, 124);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(133, 31);
            this.btnImportFromExcel.TabIndex = 18;
            this.btnImportFromExcel.Text = "Import Excel Data";
            this.btnImportFromExcel.UseVisualStyleBackColor = true;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 185);
            this.Controls.Add(this.btnImportFromExcel);
            this.Controls.Add(this.btnProcessLines);
            this.Controls.Add(this.btnViewHRU);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Line Data Manager (LDM)";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboServer;
        private System.Windows.Forms.Label lblServers;
        private System.Windows.Forms.Label lblProjects;
        private System.Windows.Forms.ComboBox cboProject;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkHRUData;
        private System.Windows.Forms.CheckBox chkP4DData;
        private System.Windows.Forms.CheckBox chkIsComparable;
        private System.Windows.Forms.Button btnLoadProject;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkNewHRUData;
        private System.Windows.Forms.Button btnViewHRU;
        private System.Windows.Forms.Button btnProcessLines;
        private System.Windows.Forms.Button btnImportFromExcel;
    }
}

