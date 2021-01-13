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
using MaxWell.CustomConstants.Recipes;
using MaxWell.Shared.Models.Foods.Plans;

namespace MaxWell.Services.Recipes
{
    public class RecipeService : IRecipeService
    {
	HttpClient client;
        public List<Recipe> Recipes  { get; private set; }
        public RecipeService()
	{
		var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
		var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

		client = new HttpClient();
        
		//client.MaxResponseContentBufferSize = 256000;
	        client.MaxResponseContentBufferSize = 10000000;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
	}

	   

        public async Task<List<Recipe>> RefreshRecipesAsync()
        {
            Recipes = new List<Recipe>();

            var uri = new Uri(string.Format(RecipeConstants.RecipesUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Recipes = JsonConvert.DeserializeObject<List<Recipe>>(content);
                }
            }
            catch (Exception ex)
            {
             
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return Recipes;
        }


	public async Task<Recipe> GetRecipeAsync(int id)
	{
            Recipe recipe = null;
	    if (id.Equals(0)) return null;
            var uri = new Uri(string.Format(RecipeConstants.RecipesUrl, id));
	    try
	    {
	        var response = await client.GetAsync(uri);
	        if (response.IsSuccessStatusCode)
	        {
	            var content = await response.Content.ReadAsStringAsync();
	            recipe = JsonConvert.DeserializeObject<Recipe>(content);
	        }
	    }
	    catch (Exception ex)
	    {
	        Debug.WriteLine(@"				ERROR {0}", ex.Message);
	        UserDialogs.Instance.AlertAsync(this.GetType() + " Exception GetRecipeAsync", ex.Message);
	           // throw ex;
            }

            return recipe;
	}


        public async Task<Recipe> SaveRecipeAsync(Recipe item, bool isNewItem)
        {
            Recipe recipe = null;
            var uri = new Uri(string.Format(RecipeConstants.RecipesUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item, new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
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
                   
                    recipe = JsonConvert.DeserializeObject<Recipe>(await response.Content.ReadAsStringAsync());
                    Debug.WriteLine(@"				TodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return recipe;
        }

        public async Task DeleteRecipeAsync(int id)
        {

            var uri = new Uri(string.Format(RecipeConstants.RecipesUrl, id));

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
