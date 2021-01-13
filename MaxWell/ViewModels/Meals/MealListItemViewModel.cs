using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels.Meals;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Meals
{
    public class MealListItemViewModel : BaseViewModel
    {

        public Meal Meal;
    
        public MealListItemViewModel(Meal meal)
        {
            Meal = meal;
        }

        public string Name => Meal.Name;
        public string Description => Meal.Description;

    }
}