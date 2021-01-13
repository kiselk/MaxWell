using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Views.Pomets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Cats
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KittensViewPage : ContentPage
	{
	    private Pomet pomet;

        public KittensViewPage()
	    {
	        InitializeComponent();
	        Title = "Котята";
        }
	    public KittensViewPage(Pomet pomet)
	    {

	        InitializeComponent();
	        this.pomet = pomet;
	        Title = "Выберите котенка";
        }
        //ADDKITTEN
	    private Person person;
        public KittensViewPage(Person person)
	    {
	        InitializeComponent();
	        this.person = person;
	    }
       
    async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {


	            if (pomet != null)
	            {
	                Cat kitten = e.SelectedItem as Cat;

	                kitten.PometId = pomet.Id;
	                await App.Database2.SaveItemAsync(kitten);
	                Navigation.RemovePage(this);

	                await Navigation.PushAsync(new PometDetailViewPage(pomet));

	            }

	            else
	            {
	                await Navigation.PushAsync(new CatDetailViewPage {BindingContext = e.SelectedItem as Cat});
	            }
	        }



	    }

                async void AddItem_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new CatDetailViewPage
            {
	            BindingContext = new Cat()
	        });
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        ((App)App.Current).ResumeAtTodoId = -1;
	        var items = await App.Database2.GetKittensAsync();

	     //   foreach (var cat in items)
	      //  {
	      //      cat.DownloadedImageBlob = await App.Database2.GetDownloadedImageSourceAsync(cat.logoPhotoId);
//
	            //  wrapLayout.Children.Add(image);
	      //  }
	        KittensListView.ItemsSource = items;
	    }

    }
}