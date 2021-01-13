using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Cats;
using MaxWell.ViewModels.Comments;
using MaxWell.Views;

namespace MaxWell.Views.Comments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CommentListViewPage : ContentPage
	{
	    CommentListViewModel viewModel;
        public CommentListViewPage()
        {
            InitializeComponent();
            Title = "Комментарии";
            BindingContext = viewModel = new CommentListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                CommentListItemViewModel model = (CommentListItemViewModel)args.SelectedItem;

                await Navigation.PushAsync(new CommentDetailViewPage(model.Comment));

            }
        }
	    async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            await Navigation.PushAsync(new CommentDetailViewPage
                {
	                BindingContext = e.SelectedItem as Comment
                });
	        }
	    }
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CommentDetailViewPage(new Comment() { PersonId=1, Date = DateTime.Now }));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var comments = await App.Database2.GetCommentsAsync();

            viewModel = (CommentListViewModel)BindingContext;

            viewModel.CommentModelList.Clear();
            foreach (var comment in comments)
            {


                viewModel.CommentModelList.Add(new CommentListItemViewModel(comment));
            }
        }



    }
}