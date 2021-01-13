using System;
using System.Collections.Generic;
//using //SyDstem.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Models.Foods;
using MaxWell.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Controllers
{
    [Route("api/[controller]")]
    public class FoodDescriptionsController : Controller
    {

        private readonly MyDatabaseContext _context;

        public FoodDescriptionsController(MyDatabaseContext context)
        {
            _context = context;
        
        }
        public void log(string message)
        {
          //  using (StreamWriter writer = System.IO.File.AppendText("logfile.txt"))
            {
              //  writer.WriteLine(message);
            }

        }

        [HttpGet]
        public IActionResult List()
        {
            //if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")

            
          var list = _context.Food;
        
            foreach (Food food in list)
            {
                try
                {


                    
                    var foodDesc = (_context.Set<FoodDescription>().Where(info => info.Name.StartsWith(food.Name)).OrderBy(info => info.Name.Length).Include(s => s.NutritionDataCollection).FirstOrDefault());
                    food.Phenylalanine_g = foodDesc.NutritionDataCollection.First(n => n.NutritionDefinitionId.Equals(508)).Amount1;//508
                    food.Protein_g = foodDesc.NutritionDataCollection.First(n => n.NutritionDefinitionId.Equals(203)).Amount1; //203
                    food.Fats_g = foodDesc.NutritionDataCollection.First(n => n.NutritionDefinitionId.Equals(204)).Amount1; //204
                    food.Carbs_g = foodDesc.NutritionDataCollection.First(n => n.NutritionDefinitionId.Equals(205)).Amount1; //205
                    food.Calcium_mg = foodDesc.NutritionDataCollection.First(n => n.NutritionDefinitionId.Equals(301)).Amount1; //301

               //     food.NutritionDataCollection = foodDesc.NutritionDataCollection.ToList();


                }
                catch (Exception e)
                {
                    log(e.Message);
                }
            }

              return Ok(list.ToList());
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            List<FoodDescription> list = new List<FoodDescription>();
            log(name);
            try
                {
                list = (_context.Set<FoodDescription>().Where(info => info.Name.StartsWith(name)).OrderBy(info => info.Name.Length).ToList());
                    log("Found " + list.Count);
            }
                catch (Exception e)
                {
                    log(e.Message);
                } //~508~^~g~^~PHE_G~^~Phenylalanine~^~3~^~17000~
         
            return Ok(list);
        }

        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetInfo(string id)
        {

            var food = await _context.FoodInfo
                .SingleOrDefaultAsync(m => m.FoodInfoId.ToString() == id);

            return Ok(food);
        }
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] Food food)
        {
      
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
         
            }
        
            return Ok(food);
        }
   
        [HttpPut]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] Food food)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.FoodId))
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
            return Ok(food);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
           // if (id == null)
          //  {
           //     return NotFound();
          //  }

            int idString = Convert.ToInt32(id);
            var food = await _context.Food
                .SingleOrDefaultAsync(m => m.FoodId.ToString() == id);
            if (food == null)
            {
                return NotFound();
            }
            else
            {
                _context.Food.Remove(food);
                await _context.SaveChangesAsync();
               
            }

            return Ok();
        }
        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.FoodId == id);
        }

    }

}