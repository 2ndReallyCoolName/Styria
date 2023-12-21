using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.MVVM.Model.Music
{
    class NoteTabNoteMapping
    {
        public int ID { get; set; }
        public int TabNoteID { get; set; }
        public TabNote  TabNote { get; set; }

        public int NoteID { get; set; }
        public Note Note { get; set; }
    }
}
