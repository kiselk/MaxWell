using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views.Cats;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Pomets
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PometListViewPage : ContentPage
	{
	    PometListViewModel viewModel;
	    public PometListViewPage()
	    {
	        InitializeComponent();
	        Title = "Пометы";
            BindingContext = viewModel = new PometListViewModel();
	    }
        protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        var pomets = await App.Database2.GetPometsAsync();

	        viewModel = (PometListViewModel)BindingContext;
	       
	        viewModel.PometModelList.Clear();
	        foreach (var pomet in pomets)
	        {


	            viewModel.PometModelList.Add(new PometListItemViewModel(pomet));
	        }
	   
	    }

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            PometListItemViewModel model = (PometListItemViewModel)e.SelectedItem;

	            await Navigation.PushAsync(new PometDetailViewPage(model.Pomet));

	        }
        }

	    async void AddItem_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new FemalesViewPage(new Pomet() { PometDate = DateTime.Now }));

	    }
    }
}