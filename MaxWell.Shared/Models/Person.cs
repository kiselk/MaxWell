using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;
using MaxWell.Shared.Models.Foods.Plans;

namespace MaxWell.Models
{
   public class Person
    {
        [PrimaryKey, AutoIncrement]
        //Id,Name,Description,Birthday,VK,Cats,Pitomniki,Gender,icon,image
        public int Id { get; set; }
         public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Birthday { get; set; }

        public string Phone { get; set; }
        public string VK { get; set; }

        public string Cats { get; set; }
        public string Pitomniki { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string VKUserId { get; set; }
        public string FbUserId { get; set; }

        public byte[] icon { get; set; }
        public byte[] image { get; set; }

        public int? PlanId { get; set; }
        public Plan Plan { get; set; }

        [JsonIgnore]
        public ImageSource IconAsImageStream => GetIconStream();
        [JsonIgnore]
        public ImageSource ImageAsImageStream => GetImageStream();
      

        public ImageSource GetImageStream()
        {
            try
            {
                if (image == null)
                    return null;

                var imageByteArray = image;

                return ImageSource.FromStream(() => new MemoryStream(imageByteArray));
            }
            catch (Exception e)
            {
            
                return null;
            }
        }

        public ImageSource GetIconStream()
        {
            try
            {
                if (icon == null)
                    return null;

                var imageByteArray = icon;

                return ImageSource.FromStream(() => new MemoryStream(imageByteArray));
            }
            catch (Exception e)
            {
               
                return null;
            }
        }
     
}
}
