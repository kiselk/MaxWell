using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreGraphics;
using MaxWell.iOS;
using MaxWell.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]
namespace MaxWell.iOS
{
    class MediaService : IMediaService
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
           originalImage = GetColoredImage(originalImage);
            return CreateImage(originalImage, width, height);
        }

        public byte[] ResizeImage(string imagePath, float width, float height)
        {
            UIImage originalImage = ImageFormPath(imagePath);
            return CreateImage(originalImage, width, height);
        }

        private UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
                return null;

            return new UIImage(Foundation.NSData.FromArray(data));
        }

        private UIImage ImageFormPath(string path)
        {
            if (String.IsNullOrEmpty(path))
                return null;

            return new UIImage(path);
        }

        private byte[] CreateImage(UIImage image, float width, float height)
        {
            var originalHeight = image.Size.Height;
            var originalWidth = image.Size.Width;

            nfloat newHeight = 0;
            nfloat newWidth = 0;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                nfloat ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                nfloat ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            width = (float)newWidth;
            height = (float)newHeight;
         
          //  UIGraphics.BeginImageContext(new SizeF(width, height));
          //  image.Draw(new Name2.RectangleF(0, 0, width, height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            var bytesImagen = resizedImage.AsJPEG().ToArray();
            resizedImage.Dispose();
            return bytesImagen;
        }

        private UIImage GetColoredImage(UIImage image)
        {
         
            UIImage coloredImage = null;

            UIGraphics.BeginImageContext(image.Size);
            using (CGContext context = UIGraphics.GetCurrentContext())
            {
                context.TranslateCTM(0, image.Size.Height);
                context.ScaleCTM(1.0f, -1.0f);

             //   var rect = new System.Drawing.RectangleF(0, 0, 600, 200);// image.Size.Height);

                    //  context.ClipToMask(rect, image.CGImage);
      
                //context.FillRect(rect);

                coloredImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
            }
            return coloredImage;
        }
    }
}