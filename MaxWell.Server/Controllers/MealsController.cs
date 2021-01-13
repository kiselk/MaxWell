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
    public class MealsController : Controller
    {
        private readonly MyDatabaseContext _context;
        public MealsController(MyDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
          var list = _context.Meal;
        
            foreach (Meal meal in list)
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
            List<Meal> list = new List<Meal>();
            try
                {
                list = (_context.Set<Meal>().Where(info => info.Name.Contains(name)).OrderBy(info => info.Name.Length).ThenBy(info => info.MealId).ToList());
                }
                catch (Exception e)
                {
                 
                }
         
            return Ok(list);
        }

       
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] Meal meal)
        {
      
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
         
            }
        
            return Ok(meal);
        }
   
        [HttpPut]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] Meal meal)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.MealId))
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
            return Ok(meal);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int idString = Convert.ToInt32(id);
            var meal = await _context.Meal
                .SingleOrDefaultAsync(m => m.MealId.ToString() == id);
            if (meal == null)
            {
                return NotFound();
            }
            else
            {
                _context.Meal.Remove(meal);
                await _context.SaveChangesAsync();
               
            }

            return Ok();
        }
        private bool MealExists(int id)
        {
            return _context.Meal.Any(e => e.MealId == id);
        }

    }

}