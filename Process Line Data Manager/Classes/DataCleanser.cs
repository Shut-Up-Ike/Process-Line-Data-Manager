using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Process_Line_Data_Manager
{
    class DataCleanser
    {
        public DataCleanser() { }

        /// <summary>
        /// Scans the Column.DataType for System.String and performs String.Replace on the values to remove System.Environment.NewLine from the value.
        /// </summary>
        /// <param name="sourceTable">DataTable to cleanse.</param>
        public static void RemoveNewLineAndSpaces(ref DataTable sourceTable)
        {
            foreach(DataColumn col in sourceTable.Columns)
            {
                if(col.DataType == Type.GetType("System.String"))
                {
                    foreach(DataRow row in sourceTable.Rows)
                    {
                        string value = row[col.ColumnName].ToString();
                        value = System.Text.RegularExpressions.Regex.Replace(value, @"\t|\n|\r", string.Empty).Trim();
                        //row[col.ColumnName] = row[col.ColumnName].ToString().Replace(System.Environment.NewLine, string.Empty);
                        //row[col.ColumnName] = System.Text.RegularExpressions.Regex.Replace(row[col.ColumnName].ToString(), @"\t|\n|\r", string.Empty);
                        row[col.ColumnName] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Removes commas from any columns that have "display" and either "temperature" or "pressure" in the column name.
        /// </summary>
        /// <param name="sourceTable">Table to evaluate</param>
        public static void RemoveCommas(ref DataTable sourceTable)
        {
            foreach(DataColumn col in sourceTable.Columns)
            {
                //If the column has a name that contains "display" and it also contains either "temperature" or "pressure", then we need to make sure there are no commas in that text.
                if (col.ColumnName.ToLower().Contains("display") && (col.ColumnName.ToLower().Contains("temperature") || col.ColumnName.ToLower().Contains("pressure")))
                {
                    foreach (DataRow row in sourceTable.Rows)
                    {
                        row[col.ColumnName] = System.Text.RegularExpressions.Regex.Replace(row[col.ColumnName].ToString(), @",", string.Empty);
                    }
                }
            }
        }

        public static void Cleanse(ref DataTable sourceTable)
        {
            RemoveNewLineAndSpaces(ref sourceTable);
            RemoveCommas(ref sourceTable);
        }
    }
}
