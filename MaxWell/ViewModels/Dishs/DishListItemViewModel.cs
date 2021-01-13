using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels.Dishs;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Dishs
{
    public class DishListItemViewModel : BaseViewModel
    {

        public Dish Dish;
    
        public DishListItemViewModel(Dish dish)
        {
            Dish = dish;
        }

        public string Name => Dish.Name;
        public string Description => Dish.Description;

    }
}