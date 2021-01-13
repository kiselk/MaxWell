using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Cats;
using MaxWell.ViewModels.Persons;
using MaxWell.Views;
using MaxWell.Views.Persons;

namespace MaxWell.Views.Cats
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CatListViewPage : ContentPage
	{
	    CatListViewModel viewModel;
        public CatListViewPage()
        {
            InitializeComponent();
            Title = "Животные";
            BindingContext = viewModel = new CatListViewModel();
        }
	    //ADDPERSON
	    private Person person;
	    public CatListViewPage(Person person)
	    {
	        InitializeComponent();
	        Title = "Выберите животное";
	        this.person = person;
	        BindingContext = viewModel = new CatListViewModel();
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                Cat cat = args.SelectedItem as Cat;
                if (cat == null)
                    return;

                if (person != null)
                {

                    cat.PersonId = person.Id;
                    cat.OwnerId = person.Id;

                    await App.Database2.SaveItemAsync(cat);
                    Navigation.RemovePage(this);

                    await Navigation.PushAsync(new PersonDetailViewPage(person));

                }
                else
                    await Navigation.PushAsync(new CatDetailViewPage {BindingContext = args.SelectedItem as Cat});

                // Manually deselect cat.
                CatListView.SelectedItem = null;
            }
        }
	    async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            await Navigation.PushAsync(new CatDetailViewPage
                {
	                BindingContext = e.SelectedItem as Cat
	            });
	        }
	    }
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CatDetailViewPage
            {
                BindingContext = new Cat() { BirthDate = DateTime.Now}
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var items = await App.Database2.GetItemsAsync();

                this.FindByName<Label>("OutputLabel").Text = ""+ items.Count;
                 CatListView.ItemsSource = items;
            }
            catch (Exception e)
            {
                this.FindByName<Label>("OutputLabel").Text = e.Message;
            }
        }



    }
}