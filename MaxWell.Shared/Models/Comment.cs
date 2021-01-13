using System;
using System.IO;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace MaxWell.Models
{
    public class Comment
    {
        public enum CommentType
        {
            Cat,
            Vyazka,
            Pregnancy,
            Pomet,
            Person
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int PersonId { get; set; }
        public string Text { get; set; }

        public int ObjectId { get; set; }
        public int ObjectType { get; set; }


        public byte[] icon { get; set; }
        public byte[] image { get; set; }


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