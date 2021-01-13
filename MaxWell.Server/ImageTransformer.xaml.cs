using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ImageMagick;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Server
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageTransformer : ContentPage
    {
        public ImageTransformer()
        {
            InitializeComponent();
        }

        /*
        public void transform()
        {
            using (var img = new MagickImage("image.jpg"))
            {
                // -fuzz XX%
                img.ColorFuzz = new Percentage(10);
                // -transparent white
                img.Transparent(MagickColors.White);
                img.Write("image.png");
            }
        }

        public byte[] MakeTransparentBackground(byte[] source)
        {
            //  byte[] output = source;
            using (var img = new MagickImage(source))//"image.jpg"))
            {
                // -fuzz XX%
                img.ColorFuzz = new Percentage(10);
                // -transparent white
                img.Transparent(MagickColors.White);
                return img.ToByteArray();
            }

            return source;

        }*/

    }
}