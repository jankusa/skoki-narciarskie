using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSkoki.Models
{
    public class Skoczek
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Imię")]
        public required String Imie {get; set;} 

        [Display(Name = "Nazwisko")]
        public required String Nazwisko {get; set;}

        [Display(Name = "Rok urodzenia")]
        public int Rok_urodzenia {get; set;}  

        [Display(Name = "Wzrost [cm]")]
        public int Wzrost {get; set;} 

        [Display(Name = "Narodowość")]
        public required String Narodowosc {get; set;} 

    }
}