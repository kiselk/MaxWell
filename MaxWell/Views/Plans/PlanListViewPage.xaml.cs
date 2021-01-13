using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Plans;
using MaxWell.ViewModels.Pomets;
using MaxWell.Views;

namespace MaxWell.Views.Plans
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanListViewPage : ContentPage
    {
        PlanListViewModel viewModel;
   
        public PlanListViewPage()
        {
            InitializeComponent();
            Title = CustomConstants.Plans.PlanConstants.PlansNameEn.Translate();
            BindingContext = viewModel = new PlanListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                PlanListItemViewModel model = (PlanListItemViewModel)args.SelectedItem;
                await Navigation.PushAsync(new PlanDetailViewPage(model.Plan));
            }
        }
	
        async void PlanAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlanDetailViewPage(new Plan()
            {
                CreateDateTime = DateTime.Now
            } ,true));
        }
	  
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var loading = UserDialogs.Instance.Loading("Loading".Translate(), null, null, true);
            await viewModel.LoadData();
            loading.Hide();
        }
    }
}