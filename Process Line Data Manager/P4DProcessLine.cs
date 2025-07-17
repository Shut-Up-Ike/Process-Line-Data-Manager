using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process_Line_Data_Manager
{
    public class P4DProcessLine
    {
        [DataNames("ID","IDENTIFIER")]
        int Identifier { get; set; }

        [DataNames("HRUParentLineNo")]
        int? HRUParentLineNo { get; set; }

        [DataNames("NELineNumber","NELineNo")]
        string NELineNumber { get; set; }

        [DataNames("MainLineDescription","LineDescription")]
        string MainLineDescription { get; set; }

        [DataNames("SecondaryLineDescription")]
        string SecondaryLineDescription { get; set; }

        [DataNames("Diameter")]
        string Diameter { get; set; }

        [DataNames("WallThickness","WallThk","Schedule")]
        string WallThickness { get; set; }

        [DataNames("LineMaterial","PipeMaterial")]
        string PipeMaterial { get; set; }

        [DataNames("CodeJurisdiction", "Code")]
        string CodeJurisdiction { get; set; }

        [DataNames("ANSIClass","ansi_class","Class")]
        string ANSIClass { get; set; }

        [DataNames("Pressure","DesPres","Despress")]
        float Pressure { get; set; }

        [DataNames("PressureUnit")]
        string PressureUnit { get; set; }

        [DataNames("Temperature","DesignTemp")]
        float Temperature { get; set; }

        [DataNames("TemperatureUnit")]
        string TemperatureUnit { get; set; }

        [DataNames("InsulationThickness","InsThk")]
        float InsulationThickness { get; set; }

        [DataNames("CorrosionAllowance")]
        float CorrosionAllowance { get; set; }

        public P4DProcessLine() { }
        
        public P4DProcessLine(int hruparentlineno, string mainlinedescription, string diameter, string wallthickness, string pipematerial, string ansiclass, float pressure, string pressureunit, float temperature, string temperatureunit, float insulationthickness)
        {
            HRUParentLineNo = hruparentlineno;
            MainLineDescription = mainlinedescription;
            Diameter = diameter;
            WallThickness = wallthickness;
            PipeMaterial = pipematerial;
            ANSIClass = ansiclass;
            Pressure = pressure;
            PressureUnit = pressureunit;
            Temperature = temperature;
            TemperatureUnit = temperatureunit;
            InsulationThickness = insulationthickness;
        }
    }
}
