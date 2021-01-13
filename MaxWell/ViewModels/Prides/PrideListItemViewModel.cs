using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Prides
{
    public class PrideListItemViewModel : BaseViewModel
    {

        public Pride Pride;

        public PrideListItemViewModel(Pride pride)
        {
            Pride = pride;


            if (pride != null)
            {
                PersonNameFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToPersonNameTask(pride.PersonId));
                PersonImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToPersonImageTask(pride.PersonId));
            }

        }

        public int PersonId => Pride.PersonId;
        public string Name => Pride.Name;
        public DateTime CreationDate => Pride.CreationDate; 
        public string Description => Pride.Description;
        public NotifyTaskCompletion<string> PersonNameFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> PersonImageFromId { get; private set; }
    
        public ImageSource ImageAsImageStream => Pride.ImageAsImageStream;
    }
}