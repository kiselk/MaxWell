using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Views;

namespace MaxWell.Services
{
	public class RemoteImageManager
	{
		IRestService restService;

		public RemoteImageManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<RemoteImage>> GetTasksAsync ()
		{
			return restService.RefreshRemoteImagesAsync ();	
		}
	    public Task<RemoteImage> GetTaskAsync(int id)
	    {
	        return restService.GetRemoteImageAsync(id);
	    }
        public Task<RemoteImage> SaveTaskAsync (RemoteImage item, bool isNewItem = false)
		{
			return restService.SaveRemoteImageAsync (item, isNewItem);
		}

		public Task DeleteTaskAsync (RemoteImage item)
		{
			return restService.DeleteRemoteImageAsync(item.Id);
		}
	}
}
