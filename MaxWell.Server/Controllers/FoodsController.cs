using System;
using System.Collections.Generic;
//using //SyDstem.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Models.Foods;
using MaxWell.Server.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Controllers
{
    [Route("api/[controller]")]
    public class FoodsController : Controller
    {

        private readonly MyDatabaseContext _context;
        private readonly IHostingEnvironment _env;
        public FoodsController(MyDatabaseContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public void log(string message)
        {
         //   using (StreamWriter writer = System.IO.File.AppendText("logfile.txt"))
            {
           //     writer.WriteLine(message);
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
                    FoodDescription foodDesc = null;
                    if (food.FoodDescriptionId != null)
                    {
                        foodDesc = (_context.Set<FoodDescription>().Where(info => info.FoodDescriptionId.Equals(food.FoodDescriptionId)).Include(s => s.NutritionDataCollection).FirstOrDefault());

                    }
                    else
                    {

                        foodDesc = (_context.Set<FoodDescription>().Where(info => info.Name.StartsWith(food.Name)).OrderBy(info => info.Name.Length).Include(s => s.NutritionDataCollection).FirstOrDefault());

                    }

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
        
        [HttpGet("ByVkId/{id}")]
        public IActionResult GetByVk(string id)
        {
            //if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")


            var list = _context.Food.Where(food=>food.VKUserId.Equals(id));

            foreach (Food food in list)
            {
                try
                {
                    FoodDescription foodDesc = null;
                    if (food.FoodDescriptionId != null)
                    {
                        foodDesc = (_context.Set<FoodDescription>().Where(info => info.FoodDescriptionId.Equals(food.FoodDescriptionId)).Include(s => s.NutritionDataCollection).FirstOrDefault());

                    }
                    else
                    {

                        foodDesc = (_context.Set<FoodDescription>().Where(info => info.Name.StartsWith(food.Name)).OrderBy(info => info.Name.Length).Include(s => s.NutritionDataCollection).FirstOrDefault());

                    }

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
        [HttpGet("description/{name}")]
        public IActionResult Get(string name)
        {
            List<FoodDescription> list = new List<FoodDescription>();
            List<FoodDescription> list1 = new List<FoodDescription>();
            List<FoodDescription> list2 = new List<FoodDescription>();
            log(name);
            try
                {
                    if (Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    {
                        list1 = (_context.Set<FoodDescription>().Where(info => info.NameEn.StartsWith(name)).OrderBy(info => info.FoodGroupId).ThenBy(info => info.FoodDescriptionId).Include(s => s.NutritionDataCollection).ToList());
                        list2 = (_context.Set<FoodDescription>().Where(info => info.NameEn.Contains(name)).OrderBy(info => info.FoodGroupId).ThenBy(info => info.FoodDescriptionId).Include(s => s.NutritionDataCollection).ToList());
                        foreach (FoodDescription fd in list)
                        {
                            list2.Remove(fd);
                        }
                        list.AddRange(list1);
                        list.AddRange(list2);
                      

                    }
                else
                    {
                        list1 = (_context.Set<FoodDescription>().Where(info => info.Name.StartsWith(name)).OrderBy(info => info.FoodGroupId).ThenBy(info => info.FoodDescriptionId).Include(s => s.NutritionDataCollection).ToList());
                        list2 = (_context.Set<FoodDescription>().Where(info => info.Name.Contains(name)).OrderBy(info => info.FoodGroupId).ThenBy(info => info.FoodDescriptionId).Include(s => s.NutritionDataCollection).ToList());
                        list1.AddRange(list2);
                        list = list1.Distinct().ToList();
                    }

                foreach (FoodDescription desc in list)
                    {
                        NutritionData phenylData = desc.NutritionDataCollection.FirstOrDefault(n => n.NutritionDefinitionId.Equals(508));
                        if (phenylData!=null) desc.Phenyl = phenylData.Amount1;
                        desc.NutritionDataCollection = null;
                    }
                }
                catch (Exception e)
                {
                    log(e.Message);
                } //~508~^~g~^~PHE_G~^~Phenylalanine~^~3~^~17000~
         
            return Ok(list);
        }

        [HttpGet("nutrition/{id}")]
        public async Task<IActionResult> GetInfo(string id)
        {

            List<NutritionData> list = new List<NutritionData>();
            log(id);
            try
            {
                list = (_context.Set<NutritionData>().Where(info => info.FoodDescriptionId.ToString().Equals(id)).Include(s => s.NutritionDefinition).ToList());
                log("Found " + list.Count);
            }
            catch (Exception e)
            {
                log(e.Message);
            } //~508~^~g~^~PHE_G~^~Phenylalanine~^~3~^~17000~


            return Ok(list);
        }


        [HttpGet("phenyl/getAll")]
        public IActionResult GetPhenylDescendingFoodDescAsync()
        {
            //if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            List<FoodDescription> list = new List<FoodDescription>();

            try
            {
                list = _context.FoodDescription.Where(fd=>fd.Phenyl>0).OrderBy(fd => fd.Phenyl).ToList();

               /* foreach (FoodDescription desc in list)
                {
                    if (desc.Phenyl == null)
                    {
                        NutritionData phenylData = _context.NutritionData.Where(nd=>nd.FoodDescriptionId.Equals(desc.FoodDescriptionId)).FirstOrDefault(n => n.NutritionDefinitionId.Equals(508));

                        if (phenylData != null)
                        {
                            desc.Phenyl = phenylData.Amount1;
                            _context.Update(desc);
                        var a = _context.SaveChangesAsync().Result;
                        }
                        else
                        {
                            //   list.Remove(desc);
                        }

                       
                    }
                }
             

                listDescFilled = _context.FoodDescription.OrderBy(d => d.Phenyl).Take(100).ToList();*/
            }
            catch (Exception e)
            {
                log(e.Message);
            } //~508~^~g~^~PHE_G~^~Phenylalanine~^~3~^~17000~


            return Ok(list);
        }


        public async Task<Food> SaveFoodImage(Food food)
        {
            try
            {

                var fileName = food.VKUserId + "_" + Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(".png");
                var PathWithFolderName = System.IO.Path.Combine(_env.WebRootPath, "uploads\\img");
                var fullFileName = PathWithFolderName + "/" + fileName;

                if (!Directory.Exists(PathWithFolderName))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
                }

                // string Base64String = eventMaster.BannerImage.Replace("data:image/png;base64,", "");


                /////
                ///


                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(fullFileName, FileMode.Create, FileAccess.ReadWrite))
                    {
                       // .Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = food.image;
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }

                /////
               // byte[] bytes = food.image;

                  /*  Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                        ms.Dispose();
                    }
                    
                    image.Save();
                    image.Dispose();
                 */   
                    food.image = null;
                    food.Description = "http://maxwell.vmobile.online/uploads/img/" + fileName;

                
            }
            catch (Exception e)
            {
             Console.WriteLine("ZZZException: " +e.Message);   
                if(e.InnerException!=null)
                    Console.WriteLine("ZZZException: " + e.InnerException.Message);
                Console.WriteLine(e.StackTrace);
            }

            return food;
        }


        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] Food food)
        {
      
            if (ModelState.IsValid)
            {
                if (food.image != null)
                {
                    await SaveFoodImage(food);
                }
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
                    if (food.image != null)
                    {
                        await SaveFoodImage(food);
                    }
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