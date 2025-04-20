using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSkoki.Models
{
    public class Sezon
    {
        [Key]
        public int Id { get; set; }
        public required string Rok { get; set; }
    }
}