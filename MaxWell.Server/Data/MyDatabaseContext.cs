using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Models.Foods;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods;
using MaxWell.Shared.Models.Foods.Plans;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Data
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NutritionData>().HasKey(nd => new { nd.FoodDescriptionId, nd.NutritionDefinitionId });
            modelBuilder.Entity<NutritionDefinition>().HasKey(nd => nd.Id );
            modelBuilder.Entity<FoodDescription>().HasKey(fd =>  fd.FoodDescriptionId );


            modelBuilder.Entity<NutritionData>()
                     .HasOne<FoodDescription>(nd => nd.FoodDescription)
                     .WithMany(fd => fd.NutritionDataCollection)
                     .HasForeignKey(nd => nd.FoodDescriptionId);


                 modelBuilder.Entity<NutritionData>()
                     .HasOne<NutritionDefinition>(NutritionData => NutritionData.NutritionDefinition)
                     .WithMany(NutritionDefinition => NutritionDefinition.NutritionDataCollection)
                     .HasForeignKey(NutritionData => NutritionData.NutritionDefinitionId);



            modelBuilder.Entity<Ingredient>().HasKey(fd => fd.IngredientId);
                        modelBuilder.Entity<Recipe>().HasKey(fd => fd.RecipeId);
                        modelBuilder.Entity<Dish>().HasKey(fd => fd.DishId);
                        modelBuilder.Entity<Meal>().HasKey(fd => fd.MealId);
                        modelBuilder.Entity<Plan>().HasKey(fd => fd.PlanId);


            modelBuilder.Entity<Ingredient>().Property(p => p.IngredientId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Recipe>().Property(p => p.RecipeId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Dish>().Property(p => p.DishId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Meal>().Property(p => p.MealId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Plan>().Property(p => p.PlanId).ValueGeneratedOnAdd();

            /*
             working version cant save objects
                        modelBuilder.Entity<Person>().HasOne<Plan>(p=>p.Plan).WithOne(p=>p.Person).HasForeignKey<Plan>(p => p.PlanId); ;
                        modelBuilder.Entity<Plan>().HasMany<Meal>(p=>p.Meals).WithOne(p=>p.Plan).HasForeignKey(p=>p.MealId);
                        modelBuilder.Entity<Meal>().HasMany<Dish>(p => p.Dishes).WithOne(p => p.Meal).HasForeignKey(p => p.MealId);
                        modelBuilder.Entity<Dish>().HasOne<Recipe>(d=>d.Recipe).WithOne(p=>p.Dish).HasForeignKey<Dish>(p=>p.DishId);
                        modelBuilder.Entity<Recipe>().HasMany<Ingredient>(r=>r.Ingredients).WithOne(p=>p.Recipe).HasForeignKey(ing => ing.RecipeId);
                        modelBuilder.Entity<Ingredient>().HasOne<Food>(p => p.Food).WithMany().HasForeignKey(p=>p.FoodId);
                        modelBuilder.Entity<Ingredient>().HasOne<FoodDescription>(p => p.FoodDescription).WithMany().HasForeignKey(p => p.FoodDescriptionId);
                        */




            //cant save meal fix
            modelBuilder.Entity<Person>().HasOne<Plan>(p => p.Plan).WithOne(p => p.Person).HasForeignKey<Plan>(p => p.PlanId); ;
               modelBuilder.Entity<Plan>().HasMany<Meal>(p => p.Meals).WithOne(p => p.Plan).HasForeignKey(p => p.MealId);
               modelBuilder.Entity<Meal>().HasMany<Dish>(p => p.Dishes).WithOne(p => p.Meal).HasForeignKey(p => p.MealId);
               modelBuilder.Entity<Dish>().HasOne<Recipe>(d => d.Recipe).WithOne(p=>p.Dish).HasForeignKey<Recipe>(p=>p.RecipeId);
               modelBuilder.Entity<Recipe>().HasMany<Ingredient>(r => r.Ingredients).WithOne(p => p.Recipe).HasForeignKey(ing => ing.RecipeId);
               modelBuilder.Entity<Ingredient>().HasOne<Food>(p => p.Food).WithMany().HasForeignKey(p => p.FoodId);
               modelBuilder.Entity<Ingredient>().HasOne<FoodDescription>(p => p.FoodDescription).WithMany().HasForeignKey(p => p.FoodDescriptionId);
               
            

            /*

            modelBuilder.Entity<Ingredient>().HasOne<Recipe>(ing => ing.Recipe).WithMany(re => re.Ingredients).HasForeignKey(ing => ing.RecipeId);
            modelBuilder.Entity<Recipe>().HasOne<Dish>(re => re.Dish).WithOne(p=>p.Recipe).HasForeignKey<Recipe>(b => b.RecipeId);
            modelBuilder.Entity<Dish>().HasOne<Meal>(di => di.Meal).WithMany(p=>p.Dishes).HasForeignKey(ing => ing.DishId);
            modelBuilder.Entity<Meal>().HasOne<Plan>(me => me.Plan).WithMany(pl => pl.Meals).HasForeignKey(ing => ing.PlanId);
            modelBuilder.Entity<Meal>().HasMany<Dish>(me => me.Dishes).WithOne(p=>p.Meal).HasForeignKey(p=>p.DishId);

            modelBuilder.Entity<Plan>().HasOne<Person>(pl => pl.Person).WithOne(p=>p.Plan).HasForeignKey<Plan>(b => b.PlanId);
            */



        }
        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<RemoteImage> RemoteImage { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<FoodInfo> FoodInfo { get; set; }
        public DbSet<NutritionData> NutritionData { get; set; }
        public DbSet<NutritionDefinition> NutritionDefinition { get; set; }
        public DbSet<FoodWeight> FoodWeight { get; set; }
        public DbSet<FoodDescription> FoodDescription { get; set; }
        public DbSet<FoodGroup> FoodGroup { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
         public DbSet<Recipe> Recipe { get; set; }
       public DbSet<Dish> Dish { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Allergen> Allergen { get; set; }
    }
}
