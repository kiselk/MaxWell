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
    public class RecipeDatabaseContext : DbContext
    {
        public RecipeDatabaseContext(DbContextOptions<RecipeDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasKey(nd => nd.RecipeId);
        }

        public DbSet<Recipe> Recipe { get; set; }
    }
}
