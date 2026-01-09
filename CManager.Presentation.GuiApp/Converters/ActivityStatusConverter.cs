using System.Globalization;
using System.Windows.Data;

namespace CManager.Presentation.GuiApp.Converters;

public class ActivityStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isCompleted)
        {
            return isCompleted ? "Completed" : "Ongoing";
        }
        return "Unknown";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
