using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace LDMClassLibrary
{
    public class ProjectDataAccess
    {
        private string SQLConnString;
        public ProjectDataAccess(string sqlconnstring)
        {
            SQLConnString = sqlconnstring;
        }
        public List<NE_HRUData> GetNE_HRUData()
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(SQLConnString))
            {
                var output = conn.Query<NE_HRUData>(Queries.NE_HRUData_SELECT).ToList();
                return output;
            }
        }

        /// <summary>
        /// Performs a query against existing data in NE_HRUData table to return the decimal precision 
        /// used on the TemperatureDisplayValue column.
        /// </summary>
        /// <returns>integer representing the number of decimal places used in temperature values</returns>
        public int GetNE_HRUData_TemperaturePrecision()
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(SQLConnString))
            {
                return conn.Query<int>(Queries.NE_HRUData_GET_TemperaturePrecision).First();
            }
        }

        /// <summary>
        /// Performs a query against existing data in NE_HRUData table to return the decimal precision 
        /// used on the PressureDisplayValue column.
        /// </summary>
        /// <returns>integer representing the number of decimal places used in pressure values</returns>
        public int GetNE_HRUData_PressurePrecision()
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(SQLConnString))
            {
                return conn.Query<int>(Queries.NE_HRUData_GET_PressurePrecision).First();
            }
        }

        /// <summary>
        /// Performs a query against the project table SETTINGS to retrieve the value for how many
        /// decimals temperature values should use.
        /// </summary>
        /// <returns>integer representing the number of decimal places used in temperature values</returns>
        public int GetSETTINGS_TemperaturePrecision()
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(SQLConnString))
            {
                return conn.Query<int>(Queries.SETTINGS_GET_TemperaturePrecision).First();
            }
        }

        /// <summary>
        /// Performs a query against the project table SETTINGS to retrieve the value for how many
        /// decimals pressure values should use.
        /// </summary>
        /// <returns>integer representing the number of decimal places used in temperature values</returns>
        public int GetSETTINGS_PressurePrecision()
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(SQLConnString))
            {
                return conn.Query<int>(Queries.SETTINGS_GET_PressurePrecision).First();
            }
        }

        /// <summary>
        /// Applies a value to SETTINGS table for temperature decimal precision
        /// </summary>
        /// <param name="newtempprecision">New value representing how many decimal places to use for temperature values</param>
        public void SetSETTINGS_TemperaturePrecision(int newtempprecision)
        {
            if (newtempprecision >= 0)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(SQLConnString))
                {
                    conn.Execute(Queries.SETTINGS_SET_TemperaturePrecision, new { @ROOT = "PROJECT", @SECTION = "SETUP", @KEYNAME = "TEMPERATUREPRECISION", @VALUE = newtempprecision });
                }
            }
            else
            {
                throw new ArgumentException("Invalid value passed, cannot be negative");
            }
        }

        /// <summary>
        /// Applies a value to SETTINGS table for pressure decimal precision
        /// </summary>
        /// <param name="newpresprecision">New value representing how many decimal places to use for pressure values</param>
        public void SetSETTINGS_PressurePrecision(int newpresprecision)
        {
            if (newpresprecision >= 0)
            {
                using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(SQLConnString))
                {
                    conn.Execute(Queries.SETTINGS_SET_PressurePrecision, new { @ROOT = "PROJECT", @SECTION = "SETUP", @KEYNAME = "PRESSUREPRECISION", @VALUE = newpresprecision });
                } 
            }
            else
            {
                throw new ArgumentException("Invalid value passed, cannot be negative");
            }
        }

        public List<NE_ExcelData> GetNE_ExcelData()
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(SQLConnString))
            {
                var result = conn.Query<NE_ExcelData>(Queries.NE_ExcelData_SELECT).ToList();
                return result;
            }
        }
    }
}
