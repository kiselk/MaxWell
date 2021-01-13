using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels.Recipes;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Recipes
{
    public class RecipeListItemViewModel : BaseViewModel
    {

        public Recipe Recipe;
    
        public RecipeListItemViewModel(Recipe recipe)
        {
            Recipe = recipe;
        }

        public string Name => Recipe.Name;
        public string ImageUrl => Recipe.ImageUrl;
        public string Description => Recipe.Description;
  
        public DateTime CreateDateTime => Recipe.CreateDateTime;
    }
}