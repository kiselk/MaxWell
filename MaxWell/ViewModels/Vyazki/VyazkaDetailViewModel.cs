using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Vyazki
{
   
    public class VyazkaDetailViewModel : BaseViewModel
    {

        public VyazkaDetailViewModel()
        {
        }
        public VyazkaDetailViewModel(Vyazka vyazka)
        {
            this.Vyazka = vyazka;
        
            MotherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(vyazka.MotherId));
            FatherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(vyazka.FatherId));
            MotherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(vyazka.MotherId));
            FatherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(vyazka.FatherId));
            
        }

        public NotifyTaskCompletion<string> MotherFromId { get; private set; }
        public NotifyTaskCompletion<string> FatherFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> MotherImageFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> FatherImageFromId { get; private set; }
    


        private Vyazka _vyazka;
        public Vyazka Vyazka
        {
            get => _vyazka;
            set
            {
                SetProperty(ref _vyazka, value);
            }
        }

        
    }
}
