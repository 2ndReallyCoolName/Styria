using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.MVVM.Model
{
    class TimeSignature
    {
        [Key]
        public int TimeSignatureID { get; set; }

        [Required]
        public int beats { get; set; }

        public int noteValue { get; set; }   
    }
}
