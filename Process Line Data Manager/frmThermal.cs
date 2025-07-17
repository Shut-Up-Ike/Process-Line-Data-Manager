using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using P4DHelperClass;
using LDMClassLibrary;

namespace Process_Line_Data_Manager
{
    public partial class frmThermal : Form
    {
        /// <summary>
        /// Point used to define DataGridView control locations within the TabControl
        /// </summary>
        static Point DGVStartPoint = new Point(6, 4);

        /// <summary>
        /// Size used to define the minimum size of the form
        /// </summary>
        static Size FormMinimumSize = new Size(600, 500);

        /// <summary>
        /// Adapter for NE_HRUData table in P4D project
        /// </summary>
        private SqlDataAdapter daP4DHRUData;

        /// <summary>
        /// DataSet to hold the data used within this form
        /// </summary>
        private DataSet dsThermal;

        /// <summary>
        /// Connection string for connecting to the project database
        /// </summary>
        private string sqlConnString;

        /// <summary>
        /// Path of the project directory. Expected to be in the format of "F:\[YEAR]\[PROJECTFOLDER]"
        /// </summary>
        private string directorypath;

        /// <summary>
        /// Constructor for the form. Should set the connection string for SQL and set up the DataAdapter.
        /// </summary>
        /// <param name="sqlconnectionstring">SQL Connection String for the project</param>
        public frmThermal(string sqlconnectionstring = "")
        {
            InitializeComponent();
            if (sqlconnectionstring == "")
            {
                sqlConnString = P4DHelperClass.Plant4D.GetProjectConnectionString(DataHolder.SelectedProject.DbName, DataHolder.SelectedServer.ServerType, DataHolder.SelectedServer.P4DType);
                directorypath = DataHolder.SelectedProject.Directory;
            }
            else sqlConnString = sqlconnectionstring;

            daP4DHRUData = Thermal.ThermalAdapterSetup(sqlConnString);
        }



        /// <summary>
        /// Reset form controls to initial state.
        /// </summary>
        private void resetForm()
        {
            dgvHRUData.DataSource = null;
            dgvP4DData.DataSource = null;
            toolStripStatusLabel1.Text = "Ready.";
            tabControl1.Enabled = false;
        }

        private void HRUForm_Load(object sender, EventArgs e)
        {
            btnImportToP4D.Enabled = false;
            btnMatchP4DtoHRU.Enabled = true;

            lblProject.Text = $"{DataHolder.SelectedProject.Name} on {DataHolder.SelectedServer.ServerName}";
            //Setup size restrictions for form
            this.MinimumSize = FormMinimumSize;

            resetForm();

            //DataGridView Location should be 6 pixels from the left side of the container; 4 from the top.
            dgvHRUData.Location = DGVStartPoint;
            dgvP4DData.Location = DGVStartPoint;
            dgvCombinedData.Location = DGVStartPoint;

            //DataGridView Size should be 20 less than the container width and 40 less than the container height.
            Size myDGVSize = new Size(tabControl1.Width - 20, tabControl1.Height - 40);
            dgvHRUData.Size = myDGVSize;
            dgvP4DData.Size = myDGVSize;
            dgvCombinedData.Size = myDGVSize;
            
            //Set up tabs
            tabPage1.Text = "Latest HRU Data";
            tabPage2.Text = "Existing P4D Data";
            tabPage4.Text = "Combined HRU and P4D Data";

            try
            {
                dsThermal = CreateDataSet();

                dgvHRUData.DataSource = dsThermal.Tables[Thermal.THERMALTABLENAMEHRU];
                dgvHRUData.ReadOnly = true;

                dgvP4DData.DataSource = dsThermal.Tables[Thermal.THERMALTABLENAMEP4D];
                dgvP4DData.ReadOnly = true;

                dgvCombinedData.ReadOnly = true;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Error loading data into the form: {ex.Message}";
            }

            tabControl1.Enabled = true;
        }


