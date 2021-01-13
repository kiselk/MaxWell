using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Data
{
    public class IngredientDatabaseContext : DbContext
    {
        public IngredientDatabaseContext(DbContextOptions<IngredientDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasKey(nd => nd.IngredientId);
        }

        public DbSet<Ingredient> Ingredient { get; set; }
    }
}
