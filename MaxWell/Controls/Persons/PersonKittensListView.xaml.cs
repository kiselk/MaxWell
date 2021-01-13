using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Prides;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Controls.Persons
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonKittensListView : ContentView
	{
		public PersonKittensListView()
		{
			InitializeComponent ();

		    var kittens = (ObservableCollection<CatListItemViewModel>) BindingContext;

            
            ObservableCollection<SfCarouselItem> collectionOfItems = new ObservableCollection<SfCarouselItem>();

		    foreach (var kitten in (kittens))
		    {
		        collectionOfItems.Add(new SfCarouselItem() { ItemContent = new Xamarin.Forms.Image() { Source = kitten.ImageAsImageStream } });

            }
            carousel.ItemsSource = collectionOfItems;
		    carousel.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
		    {
		        if (e.SelectedItem != null)
		        {
		            CatListItemViewModel model = (CatListItemViewModel)e.SelectedItem;

		            Navigation.PushAsync(new CatDetailViewPage() { BindingContext = model.Cat });

		        }
            };
            // this.Content = carousel;
        }

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        if (args.SelectedItem != null)
	        {
	            CatListItemViewModel model = (CatListItemViewModel)args.SelectedItem;

	            await Navigation.PushAsync(new CatDetailViewPage() {BindingContext = model.Cat});

	        }

	    }
    }
}