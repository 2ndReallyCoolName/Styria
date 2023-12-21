using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Song
{
    public class Song
    {
        public int SongID { get; set; }
        public string Name { get; set; }

        public int ArtistID { get; set; }
        public Artist Artist { get; set; }

    }
}
