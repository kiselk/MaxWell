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
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Dishs;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Dishs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DishListViewPage : ContentPage
    {
        DishListViewModel viewModel;
   
        public DishListViewPage()
        {
            InitializeComponent();
            Title = CustomConstants.Dishs.DishConstants.DishsNameEn.Translate();
            BindingContext = viewModel = new DishListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                DishListItemViewModel model = (DishListItemViewModel)args.SelectedItem;
                await Navigation.PushAsync(new DishDetailViewPage(model.Dish));
            }
        }
	
        async void DishAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DishDetailViewPage(new Dish()
            {
                CreateDateTime = DateTime.Now
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