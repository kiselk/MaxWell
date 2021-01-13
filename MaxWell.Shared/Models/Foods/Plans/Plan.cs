using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using MaxWell.Models;

namespace MaxWell.Shared.Models.Foods.Plans
{
    public class Plan
    {
         [PrimaryKey, AutoIncrement]
        public int PlanId { get; set; }
      public Person Person { get; set; }
        public int PersonId { get; set; }

 
        public List<Meal> Meals { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
