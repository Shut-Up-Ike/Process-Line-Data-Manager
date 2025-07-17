using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Process_Line_Data_Manager
{
    class ExcelData
    {

        //These are used to hold the table names in case they change in the future.
        /// <summary>
        /// Name of table where pipeline data is stored
        /// </summary>
        internal static readonly string EXCELDATA_TABLENAME = "NE_ExcelData";

        public ExcelData() { }


        internal static SqlDataAdapter ExcelSQLDataAdapterSetup(string sqlConnectionstring)
        {
            //Create the object to return:
            SqlDataAdapter da = new SqlDataAdapter();

            da.TableMappings.Add("Table", EXCELDATA_TABLENAME);

            //Build SELECT query to return our data
            string query = $"SELECT * FROM {EXCELDATA_TABLENAME}";
            SqlCommand comm = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            //Assign the SELECT command to the DataAdapter
            da.SelectCommand = comm;
            //Assign a connection to the select command
            SqlConnection conn = new SqlConnection(sqlConnectionstring);
            da.SelectCommand.Connection = conn;

            //Create a CommandBuilder object and build the rest of the commands:
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.UpdateCommand = builder.GetUpdateCommand();
            da.UpdateCommand.Connection = conn;
            da.InsertCommand = builder.GetInsertCommand();
            da.InsertCommand.Connection = conn;

            return da;
        }
        internal static SqlDataAdapter ExcelSQLDataAdapterSetupWithCustomINSERT(string sqlConnectionstring)
        {
            //Create the object to return:
            SqlDataAdapter da = new SqlDataAdapter();

            da.TableMappings.Add("Table", EXCELDATA_TABLENAME);

            //Build SELECT query to return our data
            //string query = $"SELECT ID, Description, Size, PipeThickness, Material, PressureValue, TemperatureValue, ANSI_Class, CodeJurisdiction FROM {EXCELDATA_TABLENAME}";
            string query = $"SELECT * FROM {EXCELDATA_TABLENAME} ORDER BY DESCRIPTION";
            SqlCommand comm = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };

            //Assign the SELECT command to the DataAdapter
            da.SelectCommand = comm;
            //Assign a connection to the select command
            SqlConnection conn = new SqlConnection(sqlConnectionstring);
            da.SelectCommand.Connection = conn;

            //Create a CommandBuilder object and build the rest of the commands:
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.UpdateCommand = builder.GetUpdateCommand();
            da.UpdateCommand.Connection = conn;

            //Build customized INSERT command:
            query = $"INSERT INTO {EXCELDATA_TABLENAME} (Description, Size, PipeThickness, Material, PressureValue, TemperatureValue, ANSI_Class, CodeJurisdiction) VALUES (@DESCRIPTION, @SIZE, @PIPETHICKNESS, @MATERIAL, @PRESSUREVALUE, @TEMPERATUREVALUE, @ANSI_CLASS, @CODEJURISDICTION)";
            comm = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = query
            };
            comm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar, 100, "Description");
            comm.Parameters.Add("@SIZE", SqlDbType.VarChar, 50, "Size");
            comm.Parameters.Add("@PIPETHICKNESS", SqlDbType.VarChar, 30, "PipeThickness");
            //comm.Parameters.Add("@MATERIAL", SqlDbType.VarChar, 15, "Material");
            comm.Parameters.Add("@MATERIAL", SqlDbType.VarChar, 100, "Material");
            comm.Parameters.Add("@PRESSUREVALUE", SqlDbType.Float, 0, "PressureValue");
            comm.Parameters.Add("@TEMPERATUREVALUE", SqlDbType.Float, 0, "TemperatureValue");
            comm.Parameters.Add("@ANSI_CLASS", SqlDbType.VarChar, 15, "ANSI_Class");
            comm.Parameters.Add("@CODEJURISDICTION", SqlDbType.VarChar, 50, "CodeJurisdiction");
            da.InsertCommand = comm;
            //da.InsertCommand = builder.GetInsertCommand();
            da.InsertCommand.Connection = conn;

            return da;
        }

        internal static OleDbDataAdapter ExcelOleDbDataAdapterSetup()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter();

            //TODO: setup OleDbDataAdapter

            return adapter;
        }
    }
}
