using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Vyazki;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Vyazki
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VyazkaDetailViewPage : ContentPage
	{
	    VyazkaDetailViewModel postDetailViewModel;


        public VyazkaDetailViewPage()
		{
		    
            InitializeComponent ();
		}

	 

	    public VyazkaDetailViewPage(Vyazka post)
	    {
	        InitializeComponent();
	        vm = new VyazkaDetailViewModel(post);
	        this.BindingContext = vm;
	    }

	    VyazkaDetailViewModel vm;
        private ContentPage _tbVisits;


        protected override async void OnAppearing()
        {
            base.OnAppearing();
           vm = (VyazkaDetailViewModel)BindingContext;

        }
      

        async void ButtonClicked(object sender, EventArgs args)
        {
            var cat = ((VyazkaDetailViewModel)BindingContext).Vyazka;
            await App.Database2.SaveVyazkaAsync(cat);
            await Navigation.PopAsync();
        }

        async void DeleteClicked(object sender, EventArgs args)
        {
            var vyazka = ((VyazkaDetailViewModel)BindingContext).Vyazka;
            // DisplayAlert("Сохранено", "" , "OK");
            await App.Database2.DeleteVyazkaAsync(vyazka);
            await Navigation.PopAsync();


        }

    

    }
}