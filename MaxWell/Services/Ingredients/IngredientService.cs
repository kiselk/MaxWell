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
using MaxWell.CustomConstants.Ingredients;
using MaxWell.Shared.Models.Foods.Plans;

namespace MaxWell.Services.Ingredients
{
    public class IngredientService : IIngredientService
    {
	HttpClient client;
        public List<Ingredient> Ingredients  { get; private set; }
        public IngredientService()
	{
		var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
		var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

		client = new HttpClient ();
		//client.MaxResponseContentBufferSize = 256000;
	        client.MaxResponseContentBufferSize = 10000000;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
	}

	   

        public async Task<List<Ingredient>> RefreshIngredientsAsync()
        {
            Ingredients = new List<Ingredient>();

            var uri = new Uri(string.Format(IngredientConstants.IngredientsUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(content);
                }
            }
            catch (Exception ex)
            {
             
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return Ingredients;
        }


	public async Task<Ingredient> GetIngredientAsync(int id)
	{
            Ingredient ingredient = null;
	    if (id.Equals(0)) return null;
            var uri = new Uri(string.Format(IngredientConstants.IngredientsUrl, id));
	    try
	    {
	        var response = await client.GetAsync(uri);
	        if (response.IsSuccessStatusCode)
	        {
	            var content = await response.Content.ReadAsStringAsync();
	            ingredient = JsonConvert.DeserializeObject<Ingredient>(content);
	        }
	    }
	    catch (Exception ex)
	    {
	        Debug.WriteLine(@"				ERROR {0}", ex.Message);
	        UserDialogs.Instance.AlertAsync(this.GetType() + " Exception GetIngredientAsync", ex.Message);
	           // throw ex;
            }

            return ingredient;
	}

        public async Task<List<Ingredient>> GetIngredientsByRecipeId(int id)
        {
          
            Ingredients = new List<Ingredient>();
            if (id==null) return new List<Ingredient>(); 

            var uri = new Uri(string.Format(IngredientConstants.IngredientsByRecipeIdUrl, id));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(content);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                UserDialogs.Instance.AlertAsync(this.GetType() + " Exception getIngredientsByRecipeId", ex.Message);
            }

            return Ingredients;
        }
        public async Task SaveIngredientAsync(Ingredient item, bool isNewItem)
        {

            var uri = new Uri(string.Format(IngredientConstants.IngredientsUrl, string.Empty));

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

        public async Task DeleteIngredientAsync(int id)
        {

            var uri = new Uri(string.Format(IngredientConstants.IngredientsUrl, id));

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
