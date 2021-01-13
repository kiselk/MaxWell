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
    public class PlansController : Controller
    {
        private readonly MyDatabaseContext _context;
        public PlansController(MyDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
          var list = _context.Plan;
        
            foreach (Plan plan in list)
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
            List<Plan> list = new List<Plan>();
            try
                {
                list = (_context.Set<Plan>().Where(info => info.Name.Contains(name)).OrderBy(info => info.Name.Length).ThenBy(info => info.PlanId).ToList());
                }
                catch (Exception e)
                {
                 
                }
         
            return Ok(list);
        }

       
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] Plan plan)
        {
      
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
         
            }
        
            return Ok(plan);
        }
   
        [HttpPut]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] Plan plan)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.PlanId))
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
            return Ok(plan);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int idString = Convert.ToInt32(id);
            var plan = await _context.Plan
                .SingleOrDefaultAsync(m => m.PlanId.ToString() == id);
            if (plan == null)
            {
                return NotFound();
            }
            else
            {
                _context.Plan.Remove(plan);
                await _context.SaveChangesAsync();
               
            }

            return Ok();
        }
        private bool PlanExists(int id)
        {
            return _context.Plan.Any(e => e.PlanId == id);
        }

    }

}