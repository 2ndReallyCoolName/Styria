using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.MVVM.Model
{
    class TabNote
    {
        public int Id { get; set; }

        public ICollection<Note> Notes { get; set; }

        public int Duration { get; set; }

        [Required]
        public int Order { get; set; }

        public int EffectID { get; set; }
        public Effect Effect { get; set; }

        public int TabID { get; set; }
        public Tab Tab { get; set; }
    }
}
