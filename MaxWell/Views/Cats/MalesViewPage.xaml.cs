using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.ViewModels;
using MaxWell.ViewModels.Pomets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MaxWell.Views;
using MaxWell.Views.Pomets;
using MaxWell.Views.Pregnancies;
using MaxWell.Views.Vyazki;

namespace MaxWell.Views.Cats
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MalesViewPage : ContentPage
	{
	    private Cat femaleCat;
	    private Vyazka vyazka;
	    private Pregnancy pregnancy;
	    private Pomet pomet;

        public MalesViewPage()
	    {
	        InitializeComponent();
	        Title = "Коты";

        }
	    public MalesViewPage(Vyazka vyazka)
	    {
	        InitializeComponent();
	        this.vyazka = vyazka;
	        Title = "Выберите кота";
        }
	    public MalesViewPage(Pregnancy pregnancy)
	    {
	        InitializeComponent();
	        this.pregnancy = pregnancy;
	        Title = "Выберите кота";
        }
	    public MalesViewPage(Pomet pomet)
	    {
	        InitializeComponent();
	        this.pomet = pomet;
	        Title = "Выберите кота";
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem != null)
	        {

	            var maleCat = e.SelectedItem as Cat;


	            if (vyazka != null)
	            {
	               // Vyazka vyazka = new Vyazka();
	                if (maleCat.Gender == null)
	                {
	                    DisplayAlert("Ошибка", "Задайте пол животного", "ОК");
	                }
	                else if (maleCat.Gender.Equals("Девочка"))
	                {
	                    DisplayAlert("Ошибка", "Однополые вязки не приносят потомства", "ОК");

	                }

	                else
	                {
	                    vyazka.SexDate=DateTime.Now;

                        vyazka.Father = maleCat.Text;
	                    vyazka.FatherId = maleCat.Id;

                        //      vyazka.Mother = femaleCat.Text;
                        //      vyazka.MotherId = femaleCat.Id;

	                    if (vyazka.MotherId == 0)
	                    {

	                        Navigation.RemovePage(this);
	                        await Navigation.PushAsync(new FemalesViewPage(vyazka));
	                    }
	                    else
	                    {

                            /* VyazkaDetailViewModel viewModel = new VyazkaDetailViewModel(vyazka);
                             Navigation.RemovePage(this);
                             await Navigation.PushAsync(new VyazkaDetailPage() { BindingContext = viewModel });
                             */
	                        Navigation.RemovePage(this);
	                        await Navigation.PushAsync(new VyazkaDetailViewPage(vyazka));

                        }
                    }
	            }
                if(pregnancy !=null)
                {

                    pregnancy.Father = maleCat.Text;
                    pregnancy.FatherId = maleCat.Id;
                    Navigation.RemovePage(this);
                    await Navigation.PushAsync(new PregnancyDetailViewPage(pregnancy));
                }
	            if (pomet != null)
	            {

	                pomet.Father = maleCat.Text;
	                pomet.FatherId = maleCat.Id;
	                Navigation.RemovePage(this);
	                await Navigation.PushAsync(new PometDetailViewPage(pomet));
                }
	            else

                    await Navigation.PushAsync(new CatDetailViewPage
                    {
	                BindingContext = e.SelectedItem as Cat
	            });
	        }

          
	    }

	    async void AddItem_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new CatDetailViewPage()
	        {
	            BindingContext = new Cat()
	        });
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        ((App)App.Current).ResumeAtTodoId = -1;
	        MalesListView.ItemsSource = await App.Database2.GetMalesAsync();
	     //   var items = await App.Database2.GetFemalesAsync();

	       // foreach (var cat in items)
	     //   {
	    //        cat.DownloadedImageBlob = await App.Database2.GetDownloadedImageSourceAsync(cat.logoPhotoId);

	            //  wrapLayout.Children.Add(image);
	   //     }
	    //    MalesListView.ItemsSource = items;
        }


    }
}