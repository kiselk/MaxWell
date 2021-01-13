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
using MaxWell.CustomConstants.Dishs;
using MaxWell.Shared.Models.Foods.Plans;

namespace MaxWell.Services.Dishs
{
    public class DishService : IDishService
    {
	HttpClient client;
        public List<Dish> Dishs  { get; private set; }
        public DishService()
	{
		var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
		var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

		client = new HttpClient ();
		//client.MaxResponseContentBufferSize = 256000;
	        client.MaxResponseContentBufferSize = 10000000;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
	}

	   

        public async Task<List<Dish>> RefreshDishsAsync()
        {
            Dishs = new List<Dish>();

            var uri = new Uri(string.Format(DishConstants.DishsUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Dishs = JsonConvert.DeserializeObject<List<Dish>>(content);
                }
            }
            catch (Exception ex)
            {
             
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return Dishs;
        }


	public async Task<Dish> GetDishAsync(int id)
	{
            Dish dish = null;
	    if (id.Equals(0)) return null;
            var uri = new Uri(string.Format(DishConstants.DishsUrl, id));
	    try
	    {
	        var response = await client.GetAsync(uri);
	        if (response.IsSuccessStatusCode)
	        {
	            var content = await response.Content.ReadAsStringAsync();
	            dish = JsonConvert.DeserializeObject<Dish>(content);
	        }
	    }
	    catch (Exception ex)
	    {
	        Debug.WriteLine(@"				ERROR {0}", ex.Message);
	        UserDialogs.Instance.AlertAsync(this.GetType() + " Exception GetDishAsync", ex.Message);
	           // throw ex;
            }

            return dish;
	}


        public async Task SaveDishAsync(Dish item, bool isNewItem)
        {

            var uri = new Uri(string.Format(DishConstants.DishsUrl, string.Empty));

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

        public async Task DeleteDishAsync(int id)
        {

            var uri = new Uri(string.Format(DishConstants.DishsUrl, id));

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
