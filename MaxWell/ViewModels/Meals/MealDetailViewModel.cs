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
using MaxWell.ViewModels.Meals;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using Xamarin.Forms;
using Xamvvm;


namespace MaxWell.ViewModels.Meals
{
    public class MealDetailViewModel : BaseViewModel
    {

        private Meal _meal;

        public MealDetailViewModel()
        {
        }

        public MealDetailViewModel(Meal meal)
        {
            this.Meal = meal;
        }

        public Meal Meal
        {
            get => _meal;
            set { SetProperty(ref _meal, value); }
        }
     
    }
}