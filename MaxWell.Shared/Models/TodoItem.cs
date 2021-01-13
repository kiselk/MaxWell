using System;
using System.IO;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace MaxWell.Models
{
	public class TodoItem
	{
	    [PrimaryKey, AutoIncrement]
		public int Id { get; set; }
  public int UserId { get; set; }
		public string Name { get; set; }
	    public string Description { get; set; }
	  
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool Done { get; set; }
	    public byte[] icon { get; set; }

	    public byte[] image { get; set; }
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
