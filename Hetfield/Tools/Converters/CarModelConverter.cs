using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hetfield.Tools.Converters
{
    internal class CarModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //if (value == null) return null;

            //string str = value as string;
            //for(int i = 1; i < str.Length; i++)
            //{
            //    if (char.IsDigit(str[i]) && str[i - 1] != ' ')
            //    {
            //        str = str.Insert(i, " ");
            //        break;
            //    }
            //}
            //return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //if (value == null) return null;

            //string str = value as string;
            //for (int i = 1; i < str.Length; i++)
            //{
            //    if (char.IsDigit(str[i]) && str[i - 1] != ' ')
            //    {
            //        str = str.Insert(i - 1, " ");
            //        break;
            //    }
            //}
            //return str;
        }
    }
}
