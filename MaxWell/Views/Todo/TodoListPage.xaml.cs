using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Views.Main;
using Xamarin.Forms;

namespace MaxWell.Views.Todo
{
	public partial class TodoListPage : ContentPage
	{
		bool alertShown = false;

		public TodoListPage ()
		{
			InitializeComponent ();
		    Title = "Todo".Translate();
        }

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
		    if (App.GetInstance().loggedInPerson != null)
		    { await LoadData(); }
		    else
		    {
		        {
		            UserDialogs.Instance.AlertAsync("Пользователь не авторизован, Войдите через VK");
		            // Navigation.PushAsync(new LoginPage());

		        }
		    }
        }

		void OnAddItemClicked (object sender, EventArgs e)
		{
		    if (App.GetInstance().loggedInPerson!=null)
		    {
		        var todoItem = new TodoItem()
		        {
		            //Id = Guid.NewGuid ().ToString ()
		            UserId = App.GetInstance().loggedInPerson.Id, CreatedDate = DateTime.Now
		        };
		        var todoPage = new TodoItemPage(true);
		        todoPage.BindingContext = todoItem;
		        Navigation.PushAsync(todoPage);
		    }
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var todoItem = e.SelectedItem as TodoItem;
			var todoPage = new TodoItemPage ();
			todoPage.BindingContext = todoItem;
			Navigation.PushAsync (todoPage);
		}

	    public async Task LoadData()
	    {
	        try
	        {

	            listView.ItemsSource = await App.TodoManager.GetTasksAsync();
	        }
	        catch (Exception e)
	        {
	            UserDialogs.Instance.AlertAsync(e.Message, this.GetType() + " Appearing Error");
	        }
        }

	    private bool _isRefreshing = false;
	    public bool IsRefreshing
	    {
	        get { return _isRefreshing; }
	        set
	        {
	            _isRefreshing = value;
	            OnPropertyChanged(nameof(IsRefreshing));
	        }
	    }
	    public ICommand RefreshCommand
	    {
	        get
	        {
	            return new Command(async () =>
	            {
	                IsRefreshing = true;

	                await LoadData();

	                IsRefreshing = false;
	            });
	        }
	    }
    }
}