        /// <summary>
        /// Checks for, compares, and updates the decimal precision SETTINGS key for temperature and pressure
        /// </summary>
        private void CheckProjectPrecisionSettings()
        {
            int hrutemp;
            int settingstemp;
            int hrupres;
            int settingspres;
            try
            {
                //Get existing temp setting:
                try
                {
                    settingstemp = GetProjectPrecision("temperature");
                }
                catch (RowNotInTableException)
                {
                    //The setting doesn't exist. Set to -1 for later:
                    settingstemp = -1;
                }

                //Get existing pres setting:
                try
                {
                    settingspres = GetProjectPrecision("pressure");
                }
                catch (RowNotInTableException)
                {
                    //The setting doesn't exist. Set to -1 for later:
                    settingspres = -1;
                }

                //Retrieve the precision used in NE_HRUData:
                try
                {
                    hrutemp = GetNE_HRUDataPrecision("temperature");
                    hrupres = GetNE_HRUDataPrecision("pressure");
                }
                catch (Exception)
                {
                    //Well, THAT didn't work. Throw it.
                    throw;
                }

                //This does all the db updating work:
                ProjectDataAccess pda = new ProjectDataAccess(sqlConnString);

                //Compare the values, and if they are different, update the SETTINGS table:
                if (settingstemp != hrutemp)
                {
                    pda.SetSETTINGS_TemperaturePrecision(hrutemp);
                }
                if (settingspres != hrupres)
                {
                    pda.SetSETTINGS_PressurePrecision(hrupres);
                }
            }
            catch (Exception ex)
            {
                //Something else happened, probably not a good thing.
                MessageBox.Show($"Failed to set project precision settings: {ex.Message}");
            }
        }

        private int GetProjectPrecision(string temperature_or_pressure)
        {
            ProjectDataAccess pda = new ProjectDataAccess(sqlConnString);
            try
            {
                switch (temperature_or_pressure.ToLower())
                {
                    case "temperature":
                        return pda.GetSETTINGS_TemperaturePrecision();
                    case "pressure":
                        return pda.GetSETTINGS_PressurePrecision();
                    default:
                        throw new ArgumentException("Invalid option. Expecting 'temperature' or 'pressure'");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    throw new RowNotInTableException();
                }
                throw;
            }
        }
        private int GetNE_HRUDataPrecision(string temperature_or_pressure)
        {
            ProjectDataAccess pda = new ProjectDataAccess(sqlConnString);
            try
            {
                switch (temperature_or_pressure.ToLower())
                {
                    case "temperature":
                        return pda.GetNE_HRUData_TemperaturePrecision();
                    case "pressure":
                        return pda.GetNE_HRUData_PressurePrecision();
                    default:
                        throw new ArgumentException("Invalid option. Expecting 'temperature' or 'pressure'");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    throw new RowNotInTableException();
                }
                throw;
            }
        }


