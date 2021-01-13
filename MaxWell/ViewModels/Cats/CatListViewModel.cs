using MaxWell.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Cats
{
    class CatListViewModel: BaseViewModel
    {
        public List<Cat> Items { get; set; }



        public CatListViewModel()
        {
        
        }

        public Xamarin.Forms.ImageSource PhotoStream { get; set; }
      

        public async void RefreshData()
        {
            Items = await App.Database2.GetItemsAsync();


          
        }
    }
}
