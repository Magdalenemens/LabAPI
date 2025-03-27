using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class TDComboModel:RequestMode 
    {
        public string TableName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        [MaxLength(35)]
        public string Description { get; set; } = string.Empty;
        public string ID { get; set; } =string.Empty;
    }
}
