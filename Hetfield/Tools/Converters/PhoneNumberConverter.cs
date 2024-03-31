using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using Hetfield;
using Hetfield.Tools;
using Hetfield.Tools.Converters;
using Hetfield.Tools.MVVMTools;
using Xceed.Wpf.Toolkit;

namespace Hetfield.Tools.Converters
{
    class PhoneNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            var str = value as string;
            Regex regex = new Regex("\\+7\\(\\d\\d\\d\\)\\d\\d\\d-\\d\\d-\\d\\d");
            string output = string.Empty;
            if (regex.IsMatch(str))
                return str;
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            var str = value as string;
            Regex regex = new Regex("\\+7\\(\\d\\d\\d\\)\\d\\d\\d-\\d\\d-\\d\\d");
            string output = string.Empty;
            if (regex.IsMatch(str))
                return str;
            return string.Empty;
        }
    }
}
