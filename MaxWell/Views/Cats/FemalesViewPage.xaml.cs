using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.Views.Vyazki;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Cats
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FemalesViewPage : ContentPage
	{
	    private Cat maleCat;
	    private Vyazka vyazka;
	    private Pomet pomet;
	    private Pregnancy pregnancy;

        public FemalesViewPage()
	    {
	        InitializeComponent();
	        Title = "Кошки";

        }

	    public FemalesViewPage(Vyazka vyazka)
	    {
	        InitializeComponent();
	        this.vyazka = vyazka;
	        Title = "Выберите кошку";
        }
	    public FemalesViewPage(Pomet pomet)
	    {
	        InitializeComponent();
	        this.pomet = pomet;
	        Title = "Выберите кошку";
	    }
	    public FemalesViewPage(Pregnancy pregnancy)
	    {
	        InitializeComponent();
	        this.pregnancy = pregnancy;
	        Title = "Выберите кошку";
	    }
        protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        ((App)App.Current).ResumeAtTodoId = -1;
            //FemalesListView.ItemsSource = await App.Database2.GetItemsAsync();


	         var items = await App.Database2.GetFemalesAsync();

	      //  foreach (var cat in items)
	     //   {
	          //  cat.DownloadedImageBlob = await App.Database2.GetDownloadedImageSourceAsync(cat.logoPhotoId);

	            //  wrapLayout.Children.Add(image);
	     //   }
	        FemalesListView.ItemsSource = items;

        //    FemalesListView.ItemsSource = await App.Database2.GetFemalesAsync();
	    }

   async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {
	            var femaleCat = e.SelectedItem as Cat;
	            

                if (vyazka != null)
	            {
	              //  Vyazka vyazka = new Vyazka();
	                if (femaleCat.Gender == null)
	                {
	                    DisplayAlert("Ошибка", "Задайте пол животного", "ОК");
	                }
	                else if (femaleCat.Gender.Equals("Мальчик"))
	                {
	                    DisplayAlert("Ошибка", "Однополые вязки не приносят потомства", "ОК");

                    }

	                else
	                {
	                    vyazka.SexDate = DateTime.Now;

                        //vyazka.Father = maleCat.Text;
	                    //vyazka.FatherId = maleCat.Id;

                        vyazka.Mother = femaleCat.Text;
	                    vyazka.MotherId = femaleCat.Id;


	                    if (vyazka.FatherId == 0)
	                    {

	                        Navigation.RemovePage(this);
	                        await Navigation.PushAsync(new MalesViewPage(vyazka));
	                    }
	                    else
	                    {

                            /*    VyazkaDetailViewModel viewModel = new VyazkaDetailViewModel(vyazka);
                                Navigation.RemovePage(this);
                                await Navigation.PushAsync(new VyazkaDetailPage() {BindingContext = viewModel});
                                */
	                        Navigation.RemovePage(this);
	                        await Navigation.PushAsync(new VyazkaDetailViewPage(vyazka));

                        }
                    }
                } else 


	            await Navigation.PushAsync(new CatDetailViewPage
                {
	                BindingContext = e.SelectedItem as Cat
	            });
	        }
        }

	    async void AddItem_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new CatDetailViewPage
            {
	            BindingContext = new Cat()
	        });
	    }


    }
}