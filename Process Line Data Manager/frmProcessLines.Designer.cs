namespace Process_Line_Data_Manager
{
    partial class frmProcessLines
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
            this.btnCreateNewPipeLine = new System.Windows.Forms.Button();
            this.lstPipeLines = new System.Windows.Forms.ListBox();
            this.lstSegments = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gbSegment = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMaxOperatingTemp = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtTemperatureCaseTemperature = new System.Windows.Forms.TextBox();
            this.txtTemperatureCasePressure = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPressureCaseTemperature = new System.Windows.Forms.TextBox();
            this.txtPressureCasePressure = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtThicknessCaseTemperature = new System.Windows.Forms.TextBox();
            this.txtThicknessCasePressure = new System.Windows.Forms.TextBox();
            this.chkIsTubing = new System.Windows.Forms.CheckBox();
            this.btnClearExcel = new System.Windows.Forms.Button();
            this.btnClearThermal = new System.Windows.Forms.Button();
            this.btnViewExcelRecord = new System.Windows.Forms.Button();
            this.btnGetExcelRecord = new System.Windows.Forms.Button();
            this.txtExcelParentID = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cboWallThickness = new System.Windows.Forms.ComboBox();
            this.cboDiameter = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnViewThermalRecord = new System.Windows.Forms.Button();
            this.cboCodeJurisdiction = new System.Windows.Forms.ComboBox();
            this.cboANSIClass = new System.Windows.Forms.ComboBox();
            this.cboPipeMaterial = new System.Windows.Forms.ComboBox();
            this.btnGetHRURecord = new System.Windows.Forms.Button();
            this.txtHRUParentID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancelSegmentChanges = new System.Windows.Forms.Button();
            this.btnSaveSegmentChanges = new System.Windows.Forms.Button();
            this.txtCorrosionAllowance = new System.Windows.Forms.TextBox();
            this.txtInsulationThickness = new System.Windows.Forms.TextBox();
            this.txtTemperatureUnits = new System.Windows.Forms.TextBox();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.txtPressureUnits = new System.Windows.Forms.TextBox();
            this.txtPressure = new System.Windows.Forms.TextBox();
            this.txtSecondaryLineDescription = new System.Windows.Forms.TextBox();
            this.btnDeleteSegment = new System.Windows.Forms.Button();
            this.btnEditSegment = new System.Windows.Forms.Button();
            this.btnNewSegment = new System.Windows.Forms.Button();
            this.txtMainLineDescription = new System.Windows.Forms.TextBox();
            this.gbPipeLines = new System.Windows.Forms.GroupBox();
            this.btnCancelPipeChanges = new System.Windows.Forms.Button();
            this.btnSavePipeChanges = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEditPipeLine = new System.Windows.Forms.Button();
            this.btnDeletePipeline = new System.Windows.Forms.Button();
            this.txtPipeLineName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbSegment.SuspendLayout();
            this.gbPipeLines.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateNewPipeLine
            // 
            this.btnCreateNewPipeLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateNewPipeLine.Location = new System.Drawing.Point(22, 464);
            this.btnCreateNewPipeLine.Name = "btnCreateNewPipeLine";
            this.btnCreateNewPipeLine.Size = new System.Drawing.Size(95, 23);
            this.btnCreateNewPipeLine.TabIndex = 3;
            this.btnCreateNewPipeLine.Text = "New";
            this.toolTip1.SetToolTip(this.btnCreateNewPipeLine, "Create a new Pipeline");
            this.btnCreateNewPipeLine.UseVisualStyleBackColor = true;
            this.btnCreateNewPipeLine.Click += new System.EventHandler(this.BtnCreateNewPipeLine_Click);
            // 
            // lstPipeLines
            // 
            this.lstPipeLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstPipeLines.FormattingEnabled = true;
            this.lstPipeLines.Location = new System.Drawing.Point(12, 19);
            this.lstPipeLines.Name = "lstPipeLines";
            this.lstPipeLines.Size = new System.Drawing.Size(114, 420);
            this.lstPipeLines.TabIndex = 0;
            this.lstPipeLines.SelectedIndexChanged += new System.EventHandler(this.LstPipeLines_SelectedIndexChanged);
            // 
            // lstSegments
            // 
            this.lstSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstSegments.FormattingEnabled = true;
            this.lstSegments.Location = new System.Drawing.Point(13, 39);
            this.lstSegments.Name = "lstSegments";
            this.lstSegments.Size = new System.Drawing.Size(120, 264);
            this.lstSegments.TabIndex = 0;
            this.toolTip1.SetToolTip(this.lstSegments, "Segments belonging to the selected Pipeline - A Pipeline has at least one Segment" +
        "");
            this.lstSegments.SelectedIndexChanged += new System.EventHandler(this.lstSegments_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Main Line Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Secondary Line Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(149, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Diameter";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(149, 239);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 13);
            this.label15.TabIndex = 57;
            this.label15.Text = "Corrosion Allowance";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(148, 193);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 13);
            this.label14.TabIndex = 56;
            this.label14.Text = "Insulation Thickness";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(311, 236);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 55;
            this.label13.Text = "Units";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(451, 191);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 54;
            this.label12.Text = "Temperature";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(345, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "Pressure";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(361, 147);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "ANSI Class";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(149, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Code Jurisdiction";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(361, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "Pipe material";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Wall Thickness";
            // 
            // gbSegment
            // 
            this.gbSegment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSegment.BackColor = System.Drawing.SystemColors.Control;
            this.gbSegment.Controls.Add(this.label11);
            this.gbSegment.Controls.Add(this.txtMaxOperatingTemp);
            this.gbSegment.Controls.Add(this.label20);
            this.gbSegment.Controls.Add(this.txtTemperatureCaseTemperature);
            this.gbSegment.Controls.Add(this.txtTemperatureCasePressure);
            this.gbSegment.Controls.Add(this.label19);
            this.gbSegment.Controls.Add(this.txtPressureCaseTemperature);
            this.gbSegment.Controls.Add(this.txtPressureCasePressure);
            this.gbSegment.Controls.Add(this.label18);
            this.gbSegment.Controls.Add(this.txtThicknessCaseTemperature);
            this.gbSegment.Controls.Add(this.txtThicknessCasePressure);
            this.gbSegment.Controls.Add(this.chkIsTubing);
            this.gbSegment.Controls.Add(this.btnClearExcel);
            this.gbSegment.Controls.Add(this.btnClearThermal);
            this.gbSegment.Controls.Add(this.btnViewExcelRecord);
            this.gbSegment.Controls.Add(this.btnGetExcelRecord);
            this.gbSegment.Controls.Add(this.txtExcelParentID);
            this.gbSegment.Controls.Add(this.label17);
            this.gbSegment.Controls.Add(this.cboWallThickness);
            this.gbSegment.Controls.Add(this.cboDiameter);
            this.gbSegment.Controls.Add(this.label16);
            this.gbSegment.Controls.Add(this.btnViewThermalRecord);
            this.gbSegment.Controls.Add(this.cboCodeJurisdiction);
            this.gbSegment.Controls.Add(this.cboANSIClass);
            this.gbSegment.Controls.Add(this.cboPipeMaterial);
            this.gbSegment.Controls.Add(this.btnGetHRURecord);
            this.gbSegment.Controls.Add(this.txtHRUParentID);
            this.gbSegment.Controls.Add(this.label4);
            this.gbSegment.Controls.Add(this.btnCancelSegmentChanges);
            this.gbSegment.Controls.Add(this.btnSaveSegmentChanges);
            this.gbSegment.Controls.Add(this.txtCorrosionAllowance);
            this.gbSegment.Controls.Add(this.txtInsulationThickness);
            this.gbSegment.Controls.Add(this.txtTemperatureUnits);
            this.gbSegment.Controls.Add(this.txtTemperature);
            this.gbSegment.Controls.Add(this.txtPressureUnits);
            this.gbSegment.Controls.Add(this.txtPressure);
            this.gbSegment.Controls.Add(this.btnDeleteSegment);
            this.gbSegment.Controls.Add(this.btnEditSegment);
            this.gbSegment.Controls.Add(this.btnNewSegment);
            this.gbSegment.Controls.Add(this.lstSegments);
            this.gbSegment.Controls.Add(this.label5);
            this.gbSegment.Controls.Add(this.label6);
            this.gbSegment.Controls.Add(this.label7);
            this.gbSegment.Controls.Add(this.label8);
            this.gbSegment.Controls.Add(this.label15);
            this.gbSegment.Controls.Add(this.label9);
            this.gbSegment.Controls.Add(this.label14);
            this.gbSegment.Controls.Add(this.label10);
            this.gbSegment.Controls.Add(this.label13);
            this.gbSegment.Controls.Add(this.label12);
            this.gbSegment.Location = new System.Drawing.Point(138, 135);
            this.gbSegment.Name = "gbSegment";
            this.gbSegment.Size = new System.Drawing.Size(573, 411);
            this.gbSegment.TabIndex = 0;
            this.gbSegment.TabStop = false;
            this.gbSegment.Text = "Segments";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(266, 340);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 89;
            this.label11.Text = "Max Operating";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaxOperatingTemp
            // 
            this.txtMaxOperatingTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxOperatingTemp.Location = new System.Drawing.Point(454, 337);
            this.txtMaxOperatingTemp.Name = "txtMaxOperatingTemp";
            this.txtMaxOperatingTemp.Size = new System.Drawing.Size(100, 20);
            this.txtMaxOperatingTemp.TabIndex = 88;
            this.txtMaxOperatingTemp.Tag = "maxOperatingTemperatureDisplay";
            this.txtMaxOperatingTemp.Text = "maxOperatingTemperatureDisplay";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(258, 314);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 13);
            this.label20.TabIndex = 86;
            this.label20.Text = "Max Temp Case";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTemperatureCaseTemperature
            // 
            this.txtTemperatureCaseTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemperatureCaseTemperature.Location = new System.Drawing.Point(454, 311);
            this.txtTemperatureCaseTemperature.Name = "txtTemperatureCaseTemperature";
            this.txtTemperatureCaseTemperature.Size = new System.Drawing.Size(100, 20);
            this.txtTemperatureCaseTemperature.TabIndex = 85;
            this.txtTemperatureCaseTemperature.Tag = "temperatureCaseTemperatureDisplay";
            this.txtTemperatureCaseTemperature.Text = "temperatureCaseTemperatureDisplay";
            // 
            // txtTemperatureCasePressure
            // 
            this.txtTemperatureCasePressure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemperatureCasePressure.Location = new System.Drawing.Point(348, 311);
            this.txtTemperatureCasePressure.Name = "txtTemperatureCasePressure";
            this.txtTemperatureCasePressure.Size = new System.Drawing.Size(100, 20);
            this.txtTemperatureCasePressure.TabIndex = 84;
            this.txtTemperatureCasePressure.Tag = "temperatureCasePressureDisplay";
            this.txtTemperatureCasePressure.Text = "temperatureCasePressureDisplay";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(267, 288);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(78, 13);
            this.label19.TabIndex = 83;
            this.label19.Text = "Max Pres Case";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPressureCaseTemperature
            // 
            this.txtPressureCaseTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPressureCaseTemperature.Location = new System.Drawing.Point(454, 285);
            this.txtPressureCaseTemperature.Name = "txtPressureCaseTemperature";
            this.txtPressureCaseTemperature.Size = new System.Drawing.Size(100, 20);
            this.txtPressureCaseTemperature.TabIndex = 82;
            this.txtPressureCaseTemperature.Tag = "pressureCaseTemperatureDisplay";
            this.txtPressureCaseTemperature.Text = "pressureCaseTemperatureDisplay";
            // 
            // txtPressureCasePressure
            // 
            this.txtPressureCasePressure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPressureCasePressure.Location = new System.Drawing.Point(348, 285);
            this.txtPressureCasePressure.Name = "txtPressureCasePressure";
            this.txtPressureCasePressure.Size = new System.Drawing.Size(100, 20);
            this.txtPressureCasePressure.TabIndex = 81;
            this.txtPressureCasePressure.Tag = "pressureCasePressureDisplay";
            this.txtPressureCasePressure.Text = "pressureCasePressureDisplay";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(259, 262);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(84, 13);
            this.label18.TabIndex = 80;
            this.label18.Text = "Max Thick Case";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label18, "Use these fields for values when MDCs are not used. If no value is provided by Th" +
        "ermal, Pressure and Temperture will be automatically copied to here.");
            // 
            // txtThicknessCaseTemperature
            // 
            this.txtThicknessCaseTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThicknessCaseTemperature.Location = new System.Drawing.Point(454, 259);
            this.txtThicknessCaseTemperature.Name = "txtThicknessCaseTemperature";
            this.txtThicknessCaseTemperature.Size = new System.Drawing.Size(100, 20);
            this.txtThicknessCaseTemperature.TabIndex = 79;
            this.txtThicknessCaseTemperature.Tag = "thicknessCaseTemperatureDisplay";
            this.txtThicknessCaseTemperature.Text = "thicknessCaseTemperatureDisplay";
            // 
            // txtThicknessCasePressure
            // 
            this.txtThicknessCasePressure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtThicknessCasePressure.Location = new System.Drawing.Point(348, 259);
            this.txtThicknessCasePressure.Name = "txtThicknessCasePressure";
            this.txtThicknessCasePressure.Size = new System.Drawing.Size(100, 20);
            this.txtThicknessCasePressure.TabIndex = 78;
            this.txtThicknessCasePressure.Tag = "thicknessCasePressureDisplay";
            this.txtThicknessCasePressure.Text = "thicknessCasePressureDisplay";
            // 
            // chkIsTubing
            // 
            this.chkIsTubing.AutoSize = true;
            this.chkIsTubing.Enabled = false;
            this.chkIsTubing.Location = new System.Drawing.Point(472, 118);
            this.chkIsTubing.Name = "chkIsTubing";
            this.chkIsTubing.Size = new System.Drawing.Size(70, 17);
            this.chkIsTubing.TabIndex = 77;
            this.chkIsTubing.Tag = "IsTubing";
            this.chkIsTubing.Text = "Is Tubing";
            this.toolTip1.SetToolTip(this.chkIsTubing, "Check box if segment represents tubing; uncheck if segment represents piping.");
            this.chkIsTubing.UseVisualStyleBackColor = true;
            // 
            // btnClearExcel
            // 
            this.btnClearExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearExcel.Location = new System.Drawing.Point(445, 68);
            this.btnClearExcel.Name = "btnClearExcel";
            this.btnClearExcel.Size = new System.Drawing.Size(104, 23);
            this.btnClearExcel.TabIndex = 76;
            this.btnClearExcel.Text = "Clear Excel";
            this.btnClearExcel.UseVisualStyleBackColor = true;
            this.btnClearExcel.Click += new System.EventHandler(this.btnClearExcel_Click);
            // 
            // btnClearThermal
            // 
            this.btnClearThermal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearThermal.Location = new System.Drawing.Point(445, 31);
            this.btnClearThermal.Name = "btnClearThermal";
            this.btnClearThermal.Size = new System.Drawing.Size(104, 23);
            this.btnClearThermal.TabIndex = 75;
            this.btnClearThermal.Text = "Clear Thermal";
            this.btnClearThermal.UseVisualStyleBackColor = true;
            this.btnClearThermal.Click += new System.EventHandler(this.btnClearThermal_Click);
            // 
            // btnViewExcelRecord
            // 
            this.btnViewExcelRecord.Location = new System.Drawing.Point(335, 68);
            this.btnViewExcelRecord.Name = "btnViewExcelRecord";
            this.btnViewExcelRecord.Size = new System.Drawing.Size(104, 23);
            this.btnViewExcelRecord.TabIndex = 74;
            this.btnViewExcelRecord.Text = "View Excel Line";
            this.toolTip1.SetToolTip(this.btnViewExcelRecord, "Click to view info related to the selected Thermal line");
            this.btnViewExcelRecord.UseVisualStyleBackColor = true;
            this.btnViewExcelRecord.Click += new System.EventHandler(this.btnViewExcelRecord_Click);
            // 
            // btnGetExcelRecord
            // 
            this.btnGetExcelRecord.Location = new System.Drawing.Point(230, 69);
            this.btnGetExcelRecord.Name = "btnGetExcelRecord";
            this.btnGetExcelRecord.Size = new System.Drawing.Size(99, 23);
            this.btnGetExcelRecord.TabIndex = 73;
            this.btnGetExcelRecord.Text = "Get Excel Line";
            this.toolTip1.SetToolTip(this.btnGetExcelRecord, "Click to select a Thermal line to assign to this segment");
            this.btnGetExcelRecord.UseVisualStyleBackColor = true;
            this.btnGetExcelRecord.Click += new System.EventHandler(this.btnGetExcelRecord_Click);
            // 
            // txtExcelParentID
            // 
            this.txtExcelParentID.Enabled = false;
            this.txtExcelParentID.Location = new System.Drawing.Point(152, 71);
            this.txtExcelParentID.Name = "txtExcelParentID";
            this.txtExcelParentID.Size = new System.Drawing.Size(62, 20);
            this.txtExcelParentID.TabIndex = 72;
            this.txtExcelParentID.Tag = "ExcelParentID";
            this.txtExcelParentID.TextChanged += new System.EventHandler(this.txtExcelParentID_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(149, 55);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 71;
            this.label17.Text = "Excel Parent";
            this.toolTip1.SetToolTip(this.label17, "Thermal Line ID that this Segment gets its data from");
            // 
            // cboWallThickness
            // 
            this.cboWallThickness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWallThickness.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboWallThickness.FormattingEnabled = true;
            this.cboWallThickness.Location = new System.Drawing.Point(258, 116);
            this.cboWallThickness.Name = "cboWallThickness";
            this.cboWallThickness.Size = new System.Drawing.Size(101, 21);
            this.cboWallThickness.TabIndex = 70;
            this.cboWallThickness.Tag = "WallThickness";
            // 
            // cboDiameter
            // 
            this.cboDiameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiameter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboDiameter.FormattingEnabled = true;
            this.cboDiameter.Location = new System.Drawing.Point(152, 116);
            this.cboDiameter.Name = "cboDiameter";
            this.cboDiameter.Size = new System.Drawing.Size(101, 21);
            this.cboDiameter.TabIndex = 69;
            this.cboDiameter.Tag = "Diameter";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 13);
            this.label16.TabIndex = 68;
            this.label16.Text = "Segment ID";
            this.toolTip1.SetToolTip(this.label16, "Thermal Line ID that this Segment gets its data from");
            // 
            // btnViewThermalRecord
            // 
            this.btnViewThermalRecord.Location = new System.Drawing.Point(335, 31);
            this.btnViewThermalRecord.Name = "btnViewThermalRecord";
            this.btnViewThermalRecord.Size = new System.Drawing.Size(104, 23);
            this.btnViewThermalRecord.TabIndex = 67;
            this.btnViewThermalRecord.Text = "View Thermal Line";
            this.toolTip1.SetToolTip(this.btnViewThermalRecord, "Click to view info related to the selected Thermal line");
            this.btnViewThermalRecord.UseVisualStyleBackColor = true;
            this.btnViewThermalRecord.Click += new System.EventHandler(this.btnViewThermalRecord_Click);
            // 
            // cboCodeJurisdiction
            // 
            this.cboCodeJurisdiction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCodeJurisdiction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboCodeJurisdiction.FormattingEnabled = true;
            this.cboCodeJurisdiction.Location = new System.Drawing.Point(152, 163);
            this.cboCodeJurisdiction.Name = "cboCodeJurisdiction";
            this.cboCodeJurisdiction.Size = new System.Drawing.Size(205, 21);
            this.cboCodeJurisdiction.TabIndex = 66;
            this.cboCodeJurisdiction.Tag = "CodeJurisdiction";
            // 
            // cboANSIClass
            // 
            this.cboANSIClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboANSIClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboANSIClass.FormattingEnabled = true;
            this.cboANSIClass.Location = new System.Drawing.Point(363, 163);
            this.cboANSIClass.Name = "cboANSIClass";
            this.cboANSIClass.Size = new System.Drawing.Size(102, 21);
            this.cboANSIClass.TabIndex = 65;
            this.cboANSIClass.Tag = "ANSIClass";
            // 
            // cboPipeMaterial
            // 
            this.cboPipeMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPipeMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboPipeMaterial.FormattingEnabled = true;
            this.cboPipeMaterial.Location = new System.Drawing.Point(364, 116);
            this.cboPipeMaterial.Name = "cboPipeMaterial";
            this.cboPipeMaterial.Size = new System.Drawing.Size(101, 21);
            this.cboPipeMaterial.TabIndex = 64;
            this.cboPipeMaterial.Tag = "PipeMaterial";
            // 
            // btnGetHRURecord
            // 
            this.btnGetHRURecord.Location = new System.Drawing.Point(230, 31);
            this.btnGetHRURecord.Name = "btnGetHRURecord";
            this.btnGetHRURecord.Size = new System.Drawing.Size(99, 23);
            this.btnGetHRURecord.TabIndex = 61;
            this.btnGetHRURecord.Text = "Get Thermal Line";
            this.toolTip1.SetToolTip(this.btnGetHRURecord, "Click to select a Thermal line to assign to this segment");
            this.btnGetHRURecord.UseVisualStyleBackColor = true;
            this.btnGetHRURecord.Click += new System.EventHandler(this.btnGetHRURecord_Click);
            // 
            // txtHRUParentID
            // 
            this.txtHRUParentID.Enabled = false;
            this.txtHRUParentID.Location = new System.Drawing.Point(152, 33);
            this.txtHRUParentID.Name = "txtHRUParentID";
            this.txtHRUParentID.Size = new System.Drawing.Size(62, 20);
            this.txtHRUParentID.TabIndex = 60;
            this.txtHRUParentID.Tag = "HRUParentID";
            this.txtHRUParentID.TextChanged += new System.EventHandler(this.txtHRUParentID_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(149, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "HRU Parent";
            this.toolTip1.SetToolTip(this.label4, "Thermal Line ID that this Segment gets its data from");
            // 
            // btnCancelSegmentChanges
            // 
            this.btnCancelSegmentChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelSegmentChanges.Location = new System.Drawing.Point(349, 375);
            this.btnCancelSegmentChanges.Name = "btnCancelSegmentChanges";
            this.btnCancelSegmentChanges.Size = new System.Drawing.Size(95, 23);
            this.btnCancelSegmentChanges.TabIndex = 16;
            this.btnCancelSegmentChanges.Text = "Cancel Changes";
            this.toolTip1.SetToolTip(this.btnCancelSegmentChanges, "Cancel any changes made to this segment");
            this.btnCancelSegmentChanges.UseVisualStyleBackColor = true;
            this.btnCancelSegmentChanges.Click += new System.EventHandler(this.btnCancelSegmentChanges_Click);
            // 
            // btnSaveSegmentChanges
            // 
            this.btnSaveSegmentChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveSegmentChanges.Location = new System.Drawing.Point(248, 375);
            this.btnSaveSegmentChanges.Name = "btnSaveSegmentChanges";
            this.btnSaveSegmentChanges.Size = new System.Drawing.Size(95, 23);
            this.btnSaveSegmentChanges.TabIndex = 15;
            this.btnSaveSegmentChanges.Text = "Save Changes";
            this.toolTip1.SetToolTip(this.btnSaveSegmentChanges, "Write changes to segment");
            this.btnSaveSegmentChanges.UseVisualStyleBackColor = true;
            this.btnSaveSegmentChanges.Click += new System.EventHandler(this.btnSaveSegmentChanges_Click);
            // 
            // txtCorrosionAllowance
            // 
            this.txtCorrosionAllowance.Location = new System.Drawing.Point(152, 255);
            this.txtCorrosionAllowance.Name = "txtCorrosionAllowance";
            this.txtCorrosionAllowance.Size = new System.Drawing.Size(100, 20);
            this.txtCorrosionAllowance.TabIndex = 14;
            this.txtCorrosionAllowance.Tag = "CorrosionAllowance";
            // 
            // txtInsulationThickness
            // 
            this.txtInsulationThickness.Location = new System.Drawing.Point(152, 209);
            this.txtInsulationThickness.Name = "txtInsulationThickness";
            this.txtInsulationThickness.Size = new System.Drawing.Size(100, 20);
            this.txtInsulationThickness.TabIndex = 13;
            this.txtInsulationThickness.Tag = "InsulationThickness";
            // 
            // txtTemperatureUnits
            // 
            this.txtTemperatureUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemperatureUnits.Location = new System.Drawing.Point(454, 233);
            this.txtTemperatureUnits.Name = "txtTemperatureUnits";
            this.txtTemperatureUnits.ReadOnly = true;
            this.txtTemperatureUnits.Size = new System.Drawing.Size(100, 20);
            this.txtTemperatureUnits.TabIndex = 12;
            this.txtTemperatureUnits.Tag = "TemperatureUnits";
            this.txtTemperatureUnits.Text = "TemperatureUnits";
            // 
            // txtTemperature
            // 
            this.txtTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemperature.Enabled = false;
            this.txtTemperature.Location = new System.Drawing.Point(454, 207);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(100, 20);
            this.txtTemperature.TabIndex = 11;
            this.txtTemperature.Tag = "Temperature";
            this.txtTemperature.Text = "Temperature";
            // 
            // txtPressureUnits
            // 
            this.txtPressureUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPressureUnits.Location = new System.Drawing.Point(348, 233);
            this.txtPressureUnits.Name = "txtPressureUnits";
            this.txtPressureUnits.ReadOnly = true;
            this.txtPressureUnits.Size = new System.Drawing.Size(100, 20);
            this.txtPressureUnits.TabIndex = 10;
            this.txtPressureUnits.Tag = "PressureUnits";
            this.txtPressureUnits.Text = "PressureUnits";
            // 
            // txtPressure
            // 
            this.txtPressure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPressure.Enabled = false;
            this.txtPressure.Location = new System.Drawing.Point(348, 207);
            this.txtPressure.Name = "txtPressure";
            this.txtPressure.Size = new System.Drawing.Size(100, 20);
            this.txtPressure.TabIndex = 9;
            this.txtPressure.Tag = "Pressure";
            this.txtPressure.Text = "Pressure";
            // 
            // txtSecondaryLineDescription
            // 
            this.txtSecondaryLineDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecondaryLineDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSecondaryLineDescription.Location = new System.Drawing.Point(283, 70);
            this.txtSecondaryLineDescription.MaxLength = 255;
            this.txtSecondaryLineDescription.Name = "txtSecondaryLineDescription";
            this.txtSecondaryLineDescription.Size = new System.Drawing.Size(428, 20);
            this.txtSecondaryLineDescription.TabIndex = 3;
            this.txtSecondaryLineDescription.Tag = "SecondaryLineDescription";
            // 
            // btnDeleteSegment
            // 
            this.btnDeleteSegment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteSegment.Location = new System.Drawing.Point(14, 375);
            this.btnDeleteSegment.Name = "btnDeleteSegment";
            this.btnDeleteSegment.Size = new System.Drawing.Size(95, 23);
            this.btnDeleteSegment.TabIndex = 17;
            this.btnDeleteSegment.Text = "Delete Segment";
            this.toolTip1.SetToolTip(this.btnDeleteSegment, "Delete the currently selected Segment");
            this.btnDeleteSegment.UseVisualStyleBackColor = true;
            this.btnDeleteSegment.Click += new System.EventHandler(this.btnDeleteSegment_Click);
            // 
            // btnEditSegment
            // 
            this.btnEditSegment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditSegment.Location = new System.Drawing.Point(14, 346);
            this.btnEditSegment.Name = "btnEditSegment";
            this.btnEditSegment.Size = new System.Drawing.Size(95, 23);
            this.btnEditSegment.TabIndex = 2;
            this.btnEditSegment.Text = "Edit Segment";
            this.toolTip1.SetToolTip(this.btnEditSegment, "Edit the data for the selected Segment");
            this.btnEditSegment.UseVisualStyleBackColor = true;
            this.btnEditSegment.Click += new System.EventHandler(this.btnEditSegment_Click);
            // 
            // btnNewSegment
            // 
            this.btnNewSegment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewSegment.Location = new System.Drawing.Point(14, 317);
            this.btnNewSegment.Name = "btnNewSegment";
            this.btnNewSegment.Size = new System.Drawing.Size(95, 23);
            this.btnNewSegment.TabIndex = 1;
            this.btnNewSegment.Text = "New Segment";
            this.toolTip1.SetToolTip(this.btnNewSegment, "Add a segment to the selected Pipeline");
            this.btnNewSegment.UseVisualStyleBackColor = true;
            this.btnNewSegment.Click += new System.EventHandler(this.BtnNewSegment_Click);
            // 
            // txtMainLineDescription
            // 
            this.txtMainLineDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMainLineDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMainLineDescription.Enabled = false;
            this.txtMainLineDescription.Location = new System.Drawing.Point(282, 28);
            this.txtMainLineDescription.Name = "txtMainLineDescription";
            this.txtMainLineDescription.Size = new System.Drawing.Size(429, 20);
            this.txtMainLineDescription.TabIndex = 2;
            this.txtMainLineDescription.Tag = "MainLineDescription";
            // 
            // gbPipeLines
            // 
            this.gbPipeLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPipeLines.Controls.Add(this.gbSegment);
            this.gbPipeLines.Controls.Add(this.btnCancelPipeChanges);
            this.gbPipeLines.Controls.Add(this.btnSavePipeChanges);
            this.gbPipeLines.Controls.Add(this.label2);
            this.gbPipeLines.Controls.Add(this.btnEditPipeLine);
            this.gbPipeLines.Controls.Add(this.btnDeletePipeline);
            this.gbPipeLines.Controls.Add(this.txtMainLineDescription);
            this.gbPipeLines.Controls.Add(this.label1);
            this.gbPipeLines.Controls.Add(this.txtPipeLineName);
            this.gbPipeLines.Controls.Add(this.lstPipeLines);
            this.gbPipeLines.Controls.Add(this.btnCreateNewPipeLine);
            this.gbPipeLines.Controls.Add(this.txtSecondaryLineDescription);
            this.gbPipeLines.Controls.Add(this.label3);
            this.gbPipeLines.Location = new System.Drawing.Point(12, 12);
            this.gbPipeLines.Name = "gbPipeLines";
            this.gbPipeLines.Size = new System.Drawing.Size(717, 558);
            this.gbPipeLines.TabIndex = 69;
            this.gbPipeLines.TabStop = false;
            this.gbPipeLines.Text = "Pipelines";
            // 
            // btnCancelPipeChanges
            // 
            this.btnCancelPipeChanges.Enabled = false;
            this.btnCancelPipeChanges.Location = new System.Drawing.Point(496, 96);
            this.btnCancelPipeChanges.Name = "btnCancelPipeChanges";
            this.btnCancelPipeChanges.Size = new System.Drawing.Size(95, 23);
            this.btnCancelPipeChanges.TabIndex = 58;
            this.btnCancelPipeChanges.Text = "Cancel Changes";
            this.btnCancelPipeChanges.UseVisualStyleBackColor = true;
            this.btnCancelPipeChanges.Click += new System.EventHandler(this.btnCancelPipeChanges_Click);
            // 
            // btnSavePipeChanges
            // 
            this.btnSavePipeChanges.Enabled = false;
            this.btnSavePipeChanges.Location = new System.Drawing.Point(395, 96);
            this.btnSavePipeChanges.Name = "btnSavePipeChanges";
            this.btnSavePipeChanges.Size = new System.Drawing.Size(95, 23);
            this.btnSavePipeChanges.TabIndex = 58;
            this.btnSavePipeChanges.Text = "Save Changes";
            this.btnSavePipeChanges.UseVisualStyleBackColor = true;
            this.btnSavePipeChanges.Click += new System.EventHandler(this.btnSavePipeChanges_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Pipeline Name";
            // 
            // btnEditPipeLine
            // 
            this.btnEditPipeLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditPipeLine.Location = new System.Drawing.Point(22, 494);
            this.btnEditPipeLine.Name = "btnEditPipeLine";
            this.btnEditPipeLine.Size = new System.Drawing.Size(95, 23);
            this.btnEditPipeLine.TabIndex = 4;
            this.btnEditPipeLine.Text = "Edit Pipeline";
            this.toolTip1.SetToolTip(this.btnEditPipeLine, "Edit the selected Pipeline");
            this.btnEditPipeLine.UseVisualStyleBackColor = true;
            this.btnEditPipeLine.Click += new System.EventHandler(this.btnEditPipeLine_Click);
            // 
            // btnDeletePipeline
            // 
            this.btnDeletePipeline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeletePipeline.Location = new System.Drawing.Point(22, 523);
            this.btnDeletePipeline.Name = "btnDeletePipeline";
            this.btnDeletePipeline.Size = new System.Drawing.Size(95, 23);
            this.btnDeletePipeline.TabIndex = 5;
            this.btnDeletePipeline.Text = "DELETE";
            this.toolTip1.SetToolTip(this.btnDeletePipeline, "DELETE the selected Pipeline and all of its Segments");
            this.btnDeletePipeline.UseVisualStyleBackColor = true;
            this.btnDeletePipeline.Click += new System.EventHandler(this.btnDeletePipeline_Click);
            // 
            // txtPipeLineName
            // 
            this.txtPipeLineName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPipeLineName.Enabled = false;
            this.txtPipeLineName.Location = new System.Drawing.Point(144, 28);
            this.txtPipeLineName.Name = "txtPipeLineName";
            this.txtPipeLineName.Size = new System.Drawing.Size(120, 20);
            this.txtPipeLineName.TabIndex = 1;
            this.txtPipeLineName.Tag = "NELineNumber";
            // 
            // frmProcessLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 572);
            this.Controls.Add(this.gbPipeLines);
            this.Name = "frmProcessLines";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pipelines and Segment Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmProcessLines_FormClosed);
            this.Load += new System.EventHandler(this.FrmProcessLines_Load);
            this.gbSegment.ResumeLayout(false);
            this.gbSegment.PerformLayout();
            this.gbPipeLines.ResumeLayout(false);
            this.gbPipeLines.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateNewPipeLine;
        private System.Windows.Forms.ListBox lstPipeLines;
        private System.Windows.Forms.ListBox lstSegments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbSegment;
        private System.Windows.Forms.GroupBox gbPipeLines;
        private System.Windows.Forms.Button btnNewSegment;
        private System.Windows.Forms.TextBox txtPipeLineName;
        private System.Windows.Forms.Button btnEditSegment;
        private System.Windows.Forms.Button btnDeleteSegment;
        private System.Windows.Forms.TextBox txtCorrosionAllowance;
        private System.Windows.Forms.TextBox txtInsulationThickness;
        private System.Windows.Forms.TextBox txtTemperatureUnits;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.TextBox txtPressureUnits;
        private System.Windows.Forms.TextBox txtPressure;
        private System.Windows.Forms.TextBox txtSecondaryLineDescription;
        private System.Windows.Forms.TextBox txtMainLineDescription;
        private System.Windows.Forms.Button btnSaveSegmentChanges;
        private System.Windows.Forms.Button btnCancelSegmentChanges;
        private System.Windows.Forms.Button btnDeletePipeline;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEditPipeLine;
        private System.Windows.Forms.Button btnCancelPipeChanges;
        private System.Windows.Forms.Button btnSavePipeChanges;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGetHRURecord;
        private System.Windows.Forms.TextBox txtHRUParentID;
        private System.Windows.Forms.ComboBox cboPipeMaterial;
        private System.Windows.Forms.ComboBox cboANSIClass;
        private System.Windows.Forms.ComboBox cboCodeJurisdiction;
        private System.Windows.Forms.Button btnViewThermalRecord;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboDiameter;
        private System.Windows.Forms.ComboBox cboWallThickness;
        private System.Windows.Forms.TextBox txtExcelParentID;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnViewExcelRecord;
        private System.Windows.Forms.Button btnGetExcelRecord;
        private System.Windows.Forms.Button btnClearExcel;
        private System.Windows.Forms.Button btnClearThermal;
        private System.Windows.Forms.CheckBox chkIsTubing;
        private System.Windows.Forms.TextBox txtThicknessCaseTemperature;
        private System.Windows.Forms.TextBox txtThicknessCasePressure;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtTemperatureCaseTemperature;
        private System.Windows.Forms.TextBox txtTemperatureCasePressure;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtPressureCaseTemperature;
        private System.Windows.Forms.TextBox txtPressureCasePressure;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMaxOperatingTemp;
    }
}