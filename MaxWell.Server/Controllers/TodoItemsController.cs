using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaxWell.Server.Controllers
{
    [Route("api/[controller]")]
    public class TodoItemsController : Controller
    {
        private readonly ILogger _logger;

        private readonly MyDatabaseContext _context;

        public TodoItemsController(MyDatabaseContext context, ILogger<TodoItemsController> logger)
        {
         
            log("init");
            _context = context;
            _logger = logger;
        }

        public void log(string message)
        {
         //   using (StreamWriter writer = System.IO.File.AppendText("logfile.txt"))
            {
            //    writer.WriteLine(message);
            }
          
        }

        [HttpGet]
        public IActionResult List()
        {
            List<TodoItem> output = new List<TodoItem>();
            log("list");
            try
            {
                log("begin");
               output = _context.TodoItem.ToList();
                log("end");
            }
            catch (Exception e) {
                log(e.Message);
            }

            return Ok(output);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            List<TodoItem> list = new List<TodoItem>();
            try
            {
                list = _context.TodoItem.Where(m => m.UserId.ToString() == id).ToList();
            }
            catch (Exception e)
            {
                log(e.Message);
            }

            return Ok(list);
            // var person = await _context.TodoItem
            //      .SingleOrDefaultAsync(m => m.UserId == id);

            // return Ok(person);
        }
        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] TodoItem person)
        {
            try { 
      
            if (ModelState.IsValid)
            {
                log("creating");
                _context.Add(person);
                await _context.SaveChangesAsync();
                log("done");

            }
            }
            catch (Exception e)
            {
                log(e.Message);
            }
            return Ok(person);
        }
   
        [HttpPut]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] TodoItem person)
        {try { 
        
            if (ModelState.IsValid)
            {
                log("editing");

                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        log("not found");
                        return NotFound();
                    }
                    else
                    {
                        log("not exists sonething happened");
                        throw;
                    }
                }
                return Ok(person);
            }
            else
            {
                log("invalid model");
                }
            }
            catch (Exception e)
            {
                log(e.Message);
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
            var person = await _context.TodoItem
                .SingleOrDefaultAsync(m => m.Id.ToString() == id);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                _context.TodoItem.Remove(person);
                await _context.SaveChangesAsync();
               
            }

            return Ok();
        }
        private bool PersonExists(int id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }

    }

}