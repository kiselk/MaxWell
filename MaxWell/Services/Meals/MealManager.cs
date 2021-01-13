using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Meals
{
    public class MealManager
    {
        IMealService mealService;

        public MealManager(IMealService service)
        {
            mealService = service;
        }

        public Task<List<Meal>> GetMealsAsync ()
        {
            return mealService.RefreshMealsAsync ();	
        }
        public Task<Meal> GetMealAsync (int id)
        {
            return mealService.GetMealAsync(id);
        }
            
        public Task SaveMealAsync (Meal item, bool isNewItem = false)
        {
    	return mealService.SaveMealAsync (item, isNewItem);
        }
    
        public Task DeleteMealAsync (Meal item)
        {
    	return mealService.DeleteMealAsync(item.MealId);
        }
    }
}