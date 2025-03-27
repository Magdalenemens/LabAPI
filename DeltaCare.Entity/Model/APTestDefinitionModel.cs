using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class APTestDefinitionModel : RequestMode
    {
        public int? SNO { get; set; }
        public int? TD_ID { get; set; }
        public string? TEST_ID { get; set; }
        public string TCODE { get; set; }
        public string? Full_Name { get; set; }
        public string? S_TYPE { get; set; }
        public string? PRTY { get; set; }
        public string? Status { get; set; }
        public string? Ordable { get; set; }
        public string? PR { get; set; }
        public string? MDL { get; set; }
        public string? DIV { get; set; }
        public string? SECT { get; set; }
        public string? WC { get; set; }
        public string? TS { get; set; }
        public string? PRFX { get; set; }
        public string? MHN { get; set; }
        public string? SHN { get; set; }        
        public decimal? TAT { get; set; }
        [IgnoreParameter]
        public string? MTHD { get; set; }
        
    }
}
