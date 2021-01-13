using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Controls.Foods;
using MaxWell.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Foods
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhenylListViewPage : ContentPage
    {
        public PhenylListViewPage()
        {
            InitializeComponent();
          //  Title = "Фенилаланин ТОП";
            Title = "Phenyl".Translate();
        }

  protected override async void OnAppearing()
    {
        base.OnAppearing();
        var phenylDetailView = this.FindByName<PhenylDetailView>("phenylDetailView");
        phenylDetailView.LoadDescriptions();
      //  phenylDetailView.DoFocus();



    }

    }

   
  
}