using System;
using System.Net.Http;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MaxWell.Views.Main.Deprecated.UI
{
    public partial class ImageWrapLayoutPage : ContentPage
    {
        private string Query = "Апельсины сырые";
        public ImageWrapLayoutPage()
        {
            InitializeComponent();
        }
        public ImageWrapLayoutPage(string query)
        {
            InitializeComponent();
            Query = query;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var images = await GetImageListAsync2();
            foreach (var photo in images.Photos)
            {
                var image = new CachedImage
                {
                    Source = ImageSource.FromUri(new Uri(photo ))
                };
                 
                wrapLayout.Children.Add(image);
                
            }
        }

        async Task<ImageList> GetImageListAsync()
        {
            var requestUri = "https://docs.xamarin.com/demo/stock.json";
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(requestUri);
                return JsonConvert.DeserializeObject<ImageList>(result);
            }
        }

        async Task<ImageList> GetImageListAsync2()
        {
            return GoogleService.getInstance().SearchImages(Query);
        }
       
    }
}