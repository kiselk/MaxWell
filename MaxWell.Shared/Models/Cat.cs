using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SQLite;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MaxWell.Models
{
    public class Cat    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Complete { get; set; }
        public string Pregnancies { get; set; }
        public string Gender { get; set; }
        public string isKitten { get; set; }
        public byte[] DownloadedImageBlob { get; set; }
        public byte[] DownloadedImageBlob2 { get; set; }

        public int PometId { get; set; }
        public int PrideId { get; set; }
        public int OwnerId { get; set; }
        public int PersonId { get; set; }

        public byte[] icon { get; set; }

        public byte[] image { get; set; }


        [JsonIgnore] public ImageSource DownloadedImageAsImageStream => GetDownloadedImageStream();
        [JsonIgnore] public ImageSource DownloadedImageAsImageStream2 => GetDownloadedImageStream2();
        [JsonIgnore] public ImageSource IconAsImageStream => GetIconStream();
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
                Debug.WriteLine(e);
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
                Debug.WriteLine(e);
                return null;
            }
        }
        public ImageSource GetDownloadedImageStream()
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
        public ImageSource GetDownloadedImageStream2()
        {
            try
            {
                if (DownloadedImageBlob2 == null)
                    return null;

                var imageByteArray = DownloadedImageBlob2;

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