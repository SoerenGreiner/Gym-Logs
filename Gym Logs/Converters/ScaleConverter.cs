using System.Globalization;

namespace Gym_Logs.Converters;

public class ScaleConverter : IValueConverter
{
    public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        if (value is double scale &&
            parameter != null &&
            double.TryParse(parameter.ToString(), out double baseSize))
        {
            return baseSize * scale;
        }

        return parameter ?? 14;
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
