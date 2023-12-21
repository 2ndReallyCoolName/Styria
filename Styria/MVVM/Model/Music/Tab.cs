using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styria.MVVM.Model.Song;

namespace Styria.MVVM.Model
{
    class Tab
    {
        public int ID { get; set; }
        
        public int InstrumentID { get; set; }
        public Song.Instrument Instrument { get; set; }

        public int TimeSignatureID { get; set; }
        public TimeSignature TimeSignature { get; set; } = new TimeSignature();

        public int SongID { get; set; }
        public Song.Song Song { get; set; }

        public ICollection<TabNote> tabNotes { get; set; } = new List<TabNote>();
    }
}
