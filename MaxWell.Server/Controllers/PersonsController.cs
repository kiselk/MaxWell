using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Server.Data;
using MaxWell.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {

        private readonly MyDatabaseContext _context;

        public PersonsController(MyDatabaseContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            // …code removed for brievety
            var user = new Person() { Name = model.Email, Email = model.Email };
            _context.Add(user);
            int personId =await _context.SaveChangesAsync();
            if (personId != 0)
                return Ok();
            else return null;
            // …code removed for brievety 
        }


        [HttpGet]
        public IActionResult List()
        {
        
            return Ok(_context.Person.ToList());
        }
        [HttpGet("vk/{id}")]
        public async Task<IActionResult> GetVk(string id)
        {

            var person = await _context.Person
                .SingleOrDefaultAsync(m => m.VKUserId == id);

            return Ok(person);
        }
        [HttpGet("fb/{id}")]
        public async Task<IActionResult> GetFb(string id)
        {

            var person = await _context.Person
                .SingleOrDefaultAsync(m => m.FbUserId == id);

            return Ok(person);
        }
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] Person person)
        {
      
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
         
            }
        
            return Ok(person);
        }
   
        [HttpPut]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] Person person)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return Ok(person);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
           // if (id == null)
          //  {
           //     return NotFound();
          //  }

            int idString = Convert.ToInt32(id);
            var person = await _context.Person
                .SingleOrDefaultAsync(m => m.Id.ToString() == id);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
               
            }

            return Ok();
        }
        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }

    }

}