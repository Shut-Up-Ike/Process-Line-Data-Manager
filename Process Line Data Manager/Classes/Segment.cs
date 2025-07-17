using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Process_Line_Data_Manager
{
    class Segment
    {
        //These are used to hold the table names in case they change in the future.
        /// <summary>
        /// Name of table where segment data is stored
        /// </summary>
        internal static readonly string SEGMENTTABLENAME = "NE_Segments";

        /// <summary>
        /// Variables used to test whether or not the project uses "MDCs"
        /// </summary>
        internal static readonly string MDCTEMPERATURE_TESTCOLUMNNAME = "thicknessCaseTemperatureDisplay";
        internal static readonly string MDCPRESSURE_TESTCOLUMNNAME = "thicknessCasePressureDisplay";

        public Segment()
        {
        }


        internal static SqlDataAdapter SegmentAdapterSetup(string sqlConnectionString = "")
        {
            //Create the object to return:
            SqlDataAdapter da = new SqlDataAdapter();

            da.TableMappings.Add("Table", SEGMENTTABLENAME);

            //Build SELECT query to return our data
            string query = $"SELECT * FROM {SEGMENTTABLENAME}";
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;

            //Assign the SELECT command to the DataAdapter
            da.SelectCommand = comm;

            //Build INSERT query for new records
            query = "Segment_CreateNew";
            comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = query;
            comm.CreateParameter();
            comm.Parameters.Add("@PipeLine", SqlDbType.VarChar, 50);
            comm.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

            //Assign the INSERT command to the DataAdapter
            da.InsertCommand = comm;

            //Build DELETE query to remove existing record
            query = $"DELETE FROM {SEGMENTTABLENAME} WHERE ID=@ID";
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;
            comm.CreateParameter();
            comm.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");

            //Assign the DELETE command to the DataAdapter
            da.DeleteCommand = comm;

            //If SqlConnectionString was provided, get the table structure and build the update statement dynamically.
            //Otherwise, build it normally (hard-coded columns)
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            if (string.IsNullOrEmpty(sqlConnectionString))
            {
                //Build UPDATE query to update existing records.
                query = $"UPDATE {SEGMENTTABLENAME} " +
                         "SET [HRUParentID]=@HRUParentID," +
                             "[ExcelParentID]=@ExcelParentID," +
                             "[Diameter]=@Diameter," +
                             "[WallThickness]=@WallThickness," +
                             "[PipeMaterial]=@PipeMaterial," +
                             "[CodeJurisdiction]=@CodeJurisdiction," +
                             "[ANSIClass]=@ANSIClass," +
                             "[Pressure]=@Pressure," +
                             "[PressureUnits]=@PressureUnits," +
                             "[Temperature]=@Temperature," +
                             "[TemperatureUnits]=@TemperatureUnits," +
                             "[InsulationThickness]=@InsulationThickness," +
                             "[CorrosionAllowance]=@CorrosionAllowance," +
                             "[IsTubing]=@IsTubing," +
                             "[maxOperatingTempString]=@MaxOperatingTemp," +
                             "[thicknessCasePressureString]=@ThicknessCasePressureString," +
                             "[thicknessCaseTemperatureString]=@ThicknessCaseTemperatureString," +
                             "[pressureCasePressureString]=@PressureCasePressureString," +
                             "[pressureCaseTemperatureDisplay]=@PressureCaseTemperatureDisplay," +
                             "[temperatureCasePressureDisplay]=@TemperatureCasePressureDisplay," +
                             "[temperatureCaseTemperatureDisplay]=@TemperatureCaseTemperatureDisplay " +
                         "WHERE ID=@ID";
                comm.Parameters.Add("@HRUParentID", SqlDbType.Int, 0, "HRUParentID");
                comm.Parameters.Add("@ExcelParentID", SqlDbType.Int, 0, "ExcelParentID");
                comm.Parameters.Add("@Diameter", SqlDbType.VarChar, 50, "Diameter");
                comm.Parameters.Add("@WallThickness", SqlDbType.VarChar, 50, "WallThickness");
                comm.Parameters.Add("@PipeMaterial", SqlDbType.VarChar, 100, "PipeMaterial");
                comm.Parameters.Add("@CodeJurisdiction", SqlDbType.VarChar, 50, "CodeJurisdiction");
                comm.Parameters.Add("@ANSIClass", SqlDbType.VarChar, 10, "ANSIClass");
                comm.Parameters.Add("@Pressure", SqlDbType.VarChar, 50, "Pressure");
                comm.Parameters.Add("@PressureUnits", SqlDbType.VarChar, 50, "PressureUnits");
                comm.Parameters.Add("@Temperature", SqlDbType.VarChar, 50, "Temperature");
                comm.Parameters.Add("@TemperatureUnits", SqlDbType.VarChar, 50, "TemperatureUnits");
                comm.Parameters.Add("@InsulationThickness", SqlDbType.Float, 0, "InsulationThickness");
                comm.Parameters.Add("@CorrosionAllowance", SqlDbType.VarChar, 50, "CorrosionAllowance");
                comm.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
                comm.Parameters.Add("@IsTubing", SqlDbType.Bit, 0, "IsTubing");
                comm.Parameters.Add("@MaxOperatingTemp", SqlDbType.VarChar, 50, "maxOperatingTempString");
                comm.Parameters.Add("@ThicknessCasePressureString", SqlDbType.VarChar, 50, "thicknessCasePressureString");
                comm.Parameters.Add("@ThicknessCaseTemperatureString", SqlDbType.VarChar, 50, "thicknessCaseTemperatureString");
                comm.Parameters.Add("@PressureCasePressureString", SqlDbType.VarChar, 50, "pressureCasePressureString");
                comm.Parameters.Add("@PressureCaseTemperatureDisplay", SqlDbType.VarChar, 50, "pressureCaseTemperatureDisplay");
                comm.Parameters.Add("@TemperatureCasePressureDisplay", SqlDbType.VarChar, 50, "temperatureCasePressureDisplay");
                comm.Parameters.Add("@TemperatureCaseTemperatureDisplay", SqlDbType.VarChar, 50, "temperatureCaseTemperatureDisplay");

                comm.CommandText = query;

                //Assign the UPDATE command to the DataAdapter
                da.UpdateCommand = comm;
            }
            else
            {
                //Create a connection object and assign it to the DataAdapter:
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                da.SelectCommand.Connection = conn;
                da.InsertCommand.Connection = conn;
                da.DeleteCommand.Connection = conn;


                try
                {
                    //Get the DataTable and build the Update CommandText from that structure:
                    DataTable temptable = new DataTable();
                    da.FillSchema(temptable, SchemaType.Source);

                    var sb = new System.Text.StringBuilder();
                    sb.Append($"UPDATE {SEGMENTTABLENAME} SET ");

                    for (int i = 0; i < temptable.Columns.Count; i++)
                    {
                        if (temptable.Columns[i].ColumnName.ToString() != temptable.PrimaryKey[0].ToString())
                        {
                            sb.Append($"{temptable.Columns[i].ColumnName.ToString()}=@{temptable.Columns[i].ColumnName.ToString().ToUpper()}");
                            if (i < (temptable.Columns.Count - 1))
                            {
                                sb.Append(", ");
                            }
                            else sb.Append(" ");
                        }
                        comm.Parameters.Add($"@{temptable.Columns[i].ColumnName.ToUpper()}", DataFunctions.GetSqlType(temptable.Columns[i].DataType), temptable.Columns[i].MaxLength, temptable.Columns[i].ColumnName);
                    }
                    sb.Append( $"WHERE {temptable.PrimaryKey[0].ToString()}=@{temptable.PrimaryKey[0].ToString().ToUpper()}");

                    comm.CommandText = sb.ToString();

                    //Assign the UPDATE command and the connection string to the DataAdapter
                    da.UpdateCommand = comm;
                    da.UpdateCommand.Connection = conn;
                }
                catch (Exception)
                {

                    throw;
                }
            }


            return da;
        }



        /// <summary>
        /// Returns a sorted list with NE_HRUData column names as the key and NE_Segments columns as the value. Used
        /// to map NE_HRUData columns to the respective NE_Segments columns when synchronizing the data.
        /// </summary>
        /// <returns>SortedList with NE_HRUData.ColumnName as key, NE_Segments.COlumnName as value</returns>
        internal static SortedList<string, string> GetGenericMapping_HRUtoSegments()
        {
            //This is the mapping of columns from NE_HRUData to NE_Segments.
            //  - when a new row gets added to NE_HRUData that needs to go to NE_Segments, it needs to get put here.
            //  - NE_HRUData columns that aren't used are commented out.
            SortedList<string, string> returnme = new SortedList<string, string>
            {
                //{ "job_no", "job_no" },
                //{ "line_no", "HRUParentID" },
                { "ID", "HRUParentID" },
                //{ "line_id_no", "line_id_no" },
                //{ "description", "description" },
                //{ "qty", "qty" },

                //NOTE - Diameter is being mapped to PipeOD - we are using PipeOD as an intermediate
                //          because HRU lists nominal diameters differently (0.5" vs 1/2").
                //{ "Size", "Diameter" },
                { "PipeOD", "Diameter" },
                //{ "PipeOD", "PipeOD" },
                //{ "PipeThickness", "PipeThickness" },
                { "PipeThicknessDisplay", "WallThickness" },
                //{ "PipeThicknessUnit", "PipeThicknessUnit" },
                { "Material", "PipeMaterial" },
                //{ "PressureValue", "DesignPressure" },
                { "PressureValueDisplay", "Pressure" },
                { "PressureUnit", "PressureUnits" },
                //{ "TemperatureValue", "DesignTemperature" },
                { "TemperatureValueDisplay", "Temperature" },
                { "TemperatureUnit", "TemperatureUnits" },
                //{ "InsulationThickness", "InsulationThickness" },
                { "InsulationThicknessDisplay", "InsulationThickness" },
                //{ "InsulationThicknessUnit", "InsulationThicknessUnit" },
                //{ "maxOperatingTemp", "maxOperatingTemperature" },
                { "maxOperatingTemperatureDisplay", "maxOperatingTemperatureDisplay" },
                //{ "thicknessCasePressure", "thicknessCasePressure" },
                { "thicknessCasePressureDisplay", "thicknessCasePressureDisplay" },
                //{ "thicknessCaseTemperature","thicknessCaseTemperature" },
                { "thicknessCaseTemperatureDisplay","thicknessCaseTemperatureDisplay" },
                //{ "pressureCasePressure","pressureCasePressure" },
                { "pressureCasePressureDisplay","pressureCasePressureDisplay" },
                //{ "pressureCaseTemperatureValue","pressureCaseTemperature" },
                { "pressureCaseTemperatureDisplay","pressureCaseTemperatureDisplay" },
                //{ "temperatureCasePressureValue","temperatureCasePressure" },
                { "temperatureCasePressureDisplay","temperatureCasePressureDisplay" },
                //{ "temperatureCaseTemperatureValue","temperatureCaseTemperature" },
                { "temperatureCaseTemperatureDisplay","temperatureCaseTemperatureDisplay" },
                { "ansi_class", "ANSIClass" }
                //{ "revision_no", "revision_no" }
            };

            return returnme;
        }



        /// <summary>
        /// Returns a sorted list with NE_Segments column names as the key and NE_HRUData columns as the value. Used
        /// to map NE_Segments columns to the respective NE_HRUData columns when synchronizing the data.
        /// </summary>
        /// <returns>SortedList with NE_Segments.ColumnName as key, NE_HRUData.ColumnName as value</returns>
        internal static SortedList<string , string> GetGenericMapping_SegmentstoHRU()
        {
            SortedList<string, string> returnme = new SortedList<string, string>();

            try
            {
                //build the other map, then swap it around to get this one.
                SortedList<string, string> p4dtohru = GetGenericMapping_HRUtoSegments();
                returnme = DataFunctions.ReverseKeyvaluePair(p4dtohru);
            }
            catch (Exception ex)
            {
                returnme.Clear();
                throw ex;
            }

            return returnme;
        }
    }
}
