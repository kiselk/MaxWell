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
    public class IngredientsController : Controller
    {
        private readonly MyDatabaseContext _context;
        public IngredientsController(MyDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
          var list = _context.Ingredient;
        
            foreach (Ingredient ingredient in list)
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
            List<Ingredient> list = new List<Ingredient>();
            try
                {
                list = (_context.Set<Ingredient>().Where(info => info.Name.Contains(name)).OrderBy(info => info.Name.Length).ThenBy(info => info.IngredientId).ToList());
                }
                catch (Exception e)
                {
                 
                }
         
            return Ok(list);
        }

        [HttpGet("recipe/{id}")]
        public IActionResult GetIngredientsByRecipeId(string id)
        {
            var list = _context.Ingredient.Where(p=>p.RecipeId.ToString().Equals(id)).ToList();

            foreach (Ingredient ingredient in list)
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
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] Ingredient ingredient)
        {
      
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
         
            }
        
            return Ok(ingredient);
        }
   
        [HttpPut]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] Ingredient ingredient)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.IngredientId))
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
            return Ok(ingredient);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int idString = Convert.ToInt32(id);
            var ingredient = await _context.Ingredient
                .SingleOrDefaultAsync(m => m.IngredientId.ToString() == id);
            if (ingredient == null)
            {
                return NotFound();
            }
            else
            {
                _context.Ingredient.Remove(ingredient);
                await _context.SaveChangesAsync();
               
            }

            return Ok();
        }
        private bool IngredientExists(int id)
        {
            return _context.Ingredient.Any(e => e.IngredientId == id);
        }

    }

}