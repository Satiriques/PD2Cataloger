using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PD2Cataloger.Converters
{
    class QualityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Quality quality)
            {
                switch (quality)
                {
                    case Quality.Normal:
                        return Brushes.White;
                    case Quality.Magic:
                        return new SolidColorBrush(Color.FromRgb(123,123,255));
                    case Quality.Rare:
                        return Brushes.Yellow;
                    case Quality.Set:
                        return Brushes.Green;
                    case Quality.Unique:
                        return Brushes.Gold;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
