using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Meals
{
	public interface IMealService
	{
	    Task<List<Meal>> RefreshMealsAsync();
	    Task<Meal> GetMealAsync(int id);
	    Task SaveMealAsync(Meal meal, bool isNewItem);
	    Task DeleteMealAsync(int id);
	    
    }
}
