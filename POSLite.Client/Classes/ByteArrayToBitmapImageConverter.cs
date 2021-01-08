using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace POSLite.Client.Classes
{
    public class ByteArrayToBitmapImageConverter : IValueConverter
    {
        public static BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = null;
            img.StreamSource = new MemoryStream(imageByteArray);
            img.DecodePixelWidth = 50;
            img.EndInit();
            
            return img;// new Bitmap(new MemoryStream(imageByteArray));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageByteArray = value as byte[];
            if (imageByteArray == null) return null;
            return ConvertByteArrayToBitMapImage(imageByteArray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
     

    }
}
