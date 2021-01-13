using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Models;
using MaxWell.Models.Foods;
using MaxWell.Services;
using MaxWell.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaxWell.Services
{
	public class FoodService : IFoodService
    {
		HttpClient client;

        public List<FoodDescription> FoodDescriptions { get; private set; }
		    public List<Food> Foods  { get; private set; }
        public FoodService()
		{
			var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
			var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
		  

			client = new HttpClient ();
			//client.MaxResponseContentBufferSize = 256000;
		    client.MaxResponseContentBufferSize = 10000000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
		}

	   

        public async Task<List<Food>> RefreshFoodsAsync()
        {
            Foods = new List<Food>();

            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(Constants.FoodsUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Foods = JsonConvert.DeserializeObject<List<Food>>(content);
                }
            }
            catch (Exception ex)
            {
             
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return Foods;
        }
        //GetFoodByVKAsync
        public async Task<List<Food>> GetFoodsForCurrentUser()
        {
            Foods = new List<Food>();
            try
            {
                Person person = (await App.GetInstance().GetLoggedInPerson());
                if (person == null) return new List<Food>(); ;
                return await GetFoodsByVkAsync(person.VKUserId);
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);

            }
            return new List<Food>();
        }

	    public async Task<List<Food>> GetFoodsByVkAsync(string id)
	    {
            Foods = new List<Food>();
            if (id.Equals("")) return new List<Food>(); ;
	       
	       var uri = new Uri(string.Format(Constants.FoodsByVkIdUrl, id));

	        try
	        {
	            var response = await client.GetAsync(uri);
	            if (response.IsSuccessStatusCode)
	            {
	                var content = await response.Content.ReadAsStringAsync();
                    Foods = JsonConvert.DeserializeObject<List<Food>>(content);
                }
	        }
	        catch (Exception ex)
	        {

	            Debug.WriteLine(@"				ERROR {0}", ex.Message);
	            UserDialogs.Instance.AlertAsync(this.GetType() + " Exception getFoodByVK", ex.Message);
	        }

            return Foods;
	    }


        public async Task SaveFoodAsync(Food item, bool isNewItem)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(Constants.FoodsUrl,string.Empty));

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

        public async Task DeleteFoodAsync(int id)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems/{0}
            var uri = new Uri(string.Format(Constants.FoodsUrl, id));

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

        public async Task<List<FoodDescription>> GetFoodDescriptionsAsync(string name)
        {

            FoodDescriptions = new List<FoodDescription>();

                var uri = new Uri(string.Format(Constants.FoodDescsUrl, name));

                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        FoodDescriptions = JsonConvert.DeserializeObject<List<FoodDescription>>(content);
                    }
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(@"				ERROR {0}", ex.Message);
                    throw ex;
                }

                return FoodDescriptions;
           
        }

        public async Task<List<FoodDescription>> GetPhenylDescendingFoodDescAsync()
        {

            FoodDescriptions = new List<FoodDescription>();

            var uri = new Uri(string.Format(Constants.PhenylUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    FoodDescriptions = JsonConvert.DeserializeObject<List<FoodDescription>>(content);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return FoodDescriptions;

        }
        
        public async Task<List<NutritionData>> GetNutritionDataAsync(string id)
        {

            List< NutritionData > list = new List<NutritionData>();

            var uri = new Uri(string.Format(Constants.NutritionUrl, id));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<NutritionData>>(content);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return list;

        }
    }
}
