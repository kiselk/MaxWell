using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using MaxWell.ViewModels.Persons;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Pomets
{

    public class PometDetailViewModel : BaseViewModel
    {

        public PometDetailViewModel()
        {
        }
        public PometDetailViewModel(Pomet pomet)
        {
            this.Pomet = pomet;

            MotherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(pomet.MotherId));
            FatherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(pomet.FatherId));
            MotherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(pomet.MotherId));
            FatherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(pomet.FatherId));
            KittenModelList = new ObservableCollection<CatListItemViewModel>();

        }

        public NotifyTaskCompletion<string> MotherFromId { get; private set; }
        public NotifyTaskCompletion<string> FatherFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> MotherImageFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> FatherImageFromId { get; private set; }
        private ObservableCollection<CatListItemViewModel> _kittenModelList;
        public ObservableCollection<CatListItemViewModel> KittenModelList
        {
            get => _kittenModelList;
            set { SetProperty(ref _kittenModelList, value); }
        }



        private Pomet _pomet;
        public Pomet Pomet
        {
            get => _pomet;
            set
            {
                SetProperty(ref _pomet, value);
            }
        }


    }
}
