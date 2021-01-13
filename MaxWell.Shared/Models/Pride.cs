using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace MaxWell.Models
{
    public class Pride
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
        public DateTime CreationDate { get; set; }

        public byte[] icon { get; set; }
        public byte[] image { get; set; }

        [JsonIgnore]
        public ImageSource IconAsImageStream => GetIconStream();
        [JsonIgnore] public ImageSource ImageAsImageStream => GetImageStream();
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
