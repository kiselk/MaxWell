using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MaxWell.Converters
{
    public abstract class ByteArrayToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value is DBNull)
                return null;
            var bArray = (byte[])value;

            var imgsrc = ImageSource.FromStream(() => {
                var ms = new MemoryStream(bArray);
                ms.Position = 0;
                return ms;
            });

            return imgsrc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
