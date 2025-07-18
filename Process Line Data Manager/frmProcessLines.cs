using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using P4DHelperClass = Plant4D;

namespace Process_Line_Data_Manager
{
    public partial class frmProcessLines : Form
    {
        //Size used to define the minimum size of the form
        static Size FormMinimumSize = new Size(751, 611);

        //Variable(s) to indicate how the program behaves
        private bool MDCEnable = false;

        //Variables to hold the data when the form is called.
        private readonly P4DHelperClass.Plant4DServer ActiveServer = new P4DHelperClass.Plant4DServer();
        private readonly P4DProject ActiveProject = new P4DProject();

        //Variables related to the data that I am going to test going forward:
        private SqlDataAdapter daPipeLine;          //NEW adapter for table NE_PipeLines
        private SqlDataAdapter daSegment;           //NEW adapter for table NE_Segments
        private SqlDataAdapter daMap;               //NEW adapter for table NE_Pipeline_Segment_Map
        private SqlDataAdapter daHRU;               //Adapter for table NE_HRUData
        private SqlDataAdapter daExcel;
        private DataSet dsPipeLine;                 //NEW dataset for tables
        private readonly string sqlConnString;      //Connection string for connecting to the project database
        private BindingSource bsSegments;           //BindingSource object to help with getting data back and forth with the form
        private BindingSource bsMap;
        private BindingSource bsPipeline;
        private BindingSource bsExcel;
        private BindingSource bsHRU;

        //These are used to hold the table names in case they change in the future.
        private const string MATERIALTABLENAME = "NE_CombinedMat";
        private const string MATERIALTABLEFIELDNAME = "PipeValveMat";
        private const string ANSICLASSTABLENAME = "NE_Class";
        private const string ANSICLASSFIELDNAME = "ClassDesc";
        private const string CODEJURISDICTIONTABLENAME = "NE_InsulColor";
        private const string CODEJURISDICTIONFIELDNAME = "InsulDesc";
        private const string DIAMETERTABLENAME = "NE_Diameter";
        private const string DIAMETERFIELDNAME = "DiameterDisplay";
        private const string DIAMETERTRANSLATIONFIELDNAME = "DiameterActualOD";
        private const string SCHEDULETABLENAME = "NE_Schedule";
        private const string SCHEDULEFIELDNAME = "ScheduleDesc";

        private enum FormActions
        {
            NewSegment,
            EditSegment,
            DeleteSegment,
            SaveSegment,
            CancelSegment,
            NewPipe,
            EditPipe,
            DeletePipe,
            SavePipe,
            CancelPipe
        }

        public frmProcessLines(P4DHelperClass.Plant4DServer projectserver, P4DProject project)
        {
            InitializeComponent();

            ActiveServer = projectserver;
            ActiveProject = project;
            sqlConnString = P4DHelperClass.Plant4D.GetProjectConnectionString(ActiveProject.DbName, ActiveServer.ServerType, ActiveServer.P4DType);

            this.Text = $"Process Lines for {ActiveProject.Name} on {ActiveServer.ServerName}";
        }

        private void FrmProcessLines_Load(object sender, EventArgs e)
        {
            //Form actions:
            this.MinimumSize = FormMinimumSize;

            //Get our dataset built and assigned:
            dsPipeLine = CreateDataSet();

            //Setup the form controls
            SetFormControls(FormActions.NewSegment);

            //Build the BindingSources needed for the form
            bsPipeline = new BindingSource();
            bsPipeline.DataSource = dsPipeLine.Tables[Pipeline.PIPELINETABLENAME];
            bsMap = new BindingSource();
            bsMap.DataSource = dsPipeLine.Tables[PipelineSegmentMap.MAPTABLENAME];
            bsSegments = new BindingSource();
            bsSegments.DataSource = dsPipeLine.Tables[Segment.SEGMENTTABLENAME];
            //Added for HRU and Excel data
            bsExcel = new BindingSource();
            bsExcel.DataSource = dsPipeLine.Tables[ExcelData.EXCELDATA_TABLENAME];
            bsHRU = new BindingSource();
            bsHRU.DataSource = dsPipeLine.Tables[Thermal.THERMALTABLENAMEP4D];

            //Associate the pipeline portion of dataset to its listbox
            lstPipeLines.DataSource = bsPipeline;
            lstPipeLines.DataBindings.Add("Text", bsPipeline, "NELineNumber");
            lstPipeLines.DisplayMember = "NELineNumber";
            lstPipeLines.ValueMember = "NELineNumber";
            txtPipeLineName.DataBindings.Add("Text", bsPipeline, "NELineNumber");
            txtMainLineDescription.DataBindings.Add("Text", bsPipeline, "LineDescription");
            txtSecondaryLineDescription.DataBindings.Add("Text", bsPipeline, "SecondaryLineDescription");

            //Bind the map view to its listbox
            lstSegments.DataSource = bsMap;
            lstSegments.DataBindings.Add("Text", bsMap, "SegmentID");
            lstSegments.DisplayMember = "SegmentID";
            lstSegments.ValueMember = "SegmentID";

            //Bind combo box data sources
            DataBoundComboBoxSetup(cboPipeMaterial, MATERIALTABLENAME, MATERIALTABLEFIELDNAME);
            DataBoundComboBoxSetup(cboANSIClass, ANSICLASSTABLENAME, ANSICLASSFIELDNAME);
            DataBoundComboBoxSetup(cboCodeJurisdiction, CODEJURISDICTIONTABLENAME, CODEJURISDICTIONFIELDNAME);
            DataBoundComboBoxSetup(cboDiameter, DIAMETERTABLENAME, DIAMETERFIELDNAME, DIAMETERTRANSLATIONFIELDNAME);
            DataBoundComboBoxSetup(cboWallThickness, SCHEDULETABLENAME, SCHEDULEFIELDNAME);

            //Bind segments to form controls
            BindSegmentDetailsControls();
        }

        /// <summary>
        /// Adds a "default" value to the datasource's table and assigns that datasource to the combobox.
        /// </summary>
        /// <param name="myComboBox">ComboBox object to update</param>
        /// <param name="tableName">Name of table for DataSource</param>
        /// <param name="displayFieldName">Name of field for DataSource</param>
        private void DataBoundComboBoxSetup(ComboBox myComboBox, string tableName, string displayFieldName, string valueFieldName = "")
        {
            DataRow row = dsPipeLine.Tables[tableName].NewRow();
            row[displayFieldName] = "";
            dsPipeLine.Tables[tableName].Rows.InsertAt(row, 0);
            myComboBox.DataSource = dsPipeLine.Tables[tableName];
            //If a value was passed in for valueFieldName, set it as the combobox's ValueMember
            if (valueFieldName == "")
            {
                myComboBox.ValueMember = displayFieldName;
            }
            else myComboBox.ValueMember = valueFieldName;
            myComboBox.DisplayMember = displayFieldName;
        }


