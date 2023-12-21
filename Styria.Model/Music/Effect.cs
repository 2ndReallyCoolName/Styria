using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Music
{
    public class Effect
    {
        public int ID { get; set; }

        // Tie, Tuplet, 
        [Required]
        public string? Name { get; set; }

        public List<TabNote>? TabNotes { get; set; }
    }
}
