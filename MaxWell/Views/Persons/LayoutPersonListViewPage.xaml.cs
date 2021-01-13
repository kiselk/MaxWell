using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Persons
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LayoutPersonListViewPage : ContentPage
	{
	    PersonListViewModel viewModel;
        public LayoutPersonListViewPage()
        {
            InitializeComponent();
            Title = "People".Translate();
            BindingContext = viewModel = new PersonListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                PersonListItemViewModel model = (PersonListItemViewModel)args.SelectedItem;

                await Navigation.PushAsync(new PersonDetailViewPage(model.Person));

            }
        }
	
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            //ZZ
            await Navigation.PushAsync(new PersonDetailViewPage(new Person() {Birthday = DateTime.Now}));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var persons = await App.Database2.GetPersonsAsync();

            viewModel = (PersonListViewModel)BindingContext;

            viewModel.PersonModelList.Clear();
            foreach (var person in persons)
            {

                try
                {
                    if (person.image == null && (person.ImageUrl != null))
                    {
                        person.image = ImageHelper.ImageUrlToByteArray(person.ImageUrl);
                        App.PersonManager.SaveTaskAsync(person);
                    }
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.AlertAsync("Error", e.Message);
                }
                viewModel.PersonModelList.Add(new PersonListItemViewModel(person));
            }
        }



    }
}