        /// <summary>
        /// The purpose of this is to enable or disable all the textboxes in the Segment group box.
        /// </summary>
        /// <param name="enabled">true or false; defaults to false</param>
        private void SegmentTextBoxesEnabled(bool enabled = false)
        {
            foreach (Control c in gbSegment.Controls)
            {
                //if ((c.GetType() == typeof(TextBox)) && (c.Tag.ToString() != "HRUParentID" && (c.Tag.ToString() != "ExcelParentID")))
                if ((c.GetType() == typeof(TextBox)) && !(string.IsNullOrEmpty(c.Tag.ToString())))
                {
                    switch (c.Tag.ToString().ToLower())
                    {
                        //add cases for boxes that should not change state during the operation of the application.
                        case "hruparentid":
                            break;
                        case "excelparentid":
                            break;
                        case "temperature":
                            //if P4D NE_Segments table has MDC columns, disable this box; otherwise, it needs to be enabled.
                            if (!MDCEnable)
                            {
                                c.Enabled = enabled;
                            }
                            break;
                        case "pressure":
                            //if P4D NE_Segments table has MDC columns, disable this box; otherwise, it needs to be enabled.
                            if (!MDCEnable)
                            {
                                c.Enabled = enabled;
                            }
                            break;
                        default:
                            if (dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Columns.Contains(c.Tag.ToString()))
                            {
                                c.Enabled = enabled;
                            }
                            else c.Text = "Disabled";
                            break;
                    }

                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    if (dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Columns.Contains(c.Tag.ToString()))
                    {
                        c.Enabled = enabled;
                    }
                }
                //IsTubing checkbox should only get enabled/disabled if the project has the column. otherwise, notify the user with a tooltip and disable the control.
                else if (c.GetType() == typeof(CheckBox) && (c.Tag.ToString() == "IsTubing"))
                {
                    if (dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Columns.Contains(c.Tag.ToString()))
                    {
                        c.Enabled = enabled;
                    }
                }
            }
        }

        /// <summary>
        /// Binds the controls for the Segment group box to the datasource bsSegments based on the control's TAG value.
        /// </summary>
        private void BindSegmentDetailsControls()
        {
            bool founddescrepancy = false;

            foreach (Control c in gbSegment.Controls)
            {
                try
                {
                    //We want text boxes that have Tag values that are not "MainLineDescription":
                    if ((c.GetType() == typeof(TextBox)) && (!(c.Tag == null)) && c.Tag.ToString() != "MainLineDescription")
                    {
                        c.DataBindings.Clear();

                        //Only bind the control if the column with the name of the Tag exists:
                        if (dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Columns.Contains(c.Tag.ToString()))
                        {
                            Binding testBinding = c.DataBindings.Add("Text", bsSegments, c.Tag.ToString(), true);
                            testBinding.DataSourceNullValue = DBNull.Value;
                            testBinding.NullValue = "";
                        }
                        else
                        {
                            c.Enabled = false;
                        }
                    }
                    //This portion is for the ComboBoxes that have tag values:
                    else if (c.GetType() == typeof(ComboBox) && (!(c.Tag == null)))
                    {
                        c.DataBindings.Clear();

                        //Only bind the control if the column with the name of the Tag exists:
                        if (dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Columns.Contains(c.Tag.ToString()))
                        {
                            Binding myBinding = new Binding("Text", bsSegments, c.Tag.ToString(), true);
                            myBinding.DataSourceNullValue = DBNull.Value;
                            myBinding.NullValue = "";
                            c.DataBindings.Add(myBinding);
                        }
                        else
                        {
                            c.Enabled = false;
                        }
                    }
                    //And now CheckBoxes that have Tag values:
                    else if (c.GetType() == typeof(CheckBox) && (!(c.Tag == null)))
                    {
                        c.DataBindings.Clear();

                        //Only bind the control if the column with the name of the Tag exists:
                        if (dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Columns.Contains(c.Tag.ToString()))
                        {
                            Binding myBinding = new Binding("Checked", bsSegments, c.Tag.ToString(), true);
                            myBinding.DataSourceNullValue = 0;
                            myBinding.NullValue = 0;
                            c.DataBindings.Add(myBinding);
                        }
                        else
                        {
                            c.Enabled = false;
                        }

                    }
                }
                catch (Exception)
                {
                    if (c.Name == "chkIsTubing")
                    {
                        MessageBox.Show("Project may not be set up for 'IsTubing' functionality. Contact CAE if this is required. The checkbox will function within the form but will have no effect on the selected segments and will not be saved.");
                    }
                    else if (founddescrepancy == false)
                    {
                        founddescrepancy = true;
                        MessageBox.Show($"Could not bind one or more data fields to the dialog. Further errors will be suppressed.");
                    }
                }
            }
        }

        private DataSet CreateDataSet()
        {
            //Create the object to return
            DataSet ds = new DataSet("PipeLineData");
            //Start with the pipe line table
            ds.Tables.Add(Pipeline.PIPELINETABLENAME);

            //Get the DataAdapter for PipeLines
            daPipeLine = Pipeline.PipeLineDataAdapterSetup();

            //Open a connection and fill the DataSet for pipelines
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnString))
                {
                    daPipeLine.SelectCommand.Connection = conn;
                    daPipeLine.FillSchema(ds, SchemaType.Source, Pipeline.PIPELINETABLENAME);
                    daPipeLine.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filling pipe line data set:" + ex.Message);
            }

            //Open a connection and fill the DataSet for Segments
            try
            {
                //Now we need the segments table, so do the same thing...
                ds.Tables.Add(Segment.SEGMENTTABLENAME);

                //Get the DataAdapter for Segments
                daSegment = Segment.SegmentAdapterSetup(sqlConnString);

                daSegment.FillSchema(ds, SchemaType.Source, Segment.SEGMENTTABLENAME);

                //The default value doesn't come across with FillSchema so it gets set here instead. Now there shouldn't be any issues when .NewRow() or .AddRow(myRow) is used.
                DataColumnCollection columns = ds.Tables[Segment.SEGMENTTABLENAME].Columns;
                //Also, the column might not exist, so check for it before trying to set the default.
                if (columns.Contains("IsTubing"))
                {
                    ds.Tables[Segment.SEGMENTTABLENAME].Columns["IsTubing"].DefaultValue = 0;
                }

                //Check if project has MDC enabled:
                if (columns.Contains(Segment.MDCPRESSURE_TESTCOLUMNNAME))
                {
                    MDCEnable = true;
                }

                daSegment.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filling segment data set:" + ex.Message);
            }

