using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Prides;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Controls.Persons
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonPridesListView : ContentView
	{
		public PersonPridesListView ()
		{
			InitializeComponent ();


		}

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        if (args.SelectedItem != null)
	        {
	            PrideListItemViewModel model = (PrideListItemViewModel)args.SelectedItem;

	            await Navigation.PushAsync(new PrideDetailViewPage(model.Pride));

	        }

	    }

    }
}