using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.MVVM.Model
{
    class Effect
    {
        public int ID { get; set; }

        // Tie, Tuplet, 
        public string Name { get; set; }

        public bool Over { get; set; } = false;
    }
}
