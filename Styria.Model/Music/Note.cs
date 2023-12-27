using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Styria.Model.Song;

namespace Styria.Model.Music
{
    public class Note
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public char String { get; set; }

        public int Fret { get; set; }

        public int InstrumentID { get; set; }
        public Instrument? Instrument { get; set; }


        // Ghost note, dead note, Pinch harmonic, natural harmonic, vibrato, palm mute
        public int? TypeID { get; set; }
        public Type? Type { get; set; }

        public string SoundFilePath { get; set; } = string.Empty;
    }
}
