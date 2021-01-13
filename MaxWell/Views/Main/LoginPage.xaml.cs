using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Controls.Common;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Resources;
using MaxWell.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Title = AppResources.Login;


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var service = DependencyService.Get<IVkService>();

                var fingerprints = await DependencyService.Get<IVkService>().GetFingerprints();
                var str = "FP: " + fingerprints[0];
                var str2 = "";
                this.FindByName<Label>("FPLabel").Text = str;

                await service.WakeUp();

                if (!service.IsLoggedIn())
                {
                    str2 = AppResources.NotLoggedIn;
                }
                else
                {
                    var button = this.FindByName<TransparentButton>("LoginVkButton");
                  button.Text = "Выйти из ";
                  /*   button.Text = null;
                    var icon = button.Image;
                    button.Image = null;
                    button.ContentLayout = null;

                    button.ContentLayout=   new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Right, 10);
  button.Image = icon;
                  */
                    Person person = (await App.GetInstance().GetLoggedInPerson());
                    if (person != null)
                    {
                        str2 = $"Куку, {person.Name}! Почта {person.Email}";


                    }
                }

                this.FindByName<Label>("LoginLabel").Text = str2;
            }
            catch (Exception ex)
            {
                 UserDialogs.Instance.AlertAsync( ex.Message,this.GetType() + " Error OnAppearing");
            }
        }

        async void OnLoginVkClicked(object sender, EventArgs e)


        {
            var str2 = "";
            var service = DependencyService.Get<IVkService>();
            var label = this.FindByName<Label>("LoginLabel");



            try
            {

                if (!service.IsLoggedIn())
                {
                    var loginResult = await service.Login();
                    switch (loginResult.LoginState)
                    {
                        case LoginState.Canceled:
                            // Обработать
                            break;
                        case LoginState.Success:
                            var str = $"Привет, {loginResult.FirstName}! Почта {loginResult.Email}";
                         //   UserDialogs.Instance.AlertAsync("id1" + loginResult.UserId);
                            label.Text = str;
                          await   SavePersonToDB(loginResult);
                            this.FindByName<TransparentButton>("LoginVkButton").Text = "Выйти из ";
                       //     this.FindByName<TransparentButton>("LoginVkButton").ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Right, 10);
                            break;
                        default:
                            UserDialogs.Instance.AlertAsync("Login error: ", "чтото случилось" + loginResult.ErrorString);
                            break;
                    }
                }
                else
                {
                    service.Logout();
                    var str = $"Вы вышли из ВК";
                    label.Text = str;
                    this.FindByName<TransparentButton>("LoginVkButton").Text = "Войти через ";
               //     this.FindByName<TransparentButton>("LoginVkButton").ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Right, 10);
                }
            }
            catch (Exception ex)
            {
                 UserDialogs.Instance.AlertAsync(ex.Message,this.GetType() + " Exception onloginclicked checking isloggedin");
            }
        }

        async void OnLoginFbClicked(object sender, EventArgs e)
        {
            var str2 = "";
            var service = DependencyService.Get<IFbService>();
            var label = this.FindByName<Label>("LoginLabel");


            try
            {

                if (!service.IsLoggedIn())
                {

                    var loginResult = await service.Login();

            switch (loginResult.LoginState)
            {
                case LoginState.Canceled:
                    // Обработать
                    break;
                case LoginState.Success:
                    var str = $"Привет, {loginResult.FirstName}! Почта {loginResult.Email}";
                    //   UserDialogs.Instance.AlertAsync("id1" + loginResult.UserId);
                    label.Text = str;
                    await SaveFbPersonToDB(loginResult);
                    this.FindByName<TransparentButton>("LoginFbButton").Text = "Выйти из ";
                 //   this.FindByName<TransparentButton>("LoginFbButton").ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Right, 10);
                    break;
                        default:
                    // Обработать ошибки
                    break;
            }
                }
                else
                {
                    service.Logout();
                    var str = $"Вы вышли из ВК";
                    label.Text = str;
                    this.FindByName<TransparentButton>("LoginFbButton").Text = "Войти через ";
                 //   this.FindByName<TransparentButton>("LoginFbButton").ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Right, 10);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.AlertAsync(ex.Message, this.GetType() + " Exception onloginclicked checking isloggedin");
            }
        }

        public async Task SaveFbPersonToDB(LoginResult UserProfile = null)
        {

            var service = DependencyService.Get<IFbService>();

            if (UserProfile == null) UserProfile = await service.GetUserProfile();
            //     var loginResult = await service.GetLoginResult();
            if (UserProfile != null)
            {
                Person person = null;
                try
                {
                    person = await App.PersonManager.GetPersonByFbAsync(UserProfile.UserId);
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.AlertAsync("No UserId Found", this.GetType() + " Error SavePersonToDB No Person");
                }

                if (person == null)
                {
                    try
                    {
                        person = FbHelper.ProfileToPerson(UserProfile);
                        //     UserDialogs.Instance.AlertAsync(UserProfile.UserId,this.GetType() + " SavePersonToDB DEBUG Profile.UserId");
                        //     UserDialogs.Instance.AlertAsync(person.VKUserId, this.GetType() + " SavePersonToDB DEBUG person.VKUserId");
                        if (person.image == null && (person.ImageUrl != null))
                        {
                            //  person.image = ImageHelper.ImageUrlToByteArray(person.ImageUrl);
                        }

                        await App.PersonManager.SaveTaskAsync(person, true);
                        person = await App.PersonManager.GetPersonByFbAsync(UserProfile.UserId);

                    }
                    catch (Exception e)
                    {
                        UserDialogs.Instance.AlertAsync(e.Message, this.GetType() + " SavePersonToDB Error");
                    }


                }




                App.GetInstance().loggedInPerson = person;

            }
        }

        public async Task SavePersonToDB(LoginResult UserProfile=null)
        {
            var service = DependencyService.Get<IVkService>();

            if(UserProfile==null) UserProfile = await service.GetUserProfile();
            //     var loginResult = await service.GetLoginResult();
            if (UserProfile != null)
            {
                Person person = null;
                try
                {
                    person = await App.PersonManager.GetPersonByVKAsync(UserProfile.UserId);
                }
                catch (Exception ex)
                {
                 UserDialogs.Instance.AlertAsync( "No UserId Found",this.GetType() + " Error SavePersonToDB No Person");
                }

                if (person == null)
                {
                    try
                    {
                        person = VKHelper.ProfileToPerson(UserProfile);
                   //     UserDialogs.Instance.AlertAsync(UserProfile.UserId,this.GetType() + " SavePersonToDB DEBUG Profile.UserId");
                   //     UserDialogs.Instance.AlertAsync(person.VKUserId, this.GetType() + " SavePersonToDB DEBUG person.VKUserId");
                        if (person.image == null && (person.ImageUrl != null))
                        {
                          //  person.image = ImageHelper.ImageUrlToByteArray(person.ImageUrl);
                        }

                        await App.PersonManager.SaveTaskAsync(person, true);
                        person = await App.PersonManager.GetPersonByVKAsync(UserProfile.UserId);

                    }
                    catch (Exception e)
                    {
                         UserDialogs.Instance.AlertAsync( e.Message,this.GetType() + " SavePersonToDB Error");
                    }


                }




                App.GetInstance().loggedInPerson = person;

            }
        }
    }
}