using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Music
{
    public class TimeSignature
    {
        [Key]
        public int TimeSignatureID { get; set; }

        [Required]
        public int Beats { get; set; }

        [Required]
        public int NoteValue { get; set; }

        public List<Tab>? Tabs { get; set; }
    }
}
