using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace CIImage.Converters;

public class CornerRadiusConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var processedValue = value switch
        {
            double d => d,
            int i => i,
            _ => 0
        };

        return new CornerRadius(processedValue);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}