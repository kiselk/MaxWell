using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;

using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using String = System.String;

namespace MaxWell.Views.Calendar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SFCalendarViewPage : ContentPage
	{
	    CalendarEventCollection calendarEventCollection;
	    List<String> subjectCollection, colorCollection, subjectCollection2, colorCollection2;
	    List<DateTime> startTimeCollection, endTimeCollection, startTimeCollection2, endTimeCollection2;

        double width;
        public SFCalendarViewPage ()
		{
			InitializeComponent ();
		    this.sampleSettings();
		    Title = "Календарь";

		}
	    void Handle_OnCalendarTapped(object sender, Syncfusion.SfCalendar.XForms.CalendarTappedEventArgs args)
	    {
	        SfCalendar calendar = args.Calendar;
	        DateTime date = args.datetime;
	    }
	    public NotifyTaskCompletion<ImageSource> MotherImageFromId { get; private set; }

        void sampleSettings()
	    {
	        width = 800;
	        MonthViewSettings monthSettings = new MonthViewSettings();
	        monthSettings.DayHeight = 50;
	        monthSettings.HeaderBackgroundColor = Color.White;
	        monthSettings.InlineBackgroundColor = Color.FromHex("#F5F5F5");
	        monthSettings.DateSelectionColor = Color.FromHex("#E0E0E0");
	        monthSettings.TodayTextColor = Color.FromHex("#2196F3");
	        SfCalendar.MonthViewSettings = monthSettings;
	        if (Device.RuntimePlatform == "Android" || (Device.RuntimePlatform == "UWP" && Device.Idiom == TargetIdiom.Desktop))
	        {
	            SfCalendar.HeaderHeight = 50;
	        }
	        if (Device.Idiom == TargetIdiom.Tablet)
	        {
	            width /= 2;
	        }

	        if (Device.RuntimePlatform == "iOS")
	        {
	            SfCalendar.HeaderHeight = 40;
	        }/*
	        List<Vyazka> vyazkas = App.Database2.GetVyazkasAsync().Result;
	        //   List<Cat> cats = App.Database2.GetCatsForPersonAsync(2).Result;
	        calendarEventCollection = new CalendarEventCollection();
	        foreach (Vyazka vyazka in vyazkas)
	        {
	            CalendarInlineEvent appointment = new CalendarInlineEvent();
	            appointment.Color = Color.Red;
	            appointment.StartTime = vyazka.SexDate;
	            appointment.Subject = "Вязка " + vyazka.MotherId + "+" + vyazka.FatherId;

	            calendarEventCollection.Add(appointment);
	            CalendarInlineEvent warning = new CalendarInlineEvent();
	            warning.Color = Color.Orange;
	            warning.StartTime = vyazka.SexDate.AddDays(14);
	            warning.Subject = "14 Дней назад была вязка" + vyazka.MotherId + "+" + vyazka.FatherId;

	            calendarEventCollection.Add(warning);
	        }

	        SfCalendar.BindingContext = calendarEventCollection;
	 */   }


	    private void ViewModePicker_SelectedIndexChanged(object sender, EventArgs e)
	    {
	       
	    }
        void swipeClicked(object sender, System.EventArgs e)
	    {
            //Restrict the swiping behaviour of calendar control has been achieved by using EnableSwiping

	        SfCalendar.EnableSwiping = !SfCalendar.EnableSwiping;
	    }
	    void navigationClicked(object sender, System.EventArgs e)
	    {
            //Restrict the swiping behaviour of calendar control has been achieved by using EnableSwiping

	        if (SfCalendar.NavigationDirection == NavigationDirection.Vertical) { SfCalendar.NavigationDirection = NavigationDirection.Horizontal; }
	        else if (SfCalendar.NavigationDirection == NavigationDirection.Horizontal) { SfCalendar.NavigationDirection = NavigationDirection.Vertical; }

        }
        void transitionClicked(object sender, System.EventArgs e)
	    {
	        if (SfCalendar.TransitionMode == TransitionMode.Scroll) { SfCalendar.TransitionMode = TransitionMode.Card; }
	        else if (SfCalendar.TransitionMode == TransitionMode.Card) { SfCalendar.TransitionMode = TransitionMode.Float; }
	        else if (SfCalendar.TransitionMode == TransitionMode.Float) { SfCalendar.TransitionMode = TransitionMode.Reveal; }
            else if (SfCalendar.TransitionMode == TransitionMode.Reveal) { SfCalendar.TransitionMode = TransitionMode.Scroll; }

	        
	    }


        private void setSubjects()
        {
            subjectCollection = new List<String>();
            subjectCollection2 = new List<String>();
            subjectCollection.Add("IT file discussion with Clara");
            subjectCollection.Add("Submit audit report");
            subjectCollection.Add("Consulting IT related queries");
            subjectCollection.Add("IT file discussion with Clara");
            subjectCollection.Add("Submit audit report");
            subjectCollection2.Add("Submit audit report");
            subjectCollection2.Add("IT file discussion with Clara");
            subjectCollection2.Add("Submit audit report");
            subjectCollection2.Add("Consulting IT related queries");
            subjectCollection2.Add("IT file discussion with Clara");

        }
        private void setColors()
        {
            colorCollection = new List<String>();
            colorCollection.Add("#FFA2C139");
            colorCollection.Add("#FFD80073");
            colorCollection.Add("#FF1BA1E2");
            colorCollection.Add("#FFE671B8");
            colorCollection.Add("#FFF09609");
            colorCollection.Add("#FF339933");
            colorCollection.Add("#FF00ABA9");
            colorCollection.Add("#FFE671B8");
            colorCollection.Add("#FF1BA1E2");
            colorCollection.Add("#FFD80073");
            colorCollection.Add("#FFA2C139");
            colorCollection.Add("#FFD80073");
            colorCollection.Add("#FF339933");
            colorCollection.Add("#FFE671B8");
            colorCollection.Add("#FF00ABA9");
            colorCollection2 = new List<String>();
            colorCollection2.Add("#FF00ABA9");
            colorCollection2.Add("#FFE671B8");
            colorCollection2.Add("#FF339933");
            colorCollection2.Add("#FF339973");
            colorCollection2.Add("#FFA2C139");
            colorCollection2.Add("#FF00ABA9");
            colorCollection2.Add("#FFA2C139");
            colorCollection2.Add("#FFA2C139");
            colorCollection2.Add("#FF1BA1E2");
            colorCollection2.Add("#FFE671B8");
            colorCollection2.Add("#FF00ABA9");
            colorCollection2.Add("#FFE671B8");
            colorCollection2.Add("#FFA2C139");
            colorCollection2.Add("#FFA2C139");
            colorCollection2.Add("#FF1BA1E2");
            colorCollection2.Add("#FFA2C139");
        }
        private void setStartTime()
        {
            startTimeCollection = new List<DateTime>();
            startTimeCollection2 = new List<DateTime>();
            DateTime currentDate = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                DateTime startTime = new DateTime(currentDate.Year, currentDate.Month, 12 + i, 3, 0, 0);
                startTimeCollection.Add(startTime);
                startTimeCollection2.Add(startTime);
            }
            for (int i = 0; i < 5; i++)
            {
                DateTime startTime = new DateTime(currentDate.Year, currentDate.Month, 12 + i, 7, 0, 0);
                startTimeCollection.Add(startTime);
                startTimeCollection2.Add(startTime);
            }

        }
        private void setEndTime()
        {
            endTimeCollection = new List<DateTime>();
            endTimeCollection2 = new List<DateTime>();
            DateTime currentDate = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                DateTime endTime = new DateTime(currentDate.Year, currentDate.Month, 12 + i, 5, 0, 0);
                endTimeCollection.Add(endTime);
                endTimeCollection2.Add(endTime);
            }
            for (int i = 0; i < 5; i++)
            {
                DateTime endTime = new DateTime(currentDate.Year, currentDate.Month, 12 + i, 8, 0, 0);
                endTimeCollection.Add(endTime);
                endTimeCollection2.Add(endTime);
            }

        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        populate();

	    }

	    public async void populate()
	    {

	        List<Vyazka> vyazkas = await App.Database2.GetVyazkasAsync();
	        List<Cat> cats = await App.Database2.GetCatsForPersonAsync(1);
	        List<Pregnancy> pregnancies = await App.Database2.GetPregnanciesAsync();
	        List<Pomet> pomets = await App.Database2.GetPometsAsync();
	        List<Person> persons = await App.Database2.GetPersonsAsync();
	        calendarEventCollection = new CalendarEventCollection();
	        try
	        {
	            foreach (Vyazka vyazka in vyazkas)
	            {
	                CalendarInlineEvent appointment = new CalendarInlineEvent();
	                appointment.Color = Color.Red;
	                appointment.StartTime = vyazka.SexDate;
	                appointment.EndTime = vyazka.SexDate.AddHours(2);
	                string motherName = (await App.Database2.GetItemAsync(vyazka.MotherId)).Text;
	                string fatherName = (await App.Database2.GetItemAsync(vyazka.FatherId)).Text;
	                appointment.Subject = "Повязались " + motherName + "+" + fatherName;

	                calendarEventCollection.Add(appointment);
	                CalendarInlineEvent warning = new CalendarInlineEvent();
	                warning.Color = Color.Orange;
	                warning.StartTime = vyazka.SexDate.AddDays(14);
	                warning.EndTime = vyazka.SexDate.AddDays(14).AddHours(2);
	                ;
	                warning.Subject = "14 Дней назад повязались " + motherName + "+" + fatherName;

	                calendarEventCollection.Add(warning);
	            }
	        }
	        catch (Exception e)
	        {
	            await UserDialogs.Instance.AlertAsync(this.GetType() + "Vyazka Error", e.Message);
	        }

	        try
	        {
	            foreach (Cat cat in cats)
	            {
	                CalendarInlineEvent appointment = new CalendarInlineEvent();
	                appointment.Color = Color.Green;
	                appointment.StartTime = cat.BirthDate;
	                appointment.EndTime = cat.BirthDate.AddHours(2);
	                appointment.Subject = "День рождения " + cat.Text;
	                calendarEventCollection.Add(appointment);
	            }
	        }
	        catch (Exception e)
	        {
	            await UserDialogs.Instance.AlertAsync(this.GetType() + "Cat Error", e.Message);
	        }

	        try
	        {
	            foreach (Person person in persons)
	            {
	                CalendarInlineEvent appointment = new CalendarInlineEvent();
	                appointment.Color = Color.Blue;
	                appointment.StartTime = person.Birthday;
	                appointment.EndTime = person.Birthday.AddHours(2);
	                appointment.Subject = "День рождения " + person.Name;
	                calendarEventCollection.Add(appointment);
	            }
	        }
	        catch (Exception e)
	        {
	            await UserDialogs.Instance.AlertAsync(this.GetType() + " Person Error", e.Message);
	        }

	        try
	        {
	            foreach (Pregnancy person in pregnancies)
	            {
	                CalendarInlineEvent appointment = new CalendarInlineEvent();
	                appointment.Color = Color.Purple;
	                appointment.StartTime = person.PregnancyDate;
	                appointment.EndTime = person.PregnancyEndDate.AddHours(2);
	                appointment.Subject = "Забеременела " + person.MotherId;
	                calendarEventCollection.Add(appointment);
	            }
	        }
	        catch (Exception e)
	        {
	            await UserDialogs.Instance.AlertAsync(this.GetType() + " Pregnancy Error", e.Message);
	        }

	        try
	        {
	            foreach (Pomet person in pomets)
	            {
	                CalendarInlineEvent appointment = new CalendarInlineEvent();
	                appointment.Color = Color.Yellow;
	                appointment.StartTime = person.PometDate;
	                appointment.EndTime = person.PometEndDate.AddHours(2);
	                appointment.Subject = "Родила " + person.MotherId;


	                calendarEventCollection.Add(appointment);
	            }
	        }
	        catch (Exception e)
	        {
	            await UserDialogs.Instance.AlertAsync(this.GetType() + " Pomet Error", e.Message);
	        }

            try
	        {
	            SfCalendar.BindingContext = calendarEventCollection;
	        }
	        catch (Exception e)
	        {
	            await UserDialogs.Instance.AlertAsync(this.GetType() + " Calendar Error", e.Message);
	        }
        }


	    private void calendar_OnInlineLoaded(object sender, InlineEventArgs args)
        {
            ListView listView = new ListView();
            listView.BackgroundColor = Color.LightGray;

            listView.BindingContext = new ViewModel();

            ObservableCollection<ViewModel> ListViewCollection = new ObservableCollection<ViewModel>();

            for (int i = 0; i < args.appointments.Count; i++)
                ListViewCollection.Add(new ViewModel() { Subject = ((args.appointments as CalendarEventCollection)[i] as CalendarInlineEvent).Subject, ColorInline = ((args.appointments as CalendarEventCollection)[i] as CalendarInlineEvent).Color });

            listView.ItemsSource = ListViewCollection;
            listView.ItemTemplate = new DataTemplate(() => {

                ViewCell viewCell = new ViewCell();



                Label label = new Label();
                label.FontSize = 13;

                label.TextColor = Color.Black;
                label.Margin = new Thickness(20, 0, 20, 0);
                label.VerticalTextAlignment = TextAlignment.Center;
                label.SetBinding(Label.TextProperty, "Subject");
                viewCell.View = label;
                return viewCell;
            });

            args.View = listView;
        }
    }

    public class ViewModel
    {
        private string Sub;
        public string Subject
        {
            set { Sub = value; }
            get { return Sub; }
        }
        private Color SubColorInline;
        public Color ColorInline
        {
            set { SubColorInline = value; }
            get { return SubColorInline; }
        }
        public ViewModel(string a, Color b)
        {
            Subject = a;
            ColorInline = b;
        }
        public ViewModel()
        {

        }
    }

}
	
