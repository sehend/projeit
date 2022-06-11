using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
   public class KanserTürleri
    {
        [Key]

        public int Id { get; set; }

        public string KanserTürüName { get; set; }
    }
}
