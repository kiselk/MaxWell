using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Foods;
using MaxWell.ViewModels.Ingredients;
using MaxWell.ViewModels.Recipes;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using Xamarin.Forms;
using Xamvvm;


namespace MaxWell.ViewModels.Recipes
{
    public class RecipeDetailViewModel : BaseViewModel
    {

        private Recipe _recipe;

        public RecipeDetailViewModel()
        {
            IngredientModelList = new ObservableCollection<IngredientListItemViewModel>();
        }

        public RecipeDetailViewModel(Recipe recipe)
        {
            this.Recipe = recipe;
            IngredientModelList = new ObservableCollection<IngredientListItemViewModel>();
        }

        public Recipe Recipe
        {
            get => _recipe;
            set { SetProperty(ref _recipe, value); }
        }
        private ObservableCollection<IngredientListItemViewModel> _ingredientModelList;
        public ObservableCollection<IngredientListItemViewModel> IngredientModelList
        {
            get => _ingredientModelList;
            set { SetProperty(ref _ingredientModelList, value); }
        }
        private double? _weight;
        public double? Weight
        {
            get => _weight;
            set { SetProperty(ref _weight, value); }
        }
       

        public double? GetWeight()
        {
            double? weight = 0;
            try
            {
                foreach (Ingredient ingr in Recipe.Ingredients)
                {
                    weight += ingr.Amount;
                }

                return weight;
            }
            catch (Exception e)
            {

                return null;
            }
        }

    }
}