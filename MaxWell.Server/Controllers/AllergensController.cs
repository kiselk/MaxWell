using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Controllers
{
    [Route("api/[controller]")]
    public class AllergensController : Controller
    {
        private readonly MyDatabaseContext _context;
        public AllergensController(MyDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
          var list = _context.Allergen;
        
            foreach (Allergen allergen in list)
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
            List<Allergen> list = new List<Allergen>();
            try
                {
                list = (_context.Set<Allergen>().Where(info => info.Name.Contains(name)).OrderBy(info => info.Name.Length).ThenBy(info => info.AllergenId).ToList());
                }
                catch (Exception e)
                {
                 
                }
         
            return Ok(list);
        }

       
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] Allergen allergen)
        {
      
            if (ModelState.IsValid)
            {
                _context.Add(allergen);
                await _context.SaveChangesAsync();
         
            }
        
            return Ok(allergen);
        }
   
        [HttpPut]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] Allergen allergen)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allergen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllergenExists(allergen.AllergenId))
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
            return Ok(allergen);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int idString = Convert.ToInt32(id);
            var allergen = await _context.Allergen
                .SingleOrDefaultAsync(m => m.AllergenId.ToString() == id);
            if (allergen == null)
            {
                return NotFound();
            }
            else
            {
                _context.Allergen.Remove(allergen);
                await _context.SaveChangesAsync();
               
            }

            return Ok();
        }
        private bool AllergenExists(int id)
        {
            return _context.Allergen.Any(e => e.AllergenId == id);
        }

    }

}