using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSkoki.Models
{
    public class Uzytkownicy
    {
        [Key]
        public int Id { get; set; }
        
        public required string Login { get; set; }

        public required String HashPassword { get; set; }

        public required String Token { get; set; } 
    }
}