using System;
using System.Collections.Generic;
using System.Text;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaxWell.Services
{

    public static class MyStaticService
    {
        public static async Task<string> ConvertIdToCatNameTask(int id)
        {
                var data = await App.Database2.GetItemAsync(id).ConfigureAwait(true);
                return data.Text;
         
        }

        public static async Task<ImageSource> ConvertIdToCatImageTask(int id)
        {
            var data = await App.Database2.GetItemAsync(id).ConfigureAwait(true);
            return data.ImageAsImageStream;

        }

        public static async Task<ImageSource> ConvertIdToCatIconTask(int id)
        {
           
            var data = await App.Database2.GetItemAsync(id).ConfigureAwait(true);
            return data.IconAsImageStream;

        }


        public static async Task<string> ConvertIdToPersonNameTask(int id)
        {
            var data = await App.Database2.GetPersonAsync(id).ConfigureAwait(true);
            return data.Name;

        }

        public static async Task<ImageSource> ConvertIdToPersonImageTask(int id)
        {
            var data = await App.Database2.GetPersonAsync(id).ConfigureAwait(true);
            return data.ImageAsImageStream;

        }

    }
}
