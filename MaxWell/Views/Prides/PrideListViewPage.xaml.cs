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
using MaxWell.ViewModels.Prides;
using MaxWell.Views;
using MaxWell.Views.Persons;

namespace MaxWell.Views.Prides
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrideListViewPage : ContentPage
	{
	    PrideListViewModel viewModel;
        public PrideListViewPage()
        {
        
            InitializeComponent();
    Title = "Прайды";
            
            BindingContext = viewModel = new PrideListViewModel();
        }
	    //ADDPERSON
	    private Person person;
	    public PrideListViewPage(Person person)
	    {
	        InitializeComponent();
	        Title = "Выберите прайд";
            this.person = person;
	        BindingContext = viewModel = new PrideListViewModel();
	    }


	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        if (args.SelectedItem != null)
	        {
	            PrideListItemViewModel pride = (PrideListItemViewModel) args.SelectedItem;
	            if (pride == null)
	                return;

	            if (person != null)
	            {

	                pride.Pride.PersonId = person.Id;

	                await App.Database2.SaveItemAsync(pride.Pride);
	                Navigation.RemovePage(this);

	                await Navigation.PushAsync(new PersonDetailViewPage(person));

	            }
	            else

	            {
	                PrideListItemViewModel model = (PrideListItemViewModel) args.SelectedItem;

	                await Navigation.PushAsync(new PrideDetailViewPage(model.Pride));

	            }
	        }

	    }
	    async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            await Navigation.PushAsync(new PrideDetailViewPage
                {
	                BindingContext = e.SelectedItem as Pride
                });
	        }
	    }
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            //await App.NavigationPage.PushAsync(new PrideDetailViewPage(new Pride() { CreationDate = DateTime.Now }));
            // await Navigation.PushAsync(App.navigationPage);
            await Navigation.PushAsync(new PrideDetailViewPage(new Pride() {CreationDate = DateTime.Now}));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var prides = await App.Database2.GetPridesAsync();

            viewModel = (PrideListViewModel)BindingContext;

            viewModel.PrideModelList.Clear();
            foreach (var pride in prides)
            {


                viewModel.PrideModelList.Add(new PrideListItemViewModel(pride));
            }
        }



    }
}