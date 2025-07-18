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
    public partial class frmMain : Form
    {
        //Size used to define the minimum size of the form
        private Size FormMinimumSize = new Size(489, 224);

        //Holds the connection string to the currently selected project
        private string sqlConnString;


        public frmMain()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Runs when form loads. Sets form properties; Populates database combo box; Adds projects based on the first(selected) database server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Setup size restrictions for form
            this.MinimumSize = FormMinimumSize;

            //Set checkboxes to not allow user interaction
            chkHRUData.AutoCheck = false;
            chkIsComparable.AutoCheck = false;
            chkP4DData.AutoCheck = false;
            chkNewHRUData.AutoCheck = false;

            //clear combo box and labels
            cboServer.Items.Clear();
            toolStripStatusLabel1.Text = "";
            resetForm();

            //Build server list
            List<P4DHelperClass.Plant4DServer> p4dservers = P4DHelperClass.Plant4D.GetP4DServers("Rome");

            //Load servers into combo
            BindingSource bs = new BindingSource
            {
                DataSource = p4dservers
            };
            cboServer.DisplayMember = "Description";
            cboServer.ValueMember = "ServerName";
            cboServer.DataSource = bs.DataSource;
        }



        /// <summary>
        /// Fires when server combo box value is changed. Loads projects for selected server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If server selection changes, reset the form.
            resetForm();

            //Set the selected server in DataHolder
            DataHolder.SelectedServer = (P4DHelperClass.Plant4DServer)cboServer.SelectedItem;

            //Call method to load the projects for the selected server
            LoadProjectsFromServer();
        }



        /// <summary>
        /// Gets projects from specified server.
        /// </summary>
        private void LoadProjectsFromServer()
        {
            //Get the server selected in cboServer
            P4DHelperClass.Plant4DServer server = DataHolder.SelectedServer;

            //datatable to hold the stuff
            DataTable projects = new DataTable("Projects");
            projects.Columns.Add("PROJECT", typeof(string));
            projects.Columns.Add("DBNAME", typeof(string));
            projects.Columns.Add("DBLocation", typeof(string));

            //Get the value to use as the tool filter:
            string version = Process_Line_Data_Manager.Properties.Resources.PCE_TOOLS_NAME.ToString();

            //SQL portion to query db for projects. Uses new table PROJECT_TOOL_RIGHTS to filter for projects that support the current version of LDM.
            string selectSQL = @"SELECT DESCRIPTION AS [NAME], DBNAME, REPLACE(DBLOCATION,'.NE.LOC','') AS [SERVERNAME], DB_YR AS [YEAR], REPLACE(DIRECTORY,'%PROJDIR%','F:') AS [DIRECTORY] " +
                $"FROM DATABASES D INNER JOIN PROJECT_TOOL_RIGHTS R ON D.DATABASEID = R.DATABASEID INNER JOIN TOOLS T ON R.TOOLID = T.TOOLID AND T.TOOLNAME = '{version}'" +
                "WHERE DBGROUP = 'PROJECT' and STATUS = 1 " +
                "ORDER BY DB_YR DESC, DBNAME";
            using (var con = P4DHelperClass.Plant4D.GetPCEConnection(server.ServerType, server.P4DType))
            {
                using (var cmd = new SqlDataAdapter(selectSQL, con))
                {
                    //Throw results into datatable
                    cmd.Fill(projects);
                }
            }

            if (projects.Rows.Count > 0)
            {
                //Set DataHolder's object to these results
                DataHolder.PCEProjects = projects;
                DataHolder.LoadP4DProjectList();

                //Bind results to the projects combo box
                BindingSource bs = new BindingSource();
                bs.DataSource = DataHolder.PCEProjects;
                cboProject.DisplayMember = "NAME";
                cboProject.ValueMember = "DBNAME";
                cboProject.DataSource = bs;

                DataHolder.AssignP4DProject(cboProject.SelectedValue.ToString());
                DataHolder.ProjectServer = GetProjectServer();

                this.btnLoadProject.Enabled = true;
            }
            else
            {
                MessageBox.Show("No projects were found on this server that are compatible with this version of LDM.");
                this.btnLoadProject.Enabled = false;
            }
        }



        /// <summary>
        /// Handles Project dropdown changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When the selected project changes, we need to do a few things:
            //1a. reset all the project-related DataHolder items to empty
            //1b. reset the form's controls.
            //2a. get the newly selected project
            //2b. reassign all the project-related DataHolder items to the new project

            //1a.
            DataHolder.ResetP4DProject();
            //1b.
            resetForm();

            //2a.
            //Store project in DataHolder
            DataHolder.AssignP4DProject(cboProject.SelectedValue.ToString());

            //2b.
            //Call method to find the project server, then assign to DataHolder.
            DataHolder.ProjectServer = GetProjectServer();
        }



        /// <summary>
        /// Queries a P4D project's Settings table and returns a single value. Default
        /// </summary>
        /// <param name="option">"MainJobnumber" returns 6-digit job number; "Units" returns "IMPERIAL" or "METRIC".</param>
        /// <returns></returns>
        private string GetP4DProjectSetting(string option = "")
        {
            string result = "";
            if (option != "")
            {
                string selectSQL = "SELECT TOP 1 [VALUE] FROM SETTINGS ";

                switch (option.ToLower())
                {
                    case "mainnumber":
                        selectSQL += "WHERE [ROOT]='PROJECT' AND [SECTION]='SETUP' AND [KEYNAME]='PROJNUMBER-NE'";
                        break;
                    case "units":
                        selectSQL += "WHERE [ROOT]='PROJECT' AND [SECTION]='UNIT' AND [KEYNAME]='SYSTEM'";
                        break;
                    default:
                        break;
                }

                //Connect and run the query
                using (SqlConnection con = new SqlConnection(sqlConnString))
                {
                    using (SqlCommand cmd = new SqlCommand(selectSQL, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    result = reader["VALUE"].ToString();
                                }
                            }
                        }
                        con.Close();
                    }
                }
            }
            return result;
        }



        /// <summary>
        /// Queries HRU database for data related to the passed-in P4D project.
        /// </summary>
        /// <param name="P4DProjNumber">6 digit P4D project number</param>
        /// <param name="P4DProjUnits">"IMPERIAL" or "METRIC"</param>
        private void LoadHRUData(string P4DProjNumber = "", string P4DProjUnits = "")
        {
            //reset checkbox and datagridview untiL we know what we have
            chkHRUData.Checked = false;

            DataTable holder = new DataTable();

            try
            {
                //Get HRU data from the DataAdapter:
                holder = Thermal.GetHRUData_Cleaned(P4DProjNumber, P4DProjUnits, sqlConnString);

                //Cleanse the data, but more so than the DataAdapter did. HRU uses different values for Schedules than P4D does, so we need to clean it up a little further.
                //1. Get Schedule "mapping" table from project and global spec db's
                string qry = "select s.ScheduleDesc as [P4DSchedule], coalesce(a.AliasText, s.ScheduleDesc) as [AliasSchedule] " +
                                "from ne_schedule s " +
                                "left outer join P4Dr_NE_GLOBAL_SPEC..NE_Schedule_Alias a on s.ScheduleCode = a.ScheduleCode";
                DataTable scheduleMapping = FillDataTableFromProject(qry);
                //2. Update HRU table ("holder") with P4D's schedule values by joining to the mapping table via linq and SetField:
                holder.AsEnumerable().Join(scheduleMapping.AsEnumerable(), tblhru => tblhru["PipeThicknessDisplay"],
                                                                            tblsch => tblsch["AliasSchedule"],
                                                                            (tblhru, tblsch) => new { tblhru, tblsch })
                    .ToList().ForEach(o => o.tblhru.SetField("PipeThicknessDisplay", o.tblsch["P4DSchedule"].ToString()));

                //The DataTable will be marked as "modified", accept the changes now:
                holder.AcceptChanges();

                if (holder.Rows.Count > 0)
                {
                    //Check the appropriate box on the form
                    chkHRUData.Checked = true;
                }
                else
                {
                    toolStripStatusLabel1.Text = "No HRU data was found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Store the DataTable so we can use it later:
                DataHolder.HRUData = holder;
            }
        }



        /// <summary>
        /// /Checks if table exists within selected P4D project database.
        /// Refactored 4/2/2019 to accept a string tablename argument to look for.
        /// </summary>
        /// <param name="tablename">Name of table to search for.</param>
        /// <returns>TRUE if table exists; FALSE if table does not exist.</returns>
        private bool DoesTableExistInP4DProject(string tablename, string dbname, string servertype, string p4dtype)
        {
            bool exists = false;

            string selectSQL = @"IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = @DbName AND TABLE_NAME = @TableName) SELECT 1 ELSE SELECT 0";

            using (var con = P4DHelperClass.Plant4D.GetProjectConnection(dbname, servertype, p4dtype))
            {
                using (SqlCommand cmd = new SqlCommand(selectSQL, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@TableName", tablename));
                    cmd.Parameters.Add(new SqlParameter("@DbName", dbname));

                    try
                    {
                        con.Open();
                        exists = (int)cmd.ExecuteScalar() == 1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while trying to check if table {tablename} exists in {dbname}:" + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return exists;
        }



        /// <summary>
        /// Loads existing P4D data from NE_HRUData into DataHolder.P4DData.
        /// </summary>
        private void LoadP4DHRUData()
        {
            //uncheck P4D checkbox until we know what we have, reset datagridview
            chkP4DData.Checked = false;

            try
            {
                DataHolder.P4DData = Thermal.GetP4DHRUTable(sqlConnString);
                chkP4DData.Checked = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable FillDataTableFromProject(string selectSQL)
        {
            DataTable newDT = new DataTable();
            try
            {
                using (var con = P4DHelperClass.Plant4D.GetProjectConnection(DataHolder.SelectedProject.DbName, DataHolder.SelectedServer.ServerType, DataHolder.SelectedServer.P4DType))
                {
                    using (var cmd = new SqlDataAdapter(selectSQL, con))
                    {
                        using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(cmd))
                        {
                            cmd.Fill(newDT);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return newDT;
        }



        /// <summary>
        /// Checks to make sure criteria has been met to compare HRU and P4D data, then calls LoadDifferences.
        /// </summary>
        private void CompareHRUandP4DData()
        {

            //Only run this if all of the criteria have been met...check by checking status of check boxes. Check check check.
            if ((chkHRUData.Checked == true) && (chkP4DData.Checked == true) && (chkIsComparable.Checked == true))
            {
                try
                {
                    //reset the checkboxes to our current state:
                    chkNewHRUData.Checked = false;
                    chkIsComparable.Checked = false;
                    btnViewHRU.Enabled = false;

                    //Test is data is actually comparable:
                    bool isComparable = false;
                    try
                    {
                        //This will throw a descriptive custom error if something is out of whack. Pass it back to the caller.
                        isComparable = Thermal.IsComparible_HRUtoP4D(DataHolder.HRUData, DataHolder.P4DData);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    if (isComparable)
                    {
                        chkIsComparable.Checked = true;
                        //...for now, let's just compare the revision_no column value. If they differ, the data has been changed and we can compare more in-depth later:
                        if (DataHolder.HRUData.Rows.Count > 0 && DataHolder.P4DData.Rows.Count > 0)
                        {
                            if (DataHolder.HRUData.Rows[0]["revision_no"].ToString() != DataHolder.P4DData.Rows[0]["revision_no"].ToString())
                            {
                                chkNewHRUData.Checked = true;
                                btnViewHRU.Enabled = true;
                            }
                        }
                        else if (DataHolder.HRUData.Rows.Count > 0 && DataHolder.P4DData.Rows.Count == 0)
                        {
                            //No data is in P4D, so this could be the first time data is imported. Mark that there is HRU data, and enable the button:
                            chkHRUData.Checked = true;
                            btnViewHRU.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        private P4DHelperClass.Plant4DServer GetProjectServer()
        {
            //We need the selected project, and then to get the server that the project exists on
            //Start with a list of servers:
            List<P4DHelperClass.Plant4DServer> servers = P4DHelperClass.Plant4D.GetP4DServers();
            //Next, find the server with the name stored in DataHolder.SelectedProject:
            var result = servers.Find(delegate (P4DHelperClass.Plant4DServer s) { return s.ServerName.ToUpper() == DataHolder.SelectedProject.ServerName.ToUpper(); });
            //Assign the result to a new instance of ServerInstance:
            P4DHelperClass.Plant4DServer server = new P4DHelperClass.Plant4DServer();
            server = result;
            return server;
        }








        //Here come the "interactive" things - methods that are called by the user via the form...

        /// <summary>
        /// /// Gets project number and units from selected project and attempts to load HRU and P4D data, and make comparisons if possible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadProject_Click(object sender, EventArgs e)
        {
            //Create connection string for use with other methods
            sqlConnString = P4DHelperClass.Plant4D.GetProjectConnectionString(DataHolder.SelectedProject.DbName, DataHolder.SelectedServer.ServerType, DataHolder.SelectedServer.P4DType);

            //Reset form controls
            resetForm();

            //Find project job number
            string P4DProjectNumber = GetP4DProjectSetting("MainNumber");

            //Find project units
            string P4DProjectUnit = GetP4DProjectSetting("Units");

            //Sanitize our data first. The method expects "english" or "metric" but P4D stores "Imperial" or "Metric"
            switch (P4DProjectUnit.ToLower())
            {
                case "imperial":
                    P4DProjectUnit = "english";
                    break;
            }

            //Find P4D project HRU data and load into form. Also handles chkP4DData.
            try
            {
                LoadP4DHRUData();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Error loading project Thermal data. Table may not exist. Contact CAE group. Exception details: {sqlEx.Message}");
                toolStripStatusLabel1.Text = "Error loading project Thermal data: SqlException.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading local Thermal data: {ex.Message}");
                toolStripStatusLabel1.Text = "General error loading local Thermal data.";
            }

            //Find matching HRU data and load into form. Also handles chkHRUData.
            try
            {
                LoadHRUData(P4DProjectNumber, P4DProjectUnit);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147024809)
                {
                    MessageBox.Show($"Could not contact HRU database. \n\n***HRU functionality will be disabled***");
                }
                else
                {
                    MessageBox.Show($"Error loading data from HRU database. \n\n***HRU functionality will be disabled*** \nError Message: {ex.Message}");
                }
                toolStripStatusLabel1.Text = "Error loading data from HRU database. HRU functionality disabled.";
            }

            //Test if data is (basically) comparible 
            try
            {
                if (DataHolder.HRUData.Columns.Count == (DataHolder.P4DData.Columns.Count - 1))
                {
                    chkIsComparable.Checked = true;
                    CompareHRUandP4DData();
                }
                else toolStripStatusLabel1.Text = "HRU and P4D data is incompatible. Contact CAE.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error comparing HRU data and P4D data: {ex.Message}");
                toolStripStatusLabel1.Text = "Error comparing HRU and P4D data.";
            }

            //Cases when we do not want to proceed:
            //1. NE_HRUData table does not have column "ID". This will break HRU/Thermal functionality and should not allow the user to continue.
            if (chkHRUData.Checked && chkP4DData.Checked && chkIsComparable.Checked && toolStripStatusLabel1.Text == "Ready.")
            {
                btnProcessLines.Enabled = true;
                btnImportFromExcel.Enabled = true;
                toolStripStatusLabel1.Text = "Project loaded.";
            }
            else if (chkHRUData.Checked == false && chkP4DData.Checked && chkIsComparable.Checked && toolStripStatusLabel1.Text == "No HRU data was found")
            {
                btnProcessLines.Enabled = true;
                btnImportFromExcel.Enabled = true;
                toolStripStatusLabel1.Text = "Project loaded.";
            }
            else if (chkHRUData.Checked == false && chkP4DData.Checked == true)
            {
                btnProcessLines.Enabled = true;
                btnImportFromExcel.Enabled = true;
                btnViewHRU.Enabled = false;
                toolStripStatusLabel1.Text = "Project loaded; HRU is disabled.";
            }
            else
            {
                MessageBox.Show("There is a problem with this project. Unable to continue. Contact CAE.");
            }
        }

        private void btnViewHRU_Click(object sender, EventArgs e)
        {
            try
            {
                frmThermal myForm = new frmThermal();
                myForm.Show();
                resetForm();
                toolStripStatusLabel1.Text = "Reload project to continue.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading HRU form: {ex.Message}");
            }
        }

        //...End "interactive" section








        //Here come the "pretty" things - methods that keep the form looking nice...

        /// <summary>
        /// Reset form controls to initial state.
        /// </summary>
        private void resetForm()
        {
            chkHRUData.Checked = false;
            chkP4DData.Checked = false;
            chkIsComparable.Checked = false;
            chkNewHRUData.Checked = false;
            toolStripStatusLabel1.Text = "Ready.";
            btnViewHRU.Enabled = false;
            btnProcessLines.Enabled = false;
            btnImportFromExcel.Enabled = false;
        }

        private void chkHRUData_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHRUData.Checked && chkIsComparable.Checked && chkNewHRUData.Checked && chkP4DData.Checked)
                btnViewHRU.Enabled = true;
            else btnViewHRU.Enabled = false;
        }


        private void BtnProcessLines_Click(object sender, EventArgs e)
        {
            //Call this function to load any changes that may have been made to the data.
            LoadP4DHRUData();

            this.Hide();
            frmProcessLines form = new frmProcessLines(DataHolder.SelectedServer, DataHolder.SelectedProject);
            form.Show();
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmImportLineDataFromExcel form = new frmImportLineDataFromExcel(sqlConnString);
            form.Show();
        }

        //...End "pretty" things section
    }
}
