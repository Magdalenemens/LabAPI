using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class DivisionModel : RequestMode
    {
        public int LAB_DIV_ID { get; set; }

        [Required]
        [MaxLength(2)]
        [Range(01, 99, ErrorMessage = "Please enter 2 digit integer Number")]
        public string DIV { get; set; }

        [MaxLength(3)]
        public string ABRV { get; set; }

        [MaxLength(35)]
        public string DESCRIP { get; set; }

       
    }
}
