using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Process_Line_Data_Manager
{
    class DataFunctions
    {
        public DataFunctions() { }

        [Obsolete]
        /// <summary>
        /// Compares two DataTables with the same format and returns a new DataTable that has all the rows that are different between the original two DataTables.
        /// </summary>
        /// <param name="firstTable"></param>
        /// <param name="secondTable"></param>
        /// <returns>DataTable with rows that are different between the passed in DataTables</returns>
        public static DataTable GetDifferences(DataTable firstTable, DataTable secondTable)
        {
            //"borrowed" from here: 
            //http://canlu.blogspot.com/2009/05/how-to-compare-two-datatables-in-adonet.html

            //Create empty table:
            DataTable dt = new DataTable("ResultDataTable");

            try
            {
                if(IsComparible(firstTable, secondTable))
                {
                    //Use a Dataset to make use of a DataRelation object
                    using (DataSet ds = new DataSet())
                    {
                        //Add tables
                        ds.Tables.AddRange(new DataTable[] { firstTable.Copy(), secondTable.Copy() });

                        //Get Columns for DataRelation
                        //Create a DataColumn array...
                        DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                        //...and then populate the array with the columns from the Source
                        for (int i = 0; i < firstColumns.Length; i++)
                        {
                            firstColumns[i] = ds.Tables[0].Columns[i];
                        }

                        //Repeat for Comparison
                        //Create a DataColumn array...
                        DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                        //...and then populate it with the columns from the Comparison
                        for (int i = 0; i < secondColumns.Length; i++)
                        {
                            secondColumns[i] = ds.Tables[1].Columns[i];
                        }

                        //Create DataRelation by feeding in the DataColumns we just created
                        //This one relates secondTable as a child to firstTable:
                        DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                        ds.Relations.Add(r1);

                        //Create DataRelation by feeding in the DataColumns we just created:
                        //This one relates firstTable as a child to secondTable:
                        DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                        ds.Relations.Add(r2);

                        //Create columns for return table based on our copied firstTable.
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            dt.Columns.Add(ds.Tables[0].Columns[i].ColumnName, ds.Tables[0].Columns[i].DataType);
                        }

                        //If firstTable Row not in secondTable, add to result table
                        dt.BeginLoadData();
                        foreach (DataRow parentrow in ds.Tables[0].Rows)
                        {
                            DataRow[] childrows = parentrow.GetChildRows(r1);
                            if (childrows == null || childrows.Length == 0)
                            {
                                dt.LoadDataRow(parentrow.ItemArray, true);
                            }
                        }

                        //If secondTable Row not in firstTable, Add to result table
                        foreach (DataRow parentrow in ds.Tables[1].Rows)
                        {
                            DataRow[] childrows = parentrow.GetChildRows(r2);
                            if (childrows == null || childrows.Length == 0)
                            {
                                dt.LoadDataRow(parentrow.ItemArray, true);
                            }
                        }
                        dt.EndLoadData();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            

            return dt;
        }


        /// <summary>
        /// Returns a DataTable that has the DataRows that exist in secondTable but not in firstTable.
        /// </summary>
        /// <param name="firstTable"></param>
        /// <param name="secondTable"></param>
        /// <returns>DataTable object with DataRows of secondTable that are not in firstTable</returns>
        public static DataTable GetRowsNotInFirstTable(DataTable firstTable, DataTable secondTable)
        {
            DataTable dtReturnMe = new DataTable("ResultDataTable");
            try
            {
                if(IsComparible(firstTable, secondTable))
                {

                    //Use a Dataset to make use of a DataRelation object
                    using (DataSet ds = new DataSet())
                    {
                        //Add tables
                        ds.Tables.AddRange(new DataTable[] { firstTable.Copy(), secondTable.Copy() });

                        //Get Columns for DataRelation
                        //Create a DataColumn array...
                        DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                        //...and then populate the array with the columns from the Source
                        for (int i = 0; i < firstColumns.Length; i++)
                        {
                            firstColumns[i] = ds.Tables[0].Columns[i];
                        }

                        //Repeat for Comparison
                        //Create a DataColumn array...
                        DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                        //...and then populate it with the columns from the Comparison
                        for (int i = 0; i < secondColumns.Length; i++)
                        {
                            secondColumns[i] = ds.Tables[1].Columns[i];
                        }

                        //Create DataRelation by feeding in the DataColumns we just created
                        //This one relates secondTable as a child to firstTable:
                        DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                        ds.Relations.Add(r1);

                        //Create DataRelation by feeding in the DataColumns we just created:
                        //This one relates firstTable as a child to secondTable:
                        DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                        ds.Relations.Add(r2);

                        //Create columns for return table based on our copied firstTable.
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            dtReturnMe.Columns.Add(ds.Tables[0].Columns[i].ColumnName, ds.Tables[0].Columns[i].DataType);
                        }

                        dtReturnMe.BeginLoadData();
                        //If secondTable Row not in firstTable, Add to result table
                        foreach (DataRow parentrow in ds.Tables[1].Rows)
                        {
                            DataRow[] childrows = parentrow.GetChildRows(r2);
                            if (childrows == null || childrows.Length == 0)
                            {
                                dtReturnMe.LoadDataRow(parentrow.ItemArray, true);
                            }
                        }
                        dtReturnMe.EndLoadData();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dtReturnMe;
        }

        private static bool IsComparible(DataTable firstTable, DataTable secondTable)
        {
            bool returnme = true;

            //Check the DataTables to see if the table format is the same between both tables
            //First, do they have the same number of columns?
            if (firstTable.Columns.Count == secondTable.Columns.Count)
            {
                //Second, are they the same Name and DataType?
                for (int i = 0; i < firstTable.Columns.Count - 1; i++)
                {
                    //get the column name in the first table:
                    string leftColName = firstTable.Columns[i].ColumnName;
                    string rightColName = secondTable.Columns[i].ColumnName;
                    if(secondTable.Columns[i].ColumnName != rightColName)
                    {
                        throw new ArgumentException($"ColumnName mismatch: firstTable.ColumnName={leftColName}, secondTable.ColumnName={secondTable.Columns[i].ColumnName}");
                    }
                    if ((firstTable.Columns[i].DataType != secondTable.Columns[i].DataType))
                    {
                        throw new ArgumentException($"DataType mismatch. {firstTable.Columns[i].ColumnName}:{firstTable.Columns[i].DataType.ToString()} vs {secondTable.Columns[i].ColumnName}:{secondTable.Columns[i].DataType.ToString()}");
                    }
                }
            }
            else throw new ArgumentException("Comparison tables do not have same number of columns.");

            return returnme;
        }



        /// <summary>
        /// Builds a ConnectionStringBuilder object for the selected Excel file.
        /// </summary>
        /// <param name="filepathToOpen">Path of the file to open</param>
        /// <returns>OleDbConnectionStringBuilder object that can be used with an OleDbConnection object to open an Excel file</returns>
        public static OleDbConnectionStringBuilder GetACEConnectionString(string filepathToOpen, bool UseHeaders = false)
        {
            System.Data.OleDb.OleDbConnectionStringBuilder connBuilder = new System.Data.OleDb.OleDbConnectionStringBuilder
            {
                { "Data Source", filepathToOpen },
                { "Provider", "Microsoft.ACE.OLEDB.12.0" }
            };

            if (UseHeaders)
            {
                connBuilder.Add("Extended Properties", "Excel 12.0 Macro; HDR=YES");
            }
            else connBuilder.Add("Extended Properties", "Excel 12.0 Macro; HDR=NO");

            return connBuilder;
        }

        /// <summary>
        /// Uses SqlDataAdapter.SelectCommand.Connection.GetSchema to check if the adapter's table exists. Assumes that the adapter's Connection is correct.
        /// </summary>
        /// <param name="da">SqlDataAdapter to check against</param>
        /// <param name="tableToCheckFor">Name of table to check for</param>
        /// <returns>true if table exists; false otherwise</returns>
        internal static bool DoesDBTableExist(System.Data.SqlClient.SqlDataAdapter da, string tableToCheckFor)
        {
            bool returnme = false;
            try
            {
                //Notes for the TableRestrictions array:
                //0 member represents Catalog;
                //1 member represents Schema;
                //2 member represents Table Name;
                //3 member represents Table Type
                string[] tableRestrictions = new string[4];
                tableRestrictions[2] = tableToCheckFor;
                DataTable dt = da.SelectCommand.Connection.GetSchema("Tables", tableRestrictions);
                if (!(dt is null))
                {
                    returnme = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return returnme;
        }

        internal static bool DoesDBColumnExist(System.Data.SqlClient.SqlDataAdapter da, string tablename, string columnname)
        {
            bool returnme = false;
            try
            {
                //Notes for the ColumnRestrictions array:
                //0 member represents Catalog;
                //1 member represents Schema;
                //2 member represents Table Name;
                //3 member represents Column Name
                string[] ColumnRestrictions = new string[4];
                ColumnRestrictions[2] = tablename;
                ColumnRestrictions[3] = columnname;
                DataTable dt = da.SelectCommand.Connection.GetSchema("Columns", ColumnRestrictions);
                if (!(dt is null))
                {
                    returnme = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return returnme;
        }


        /// <summary>
        /// Performs a join on the 2 tables based on the provided column names and returns a DataTable containing records from firstTable
        /// that do not have matching entries in secondTable.
        /// </summary>
        /// <param name="firstTable">DataTable to get results from</param>
        /// <param name="secondTable">DataTable to check for matches against</param>
        /// <param name="FirstTable_ColumnNameToJoinOn">Name of DataColumn in firstTable to join on</param>
        /// <param name="SecondTable_ColumnNameToJoinOn">Name of DataColumn in secondTable to join on</param>
        /// <returns></returns>
        public static DataTable GetRowsNotInSecondTable(DataTable firstTable, DataTable secondTable, string FirstTable_ColumnNameToJoinOn, string SecondTable_ColumnNameToJoinOn)
        {
            DataTable returnme;
            try
            {
                returnme = (from a in firstTable.AsEnumerable()
                            join b in secondTable.AsEnumerable()
                            on a[FirstTable_ColumnNameToJoinOn].ToString() equals b[SecondTable_ColumnNameToJoinOn].ToString()
                            into g
                            where g.Count() == 0
                            select a).CopyToDataTable();
            }
            catch (Exception ex)
            {
                if (ex.Message == "The source contains no DataRows.")
                {
                    throw ex;
                }
                throw ex;
            }
            return returnme;
        }




        /// <summary>
        /// Queries a DataTable object to see if it contains a column with the specified name.
        /// </summary>
        /// <param name="tableToCheck">DataTable object that contains the columns</param>
        /// <param name="columnNameToCheckFor">Name of the column to check for</param>
        /// <returns></returns>
        internal static bool DoesColumnExist(DataTable tableToCheck, string columnNameToCheckFor)
        {
            bool returnme = false;
            try
            {
                DataColumnCollection columns = tableToCheck.Columns;
                if (columns.Contains(columnNameToCheckFor))
                {
                    returnme = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return returnme;
        }

        internal static SqlDbType GetSqlType(Type type)
        {
            if(type == typeof(string))
            {
                return SqlDbType.NVarChar;
            }
            if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable))
            {
                type = Nullable.GetUnderlyingType(type);
            }
            var param = new System.Data.SqlClient.SqlParameter("", Activator.CreateInstance(type));
            return param.SqlDbType;
        }



        /// <summary>
        /// Returns a DataTable filled by the SqlDataAdapter.FillSchema method.
        /// </summary>
        /// <param name="daForTableToGet">SqlDataAdapter with valid SelectCommand</param>
        /// <returns>DataTable with schema of table queried by SelectCommand.</returns>
        internal static DataTable GetTableSchema(SqlDataAdapter daForTableToGet)
        {
            DataTable temptable = new DataTable();
            try
            {
                if (!(daForTableToGet.SelectCommand.Connection is null))
                {
                    daForTableToGet.FillSchema(temptable, SchemaType.Source);
                }
                else throw new ArgumentException($"DataAdapter is missing SelectCommand.Connection.");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return temptable;
        }


        /// <summary>
        /// Returns a list of column names from the supplied DataTable.
        /// </summary>
        /// <param name="dtWithColumns">DataTable with columns</param>
        /// <returns>List of column names of supplied DataTable.</returns>
        internal static List<string> GetTableColumns(DataTable dtWithColumns)
        {
            try
            {
                List<string> columns = new List<string>();
                if (dtWithColumns.Columns.Count > 0)
                {
                    foreach(DataColumn col in dtWithColumns.Columns)
                    {
                        columns.Add(col.ColumnName);
                    }
                }
                else throw new ArgumentException($"DataTable {dtWithColumns.TableName} has no columns.");
                return columns;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Uses the SqlDataAdapter's SelectCommand to get the schema of the table queried.
        /// </summary>
        /// <param name="da">SqlDataAdapter with valid SelectCommand.</param>
        /// <returns>List of table columns for the table queried in SqlDataAdapter.SelectCommand</returns>
        internal static List<string> GetTableColumns(SqlDataAdapter da)
        {
            List<string> returnme;
            try
            {
                if (!(da.SelectCommand.Connection is null))
                {
                    returnme = GetTableColumns(GetTableSchema(da));
                }
                else throw new ArgumentException($"DataAdapter is missing SelectCommand.Connection");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnme;
        }

        internal static SortedList<string, string> ReverseKeyvaluePair(SortedList<string, string> reverseme)
        {
            SortedList<string, string> returnme = new SortedList<string, string>();

            try
            {
                //build the other map, then swap it around to get this one.
                foreach (KeyValuePair<string, string> kvp in reverseme)
                {
                    returnme.Add(kvp.Value.ToString(), kvp.Key.ToString());
                }
            }
            catch (Exception ex)
            {
                returnme.Clear();
                throw ex;
            }

            return returnme;
        }

        internal static bool TableHasChanges(DataTable tabletocheck)
        {
            try
            {
                    foreach (DataRow row in tabletocheck.Rows)
                    {
                        if (row.RowState != DataRowState.Unchanged)
                        {
                            return true;
                        }
                    }
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
