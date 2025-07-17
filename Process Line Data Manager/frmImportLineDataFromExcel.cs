using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using LDMClassLibrary;

namespace Process_Line_Data_Manager
{
    public partial class frmImportLineDataFromExcel : Form
    {
        //Default tab to try to import from:
        private const string defaultTabName = "Outputs";

        private readonly string sqlConnectionString;

        //DataSet to hold the data used in this form
        private DataSet dsExcelData;

        //Names of the tables that will exist within the DataSet
        private readonly string ImportedExcelTableName = "ToImportFromExcel";

        //DataAdapter to interact with SQL
        SqlDataAdapter daExcelData;

        public frmImportLineDataFromExcel(string sqlconnectionstring = "")
        {
            InitializeComponent();
            sqlConnectionString = sqlconnectionstring;
            dsExcelData = new DataSet("ExcelData");
            dsExcelData.Tables.Add(ExcelData.EXCELDATA_TABLENAME);
            daExcelData = ExcelData.ExcelSQLDataAdapterSetupWithCustomINSERT(sqlConnectionString);
            daExcelData.FillSchema(dsExcelData, SchemaType.Source, ExcelData.EXCELDATA_TABLENAME);
            daExcelData.Fill(dsExcelData);
            dgvExisting.DataSource = dsExcelData.Tables[ExcelData.EXCELDATA_TABLENAME];
            dgvExisting.Columns["ID"].Visible = false;
        }

        private void btnBrowseToFile_Click(object sender, EventArgs e)
        {
            //Ask user what file to open and store it
            string filePath;
            filePath = GetFileToOpen();

            //If a path was selected, continue
            if (!string.IsNullOrEmpty(filePath))
            {
                lblSelectedFile.Text = filePath;
                try
                {
                    //Test(filePath);
                    LoadAndCompareExcelData(filePath);
                    ExcelDataCountLabel.Text = $"(Count: {dgvImporting.Rows.Count})";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data from spreadsheet: {ex.Message}");
                }
            }
            else MessageBox.Show("No file path found to open");
        }

        private void LoadAndCompareExcelData(string filePath)
        {
            //PLOT TWIST!! Excel has been changed to store sizes the same way that P4D does! So now Size isn't a float but a string!
            //What are we gonna do to work with that? Preliminary thoughts:
            //1. Change the datatype of the Size column in excelDataTable to string, and re-populate the data 
            //      later (after import) with a copy of NE_Diameter table as a translation...

            //We will need a number of DataTables for this functionality:
            //1. A copy (with data and schema) of the original DataTable from SQL's NE_ExcelData, but removing 
            //      column ID and the primary key - this will get compared to after we import the data from excel
            //2. A clone (schema only) of the DataTable from (1) (with constraints removed to avoid errors during
            //      import) - this will store the data that gets imported from excel
            //3. A clone (schema only) of (2) - this will clear out any duplicates that may exist in the imported 
            //      excel data itself
            //4. The final DataTable that will hold the data to be imported into SQL - this will be clean of any
            //      duplicates that existed in the spreadsheet, as well as any that already exist in SQL.

            //1. Create a copy of the SQL DataTable and remove the PK and ID column. We will use this to compare against later...
            DataTable sqlTableToCompareTo = dsExcelData.Tables[ExcelData.EXCELDATA_TABLENAME].Copy();
            sqlTableToCompareTo.PrimaryKey = null;
            sqlTableToCompareTo.Columns.Remove("ID");

            //2. Load the Excel data into a clone of the previously created DataTable sans constraints:
            DataTable excelDataTable = sqlTableToCompareTo.Clone();
            excelDataTable.Constraints.Clear();
            excelDataTable = GetTabAsDataTable(excelDataTable, filePath, defaultTabName);


            //TODO: We need to filter out any "problem" data, ie. "#Value!" "#NAME?" etc.


            //Now that we have the Excel data, we should select distinct to make sure there aren't any duplicates.
            //  This is done by passing the column names that are to be compared (all of them in this case) to the
            //  .DefaultView.ToTable method of the table that has the data to be filtered.
            string[] myParams = new string[excelDataTable.Columns.Count];
            for (int i = 0; i < excelDataTable.Columns.Count; i++)
            {
                myParams[i] = excelDataTable.Columns[i].ColumnName.ToString();
            }
            //3. "Distinct" the imported data to remove any possible duplicates
            DataTable excelDataTable_DistinctRows = excelDataTable.DefaultView.ToTable(true, myParams);
            if (excelDataTable.Rows.Count != excelDataTable_DistinctRows.Rows.Count)
            {
                //Tell the user that something was removed as a duplicate.
                //TODO: get more specific on what was ignored, if the user would like to know...
                MessageBox.Show($"Excel has duplicate rows that are being ignored.");
            }

            //Compare SQL datatable to Excel datatable, store differences in new table called ExcelDataTable_Cleaned:
            DataTable excelDataTable_Cleaned = DataFunctions.GetRowsNotInFirstTable(sqlTableToCompareTo, excelDataTable_DistinctRows);
            excelDataTable_Cleaned.TableName = ImportedExcelTableName;

            //Add differences table to the dataset, but check if it exists first:
            if (dsExcelData.Tables.Contains(ImportedExcelTableName))
            {
                dsExcelData.Tables.Remove(ImportedExcelTableName);
            }
            dsExcelData.Tables.Add(excelDataTable_Cleaned);
            
            //Set the DataGridView object to look at this for its DataSource:
            dgvImporting.DataSource = dsExcelData.Tables[ImportedExcelTableName];

            //If we have some rows left, enable the button!
            if (dsExcelData.Tables[ImportedExcelTableName].Rows.Count > 0)
            {
                btnImportExcel.Enabled = true;
            }
            else MessageBox.Show("No records found to import.");
        }

