using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Pregnancies;
using MaxWell.ViewModels.Vyazki;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Pregnancies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PregnancyDetailViewPage : ContentPage
	{
		public PregnancyDetailViewPage ()
		{
			InitializeComponent ();
		}
	    public PregnancyDetailViewPage(Pregnancy post)
	    {
	        InitializeComponent();
	        vm = new PregnancyDetailViewModel(post);
	        this.BindingContext = vm;
	    }
	    PregnancyDetailViewModel vm;
        protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        vm = (PregnancyDetailViewModel)BindingContext;

	    }



        async void ButtonClicked(object sender, EventArgs args)
	    {
	        var pregnancy = ((PregnancyDetailViewModel)BindingContext).Pregnancy;
	        // DisplayAlert("Сохранено", "" , "OK");
	        await App.Database2.SavePregnancyAsync(pregnancy);
	        await Navigation.PopAsync();


	    }

	    async void DeleteClicked(object sender, EventArgs args)
	    {
	        var pregnancy = ((PregnancyDetailViewModel)BindingContext).Pregnancy;
	        // DisplayAlert("Сохранено", "" , "OK");
	        await App.Database2.DeletePregnancyAsync(pregnancy);
	        await Navigation.PopAsync();


	    }
    }
}