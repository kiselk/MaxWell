using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Persons
{
    public class CatListItemViewModel : BaseViewModel
    {

        public Cat Cat;

        public CatListItemViewModel(Cat cat)
        {
            Cat = cat;


            if (cat != null)
            {
            }

        }

        public DateTime Birthday => Cat.BirthDate;

        public string Name => Cat.Name;
        public string Text => Cat.Text;
        public string Description => Cat.Description;
        public ImageSource ImageAsImageStream => Cat.ImageAsImageStream;

    }
}