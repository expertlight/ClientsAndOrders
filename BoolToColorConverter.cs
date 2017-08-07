using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsAndOrders
{
    public class BoolToColorConverter : IValueConverter
    {
        public Brush HighlughtBrush { get; set; }
        public Brush DefaultBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter,  CultureInfo culture)
        {
            Brush HighlughtBrush = Brushes.Aqua;
            if ((bool)value)
                return HighlughtBrush;
            else
                return DefaultBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Brushes.Transparent;
        }

    }
        
}
