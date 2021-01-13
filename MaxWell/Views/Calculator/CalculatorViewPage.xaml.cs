using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.ViewModels.Calculator;
using MaxWell.ViewModels.Persons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Calculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorViewPage : ContentPage
    {
        private CalculatorViewModel vm;

        public CalculatorViewPage()
        {
            InitializeComponent();
            BindingContext = vm = new CalculatorViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
           
       //     var prides = await App.Database2.GetPridesForPersonAsync(2);
            var cats = await App.Database2.GetCatsForPersonAsync(2);
            var females = await App.Database2.GetFemalesForPersonAsync(2);
            var males = await App.Database2.GetMalesForPersonAsync(2);
            var kittens = await App.Database2.GetKittensForPersonAsync(2);
            

            //     var commentsBy = await App.Database2.GetCommentsByPersonAsync(2);
            //    var commentsFor = await App.Database2.GetCommentsForPersonAsync(2);


            vm.FemaleCatCount = females.Count;
            vm.MaleCatCount = males.Count;
            vm.KittensCount = kittens.Count;
            vm.CatsCount = females.Count + males.Count + kittens.Count;

            vm.PricePerBagOfFood = 6000;
            vm.KilosInBagOfFood = 15;
            vm.PricePerVaccination = 500;
            vm.PricePerKiloOfFood = vm.PricePerBagOfFood / vm.KilosInBagOfFood;
            vm.KilosForCatPerDay = 200;
            vm.KilosForKittenPerDay = 100;
            vm.KilosForFemalePerDay = 200;
            vm.KilosForMalePerDay = 200;
            vm.KilosForFemalesPerDay = vm.KilosForFemalePerDay*vm.FemaleCatCount;
            vm.KilosForMalesPerDay = vm.KilosForMalePerDay * vm.MaleCatCount;
            vm.KilosForKittensPerDay = vm.KilosForKittenPerDay * vm.KittensCount;
            vm.KilosForCatsPerDay = vm.KilosForCatPerDay * vm.CatsCount;

            vm.MoneyForFemalePerDay = vm.KilosForFemalePerDay * vm.PricePerKiloOfFood/1000;
            vm.MoneyForMalePerDay = vm.KilosForMalePerDay * vm.PricePerKiloOfFood/1000;
            vm.MoneyForKittenPerDay = vm.KilosForKittenPerDay * vm.PricePerKiloOfFood/1000;
            vm.MoneyForCatPerDay = vm.MoneyForFemalePerDay + vm.MoneyForMalePerDay + vm.MoneyForKittenPerDay;
            vm.MoneyForFemalePerMonth = vm.MoneyForCatPerDay * 30 + vm.PricePerVaccination;
            vm.MoneyForMalePerMonth = vm.MoneyForCatPerDay * 30 + vm.PricePerVaccination;
            vm.MoneyForKittenPerMonth = vm.MoneyForKittenPerDay * 30 + vm.PricePerVaccination;
             vm.MoneyForCatPerMonth = vm.MoneyForFemalePerMonth + vm.MoneyForMalePerMonth + vm.MoneyForKittenPerMonth;
           vm.MoneyForFemalesPerMonth = vm.MoneyForFemalePerMonth * vm.FemaleCatCount;
            vm.MoneyForMalesPerMonth = vm.MoneyForMalePerMonth * vm.MaleCatCount;
            vm.MoneyForKittensPerMonth = vm.MoneyForKittenPerMonth * vm.KittensCount;
                      vm.MoneyForCatsPerMonth = vm.MoneyForFemalesPerMonth + vm.MoneyForMalesPerMonth + vm.MoneyForKittensPerMonth;
  vm.TotalMoneyPerMonth = vm.MoneyForCatsPerMonth + vm.MoneyForKittensPerMonth;






















        }
    }
}