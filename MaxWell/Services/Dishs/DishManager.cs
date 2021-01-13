using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Dishs
{
    public class DishManager
    {
        IDishService dishService;

        public DishManager(IDishService service)
        {
            dishService = service;
        }

        public Task<List<Dish>> GetDishsAsync ()
        {
            return dishService.RefreshDishsAsync ();	
        }
        public Task<Dish> GetDishAsync (int id)
        {
            return dishService.GetDishAsync(id);
        }
            
        public Task SaveDishAsync (Dish item, bool isNewItem = false)
        {
    	return dishService.SaveDishAsync (item, isNewItem);
        }
    
        public Task DeleteDishAsync (Dish item)
        {
    	return dishService.DeleteDishAsync(item.DishId);
        }
    }
}