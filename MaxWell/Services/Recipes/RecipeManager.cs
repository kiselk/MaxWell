using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Recipes
{
    public class RecipeManager
    {
        IRecipeService recipeService;

        public RecipeManager(IRecipeService service)
        {
            recipeService = service;
        }

        public Task<List<Recipe>> GetRecipesAsync ()
        {
            return recipeService.RefreshRecipesAsync ();	
        }
        public Task<Recipe> GetRecipeAsync (int id)
        {
            return recipeService.GetRecipeAsync(id);
        }
            
        public Task<Recipe> SaveRecipeAsync (Recipe item, bool isNewItem = false)
        {
    	return recipeService.SaveRecipeAsync (item, isNewItem);
        }
    
        public Task DeleteRecipeAsync (Recipe item)
        {
    	return recipeService.DeleteRecipeAsync(item.RecipeId);
        }
    }
}