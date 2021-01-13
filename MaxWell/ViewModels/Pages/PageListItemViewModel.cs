using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Pages
{
    public class PageListItemViewModel : BaseViewModel
    {

        public ContentPage Page;

        public PageListItemViewModel(ContentPage page)
        {
            Page = page;


            if (page != null)
            {
             }

        }

        public string Title => Page.Title;
       
    }
}