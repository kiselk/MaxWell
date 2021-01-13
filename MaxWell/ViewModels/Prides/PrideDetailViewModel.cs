using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MaxWell.Models;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Prides
{
	public class PrideDetailViewModel : BaseViewModel
    {


        public PrideDetailViewModel()
        {
        }

        public PrideDetailViewModel(Pride vyazka)
        {
            this.Pride = vyazka;


        }



        private Pride _vyazka;

        public Pride Pride
        {
            get => _vyazka;
            set { SetProperty(ref _vyazka, value); }
        }
    }
}