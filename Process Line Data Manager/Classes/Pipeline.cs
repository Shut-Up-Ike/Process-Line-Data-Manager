using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Process_Line_Data_Manager
{
    class Pipeline
    {

        //These are used to hold the table names in case they change in the future.
        /// <summary>
        /// Name of table where pipeline data is stored
        /// </summary>
        internal static readonly string PIPELINETABLENAME = "NE_PipeLines";

        public Pipeline()
        {
        }

        internal static SqlDataAdapter PipeLineDataAdapterSetup()
        {
            //Create the object to return:
            SqlDataAdapter da = new SqlDataAdapter();

            da.TableMappings.Add("Table", PIPELINETABLENAME);

            //Build SELECT query to return our data
            string query = $"SELECT NELINENUMBER, LINEDESCRIPTION, SECONDARYLINEDESCRIPTION FROM {PIPELINETABLENAME} ORDER BY NELINENUMBER, LINEDESCRIPTION, SECONDARYLINEDESCRIPTION";
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;

            //Assign the SELECT command to the DataAdapter
            da.SelectCommand = comm;

            //Build INSERT query for new records
            query = "PipeLine_CreateNew";
            comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = query;
            comm.CreateParameter();
            comm.Parameters.Add("@NAME", SqlDbType.VarChar, 50, "NELineNumber");
            comm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar, 100, "LineDescription");
            comm.Parameters.Add("@THERMALID", SqlDbType.TinyInt);
            comm.Parameters.Add("@EXCELID", SqlDbType.Int);
            comm.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
            comm.Parameters.Add("@SECONDARYLINEDESCRIPTION", SqlDbType.VarChar, 100, "SecondaryLineDescription");

            //Assign the INSERT command to the DataAdapter
            da.InsertCommand = comm;

            //Build DELETE query to remove existing record
            query = $"DELETE FROM {PIPELINETABLENAME} WHERE NELINENUMBER=@NELINENUMBER";
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;
            comm.CreateParameter();
            comm.Parameters.Add("@NELINENUMBER", SqlDbType.VarChar, 50, "NELineNumber");

            //Assign the DELETE command to the DataAdapter
            da.DeleteCommand = comm;

            //Build UPDATE query to update existing records.
            //TODO: this will not work if the user changes the NELineNumber value. Might need a better way to do this...
            query = $"UPDATE {PIPELINETABLENAME} SET LINEDESCRIPTION=@NELINEDESCRIPTION, SECONDARYLINEDESCRIPTION=@SECONDARYLINEDESCRIPTION WHERE NELINENUMBER=@NELINENUMBER";
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;
            comm.CreateParameter();
            comm.Parameters.Add("@NELINENUMBER", SqlDbType.VarChar, 50, "NELINENUMBER");
            comm.Parameters.Add("@NELINEDESCRIPTION", SqlDbType.VarChar, 100, "LINEDESCRIPTION");
            comm.Parameters.Add("@SECONDARYLINEDESCRIPTION", SqlDbType.VarChar, 100, "SECONDARYLINEDESCRIPTION");

            //Assign the UPDATE command to the DataAdapter
            da.UpdateCommand = comm;

            return da;
        }
    }
}
