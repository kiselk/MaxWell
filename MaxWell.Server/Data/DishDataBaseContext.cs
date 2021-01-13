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
    public class DishDatabaseContext : DbContext
    {
        public DishDatabaseContext(DbContextOptions<DishDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().HasKey(nd => nd.DishId);
        }

        public DbSet<Dish> Dish { get; set; }
    }
}
