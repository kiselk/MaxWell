using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Views;

namespace MaxWell.Services
{
	public class PersonManager
	{
		IRestService restService;

		public PersonManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<Person>> GetTasksAsync ()
		{
			return restService.RefreshPersonsAsync ();	
		}
	    public Task<Person> GetPersonByVKAsync(string id)
	    {
	        return restService.GetPersonByVKAsync(id);
	    }
	    public Task<Person> GetPersonByFbAsync(string id)
	    {
	        return restService.GetPersonByFbAsync(id);
	    }
	    public Task SaveTaskAsync (Person item, bool isNewItem = false)
		{
			return restService.SavePersonAsync (item, isNewItem);
		}

		public Task DeleteTaskAsync (Person item)
		{
			return restService.DeletePersonAsync(item.Id);
		}
	}
}
