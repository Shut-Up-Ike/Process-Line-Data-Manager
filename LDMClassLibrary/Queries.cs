using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMClassLibrary
{
    public static class Queries
    {
        public static string NE_HRUData_SELECT
        {
            get
            {
                return $"SELECT * FROM NE_HRUDATA";
            }
        }
        private static string NE_HRUData_GetPrecisionQuery(string temperature_or_pressure)
        {
            return $"SELECT TOP 1 CASE WHEN _CHINDX > 0 THEN LEN(_MAX)-_CHINDX ELSE 0 END FROM NE_HRUDATA OUTER APPLY(SELECT _MAX = MAX({temperature_or_pressure}VALUEDISPLAY) FROM NE_HRUDATA)MYMAX OUTER APPLY(SELECT _CHINDX = CHARINDEX('.',_MAX))CHINDX";
        }
        public static string NE_HRUData_GET_TemperaturePrecision
        {
            get
            {
                return NE_HRUData_GetPrecisionQuery("temperature");
            }
        }
        public static string NE_HRUData_GET_PressurePrecision
        {
            get
            {
                return NE_HRUData_GetPrecisionQuery("pressure");
            }
        }
        public static string SETTINGS_GET_TemperaturePrecision
        {
            get
            {
                return $"SELECT VALUE {SETTINGS_FROM_Temperature}";
            }
        }
        public static string SETTINGS_GET_PressurePrecision
        {
            get
            {
                return $"SELECT VALUE {SETTINGS_FROM_Pressure}";
            }
        }
        private static string SETTINGS_FROM_Pressure
        {
            get
            {
                return "FROM SETTINGS WHERE ROOT='PROJECT' AND SECTION='SETUP' AND KEYNAME='PRESSUREPRECISION'";
            }
        }
        private static string SETTINGS_FROM_Temperature
        {
            get
            {
                return "FROM SETTINGS WHERE ROOT='PROJECT' AND SECTION='SETUP' AND KEYNAME='TEMPERATUREPRECISION'";
            }
        }
        public static string SETTINGS_SET_TemperaturePrecision
        {
            get
            {
                return $"IF NOT EXISTS({SETTINGS_GET_TemperaturePrecision}) INSERT INTO SETTINGS (ROOT,SECTION,KEYNAME,VALUE) VALUES (@ROOT,@SECTION,@KEYNAME,@VALUE) ELSE UPDATE SETTINGS SET VALUE=@VALUE {SETTINGS_FROM_Temperature}";
            }
        }
        public static string SETTINGS_SET_PressurePrecision
        {
            get
            {
                return $"IF NOT EXISTS({SETTINGS_GET_PressurePrecision}) INSERT INTO SETTINGS (ROOT,SECTION,KEYNAME,VALUE) VALUES (@ROOT,@SECTION,@KEYNAME,@VALUE) ELSE UPDATE SETTINGS SET VALUE=@VALUE {SETTINGS_FROM_Pressure}";
            }
        }
        public static string NE_ExcelData_SELECT
        {
            get
            {
                return $"SELECT * FROM NE_EXCELDATA";
            }
        }
        public static string EXCEL_SELECT
        {
            get
            {
                return "SELECT DISTINCT * FROM [OUTPUTS$] WHERE DESCRIPTION IS NOT NULL AND DESCRIPTION <> ''";
            }
        }
    }
}
