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
    public class PlanDatabaseContext : DbContext
    {
        public PlanDatabaseContext(DbContextOptions<PlanDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan>().HasKey(nd => nd.PlanId);
        }

        public DbSet<Plan> Plan { get; set; }
    }
}
