using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSkoki.Models
{
    public class Punktacja
    {
        required public int PunktacjaID { get; set; }
        required public int SkoczekID { get; set; }
        required public int KonkursID { get; set; }
        public decimal Wynik {get; set; }
        public Skoczek? Skoczek { get; set; }
        public Konkurs? Konkurs { get; set; }
    }
}