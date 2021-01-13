using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using MaxWell.Shared.Models;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Allergens;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using Xamarin.Forms;
using Xamvvm;
using RemoteImage = MaxWell.Models.RemoteImage;

namespace MaxWell.ViewModels.Allergens
{
    public class AllergenDetailViewModel : BaseViewModel
    {

        private Allergen _allergen;

        public AllergenDetailViewModel()
        {
        }

        public AllergenDetailViewModel(Allergen allergen)
        {
            this.Allergen = allergen;
        }

        public Allergen Allergen
        {
            get => _allergen;
            set { SetProperty(ref _allergen, value); }
        }
     
    }
}