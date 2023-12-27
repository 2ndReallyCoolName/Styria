using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Music
{
    public class NoteTabNote
    {
        [Required]
        public int TabNoteID { get; set; }
        public TabNote? TabNote { get; set; }

        [Required]
        public int NoteID { get; set; }
        public Note? Note { get; set; }
    }
}
