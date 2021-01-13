using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Vyazki;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Vyazki
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VyazkaListViewPage : ContentPage
	{
	    VyazkaListViewModel viewModel;
        public VyazkaListViewPage()
		{
			InitializeComponent ();
		    Title = "Вязки";
		    BindingContext = viewModel = new VyazkaListViewModel();
        }


	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        viewModel = (VyazkaListViewModel) BindingContext;
	        List<Vyazka> items = await App.Database2.GetVyazkasAsync();
	        viewModel.VyazkaModelList.Clear();
	        foreach (var vyazka in items)
	        {
                viewModel.VyazkaModelList.Add(new VyazkaListItemViewModel(vyazka));
	        }
	    }

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            VyazkaListItemViewModel model = (VyazkaListItemViewModel) e.SelectedItem;

	            await Navigation.PushAsync(new VyazkaDetailViewPage(model.Vyazka));

	        }
	    }

	    async void AddItem_Clicked(object sender, EventArgs e)
	    {



	        await Navigation.PushAsync(new VyazkaDetailViewPage(new Vyazka() {SexDate = DateTime.Now}));
	    
        }

    }
}