using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Practice4
{
    public class StringToAllowedNubmerSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || !(parameter is string))
            {
                return null;
            }
            if (value == null || !int.TryParse(parameter.ToString(), out int par))
            { 
                return null; 
            }
            if (value is string)
            {
                string val = value.ToString();
                if (val.Length >= par)
                {
                    return val.Insert(par, "...").Remove(par + 3);
                }
                return val;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
