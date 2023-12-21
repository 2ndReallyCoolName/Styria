﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Song
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Song> Songs { get; set; }
    }
}
