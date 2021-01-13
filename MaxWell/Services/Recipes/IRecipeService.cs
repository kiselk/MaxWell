using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Recipes
{
	public interface IRecipeService
	{
	    Task<List<Recipe>> RefreshRecipesAsync();
	    Task<Recipe> GetRecipeAsync(int id);
	    Task<Recipe> SaveRecipeAsync(Recipe recipe, bool isNewItem);
	    Task DeleteRecipeAsync(int id);
	    
    }
}
