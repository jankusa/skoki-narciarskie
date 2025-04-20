using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSkoki.Models
{
    public class Konkurs
    {
        [Key]
        public int Id { get; set; }

        required public int SezonID { get; set; }
        required public int SkoczniaID { get; set; }

        public Sezon? Sezon{ get; set; }

        public Skocznia? Skocznia{ get; set; }

        required public String Data { get; set; }
        
        [Display(Name = "Opis")]
        public String? Opis { get; set; }
    }
}