 using System;
using System.Collections.Generic;
 using System.Collections.ObjectModel;
 using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using MaxWell.ViewModels.Pages;
 using MaxWell.Views.Allergens;
 using MaxWell.Views.Calculator;
 using MaxWell.Views.Calendar;
 using MaxWell.Views.Cats;
 using MaxWell.Views.Comments;
 using MaxWell.Views.Dishs;
 using MaxWell.Views.Foods;
 using MaxWell.Views.Ingredients;
 using MaxWell.Views.Main.Deprecated.UI;
 using MaxWell.Views.Meals;
 using MaxWell.Views.Persons;
 using MaxWell.Views.Plans;
 using MaxWell.Views.Pomets;
 using MaxWell.Views.Pregnancies;
 using MaxWell.Views.Prides;
 using MaxWell.Views.Recipes;
 using MaxWell.Views.Todo;
 using MaxWell.Views.Vyazki;
 using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Main
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
	    public List<PageListItemViewModel> Pages;
	    private PageListViewModel viewModel;
        public MenuPage()
		{
			InitializeComponent ();
		    BindingContext = viewModel = new PageListViewModel();
            viewModel.PageModelList = new ObservableCollection<PageListItemViewModel>
            {
                //  new PageListItemViewModel(new RestPersonListViewPage()),
                //    new PageListItemViewModel(new SFCalendarViewPage()),


                new PageListItemViewModel(new LoginPage()),
                new PageListItemViewModel(new ProfileViewPage()),

                new PageListItemViewModel(new TodoListPage()),
                new PageListItemViewModel(new PhenylListViewPage()),
                new PageListItemViewModel(new AllergenListViewPage()),

                new PageListItemViewModel(new IngredientListViewPage()),
                new PageListItemViewModel(new RecipeListViewPage()),
                new PageListItemViewModel(new DishListViewPage()),
                new PageListItemViewModel(new MealListViewPage()),
                new PageListItemViewModel(new PlanListViewPage()),      

                //    new PageListItemViewModel(new PersonListViewPage()),
              //  new PageListItemViewModel(new LayoutPersonListViewPage()),
             //   new PageListItemViewModel(new PrideListViewPage()),

             //   new PageListItemViewModel(new CatListViewPage()),
             //   new PageListItemViewModel(new FemalesViewPage()),
             //   new PageListItemViewModel(new MalesViewPage()),
            //    new PageListItemViewModel(new KittensViewPage()),
             

             //   new PageListItemViewModel(new VyazkaListViewPage()),
            //    new PageListItemViewModel(new PregnancyListViewPage()),
            //    new PageListItemViewModel(new PometListViewPage()),
            //    new PageListItemViewModel(new CalculatorViewPage()),
                
            //    new PageListItemViewModel(new CommentListViewPage()),
              //  new PageListItemViewModel(new ImageWrapLayoutPage()),
                new PageListItemViewModel(new AboutPage())
            };

        }
	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        if (args.SelectedItem != null)
	        {


	            PageListItemViewModel page = (PageListItemViewModel)args.SelectedItem;

	                await Navigation.PushAsync(page.Page);

	        }

	    }
       
	}
}