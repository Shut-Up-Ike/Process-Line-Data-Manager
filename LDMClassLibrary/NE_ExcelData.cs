using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMClassLibrary
{
    public class NE_ExcelData : ExcelData, IEquatable<NE_ExcelData>
    {
        public int ID { get; set; }

        public bool Equals(NE_ExcelData other)
        {
            return (base.Equals(other) && this.ID == other.ID);
        }
    }
}
