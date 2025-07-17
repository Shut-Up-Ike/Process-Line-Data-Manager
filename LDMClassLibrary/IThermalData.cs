using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMClassLibrary
{
    /// <summary>
    /// Interface for all thermal data classes
    /// </summary>
    public interface IThermalData
    {
        string Description { get; set; }
        string Size { get; set; }
        string PipeThickness { get; set; }
        string Material { get; set; }
        double? PressureValue { get; set; }
        double? TemperatureValue { get; set; }
        string ANSI_class { get; set; }
    }
}
