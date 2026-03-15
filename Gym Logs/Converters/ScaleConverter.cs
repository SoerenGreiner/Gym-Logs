using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Gym_Logs.Converters;

/// <summary>
/// Converts a numeric base value (e.g., FontSize, Padding, Width, Height)
/// by multiplying it with a scale factor provided via ConverterParameter.
/// </summary>
public class ScaleConverter : IValueConverter
{
    /// <summary>
    /// Converts a base numeric value by applying a scale factor.
    /// </summary>
    /// <param name="value">
    /// The base value to scale (e.g., 14 for font size, 16 for padding).
    /// </param>
    /// <param name="targetType">
    /// The type of the binding target property (not used in this converter).
    /// </param>
    /// <param name="parameter">
    /// Optional scale factor to apply (e.g., UIScale). If null, scale factor = 1.
    /// </param>
    /// <param name="culture">
    /// Culture information (not used in this converter).
    /// </param>
    /// <returns>
    /// The scaled value. If conversion fails, returns the original value or 14 as fallback.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return 14;

        if (!double.TryParse(value.ToString(), out double baseValue))
            return 14;

        double scale = 1;
        if (parameter != null && double.TryParse(parameter.ToString(), out double p))
        {
            scale = p;
        }

        return baseValue * scale;
    }

    /// <summary>
    /// ConvertBack is not supported.
    /// </summary>
    /// <param name="value">The value from the target (ignored).</param>
    /// <param name="targetType">The type of the binding target property (ignored).</param>
    /// <param name="parameter">Converter parameter (ignored).</param>
    /// <param name="culture">Culture info (ignored).</param>
    /// <returns>Throws <see cref="NotSupportedException"/>.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}