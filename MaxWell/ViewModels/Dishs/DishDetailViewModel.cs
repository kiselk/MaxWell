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
using MaxWell.ViewModels.Dishs;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using Xamarin.Forms;
using Xamvvm;


namespace MaxWell.ViewModels.Dishs
{
    public class DishDetailViewModel : BaseViewModel
    {

        private Dish _dish;

        public DishDetailViewModel()
        {
        }

        public DishDetailViewModel(Dish dish)
        {
            this.Dish = dish;
        }

        public Dish Dish
        {
            get => _dish;
            set { SetProperty(ref _dish, value); }
        }
     
    }
}