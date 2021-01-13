using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;

using MaxWell.Shared.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Allergens;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Allergens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllergenListViewPage : ContentPage
    {
        AllergenListViewModel viewModel;
   
        public AllergenListViewPage()
        {
            InitializeComponent();
            Title = CustomConstants.Allergens.AllergenConstants.AllergensNameEn.Translate();
            BindingContext = viewModel = new AllergenListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                AllergenListItemViewModel model = (AllergenListItemViewModel)args.SelectedItem;
                await Navigation.PushAsync(new AllergenDetailViewPage(model.Allergen));
            }
        }
	
        async void AllergenAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllergenDetailViewPage(new Allergen()
            { CreateDateTime = DateTime.Now
            } ,true));
        }
	  
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var loading = UserDialogs.Instance.Loading("Loading".Translate(), null, null, true);
            await viewModel.LoadData();
            loading.Hide();
        }
    }
}