using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcSkoki.Models;

namespace MvcSkoki.Data
{
    public class MvcSkokiContext : DbContext
    {
        public MvcSkokiContext (DbContextOptions<MvcSkokiContext> options)
            : base(options)
        {
        }

        public DbSet<MvcSkoki.Models.Konkurs> Konkurs { get; set; } = default!;

        public DbSet<MvcSkoki.Models.Skocznia> Skocznia { get; set; } = default!;

        public DbSet<MvcSkoki.Models.Sezon> Sezon { get; set; } = default!;

        public DbSet<MvcSkoki.Models.Skoczek> Skoczek { get; set; } = default!;

        public DbSet<MvcSkoki.Models.Punktacja> Punktacja { get; set; } = default!;
        public DbSet<MvcSkoki.Models.Uzytkownicy> Uzytkownicy { get; set; } = default!;
    }
}
