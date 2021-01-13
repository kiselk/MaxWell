using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Pregnancies
{

    public class PregnancyDetailViewModel : BaseViewModel
    {

        public PregnancyDetailViewModel()
        {
        }
        public PregnancyDetailViewModel(Pregnancy pregnancy)
        {
            this.Pregnancy = pregnancy;

            MotherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(pregnancy.MotherId));
            FatherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(pregnancy.FatherId));
            MotherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(pregnancy.MotherId));
            FatherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(pregnancy.FatherId));

        }

        public NotifyTaskCompletion<string> MotherFromId { get; private set; }
        public NotifyTaskCompletion<string> FatherFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> MotherImageFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> FatherImageFromId { get; private set; }



        private Pregnancy _pregnancy;
        public Pregnancy Pregnancy
        {
            get => _pregnancy;
            set
            {
                SetProperty(ref _pregnancy, value);
            }
        }


    }
}
