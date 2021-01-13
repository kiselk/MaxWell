using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Allergens;
using Xamarin.Forms;
using MaxWell.Shared.Models;
namespace MaxWell.ViewModels.Allergens
{
    public class AllergenListItemViewModel : BaseViewModel
    {

        public Allergen Allergen;
    
        public AllergenListItemViewModel(Allergen allergen)
        {
            Allergen = allergen;
        }

        public string Name => Allergen.Name;
        public string Description => Allergen.Description;
        public string ImageUrl => Allergen.ImageUrl;
        public DateTime CreateDateTime => Allergen.CreateDateTime;
    }
}