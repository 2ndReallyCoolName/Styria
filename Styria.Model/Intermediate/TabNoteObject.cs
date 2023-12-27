using Styria.Model.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Intermediate
{
    public class TabNoteObject
    {
        public int TabNoteID { get; set; }
        public List<Note> Notes { get; set; } = new ();
    }
}
