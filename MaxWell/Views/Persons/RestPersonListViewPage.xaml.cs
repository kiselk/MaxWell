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
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Persons
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RestPersonListViewPage : ContentPage
	{
	    RestPersonListViewModel viewModel;
        public RestPersonListViewPage()
        {
            InitializeComponent();
            Title = "People".Translate();
             BindingContext = viewModel = new RestPersonListViewModel();
            // viewModel = new PersonListViewModel();
           // viewModel =(PersonListViewModel) BindingContext;
          //  Source = new ObservableCollection<PersonListItemViewModel>();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                PersonListItemViewModel model = (PersonListItemViewModel)args.SelectedItem;

                await Navigation.PushAsync(new RestPersonDetailViewPage(model.Person));

            }
        }
	
        async void RestAddItem_Clicked(object sender, EventArgs e)
        {
            //ZZ
            await Navigation.PushAsync(new RestPersonDetailViewPage(new Person() {Birthday = DateTime.Now},true));
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