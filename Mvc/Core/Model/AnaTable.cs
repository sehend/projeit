using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
   public class AnaTable
    {
        [Key]

        public int Id { get; set; }


        public string Alıcı { get; set; }

        public string Gönderici { get; set; }


        public string ProjeName { get; set; }

        public string ProjectStatus { get; set; }

        public string ProjectType { get; set; }

        public string SponsorName { get; set; }

        public string PatientSpecialization { get; set; }

        public string CancerSpecies { get; set; }

        public string MaterialType { get; set; }

        public string MaterialQuantity { get; set; }

        public string TubeType { get; set; }

        public string Phase{ get; set; }

        public string Tarih { get; set; }



    }
}
