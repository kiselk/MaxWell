using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Views;

namespace MaxWell.Services.Allergens
{
    public class AllergenManager
    {
        IAllergenService allergenService;

        public AllergenManager(IAllergenService service)
        {
            allergenService = service;
        }

        public Task<List<Allergen>> GetAllergensAsync ()
        {
            return allergenService.RefreshAllergensAsync ();	
        }
        public Task<Allergen> GetAllergenAsync (int id)
        {
            return allergenService.GetAllergenAsync(id);
        }
            
        public Task SaveAllergenAsync (Allergen item, bool isNewItem = false)
        {
    	return allergenService.SaveAllergenAsync (item, isNewItem);
        }
    
        public Task DeleteAllergenAsync (Allergen item)
        {
    	return allergenService.DeleteAllergenAsync(item.AllergenId);
        }
    }
}