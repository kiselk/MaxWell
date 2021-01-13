using System;
using System.Collections.Generic;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace MaxWell.Views.Calendar
{
   public class MyAppointment: CalendarInlineEvent

    {
        public ImageSource Image;
        public Vyazka Vyazka;

        public MyAppointment()
        {
        }

        public MyAppointment(Vyazka vyazka)
        {
            Vyazka = vyazka;


            if (vyazka != null)
            {
                MotherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(vyazka.MotherId));
                FatherFromId = new NotifyTaskCompletion<string>(MyStaticService.ConvertIdToCatNameTask(vyazka.FatherId));
                MotherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(vyazka.MotherId));
                FatherImageFromId = new NotifyTaskCompletion<ImageSource>(MyStaticService.ConvertIdToCatImageTask(vyazka.FatherId));
                Subject = "Вязка: "+ MotherFromId.Result+ "+" + FatherFromId.Result;
            }

        }
        public NotifyTaskCompletion<string> MotherFromId { get; private set; }
        public NotifyTaskCompletion<string> FatherFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> MotherImageFromId { get; private set; }
        public NotifyTaskCompletion<ImageSource> FatherImageFromId { get; private set; }

     
}
    }



