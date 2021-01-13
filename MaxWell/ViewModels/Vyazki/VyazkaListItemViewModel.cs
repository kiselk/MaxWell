using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Vyazki
{
    class VyazkaListItemViewModel : BaseViewModel


    {

        public Vyazka Vyazka;

        public VyazkaListItemViewModel(Vyazka vyazka)
        {
            Vyazka = vyazka;

      
            if (vyazka != null)
            {
                MotherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(vyazka.MotherId));
                FatherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(vyazka.FatherId));
                MotherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(vyazka.MotherId));
                FatherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(vyazka.FatherId));
            }

        }

        public DateTime SexDate => Vyazka.SexDate;
        public int MotherId => Vyazka.MotherId;
        public int FatherId => Vyazka.MotherId;
        public NotifyTaskCompletion<string> MotherFromId { get; private set; }
        public NotifyTaskCompletion<string> FatherFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> MotherImageFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> FatherImageFromId { get; private set; }


    }

}
