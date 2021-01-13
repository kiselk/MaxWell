using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Server.Data;
using MaxWell.Shared.Models.Foods.Plans;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Controllers
{
    [Route("api/[controller]")]
    public class DishsController : Controller
    {
        private readonly MyDatabaseContext _context;
        public DishsController(MyDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
          var list = _context.Dish;
        
            foreach (Dish dish in list)
            {
                try
                { // Do Something with the list
                }
                catch (Exception e)
                {
                }
            }

              return Ok(list.ToList());
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            List<Dish> list = new List<Dish>();
            try
                {
                list = (_context.Set<Dish>().Where(info => info.Name.Contains(name)).OrderBy(info => info.Name.Length).ThenBy(info => info.DishId).ToList());
                }
                catch (Exception e)
                {
                 
                }
         
            return Ok(list);
        }

       
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] Dish dish)
        {
      
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
         
            }
        
            return Ok(dish);
        }
   
        [HttpPut]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] Dish dish)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return Ok(dish);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int idString = Convert.ToInt32(id);
            var dish = await _context.Dish
                .SingleOrDefaultAsync(m => m.DishId.ToString() == id);
            if (dish == null)
            {
                return NotFound();
            }
            else
            {
                _context.Dish.Remove(dish);
                await _context.SaveChangesAsync();
               
            }

            return Ok();
        }
        private bool DishExists(int id)
        {
            return _context.Dish.Any(e => e.DishId == id);
        }

    }

}