using MaxWell.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Cats
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCatPage : ContentPage
    {
        public Cat Cat { get; set; }

        public NewCatPage()
        {
            InitializeComponent();

           
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var todoItem = (Cat)BindingContext;
          await App.Database2.SaveItemAsync(todoItem);
            await Navigation.PopAsync();

          //  MessagingCenter.Send(this, "AddItem", Cat);
          //  await Navigation.PopModalAsync();
        }
    }
}