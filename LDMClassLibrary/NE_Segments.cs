using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMClassLibrary
{
    public class NE_Segments
    {
        public int ID { get; set; }

        public int? HRUParentID { get; set; }

        public int? ExcelParentID { get; set; }

        public string SecondaryLineDescription { get; set; }

        public string Diameter { get; set; }

        public string WallThickness { get; set; }

        public string PipeMaterial { get; set; }

        public string CodeJurisdiction { get; set; }

        public string ANSIClass { get; set; }

        public string Pressure { get; set; }

        public string PressureUnits { get; set; }

        public string Temperature { get; set; }

        public string TemperatureUnits { get; set; }

        public double? InsulationThickness { get; set; }

        public string CorrosionAllowance { get; set; }

        public bool IsTubing { get; set; }

        public string maxOperatingTemperatureDisplay { get; set; }

        public string thicknessCasePressureDisplay { get; set; }

        public string thicknessCaseTemperatureDisplay { get; set; }

        public string pressureCasePressureDisplay { get; set; }

        public string pressureCaseTemperatureDisplay { get; set; }

        public string temperatureCasePressureDisplay { get; set; }

        public string temperatureCaseTemperatureDisplay { get; set; }

    }
}