        private string GetFileToOpen()
        {
            string filePath = string.Empty;
            string fileDirectory;
            switch(Properties.Settings.Default.LastExcelDirectoryPath)
            {
                case "":
                    fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;
                default:
                    fileDirectory = Properties.Settings.Default.LastExcelDirectoryPath;
                    break;
            }
            OpenFileDialog openFiledlg = new OpenFileDialog
            {
                Title = "Select Excel File to Import...",
                InitialDirectory = fileDirectory,

                //Add additional Excel file extensions to list using semicolon [;] as separator:
                Filter = "Excel Sheet(*.xls,*.xlsm,*.xlsx)|*.xls;*.xlsm;*.xlsx|All Files(*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFiledlg.ShowDialog() == DialogResult.OK)
            {
                filePath = openFiledlg.FileName;
                //Save the path for future reference:
                fileDirectory = System.IO.Path.GetDirectoryName(filePath);
                Properties.Settings.Default.LastExcelDirectoryPath = fileDirectory;
                Properties.Settings.Default.Save();
            }


            openFiledlg.Dispose();

            return filePath;
        }

        private DataTable GetTabAsDataTable(DataTable baseTableWithSchema, string filepathToOpen, string tabNameToRetrieve)
        {
            //Build the datatable that we want to fill. Should match the format of the SQL table, so clone and rename it.
            DataTable returnMe = baseTableWithSchema.Clone();
            //DataTable returnMe = new DataTable();
            returnMe.TableName = "ExcelToImport";

            //Get our connectionstring:
            OleDbConnectionStringBuilder connBuilder = DataFunctions.GetACEConnectionString(filepathToOpen, true);

            //Use the connectionstring to create and use a connection
            using (OleDbConnection conn = new OleDbConnection(connBuilder.ConnectionString))
            {
                try
                {
                    conn.Open();
                    OleDbDataAdapter objDA = new OleDbDataAdapter($"SELECT * FROM [{tabNameToRetrieve}$] WHERE DESCRIPTION IS NOT NULL AND DESCRIPTION <> ''", conn);
                    objDA.Fill(returnMe);
                    objDA.Dispose();
                }
                catch(OleDbException ex)
                {
                    MessageBox.Show($"Problem loading tab from spreadsheet. Make sure tab {tabNameToRetrieve} exists. Full Error:{ ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"General issue loading tab {tabNameToRetrieve} from spreadsheet {filepathToOpen}: {ex.Message}");
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            return returnMe;
        }

        private void Test(string filepathToOpen)
        {
            //Get our connectionstring:
            OleDbConnectionStringBuilder connBuilder = DataFunctions.GetACEConnectionString(filepathToOpen, true);
            LDMClassLibrary.ExcelDataAccess eda = new LDMClassLibrary.ExcelDataAccess();
            //Query excel for the tab data:
            List<LDMClassLibrary.ExcelData> exceldata = eda.GetExcelData(connBuilder.ConnectionString);
            //The data was just read from Excel, but the decimal precision has been lost.

            //Query p4d for the db data:
            LDMClassLibrary.ProjectDataAccess pda = new ProjectDataAccess(sqlConnectionString);
            List<LDMClassLibrary.NE_ExcelData> p4ddata = pda.GetNE_ExcelData();
            //Re-implement the precision for the strings using ProjectDataAccess:
            int tempPrecision = pda.GetSETTINGS_TemperaturePrecision();
            int presPrecision = pda.GetSETTINGS_PressurePrecision();

            //We have both sets of data, now we need to compare them and make a composite.
            //p4ddata will always prevail, so we need to compare the "new" exceldata items
            //  to p4ddata, and anything that isn't in p4ddata needs to get added.
            var resultSet = exceldata.Except(p4ddata).ToList();
            MessageBox.Show($"resultSet has {resultSet.Count} rows.");

            var resultSet2 = p4ddata.Except(exceldata).ToList();
            MessageBox.Show($"resultSet2 has {resultSet2.Count} rows.");


            //Just a little something to keep the method active while I look at it in Debug...
            int count = exceldata.Count;
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ImportExcelToSQL();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing data: {ex.Message}");
            }
        }

        private void ImportExcelToSQL()
        {
            if (dsExcelData.Tables.Contains(ImportedExcelTableName))
            {
                try
                {
                    foreach (DataRow row in dsExcelData.Tables[ImportedExcelTableName].Rows)
                    {
                        //Add a new empty row to the Existing DataTable:
                        DataRow newRow = dsExcelData.Tables[ExcelData.EXCELDATA_TABLENAME].NewRow();
                        //Cycle through all the columns from the Excel DataTable that match the Existing DataTable columns and set the values to be the same:
                        foreach (DataColumn col in row.Table.Columns)
                        {
                            newRow[col.ColumnName] = row[col.ColumnName];
                        }
                        dsExcelData.Tables[ExcelData.EXCELDATA_TABLENAME].Rows.Add(newRow);
                    }

                    //Call the DataAdapter to perform the updates.
                    daExcelData.Update(dsExcelData, ExcelData.EXCELDATA_TABLENAME);

                    //Refresh all the form stuff so it's pretty.
                    dsExcelData.Tables[ImportedExcelTableName].Clear();
                    dgvImporting.Refresh();
                    dgvExisting.Refresh();

                    MessageBox.Show("Data imported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing data to SQL: {ex.Message}");
                }
            }
            else
            {
                throw new Exception("Could not find data to import.");
            }
        }

        private void frmImportLineDataFromExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = new frmMain();
            if (!(Application.OpenForms[form.Name] == null))
            {
                Application.OpenForms[form.Name].Show();
                Application.OpenForms[form.Name].Focus();
            }
        }

        private void frmImportLineDataFromExcel_Load(object sender, EventArgs e)
        {
            NE_ExcelDataCountLabel.Text = $"(Count: {dgvExisting.Rows.Count})";
            ExcelDataCountLabel.Text = "";
        }
    }
}
