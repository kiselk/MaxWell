using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Dishs
{
	public interface IDishService
	{
	    Task<List<Dish>> RefreshDishsAsync();
	    Task<Dish> GetDishAsync(int id);
	    Task SaveDishAsync(Dish dish, bool isNewItem);
	    Task DeleteDishAsync(int id);
	    
    }
}
