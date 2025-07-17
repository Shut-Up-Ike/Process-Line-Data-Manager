using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace LDMClassLibrary
{
    public class ExcelDataAccess
    {
        public List<ExcelData> GetExcelData(string excelconnstring)
        {
            using (var conn = new System.Data.OleDb.OleDbConnection(excelconnstring))
            {
                var output = conn.Query<ExcelData>(Queries.EXCEL_SELECT).ToList();
                return output;
            }
        }
    }
}
