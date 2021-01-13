using System;
using System.Collections.Generic;
using MaxWell.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MaxWell.Models.Foods;
using MaxWell.Shared.Models.Foods;

namespace MaxWell.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*    migrationBuilder.CreateTable(name: "TodoItem", columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Done = table.Column<bool>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    icon = table.Column<byte[]>(nullable: true),
                    image = table.Column<byte[]>(nullable: true)
                }, constraints: table => { table.PrimaryKey("PK_TodoItem", x => x.Id); });



                migrationBuilder.CreateTable(name: "Person", columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    VK = table.Column<string>(nullable: true),
                    icon = table.Column<byte[]>(nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    VKUserId = table.Column<string>(nullable: true),
                        FbUserId = table.Column<string>(nullable: true),
                PlanId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Cats = table.Column<string>(nullable: true),
                    Pitomniki = table.Column<string>(nullable: true),
                    PlanId = table.Column<int>(nullable: true),
                    Cats = table.Column<string>(nullable: true)
                }, constraints: table => { table.PrimaryKey("PK_Person", x => x.Id); });


                migrationBuilder.CreateTable(name: "RemoteImage", columns: table => new {
                Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn), 
                  ImageUrl = table.Column<string>(nullable: true),
              DownloadedImageBlob = table.Column<byte[]>(nullable: true)
                }, constraints: table => { table.PrimaryKey("PK_RemoteImage", x => x.Id); });




                migrationBuilder.CreateTable(name: "Food", columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    icon = table.Column<byte[]>(nullable: true),
                    image = table.Column<byte[]>(nullable: true),
                    VKUserId = table.Column<string>(nullable: true),
                    Protein_g = table.Column<double>(nullable: true),
                    Fats_g = table.Column<double>(nullable: true),
                    Carbs_g = table.Column<double>(nullable: true),
                    Calcium_mg = table.Column<double>(nullable: true),
                    Phenylalanine_g = table.Column<double>(nullable: true),
                    NutritionDataCollection = table.Column<string>(nullable: true),
                    FoodDescriptionId = table.Column<int>(nullable: true)
                }, constraints: table => { table.PrimaryKey("PK_Food", x => x.FoodId); });

           migrationBuilder.CreateTable(name: "Ingredient", columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: true),
                    IngredientId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodId = table.Column<int>(nullable: true),
                    FoodDescriptionId = table.Column<int>(nullable: true),

                    PersonId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: true),

                    CreateDateTime = table.Column<DateTime>(nullable: false)
       constraints: table => { table.PrimaryKey("PK_Ingredient", x => x.IngredientId); });

            },    */

            migrationBuilder.CreateTable(name: "Meal", columns: table => new
            {
                DishId = table.Column<int>(nullable: true),
                MealId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                PlanId = table.Column<int>(nullable: true),
                PersonId = table.Column<int>(nullable: true),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                Amount = table.Column<double>(nullable: true),
                ImageUrl = table.Column<string>(nullable: true),
                CreateDateTime = table.Column<DateTime>(nullable: false)
            }, constraints: table => { table.PrimaryKey("PK_Meal", x => x.MealId); });




            migrationBuilder.CreateTable(name: "Dish", columns: table => new
            {
                RecipeId = table.Column<int>(nullable: true),
                DishId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                MealId = table.Column<int>(nullable: true),
                PersonId = table.Column<int>(nullable: true),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                Amount = table.Column<double>(nullable: true),
                ImageUrl = table.Column<string>(nullable: true),
           
                CreateDateTime = table.Column<DateTime>(nullable: false)
            }, constraints: table => { table.PrimaryKey("PK_Dish", x => x.DishId); });


            /*

                            migrationBuilder.CreateTable(name: "Recipe", columns: table => new
                            {

                                RecipeId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                                DishId = table.Column<int>(nullable: true),

                                PersonId = table.Column<int>(nullable: true),
                                Name = table.Column<string>(nullable: true),
                                Description = table.Column<string>(nullable: true),
                                Amount = table.Column<double>(nullable: true),
                                ImageUrl = table.Column<string>(nullable: true),

                                CreateDateTime = table.Column<DateTime>(nullable: false)

                            }, constraints: table => { table.PrimaryKey("PK_Recipe", x => x.RecipeId); });





                            migrationBuilder.CreateTable(name: "Dish", columns: table => new
                            {

                                DishId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                                RecipeId = table.Column<int>(nullable: true),
                                MealId = table.Column<int>(nullable: true),
                                PersonId = table.Column<int>(nullable: true),
                                Name = table.Column<string>(nullable: true),
                                Description = table.Column<string>(nullable: true),
                                Amount = table.Column<double>(nullable: true),
                                ImageUrl = table.Column<string>(nullable: true),

                                CreateDateTime = table.Column<DateTime>(nullable: false)
                            }, constraints: table => { table.PrimaryKey("PK_Dish", x => x.DishId); });



                            migrationBuilder.CreateTable(name: "Meal", columns: table => new
                            {
                                DishId = table.Column<int>(nullable: true),
                                MealId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                                PersonId = table.Column<int>(nullable: true),
                                Name = table.Column<string>(nullable: true),
                                Description = table.Column<string>(nullable: true),
                                Amount = table.Column<double>(nullable: true),
                                ImageUrl = table.Column<string>(nullable: true),
                                CreateDateTime = table.Column<DateTime>(nullable: false)
                            }, constraints: table => { table.PrimaryKey("PK_Meal", x => x.MealId); });



                            migrationBuilder.CreateTable(name: "Plan", columns: table => new
                            {
                                PersonId = table.Column<int>(nullable: true),
                                PlanId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                                Name = table.Column<string>(nullable: true),
                                Description = table.Column<string>(nullable: true),
                                ImageUrl = table.Column<string>(nullable: true),
                                CreateDateTime = table.Column<DateTime>(nullable: false)
                            }, constraints: table => { table.PrimaryKey("PK_Plan", x => x.PlanId); });
                      */       /*
                                       migrationBuilder.CreateTable(name: "FoodInfo", columns: table => new
                                       {
                                           Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                                           Group = table.Column<string>(nullable: true),
                                           Desc = table.Column<string>(nullable: true),
                                           Shrt_Desc = table.Column<string>(nullable: true),
                                           Shrt_Desc_Ru = table.Column<string>(nullable: true),
                                           icon = table.Column<byte[]>(nullable: true),
                                           image = table.Column<byte[]>(nullable: true),
                                           Water_g = table.Column<double>(nullable: true),
                                           Energ_Kcal = table.Column<double>(nullable: true),
                                           Protein_g = table.Column<double>(nullable: true),
                                           Lipid_Tot_g = table.Column<double>(nullable: true),
                                           Ash_g = table.Column<double>(nullable: true),
                                           Carbohydrt_g = table.Column<double>(nullable: true),
                                           Fiber_TD_g = table.Column<double>(nullable: true),
                                           Sugar_Tot_g = table.Column<double>(nullable: true),
                                           Calcium_mg = table.Column<double>(nullable: true),
                                           Iron_mg = table.Column<double>(nullable: true),
                                           Magnesium_mg = table.Column<double>(nullable: true),
                                           Phosphorus_mg = table.Column<double>(nullable: true),
                                           Potassium_mg = table.Column<double>(nullable: true),
                                           Sodium_mg = table.Column<double>(nullable: true),
                                           Zinc_mg = table.Column<double>(nullable: true),
                                           Copper_mg = table.Column<double>(nullable: true),
                                           Manganese_mg = table.Column<double>(nullable: true),
                                           Selenium_µg = table.Column<double>(nullable: true),
                                           Vit_C_mg = table.Column<double>(nullable: true),
                                           Thiamin_mg = table.Column<double>(nullable: true),
                                           Riboflavin_mg = table.Column<double>(nullable: true),
                                           Niacin_mg = table.Column<double>(nullable: true),
                                           Panto_Acid_mg = table.Column<double>(nullable: true),
                                           Vit_B6_mg = table.Column<double>(nullable: true),
                                           Folate_Tot_ug = table.Column<double>(nullable: true),
                                           Folic_Acid_ug = table.Column<double>(nullable: true),
                                           Food_Folate_ug = table.Column<double>(nullable: true),
                                           Folate_DFE_ug = table.Column<double>(nullable: true),
                                           Choline_Tot_mg = table.Column<double>(nullable: true),
                                           Vit_B12_ug = table.Column<double>(nullable: true),
                                           Vit_A_IU = table.Column<double>(nullable: true),
                                           Vit_A_RAE = table.Column<double>(nullable: true),
                                           Retinol_ug = table.Column<double>(nullable: true),
                                           Alpha_Carot_ug = table.Column<double>(nullable: true),
                                           Beta_Carot_ug = table.Column<double>(nullable: true),
                                           Beta_Crypt_ug = table.Column<double>(nullable: true),
                                           Lycopene_ug = table.Column<double>(nullable: true),
                                           Lut_Zea_ug = table.Column<double>(nullable: true),
                                           Vit_E_mg = table.Column<double>(nullable: true),
                                           Vit_D_ug = table.Column<double>(nullable: true),
                                           Vit_D_IU = table.Column<double>(nullable: true),
                                           Vit_K_ug = table.Column<double>(nullable: true),
                                           FA_Sat_g = table.Column<double>(nullable: true),
                                           FA_Mono_g = table.Column<double>(nullable: true),
                                           FA_Poly_g = table.Column<double>(nullable: true),
                                           Cholestrl_mg = table.Column<double>(nullable: true),
                                           GmWt_1 = table.Column<double>(nullable: true),
                                           GmWt_Desc1 = table.Column<string>(nullable: true),
                                           GmWt_2 = table.Column<string>(nullable: true),
                                           GmWt_Desc2 = table.Column<double>(nullable: true),
                                           Refuse_Pct = table.Column<double>(nullable: true),
                                           Phenylalanine_g = table.Column<double>(nullable: true)

                                       }, constraints: table => { table.PrimaryKey("PK_FoodInfo", x => x.Id); });
                                       */

            /*
            migrationBuilder.CreateTable(name: "Allergen", columns: table => new
            {
                AllergenId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                CreateDateTime = table.Column<DateTime>(nullable: true),
                PersonId = table.Column<int>(nullable: true),
                ImageUrl = table.Column<string>(nullable: true)
            }, constraints: table => { table.PrimaryKey("PK_Allergen", x => x.AllergenId); });
            */


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItem");
            migrationBuilder.DropTable(
                name: "Person");
            migrationBuilder.DropTable(
                name: "RemoteImage");
            migrationBuilder.DropTable(
                name: "Food");
        }
    }
}
