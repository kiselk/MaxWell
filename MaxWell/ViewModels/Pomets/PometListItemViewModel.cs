using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Pomets
{
    class PometListItemViewModel : BaseViewModel
    {

        public Pomet Pomet;

        public PometListItemViewModel(Pomet pomet)
        {
            Pomet = pomet;


            if (pomet != null)
            {
                MotherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(pomet.MotherId));
                FatherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(pomet.FatherId));
                MotherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(pomet.MotherId));
                FatherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(pomet.FatherId));
            }

        }

        public DateTime PometDate => Pomet.PometDate;
        public DateTime PometEndDate => Pomet.PometEndDate;
        public TimeSpan StartTime => Pomet.StartTime;
        public TimeSpan EndTime => Pomet.EndTime;
        public int MotherId => Pomet.MotherId;
        public int FatherId => Pomet.MotherId;
        public NotifyTaskCompletion<string> MotherFromId { get; private set; }
        public NotifyTaskCompletion<string> FatherFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> MotherImageFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> FatherImageFromId { get; private set; }

    }
}