        /// <summary>
        /// Checks if P4D rows are unassigned to HRU rows, and if so, displays a form to allow the user create additional matches. 
        /// Returns true if matches have been made, false if they have not
        /// </summary>
        /// <returns>Returns true if matches have been made, false if they have not</returns>
        private bool ManuallyMatchHRUtoP4D()
        {
            try
            {
                bool returnme = false;
                //If any of the P4D rows have a value of 0 in line_no, then we need to allow - nay FORCE - the user to map them manually.
                //Start by getting the rows where line_no = 0:
                var rows = dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].AsEnumerable()
                    .Where(r => r.Field<Byte>("line_no") == 0);

                //If we have rows, we need to call the assignment form:
                if (rows.Count() > 0)
                {
                    DialogResult dialogResult;
                    dialogResult = MessageBox.Show("There are unmatched HRU<-->P4D records. You must manually match them or they will be marked as DELETED.", "Unmatched Records Found", MessageBoxButtons.OK);
                    if (dialogResult == DialogResult.OK)
                    {
                        try
                        {
                            using (var matchform = new frmMatchThermalRecord(dsThermal))
                            {
                                var result = matchform.ShowDialog();
                                //OK means user matched everything, IGNORE means user left some unmatched and those will be marked as deleted, but continue anyway:
                                if (result == DialogResult.OK || result == DialogResult.Ignore)
                                {
                                    DataTable updatedP4D = matchform.dsThermal.Tables[Thermal.THERMALTABLENAMEP4D];
                                    dsThermal.Tables.Remove(Thermal.THERMALTABLENAMEP4D);
                                    dsThermal.Tables.Add(updatedP4D);
                                    returnme = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading Match form: {ex.Message}");
                        }
                    }
                }
                //If we do NOT have rows, we need to return TRUE because everything is matched and all is right with the world.
                else
                {
                    returnme = true;
                }

                return returnme;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AttemptToMatchHRUtoP4D()
        {
            //This method needs to compare existing P4D data to incoming HRU data, and it is going to use the column "description" to do it.
            //We have some additional rules that have been provided by Andrew Franklin:
            //1. Some lines have the same description, and another field is needed to determine uniqueness
            //      - Drain lines should also include the quantity column
            //2. "Evaporator" lines have "Panel" in the description, as well as a set of numbers that can change between revisions
            //          Ex: IP Panel 1 DC Stub (o=2.875" , 73 mm)
            //              We would want to match on "IP Panel 1 DC Stub " in case the numbers have changed.
            try
            {
                //First, reset P4D table's "line_no" column to 0:
                var col = dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].Columns["line_no"];
                foreach (DataRow row in dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].Rows)
                {
                    row[col] = 0;
                }
                foreach (DataRow p4drow in dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].Rows)
                {
                    bool matched = false;
                    foreach (DataRow hrurow in dsThermal.Tables[Thermal.THERMALTABLENAMEHRU].Rows)
                    {
                        //Put the descriptions into lower cased strings for easier comparison work later:
                        string p4dDesc = p4drow["description"].ToString().ToLower();
                        string hruDesc = hrurow["description"].ToString().ToLower();
                        //Check for exact same description:
                        if (p4dDesc == hruDesc)
                        {
                            //Drains need to also consider the quantity, since they could have more than one row with matching descriptions
                            if (p4dDesc.Contains("drain"))
                            {
                                if (p4drow["qty"].ToString() == hrurow["qty"].ToString())
                                {
                                    //Good enough. Mark it as a match
                                    p4drow["line_no"] = hrurow["line_no"];
                                    matched = true;
                                }
                            }
                            else
                            {
                                p4drow["line_no"] = hrurow["line_no"];
                                matched = true;
                            }
                        }
                        //check for matching "stub" description with potentially different numbers:
                        else if (p4dDesc.Contains("stub") && hruDesc.Contains("stub"))
                        {
                            int first = p4dDesc.IndexOf("stub (o=");
                            int second = hruDesc.IndexOf("stub (o=");
                            if (first != -1 && second != -1)
                            {
                                if (p4dDesc.Substring(0, first) == hruDesc.Substring(0, second))
                                {
                                    p4drow["line_no"] = hrurow["line_no"];
                                    matched = true;
                                }
                            }
                        }

                        if (matched)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DataSet CreateDataSet()
        {
            DataSet ds = new DataSet("ThermalData");
            try
            {
                //New plan: 
                //Try to match on description, and if that doesn't work, then fall back to line_no.
                //1. Get new hru data
                //2. For each row in old P4D data, try to find a matching row in new hru data by searching for the description used in P4D
                //      - if a row is found in new hru data, map the rows to each other somehow, maybe a temp map table (should this be within the actual p4d table instead of an external map table?)
                //3. For p4d rows that were not matched, ask user to apply matches manually.
                //4. Once all the matches have been identified, compare the data as usual!

                //Here we go...
                //We should have already loaded the thermal data, so let's get a copy to work with:
                DataTable dtHRU = DataHolder.HRUData.Copy();
                //...Same for the P4D data:
                DataTable dtP4D = DataHolder.P4DData.Copy();
                
                ds.Tables.Add(dtHRU);
                ds.Tables.Add(dtP4D);

                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

        /// <summary>
        /// Handles click event for button btnImportToP4D. Calls CreateTableHRUData and WriteHRUtoP4D.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportToP4D_Click(object sender, EventArgs e)
        {
            //check if there is data in the datagridview
            if (dgvHRUData.Rows.Count > 0)
            {
                try
                {
                    ////Old method:
                    ////Merge the DataTables, then call the update method from the DataAdapter.
                    //dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].Merge(dsThermal.Tables[Thermal.THERMALTABLENAMECOMBINED]);
                    //daP4DHRUData.Update(dsThermal.Tables[Thermal.THERMALTABLENAMEP4D]);

                    //New method, based loosely on this: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-identity-or-autonumber-values#merging-new-identity-values
                    //Get changes to the datatable (only rows that are updated, inserted, or deleted)
                    DataTable datachanges = dsThermal.Tables[Thermal.THERMALTABLENAMECOMBINED].GetChanges();
                    //Add an event handler:
                    daP4DHRUData.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);
                    //Do the update:
                    daP4DHRUData.Update(datachanges);
                    //Merge the updates into the dataset:
                    dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].Merge(datachanges);

                    //Make a copy of the merged data:
                    DataTable copy = dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].GetChanges().Copy();

                    //Commit the changes:
                    dsThermal.Tables[Thermal.THERMALTABLENAMEP4D].AcceptChanges();

                    //Re-load the modified data back to DataHolder:
                    DataHolder.P4DData = dsThermal.Tables[Thermal.THERMALTABLENAMEP4D];

                    btnImportToP4D.Enabled = false;
                    toolStripStatusLabel1.Text = "Data imported to P4D successfully.";

                    try
                    {
                        //Send the results off to save out as a spreadsheet
                        //Path to save to:
                        string mypath = System.IO.Path.Combine(directorypath, "xls");
                        string myfilename = "NE_HRUData.xlsx";
                        SaveToExcelFile(System.IO.Path.Combine(mypath, myfilename), copy);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save results to Excel file: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                    toolStripStatusLabel1.Text = "Error.";
                }
            }
        }

        protected static void OnRowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            //if this is an insert, then skip this row.
            if (e.StatementType == StatementType.Insert)
            {
                e.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        private void frmThermal_FormClosed(object sender, FormClosedEventArgs e)
        {
            CheckProjectPrecisionSettings();
            var form = new frmMain();
            if (!(Application.OpenForms[form.Name] == null))
            {
                Application.OpenForms[form.Name].Show();
                Application.OpenForms[form.Name].Focus();
            }
        }

        private void dgvCombinedData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //Color the row based on the RowState of the the backing DataTable.
            foreach (DataGridViewRow row in dgvCombinedData.Rows)
            {
                DataRowView dr = row.DataBoundItem as DataRowView;
                if (!(dr is null))
                {
                    switch (dr.Row.RowState)
                    {
                        case DataRowState.Added:
                            row.DefaultCellStyle.BackColor = Color.LimeGreen;
                            break;
                        case DataRowState.Deleted:
                            //This SHOULDN'T be the case, but I guess it could happen...
                            row.DefaultCellStyle.BackColor = Color.Red;
                            break;
                        case DataRowState.Modified:
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Form.DefaultBackColor;
                            break;
                    }
                    //Since the rows SHOULDN'T be deleted, they will be kept and changed to have a description of "deleted" instead.
                    if(dr.Row["description"].ToString().ToLower() == "deleted")
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnMatchP4DtoHRU_Click(object sender, EventArgs e)
        {
            try
            {
                AttemptToMatchHRUtoP4D();

                if (ManuallyMatchHRUtoP4D())
                {
                    DataTable dt = Thermal.GetMergedTable(dsThermal.Tables[Thermal.THERMALTABLENAMEP4D], dsThermal.Tables[Thermal.THERMALTABLENAMEHRU]);
                    DataHolder.Differences = dt;
                    dsThermal.Tables.Add(dt);

                    dgvCombinedData.DataSource = dsThermal.Tables[Thermal.THERMALTABLENAMECOMBINED];
                    dgvCombinedData.ReadOnly = true;

                    btnImportToP4D.Enabled = true;
                    btnMatchP4DtoHRU.Enabled = false;
                    tabControl1.SelectedIndex = 2;
                    toolStripStatusLabel1.Text = "Ready to import data to P4D.";
                }
                else
                {
                    toolStripStatusLabel1.Text = "Matching aborted. Try again or exit.";
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Error loading data into the form: {ex.Message}";
            }
        }

        /// <summary>
        /// Extract the data from the given DataTable and save as an Excel file.
        /// </summary>
        /// <param name="excelFile">Path and name of excel file to save</param>
        /// <param name="table">DataTable containing the data to be exported to Excel</param>
        private void SaveToExcelFile(string excelFile, DataTable table)
        {
            try
            {
                ExportToExcel.CreateExcelFile.CreateExcelDocument(table, excelFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
