using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace MaxWell.Shared.Models.Foods.Plans
{
    public class Recipe
    {
      
         [PrimaryKey, AutoIncrement]
        public int RecipeId { get; set; }


        public int PersonId { get; set; }

        public Dish Dish { get; set; }
        public int DishId { get; set; }

    
        public List<Ingredient> Ingredients { get; set; }




        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDateTime { get; set; }


        [JsonIgnore]
        public double? Weight => GetWeight();

        public double? GetWeight()
        {
            double? weight = 0;
            try
            {
                foreach(Ingredient ingr in Ingredients)
                {
                    weight += ingr.Amount;
                }

                return weight;
            }
            catch (Exception e)
            {

                return null;
            }
        }

    }
}
