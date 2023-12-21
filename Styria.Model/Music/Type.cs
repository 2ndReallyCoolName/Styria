using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Music
{
    public class Type
    {
        [Key]
        public int ID { get; set; }

        public string? Name { get; set; }

        [Required]
        public int TypeGroupID { get; set; }
        public TypeGroup? TypeGroup { get; set; }

        public List<Note>? Notes { get; set; }
    }
}
