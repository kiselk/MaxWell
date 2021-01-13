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
using MaxWell.ViewModels.Ingredients;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using Xamarin.Forms;
using Xamvvm;


namespace MaxWell.ViewModels.Ingredients
{
    public class IngredientDetailViewModel : BaseViewModel
    {

        private Ingredient _ingredient;

        public IngredientDetailViewModel()
        {
        }

        public IngredientDetailViewModel(Ingredient ingredient)
        {
            this.Ingredient = ingredient;
        }

        public Ingredient Ingredient
        {
            get => _ingredient;
            set { SetProperty(ref _ingredient, value); }
        }
     
    }
}