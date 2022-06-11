using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
     public class TüpCinsi
    {
        [Key]

        public int Id { get; set; }

        public string TüpCinsiName { get; set; }
    }
}
