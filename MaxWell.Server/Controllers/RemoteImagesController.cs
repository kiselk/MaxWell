using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Server.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxWell.Server.Controllers
{
    [Route("api/[controller]")]
    public class RemoteImagesController : Controller
    {

        private readonly MyDatabaseContext _context;
        private readonly IHostingEnvironment _env;
        public RemoteImagesController(MyDatabaseContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult List()
        {
        
            return Ok(_context.RemoteImage.ToList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            var person = await _context.RemoteImage
                .SingleOrDefaultAsync(m => m.Id.ToString() == id);

            return Ok(person);
        }




        public async Task<RemoteImage> SaveFoodImage(RemoteImage RemoteImage)
        {
            try
            {

                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(".png");
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
                        byte[] bytes = RemoteImage.DownloadedImageBlob;
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
                RemoteImage.DownloadedImageBlob = null;
                RemoteImage.ImageUrl = "http://maxwell.vmobile.online/uploads/img/" + fileName;


            }
            catch (Exception e)
            {
                Console.WriteLine("ZZZException: " + e.Message);
                if (e.InnerException != null)
                    Console.WriteLine("ZZZException: " + e.InnerException.Message);
                Console.WriteLine(e.StackTrace);
            }

            return RemoteImage;
        }





        [HttpPost]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromBody([FromBody] RemoteImage remoteImage)
        {
      
            if (ModelState.IsValid)
            {
                if (remoteImage.DownloadedImageBlob != null)
                {
                    await SaveFoodImage(remoteImage);
                }
                _context.Add(remoteImage);
                await _context.SaveChangesAsync();
         
            }
        
            return Ok(remoteImage);
        }
   
        [HttpPut]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFromBody([FromBody] RemoteImage remoteImage)
        {
        
            if (ModelState.IsValid)
            {
                try
                {
                    if (remoteImage.DownloadedImageBlob != null)
                    {
                        await SaveFoodImage(remoteImage);
                    }
                    _context.Update(remoteImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RemoteImageExists(remoteImage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(remoteImage);
            }
            return Ok(remoteImage);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
           // if (id == null)
          //  {
           //     return NotFound();
          //  }

            int idString = Convert.ToInt32(id);
            var person = await _context.RemoteImage
                .SingleOrDefaultAsync(m => m.Id.ToString() == id);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                _context.RemoteImage.Remove(person);
                await _context.SaveChangesAsync();
               
            }

            return Ok(person);
        }
        private bool RemoteImageExists(int id)
        {
            return _context.RemoteImage.Any(e => e.Id == id);
        }

    }

}