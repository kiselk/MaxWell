using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Models;
using MaxWell.Services;
using MaxWell.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaxWell.Services
{
	public class RestService : IRestService
	{
		HttpClient client;

		public List<TodoItem> Items { get; private set; }
	    public List<RemoteImage> RemoteImages { get; private set; }
	    public List<Person> Persons  { get; private set; }
        public RestService ()
		{
			var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
			var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

			client = new HttpClient ();
			//client.MaxResponseContentBufferSize = 256000;
		    client.MaxResponseContentBufferSize = 10000000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
		}

	    public async Task<List<TodoItem>> RefreshDataAsync()
	    {
	        Items = new List<TodoItem>();
	        if ((await App.GetInstance().GetLoggedInPerson()) != null)
	        {

	            try
	            {
	                string id = (await App.GetInstance().GetLoggedInPerson()).Id.ToString();
	                //    UserDialogs.Instance.AlertAsync(id);
	                var uri = new Uri(string.Format(Constants.RestUrl, id));

	                //	try {
	                var response = await client.GetAsync(uri);
	                //    UserDialogs.Instance.AlertAsync(response.StatusCode.ToString());
	                if (response.IsSuccessStatusCode)
	                {
	                    var content = await response.Content.ReadAsStringAsync();
	                    //    UserDialogs.Instance.AlertAsync(content);

	                    Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
	                }

	                //} catch (Exception ex) {
	                //		Debug.WriteLine (@"				ERROR {0}", ex.Message);
	                //	}
	            }
	            catch (Exception e)
	            {
	                UserDialogs.Instance.AlertAsync(e.Message);
	            } // RestUrl = http://developer.xamarin.com:8081/api/todoitems

	        }

	        return Items;
	    }



	    public async Task SaveTodoItemAsync (TodoItem item, bool isNewItem = false)
		{
			// RestUrl = http://developer.xamarin.com:8081/api/todoitems
			var uri = new Uri (string.Format (Constants.RestUrl, string.Empty));

			try {
				var json = JsonConvert.SerializeObject (item);
				var content = new StringContent (json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem) {
					response = await client.PostAsync (uri, content);
				} else {
					response = await client.PutAsync (uri, content);
				}
				
				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"				TodoItem successfully saved.");
				}
				
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
		}

		public async Task DeleteTodoItemAsync (int id)
		{
			// RestUrl = http://developer.xamarin.com:8081/api/todoitems/{0}
			var uri = new Uri (string.Format (Constants.RestUrl, id));

			try {
				var response = await client.DeleteAsync (uri);

				if (response.IsSuccessStatusCode) {
					Debug.WriteLine (@"				TodoItem successfully deleted.");	
				}
				
			} catch (Exception ex) {
				Debug.WriteLine (@"				ERROR {0}", ex.Message);
			}
		}

        public async Task<List<Person>> RefreshPersonsAsync()
        {
            Persons = new List<Person>();

            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(Constants.PersonsUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Persons = JsonConvert.DeserializeObject<List<Person>>(content);
                }
            }
            catch (Exception ex)
            {
             
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return Persons;
        }
        //GetPersonByVKAsync

	    public async Task<Person> GetPersonByVKAsync(string id)
	    {
	    //   UserDialogs.Instance.AlertAsync(this.GetType() + " DEBUG getPersonByVK id", id);
          Person person = null;
	        if (id.Equals("")) return null;
	       
	        // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(Constants.PersonsVkUrl, id));

	        try
	        {
	            var response = await client.GetAsync(uri);
	            if (response.IsSuccessStatusCode)
	            {
	                var content = await response.Content.ReadAsStringAsync();
                    //   var jobject = JObject.Parse(content);
	              //  await UserDialogs.Instance.AlertAsync(this.GetType() + " DEBUG getPersonByVK", content);
	                person = JsonConvert.DeserializeObject<Person>(content);
	            }
	        }
	        catch (Exception ex)
	        {

	            Debug.WriteLine(@"				ERROR {0}", ex.Message);
	            UserDialogs.Instance.AlertAsync(this.GetType() + " Exception getPersonByVK", ex.Message);
	           // throw ex;
            }

            return person;
	    }

	    public async Task<Person> GetPersonByFbAsync(string id)
	    {
	        //   UserDialogs.Instance.AlertAsync(this.GetType() + " DEBUG getPersonByVK id", id);
	        Person person = null;
	        if (id.Equals("")) return null;

	        // RestUrl = http://developer.xamarin.com:8081/api/todoitems
	        var uri = new Uri(string.Format(Constants.PersonsFbUrl, id));

	        try
	        {
	            var response = await client.GetAsync(uri);
	            if (response.IsSuccessStatusCode)
	            {
	                var content = await response.Content.ReadAsStringAsync();
	                //   var jobject = JObject.Parse(content);
	                //  await UserDialogs.Instance.AlertAsync(this.GetType() + " DEBUG getPersonByVK", content);
	                person = JsonConvert.DeserializeObject<Person>(content);
	            }
	        }
	        catch (Exception ex)
	        {

	            Debug.WriteLine(@"				ERROR {0}", ex.Message);
	            UserDialogs.Instance.AlertAsync(this.GetType() + " Exception getPersonByFb", ex.Message);
	            // throw ex;
	        }

	        return person;
	    }
        public async Task SavePersonAsync(Person item, bool isNewItem)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(Constants.PersonsUrl,string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }
        }

        public async Task DeletePersonAsync(int id)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems/{0}
            var uri = new Uri(string.Format(Constants.PersonsUrl, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }


        public async Task<List<RemoteImage>> RefreshRemoteImagesAsync()
        {
            RemoteImages = new List<RemoteImage>();

            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(Constants.RemoteImagesUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    RemoteImages = JsonConvert.DeserializeObject<List<RemoteImage>>(content);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return RemoteImages;
        }
	    public async Task<RemoteImage> GetRemoteImageAsync(int id)
	    {
	        RemoteImage RemoteImage = null;

	        // RestUrl = http://developer.xamarin.com:8081/api/todoitems
	        var uri = new Uri(string.Format(Constants.RemoteImagesUrl, id));

	        try
	        {
	            var response = await client.GetAsync(uri);
	            if (response.IsSuccessStatusCode)
	            {
	                var content = await response.Content.ReadAsStringAsync();
	             //   var jobject = JObject.Parse(content);
                    RemoteImage = JsonConvert.DeserializeObject<RemoteImage>(content);
	            }
	        }
	        catch (Exception ex)
	        {

	            Debug.WriteLine(@"				ERROR {0}", ex.Message);
	         //   throw ex;
	            UserDialogs.Instance.AlertAsync("" + this.GetType(), " Get Remote Image error");
	        }

	        return RemoteImage;
	    }
        public async Task<RemoteImage> SaveRemoteImageAsync(RemoteImage item, bool isNewItem)
        {
            RemoteImage RemoteImage = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(Constants.RemoteImagesUrl, string.Empty));
            HttpResponseMessage response;
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                 response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    
                    //   var jobject = JObject.Parse(content);
                    RemoteImage = JsonConvert.DeserializeObject<RemoteImage>(await response.Content.ReadAsStringAsync());
                    Debug.WriteLine(@"				TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return RemoteImage;
        }

        public async Task DeleteRemoteImageAsync(int id)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems/{0}
            var uri = new Uri(string.Format(Constants.RemoteImagesUrl, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				TodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }
    }
}
