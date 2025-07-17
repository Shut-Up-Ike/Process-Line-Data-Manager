using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMClassLibrary
{
    /// <summary>
    /// Represents data that is being imported out of Excel
    /// </summary>
    public class ExcelData : IThermalData, IEquatable<ExcelData>
    {
        public string Description { get; set; }
        public string Size { get; set; }
        public string PipeThickness { get; set; }
        public string Material { get; set; }
        public double? PressureValue { get; set; }
        public double? TemperatureValue { get; set; }
        public string ANSI_class { get; set; }
        public string CodeJurisdiction { get; set; }

        public override int GetHashCode()
        {
            return (this.Description, this.Size, this.PipeThickness, this.Material, this.PressureValue, this.TemperatureValue, this.ANSI_class, this.CodeJurisdiction).GetHashCode();
        }
        public bool Equals(ExcelData other)
        {
            if (other == null)
            {
                return false;
            }
            if (this.Description == other.Description && this.Size == other.Size && this.PipeThickness == other.PipeThickness && this.Material == other.Material && this.PressureValue == other.PressureValue && this.TemperatureValue == other.TemperatureValue && this.ANSI_class == other.ANSI_class && this.CodeJurisdiction == other.CodeJurisdiction)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
