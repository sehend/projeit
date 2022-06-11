using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
   public class HastaUzmanlık
    {
        [Key]

        public int Id { get; set; }

        public string HastaUzmanlıkName { get; set; }
    }
}
