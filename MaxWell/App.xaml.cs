using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Databases;
using MaxWell.Models;
using MaxWell.Resources;
using MaxWell.Services;
using MaxWell.Services.Allergens;
using MaxWell.Services.Dishs;
using MaxWell.Services.Ingredients;
using MaxWell.Services.Meals;
using MaxWell.Services.Plans;
using MaxWell.Services.Recipes;
using MaxWell.Views;
using MaxWell.Views.Main;
using Plugin.Multilingual;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;
using Xamvvm;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MaxWell
{
    public partial class App : Application
    {
        public static TodoItemManager TodoManager { get; private set; }
        public static PersonManager PersonManager { get; private set; }
        public static RemoteImageManager RemoteImageManager { get; private set; }
        public static FoodManager FoodManager { get; private set; }
        public static AllergenManager AllergenManager { get; private set; }
        public static IngredientManager IngredientManager { get; private set; }
        public static RecipeManager RecipeManager { get; private set; }
        public static DishManager DishManager { get; private set; }
        public static MealManager MealManager { get; private set; }
        public static PlanManager PlanManager { get; private set; }

        public static int PersonId = 2;
        public Person loggedInPerson;
        static Database database2;
        public static App Instance;
        public static ITextToSpeech Speech { get; set; }
        public static NavigationPage navigationPage;
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDgxNDZAMzEzNjJlMzMyZTMwRkl1WWFPRHNic2RDbFNkVS9weExvdmRXbTgvMjQ1RzhqU0VnTDB2YXVwZz0=");
            Resources = new ResourceDictionary();
            TodoManager = new TodoItemManager(new RestService());
            PersonManager = new PersonManager(new RestService());
            RemoteImageManager = new RemoteImageManager(new RestService());
            FoodManager = new FoodManager(new FoodService());
            AllergenManager = new AllergenManager(new AllergenService());

            IngredientManager = new IngredientManager(new IngredientService());
            RecipeManager = new RecipeManager(new RecipeService());
            DishManager = new DishManager(new DishService());
            MealManager = new MealManager(new MealService());
            PlanManager = new PlanManager(new PlanService());

       
            SQLitePCL.Batteries.Init();
            MainPage = new MainPage();
            navigationPage = new NavigationPage();
            Instance = this;
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            /* Microsoft.AppCenter.Analytics.TrackEvent("something meaningful", new Dictionary<string, string> {
                 { "User", Helpers.Settings.FirstName + Helpers.Settings.LastName},
                 { "Date", DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")},
                 { "AppID", DeviceInfoHelper.AppId }
             });
             Microsoft.AppCenter.Start(“ios ={ Your App Secret}; android ={ Your App secret}; uwp ={ Your App secret}”, typeof(Analytics));
             Crashes.TrackError(ex, new Dictionary<string, string> {
                 { "User", Helpers.Settings.UserName },
                 { "Date", DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm tt")},
                 { "AppID", DeviceInfoHelper.AppId },
                 { "someMeaningfulID", id }
             });*/
        }

        public static App GetInstance()
        {
            return Instance;
        }

        public async Task<Person> GetLoggedInPerson()
        {
            if (loggedInPerson == null)
                try
                {
                    string vkId = await DependencyService.Get<IVkService>().GetVkIdFromPrefs();

                    if (vkId != null)
                        loggedInPerson = await PersonManager.GetPersonByVKAsync(vkId);
                }
                catch (Exception e)
                {
                     UserDialogs.Instance.AlertAsync(e.Message);
                }
            return loggedInPerson;

        }
        public async Task<int> GetVkUserId()
        {
            if (GetLoggedInPerson() == null) return 0;
            return Convert.ToInt32((await App.GetInstance().GetLoggedInPerson()).VKUserId);

        }
        public static NavigationPage NavigationPage
        {
            get
            {
                if (navigationPage == null)
                {
                    navigationPage = new NavigationPage();
                }

                return navigationPage;
            }
        }

        public static Database Database2
        {
            get
            {
                if (database2 == null)
                {
                    database2 = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MaxWell8SQLite.db3"));
                }
                return database2;
            }
        }
        public int ResumeAtTodoId { get; set; }
        public int ResumeAtImageId { get; set; }

        protected override void OnStart()
        {

            //   C1.Xamarin.Forms.Core.LicenseManager.Key = License.Key; //Debug.WriteLine("OnStart");

            //// always re-set when the app starts
            //// users expect this (usually)
            ////			Properties ["ResumeAtTodoId"] = "";
            //if (Properties.ContainsKey("ResumeAtTodoId"))
            //{
            //	var rati = Properties["ResumeAtTodoId"].ToString();
            //	Debug.WriteLine("   rati=" + rati);
            //	if (!String.IsNullOrEmpty(rati))
            //	{
            //		Debug.WriteLine("   rati=" + rati);
            //		ResumeAtTodoId = int.Parse(rati);

            //		if (ResumeAtTodoId >= 0)
            //		{
            //			var todoPage = new TodoItemPage();
            //			todoPage.BindingContext = await Database.GetItemAsync(ResumeAtTodoId);
            //			await MainPage.Navigation.PushAsync(todoPage, false); // no animation
            //		}
            //	}
            //}
        }

        protected override void OnSleep()
        {
            //Debug.WriteLine("OnSleep saving ResumeAtTodoId = " + ResumeAtTodoId);
            //// the app should keep updating this value, to
            //// keep the "state" in case of a sleep/resume
            //Properties["ResumeAtTodoId"] = ResumeAtTodoId;
        }

        protected override void OnResume()
        {
            //Debug.WriteLine("OnResume");
            //if (Properties.ContainsKey("ResumeAtTodoId"))
            //{
            //	var rati = Properties["ResumeAtTodoId"].ToString();
            //	Debug.WriteLine("   rati=" + rati);
            //	if (!String.IsNullOrEmpty(rati))
            //	{
            //		Debug.WriteLine("   rati=" + rati);
            //		ResumeAtTodoId = int.Parse(rati);

            //		if (ResumeAtTodoId >= 0)
            //		{
            //			var todoPage = new TodoItemPage();
            //			todoPage.BindingContext = await Database.GetItemAsync(ResumeAtTodoId);
            //			await MainPage.Navigation.PushAsync(todoPage, false); // no animation
            //		}
            //	}
            //}
        }


    }
}

