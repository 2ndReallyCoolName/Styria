using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.Model.Music
{
    public class TypeGroup
    {
        public int ID { get; set; }

        [Required]
        public string? Name { get; set; }

        public List<Type>? Types { get; set; }
    }
}
