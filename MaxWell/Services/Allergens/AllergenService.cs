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
using MaxWell.CustomConstants.Allergens;

namespace MaxWell.Services.Allergens
{
    public class AllergenService : IAllergenService
    {
	HttpClient client;
        public List<Allergen> Allergens  { get; private set; }
        public AllergenService()
	{
		var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
		var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

		client = new HttpClient ();
		//client.MaxResponseContentBufferSize = 256000;
	        client.MaxResponseContentBufferSize = 10000000;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
	}

	   

        public async Task<List<Allergen>> RefreshAllergensAsync()
        {
            Allergens = new List<Allergen>();

            var uri = new Uri(string.Format(AllergenConstants.AllergensUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Allergens = JsonConvert.DeserializeObject<List<Allergen>>(content);
                }
            }
            catch (Exception ex)
            {
             
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                throw ex;
            }

            return Allergens;
        }


	public async Task<Allergen> GetAllergenAsync(int id)
	{
            Allergen allergen = null;
	    if (id.Equals(0)) return null;
            var uri = new Uri(string.Format(AllergenConstants.AllergensUrl, id));
	    try
	    {
	        var response = await client.GetAsync(uri);
	        if (response.IsSuccessStatusCode)
	        {
	            var content = await response.Content.ReadAsStringAsync();
	            allergen = JsonConvert.DeserializeObject<Allergen>(content);
	        }
	    }
	    catch (Exception ex)
	    {
	        Debug.WriteLine(@"				ERROR {0}", ex.Message);
	        UserDialogs.Instance.AlertAsync(this.GetType() + " Exception GetAllergenAsync", ex.Message);
	           // throw ex;
            }

            return allergen;
	}


        public async Task SaveAllergenAsync(Allergen item, bool isNewItem)
        {

            var uri = new Uri(string.Format(AllergenConstants.AllergensUrl, string.Empty));

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

        public async Task DeleteAllergenAsync(int id)
        {

            var uri = new Uri(string.Format(AllergenConstants.AllergensUrl, id));

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
