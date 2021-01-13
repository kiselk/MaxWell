using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Pregnancies;
using MaxWell.ViewModels.Vyazki;
using MaxWell.Views.Cats;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Pregnancies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PregnancyListViewPage : ContentPage
	{
	    PregnancyListViewModel viewModel;
	    public PregnancyListViewPage()
		{
			InitializeComponent ();
		    Title = "Беременности";
            BindingContext = viewModel = new PregnancyListViewModel();
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        viewModel = (PregnancyListViewModel)BindingContext;
	        List<Pregnancy> items = await App.Database2.GetPregnanciesAsync();
	         viewModel.PregnancyModelList.Clear();
	        foreach (var pregnancy in items)
	        {
	          

	            viewModel.PregnancyModelList.Add(new PregnancyListItemViewModel(pregnancy));
	        }
        }

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            PregnancyListItemViewModel model = (PregnancyListItemViewModel)e.SelectedItem;

	            await Navigation.PushAsync(new PregnancyDetailViewPage(model.Pregnancy));

	        }
        }

	    async void AddItem_Clicked(object sender, EventArgs e)
	    {



	        await Navigation.PushAsync(new FemalesViewPage(new Pregnancy() { PregnancyDate = DateTime.Now }));
	        /*  await Navigation.PushAsync(new VyazkaDetailPage
              {
                  BindingContext = new VyazkaDetailViewModel(new Vyazka()
                  {
                      SexDate = DateTime.Now
                  })
              });
              */
	    }

    }
}