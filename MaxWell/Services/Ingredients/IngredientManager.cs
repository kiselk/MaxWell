using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Ingredients
{
    public class IngredientManager
    {
        IIngredientService ingredientService;

        public IngredientManager(IIngredientService service)
        {
            ingredientService = service;
        }

        public Task<List<Ingredient>> GetIngredientsAsync ()
        {
            return ingredientService.RefreshIngredientsAsync ();	
        }
        public Task<Ingredient> GetIngredientAsync (int id)
        {
            return ingredientService.GetIngredientAsync(id);
        }
        public Task<List<Ingredient>> GetIngredientsByRecipeId(int id)
        {
            return ingredientService.GetIngredientsByRecipeId(id);
        }
        
        public Task SaveIngredientAsync (Ingredient item, bool isNewItem = false)
        {
    	return ingredientService.SaveIngredientAsync (item, isNewItem);
        }
    
        public Task DeleteIngredientAsync (Ingredient item)
        {
    	return ingredientService.DeleteIngredientAsync(item.IngredientId);
        }
    }
}