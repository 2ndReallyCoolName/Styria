using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styria.MVVM.Model
{
    class Type
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public int TypeGroupID { get; set; }
        public TypeGroup TypeGroup { get; set; }
    }
}
