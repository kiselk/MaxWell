using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Ingredients
{
	public interface IIngredientService
	{
	    Task<List<Ingredient>> RefreshIngredientsAsync();
	    Task<Ingredient> GetIngredientAsync(int id);
	    Task<List<Ingredient>> GetIngredientsByRecipeId(int id);
        Task SaveIngredientAsync(Ingredient ingredient, bool isNewItem);
	    Task DeleteIngredientAsync(int id);
	    
    }
}
