using Styria.Model.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Song
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Tab> Tabs { get; set; }

        public List<Note> Notes { get; set; }
    }
}