            //...and finally, we need the mapping table...
            ds.Tables.Add(PipelineSegmentMap.MAPTABLENAME);

            //get the DataAdapter for the map table
            daMap = PipelineSegmentMap.MapAdapterSetup();

            //Open a connection and fill the DataSet for the mapping table
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnString))
                {
                    daMap.SelectCommand.Connection = conn;
                    daMap.FillSchema(ds, SchemaType.Source, PipelineSegmentMap.MAPTABLENAME);
                    daMap.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filling mapping table data set:" + ex.Message);
            }

            //Build the relationships between the 3 tables:
            //Pipeline to map table
            DataColumn parentColumn = ds.Tables[Pipeline.PIPELINETABLENAME].Columns["NELineNumber"];
            DataColumn childColumn = ds.Tables[PipelineSegmentMap.MAPTABLENAME].Columns["NELineNumber"];
            DataRelation relation = new DataRelation("PipelineSegments", parentColumn, childColumn);

            ds.Relations.Add(relation);

            //Segments to map table
            parentColumn = ds.Tables[Segment.SEGMENTTABLENAME].Columns["ID"];
            childColumn = ds.Tables[PipelineSegmentMap.MAPTABLENAME].Columns["SegmentID"];
            relation = new DataRelation("SegmentFromPipeline", parentColumn, childColumn);

            ds.Relations.Add(relation);

            ds.Tables.Add("NE_HRUData");

            //Now we need to have the P4D HRU data available to us:
            daHRU = Thermal.ThermalAdapterSetup(sqlConnString);

            //Open a connection and fill the DataSet for the HRU table
            try
            {
                daHRU.FillSchema(ds, SchemaType.Source, Thermal.THERMALTABLENAMEP4D);
                daHRU.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filling HRU table data set:" + ex.Message);
            }

            //Now we need Excel data to be available to us:
            daExcel = ExcelData.ExcelSQLDataAdapterSetup(sqlConnString);

            //Open a connection and fill the DataSet for the Excel table
            try
            {
                daExcel.FillSchema(ds, SchemaType.Source, ExcelData.EXCELDATA_TABLENAME);
                daExcel.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filling Excel table data set: {ex.Message}");
            }

            //Load combo boxes with data
            //Pipe Material
            FillLookupTable(ds, MATERIALTABLENAME, MATERIALTABLEFIELDNAME);
            //ANSI Class
            FillLookupTable(ds, ANSICLASSTABLENAME, ANSICLASSFIELDNAME);
            //Code Jurisdiction
            FillLookupTable(ds, CODEJURISDICTIONTABLENAME, CODEJURISDICTIONFIELDNAME);
            //Diameter
            FillLookupTable(ds, DIAMETERTABLENAME, $"{DIAMETERFIELDNAME},{DIAMETERTRANSLATIONFIELDNAME}", DIAMETERTRANSLATIONFIELDNAME);
            //Schedule
            FillLookupTable(ds, SCHEDULETABLENAME, SCHEDULEFIELDNAME);

            return ds;
        }

        /// <summary>
        /// Creates/populates a lookup table with one field, and adds it to the provided DataSet. 
        /// </summary>
        /// <param name="ds">DataSet to add table to</param>
        /// <param name="TableName">Name of table to query</param>
        /// <param name="FieldName">Name of column that holds values</param>
        /// <param name="OptionalOrderBy">Column to order by; default is FieldName</param>
        private void FillLookupTable(DataSet ds, string TableName, string FieldName, string OptionalOrderBy = "")
        {
            SqlDataAdapter da;
            string query;
            SqlCommand comm;
            da = new SqlDataAdapter();
            ds.Tables.Add(TableName);
            da = new SqlDataAdapter();
            da.TableMappings.Add("Table", TableName);

            query = $"SELECT {FieldName} FROM {TableName} ORDER BY ";
            switch (OptionalOrderBy)
            {
                case "":
                    query += $"{FieldName}";
                    break;
                default:
                    query += $"{OptionalOrderBy}";
                    break;
            }
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;

            da.SelectCommand = comm;

            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnString))
                {
                    da.SelectCommand.Connection = conn;
                    da.FillSchema(ds, SchemaType.Source, TableName);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filling data set for {TableName}.{FieldName}:" + ex.Message);
            }
        }

        private void LstPipeLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Make sure something is selected; -1 means nothing is selected; "System.Data.DataRowView" means the list is updating and isn't ready for prime time...
            if ((lstPipeLines.SelectedIndex > -1) && (lstPipeLines.Text != "System.Data.DataRowView"))
            {
                if (!(bsSegments == null))
                {
                    //Refresh Segments view
                    ApplyFilterToView(bsMap, "NELineNumber", lstPipeLines.Text);

                    if (lstSegments.SelectedIndex > -1)
                    {
                        //The data is filtered, now we need to reset the selected value in the next list box, so it will
                        //  populate the data accordingly...
                        lstSegments.SelectedIndex = 0;

                        ApplyFilterToView(bsSegments, "ID", int.Parse(lstSegments.Text));
                    }
                }
            }
        }

        private void ApplyFilterToView(BindingSource bsToFilter, string columnToFilterOn, string valueToFilterOn)
        {
            bsToFilter.RemoveFilter();
            string filter = $"[{columnToFilterOn}]='{valueToFilterOn}'";
            bsToFilter.Filter = filter;
        }
        private void ApplyFilterToView(BindingSource bsToFilter, string columnToFilterOn, int valueToFilterOn)
        {
            bsToFilter.RemoveFilter();
            string filter = $"[{columnToFilterOn}]={valueToFilterOn}";
            bsToFilter.Filter = filter;
        }

        private void FrmProcessLines_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = new frmMain();
            if (!(Application.OpenForms[form.Name] == null))
            {
                Application.OpenForms[form.Name].Show();
                Application.OpenForms[form.Name].Focus();
            }
        }

        private void AddNewSegment(string PipelineID)
        {
            //TODO: fix this so adding a segment will take care of the map table entry also.

            //We will need this later...
            string identifier;

            //Create a row object for the map table, and one for the segment table
            DataRow segmentRow = dsPipeLine.Tables[Segment.SEGMENTTABLENAME].NewRow();
            dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Rows.Add(segmentRow);

            //Call the update method for the DataSet. It should return the newly added IDENTIFIER value
            using (SqlConnection conn = new SqlConnection(sqlConnString))
            {
                daSegment.InsertCommand.Parameters["@PipeLine"].Value = PipelineID;
                daSegment.InsertCommand.Connection = conn;
                daSegment.Update(dsPipeLine);
                identifier = daSegment.InsertCommand.Parameters["@ID"].Value.ToString();

                //refresh our table
                daSegment.SelectCommand.Connection = conn;
                daSegment.Fill(dsPipeLine.Tables[Segment.SEGMENTTABLENAME]);

                //Now refresh our data from the database
                daMap.SelectCommand.Connection = conn;
                daMap.Fill(dsPipeLine.Tables[PipelineSegmentMap.MAPTABLENAME]);
            }

            //Search through the list of items and select what needs to be selected. Start with the NELineNumber
            lstPipeLines.SelectedIndex = lstPipeLines.FindStringExact(PipelineID);

            //That should have loaded the segments, so let's tell it to select our new one, which should be the last item in the list.
            lstSegments.SelectedIndex = lstSegments.FindStringExact(identifier);
        }

        private void SetFormControls(FormActions action)
        {
            switch (action)
            {
                //case SegmentActions.Delete:
                //default action
                //case SegmentActions.New:
                //default action
                //case SegmentActions.Save:
                //default action
                //case SegmentAction.Cancel:
                //default action
                case FormActions.EditSegment:
                    SegmentTextBoxesEnabled(true);
                    btnNewSegment.Enabled = false;
                    btnEditSegment.Enabled = false;
                    btnSaveSegmentChanges.Enabled = true;
                    btnCancelSegmentChanges.Enabled = true;
                    btnDeleteSegment.Enabled = false;
                    lstSegments.Enabled = false;
                    btnGetHRURecord.Enabled = true;
                    btnViewThermalRecord.Enabled = true;
                    btnClearThermal.Enabled = true;
                    btnGetExcelRecord.Enabled = true;
                    btnViewExcelRecord.Enabled = true;
                    btnClearExcel.Enabled = true;
                    btnEditPipeLine.Enabled = false;
                    btnCreateNewPipeLine.Enabled = false;
                    btnDeletePipeline.Enabled = false;
                    btnSavePipeChanges.Enabled = false;
                    btnCancelPipeChanges.Enabled = false;
                    lstPipeLines.Enabled = false;
                    //txtPipeLineName.Enabled = false;      //<--should never be enabled since it's the table's key value.
                    txtMainLineDescription.Enabled = false;
                    txtSecondaryLineDescription.Enabled = false;
                    break;
                case FormActions.EditPipe:
                    SegmentTextBoxesEnabled(false);
                    btnNewSegment.Enabled = false;
                    btnEditSegment.Enabled = false;
                    btnSaveSegmentChanges.Enabled = false;
                    btnCancelSegmentChanges.Enabled = false;
                    btnDeleteSegment.Enabled = false;
                    lstSegments.Enabled = false;
                    btnGetHRURecord.Enabled = false;
                    btnViewThermalRecord.Enabled = false;
                    btnClearThermal.Enabled = false;
                    btnGetExcelRecord.Enabled = false;
                    btnViewExcelRecord.Enabled = false;
                    btnClearExcel.Enabled = false;
                    btnEditPipeLine.Enabled = false;
                    btnCreateNewPipeLine.Enabled = false;
                    btnDeletePipeline.Enabled = false;
                    btnSavePipeChanges.Enabled = true;
                    btnCancelPipeChanges.Enabled = true;
                    lstPipeLines.Enabled = false;
                    //txtPipeLineName.Enabled = false;      //<--should never be enabled since it's the table's key value.
                    txtMainLineDescription.Enabled = true;
                    txtSecondaryLineDescription.Enabled = true;
                    break;
                default:
                    SegmentTextBoxesEnabled(false);
                    btnNewSegment.Enabled = true;
                    btnEditSegment.Enabled = true;
                    btnSaveSegmentChanges.Enabled = false;
                    btnCancelSegmentChanges.Enabled = false;
                    btnDeleteSegment.Enabled = true;
                    btnCancelSegmentChanges.Enabled = false;
                    btnSaveSegmentChanges.Enabled = false;
                    lstSegments.Enabled = true;
                    btnGetHRURecord.Enabled = false;
                    btnViewThermalRecord.Enabled = false;
                    btnClearThermal.Enabled = false;
                    btnGetExcelRecord.Enabled = false;
                    btnViewExcelRecord.Enabled = false;
                    btnClearExcel.Enabled = false;
                    btnEditPipeLine.Enabled = true;
                    btnCreateNewPipeLine.Enabled = true;
                    btnDeletePipeline.Enabled = true;
                    btnSavePipeChanges.Enabled = false;
                    btnCancelPipeChanges.Enabled = false;
                    lstPipeLines.Enabled = true;
                    //txtPipeLineName.Enabled = false;      //<--should never be enabled since it's the table's key value.
                    txtMainLineDescription.Enabled = false;
                    txtSecondaryLineDescription.Enabled = false;
                    break;
            }
        }

        private void AddNewPipeLine(string name, string description = "NEW PIPE LINE", string secondarylinedescription = "", int thermallineid = 0, int excelid = 0)
        {
            //Create a new row object with properties of the table, give it a temporary LineDescription value
            var newRow = dsPipeLine.Tables[Pipeline.PIPELINETABLENAME].NewRow();
            newRow["NELineNumber"] = name.ToUpper();
            newRow["LineDescription"] = description.ToUpper();
            newRow["SecondaryLineDescription"] = secondarylinedescription.ToUpper();
            dsPipeLine.Tables[Pipeline.PIPELINETABLENAME].Rows.Add(newRow);

            //Call the update method for the DataSet. It should return the newly added IDENTIFIER value as an OUTPUT parameter...which I don't think we actually need?
            string identifier;
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnString))
                {
                    daPipeLine.InsertCommand.Connection = conn;
                    daPipeLine.InsertCommand.Parameters["@THERMALID"].Value = thermallineid;
                    daPipeLine.InsertCommand.Parameters["@EXCELID"].Value = excelid;
                    daPipeLine.Update(dsPipeLine);
                    identifier = daPipeLine.InsertCommand.Parameters["@ID"].Value.ToString();

                    daSegment.SelectCommand.Connection = conn;
                    daSegment.Fill(dsPipeLine.Tables[Segment.SEGMENTTABLENAME]);

                    daMap.SelectCommand.Connection = conn;
                    daMap.Fill(dsPipeLine.Tables[PipelineSegmentMap.MAPTABLENAME]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding new pipe line: " + ex.Message);
            }

            //Search through the list and select the pipeline we just added, which will also filter and select the new segment:
            lstPipeLines.SelectedIndex = lstPipeLines.FindStringExact(name);

            //enable/disable the text boxes associated to the segments:
            SetFormControls(FormActions.NewSegment);
        }

        private void DeleteSegment(string SegmentToDelete)
        {
            try
            {
                //Map entries MUST go first, then segments.
                //Find any entries from map table and remove them before continuing.
                DataTable dt = GetDataTableRows(PipelineSegmentMap.MAPTABLENAME, $"SEGMENTID={SegmentToDelete}");

                if (!(dt == null))
                {
                    //Child rows exist in map table. Kill them so we may proceed, Anakin...
                    foreach (DataRow row in dt.Rows)
                    {
                        string segmentid = row["SEGMENTID"].ToString();
                        string pipelineid = row["NELINENUMBER"].ToString();
                        DeleteMapping(segmentid, pipelineid);
                    }
                }
                //Find the row we are deleting
                DataRow rowS = dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Rows.Find(SegmentToDelete);

                //Mark the found rows for deletion. 
                int indexRowS = dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Rows.IndexOf(rowS);
                dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Rows[indexRowS].Delete();

                //Connect to the database and coordinate our changes.
                using (SqlConnection conn = new SqlConnection(sqlConnString))
                {
                    //Assign the connection to the commands, then call .Update on the dataset.
                    daSegment.DeleteCommand.Connection = conn;
                    daSegment.Update(dsPipeLine);
                }
            }
            catch (Exception ex)
            {
                switch (ex.HResult)
                {
                    case (-2146232060):
                        MessageBox.Show("Segment is referenced in Map table and cannot be deleted.");
                        break;
                    default:
                        MessageBox.Show($"Error deleting Segment {SegmentToDelete}: {ex.Message}");
                        break;
                }
            }
        }

        private void DeletePipeline(string PipeLineToDelete)
        {
            try
            {
                //Map entries go first, then segments, then, finally, pipeline entry.
                //Get entries from map table so we know which segments will need deleted later.
                DataTable dt = GetDataTableRows(PipelineSegmentMap.MAPTABLENAME, $"NELINENUMBER='{PipeLineToDelete}'");

                if (!(dt == null))
                {
                    //Rows exist in map table. Kill them, and then their parents...
                    foreach (DataRow row in dt.Rows)
                    {
                        string segmentid = row["SEGMENTID"].ToString();
                        DeleteMapping(segmentid, PipeLineToDelete);
                        DeleteSegment(segmentid);
                    }
                }

                //At this point, there should be no entries for this pipeline in the map table, and the corresponding
                //  segments should also be deleted. Errors will happen otherwise.
                //Find the rows we are deleting
                DataRow rowS = dsPipeLine.Tables[Pipeline.PIPELINETABLENAME].Rows.Find(PipeLineToDelete);

                //Mark the found rows for deletion. 
                int indexRowS = dsPipeLine.Tables[Pipeline.PIPELINETABLENAME].Rows.IndexOf(rowS);
                dsPipeLine.Tables[Pipeline.PIPELINETABLENAME].Rows[indexRowS].Delete();

                //Connect to the database and coordinate our changes.
                using (SqlConnection conn = new SqlConnection(sqlConnString))
                {
                    //Assign the connection to the commands, then call .Update on the dataset.
                    daPipeLine.DeleteCommand.Connection = conn;
                    daPipeLine.Update(dsPipeLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting pipeline {PipeLineToDelete}: {ex.Message}");
            }
        }

        private void DeleteMapping(string ParentSegment, string ParentPipeLine)
        {
            try
            {
                //Find the row we are deleting
                Object[] keys = new object[2];
                keys[0] = ParentPipeLine;
                keys[1] = ParentSegment;
                DataRow rowM = dsPipeLine.Tables[PipelineSegmentMap.MAPTABLENAME].Rows.Find(keys);

                //Mark the found rows for deletion. 
                int indexRowM = dsPipeLine.Tables[PipelineSegmentMap.MAPTABLENAME].Rows.IndexOf(rowM);
                dsPipeLine.Tables[PipelineSegmentMap.MAPTABLENAME].Rows[indexRowM].Delete();

                //Connect to the database and coordinate our changes.
                using (SqlConnection conn = new SqlConnection(sqlConnString))
                {
                    //Assign the connection to the commands, then call .Update on the dataset.
                    daMap.DeleteCommand.Connection = conn;
                    daMap.Update(dsPipeLine);
                }
            }
            catch (Exception ex)
            {
                switch (ex.HResult)
                {
                    default:
                        MessageBox.Show($"Error deleting Map entry for pipeline {ParentPipeLine} and segment {ParentSegment}: {ex.Message}");
                        break;
                }
            }
        }

        private DataTable GetDataTableRows(string dataTableToSearch, string queryString)
        {
            DataTable dt = null;
            var existsResult = dsPipeLine.Tables[dataTableToSearch].Select(queryString);
            if (existsResult.Count() > 0)
            {
                dt = existsResult.CopyToDataTable();
            }

            return dt;
        }

        private void lstSegments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSegments.SelectedIndex > -1 && lstSegments.Text != "System.Data.DataRowView")
            {
                //Apply filter to segment view; that should update the form controls to display the selected segment's info
                ApplyFilterToView(bsSegments, "ID", int.Parse(lstSegments.Text));
            }
        }

        private void btnEditPipeLine_Click(object sender, EventArgs e)
        {
            SetFormControls(FormActions.EditPipe);
        }

        private void btnSavePipeChanges_Click(object sender, EventArgs e)
        {
            //Apply pending changes to the underlying datasource
            bsPipeline.EndEdit();

            using (SqlConnection conn = new SqlConnection(sqlConnString))
            {
                daPipeLine.UpdateCommand.Connection = conn;
                daPipeLine.Update(dsPipeLine);
            }

            //Reload the data to the form (just to be sure it is what we think it should be)
            txtPipeLineName.DataBindings[0].ReadValue();
            txtMainLineDescription.DataBindings[0].ReadValue();
            txtSecondaryLineDescription.DataBindings[0].ReadValue();

            SetFormControls(FormActions.SavePipe);
        }

        private void btnCancelPipeChanges_Click(object sender, EventArgs e)
        {
            //tell the binding source we've changed our minds...
            bsPipeline.CancelEdit();

            //reload the values to what they were previously
            txtPipeLineName.DataBindings[0].ReadValue();
            txtMainLineDescription.DataBindings[0].ReadValue();
            txtSecondaryLineDescription.DataBindings[0].ReadValue();

            SetFormControls(FormActions.CancelPipe);
        }

        private void btnGetHRURecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (dsPipeLine.Tables[Thermal.THERMALTABLENAMEP4D].Rows.Count > 0)
                {
                    using (var form = new frmGetThermalRecord(dsPipeLine.Tables[Thermal.THERMALTABLENAMEP4D], txtHRUParentID.Text.ToString()))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string selectedLineId = form.SelectedLineId.ToString();

                            //load remaining data from dataview based on the selected id.
                            //If the selected value is different from what was previously in the textbox,
                            //  warn user that data is about to be overwritten with HRU data.
                            if (txtExcelParentID.Text != "")
                            {
                                MessageBox.Show("Changing Parent from Excel to HRU!");
                            }
                            if (selectedLineId != txtHRUParentID.Text.ToString() && txtHRUParentID.Text.ToString() != "")
                            {
                                DialogResult overwriteResult;
                                overwriteResult = MessageBox.Show("Thermal Parent has changed. Overwrite segment data with HRU data?", "HRU Parent has changed", MessageBoxButtons.YesNoCancel);
                                if (overwriteResult == DialogResult.Yes)
                                {
                                    SetSegmentDataFromThermal(selectedLineId);
                                }
                                else if (overwriteResult == DialogResult.Cancel)
                                {
                                    //Cancel means put it back to what it was before the button was selected:
                                    txtHRUParentID.DataBindings[0].ReadValue();
                                }
                                else
                                {
                                    //if not Yes and not Cancel, then we just use the HRUParentID value without overwriting anything.
                                    //  Why? I'm not sure. But there ya go.
                                    txtHRUParentID.Text = selectedLineId;
                                }
                            }
                            else if (selectedLineId == txtHRUParentID.Text.ToString())
                            {
                                DialogResult overwriteResult;
                                overwriteResult = MessageBox.Show("Same Thermal Line selected. Overwrite Segment data with Thermal data?", "Refresh Thermal data", MessageBoxButtons.YesNoCancel);
                                if (overwriteResult == DialogResult.Yes)
                                {
                                    SetSegmentDataFromThermal(selectedLineId);
                                }
                                else if (overwriteResult == DialogResult.Cancel)
                                {
                                    //Cancel means put it back to what it was before the button was selected:
                                    txtHRUParentID.DataBindings[0].ReadValue();
                                }
                                else
                                {
                                    //if not Yes and not Cancel, then we just use the HRUParentID value without overwriting anything.
                                    //  Why? I'm not sure. But there ya go.
                                    txtHRUParentID.Text = selectedLineId;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Changing Segment to match selected Thermal data. This warning can be removed in the future...");
                                SetSegmentDataFromThermal(selectedLineId);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No HRU records have been loaded for this project");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while attempting to load HRU record data: {ex.Message}");
            }
        }

        private enum SourceType
        {
            Thermal,
            Excel
        }

        private void SetSegmentData(string selectedLineId, SourceType sourceType)
        {
            try
            {
                //Set value for ParentID:
                switch (sourceType)
                {
                    case SourceType.Thermal:
                        txtHRUParentID.Text = selectedLineId;
                        txtExcelParentID.Text = null;
                        break;
                    case SourceType.Excel:
                        txtHRUParentID.Text = null;
                        txtExcelParentID.Text = selectedLineId;
                        break;
                    default:
                        throw new ArgumentException($"Invalid SourceType passed to SetSegmentData: {sourceType}");
                }

                //Next, clear out the values that were previously selected.
                foreach (Control c in gbSegment.Controls)
                {
                    if ((c.GetType() == typeof(TextBox)) && (c.Tag.ToString() != "HRUParentID" && (c.Tag.ToString() != "ExcelParentID")))
                    {
                        c.Text = "";
                    }
                    if (c.GetType() == typeof(ComboBox))
                    {
                        c.Text = "";
                    }
                    if (c.GetType() == typeof(CheckBox))
                    {
                        CheckBox check;
                        check = c as CheckBox;
                        check.Checked = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetSegmentDataFromThermal(string selectedLineId)
        {
            //First, set the Excel and HRU values accordingly
            txtHRUParentID.Text = selectedLineId;
            txtExcelParentID.Text = null;

            //Get the DataRow by the LineID value:
            DataRow dr = dsPipeLine.Tables["NE_HRUData"].Rows.Find(selectedLineId);

            //Now we need to map out the HRU columns to the Segment columns
            //Start by getting the mapping:
            SortedList<string, string> mapping = Segment.GetGenericMapping_SegmentstoHRU();

            //Now we cycle through the controls and find the tags that match the column names.
            //NOTE: NE_HRUData..PressureValueDisplay and NE_HRUData.TemperatureValueDisplay are considered the "seed"
            //  values for NE_Segments..thicknessCasePressureDisplay and NE_Segments..thicknessCaseTemperatureDisplay - 
            //  this means that if there isn't a value in NE_HRUData..thicknessCasePressureDisplay or 
            //  NE_Segments..thicknessCaseTemperatureDisplay, copy the value(s) out of the corresponding ValueDisplay
            //  column.
            foreach (Control ctrl in gbSegment.Controls)
            {
                try
                {
                    if ((ctrl.GetType() == typeof(TextBox)) && (!(string.IsNullOrEmpty(ctrl.Tag.ToString()))))
                    {
                        //Ask the mapping if it has one that matches the controls tag:
                        mapping.TryGetValue(ctrl.Tag.ToString(), out string colname);
                        //Break this apart so we can handle column/control combos that do not exist separately:
                        //Was the column name found in the mapping?
                        if (!(string.IsNullOrEmpty(colname)))
                        {
                            //Does the column exist in the DataTable?
                            if (dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Columns.Contains(ctrl.Tag.ToString()))
                            {
                                //Great! Set the value of the control!
                                ctrl.Text = dr[colname].ToString();
                            }
                            //Nope - it has a Tag, but the column doesn't exist in the DataTable. Must be something we aren't able to work with.
                            else ctrl.Text = "Disabled";
                        }
                    }
                    else if ((ctrl.GetType() == typeof(ComboBox)) && (!(string.IsNullOrEmpty(ctrl.Tag.ToString()))))
                    {
                        //Ask the mapping if it has one that matches the controls tag:
                        mapping.TryGetValue(ctrl.Tag.ToString(), out string colname);
                        //Break this apart so we can handle column/control combos that do not exist separately:
                        //Was the column name found in the mapping?
                        if (!(string.IsNullOrEmpty(colname)))
                        {
                            //Does the column exist in the DataTable?
                            if (dsPipeLine.Tables[Segment.SEGMENTTABLENAME].Columns.Contains(ctrl.Tag.ToString()))
                            {
                                //Great! Set the value of the control!
                                ComboBox cmbo = ctrl as ComboBox;
                                cmbo.SelectedValue = dr[colname].ToString();
                            }
                            //Nope - it has a Tag, but the column doesn't exist in the DataTable. Must be something we aren't able to work with.
                            else ctrl.Text = "Disabled";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    //something failed while assigning values??
                }
            }

            //If the txtThicknessCaseTemperature or txtThicknessCasePressure controls have no value, set it to that of the corresponding Pressure or Temperature control:
            //(but only if the controls are active!)
            if (MDCEnable)
            {
                if (string.IsNullOrEmpty(txtThicknessCaseTemperature.Text.ToString()) && !(string.IsNullOrEmpty(txtTemperature.Text.ToString())))
                {
                    if (txtTemperature.Text != "0")
                    {
                        txtThicknessCaseTemperature.Text = txtTemperature.Text;
                    }
                }
                if (string.IsNullOrEmpty(txtThicknessCasePressure.Text.ToString()) && !(string.IsNullOrEmpty(txtPressure.Text.ToString())))
                {
                    if (txtPressure.Text != "0")
                    {
                        txtThicknessCasePressure.Text = txtPressure.Text;
                    }
                }
            }
        }

        private void SetSegmentDataFromExcel(string selectedLineId)
        {
            //First, set the Excel and HRU values accordingly:
            txtExcelParentID.Text = selectedLineId;
            txtHRUParentID.Text = null;

            //Now we need to map out the Excel columns to the Segment columns
            DataRow dr = dsPipeLine.Tables[ExcelData.EXCELDATA_TABLENAME].Rows.Find(selectedLineId);
            cboANSIClass.Text = dr["ansi_class"].ToString();
            cboANSIClass.DataBindings[0].WriteValue();
            cboWallThickness.Text = dr["pipeThickness"].ToString();
            cboPipeMaterial.Text = dr["Material"].ToString();
            txtPressure.Text = dr["PressureValue"].ToString();
            txtTemperature.Text = dr["TemperatureValue"].ToString();
            cboDiameter.Text = dr["Size"].ToString();
            cboCodeJurisdiction.Text = dr["CodeJurisdiction"].ToString();

            //If the txtThicknessCaseTemperature or txtThicknessCasePressure controls have no value, set it to that of the corresponding Pressure or Temperature control:
            //(but only if the controls are active!)
            if (MDCEnable)
            {
                //If the incoming temperature has a value:
                if (string.IsNullOrEmpty(txtTemperature.Text) == false)
                {
                    //If the existing thickness temp is blank/empty/null, "default it" to the incoming temperature, unless incoming temperature is "0":
                    if (string.IsNullOrEmpty(txtThicknessCaseTemperature.Text) && txtTemperature.Text != "0")
                    {
                        txtThicknessCaseTemperature.Text = txtTemperature.Text;
                    }
                    //OR if the existing thickness temp is NOT blank/empty/null AND it is not equal to the incoming temperature, prompt the user if they want to take it over:
                    else if (string.IsNullOrEmpty(txtThicknessCaseTemperature.Text) == false && txtThicknessCaseTemperature.Text != txtTemperature.Text)
                    {
                        string message = $"Do you want to over write the existing Thickness Case Temperature ({txtThicknessCaseTemperature.Text}) with the incoming Temperature value ({txtTemperature.Text})?";
                        string caption = "Overwrite existing Thickness Case Temperature?";
                        MessageBoxButtons mbbuttons = MessageBoxButtons.YesNo;
                        DialogResult answer;
                        answer = MessageBox.Show(message, caption, mbbuttons);
                        if (answer == DialogResult.Yes)
                        {
                            txtThicknessCaseTemperature.Text = txtTemperature.Text;
                        }
                    }
                }


                //If the incoming pressure has a value:
                if (string.IsNullOrEmpty(txtPressure.Text) == false)
                {
                    //If the existing thickness pres is blank/empty/null, "default it" to the incoming pressure, unless incoming pressure is "0":
                    if (string.IsNullOrEmpty(txtThicknessCasePressure.Text) && txtPressure.Text != "0")
                    {
                        txtThicknessCasePressure.Text = txtPressure.Text;
                    }
                    //OR if the existing thickness pres is NOT blank/empty/null AND it is not equal to the incoming pressure, prompt the user if they want to take it over:
                    else if (string.IsNullOrEmpty(txtThicknessCasePressure.Text) == false && txtThicknessCasePressure.Text != txtPressure.Text)
                    {
                        string message = $"Do you want to over write the existing Thickness Case Pressure ({txtThicknessCasePressure.Text}) with the incoming Pressure value ({txtPressure.Text})?";
                        string caption = "Overwrite existing Thickness Case Pressure?";
                        MessageBoxButtons mbbuttons = MessageBoxButtons.YesNo;
                        DialogResult answer;
                        answer = MessageBox.Show(message, caption, mbbuttons);
                        if (answer == DialogResult.Yes)
                        {
                            txtThicknessCasePressure.Text = txtPressure.Text;
                        }
                    }
                }
                ////This section was replaced by the previous section:
                ////If the incoming temperature is not empty, null, or "0", AND the existing Thickness Temp is blank, set the
                ////  Thickness Temp to the incoming temperature value:
                //if (string.IsNullOrEmpty(txtThicknessCaseTemperature.Text.ToString()) && !(string.IsNullOrEmpty(txtTemperature.Text.ToString())))
                //{
                //    if (txtTemperature.Text != "0")
                //    {
                //        txtThicknessCaseTemperature.Text = txtTemperature.Text;
                //    }
                //}
                //if (string.IsNullOrEmpty(txtThicknessCasePressure.Text.ToString()) && !(string.IsNullOrEmpty(txtPressure.Text.ToString())))
                //{
                //    if (txtPressure.Text != "0")
                //    {
                //        txtThicknessCasePressure.Text = txtPressure.Text;
                //    }
                //}
            }
        }

        private void btnSaveSegmentChanges_Click(object sender, EventArgs e)
        {
            //Apply pending changes to the underlying datasource
            bsSegments.EndEdit();

            using (SqlConnection conn = new SqlConnection(sqlConnString))
            {
                daSegment.UpdateCommand.Connection = conn;
                daSegment.Update(dsPipeLine);
            }

            bsSegments.ResetBindings(false);

            SetFormControls(FormActions.SaveSegment);
        }

        private void btnCancelSegmentChanges_Click(object sender, EventArgs e)
        {
            bsSegments.CancelEdit();

            bsSegments.ResetBindings(false);

            SetFormControls(FormActions.CancelSegment);
        }

        private void btnDeletePipeline_Click(object sender, EventArgs e)
        {
            if (lstPipeLines.SelectedIndex > -1)
            {
                string selectedPipeLine = lstPipeLines.Text;
                //Verify with user that they want to delete this
                DialogResult result = MessageBox.Show($"THIS WILL DELETE ALL SEGMENTS RELATED TO {selectedPipeLine}. Do you want to continue?", "WARNING!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //Good to go. NOW KILL IT! KILL IT WITH FIRE!
                    DeletePipeline(selectedPipeLine);
                }
            }
            else MessageBox.Show("Nothing selected!");
        }

        private void btnEditSegment_Click(object sender, EventArgs e)
        {
            //If a segment is selected, enable it for editing
            if (lstSegments.SelectedIndex > -1)
            {
                SetFormControls(FormActions.EditSegment);
            }
            else MessageBox.Show("No segment selected.");
        }

        private void btnDeleteSegment_Click(object sender, EventArgs e)
        {
            //If a segment is selected, delete it.
            if (lstSegments.SelectedIndex > -1)
            {
                bool KeepOnTruckin = true;
                //If there is only one segment, deleting it will remove the Pipeline, as well (since they are technically the same thing).
                if (lstSegments.Items.Count == 1)
                {
                    DialogResult result = MessageBox.Show("This is the only segment for this line. Continuing will remove the PipeLine, also. Continue?", "Delete ENTIRE Pipeline", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        KeepOnTruckin = false;
                    }
                }

                if (KeepOnTruckin)
                {
                    DeleteSegment(lstSegments.Text);
                }
                SetFormControls(FormActions.DeleteSegment);
            }
            else MessageBox.Show("No segment selected.");
        }

        private void BtnNewSegment_Click(object sender, EventArgs e)
        {
            //Store the selected NELIneNumber for later:
            string NELineNumber = lstPipeLines.Text;
            AddNewSegment(NELineNumber);
            SetFormControls(FormActions.NewSegment);
        }

        private void BtnCreateNewPipeLine_Click(object sender, EventArgs e)
        {
            using (var form = new frmNewPipeline(dsPipeLine.Tables[Thermal.THERMALTABLENAMEP4D], dsPipeLine.Tables[ExcelData.EXCELDATA_TABLENAME]))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string newPipeline = form.PipeLineName;
                    string newPipelineDescription = form.PipeLineDescription;
                    string newPipelineSecondaryDescription = form.SecondaryLineDescription;
                    int newPipelineThermalLineId = form.ThermalLineId;
                    int newPipelineExcelID = form.ExcelLineId;

                    //Check if the PipeLine name is distinct
                    if (lstPipeLines.FindStringExact(newPipeline) != -1)
                    {
                        MessageBox.Show("Pipeline name already exists.");
                    }
                    else
                    {
                        //Pass the value to the method to add a new record.
                        AddNewPipeLine(newPipeline, newPipelineDescription, newPipelineSecondaryDescription, newPipelineThermalLineId, newPipelineExcelID);
                    }
                }
            }
        }

        private void btnViewThermalRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHRUParentID.Text.ToString() != "")
                {
                    using (var form = new frmGetThermalRecord(dsPipeLine.Tables[Thermal.THERMALTABLENAMEP4D], txtHRUParentID.Text.ToString()))
                    {
                        var result = form.ShowDialog();
                    }
                }
                else MessageBox.Show("No Thermal Line ID assigned.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading thermal form: {ex.Message}");
            }
        }

        private void btnGetExcelRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (dsPipeLine.Tables[ExcelData.EXCELDATA_TABLENAME].Rows.Count > 0)
                {
                    using (var form = new frmGetExcelRecord(dsPipeLine.Tables[ExcelData.EXCELDATA_TABLENAME], txtExcelParentID.Text.ToString()))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string selectedLineId = form.SelectedLineId.ToString();

                            //load remaining data from dataview based on the selected id.
                            //If the selected value is different from what was previously in the textbox,
                            //  warn user that data is about to be overwritten with Excel data.
                            if (txtHRUParentID.Text.ToString() != "")
                            {
                                MessageBox.Show("Changing Parent from HRU to Excel!");
                            }
                            if (selectedLineId != txtExcelParentID.Text.ToString() && txtExcelParentID.Text.ToString() != "")
                            {
                                DialogResult overwriteResult;
                                overwriteResult = MessageBox.Show("Selected parent has changed. Overwrite segment data with Excel data?", "Selected parent has changed", MessageBoxButtons.YesNoCancel);
                                if (overwriteResult == DialogResult.Yes)
                                {
                                    SetSegmentDataFromExcel(selectedLineId);
                                }
                                else if (overwriteResult == DialogResult.Cancel)
                                {
                                    //Cancel means put it back to what it was before the button was selected:
                                    txtExcelParentID.DataBindings[0].ReadValue();
                                }
                                else
                                {
                                    //if not Yes and not Cancel, then we just use the HRUParentID value without overwriting anything.
                                    //  Why? I'm not sure. But there ya go.
                                    txtExcelParentID.Text = selectedLineId;
                                }
                            }
                            else if (selectedLineId == txtExcelParentID.Text.ToString())
                            {
                                DialogResult overwriteResult;
                                overwriteResult = MessageBox.Show("Same Excel Line selected. Overwrite Segment data with Excel data?", "Refresh Excel data", MessageBoxButtons.YesNoCancel);
                                if (overwriteResult == DialogResult.Yes)
                                {
                                    SetSegmentDataFromExcel(selectedLineId);
                                }
                                else if (overwriteResult == DialogResult.Cancel)
                                {
                                    //Cancel means put it back to what it was before the button was selected:
                                    txtExcelParentID.DataBindings[0].ReadValue();
                                }
                                else
                                {
                                    //if not Yes and not Cancel, then we just use the HRUParentID value without overwriting anything.
                                    //  Why? I'm not sure. But there ya go.
                                    txtExcelParentID.Text = selectedLineId;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Changing Segment to match selected Excel data. This warning can be removed in the future...");
                                SetSegmentDataFromExcel(selectedLineId);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Excel records have been loaded for this project");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while attempting to load Excel record data: {ex.Message}");
            }
        }

        private void btnClearThermal_Click(object sender, EventArgs e)
        {
            if (txtHRUParentID.Text != "")
            {
                txtHRUParentID.Text = "";
            }
        }

        private void btnClearExcel_Click(object sender, EventArgs e)
        {
            if (txtExcelParentID.Text != "")
            {
                txtExcelParentID.Text = "";
            }
        }

        private void btnViewExcelRecord_Click(object sender, EventArgs e)
        {
            if (txtExcelParentID.Text.ToString() != "")
            {
                using (var form = new frmGetExcelRecord(dsPipeLine.Tables[ExcelData.EXCELDATA_TABLENAME], txtExcelParentID.Text.ToString()))
                {
                    var result = form.ShowDialog();
                }
            }
            else MessageBox.Show("No Excel Line ID assigned.");
        }

        private void txtExcelParentID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExcelParentID.Text.ToString()))
            {
                btnViewExcelRecord.Enabled = false;
            }
            else btnViewExcelRecord.Enabled = true;
        }

        private void txtHRUParentID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHRUParentID.Text.ToString()))
            {
                btnViewThermalRecord.Enabled = false;
            }
            else btnViewThermalRecord.Enabled = true;
        }
    }
}
