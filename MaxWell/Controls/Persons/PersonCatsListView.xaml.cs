using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.ViewModels.Persons;
using MaxWell.Views.Cats;
using MaxWell.Views.Prides;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Controls.Persons
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonCatsListView : ContentView
	{
		public PersonCatsListView()
		{
			InitializeComponent ();
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