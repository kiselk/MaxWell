using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Models;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Foods;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using MaxWell.Views.Cats;
using MaxWell.Views.Foods;
using MaxWell.Views.Prides;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Controls.Persons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonDetailEditView : ContentView
    {
        private PersonDetailViewModel vm;
        private List<Food> foods;
        private List<Comment> commentsBy;
        private List<Comment> commentsFor;

        public PersonDetailEditView()
        {
            InitializeComponent();


        }

        async void OnFoodItemSelected(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            if (itemTappedEventArgs.Item != null)
            {
                FoodListItemViewModel model = (FoodListItemViewModel) itemTappedEventArgs.Item;

                await Navigation.PushAsync(new FoodDetailViewPage(model.Food));

            }

        }

        async void AddFoodClicked(object sender, EventArgs eventArgs)
        {
            

                await Navigation.PushAsync(new FoodDetailViewPage( new Food(),true));

            

        }




        public async Task PopulateLists()
        {
            vm = (PersonDetailViewModel) BindingContext;


            vm.FoodModelList.Clear();
            vm.CommentByModelList.Clear();
            vm.CommentForModelList.Clear();


            foods = await App.FoodManager.GetFoodsAsync();
            commentsBy = await App.Database2.GetCommentsByPersonAsync(vm.Person.Id);
            commentsFor = await App.Database2.GetCommentsForPersonAsync(vm.Person.Id);

            foreach (Food food in foods)
            {
                vm.FoodModelList.Add(new FoodListItemViewModel(food));
            }

            foreach (Comment comment in commentsBy)
            {
                vm.CommentByModelList.Add(new CommentListItemViewModel(comment));
            }

            foreach (Comment comment in commentsFor)
            {
                vm.CommentForModelList.Add(new CommentListItemViewModel(comment));
            }

            await LoadImage();
            await ResizeLists();
        }

        public async Task LoadImage()
        {

            try
            {

                if ((vm.Person.image == null) && (vm.Person.ImageUrl != null))
                {
                    selectedImage.Source = ImageSource.FromUri(new Uri(vm.Person.ImageUrl));


                }


            }
            catch (Exception e)
            {
                await UserDialogs.Instance.AlertAsync(this.GetType() + " RemoteImage Error", e.Message);
            }
        }

        public async Task ResizeLists()
        {
            //   CommentsByList.HeightRequest = (64 * commentsBy.Count) + (20 * commentsBy.Count);
         //   CommentsForList.HeightRequest = (64 * commentsFor.Count) + (20 * commentsFor.Count);
        }

        public async Task ToggleLists()
        {
            try
            {
                var vm = (PersonDetailViewModel) BindingContext;


                if (vm.FoodModelList.Count == 0)
                {
                    FoodsScrollView.IsVisible = false;

                }
                else
                {
                    FoodsScrollView.IsVisible = true;
                }

              /*  if (vm.CommentByModelList.Count == 0) CommentsByList.IsVisible = false;
                else CommentsByList.IsVisible = true;
                if (vm.CommentForModelList.Count == 0) CommentsForList.IsVisible = false;
                else CommentsForList.IsVisible = true;
          */  }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert(e.Message, this.GetType() + " Init error");
            }
        }

    }
}