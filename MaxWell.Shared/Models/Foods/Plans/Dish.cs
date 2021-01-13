using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MaxWell.Shared.Models.Foods.Plans
{
    public class Dish
    {

     
        [PrimaryKey, AutoIncrement]
        public int DishId { get; set; }
   public Meal Meal { get; set; }
        public int MealId { get; set; }

        public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }


        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double? Amount { get; set; }
        public DateTime CreateDateTime { get; set; }

    }
}
