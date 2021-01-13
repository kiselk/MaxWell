using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Views;

namespace MaxWell.Services.Allergens
{
	public interface IAllergenService
	{
	    Task<List<Allergen>> RefreshAllergensAsync();
	    Task<Allergen> GetAllergenAsync(int id);
	    Task SaveAllergenAsync(Allergen allergen, bool isNewItem);
	    Task DeleteAllergenAsync(int id);
	    
    }
}
