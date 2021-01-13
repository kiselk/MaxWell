using System;
using System.Collections.Generic;
using System.Text;

namespace MaxWell.ViewModels.Calculator
{
    public class CalculatorViewModel : BaseViewModel
    {
        public int _FemaleCatCount;
        public int _MaleCatCount;
        public int _KittensCount;
        public int _CatsCount;
        public int _PricePerBagOfFood;
        public int _KilosInBagOfFood;
        public int _PricePerKiloOfFood;
        public int _PricePerVaccination;


        public int _KilosForFemalePerDay;
        public int _KilosForMalePerDay;
        public int _KilosForKittenPerDay;
        public int _KilosForCatPerDay;
        public int _KilosForFemalesPerDay;
        public int _KilosForMalesPerDay;
        public int _KilosForKittensPerDay;
        public int _KilosForCatsPerDay;

        public int _MoneyForFemalePerDay;
        public int _MoneyForMalePerDay;
        public int _MoneyForKittenPerDay;
        public int _MoneyForCatPerDay;

        public int _MoneyForFemalePerMonth;
        public int _MoneyForMalePerMonth;
        public int _MoneyForKittenPerMonth;
        public int _MoneyForCatPerMonth;

        public int _MoneyForFemalesPerMonth;
        public int _MoneyForMalesPerMonth;
        public int _MoneyForKittensPerMonth;
        public int _MoneyForCatsPerMonth;
        public int _TotalMoneyPerMonth;
        public CalculatorViewModel()
        {

        }
        public int FemaleCatCount { get => _FemaleCatCount; set { SetProperty(ref _FemaleCatCount, value); } }
        public int MaleCatCount { get => _MaleCatCount; set { SetProperty(ref _MaleCatCount, value); } }
        public int KittensCount { get => _KittensCount; set { SetProperty(ref _KittensCount, value); } }
        public int CatsCount { get => _CatsCount; set { SetProperty(ref _CatsCount, value); } }
        public int PricePerBagOfFood { get => _PricePerBagOfFood; set { SetProperty(ref _PricePerBagOfFood, value); } }
        public int KilosInBagOfFood { get => _KilosInBagOfFood; set { SetProperty(ref _KilosInBagOfFood, value); } }
        public int PricePerKiloOfFood { get => _PricePerKiloOfFood; set { SetProperty(ref _PricePerKiloOfFood, value); } }
        public int PricePerVaccination { get => _PricePerVaccination; set { SetProperty(ref _PricePerVaccination, value); } }
        public int KilosForFemalePerDay { get => _KilosForFemalePerDay; set { SetProperty(ref _KilosForFemalePerDay, value); } }
        public int KilosForMalePerDay { get => _KilosForMalePerDay; set { SetProperty(ref _KilosForMalePerDay, value); } }
        public int KilosForCatPerDay { get => _KilosForCatPerDay; set { SetProperty(ref _KilosForCatPerDay, value); } }
        public int KilosForKittenPerDay { get => _KilosForKittenPerDay; set { SetProperty(ref _KilosForKittenPerDay, value); } }
        public int KilosForFemalesPerDay { get => _KilosForFemalesPerDay; set { SetProperty(ref _KilosForFemalesPerDay, value); } }
        public int KilosForMalesPerDay { get => _KilosForMalesPerDay; set { SetProperty(ref _KilosForMalesPerDay, value); } }
        public int KilosForCatsPerDay { get => _KilosForCatsPerDay; set { SetProperty(ref _KilosForCatsPerDay, value); } }
        public int KilosForKittensPerDay { get => _KilosForKittensPerDay; set { SetProperty(ref _KilosForKittensPerDay, value); } }
        public int MoneyForFemalePerDay { get => _MoneyForFemalePerDay; set { SetProperty(ref _MoneyForFemalePerDay, value); } }
        public int MoneyForMalePerDay { get => _MoneyForMalePerDay; set { SetProperty(ref _MoneyForMalePerDay, value); } }
        public int MoneyForFemalePerMonth { get => _MoneyForFemalePerMonth; set { SetProperty(ref _MoneyForFemalePerMonth, value); } }
        public int MoneyForMalePerMonth { get => _MoneyForMalePerMonth; set { SetProperty(ref _MoneyForMalePerMonth, value); } }
        public int MoneyForFemalesPerMonth { get => _MoneyForFemalesPerMonth; set { SetProperty(ref _MoneyForFemalesPerMonth, value); } }
        public int MoneyForMalesPerMonth { get => _MoneyForMalesPerMonth; set { SetProperty(ref _MoneyForMalesPerMonth, value); } }
        public int MoneyForKittenPerDay { get => _MoneyForKittenPerDay; set { SetProperty(ref _MoneyForKittenPerDay, value); } }
        public int MoneyForCatPerDay { get => _MoneyForCatPerDay; set { SetProperty(ref _MoneyForCatPerDay, value); } }
        public int MoneyForKittenPerMonth { get => _MoneyForKittenPerMonth; set { SetProperty(ref _MoneyForKittenPerMonth, value); } }
        public int MoneyForCatPerMonth { get => _MoneyForCatPerMonth; set { SetProperty(ref _MoneyForCatPerMonth, value); } }
        public int MoneyForKittensPerMonth { get => _MoneyForKittensPerMonth; set { SetProperty(ref _MoneyForKittensPerMonth, value); } }
        public int MoneyForCatsPerMonth { get => _MoneyForCatsPerMonth; set { SetProperty(ref _MoneyForCatsPerMonth, value); } }
        public int TotalMoneyPerMonth { get => _TotalMoneyPerMonth; set { SetProperty(ref _TotalMoneyPerMonth, value); } }

    }
}