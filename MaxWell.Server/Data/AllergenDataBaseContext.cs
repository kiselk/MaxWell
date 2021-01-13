using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Data
{
    public class AllergenDatabaseContext : DbContext
    {
        public AllergenDatabaseContext(DbContextOptions<AllergenDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allergen>().HasKey(nd => nd.AllergenId); 
        }

        public DbSet<Allergen> Allergen { get; set; }
    }
}
