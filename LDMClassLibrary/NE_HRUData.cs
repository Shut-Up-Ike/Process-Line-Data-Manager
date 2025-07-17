using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMClassLibrary
{
    public class NE_HRUData
    {
        /// <summary>
        /// Table record identifier for LDM/P4D. Not in HRU.
        /// </summary>
        public int ID { get; set; }
        public string job_no { get; set; }

        /// <summary>
        /// Line number used in HRU Data Sheets
        /// </summary>
        public byte line_no { get; set; }

        /// <summary>
        /// Equavalent to NELineNumber in P4D
        /// </summary>
        public string line_id_no { get; set; }
        public string description { get; set; }
        public string qty { get; set; }
        public double? Size { get; set; }
        public double? PipeOD { get; set; }
        public string PipeThickness { get; set; }
        public string PipeThicknessDisplay { get; set; }
        public string PipeThicknessUnit { get; set; }
        public string Material { get; set; }
        public double? PressureValue { get; set; }
        public string PressureValueDisplay { get; set; }
        public string PressureUnit { get; set; }
        public double? TemperatureValue { get; set; }
        public string TemperatureValueDisplay { get; set; }
        public string TemperatureUnit { get; set; }
        public double? InsulationThickness { get; set; }
        public string InsulationThicknessDisplay { get; set; }
        public string InsulationThicknessUnit { get; set; }
        public double? maxOperatingTemperature { get; set; }
        public string maxOperatingTemperatureDisplay { get; set; }
        public double? thicknessCasePressure { get; set; }
        public string thicknessCasePressureDisplay { get; set; }
        public double? thicknessCaseTemperature { get; set; }
        public string thicknessCaseTemperatureDisplay { get; set; }
        public double? pressureCasePressure { get; set; }
        public string pressureCasePressureDisplay { get; set; }
        public double? pressureCaseTemperature { get; set; }
        public string pressureCaseTemperatureDisplay { get; set; }
        public double? temperatureCasePressure { get; set; }
        public string temperatureCasePressureDisplay { get; set; }
        public double? temperatureCaseTemperature { get; set; }
        public string temperatureCaseTemperatureDisplay { get; set; }
        public string ansi_class { get; set; }
        public short? revision_no { get; set; }
    }
}
