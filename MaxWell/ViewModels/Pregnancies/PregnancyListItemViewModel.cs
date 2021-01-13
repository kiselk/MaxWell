using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Pregnancies
{
    class PregnancyListItemViewModel : BaseViewModel
    {

        public Pregnancy Pregnancy;

        public PregnancyListItemViewModel(Pregnancy pregnancy)
        {
            Pregnancy = pregnancy;


            if (pregnancy != null)
            {
                MotherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(pregnancy.MotherId));
                FatherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(pregnancy.FatherId));
                MotherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(pregnancy.MotherId));
                FatherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(pregnancy.FatherId));
            }

        }

        public DateTime PregnancyDate => Pregnancy.PregnancyDate;
        public DateTime PregnancyEndDate => Pregnancy.PregnancyEndDate;
        public int MotherId => Pregnancy.MotherId;
        public int FatherId => Pregnancy.MotherId;
        public NotifyTaskCompletion<string> MotherFromId { get; private set; }
        public NotifyTaskCompletion<string> FatherFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> MotherImageFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> FatherImageFromId { get; private set; }

    }
}