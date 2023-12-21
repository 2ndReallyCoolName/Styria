using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styria.Model.Song;

namespace Styria.Model.Music
{
    public class Tab
    {
        public int ID { get; set; }

        public int TimeSignatureID { get; set; }
        public TimeSignature? TimeSignature { get; set; }

        public int SongID { get; set; }
        public Song.Song? Song { get; set; }

        public List<TabNote>? TabNotes { get; set; }
    }
}
