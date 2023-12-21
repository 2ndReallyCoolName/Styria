using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.MVVM.Model.Song
{
    class Artist
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public IList<Song> Songs { get; set; }
    }
}
