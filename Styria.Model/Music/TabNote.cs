using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Music
{
    public class TabNote
    {
        public int ID { get; set; }

        public int Duration { get; set; }

        [Required]
        public int Order { get; set; }

        public int? EffectID { get; set; }
        public Effect? Effect { get; set; }

        public int TabID { get; set; }
        public Tab? Tab { get; set; }

        public List<NoteTabNote> NoteTabNotes { get; set; } = new();
    }
}
