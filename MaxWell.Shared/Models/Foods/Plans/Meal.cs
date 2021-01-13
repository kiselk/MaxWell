using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MaxWell.Shared.Models.Foods.Plans
{
    public class Meal
    {


        [PrimaryKey, AutoIncrement]
        public int MealId { get; set; }

        public Plan Plan { get; set; }
        public int PlanId { get; set; }

        public List<Dish> Dishes { get; set; }




        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDateTime { get; set; }
        public double? Amount { get; set; }
    }
}
