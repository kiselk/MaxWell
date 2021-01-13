using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels.Ingredients;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Ingredients
{
    public class IngredientListItemViewModel : BaseViewModel
    {

        private Ingredient _ingredient;

        public IngredientListItemViewModel(Ingredient ingredient)
        {
            this.Ingredient = ingredient;
        }

        public string Name => Ingredient.Name;
        public string ImageUrl => Ingredient.ImageUrl;
        public double? Amount => Ingredient.Amount;
        public string Description => Ingredient.Description;
        public DateTime CreateDateTime => Ingredient.CreateDateTime;
        public Ingredient Ingredient
        {
            get => _ingredient;
            set { SetProperty(ref _ingredient, value); }
        }

    }
}