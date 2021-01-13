using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Pomets;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Pomets
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PometDetailViewPage : ContentPage
	{
		public PometDetailViewPage ()
		{
			InitializeComponent ();
		}

	    public PometDetailViewPage(Pomet post)
	    {
	        InitializeComponent();
	        vm = new PometDetailViewModel(post);
	        this.BindingContext = vm;
	    }
	    PometDetailViewModel vm;
	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        vm = (PometDetailViewModel)BindingContext;

	      

	     
	        vm.KittenModelList.Clear();
	     
	        var kittens = await App.Database2.GetKittensForPometAsync(vm.Pomet.Id);
	     
	        foreach (Cat kitten in kittens) { vm.KittenModelList.Add(new CatListItemViewModel(kitten)); }

	        this.FindByName<ListView>("KittensList").HeightRequest = (128 * kittens.Count) + (20 * kittens.Count);



        }
        async void AddKittenClicked(object sender, EventArgs args)
        {
            var pomet = ((PometDetailViewModel)BindingContext).Pomet;
            //add kitten
            await Navigation.PushAsync(new KittensViewPage(pomet));

        }
	    async void OnCatItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        if (args.SelectedItem != null)
	        {
	            CatListItemViewModel model = (CatListItemViewModel)args.SelectedItem;

	            await Navigation.PushAsync(new CatDetailViewPage() { BindingContext = model.Cat });

	        }

	    }

        async void ButtonClicked(object sender, EventArgs args)
	    {
	        var pomet = ((PometDetailViewModel)BindingContext).Pomet;
	        // DisplayAlert("Сохранено", "" , "OK");
	        await App.Database2.SavePometAsync(pomet);
	        await Navigation.PopAsync();


	    }

	    async void DeleteClicked(object sender, EventArgs args)
	    {
	        var pomet = ((PometDetailViewModel)BindingContext).Pomet;
	        // DisplayAlert("Сохранено", "" , "OK");
	        await App.Database2.DeletePometAsync(pomet);
	        await Navigation.PopAsync();


	    }
    }
}