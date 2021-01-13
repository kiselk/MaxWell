using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Models.Foods;
using MaxWell.Views;

namespace MaxWell.Services
{
	public interface IFoodService
	{
		    Task<List<Food>> RefreshFoodsAsync();
	    Task<List<Food>> GetFoodsByVkAsync(string id);
	    Task<List<Food>> GetFoodsForCurrentUser();


        Task SaveFoodAsync(Food food, bool isNewItem);
	    Task DeleteFoodAsync(int id);
	    Task<List<FoodDescription>> GetFoodDescriptionsAsync(string name);
	    Task<List<NutritionData>> GetNutritionDataAsync(string id);
	    Task<List<FoodDescription>> GetPhenylDescendingFoodDescAsync();
	    
    }
}
