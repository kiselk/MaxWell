using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaxWell.Services
{
    class MediaService
    {
        private static IMediaService Instance;

        public static IMediaService getInstance()
        {
            
            if (Instance == null) Instance = DependencyService.Get<IMediaService>();
            return Instance;
        }
    }
}
