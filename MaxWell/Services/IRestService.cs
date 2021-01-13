using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Views;

namespace MaxWell.Services
{
	public interface IRestService
	{
		Task<List<TodoItem>> RefreshDataAsync ();
	    Task<List<Person>> RefreshPersonsAsync();
	    Task<List<RemoteImage>> RefreshRemoteImagesAsync();
	    Task<RemoteImage> GetRemoteImageAsync(int id);
	    Task<Person> GetPersonByVKAsync(string id);
	    Task<Person> GetPersonByFbAsync(string id);

        Task SaveTodoItemAsync (TodoItem item, bool isNewItem);
	    Task SavePersonAsync(Person item, bool isNewItem);
	    Task<RemoteImage> SaveRemoteImageAsync(RemoteImage item, bool isNewItem);

        Task DeleteTodoItemAsync (int id);
	    Task DeletePersonAsync(int id);
	    Task DeleteRemoteImageAsync(int id);
    }
}
