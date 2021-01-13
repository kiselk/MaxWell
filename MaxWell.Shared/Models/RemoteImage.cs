using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using SQLite;

using Xamarin.Forms;

namespace MaxWell.Models
{
    public class RemoteImage
    {
    
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public byte[] DownloadedImageBlob { get; set; }

        [JsonIgnore] public ImageSource DownloadedImageAsImageStream => GetImageStream();
        public ImageSource GetImageStream()
        {
            try
            {
                if (DownloadedImageBlob == null)
                    return null;

                var imageByteArray = DownloadedImageBlob;

                return ImageSource.FromStream(() => new MemoryStream(imageByteArray));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }
    }
}
