using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Process_Line_Data_Manager
{
    class PipelineSegmentMap
    {
        //These are used to hold the table names in case they change in the future.
        /// <summary>
        /// Name of table where map data is stored
        /// </summary>
        internal static readonly string MAPTABLENAME = "NE_Pipeline_Segment_Map";

        public PipelineSegmentMap()
        {
        }

        internal static SqlDataAdapter MapAdapterSetup()
        {
            //Create the object to return:
            SqlDataAdapter da = new SqlDataAdapter();

            da.TableMappings.Add("Table", MAPTABLENAME);

            //Build SELECT query to return our data
            string query = $"SELECT NELINENUMBER,SEGMENTID FROM {MAPTABLENAME}";
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;

            //Assign the SELECT command to the DataAdapter
            da.SelectCommand = comm;

            //Build DELETE query to remove existing record
            query = $"DELETE FROM {MAPTABLENAME} WHERE SEGMENTID=@SEGMENTID";
            comm = new SqlCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = query;
            comm.CreateParameter();
            comm.Parameters.Add("@SEGMENTID", SqlDbType.Int, 0, "SEGMENTID");

            //Assign the DELETE command to the DataAdapter
            da.DeleteCommand = comm;

            return da;
        }
    }
}
