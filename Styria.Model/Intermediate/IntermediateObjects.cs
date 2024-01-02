using Styria.Model.Music;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Intermediate
{
    public class TabNoteObject
    {
        public int TabNoteID { get; set; }
        public List<Note> Notes { get; set; } = new();
    }

    public class TabNoteCreateObject
    {
        [Required]
        public int Duration { get; set; }

        [Required]
        public int Order { get; set; }
        public int? EffectID { get; set; }

        public int TabID { get; set; }

        public List<int> NoteIDs { get; set; } = new List<int>();
    }

    public class TabCreateObject
    {

        [Required]
        public int TimeSignatureID { get; set; }

        [Required]
        public int SongID { get; set; }

        [Required]
        public List<TabNoteCreateObject> TabNoteCreateObjects { get; set; } = new List<TabNoteCreateObject>();

    }

    public class TabNoteUpdateObject
    {
        [Required]
        public int TabNoteID { get; set; }
        [Required]
        public int Duration { get; set; }

        [Required]
        public int Order { get; set; }
        public int? EffectID { get; set; }

        public List<int> NoteIDs { get; set; } = new List<int>();
    }
}
