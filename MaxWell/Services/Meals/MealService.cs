using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MaxWell.CustomConstants.Meals;
using MaxWell.Shared.Models.Foods.Plans;

namespace MaxWell.Services.Meals
{
    public class MealService : IMealService
    {
	HttpClient client;
        public List<Meal> Meals  { get; private set; }
        public MealService()
	{
		var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
		var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

		client = new HttpClient ();
		//client.MaxResponseContentBufferSize = 256000;
	        client.MaxResponseContentBufferSize = 10000000;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
	}

	   

        public async Task<List<Meal>> RefreshMealsAsync()
        {
            Meals = new List<Meal>();

            var uri = new Uri(string.Format(MealConstants.MealsUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Meals = JsonConvert.DeserializeObject<List<Meal>>(content);
                }
            }
            catch (Exception ex)
            {
             
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return Meals;
        }


	public async Task<Meal> GetMealAsync(int id)
	{
            Meal meal = null;
	    if (id.Equals(0)) return null;
            var uri = new Uri(string.Format(MealConstants.MealsUrl, id));
	    try
	    {
	        var response = await client.GetAsync(uri);
	        if (response.IsSuccessStatusCode)
	        {
	            var content = await response.Content.ReadAsStringAsync();
	            meal = JsonConvert.DeserializeObject<Meal>(content);
	        }
	    }
	    catch (Exception ex)
	    {
	        Debug.WriteLine(@"				ERROR {0}", ex.Message);
	        UserDialogs.Instance.AlertAsync(this.GetType() + " Exception GetMealAsync", ex.Message);
	           // throw ex;
            }

            return meal;
	}


        public async Task SaveMealAsync(Meal item, bool isNewItem)
        {

            var uri = new Uri(string.Format(MealConstants.MealsUrl, string.Empty));

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

        public async Task DeleteMealAsync(int id)
        {

            var uri = new Uri(string.Format(MealConstants.MealsUrl, id));

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
