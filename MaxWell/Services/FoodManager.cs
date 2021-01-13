using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Models.Foods;
using MaxWell.Views;

namespace MaxWell.Services
{
	public class FoodManager
    {
		IFoodService foodService;

		public FoodManager(IFoodService service)
		{
			foodService = service;
		}

		public Task<List<Food>> GetFoodsAsync ()
		{
			return foodService.RefreshFoodsAsync ();	
		}
	    public Task<List<Food>> GetFoodsByVkAsync(string id)
	    {
	        return foodService.GetFoodsByVkAsync(id);
	    }

        public Task<List<Food>> GetFoodsForCurrentUser()
        {
            return foodService.GetFoodsForCurrentUser();
        }
        
        public Task<List<NutritionData>> GetNutritionDataAsync(string id)
        {
            return foodService.GetNutritionDataAsync(id);
        }
        public Task<List<FoodDescription>> GetFoodDescriptionsAsync(string name)
        {
            return foodService.GetFoodDescriptionsAsync(name);
        }
        public Task<List<FoodDescription>> GetPhenylDescendingFoodDescAsync()
        {
            return foodService.GetPhenylDescendingFoodDescAsync();
        }
        
        public Task SaveFoodAsync (Food item, bool isNewItem = false)
		{
			return foodService.SaveFoodAsync (item, isNewItem);
		}

		public Task DeleteFoodAsync (Food item)
		{
			return foodService.DeleteFoodAsync(item.FoodId);
		}
	}
}
