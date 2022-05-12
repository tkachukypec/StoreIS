using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.IO;

namespace StoreIS.converters
{
    class RelativeToFullPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = value as string;
            if (String.IsNullOrWhiteSpace(path))
            {
                return "../resources/images/noImage.png";
            }
            try
            {
                return !File.Exists(path) ? "../resources/images/noImage.png" : Path.GetFullPath(path);
            }
            catch(Exception)
            {
                return "../resources/images/noImage.png";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
