using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Models;
using SQLite;
using MaxWell.Models.Foods;

namespace MaxWell.Shared.Models.Foods.Plans
{
    public class Ingredient
    {
   
        [PrimaryKey, AutoIncrement]
        public int IngredientId { get; set; }
     public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }

        public Food Food { get; set; }
        public int FoodId { get; set; }

        public FoodDescription FoodDescription { get; set; }
        public int FoodDescriptionId { get; set; }



        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public double? Amount { get; set; }

        public DateTime CreateDateTime { get; set; }

    }
}
