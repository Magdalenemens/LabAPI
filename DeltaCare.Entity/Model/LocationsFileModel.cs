using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class LocationsFileModel:RequestMode
    {
        [Key]
        public int LOCATION_ID { get; set; }
        public string LOC { get; set; }
        public string DESCRP { get; set; }
        public string TP { get; set; }
    }
}
