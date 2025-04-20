using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSkoki.Models
{
    public class Skocznia
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Nazwa")]
        public required String Nazwa { get; set; }

        [Display(Name = "Miejscowość")]
        public required String Miejscowosc { get; set; }

        [Display(Name = "Państwo")]
        public required String Panstwo { get; set; }

        [Display(Name = "Punkt konstrukcyjny [m]")]
        public int K { get; set; }

        [Display(Name = "Rozmiar skoczni [m]")]
        public int HS { get; set; }

        [Display(Name = "Rekord skoczni [m]")]
        public decimal Rekord { get; set; }

        [Display(Name = "Rekordzista (SkoczekID)")]
        required public int SkoczekID { get; set; }
        [Display(Name = "Rekordzista (SkoczekID)")]
        public Skoczek? Skoczek { get; set; }
    }
}