using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Persons;
using MaxWell.Views.Cats;
using MaxWell.Views.Comments;
using MaxWell.Views.Prides;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Controls.Persons
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonCommentsByListView : ContentView
	{
		public PersonCommentsByListView()
		{
			InitializeComponent ();
		}

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        if (args.SelectedItem != null)
	        {
	            CommentListItemViewModel model = (CommentListItemViewModel)args.SelectedItem;

	            await Navigation.PushAsync(new CommentDetailViewPage(model.Comment));

	        }

	    }
    }
}