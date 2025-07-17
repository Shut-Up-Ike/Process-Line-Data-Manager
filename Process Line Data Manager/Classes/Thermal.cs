using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Process_Line_Data_Manager
{
    class Thermal
    {
        //These are used to hold the table names in case they change in the future.
        /// <summary>
        /// Name of table where thermal data is stored in the P4D project database
        /// </summary>
        internal static readonly string THERMALTABLENAMEP4D = "NE_HRUData";
        internal static readonly string THERMALTABLENAMEHRU = "vPiping";
        internal static readonly string THERMALTABLENAMECOMBINED = "Combined";

        public Thermal()
        {
        }


        internal static DataTable GetP4DHRUTable(string sqlConnectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlDataAdapter da = ThermalAdapterSetup(sqlConnectionString))
                {
                    da.FillSchema(dt, SchemaType.Source);
                    da.Fill(dt);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        internal static SqlDataAdapter ThermalAdapterSetup(string sqlConnectionString = "")
        {
            SqlDataAdapter daHRU;
            //Now we need to have the P4D HRU data available to us:
            daHRU = new SqlDataAdapter();

            daHRU.TableMappings.Add("Table", THERMALTABLENAMEP4D);

            //Build SELECT query to return our data
            string query = $"SELECT * FROM {THERMALTABLENAMEP4D}";
            SqlCommand comm = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            //Assign the SELECT command to the DataAdapter
            daHRU.SelectCommand = comm;

            //If a connectionstring was passed in, use it to build the connection stuff:
            if (sqlConnectionString != "")
            {
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                daHRU.SelectCommand.Connection = conn;
                try
                {
                    //If the DataAdapter is provied to SqlCommandBuilder during initialization, no assignment of commands 
                    //  is necessary; it automatically adds listeners for them.
                    SqlCommandBuilder builder = new SqlCommandBuilder(daHRU);
                    
                    //... but we don't want to be able to delete them, so let's NOT let those be in there:
                    daHRU.DeleteCommand = null;

                    //Build custom UPDATE command:
                    comm = new SqlCommand
                    {
                        CommandType = CommandType.Text
                    };

                    //Also build custom INSERT command:
                    SqlCommand comm_ins = new SqlCommand
                    {
                        CommandType = CommandType.Text
                    };

                    //Get the DataTable and build the Update CommandText from that structure:
                    DataTable temptable = new DataTable();
                    daHRU.FillSchema(temptable, SchemaType.Source);


                    //This will build the UPDATE statement, along with the INSERT statement, dynamically based on the schema of the table.
                    string updateQuery = $"UPDATE {THERMALTABLENAMEP4D} SET ";

                    //Idea behind insertQuery* parts:
                    //{insertQuery}+{insertQuery_ColNames}+{insertQuery_ColValues}+";"+{selectQuery}+";"
                    //...to build something like...
                    //INSERT INTO {TABLENAME} (COLNAME1, COLNAME2, COLNAME3, ...) VALUES (@PARAM1, @PARAM2, @PARAM3, ...); 
                    //...followed directly by a SELECT statement like...
                    //SELECT {NAME_OF_PK_COLUMN} FROM NE_HRUDATA WHERE ID = SCOPE_IDENTITY();
                    //...Although I have also seen it as simply as...
                    //SELECT SCOPE_IDENTITY();
                    //...Either way, it is broken into pieces like...
                    //{insertQuery_SelectPKColumnName}+{insertQuery_From}+{insertQuery_Where}
                    //...to retrieve the IDENTITY value of ID for new rows.
                    string insertQuery = $"INSERT INTO {THERMALTABLENAMEP4D}";
                    string insertQuery_ColNames = "(";
                    string insertQuery_ColValues = "VALUES (";
                    string insertQuery_SelectID = "SELECT ";
                    string insertQuery_From = $"FROM {THERMALTABLENAMEP4D}";
                    string insertQuery_Where = "WHERE ";

                    for (int i = 0; i < temptable.Columns.Count; i++)
                    {
                        //For every column that isn't the PK:
                        if(temptable.Columns[i].ColumnName.ToString() != temptable.PrimaryKey[0].ToString())
                        {
                            //Add it to the update query in the format of "{columnname}=@{columnnameAsParameter}":
                            updateQuery += $"{temptable.Columns[i].ColumnName.ToString()}=@{temptable.Columns[i].ColumnName.ToString().ToUpper()}";
                            //Add it to the insert query in the format of "{columnname}" (building comma-separated list of column names):
                            insertQuery_ColNames += $"{temptable.Columns[i].ColumnName.ToString()}";
                            //Add it to the insert query's values list in the format of "@{columnname}" (building comma-separated list of column names as parameters):
                            insertQuery_ColValues += $"@{temptable.Columns[i].ColumnName.ToString().ToUpper()}";

                            //If we are not at the last column, add a comma and a space for the next iteration:
                            if (i < (temptable.Columns.Count - 1))
                            {
                                updateQuery += ", ";
                                insertQuery_ColNames += ", ";
                                insertQuery_ColValues += ", ";
                            }
                            //If we ARE at the last column, end it with a space or closing paren (as applicable):
                            else
                            {
                                updateQuery += " ";
                                insertQuery_ColNames += ")";
                                insertQuery_ColValues += ")";
                            }
                        }
                        //For the column that IS the PK:
                        else
                        {
                            //This should be the primary key. Add it to the _SelectID and _Where portions of the insertQuery:
                            //_SelectID should end up as "SELECT {name of the pk column}"
                            insertQuery_SelectID += $"{temptable.Columns[i].ColumnName.ToString()}";
                            //_Where should end up as "WHERE {name of the pk column} = SCOPE_IDENTITY()"
                            insertQuery_Where += $"{temptable.Columns[i].ColumnName.ToString()} = SCOPE_IDENTITY()";
                            //Also add it to the parameter?
                        }
                        //Now add the column as a parameter (where needed) in format of "@{columnnameAsParameter}, {sqlDataType of column}, {max length of column}, {columnname}"
                        //Update parameter:
                        comm.Parameters.Add($"@{temptable.Columns[i].ColumnName.ToUpper()}", DataFunctions.GetSqlType(temptable.Columns[i].DataType), temptable.Columns[i].MaxLength, temptable.Columns[i].ColumnName);
                        //Insert parameter:
                        comm_ins.Parameters.Add($"@{temptable.Columns[i].ColumnName.ToUpper()}", DataFunctions.GetSqlType(temptable.Columns[i].DataType), temptable.Columns[i].MaxLength, temptable.Columns[i].ColumnName);
                    }
                    //Finish off the updateQuery with the "WHERE" portion of the statement:
                    updateQuery += $"WHERE {temptable.PrimaryKey[0].ToString()}=@{temptable.PrimaryKey[0].ToString().ToUpper()}";
                    //And finally, assign it to the command:
                    comm.CommandText = updateQuery;
                    daHRU.UpdateCommand = comm;
                    daHRU.UpdateCommand.Connection = conn;

                    //Wrap up the insertQuery by putting everything together...
                    insertQuery += $"{insertQuery_ColNames} {insertQuery_ColValues}; {insertQuery_SelectID} {insertQuery_From} {insertQuery_Where};";
                    //...and then assign it to the command:
                    comm_ins.CommandText = insertQuery;
                    daHRU.InsertCommand = comm_ins;
                    daHRU.InsertCommand.Connection = conn;
                    //Adding these based on https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-identity-or-autonumber-values#merging-new-identity-values
                    //  but I'm not sure it's needed since this is being built based on the table schema..
                    daHRU.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;
                    daHRU.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //Return it for use
            return daHRU;
        }


        /// <summary>
        /// Retrieves data from HRU database view and returns it as a DataTable.
        /// </summary>
        /// <param name="ProjectNumber">6-digit project number</param>
        /// <param name="ProjectUnits">"English" or "Metric"</param>
        /// <returns>DataTable of data extracted from HRU database view for the provided project</returns>
        private static DataTable GetHRUData(string ProjectNumber = "", string ProjectUnits = "")
        {

            try
            {
                DataTable holder = new DataTable();
                //Set DataHolder's HRUData to the project HRU data.
                holder = HRUInterface.HRU.GetDesignInfo_Full(ProjectNumber, ProjectUnits);
                holder.TableName = THERMALTABLENAMEHRU;
                
                if (holder.Rows.Count > 0)
                {
                    //Let's remove the project_id column since P4D doesn't use it
                    holder.Columns.Remove("project_ID");
                }

                holder.PrimaryKey = new DataColumn[] { holder.Columns["line_no"] };

                //Cleanse the data:
                DataCleanser.Cleanse(ref holder);

                return holder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns a SortedList with P4D ColumnNames as the Key and HRU ColumnNames as the Value.
        /// Used to map P4D's NE_HRUData columns to HRU's columns.
        /// </summary>
        /// <returns></returns>
        internal static SortedList<string, string> GetGenericMapping_P4DtoHRU()
        {
            //These are the column names of P4D's NE_HRUData table and HRU's vPiping[units] view columns.
            //  - P4D on the left (key), HRU on the right (value)
            //  - When a new column gets added or modified, it needs to go here.
            SortedList<string, string> returnme = new SortedList<string, string>
            {
                { "job_no", "job_no" },
                { "line_no", "line_no" },
                { "line_id_no", "line_id_no" },
                { "description", "description" },
                { "qty", "qty" },
                { "Size", "Size" },
                { "PipeOD", "PipeOD" },
                { "PipeThickness", "PipeThickness" },
                { "PipeThicknessDisplay", "PipeThicknessDisplay" },
                { "PipeThicknessUnit", "PipeThicknessUnit" },
                { "Material", "Material" },
                { "PressureValue", "DesignPressure" },
                { "PressureValueDisplay", "DesignPressureDisplay" },
                { "PressureUnit", "PressureUnit" },
                { "TemperatureValue", "DesignTemperature" },
                { "TemperatureValueDisplay", "DesignTemperatureDisplay" },
                { "TemperatureUnit", "TemperatureUnit" },
                { "InsulationThickness", "InsulationThickness" },
                { "InsulationThicknessDisplay", "InsulationThicknessDisplay" },
                { "InsulationThicknessUnit", "InsulationThicknessUnit" },
                { "ansi_class", "ansi_class" },
                { "revision_no", "revision_no" },
                //These rows were added since initial creation of the tables:
                { "maxOperatingTemperature", "maxOperatingTemperature" },
                { "maxOperatingTemperatureDisplay", "maxOperatingTemperatureDisplay" },
                { "thicknessCasePressure", "thicknessCasePressure" },
                { "thicknessCasePressureDisplay", "thicknessCasePressureDisplay" },
                { "thicknessCaseTemperature","thicknessCaseTemperature" },
                { "thicknessCaseTemperatureDisplay","thicknessCaseTemperatureDisplay" },
                { "pressureCasePressure","pressureCasePressure" },
                { "pressureCasePressureDisplay","pressureCasePressureDisplay" },
                { "pressureCaseTemperature","pressureCaseTemperature" },
                { "pressureCaseTemperatureDisplay","pressureCaseTemperatureDisplay" },
                { "temperatureCasePressure","temperatureCasePressure" },
                { "temperatureCasePressureDisplay","temperatureCasePressureDisplay" },
                { "temperatureCaseTemperature","temperatureCaseTemperature" },
                { "temperatureCaseTemperatureDisplay","temperatureCaseTemperatureDisplay" }
            };

            return returnme;
        }

        internal static SortedList<string, string> GetSpecificMapping_P4DtoHRU(SqlDataAdapter p4dDA)
        {
            SortedList<string, string> returnme = new SortedList<string, string>();

            try
            {
                DataTable dt = DataFunctions.GetTableSchema(p4dDA);
                SortedList<string, string> map = GetGenericMapping_P4DtoHRU();

                foreach(KeyValuePair<string, string> kvp in map)
                {
                    if(dt.Columns.Contains(kvp.Key))
                    {
                        returnme.Add(kvp.Key, kvp.Value);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return returnme;
        }

        internal static SortedList<string, string> GetSpecificMapping_HRUtoP4D(SqlDataAdapter p4dDA)
        {
            SortedList<string, string> returnme = new SortedList<string, string>();

            try
            {
                returnme = DataFunctions.ReverseKeyvaluePair(GetSpecificMapping_P4DtoHRU(p4dDA));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return returnme;
        }

        internal static SortedList<string,string> GetGenericMapping_HRUtoP4D()
        {
            SortedList<string, string> returnme = new SortedList<string, string>();

            try
            {
                //build the other map, then swap it around to get this one.
                SortedList<string, string> p4dtohru = GetGenericMapping_P4DtoHRU();
                returnme = DataFunctions.ReverseKeyvaluePair(p4dtohru);
            }
            catch (Exception ex)
            {
                returnme.Clear();
                throw ex;
            }

            return returnme;
        }

        internal static List<string> GetP4DHRUColumnNames(SqlDataAdapter da)
        {
            List<string> returnme = DataFunctions.GetTableColumns(da);
            return returnme;
        }

        internal static List<string> GetP4DHRUColumnNames(string sqlConnectionString)
        {
            List<string> returnme = new List<string>();
            try
            {
                using (SqlDataAdapter da = Thermal.ThermalAdapterSetup(sqlConnectionString))
                {
                    returnme = GetP4DHRUColumnNames(da);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return returnme;
        }


        /// <summary>
        /// Queries HRU for the project represented by ProjectNumber. Returns a DataTable formatted for use with NE_HRUData.
        /// </summary>
        /// <param name="ProjectNumber">6-digit project number</param>
        /// <param name="ProjectUnits">English or Metric</param>
        /// <param name="sqlConnectionString">connection string for the projects P4D database</param>
        /// <returns></returns>
        internal static DataTable GetHRUData_Cleaned(string ProjectNumber = "", string ProjectUnits = "", string sqlConnectionString = "")
        {
            DataTable returnme = new DataTable();

            try
            {
                if(string.IsNullOrEmpty(ProjectNumber) || string.IsNullOrEmpty(ProjectUnits) || string.IsNullOrEmpty(sqlConnectionString))
                {
                    throw new ArgumentException($"Invalid value passed to method.");
                }
                else
                {
                    //Get the full HRU table and data, and copy for returnme variable:
                    DataTable hru = GetHRUData(ProjectNumber, ProjectUnits);
                    returnme = hru.Copy();

                    //Variables that we will be using for the actions:
                    SortedList<string, string> mapping;
                    SortedList<string, string> hruMapping;
                    List<string> P4Dcolumns;

                    //Get all the data we need from the SqlDataAdapter:
                    using (SqlDataAdapter da = ThermalAdapterSetup(sqlConnectionString))
                    {
                        //Get the mapping of (actual) P4D columns to (theoretical) HRU columns:
                        mapping = GetSpecificMapping_HRUtoP4D(da);

                        //Now, we want the rows to be in the correct order so we can do a direct copy into the P4D NE_HRUData table...
                        //...for that, we will need the SortedList of P4DColumns(the key) and HRUColumns(the value) and the List of P4D Columns 
                        //in the same order in which they are in the table:
                        hruMapping = GetSpecificMapping_P4DtoHRU(da);
                        P4Dcolumns = DataFunctions.GetTableColumns(DataFunctions.GetTableSchema(da));
                        //Remove the "ID" column if it exists; there is no need for it to be in this list right now.
                        P4Dcolumns.Remove("ID");
                    }

                    //Now we have the mapping list and what columns we NEED from HRU...
                    //...let's remove all the HRU columns that do not pertain to our needs:
                    foreach (DataColumn col in hru.Columns)
                    {
                        if(!(mapping.ContainsKey(col.ColumnName)))
                        {
                            returnme.Columns.Remove(col.ColumnName);
                        }
                    }

                    //We have our mapping SortedList and the columns, now we need to iterate through and re-order the columns
                    //of the return table:
                    //We need to iterate as many times as there are columns
                    for (int i = 0; i < returnme.Columns.Count; i++)
                    {
                        hruMapping.TryGetValue(P4Dcolumns[i], out string hrucolumnName);
                        if (!string.IsNullOrEmpty(hrucolumnName))
                        {
                            returnme.Columns[hrucolumnName].SetOrdinal(i);
                        }
                        else throw new InvalidOperationException($"Thermal.GetHRUData_Cleaned({ProjectNumber},{ProjectUnits}): Error reordering HRU columns to P4D columns. Check column names in mapping method.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return returnme;
        }


        internal static bool IsComparible_HRUtoP4D(DataTable HRUTable, DataTable P4DTable)
        {
            return IsComparible(HRUTable, P4DTable);
        }

        internal static bool IsComparible_P4DtoHRU(DataTable P4DTable, DataTable HRUTable)
        {
            return IsComparible(HRUTable, P4DTable);
        }

        private static bool IsComparible(DataTable HRUTable, DataTable P4DTable)
        {
            bool returnme = true;

            //Check the DataTables to see if the table format is the same between both tables
            //First, do they have the same number of columns?
            //Update for ID column in P4D table: subtract 1 from the column count, HRU will not have the extra column "ID".
            if (HRUTable.Columns.Count == (P4DTable.Columns.Count - 1))
            {
                SortedList<string, string> columnMap = Thermal.GetGenericMapping_HRUtoP4D();
                //Second, are they the same Name and DataType?
                for (int i = 0; i < HRUTable.Columns.Count - 1; i++)
                {
                    //get the column name in the first table:
                    string hruColName = HRUTable.Columns[i].ColumnName;
                    string p4dColName;// = secondTable.Columns[i].ColumnName;
                    columnMap.TryGetValue(hruColName, out p4dColName);

                    //If we didn't find a match in the mapping, we can't compare it later:
                    if(string.IsNullOrEmpty(p4dColName))
                    {
                        throw new ArgumentException($"Could not find matching column for {hruColName} in P4D table.");
                    }
                    //If we found the match, compare the datatype:
                    if ((HRUTable.Columns[hruColName].DataType != P4DTable.Columns[p4dColName].DataType))
                    {
                        throw new ArgumentException($"DataType mismatch. {HRUTable.Columns[hruColName].ColumnName}:{HRUTable.Columns[hruColName].DataType.ToString()} vs {P4DTable.Columns[p4dColName].ColumnName}:{P4DTable.Columns[p4dColName].DataType.ToString()}");
                    }
                }
            }
            else throw new ArgumentException("Comparison tables do not have same number of columns.");

            return returnme;
        }


        /// <summary>
        /// Returns a datatable containing a combination of the 2 provided tables, with RowState set accordingly.
        /// Intended to compare 2 DataTables that contain the same information but come from different sources.
        /// BOTH TABLES MUST HAVE PRIMARY KEYS DEFINED. If a row exists in OriginalTable but not in UpdatedTable, 
        /// then it is marked as deleted; If row exists in UpdatedTable but not OriginalTable, it is marked as new; 
        /// if it exists in both but the data has changed, then it is marked as modified. Rows that exist in both
        /// but have no data changes are marked as unchanged.
        /// </summary>
        /// <param name="P4DTable">DataTable to consider as "Unchanged"</param>
        /// <param name="HRUTable">DataTable with changed data</param>
        /// <returns>DataTable with combination of OriginalTable and UpdatedTable data with RowState set accordingly</returns>
        internal static DataTable GetChanges(DataTable P4DTable, DataTable HRUTable)
        {
            //For my purposes, OriginalTable is P4D's NE_HRUData and UpdateTable is HRU's data table.
            //If a row exists in P4D but not in HRU, then it was deleted.
            //If a row exists in HRU but not P4D, then it was added.
            //If the row exists in both, compare the data.
            //  if the data has changed, the row was updated.
            //  else, the row remains unchanged.
            //To further throw a wrench in things, "line_no" in HRU is no longer pertinent to our needs. We will need
            //  to disregard any value passed in from there as a "matching" value to P4D and instead try to match on 
            //  the "description" field.
            if (IsComparible_P4DtoHRU(P4DTable, HRUTable))
            {
                //Create a DataTable variable with the same structure and data as what was passed in:
                DataTable returnme = P4DTable.Copy();
                //Set this to have a different TableName so we can differentiate it from the others:
                returnme.TableName = THERMALTABLENAMECOMBINED;

                SortedList<string, string> columnMap = Thermal.GetGenericMapping_HRUtoP4D();

                //We need to know what the PK is for the Datatable so we can search for it later.
                DataColumn[] pk = P4DTable.PrimaryKey;
                string pkName = string.Empty;
                //Is it a single field or more than one?
                if (pk.Length == 1)
                {
                    pkName = pk[0].ColumnName;
                }
                else
                {
                    //TODO: WHAT DO WE DO IF IT IS COMPOUND KEY??
                }

                //Cycle through all the rows in the updated table
                foreach (DataRow drUpdatedRow in HRUTable.Rows)
                {
                    //For each updated row, we want to find a match in the original table.
                    try
                    {
                        //Since we aren't using line_no as PK anymore, we cannot use Rows.Find. Use this instead:
                        DataRow match = P4DTable.AsEnumerable()
                                        .SingleOrDefault(r => r.Field<byte>("line_no") == (byte)drUpdatedRow["line_no"]);
                        //if a match is found, we need to compare the column values.
                        if (!(match is null))
                        {
                            //boolean to know if we found a difference while comparing:
                            bool difference = false;
                            //Cycle through the columns for comparison reasons:
                            for (int i = 0; i < P4DTable.Columns.Count & difference == false; i++)
                            {
                                //get the column name in the first table:
                                string hruColName = HRUTable.Columns[i].ColumnName;
                                //Ask the SortedList what the matching column name should be:
                                columnMap.TryGetValue(hruColName, out string p4dColName);

                                //If the value in the column does not match, we need to mark the row as modified:
                                if (drUpdatedRow[hruColName].ToString() != match[p4dColName].ToString())
                                {
                                    //Set the boolean so we can exit this for loop:
                                    difference = true;
                                    //Create a new row in the return datatable and assign it to a variable for later work:
                                    DataRow newRow = returnme.NewRow();
                                    //Set the columns to be the same values as the UpdatedRow values
                                    for (int c = 0; c < P4DTable.Columns.Count; c++)
                                    {
                                        if (P4DTable.Columns[c].ColumnName != "ID")
                                        {
                                            newRow[c] = drUpdatedRow[c];
                                        }
                                        else
                                        {
                                            newRow[c] = match["ID"];
                                        }
                                    }
                                    //Add the row and Set the row status to Modified.
                                    returnme.Rows.Add(newRow);
                                    newRow.AcceptChanges();
                                    newRow.SetModified();
                                }
                                //else: the values were the same, compare the next column or move to the next row.
                            }
                            if (!difference)
                            {
                                DataRow newRow = returnme.NewRow();
                                //Set the columns to be the same values as the UpdatedRow values
                                for (int c = 0; c < P4DTable.Columns.Count; c++)
                                {
                                    if (P4DTable.Columns[c].ColumnName != "ID")
                                    {
                                        newRow[c] = drUpdatedRow[c];
                                    }
                                    else
                                    {
                                        newRow[c] = match["ID"];
                                    }
                                }
                                returnme.Rows.Add(newRow);
                                newRow.AcceptChanges();
                            }
                        }
                        //if NO match is found, then we need to add the row and mark it as NEW or ADDED.
                        else
                        {
                            //Create a new row in the return datatable and assign it to a variable for later work:
                            DataRow newRow = returnme.NewRow();
                            //Set the columns to be the same values as the UpdatedRow values
                            for (int c = 0; c < P4DTable.Columns.Count; c++)
                            {
                                newRow[c] = drUpdatedRow[c];
                            }
                            //Add the row; the row status will automatically be "added".
                            returnme.Rows.Add(newRow);
                        }

                    }
                    catch (MissingPrimaryKeyException)
                    {
                        //Primary key functionality is apparently broken. Try something else.
                        throw;
                    }
                }

                //Now we have checked the UpdatedTable... but we still need to find rows that are in Original but NOT in Updated (aka DELETED rows)
                foreach (DataRow drOriginal in P4DTable.Rows)
                {
                    try
                    {
                        DataRow match = HRUTable.Rows.Find(drOriginal[pkName]);
                        if (match is null)
                        {
                            DataRow newRow = returnme.NewRow();
                            for (int i = 0; i < P4DTable.Columns.Count; i++)
                            {
                                newRow[i] = drOriginal[i];
                            }
                            //Add the row; the row status will automatically be "added".
                            returnme.Rows.Add(newRow);
                            //Deleting the row will mark it as "Deleted". Until AcceptChanges() is called, it will remain in the table.
                            newRow.Delete();
                        }
                    }
                    catch (MissingPrimaryKeyException)
                    {
                        throw;
                    }
                }

                return returnme;
            }
            else throw new ArgumentException("Tables are not comparible.");
        }

        internal static DataTable GetMergedTable(DataTable p4dDataTable, DataTable hruDataTable)
        {
            //It is assumed that these tables have already been evaluated for matches and that the line_no column has been set/matched accordingly.
            try
            {
                //Copy P4D table. We will use this to establish the merged state:
                DataTable returnme = p4dDataTable.Copy();
                //Set this to have a different TableName so we can differentiate it from the others:
                returnme.TableName = THERMALTABLENAMECOMBINED;

                //Accept any changes so it is "clean":
                returnme.AcceptChanges();

                //This will map P4D column names to those in HRU:
                SortedList<string, string> columnMap = Thermal.GetGenericMapping_HRUtoP4D();

                //for each P4D record, we need to find its matching HRU record and evaluate its new rowstate.
                //We are going to use the passed-in P4D table for the loop because no changes will be made to it; changes will be applied to our copy "returnme":
                foreach (DataRow p4drow in p4dDataTable.Rows)
                {
                    DataRow hrumatch;
                    DataRow newMatch;
                    try
                    {
                        //Since we aren't using line_no as PK anymore, we cannot use Rows.Find. Use this instead:
                        //DataRow hrumatch = hruDataTable.AsEnumerable()
                        hrumatch = hruDataTable.AsEnumerable()
                                        .Single(r => r.Field<byte>("line_no") == (byte)p4drow["line_no"]);
                        //This ^^ SHOULD find a match, and only a SINGLE match. Anything more will cause an error.
                        
                        
                        //Get the matching row from returnme so we can make changes as needed:
                        //DataRow newMatch = returnme.AsEnumerable()
                        newMatch = returnme.AsEnumerable()
                                            .Single(r => r.Field<int>("ID") == (int)p4drow["ID"]);
                        //This ^^ SHOULD find a match, and only a SINGLE match. Anything more will cause an error.

                        //Now we need to compare the value in each column for each row (based on the count of columns of HRU, because P4D has extra columns that we do not want to have involved here):
                        for (int i = 0; i < hruDataTable.Columns.Count; i++)
                        {
                            //get the column name in the HRU table:
                            string hruColName = hruDataTable.Columns[i].ColumnName;

                            //Ask the SortedList what the matching P4D column name should be:
                            columnMap.TryGetValue(hruColName, out string p4dColName);

                            //Make sure a match was found:
                            if (!(string.IsNullOrEmpty(p4dColName)))
                            {
                                //Compare the values in the columns:
                                if (hrumatch[hruColName].ToString() != p4drow[p4dColName].ToString())
                                {
                                    //Values are different. Update our working record to be what HRU has:
                                    newMatch[p4dColName] = hrumatch[hruColName];
                                }
                            }
                            //else: the values were the same, compare the next column or move to the next row.
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Sequence contains no matching element")
                        {
                            //Couldn't find a match between hru and p4d. This SHOULD be because p4d has a DELETED line. Let's be sure:
                            if (p4drow["DESCRIPTION"].ToString() == "DELETED")
                            {
                                //This is fine. Eat it and move to the next row.
                            }
                            else
                            {
                                throw;
                            }
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                try
                {
                    //Now we need to evaluate the HRU records for new rows and add them to the new DataTable. Call the method to return the filtered rows;
                    //Wrapped in a try block because if no matches are found, and error is thrown.
                    DataTable nonMatchedHRURecords = DataFunctions.GetRowsNotInSecondTable(hruDataTable, returnme, "line_no", "line_no");
                    foreach (DataRow hruRow in nonMatchedHRURecords.Rows)
                    {
                        //Create a new row in the table to be returned, and assign the variable to it:
                        DataRow newrow = returnme.NewRow();

                        //loop through all of the columns and set the values:
                        for (int i = 0; i < hruDataTable.Columns.Count; i++)
                        {
                            //Get the column name in the HRU table:
                            string hruColName = hruDataTable.Columns[i].ColumnName;

                            //Ask the SortedList what the matching P4D column name should be:
                            columnMap.TryGetValue(hruColName, out string p4dColName);

                            //Make sure a match was found:
                            if (!(string.IsNullOrEmpty(p4dColName)))
                            {
                                //Values are different. Update our working record to be what HRU has:
                                newrow[p4dColName] = hruRow[hruColName];
                            }
                        }

                        //Now add the row. The status will automatically be "Added".
                        returnme.Rows.Add(newrow);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "The source contains no DataRows.")
                    {
                        //No records are unmatched. This is ok, continue.
                    }
                    else
                    {
                        //Uh oh. I don't know what's up, pass it up the chain.
                        throw;
                    }
                }

                try
                {
                    //And finally, we need to mark any P4D records that do not have matching HRU records as "DELETED". It is doubtful that this will ever be needed, but just in case...
                    //Wrapped in a try block because if no matches are found an error is thrown.
                    DataTable nonMatchedP4DRecords = DataFunctions.GetRowsNotInSecondTable(returnme, hruDataTable, "line_no", "line_no");
                    foreach (DataRow delRow in nonMatchedP4DRecords.Rows)
                    {
                        //Get the row from returnme so we can update it:
                        DataRow thisrow = returnme.AsEnumerable()
                                            .Single(r => r.Field<int>("ID") == delRow.Field<int>("ID"));

                        //"DELETING" in HRU means changing the description to "DELETED"; otherwise, the row will always exist. Let's follow that mentality for P4D as well (for now):
                        thisrow["description"] = "DELETED";
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "The source contains no DataRows.")
                    {
                        //No records are unmatched. This is ok, continue.
                    }
                    else
                    {
                        //Uh oh. I don't know what's up, pass it up the chain.
                        throw;
                    }
                }

                return returnme;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
